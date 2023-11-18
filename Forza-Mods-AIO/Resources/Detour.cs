using System;
using System.Linq;
using static System.Buffer;
using static Forza_Mods_AIO.MainWindow;
using static Memory.Imps;

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
    /// <returns>True if is setup, successfully created or false if failed at some point</returns>
    /// <throws>Exception if: sig is null or whitespace, detour bytes are null, replace count is less than 5</throws>
    public bool Setup(object? button,
        UIntPtr addr,
        byte[] detourBytes,
        int replaceCount,
        bool useVarAddress = false,
        UIntPtr varAddressOffset = 0,
        bool saveOrigBytesToDetour = false)
    {
        // return true if detour was already setup
        if (_isSetup)
        {
            return true;
        }

        if (addr <= 0)
        {
            throw new Exception("Addr argument is invalid");
        }
        
        _detourAddr = addr;
        
        if (detourBytes == null)
        {
            throw new Exception("Detour bytes argument cant be null");
        }

        if (replaceCount < 5)
        {
            replaceCount = 5;
        }
        
        // disable the button bound to the detour for the detour create time
        button?.GetType().GetProperty("IsEnabled")?.SetValue(button, false);

        // save original bytes to toggle the hook
        _originalBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);

        // finally, create detour
        if (!CreateDetour(detourBytes, replaceCount))
        {
            button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
            return false;
        }

        
        if (saveOrigBytesToDetour)
        {
            BlockCopy(_originalBytes, 0, detourBytes, 0, _originalBytes.Length);
        }

        
        if (useVarAddress)
        {
            // address of allocated mem + size of detour bytes + offset + size of jmp back
            _variableAddress = _allocatedAddress + (UIntPtr)detourBytes.Length + varAddressOffset + 5;
        }

        // save new/jmp bytes for hook toggle
        _newBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);
        
        // then, enable the button and return true
        button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);        
        return IsHooked = _isSetup = true;
    }

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
        if (_isSetup)
        {
            return true;
        }

        if (addr <= 0)
        {
            throw new Exception("Addr argument is invalid");
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
        button?.GetType().GetProperty("IsEnabled")?.SetValue(button, false);

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
            BlockCopy(_originalBytes, 0, finalDetourBytes, 0, _originalBytes.Length);
        }
        
        // finally, create detour
        if (!CreateDetour(finalDetourBytes, replaceCount))
        {
            button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
            return false;
        }
        
        if (useVarAddress)
        {
            // address of allocated mem + size of detour bytes + offset + size of jmp back
            _variableAddress = _allocatedAddress + (UIntPtr)finalDetourBytes.Length + varAddressOffset + 5;
        }

        // save new/jmp bytes for hook toggle
        _newBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, replaceCount);
        
        // then, enable the button and return true
        button?.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
        return IsHooked = _isSetup = true;
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
        _detourAddr = _allocatedAddress = _variableAddress = UIntPtr.Zero;
        _originalBytes = _newBytes = null;
        _isSetup = false;
        _firstTime = true;
    }
    
    public void Toggle()
    {
        if (_firstTime)
        {
            _firstTime = false;
            return;
        }
        
        var currentBytes = Mw.M.ReadArrayMemory<byte>(_detourAddr, _originalBytes!.Length);
        
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

    public void Hook() => Mw.M.WriteArrayMemory(_detourAddr, _newBytes);
    public void UnHook() => Mw.M.WriteArrayMemory(_detourAddr, _originalBytes);
    
    private bool CreateDetour(byte[] caveBytes, int replaceCount)
    {
        _allocatedAddress = Mw.M.CreateDetour(_detourAddr.ToString("X"), caveBytes, replaceCount);
        return _allocatedAddress != UIntPtr.Zero;
    }

    public void UpdateVariable<T>(T value) where T : unmanaged
    {
        if (_variableAddress == UIntPtr.Zero)
        {
            return;
        }
        
        Mw.M.WriteMemory(_variableAddress, value);
    }
    
    public void UpdateVariable<T>(T[] value) where T : unmanaged
    {
        if (_variableAddress == UIntPtr.Zero)
        {
            return;
        }
        
        Mw.M.WriteArrayMemory(_variableAddress, value);
    }
    
    public T ReadVariable<T>() where T : unmanaged
    {
        return _variableAddress == UIntPtr.Zero ? new T() : Mw.M.ReadMemory<T>(_variableAddress);
    }
    
    public T[] ReadArrayVariable<T>(int length) where T : unmanaged
    {
        return _variableAddress == UIntPtr.Zero ? Array.Empty<T>() : Mw.M.ReadArrayMemory<T>(_variableAddress, length);
    }
    
    public bool IsHooked;
    private UIntPtr _detourAddr, _allocatedAddress, _variableAddress;
    private byte[]? _originalBytes, _newBytes;
    private bool _isSetup, _firstTime = true;
}