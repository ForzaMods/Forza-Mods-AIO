using System;
using System.Linq;
using static Memory.Imps;
using static System.BitConverter;
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
    private static UIntPtr _protectedAddresses = UIntPtr.Zero;
    
    public static bool DisableAntiCheat()
    {
        switch (Mw.Gvp.Type)
        {
            case GameVerPlat.GameType.Fh5:
            {
                return PointChecksToCopyFh5();
            }
            case GameVerPlat.GameType.Fm8:
            {
                return PointChecksToCopyFm8();
            }
            case GameVerPlat.GameType.Fh4:
            {
                DisableFh4();
                return true;
            }
            case GameVerPlat.GameType.None:
            default:
            {
                throw new Exception("You cant disable the anti cheat while not attached");
            }
        }
    }

    public static void EnableAntiCheat()
    {
        if (Mw.Gvp.Type != GameVerPlat.GameType.Fh4)
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

    private static bool Bypassed { get; set; }
    
    private static bool PointChecksToCopyFh5()
    {
        if (Bypassed)
        {
            return true;
        }
        
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

    private static bool PointChecksToCopyFm8()
    {
        if (Bypassed)
        {
            return true;
        }

        const string sig = "E8 ? ? ? ? 40 8A ? E9 ? ? ? ? CC";
        var checkAddr = Mw.M.ScanForSig(sig).FirstOrDefault() + 5;

        if (checkAddr < (UIntPtr)Mw.Gvp.Process.MainModule!.BaseAddress)
        {
            return false;
        }

        checkAddr += Mw.Gvp.Plat == "MS" ? (UIntPtr)329 : 337;
        var procHandle = Mw.Gvp.Process.Handle;
        var memSize = (uint)Mw.Gvp.Process.MainModule.ModuleMemorySize;

        const int protectedAddressesSize = 0x800;
        _memCopyAddress = VirtualAllocEx(procHandle, UIntPtr.Zero, memSize, MemCommit | MemReserve, ExecuteReadwrite);
        _protectedAddresses = VirtualAllocEx(procHandle, UIntPtr.Zero, protectedAddressesSize, MemCommit | MemReserve, ExecuteReadwrite);
        WriteProcessMemory(procHandle, _memCopyAddress, Mw.M._memoryCache["default"], memSize, nint.Zero);
        
        const string checkBytes = "51 56 53 52 48 31 C9 48 8B 35 48 00 00 00 48 8B 14 CE 48 83 FA 00 74 30 48 8B DA " +
                                  "48 81 EA 00 10 00 00 48 39 D0 72 1C 48 81 C3 00 10 00 00 48 39 D8 77 10 48 2B 05 " +
                                  "24 00 00 00 48 03 05 25 00 00 00 EB 05 48 FF C1 EB C6 5A 5B 5E 59 F3 0F 6F 50 F0";

        const string checkOriginalBytes = "F3 0F 6F 50 F0";
        
        if (!CheckDetour.Setup(checkAddr, checkOriginalBytes, checkBytes, 5, true))
        {
            Destroy();
            return false;
        }

        var baseAddress = Mw.Gvp.Process.MainModule.BaseAddress;
        var addresses = GetBytes(_protectedAddresses).Concat(GetBytes(baseAddress)).Concat(GetBytes(_memCopyAddress)).ToArray();
        CheckDetour.UpdateVariable(addresses);
        AddProtectAddress(checkAddr);
        return Bypassed = true;
    }

    public static void AddProtectAddress(UIntPtr address)
    {
        Mw.M.WriteMemory(_protectedAddresses, address);
        _protectedAddresses += 8;
    }
    
    private static void Destroy()
    {
        CheckDetour.Destroy();
        CheckDetour.Clear();
        const uint memRelease = 0x8000;
    
        if (_memCopyAddress != UIntPtr.Zero)
        {
            VirtualFreeEx(Mw.Gvp.Process.Handle, _memCopyAddress, 0, memRelease);
        }
    
        if (_protectedAddresses != UIntPtr.Zero)
        {
            VirtualFreeEx(Mw.Gvp.Process.Handle, _protectedAddresses, 0, memRelease);
        }
    }

    public static void Clear()
    {
        _memCopyAddress = _protectedAddresses = 0;
        CheckDetour.Clear();
    }
}