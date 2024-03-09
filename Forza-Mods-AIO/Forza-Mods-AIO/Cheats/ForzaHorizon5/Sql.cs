using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Views.Pages;
using Memory.Types;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class Sql : CheatsUtilities, ICheatsBase
{
    public UIntPtr CDatabaseAddress;
    private UIntPtr _ptr;

    public async Task SqlExecAobScan()
    {
        CDatabaseAddress = 0;
        _ptr = 0;

        const string sig = "0F 84 ? ? ? ? 48 8B 35 ? ? ? ? 48 85 F6 74";
        CDatabaseAddress = await SmartAobScan(sig);

        if (CDatabaseAddress > 0)
        {
            var relativeAddress = CDatabaseAddress + 0x6 + 0x3;
            var relative = Resources.Memory.GetInstance().ReadMemory<int>(relativeAddress);
            var pCDataBaseAddress = CDatabaseAddress + (nuint)relative + 0x6 + 0x7;
            _ptr = Resources.Memory.GetInstance().ReadMemory<nuint>(pCDataBaseAddress);
            return;
        }

        ShowError("Sql", sig);
    }
    
    private static nuint GetVirtualFunctionPtr(nuint ptr, int index)
    {
        var pVtableBytes = new byte[8];
        var procHandle = Resources.Memory.GetInstance().MProc.Handle;
        Imps.ReadProcessMemory(procHandle, ptr, pVtableBytes, (nuint)pVtableBytes.Length, nint.Zero);

        var pVtable = (nuint)BitConverter.ToInt64(pVtableBytes, 0);
        var vTableBytes = new byte[8];
        var lpBaseAddress = pVtable + (nuint)nuint.Size * (nuint)index;
        Imps.ReadProcessMemory(procHandle, lpBaseAddress, vTableBytes, (nuint)vTableBytes.Length, nint.Zero);
        return (nuint)BitConverter.ToInt64(vTableBytes, 0);
    }
    
    public Task Query(string command)
    {
        var procHandle = Resources.Memory.GetInstance().MProc.Handle;

        var rcx = _ptr;
        const int virtualFunctionIndex = 9;
        var callFunction = GetVirtualFunctionPtr(_ptr, virtualFunctionIndex);
        var shellCodeAddress = Imps.VirtualAllocEx(procHandle, 0, 0x1000, 0x3000, 0x40);
        var rdx = Imps.VirtualAllocEx(procHandle, 0, 0x1000, 0x3000, 0x40);
        var r8 = Imps.VirtualAllocEx(procHandle, 0, 0x1000, 0x3000, 0x40);
        var rdxBytes = BitConverter.GetBytes(rdx.ToUInt64());
        var r8Bytes = BitConverter.GetBytes(r8.ToUInt64());
        var callBytes = BitConverter.GetBytes(callFunction.ToUInt64());
        
        byte[] shellCode =
        [
            0x48, 0xBA, rdxBytes[0], rdxBytes[1], rdxBytes[2], rdxBytes[3], rdxBytes[4], rdxBytes[5], rdxBytes[6],
            rdxBytes[7], 0x49, 0xB8, r8Bytes[0], r8Bytes[1], r8Bytes[2], r8Bytes[3], r8Bytes[4], r8Bytes[5], r8Bytes[6],
            r8Bytes[7], 0xFF, 0x25, 0x00, 0x00, 0x00, 0x00, callBytes[0], callBytes[1], callBytes[2], callBytes[3],
            callBytes[4], callBytes[5], callBytes[6], callBytes[7]
        ];
      
        Resources.Memory.GetInstance().WriteStringMemory(r8, command + "\0");
        Resources.Memory.GetInstance().WriteArrayMemory(shellCodeAddress, shellCode);
        var thread = Imports.CreateRemoteThread(procHandle, 0, 0, shellCodeAddress, rcx, 0, out _);
        _ = Imports.WaitForSingleObject(thread, int.MaxValue);
        Free(shellCodeAddress);
        Free(r8);
        Free(rdx);
        return Task.CompletedTask;
    }

    public void Cleanup()
    {
    }

    public void Reset()
    {
        var fields = typeof(Sql).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}