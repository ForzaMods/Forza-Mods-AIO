using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using static Memory.Imps;
using static System.Buffer;
using static Forza_Mods_AIO.MainWindow;
using static System.Windows.Threading.Dispatcher;

namespace Forza_Mods_AIO.Resources;

/// <summary>
/// Detour class automatically handling everything for you.
/// </summary>
public class Detour : Asm
{
    private readonly bool _bypassDetour;
    
    public Detour(bool bypassDetour = false)
    {
        _bypassDetour = bypassDetour;
    }

    /// <summary>
    /// Setup the detour.
    /// </summary>
    /// <param name="button">Button bound to the detour</param>
    /// <param name="addr">Source address for the detour</param>
    /// <param name="originalBytes">Original bytes string to check if the detour should be created. (Optional)</param>
    /// <param name="detourBytes">Bytes for inside the detour</param>
    /// <param name="replaceCount">How many bytes to replace on the address of signature</param>
    /// <param name="useVarAddress">Creates an address for variables after jmp back of detour</param>
    /// <param name="varAddressOffset">Offset from the originally calculated variable address</param>
    /// <param name="saveOrigBytesToDetour">Adds the original bytes of the detour at the start of it</param>
    /// <returns>True if is setup, successfully created or false if failed at some point </returns>
    /// <throws>Exception if: sig or detour bytes are null or whitespace, replace count is less than 5</throws>
    public bool Setup(object? button,
        UIntPtr addr,
        string? originalBytes,
        string detourBytes,
        int replaceCount,
        bool useVarAddress = false,
        uint varAddressOffset = 0,
        bool saveOrigBytesToDetour = false)
    {
        if (IsSetup)
        {
            return true;
        }
        
        if (string.IsNullOrWhiteSpace(detourBytes))
        {
            throw new Exception(@"Detour bytes argument cant be null nor whitespace");
        }
        
        if (replaceCount < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(replaceCount));
        }

        if (!Mw.Attached || Mw.Gvp.Process.MainModule == null)
        {
            MessageBox.Show(@"You arent attached to the game");
            return false;
        }

        if (addr <= (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            MessageBox.Show(@"The address for this feature wasn't found.");
            return false;
        }
#if !RELEASE
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Starting to hook. Addr: {addr:X}");
#endif

        _detourAddr = addr;
        ToggleButton(button, false);
        _realOriginalBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);

        if (originalBytes == null)
        {
            goto skipOriginalBytesCheck;
        }
        
        var bytes = StringToBytes(originalBytes);
            
        if (bytes.Length != replaceCount)
        {
            throw new ArgumentException(@"Original bytes length should be equal to the replace count");
        }

        if (_realOriginalBytes.Where((t, i) => t != bytes[i]).Any())
        {
            MessageBox.Show(@"Original bytes didnt match");
            ToggleButton(button, true);
            return false;
        }
       
        skipOriginalBytesCheck:
        var finalDetourBytes = StringToBytes(detourBytes);

        if (saveOrigBytesToDetour)
        {
            var combinedBytes = new byte[_realOriginalBytes.Length + finalDetourBytes.Length];
            BlockCopy(_realOriginalBytes, 0, combinedBytes, 0, _realOriginalBytes.Length);
            BlockCopy(finalDetourBytes, 0, combinedBytes, _realOriginalBytes.Length, finalDetourBytes.Length);
            finalDetourBytes = combinedBytes;
        }

        var isFh4 = Mw.Gvp.Type == GameVerPlat.GameType.Fh4;
        
        if (!isFh4 && !_bypassDetour && !Bypass.DisableAntiCheat())
        {
            MessageBox.Show(@"Failed to disable anti-cheat");
            ToggleButton(button, true);
            return false;
        }

        if (Mw.Gvp.Type == GameVerPlat.GameType.Fm8)
        {
            Bypass.AddProtectAddress(addr);
        }
            
        if (!CreateDetour(finalDetourBytes, replaceCount))
        {
            MessageBox.Show(@"Failed to create the detour");
            ToggleButton(button, true);
            return false;
        }

        if (useVarAddress)
        {
            VariableAddress = AllocatedAddress + (UIntPtr)finalDetourBytes.Length + varAddressOffset + 5;
        }
        
#if !RELEASE
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Finished hooking. Addr: {addr:X}\nVar Addr: {VariableAddress:X}, Allocated Addr: {AllocatedAddress:X}");
#endif

        _newBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);
        ToggleButton(button, true);
        return IsHooked = IsSetup = true;
    }

    private static void ToggleButton(object? button, bool enable)
    {
        CurrentDispatcher.BeginInvoke(delegate ()
        {
            button?.GetType().GetProperty("IsEnabled")?.SetValue(button, enable);
        });
    }

    /// <summary>
    /// Setup the detour.
    /// </summary>
    /// <param name="addr">Source address for the detour</param>
    /// <param name="originalBytes">Original bytes string to check if the detour should be created. (Optional)</param>
    /// <param name="detourBytes">Bytes for inside the detour</param>
    /// <param name="replaceCount">How many bytes to replace on the address of signature</param>
    /// <param name="useVarAddress">Creates an address for variables after jmp back of detour</param>
    /// <param name="varAddressOffset">Offset from the originally calculated variable address</param>
    /// <param name="saveOrigBytesToDetour">Adds the original bytes of the detour at the start of it</param>
    /// <returns>True if is setup, successfully created or false if failed at some point </returns>
    /// <throws>Exception if: sig or detour bytes are null or whitespace, replace count is less than 5</throws>
    public bool Setup(UIntPtr addr,
        string? originalBytes,
        string detourBytes,
        int replaceCount,
        bool useVarAddress = false,
        uint varAddressOffset = 0,
        bool saveOrigBytesToDetour = false)
    {
        return Setup(null, addr, originalBytes, detourBytes, replaceCount, useVarAddress,varAddressOffset, saveOrigBytesToDetour);
    }
    
    /// <summary>
    /// Deallocates the memory and unhooks
    /// </summary>
    public void Destroy()
    {
        if (AllocatedAddress == UIntPtr.Zero || _realOriginalBytes == null)
        {
            return;
        }
        
        UnHook();

        if (_bypassDetour)
        {
            Task.Delay(25).Wait();
        }
        
        VirtualFreeEx(Mw.Gvp.Process!.Handle, AllocatedAddress, 0, MemRelease);
    }

    /// <summary>
    /// Sets the detour class to default
    /// </summary>
    public void Clear()
    {
        _detourAddr = AllocatedAddress = VariableAddress = UIntPtr.Zero;
        _realOriginalBytes = _newBytes = null;
        IsSetup = false;
        _firstTime = true;
    }
    
    public void Toggle()
    {
        if (_firstTime)
        {
            IsHooked = true;
            _firstTime = false;
            return;
        }

        if (!IsSetup || _realOriginalBytes == null)
        {
            return;
        }
        
        Mw.Gvp.Process.Refresh();
        var currentBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, _realOriginalBytes.Length);
        
        if (currentBytes.SequenceEqual(_realOriginalBytes))
        {
            Hook();
        }
        else
        {
            UnHook();
        }

        IsHooked = !IsHooked;
    }

    private void Hook() => Mw.M.WriteArrayMemory(_detourAddr, _newBytes);
    private void UnHook() => Mw.M.WriteArrayMemory(_detourAddr, _realOriginalBytes);
    
    private bool CreateDetour(byte[] caveBytes, int replaceCount)
    {
        AllocatedAddress = Mw.M.CreateDetour(_detourAddr.ToString("X"), caveBytes, replaceCount);
        return AllocatedAddress != UIntPtr.Zero;
    }

    public void UpdateVariable<T>(T value, uint varOffset = 0) where T : unmanaged
    {
        if (VariableAddress == UIntPtr.Zero || !IsSetup)
        {
            return;
        }
        
        Mw.Gvp.Process.Refresh();
        Mw.M.WriteMemory(VariableAddress + varOffset, value);
    }
    
    public void UpdateVariable<T>(T[] value, uint varOffset = 0) where T : unmanaged
    {
        if (VariableAddress == UIntPtr.Zero || !IsSetup)
        {
            return;
        }
        
        Mw.Gvp.Process.Refresh();
        Mw.M.WriteArrayMemory(VariableAddress + varOffset, value);
    }
    
    public T ReadVariable<T>(uint varOffset = 0) where T : unmanaged
    {
        return VariableAddress == UIntPtr.Zero || !IsSetup ? new T() : Mw.M.ReadMemory<T>(VariableAddress + varOffset);
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder(512);
        sb.Append("IsHooked: ").AppendLine(IsHooked.ToString());
        sb.Append("IsSetup: ").AppendLine(IsSetup.ToString());
        sb.Append("First Time Toggle: ").AppendLine(_firstTime.ToString());
        sb.Append("Detour Addr: ").AppendLine(_detourAddr.ToString("X"));
        sb.Append("Allocated Addr: ").AppendLine(AllocatedAddress.ToString("X"));
        sb.Append("Variable Addr: ").AppendLine(VariableAddress.ToString("X"));

        if (_realOriginalBytes != null)
        {
            sb.Append("Original Bytes: ").AppendLine(BitConverter.ToString(_realOriginalBytes).Replace("-", " "));
        }

        if (_newBytes != null)
        {
            sb.Append("New Bytes: ").AppendLine(BitConverter.ToString(_newBytes).Replace("-", " "));
        }
        
        return sb.ToString();
    }

    public static void FailedHandler(object @switch, RoutedEventHandler action, bool fh4 = false)
    {
        if (@switch is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        Application.Current.Dispatcher.Invoke(() =>
        {
            toggleSwitch.Toggled -= action;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += action;
        });

        const string fh4String = "This feature was never ported to FH4";
        const string failedString = "Failed";
        MessageBox.Show(fh4 ? fh4String : failedString);
    }
    
    public bool IsHooked { get; private set; }
    public bool IsSetup { get; private set; }
    public UIntPtr VariableAddress { get; private set; }
    public UIntPtr AllocatedAddress { get; private set; }
    private UIntPtr _detourAddr;
    private byte[]? _realOriginalBytes, _newBytes;
    private bool _firstTime = true;
}