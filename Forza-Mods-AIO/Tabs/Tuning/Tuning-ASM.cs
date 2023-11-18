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
    private static nuint _codeCave1, _codeCave2, _codeCave3, _codeCave4;
    private static byte[]? _hook1OriginalBytes, _hook2OriginalBytes, _hook3OriginalBytes, _hook4OriginalBytes;

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
            Imps.VirtualFreeEx(Mw.Gvp.Process!.Handle, _codeCave1, 0, MemRelease);
        }

        if (_codeCave2 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process!.Handle, _codeCave2, 0, MemRelease);
        }

        if (_codeCave3 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process!.Handle, _codeCave3, 0, MemRelease);
        }

        if (_codeCave4 != UIntPtr.Zero)
        {
            Imps.VirtualFreeEx(Mw.Gvp.Process!.Handle, _codeCave4, 0, MemRelease);
        }
    }
    
    private static void ClearCodeCaveJumps()
    {
        Mw.M.WriteArrayMemory(TuningTableHook1, _hook1OriginalBytes);
        Mw.M.WriteArrayMemory(TuningTableHook2, _hook2OriginalBytes);
        Mw.M.WriteArrayMemory(TuningTableHook3, _hook3OriginalBytes);
        Mw.M.WriteArrayMemory(TuningTableHook4, _hook4OriginalBytes);
    }

    public static void GetTuningBaseAddresses()
    {        
        var hook1ReplaceCount = Mw.Gvp.Name == "Forza Horizon 4" ? 5 : 8 ;
        _hook1OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningTableHook1, hook1ReplaceCount);
        var hook1Bytes = Mw.Gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893549000000498B068BD6") : StringToBytes(BitConverter.ToString(_hook1OriginalBytes).Replace("-", string.Empty) + "4881E97005000048890D3A0000004881C170050000");
        _codeCave1 = Mw.M.CreateDetour(TuningTableHook1.ToString("X"), hook1Bytes, hook1ReplaceCount, size: 30);
        
        var hook2ReplaceCount = Mw.Gvp.Name == "Forza Horizon 4" ? 7 : 6 ;
        _hook2OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningTableHook2, hook2ReplaceCount);
        var hook2Bytes = Mw.Gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893D49000000498B07488D5577") : StringToBytes(BitConverter.ToString(_hook2OriginalBytes).Replace("-", string.Empty) + "48891D43000000");
        _codeCave2 = Mw.M.CreateDetour(TuningTableHook2.ToString("X"), hook2Bytes, hook2ReplaceCount, size: 30);
        
        var hook3ReplaceCount = Mw.Gvp.Name == "Forza Horizon 4" ? 7 : 6 ;
        _hook3OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningTableHook3, hook3ReplaceCount);
        var hook3Bytes = Mw.Gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893D49000000498B07488D5577") : StringToBytes(BitConverter.ToString(_hook3OriginalBytes).Replace("-", string.Empty) + "48890D43000000");
        _codeCave3 = Mw.M.CreateDetour(TuningTableHook3.ToString("X"), hook3Bytes, hook3ReplaceCount, size: 30);
        
        var hook4ReplaceCount = Mw.Gvp.Name == "Forza Horizon 4" ? 10 : 6 ;
        _hook4OriginalBytes = Mw.M.ReadArrayMemory<byte>(TuningTableHook4, hook4ReplaceCount);
        var hook4Bytes = Mw.Gvp.Name == "Forza Horizon 4" ? StringToBytes("488B0748893D46000000488D9560020000") : StringToBytes("51488BC848890D450000005941B806000000");
        _codeCave4 = Mw.M.CreateDetour(TuningTableHook4.ToString("X"), hook4Bytes, hook4ReplaceCount, size: 30);

        Task.Run(ReadAddresses);
    }

    private static void ReadAddresses()
    {
        while (Mw.Attached)
        {
            Base1 = Mw.M.ReadMemory<UIntPtr>(_codeCave1 + 0x50);
            Base2 = Mw.M.ReadMemory<UIntPtr>(_codeCave2 + 0x50) + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0 : 400);
            Base3 = Mw.M.ReadMemory<UIntPtr>(_codeCave3 + 0x50);
            Base4 = Mw.M.ReadMemory<UIntPtr>(_codeCave4 + 0x50) + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 400 : 0);
            
            Addresses();
            
            Thread.Sleep(1000);
        } 
    }
}