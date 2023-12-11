using System;
using System.Linq;
using System.Threading.Tasks;
using static System.BitConverter;
using static Memory.Imps;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Resources;

public abstract class Bypass
{
    private static readonly byte[] RtlUserThreadStartOrig = { 0x48, 0x83, 0xEC, 0x78, 0x4C, 0x8B, 0xC2 };
    private static readonly byte[] NtCreateThreadExOrig = { 0x4C, 0x8B, 0xD1, 0xB8, 0xC7, 0x00, 0x00, 0x00 };

    private static readonly Detour Check1Detour = new(), Check2Detour = new(), Check3Detour = new(), Check4Detour = new();
    private static UIntPtr _memCopyAddress = UIntPtr.Zero;
    
    public static void DisableAntiCheat(int ver)
    {
        var ntDll = GetModuleHandle("ntdll.dll");
        var rtlUserThreadStart = GetProcAddress(ntDll, "RtlUserThreadStart");
        var ntCreateThreadEx = GetProcAddress(ntDll, "NtCreateThreadEx");

        Mw.M.WriteArrayMemory(rtlUserThreadStart, RtlUserThreadStartOrig);
        Mw.M.WriteArrayMemory(ntCreateThreadEx, NtCreateThreadExOrig);
        
        if (ver == 4)
        {
            return;
        }
        
        PointChecksToCopy();
    }


    public static bool IsScanRunning { get; set; }
    
    private static void PointChecksToCopy()
    {
        if (IsScanRunning)
        {
            return;
        }
        
        IsScanRunning = true;
        
        while (Mw.Gvp.Process == null)
        {
            Task.Delay(5).Wait();
        }

        var checkAddr1 = Mw.M.ScanForSig("40 8A ? E9 ? ? ? ? CC").FirstOrDefault();
        checkAddr1 += Mw.Gvp.Plat == "MS" ? (UIntPtr)325 : 333;

        if (Mw.M.ReadMemory<byte>(checkAddr1) == 0xE9)
        {
            return;
        }
        
        var checkAddr2 = checkAddr1 + 40;
        var checkAddr3 = checkAddr1 + 79;
        var checkAddr4 = checkAddr1 + 119;

        var baseAddress = (long)Mw.Gvp.Process.MainModule!.BaseAddress;
        var endAddress = baseAddress + Mw.Gvp.Process.MainModule.ModuleMemorySize;
        var procHandle = Mw.Gvp.Process.Handle;
        var memSize = (uint)Mw.Gvp.Process.MainModule.ModuleMemorySize;

        while (_memCopyAddress == 0)
        {
            _memCopyAddress = VirtualAllocEx(procHandle, 0, memSize, MemCommit | MemReserve, ExecuteReadwrite);
            Task.Delay(5).Wait();
        }
        WriteProcessMemory(procHandle, _memCopyAddress, Mw.M._memoryCache["default"], memSize, nint.Zero);
        var addresses = GetBytes(baseAddress).Concat(GetBytes(endAddress)).Concat(GetBytes(_memCopyAddress)).ToArray();
        
        const string check1Bytes = "53 48 8D 58 F0 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 F0 5B";
        const string check2Bytes = "53 48 8D 18 48 3B 1D 2E 00 00 00 72 1D 48 3B 1D 2D 00 00 00 77 14 48 2B 1D 1C 00 00 00 48 03 1D 25 00 00 00 F3 0F 6F 03 EB 04 F3 0F 6F 00 5B F3 0F 6F 51 E8";
        const string check3Bytes = "53 48 8D 58 10 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 10 5B";
        const string check4Bytes = "53 48 8D 58 20 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 20 5B";
        
        Check1Detour.Setup(null, checkAddr1, check1Bytes, 5, true);
        Check1Detour.UpdateVariable(addresses);
        
        Check2Detour.Setup(null, checkAddr2, check2Bytes, 9, true);
        Check2Detour.UpdateVariable(addresses);
        
        Check3Detour.Setup(null, checkAddr3, check3Bytes, 5, true);
        Check3Detour.UpdateVariable(addresses);
        
        Check4Detour.Setup(null, checkAddr4, check4Bytes, 5, true);
        Check4Detour.UpdateVariable(addresses);
    }
}