#pragma warning disable CA1806

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static Memory.Imps;

namespace Forza_Mods_AIO.Resources;

public abstract partial class Bypass
{
    #region DLL Imports
    [LibraryImport("kernel32.dll")]
    private static partial void CloseHandle(nint hObject);
    
    [LibraryImport("ntdll.dll", SetLastError = true)]
    private static partial int NtQueryInformationThread(nint threadHandle, ThreadInfoClass threadInformationClass, nint threadInformation, int threadInformationLength, nint returnLengthPtr);

    [LibraryImport("kernel32.dll")]
    private static partial void TerminateThread(IntPtr hThread);
    #endregion

    #region Functions
    
    private static IntPtr GetThreadStartAddress(int threadId)
    {
        var hThread = OpenThread(ThreadAccess.QueryInformation, false, ((uint)threadId));
        if (hThread == IntPtr.Zero)
            throw new Win32Exception();
        var buf = Marshal.AllocHGlobal(IntPtr.Size);
        try
        {
            var result = NtQueryInformationThread(hThread, ThreadInfoClass.ThreadQuerySetWin32StartAddress, buf, IntPtr.Size, IntPtr.Zero);
            
            if (result != 0)
                throw new Win32Exception($"NtQueryInformationThread failed; NTSTATUS = {result:X8}");
            return Marshal.ReadIntPtr(buf);
        }
        finally
        {
            CloseHandle(hThread);
            Marshal.FreeHGlobal(buf);
        }
    }
    #endregion

    #region Magic

                    
    /*                                          THREAD DESCRIPTIONS !!!!
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0x24a7e0 <- dont run on startup, some of them are bink ones. (multiple threads)
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0xd5f20  <- not really sure what it does. launches on startup along with watchdog
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0x2ce8c  <- starts when u kill watchdog.
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0x2cf00  <- same as on thread +0x2ce8c.
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0xb2ef4  <- same as on thread +0x2ce8c. (multiple threads)
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0xc3a58  <- same as on thread +0x2ce8c, though dies after some time.
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0xeef34  <- watchdog. runs on startup
     * ForzaHorizon5.exe!tm_sdk_installer_unregister_changes+0x1e4b00 <- dies when you kill watchdog so not sure, might aswell suspend it too.
     * ForzaHorizon5.exe!Turn10GainGetDSPDescription+0x477710         <- I suspended it once and my breakpoints on mem integrity check functions stopped for some time
     *                                                                   (no other threads died), probably some placeholder thread that Idk what impact has on the game.
     *                                                                   I doubt its audio thread as theadmiester said, bc it has no impact on audio whatsoever.
     * ForzaHorizon5.exe+0x9390e0                                     <- Main game threads
     * ForzaHorizon5.exe!tm_api_update+0x2840                         <- Unknown thread. not sure what it does
     * ForzaHorizon5.exe+0x10d9be0                                    <- steam_api64.SteamInternal_ContextInit thread. runs only at "press start" screen
     */
    
    public static void DisableAnticheat()
    {
        // get addresses for bypass
        var NtDLL = GetModuleHandle("ntdll.dll");
        var RtlUserThreadStart = GetProcAddress(NtDLL, "RtlUserThreadStart");
        var NtCreateThreadEx = GetProcAddress(NtDLL, "NtCreateThreadEx");
        var tm_sdk_installer_unregister_changes = (nint)MainWindow.mw.m.ScanForSig("48 83 EC ? 48 8B ? ? ? ? ? FF 15 ? ? ? ? 48 8B ? ? ? ? ? BA").FirstOrDefault();
            
        // this one fixes dll injection
        MainWindow.mw.m.WriteArrayMemory(RtlUserThreadStart, new byte[] { 0x48, 0x83, 0xEC, 0x78, 0x4C, 0x8B, 0xC2 });
        // this one allows to kill threads
        MainWindow.mw.m.WriteArrayMemory(NtCreateThreadEx, new byte[] { 0x4C, 0x8B, 0xD1, 0xB8, 0xC7, 0x00, 0x00, 0x00 });
        
        foreach (ProcessThread Thread in MainWindow.mw.gvp.Process.Threads)
        {
    
            if (GetThreadStartAddress(Thread.Id) == (tm_sdk_installer_unregister_changes + 0xeef34))
            {
                TerminateThread(OpenThread(ThreadAccess.Terminate, false, (uint)Thread.Id));
            }
            else if (GetThreadStartAddress(Thread.Id) == (tm_sdk_installer_unregister_changes + 0xd5f20))
            {
                TerminateThread(OpenThread(ThreadAccess.Terminate, false, (uint)Thread.Id));
            }
            else if (GetThreadStartAddress(Thread.Id) == (tm_sdk_installer_unregister_changes + 0x1e4b00))
            {
                TerminateThread(OpenThread(ThreadAccess.Terminate, false, (uint)Thread.Id));
            }
    
        }
    }

    #endregion
}