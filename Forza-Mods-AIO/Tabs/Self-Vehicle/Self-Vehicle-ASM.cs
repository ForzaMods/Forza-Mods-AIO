using System;
using System.Threading;
using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Memory;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

public abstract class Self_Vehicle_ASM : Resources.ASM
{
    public static UIntPtr CodeCave1, CodeCave2, CodeCave3, CodeCave4, CodeCave5, CodeCave6, CodeCave7, CodeCave8, CodeCave9, CodeCave10, CodeCave11;
    private static bool CreditsFirstTime = true, GlowingPaintFirstTime = true, BuildCapFirstTime = true, XpFirstTime = true, FlyhackFirstTime = true, FovFirstTime = true;
    public static byte[]? CreditsJmpBytes, GlowingPaintJmpBytes, BuildCapJmpBytes1, BuildCapJmpBytes2, XpJmpBytes, FlyhackJmpBytes, FovJmpBytes;
    public static byte[]? CreditsOrigBytes, GlowingPaintOrigBytes, BuildCapOrigBytes1, BuildCapOrigBytes2, XpOrigBytes, FlyhackOrigBytes, GetWayPointAddrOrigBytes, BaseAddressHookOrigBytes, FovOrigBytes;
    
    
    public static void Cleanup()
    {
        try
        {
            CleanCodeCaveJumps();
            FreeMem();
        }
        catch { /* ignored */}
    }

    
    private static void CleanCodeCaveJumps()
    {
        if (CreditsOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr, CreditsOrigBytes);
        if (GlowingPaintOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.GlowingPaintAddr, GlowingPaintOrigBytes);
        if (BuildCapOrigBytes1 != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, BuildCapOrigBytes1);
        if (BuildCapOrigBytes2 != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, BuildCapOrigBytes2);
        if (XpOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, XpOrigBytes);
        if (FlyhackOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, FlyhackOrigBytes);
        if (GetWayPointAddrOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.WayPointxASMAddr, GetWayPointAddrOrigBytes);
        if (BaseAddressHookOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BaseAddrHook, BaseAddressHookOrigBytes);
        if (FovOrigBytes != null)
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.FovHookAddr, FovOrigBytes);
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

        if (CodeCave5 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave5, 0, MEM_RELEASE);

        if (CodeCave6 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave6, 0, MEM_RELEASE);

        if (CodeCave7 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave7, 0, MEM_RELEASE);

        if (CodeCave8 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave8, 0, MEM_RELEASE);

        if (CodeCave9 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave9, 0, MEM_RELEASE);

        if (CodeCave10 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave10, 0, MEM_RELEASE);
        
        if (CodeCave11 != UIntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, CodeCave11, 0, MEM_RELEASE);
    }
    
    
    public static void GetTimeAddr()
    {
        CodeCave1 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.TimeNOPAddr, StringToBytes("48891D21000000F20F1143084883C440"), 9, size: 30);
        Thread.Sleep(250);
        Self_Vehicle_Addrs.TimeAddr = (MainWindow.mw.m.ReadMemory<long>(((long)CodeCave1 + 40).ToString("X")) + 8).ToString("X");
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.TimeNOPAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 });
    }
    
    
    public static void StartXPtool()
    {
        if (XpFirstTime)
        {
            string InsideCaveCodeString = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? "4154F30F2CC64C8B251E0000004C8965B8415C" : "4154F30F2CC64C8B251E0000004C8965B0415C";
            XpOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.XPaddr, 7);
            CodeCave2 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.XPaddr, StringToBytes(InsideCaveCodeString), 7, size: 30);
            
            UnlocksPage.Up.XpNum_OnValueChanged(new object(), new RoutedPropertyChangedEventArgs<double?>(UnlocksPage.Up.XpNum.Value, UnlocksPage.Up.XpNum.Value));
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPAmountaddr, new byte[] { 0xB9, 0x01, 0x00, 0x00, 0x00, 0x90 });
            XpJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.XPaddr, 7);
            
            XpFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, XpJmpBytes);
        }
    }
    
    
    public static void GetWayPointAddr()
    {        
        GetWayPointAddrOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.WayPointxASMAddr, 7);
        CodeCave3 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.WayPointxASMAddr, StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 4" ? "48893D220000000F1097A0030000" : "0F10973002000048893D1B000000"), 7, size: 30);

        while (MainWindow.mw.m.ReadMemory<long>(CodeCave3 + 41) == 0)
        {
            Thread.Sleep(500);
        }
    }
    
    public static void GlowingPaint()
    {
        if (GlowingPaintFirstTime)
        {
            var CodeCaveBytes = StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? "0F590D490000000F110AC642F001" : "0F590D49000000410F114A10");
            var ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 5" ? 7 : 5;
            
            GlowingPaintOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.GlowingPaintAddr, ReplaceCount);
            
            CodeCave4 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.GlowingPaintAddr, CodeCaveBytes, ReplaceCount, size: 30);
            GlowingPaintJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.GlowingPaintAddr, ReplaceCount);
            
            GlowingPaintFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.GlowingPaintAddr, GlowingPaintJmpBytes);
        }
    }
    
    
    public static void RemoveBuildCap()
    {
        if (BuildCapFirstTime)
        {
            BuildCapOrigBytes1 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM1, 8);
            BuildCapOrigBytes2 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM2, 5);
            
            var CodeCave5Bytes = StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? "F30F11834C040000C7834C04000000000000" : "F30F11B3DC030000C783DC03000000000000");
            var CodeCave6Bytes = StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? "F30F114344C7434400000000" : "F30F114330C7433000000000");
        
            CodeCave5 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM1, CodeCave5Bytes, 8, size: 30);
            CodeCave6 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM2, CodeCave6Bytes, 5, size: 30);
            
            BuildCapJmpBytes1 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM1, 8);
            BuildCapJmpBytes2 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM2, 5);
            
            BuildCapFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, BuildCapJmpBytes1);
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, BuildCapJmpBytes2);
        }
    }
    
    
    public static void GetBaseAddress()
    {
        var ReplaceCount = 8;
        BaseAddressHookOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BaseAddrHook, ReplaceCount);
        var CodeCaveBytes = StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? BitConverter.ToString(BaseAddressHookOrigBytes).Replace("-", string.Empty) + "4881E97005000048890D2B0000004881C170050000" : "F30F1081900100004881E96005000048890D2B0000004881C160050000");
        CodeCave7 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BaseAddrHook, CodeCaveBytes, ReplaceCount, size: 30);
    }
    
    
    public static void Credits()
    {
        if (CreditsFirstTime)
        {
            CreditsOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.CreditsHookAddr, 7);
            CodeCave8 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.CreditsHookAddr, StringToBytes("488B052E00000089842480000000"), 7, size: 30);
            CreditsJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.CreditsHookAddr, 7);
            CreditsFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr, CreditsJmpBytes);
        }
    }
    
    
    public static void GetUnbreakableSkillComboAddr()
    {
        CodeCave9 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.UnbSkillHookAddr, StringToBytes("48891D49000000488B10488BC8"), 6, size: 30);
        Self_Vehicle_Addrs.UnbSkillBase = 0x0;

        while (Self_Vehicle_Addrs.UnbSkillBase == 0x0)
        {
            Self_Vehicle_Addrs.UnbSkillBase = MainWindow.mw.m.ReadMemory<long>((CodeCave9 + 0x50).ToString("X")); 
            Thread.Sleep(250);
        }

        Self_Vehicle_Addrs.WorldCollisionThreshold = (Self_Vehicle_Addrs.UnbSkillBase + 44).ToString("X");
        Self_Vehicle_Addrs.CarCollisionThreshold = (Self_Vehicle_Addrs.UnbSkillBase + 48).ToString("X");
        Self_Vehicle_Addrs.SmashableCollisionTolerance = (Self_Vehicle_Addrs.UnbSkillBase + 52).ToString("X");
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.UnbSkillHookAddr, new byte[] { 0x48, 0x8B, 0x10, 0x48, 0x8B, 0xC8 });
            
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.WorldCollisionThreshold, (float)9999999999);
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.CarCollisionThreshold,(float)9999999999);
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.SmashableCollisionTolerance,(float)9999999999);
    }
    
    
    public static void Flyhack()
    {
        if (FlyhackFirstTime)
        {
            FlyhackOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.RotationAddr, 9);
            CodeCave10 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.RotationAddr, StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? "483B0D190000000F8409000000F3440F108994000000" : "483B0D190000000F8409000000F3440F1089F4000000"), 9, size: 30);
            MainWindow.mw.m.WriteArrayMemory(((long)CodeCave10 + 32).ToString("X"), MainWindow.mw.m.ReadArrayMemory<byte>(CodeCave7 + 65, 8));
            FlyhackJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.RotationAddr, 9);
            FlyhackFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, FlyhackJmpBytes);
        }
    } 
    
    public static void Fov() 
    {
        if (FovFirstTime)
        {
            FovOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.FovHookAddr, 5);
            CodeCave11 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.FovHookAddr, StringToBytes("0F1001807926EC0F8519000000807927410F850F00000080792A800F8505000000E91E000000807926160F851B000000807927430F851100000080792AC00F85070000000F100507000000B001"), 5, size: 100);
            FovJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.FovHookAddr, 5);
            FovFirstTime = false;
            return;
        }

        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.FovHookAddr, FovJmpBytes);
    }
}