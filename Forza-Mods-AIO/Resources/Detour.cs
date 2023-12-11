using System;
using System.Linq;
using System.Text;
using static Memory.Imps;
using static System.Buffer;
using static Forza_Mods_AIO.MainWindow;
using static System.Windows.Threading.Dispatcher;

namespace Forza_Mods_AIO.Resources;

public class Detour : Asm
{
    /// <summary>
    /// Setup the detour.
    /// </summary>
    /// <param name="button">Button bound to the detour</param>
    /// <param name="addr">Source address for the detour</param>
    /// <param name="detourBytes">Bytes for inside the detour</param>
    /// <param name="replaceCount">How many bytes to replace on the address of signature</param>
    /// <param name="useVarAddress">Creates an address for variables after jmp back of detour</param>
    /// <param name="varAddressOffset">Offset from the originally calculated variable address</param>
    /// <param name="saveOrigBytesToDetour">Adds the original bytes of the detour at the start of it</param>
    /// <returns>True if is setup, successfully created or false if failed at some point </returns>
    /// <throws>Exception if: sig or detour bytes are null or whitespace, replace count is less than 5</throws>
    public bool Setup(object? button,
        UIntPtr addr,
        string detourBytes,
        int replaceCount,
        bool useVarAddress = false,
        UIntPtr varAddressOffset = 0,
        bool saveOrigBytesToDetour = false)
    {
        // return true if detour was already setup
        if (IsSetup)
        {
            return true;
        }

        if (addr <= (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            return false;
        }

        _detourAddr = addr;
        
        if (string.IsNullOrWhiteSpace(detourBytes))
        {
            throw new Exception("Detour bytes argument cant be null nor whitespace");
        }

        if (replaceCount < 5)
        {
            replaceCount = 5;
        }
        
        // disable the button bound to the detour for the detour create time
        CurrentDispatcher.BeginInvoke(delegate ()
        {
            button?.GetType().GetProperty("IsEnabled")?.SetValue(button, false);
        });

        // save original bytes to toggle the hook
        _originalBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);

        // delete any spaces if user hasn't cleared them
        if (detourBytes.Contains(' '))
        {
            detourBytes = detourBytes.Replace(" ", "");
        }

        var finalDetourBytes = StringToBytes(detourBytes);

        if (saveOrigBytesToDetour)
        {
            var combinedBytes = new byte[_originalBytes.Length + finalDetourBytes.Length];
            BlockCopy(_originalBytes, 0, combinedBytes, 0, _originalBytes.Length);
            BlockCopy(finalDetourBytes, 0, combinedBytes, _originalBytes.Length, finalDetourBytes.Length);
            finalDetourBytes = combinedBytes;
        }
        
        // finally, create detour
        if (!CreateDetour(finalDetourBytes, replaceCount))
        {
            CurrentDispatcher.BeginInvoke(delegate ()
            {
                button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
            });
            return false;
        }
        
        if (useVarAddress)
        {
            // address of allocated mem + size of detour bytes + offset + size of jmp back
            VariableAddress = _allocatedAddress + (UIntPtr)finalDetourBytes.Length + varAddressOffset + 5;
        }

        // save new/jmp bytes for hook toggle
        _newBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);
        
        // then, enable the button and return true
        CurrentDispatcher.BeginInvoke(delegate ()
        {
            button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
        });
        return IsHooked = IsSetup = true;
    }
    
    /// <summary>
    /// Setup the detour.
    /// </summary>
    /// <param name="addr">Source address for the detour</param>
    /// <param name="detourBytes">Bytes for inside the detour</param>
    /// <param name="replaceCount">How many bytes to replace on the address of signature</param>
    /// <param name="useVarAddress">Creates an address for variables after jmp back of detour</param>
    /// <param name="varAddressOffset">Offset from the originally calculated variable address</param>
    /// <param name="saveOrigBytesToDetour">Adds the original bytes of the detour at the start of it</param>
    /// <returns>True if is setup, successfully created or false if failed at some point </returns>
    /// <throws>Exception if: sig or detour bytes are null or whitespace, replace count is less than 5</throws>
    public bool Setup(UIntPtr addr,
        string detourBytes,
        int replaceCount,
        bool useVarAddress = false,
        UIntPtr varAddressOffset = 0,
        bool saveOrigBytesToDetour = false)
    {
        return Setup(null, addr, detourBytes, replaceCount, useVarAddress, varAddressOffset, saveOrigBytesToDetour);
    }
    
    /// <summary>
    /// Deallocates the memory and unhooks
    /// </summary>
    public void Destroy()
    {
        if (_allocatedAddress == UIntPtr.Zero || _originalBytes == null)
        {
            return;
        }
        
        UnHook();
        VirtualFreeEx(Mw.Gvp.Process!.Handle, _allocatedAddress, 0, MemRelease);
    }

    /// <summary>
    /// Sets the detour class to default
    /// </summary>
    public void Clear()
    {
        _detourAddr = _allocatedAddress = VariableAddress = UIntPtr.Zero;
        _originalBytes = _newBytes = null;
        IsSetup = false;
        _firstTime = true;
    }
    
    public void Toggle()
    {
        if (_firstTime)
        {
            _firstTime = false;
            return;
        }
        
        var currentBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, _originalBytes.Length);
        
        if (currentBytes.SequenceEqual(_originalBytes))
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
    private void UnHook() => Mw.M.WriteArrayMemory(_detourAddr, _originalBytes);
    
    private bool CreateDetour(byte[] caveBytes, int replaceCount)
    {
        _allocatedAddress = Mw.M.CreateDetour(_detourAddr.ToString("X"), caveBytes, replaceCount);
        return _allocatedAddress != UIntPtr.Zero;
    }

    public void UpdateVariable<T>(T value, UIntPtr varOffset = 0) where T : unmanaged
    {
        if (VariableAddress == UIntPtr.Zero)
        {
            return;
        }
        
        Mw.M.WriteMemory(VariableAddress + varOffset, value);
    }
    
    public void UpdateVariable<T>(T[] value, UIntPtr varOffset = 0) where T : unmanaged
    {
        if (VariableAddress == UIntPtr.Zero)
        {
            return;
        }
        
        Mw.M.WriteArrayMemory(VariableAddress + varOffset, value);
    }
    
    public T ReadVariable<T>(UIntPtr varOffset = 0) where T : unmanaged
    {
        return VariableAddress == UIntPtr.Zero ? new T() : Mw.M.ReadMemory<T>(VariableAddress + varOffset);
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder(512);
        sb.Append("IsHooked: ").AppendLine(IsHooked.ToString());
        sb.Append("IsSetup: ").AppendLine(IsSetup.ToString());
        sb.Append("First Time Toggle: ").AppendLine(_firstTime.ToString());
        sb.Append("Detour Addr: ").AppendLine(_detourAddr.ToString("X"));
        sb.Append("Allocated Addr: ").AppendLine(_allocatedAddress.ToString("X"));
        sb.Append("Variable Addr: ").AppendLine(VariableAddress.ToString("X"));

        if (_originalBytes != null)
        {
            sb.Append("Original Bytes: ").AppendLine(BitConverter.ToString(_originalBytes).Replace("-", " "));
        }

        if (_newBytes != null)
        {
            sb.Append("New Bytes: ").AppendLine(BitConverter.ToString(_newBytes).Replace("-", " "));
        }
        
        return sb.ToString();
    }
    
    public bool IsHooked { get; private set; }
    public bool IsSetup { get; private set; }
    public UIntPtr VariableAddress { get; private set; }
    private UIntPtr _detourAddr, _allocatedAddress;
    private byte[]? _originalBytes, _newBytes;
    private bool _firstTime = true;
}