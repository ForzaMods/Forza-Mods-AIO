using System;
using System.Threading;
using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Memory;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

public class Self_Vehicle_ASM : Resources.ASM
{
    private static UIntPtr CodeCave1, CodeCave2, CodeCave3, CodeCave4, CodeCave5, CodeCave6, CodeCave7, CodeCave8, CodeCave9, CodeCave10;
    private static bool CreditsFirstTime = true, GlowingPaintFirstTime = true, BuildCapFirstTime = true, XpFirstTime = true, FlyhackFirstTime = true;
    private static byte[] CreditsJmpBytes, GlowingPaintJmpBytes, BuildCapJmpBytes1, BuildCapJmpBytes2, XpJmpBytes, FlyhackJmpBytes;
    private static byte[] CreditsOrigBytes, GlowingPaintOrigBytes, BuildCapOrigBytes1, BuildCapOrigBytes2, XpOrigBytes, FlyhackOrigBytes, GetWayPointAddrOrigBytes, BaseAddressHookOrigBytes;
    
    
    public static void Cleanup()
    {
        CleanCodeCaveJumps();
        FreeMem();
    }

    
    private static void CleanCodeCaveJumps()
    {
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr, CreditsOrigBytes);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.GlowingPaintAddr, GlowingPaintOrigBytes);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, BuildCapOrigBytes1);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, BuildCapOrigBytes2);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, XpOrigBytes);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, FlyhackOrigBytes);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.WayPointxASMAob, GetWayPointAddrOrigBytes);
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BaseAddrHook, BaseAddressHookOrigBytes);
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
    }
    
    
    public static void GetTimeAddr()
    {
        CodeCave1 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.TimeNOPAddr, StringToBytes("48891D21000000F20F1143084883C440"), 9, size: 30);
        Thread.Sleep(25);
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
        CodeCave3 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.WayPointxASMAddr, StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 4" ? "48893D220000000F1097A0030000" : "48893D220000000F109750020000"), 7, size: 30);
        long WayPointBaseAddr = MainWindow.mw.m.ReadMemory<long>((CodeCave3 + 41).ToString("X"));

        while (WayPointBaseAddr == 0)
        {
            WayPointBaseAddr = MainWindow.mw.m.ReadMemory<long>((CodeCave3 + 41).ToString("X"));
            Thread.Sleep(250);
        }

        Self_Vehicle_Addrs.WayPointxAddr = (WayPointBaseAddr + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 928 : 592).ToString("X");
        Self_Vehicle_Addrs.WayPointyAddr = (WayPointBaseAddr + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 932 : 596).ToString("X");
        Self_Vehicle_Addrs.WayPointzAddr = (WayPointBaseAddr + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 936 : 600).ToString("X");

        float WayPointX = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.WayPointxAddr);
        float WayPointY = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.WayPointyAddr) + 3;
        float WayPointZ = MainWindow.mw.m.ReadMemory<float>(Self_Vehicle_Addrs.WayPointzAddr);
        
        if (WayPointX != 0 && WayPointY != 0 && WayPointZ != 0 &&
            WayPointX is < 10000 and > -10000 &&
            WayPointY is < 1000 and > -1000 &&
            WayPointZ is < 10000 and > -10000)
        {
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.xAddr, WayPointX);
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.yAddr, WayPointY);
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.zAddr, WayPointZ);
        }

        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.WayPointxASMAddr, GetWayPointAddrOrigBytes);
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
        var ReplaceCount = MainWindow.mw.gvp.Name == "Forza Horizon 5" ? 8 : 6;
        BaseAddressHookOrigBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BaseAddrHook, ReplaceCount);
        var CodeCaveBytes = StringToBytes(MainWindow.mw.gvp.Name == "Forza Horizon 5" ? BitConverter.ToString(BaseAddressHookOrigBytes).Replace("-", string.Empty) + "4881E97005000048890D2B0000004881C170050000" : "488B07488BCF4881EF6005000048893D2D0000004881C760050000");
        CodeCave7 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BaseAddrHook, CodeCaveBytes, ReplaceCount, size: 30);

        while (MainWindow.mw.Attached)
        {
            Self_Vehicle_Addrs.BaseAddrLong = MainWindow.mw.m.ReadMemory<long>((CodeCave7 + 65).ToString("X"));
            Self_Vehicle_Addrs.BaseAddr = Self_Vehicle_Addrs.BaseAddrLong.ToString("X");
            
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                Self_Vehicle_Addrs.AddressesFive();
            else
                Self_Vehicle_Addrs.AddressesFour();
            
            Thread.Sleep(1000);
        }
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
        Self_Vehicle_Addrs.UnbSkillAddrLong = 0x0;

        while (Self_Vehicle_Addrs.UnbSkillAddrLong == 0x0)
        {
            Self_Vehicle_Addrs.UnbSkillAddrLong = MainWindow.mw.m.ReadMemory<long>((CodeCave9 + 0x50).ToString("X")); 
            Thread.Sleep(250);
        }

        Self_Vehicle_Addrs.WorldCollisionThreshold = (Self_Vehicle_Addrs.UnbSkillAddrLong + 44).ToString("X");
        Self_Vehicle_Addrs.CarCollisionThreshold = (Self_Vehicle_Addrs.UnbSkillAddrLong + 48).ToString("X");
        Self_Vehicle_Addrs.SmashableCollisionTolerance = (Self_Vehicle_Addrs.UnbSkillAddrLong + 52).ToString("X");
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
            CodeCave10 = MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.RotationAddr, StringToBytes("483B0D190000000F8409000000F3440F108994000000"), 9, size: 30);
            byte[] MyAddr = StringToBytes("0" + ((long)MainWindow.mw.m.GetCode(Self_Vehicle_Addrs.Rotation) - 148).ToString("X"));
            Array.Reverse(MyAddr);
            MainWindow.mw.m.WriteArrayMemory(((long)CodeCave10 + 32).ToString("X"), MyAddr);
            FlyhackJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.RotationAddr, 9);
            FlyhackFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, FlyhackJmpBytes);
        }
    }
}