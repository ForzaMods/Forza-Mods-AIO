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
    
    private static readonly Detour CheckDetour = new(true);
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
            Destroy();
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

        if (IsScanRunning)
        {
            return false;
        }
        
        IsScanRunning = true;

        const string sig = "40 8A ? E9 ? ? ? ? CC";
        var checkAddr = Mw.M.ScanForSig(sig).FirstOrDefault();

        if (checkAddr < (UIntPtr)Mw.Gvp.Process.MainModule!.BaseAddress)
        {
            return false;
        }
        
        checkAddr += Mw.Gvp.Plat == "MS" ? (UIntPtr)325 : 333;
        var procHandle = Mw.Gvp.Process.Handle;
        var memSize = (uint)Mw.Gvp.Process.MainModule.ModuleMemorySize;
        
        _memCopyAddress = VirtualAllocEx(procHandle, UIntPtr.Zero, memSize, MemCommit | MemReserve, ExecuteReadwrite);
        WriteProcessMemory(procHandle, _memCopyAddress, Mw.M._memoryCache["default"], memSize, nint.Zero);
        
        const string checkBytes = "48 3B 05 23 00 00 00 72 17 48 3B 05 22 00 00 00 77 0E 48 2B 05 11 00 00 00 48 03 05 1A 00 00 00 F3 0F 6F 40 F0";
        const string checkOriginalBytes = "F3 0F 6F 40 F0";

        if (!CheckDetour.Setup(checkAddr, checkOriginalBytes, checkBytes, 5, true))
        {
            Destroy();
            return false;
        }

        var baseAddress = Mw.Gvp.Process.MainModule.BaseAddress;
        var endAddress = baseAddress + Mw.Gvp.Process.MainModule.ModuleMemorySize;
        var addresses = GetBytes(baseAddress).Concat(GetBytes(endAddress)).Concat(GetBytes(_memCopyAddress)).ToArray();
        CheckDetour.UpdateVariable(addresses);
        return Bypassed = true;
    }

    private static void Destroy()
    {
        CheckDetour.Destroy();
        CheckDetour.Clear();
        const uint memRelease = 0x8000;
        VirtualFreeEx(Mw.Gvp.Process.Handle, _memCopyAddress, 0, memRelease);
    }
}