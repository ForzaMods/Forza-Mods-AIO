using System;
using System.Threading;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using Memory;

namespace Forza_Mods_AIO.Tabs.Tuning;

public abstract class Tuning_ASM : ASM
{
    private static nuint CodeCave1, CodeCave2, CodeCave3, CodeCave4;
    private static byte[] Hook1OriginalBytes, Hook2OriginalBytes, Hook3OriginalBytes, Hook4OriginalBytes;

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
        if (CodeCave1 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave1, 0, MEM_RELEASE);
   
        if (CodeCave2 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave2, 0, MEM_RELEASE);
   
        if (CodeCave3 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave3, 0, MEM_RELEASE);
   
        if (CodeCave4 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave4, 0, MEM_RELEASE);
    }
    
    private static void ClearCodeCaveJumps()
    {
        MainWindow.mw.m.WriteArrayMemory(Tuning_Addresses.TuningTableHook1, Hook1OriginalBytes);
        MainWindow.mw.m.WriteArrayMemory(Tuning_Addresses.TuningTableHook2, Hook2OriginalBytes);
        MainWindow.mw.m.WriteArrayMemory(Tuning_Addresses.TuningTableHook3, Hook3OriginalBytes);
        MainWindow.mw.m.WriteArrayMemory(Tuning_Addresses.TuningTableHook4, Hook4OriginalBytes);
    }

    public static void GetTuningBaseAddresses()
    {        
        var Hook1ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 5 : 8 ;
        Hook1OriginalBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Tuning_Addresses.TuningTableHook1, Hook1ReplaceCount);
        var Hook1Bytes = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893549000000498B068BD6") : StringToBytes(BitConverter.ToString(Hook1OriginalBytes).Replace("-", string.Empty) + "4881E97005000048890D3A0000004881C170050000");
        CodeCave1 = MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook1, Hook1Bytes, Hook1ReplaceCount, size: 30);
        
        var Hook2ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 7 : 6 ;
        Hook2OriginalBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Tuning_Addresses.TuningTableHook2, Hook2ReplaceCount);
        var Hook2Bytes = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893D49000000498B07488D5577") : StringToBytes(BitConverter.ToString(Hook2OriginalBytes).Replace("-", string.Empty) + "48891D43000000");
        CodeCave2 = MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook2, Hook2Bytes, Hook2ReplaceCount, size: 30);
        
        var Hook3ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 7 : 6 ;
        Hook3OriginalBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Tuning_Addresses.TuningTableHook3, Hook3ReplaceCount);
        var Hook3Bytes = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? StringToBytes("4C893D49000000498B07488D5577") : StringToBytes(BitConverter.ToString(Hook3OriginalBytes).Replace("-", string.Empty) + "48890D43000000");
        CodeCave3 = MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook3, Hook3Bytes, Hook3ReplaceCount, size: 30);
        
        var Hook4ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 10 : 6 ;
        Hook4OriginalBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Tuning_Addresses.TuningTableHook4, Hook4ReplaceCount);
        var Hook4Bytes = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? StringToBytes("488B0748893D46000000488D9560020000") : StringToBytes("51488BC848890D450000005941B806000000");
        CodeCave4 = MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook4, Hook4Bytes, Hook4ReplaceCount, size: 30);

        Task.Run(ReadAddresses);
    }

    private static void ReadAddresses()
    {
        while (MainWindow.mw.Attached)
        {
            Tuning_Addresses.TuningTableBase1Long = MainWindow.mw.m.ReadMemory<long>(((long)CodeCave1 + 0x50).ToString("X"));
            Tuning_Addresses.TuningTableBase2Long = MainWindow.mw.m.ReadMemory<long>(((long)CodeCave2 + 0x50).ToString("X")) + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0 : 400);
            Tuning_Addresses.TuningTableBase3Long = MainWindow.mw.m.ReadMemory<long>(((long)CodeCave3 + 0x50).ToString("X"));
            Tuning_Addresses.TuningTableBase4Long = MainWindow.mw.m.ReadMemory<long>(((long)CodeCave4 + 0x50).ToString("X")) + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 400 : 0);
            
            Tuning_Addresses.Addresses();
            
            Thread.Sleep(1000);
        } 
    }
}