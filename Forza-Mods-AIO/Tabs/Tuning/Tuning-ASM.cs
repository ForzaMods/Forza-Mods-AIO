using System;
using System.Threading;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using Memory;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Tuning.TuningAddresses;

namespace Forza_Mods_AIO.Tabs.Tuning;

public abstract class TuningAsm : Asm
{
    private static nuint _codeCave1, _codeCave2, _codeCave3;
    private static byte[]? _hook1OriginalBytes, _hook2OriginalBytes, _hook3OriginalBytes;

    public static void Cleanup()
    {
        try
        {
            ClearCodeCaveJumps();
            FreeMem();
        }
        catch { /* ignored */}
    }
    
    private static void FreeMem()
    {
        if (_codeCave1 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process.Handle, _codeCave1, 0, MemRelease);
        }

        if (_codeCave2 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process.Handle, _codeCave2, 0, MemRelease);
        }

        if (_codeCave3 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process.Handle, _codeCave3, 0, MemRelease);
        }
    }
    
    private static void ClearCodeCaveJumps()
    {
        Mw.M.WriteArrayMemory(TuningHook1, _hook1OriginalBytes);
        Mw.M.WriteArrayMemory(TuningHook1, _hook1OriginalBytes);
        Mw.M.WriteArrayMemory(TuningHook2, _hook2OriginalBytes);
        Mw.M.WriteArrayMemory(TuningHook3, _hook3OriginalBytes);
    }

    public static void GetTuningBaseAddresses()
    {        
        _hook1OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningHook1, 7);
        var hook2Bytes = StringToBytes("4C893D49000000498B07488D5577");
        _codeCave1 = Mw.M.CreateDetour(TuningHook1.ToString("X"), hook2Bytes, 7);
        
        _hook2OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningHook2, 7);
        var hook3Bytes = StringToBytes("4C893D49000000498B07488D5577");
        _codeCave2 = Mw.M.CreateDetour(TuningHook2.ToString("X"), hook3Bytes, 7);
        
        _hook3OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningHook3, 10);
        var hook4Bytes = StringToBytes("488B0748893D46000000488D9560020000");
        _codeCave3 = Mw.M.CreateDetour(TuningHook3.ToString("X"), hook4Bytes, 10);

        Task.Run(ReadAddresses);
    }

    private static void ReadAddresses()
    {
        while (Mw.Attached)
        {
            Base1 = Mw.M.ReadMemory<UIntPtr>(_codeCave1 + 0x50);
            Base2 = Mw.M.ReadMemory<UIntPtr>(_codeCave2 + 0x50);
            Base3 = Mw.M.ReadMemory<UIntPtr>(_codeCave3 + 0x50) + 400;
            Thread.Sleep(1000);
        } 
    }
}