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
    
    public static void DisableAntiCheat()
    {
        if (Mw.Gvp.Name.Contains('4'))
        {
            DisableFh4();
            return;
        }

        PointChecksToCopy();    
    }

    private static void DisableFh4()
    {
        var ntDll = GetModuleHandle("ntdll.dll");
        var rtlUserThreadStart = GetProcAddress(ntDll, "RtlUserThreadStart");
        var ntCreateThreadEx = GetProcAddress(ntDll, "NtCreateThreadEx");
        Mw.M.WriteArrayMemory(rtlUserThreadStart, RtlUserThreadStartOrig);
        Mw.M.WriteArrayMemory(ntCreateThreadEx, NtCreateThreadExOrig);
    }

    public static bool IsScanRunning { get; set; }
    private static bool Bypassed { get; set; }
    
    private static void PointChecksToCopy()
    {
        if (IsScanRunning || Bypassed)
        {
            return;
        }
        
        IsScanRunning = true;

        const string sig = "40 8A ? E9 ? ? ? ? CC";
        var checkAddr1 = Mw.M.ScanForSig(sig).FirstOrDefault();

        if (checkAddr1 == 0)
        {
            Mw.M._memoryCache.Clear();
            return;
        }
        
        checkAddr1 += Mw.Gvp.Plat == "MS" ? (UIntPtr)325 : 333;

        if (Mw.M.ReadMemory<byte>(checkAddr1) == 0xE9)
        {
            Bypassed = true;
            return;
        }
        
        var checkAddr2 = checkAddr1 + 40;
        var checkAddr3 = checkAddr1 + 79;
        var checkAddr4 = checkAddr1 + 119;

        var baseAddress = Mw.Gvp.Process.MainModule!.BaseAddress;
        var endAddress = baseAddress + Mw.Gvp.Process.MainModule.ModuleMemorySize;
        var procHandle = Mw.Gvp.Process.Handle;
        var memSize = (uint)Mw.Gvp.Process.MainModule.ModuleMemorySize;
        var memCopyAddress = UIntPtr.Zero;

        while (memCopyAddress == UIntPtr.Zero)
        {
            memCopyAddress = VirtualAllocEx(procHandle, 0, memSize, MemCommit | MemReserve, Readwrite);
            Task.Delay(5).Wait();
        }
        WriteProcessMemory(procHandle, memCopyAddress, Mw.M._memoryCache["default"], memSize, nint.Zero);
        var addresses = GetBytes(baseAddress).Concat(GetBytes(endAddress)).Concat(GetBytes(memCopyAddress)).ToArray();
        
        const string check1Bytes = "53 48 8D 58 F0 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 F0 5B";
        const string check2Bytes = "53 48 8D 18 48 3B 1D 2E 00 00 00 72 1D 48 3B 1D 2D 00 00 00 77 14 48 2B 1D 1C 00 00 00 48 03 1D 25 00 00 00 F3 0F 6F 03 EB 04 F3 0F 6F 00 5B F3 0F 6F 51 E8";
        const string check3Bytes = "53 48 8D 58 10 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 10 5B";
        const string check4Bytes = "53 48 8D 58 20 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 20 5B";
        
        Check1Detour.Setup(checkAddr1, check1Bytes, 5, true);
        Check2Detour.Setup(checkAddr2, check2Bytes, 9, true);
        Check3Detour.Setup(checkAddr3, check3Bytes, 5, true);
        Check4Detour.Setup(checkAddr4, check4Bytes, 5, true);

        Check1Detour.UpdateVariable(addresses);
        Check2Detour.UpdateVariable(addresses);
        Check3Detour.UpdateVariable(addresses);
        Check4Detour.UpdateVariable(addresses);
        Bypassed = true;
    }
}