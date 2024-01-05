using System;
using System.Linq;
using System.Threading.Tasks;
using static System.BitConverter;
using static Memory.Imps;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Resources;

public abstract class Bypass
{
    private static readonly byte[] RtlUserThreadStartPatch = { 0x48, 0x83, 0xEC, 0x78, 0x4C, 0x8B, 0xC2 };
    private static readonly byte[] NtCreateThreadExPatch = { 0x4C, 0x8B, 0xD1, 0xB8, 0xC7, 0x00, 0x00, 0x00 };

    private static byte[]? _rtlUserThreadStartOrig;
    private static byte[]? _ntCreateThreadExOrig;
    
    private static readonly Detour Check1Detour = new(true), Check2Detour = new(true), Check3Detour = new(true), Check4Detour = new(true);
    
    private static UIntPtr _memCopyAddress = UIntPtr.Zero;

    public static bool DisableAntiCheat()
    {
        if (!Mw.Gvp.Name.Contains('4'))
        {
            return PointChecksToCopy();
        }
        
        DisableFh4();
        return true;
    }

    public static void EnableAntiCheat()
    {
        if (Mw.Gvp.Name.Contains('5'))
        {
            ClearDetours();
            return;
        }
        
        
        var ntDll = GetModuleHandle("ntdll.dll");

        if (_rtlUserThreadStartOrig != null)
        {
            var rtlUserThreadStart = GetProcAddress(ntDll, "RtlUserThreadStart");
            Mw.M.WriteArrayMemory(rtlUserThreadStart, _rtlUserThreadStartOrig);
        }

        if (_ntCreateThreadExOrig == null) return;
        var ntCreateThreadEx = GetProcAddress(ntDll, "NtCreateThreadEx");
        Mw.M.WriteArrayMemory(ntCreateThreadEx, _ntCreateThreadExOrig);
    }
    
    private static void DisableFh4()
    {
        var ntDll = GetModuleHandle("ntdll.dll");
        var rtlUserThreadStart = GetProcAddress(ntDll, "RtlUserThreadStart");
        var ntCreateThreadEx = GetProcAddress(ntDll, "NtCreateThreadEx");
        _rtlUserThreadStartOrig = Mw.M.ReadArrayMemory<byte>(rtlUserThreadStart, 7);
        _ntCreateThreadExOrig = Mw.M.ReadArrayMemory<byte>(ntCreateThreadEx, 8);
        Mw.M.WriteArrayMemory(rtlUserThreadStart, RtlUserThreadStartPatch);
        Mw.M.WriteArrayMemory(ntCreateThreadEx, NtCreateThreadExPatch);
    }

    public static bool IsScanRunning { get; set; }
    private static bool Bypassed { get; set; }
    
    private static bool PointChecksToCopy()
    {
        if (Bypassed)
        {
            return true;
        }

        if (IsScanRunning || Mw.Gvp.Process?.MainModule == null)
        {
            return false;
        }
        
        IsScanRunning = true;

        const string sig = "40 8A ? E9 ? ? ? ? CC";
        var checkAddr1 = Mw.M.ScanForSig(sig).FirstOrDefault();

        if (checkAddr1 < (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            return false;
        }
        
        checkAddr1 += Mw.Gvp.Plat == "MS" ? (UIntPtr)325 : 333;
        var checkAddr2 = checkAddr1 + 40;
        var checkAddr3 = checkAddr1 + 79;
        var checkAddr4 = checkAddr1 + 119;

        var baseAddress = Mw.Gvp.Process.MainModule!.BaseAddress;
        var endAddress = baseAddress + Mw.Gvp.Process.MainModule.ModuleMemorySize;
        var procHandle = Mw.Gvp.Process.Handle;
        var memSize = (uint)Mw.Gvp.Process.MainModule.ModuleMemorySize;

        while (_memCopyAddress == UIntPtr.Zero)
        {
            _memCopyAddress = VirtualAllocEx(procHandle, 0, memSize, MemCommit | MemReserve, Readwrite);
            Task.Delay(5).Wait();
        }
        WriteProcessMemory(procHandle, _memCopyAddress, Mw.M._memoryCache["default"], memSize, nint.Zero);
        var addresses = GetBytes(baseAddress).Concat(GetBytes(endAddress)).Concat(GetBytes(_memCopyAddress)).ToArray();
        
        const string check1Bytes = "53 48 8D 58 F0 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 F0 5B";
        const string check2Bytes = "53 48 8D 18 48 3B 1D 2E 00 00 00 72 1D 48 3B 1D 2D 00 00 00 77 14 48 2B 1D 1C 00 00 00 48 03 1D 25 00 00 00 F3 0F 6F 03 EB 04 F3 0F 6F 00 5B F3 0F 6F 51 E8";
        const string check3Bytes = "53 48 8D 58 10 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 10 5B";
        const string check4Bytes = "53 48 8D 58 20 48 3B 1D 2A 00 00 00 72 1D 48 3B 1D 29 00 00 00 77 14 48 2B 1D 18 00 00 00 48 03 1D 21 00 00 00 F3 0F 6F 03 EB 05 F3 0F 6F 40 20 5B";

        const string check1OriginalBytes = "F3 0F 6F 40 F0";
        const string check2OriginalBytes = "F3 0F 6F 00 F3 0F 6F 51 E8";           
        const string check3OriginalBytes = "F3 0F 6F 40 10";        
        const string check4OriginalBytes = "F3 0F 6F 40 20";

        if (!Check1Detour.Setup(checkAddr1, check1OriginalBytes, check1Bytes, 5, true))
        {
            ClearDetours();
            return false;
        }

        if (!Check2Detour.Setup(checkAddr2, check2OriginalBytes, check2Bytes, 9, true))
        {
            ClearDetours();
            return false;        
        }

        if (!Check3Detour.Setup(checkAddr3, check3OriginalBytes, check3Bytes, 5, true))
        {
            ClearDetours();
            return false;
        }

        if (!Check4Detour.Setup(checkAddr4, check4OriginalBytes, check4Bytes, 5, true))
        {
            ClearDetours();
            return false;
        }

        var list = new[] { Check1Detour,Check2Detour,Check3Detour,Check4Detour };
        Parallel.ForEach(list, detour => detour.UpdateVariable(addresses));
        return Bypassed = true;
    }

    private static void ClearDetours()
    {
        var list = new[] { Check1Detour,Check2Detour,Check3Detour,Check4Detour };
        Parallel.ForEach(list, detour =>
        {
            detour.Destroy();
            detour.Clear();
        });
        
        const uint memRelease = 0x8000;
        VirtualFreeEx(Mw.Gvp.Process.Handle, _memCopyAddress, 0, memRelease);
    }
}