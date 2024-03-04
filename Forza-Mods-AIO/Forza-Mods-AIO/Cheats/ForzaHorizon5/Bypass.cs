using System.Diagnostics;
using Forza_Mods_AIO.Models;
using static Memory.Types.Imps;
using static System.BitConverter;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class Bypass : CheatsUtilities, ICheatsBase
{
    public readonly DebugSession BypassDebug = new("Bypass", [], []);
    
    public UIntPtr CrcFuncAddress, CrcFuncDetourAddress, MemCopyAddress;
    
    public async Task DisableCrcChecks()
    {
        CrcFuncAddress = 0;
        CrcFuncDetourAddress = 0;

        const string sig = "E8 ? ? ? ? 48 83 C4 ? 5F 5B C3 CC CC CC 48 89";
        CrcFuncAddress = await SmartAobScan(sig);
        
        BypassDebug.DebugInfoReports.Add(new DebugInfoReport($"Address: {CrcFuncAddress:X}"));
        
        if (CrcFuncAddress > 0)
        {
            var mem = Resources.Memory.GetInstance();
            var scanResult = (IntPtr)CrcFuncAddress + mem.ReadMemory<int>(CrcFuncAddress + 1) + 5;
            CrcFuncAddress = (UIntPtr)(scanResult + 311);
            
            var procHandle = mem.MProc.Process.Handle;
            var memSize = mem.MProc.Process.MainModule!.ModuleMemorySize;
            var baseAddress = (UIntPtr)mem.MProc.Process.MainModule!.BaseAddress;
        
            var memCopy = new byte[memSize];
            ReadProcessMemory(procHandle, baseAddress, memCopy, memSize);
            
            MemCopyAddress = VirtualAllocEx(procHandle, UIntPtr.Zero, (uint)memSize, MemCommit | MemReserve, ReadWrite);
            WriteProcessMemory(procHandle, MemCopyAddress, memCopy, (uint)memSize, nint.Zero);

             var currentProcess = Process.GetCurrentProcess();
            currentProcess.MinWorkingSet = 300000;
            
            var endAddress = baseAddress + (uint)memSize;
            var varBytes = GetBytes(baseAddress).Concat(GetBytes(endAddress)).Concat(GetBytes(MemCopyAddress)).ToArray();
            
            var asm = new byte[]
            {
                0x48, 0x3B, 0x05, 0x23, 0x00, 0x00, 0x00, 0x72, 0x17, 0x48, 0x3B, 0x05, 0x22, 0x00, 0x00, 0x00, 0x77,
                0x0E, 0x48, 0x2B, 0x05, 0x11, 0x00, 0x00, 0x00, 0x48, 0x03, 0x05, 0x1A, 0x00, 0x00, 0x00, 0xF3, 0x0F,
                0x6F, 0x40, 0xF0
            };
            
            CrcFuncDetourAddress = mem.CreateDetour(CrcFuncAddress, asm, 5, varBytes: varBytes);
            BypassDebug.DebugInfoReports.Add(new DebugInfoReport($"Hook Addr: {CrcFuncDetourAddress:X}"));
            return;
        }
        
        ShowError("Bypass", sig);
    }

    public void Cleanup()
    {
        if (CrcFuncAddress <= 0) return;
        var memInstance = Resources.Memory.GetInstance();
        memInstance.WriteArrayMemory(CrcFuncAddress, new byte[] { 0xF3, 0x0F, 0x6F, 0x40, 0xF0 });
        Free(CrcFuncDetourAddress);
        Free(MemCopyAddress);
    }

    public void Reset()
    {
        var fields = typeof(Bypass).GetFields().Where(f => f.FieldType == typeof(nuint));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}