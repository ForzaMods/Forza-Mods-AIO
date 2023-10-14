using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Tuning;
using Memory;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

internal abstract class ASM
{
    #region Utils

    static byte[] StringToBytes(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex cannot be null/empty/whitespace");

        if (hex.Length % 2 != 0)
            throw new FormatException("Hex must have an even number of characters");

        bool startsWithHexStart = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase);

        if (startsWithHexStart && hex.Length == 2)
            throw new ArgumentException("There are no characters in the hex string");


        int startIndex = startsWithHexStart ? 2 : 0;

        byte[] bytesArr = new byte[(hex.Length - startIndex) / 2];

        try
        {
            int x = 0;
            for (int i = startIndex; i < hex.Length; i += 2, x++)
            {
                var left = hex[i];
                var right = hex[i + 1];
                bytesArr[x] = (byte)((Hexmap[left] << 4) | Hexmap[right]);
            }
            return bytesArr;
        }
        catch (KeyNotFoundException)
        {
            throw new FormatException("Hex string has non-hex character");
        }
    }

    private static readonly Dictionary<char, byte> Hexmap = new()
    {
        { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
        { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
        { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
        { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
        { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
        { '8', 0x8 },{ '9', 0x9 }
    };
    
    
    // 0x8000 == MEM_RELEASE
    public static void FreeMem()
    {
        // Self/Vehicle
        if (Self_Vehicle_Addrs.CodeCave4 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave4, 0, 0x8000);
        
        if (Self_Vehicle_Addrs.CodeCave2 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave2, 0, 0x8000);

        if (Self_Vehicle_Addrs.CodeCave3 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave3, 0, 0x8000);

        if (Self_Vehicle_Addrs.CodeCave9 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave9, 0, 0x8000);
        
        if (Self_Vehicle_Addrs.CodeCave10 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave10, 0, 0x8000);

        if (Self_Vehicle_Addrs.CodeCave11 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave11, 0, 0x8000);
        
        if (Self_Vehicle_Addrs.CodeCave12 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave12, 0, 0x8000);

        if (Self_Vehicle_Addrs.CodeCave13 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave13, 0, 0x8000);

        if (Self_Vehicle_Addrs.CodeCave14 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Self_Vehicle_Addrs.CodeCave14, 0, 0x8000);
        
        
        // Tuning
        if (Tuning_Addresses.TuningCodeCave1 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Tuning_Addresses.TuningCodeCave1, 0, 0x8000);
   
        if (Tuning_Addresses.TuningCodeCave2 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Tuning_Addresses.TuningCodeCave2, 0, 0x8000);
   
        if (Tuning_Addresses.TuningCodeCave3 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Tuning_Addresses.TuningCodeCave3, 0, 0x8000);
   
        if (Tuning_Addresses.TuningCodeCave4 != IntPtr.Zero)
            Imps.VirtualFreeEx(MainWindow.mw.gvp.Process.Handle, (UIntPtr)Tuning_Addresses.TuningCodeCave4, 0, 0x8000);
    }
    
    public static byte[] OriginalBaseAddressHookBytes;
    private static byte[] CreditsJmpBytes;
    private static byte[] GlowingPaintJmpBytes;
    private static byte[] RemoveBuildCapJmpBytes1;
    private static byte[] RemoveBuildCapJmpBytes2;
    private static byte[] XpJmpBytes;
    private static byte[] FlyhackJmpBytes;
    public static bool CreditsFirstTime = true;
    public static bool GlowingPaintFirstTime = true;
    public static bool RemoveBuildCapFirstTime = true;
    public static bool XpFirstTime = true;
    public static bool FlyhackFirstTime = true;
    #endregion 

    #region ASM

    /*public void GetCheckXAddr(IntPtr CodeCave, out string CheckpointBaseAddr)
    {
        string CodeCaveAddrString = ((long)CodeCave).ToString("X"); // CodeCave address
        string CodeCavejmpString = ((long)CodeCave - (Self_Vehicle_Addrs.CheckPointxASMAddrLong + 5)).ToString("X"); // address to calculate jump from
        if (CodeCavejmpString.Length % 2 != 0)
            CodeCavejmpString = "0" + CodeCavejmpString;
        byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
        Array.Reverse(CodeCaveAddr);

        string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
        byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

        byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.CheckPointxASMAddrLong + 6 - (long)(CodeCave + 19)); // (address + jmp code) - address of end of code cave
        Array.Reverse(jmpBackBytes);
        string InsideCaveCodeString = "48890D21000000" + "0F288960020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
        if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
            InsideCaveCodeString = "48890D21000000" + "0F108960020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
        byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

        MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
        MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.CheckPointxASMAddr, JmpToCodeCaveCode);

        byte[] CheckBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
        Array.Reverse(CheckBaseAddrArray);
        CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);

        while (CheckpointBaseAddr == "0000000000000000")
        {
            CheckBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
            Array.Reverse(CheckBaseAddrArray);
            CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);
            if (Self_Vehicle.s.CheckPointTPworker.CancellationPending)
                break;
        }
    }

    public void GetAIXAddr(IntPtr CodeCave6)
    {
        string CodeCaveAddrString = ((long)CodeCave6).ToString("X"); // CodeCave address
        string CodeCavejmpString = ((long)CodeCave6 - (Self_Vehicle_Addrs.AIXAobAddrLong + 5)).ToString("X"); // address to calculate jump from
        if (CodeCavejmpString.Length % 2 != 0)
            CodeCavejmpString = "0" + CodeCavejmpString;
        byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
        Array.Reverse(CodeCaveAddr);

        string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
        byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

        byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.AIXAobAddrLong + 7 - (long)(CodeCave6 + 30)); // (address + jmp code) - address of end of code cave
        Array.Reverse(jmpBackBytes);
        string InsideCaveCodeString = "58488B052D000000488B00483B414075040F114140488BFA50E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
        if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
            //FH5
            InsideCaveCodeString = "58488B052D000000488B00483B415075040F1141500F28EB50E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
        byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

        MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
        MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.AIXAobAddr, JmpToCodeCaveCode);

        while (Self_Vehicle.s.FreezeAIBox.Checked)
        {
            if (Self_Vehicle.s.FreezeAIWorker.CancellationPending)
                break;
            try
            {
                byte[] MyAddr = StringToBytes("0" + ((long)MainWindow.mw.m.Get64BitCode(Self_Vehicle_Addrs.xAddr)).ToString("X"));
                Array.Reverse(MyAddr);
                MainWindow.mw.m.WriteBytes(((long)CodeCave6 + 53).ToString("X"), MyAddr);
            }
            catch { }
            Thread.Sleep(10);
        }
    }*/
    public static void GetWayPointXAddr()
    {
        Self_Vehicle_Addrs.CodeCave4 = (IntPtr)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.WayPointxASMAddr, StringToBytes("48893D220000000F1097A0030000"), 7);

        Thread.Sleep(25);
        long WayPointBaseAddr = MainWindow.mw.m.ReadMemory<long>((Self_Vehicle_Addrs.CodeCave4 + 41).ToString("X"));

        while (WayPointBaseAddr == 0)
        {
            Thread.Sleep(10);
            try
            {
                WayPointBaseAddr = MainWindow.mw.m.ReadMemory<long>((Self_Vehicle_Addrs.CodeCave4 + 41).ToString("X"));
            }
            catch { }
        }

        if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
        {
            Self_Vehicle_Addrs.WayPointxAddr = (WayPointBaseAddr + 928).ToString("X");
            Self_Vehicle_Addrs.WayPointyAddr = (WayPointBaseAddr + 932).ToString("X");
            Self_Vehicle_Addrs.WayPointzAddr = (WayPointBaseAddr + 936).ToString("X");
        }
        else
        {
            Self_Vehicle_Addrs.WayPointxAddr = (WayPointBaseAddr + 592).ToString("X");
            Self_Vehicle_Addrs.WayPointyAddr = (WayPointBaseAddr + 596).ToString("X");
            Self_Vehicle_Addrs.WayPointzAddr = (WayPointBaseAddr + 600).ToString("X");
        }

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

        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.WayPointxASMAddr, MainWindow.mw.gvp.Name == "Forza Horizon 5" ? new byte[] { 0x0F, 0x10, 0x97, 0x50, 0x02, 0x00, 0x00 } : new byte[] { 0x0F, 0x10, 0xA0, 0x90, 0x03, 0x00, 0x00 });
    }

    public static void GetTimeAddr()
    {
        Self_Vehicle_Addrs.CodeCave2 = (IntPtr)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.TimeNOPAddr, StringToBytes("48891D21000000F20F1143084883C440"), 9, size: 0x256);
        Thread.Sleep(500);
        Self_Vehicle_Addrs.TimeAddr = (MainWindow.mw.m.ReadMemory<long>(((long)Self_Vehicle_Addrs.CodeCave2 + 40).ToString("X")) + 8).ToString("X");
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.TimeNOPAddr, new byte[] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 });
    }

    public static void StartXPtool()
    {
        if (XpFirstTime)
        {
            string InsideCaveCodeString = "4154F30F2CC64C8B251E0000004C8965B8415C";

            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                InsideCaveCodeString = "4154F30F2CC64C8B251E0000004C8965B0415C";

            Self_Vehicle_Addrs.CodeCave3 = (IntPtr)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.XPaddr, StringToBytes(InsideCaveCodeString), 7, size: 0x256);
            UnlocksPage.Up.XpNum_OnValueChanged(new object(), new RoutedPropertyChangedEventArgs<double?>(UnlocksPage.Up.XpNum.Value, UnlocksPage.Up.XpNum.Value));
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPAmountaddr, new byte[] { 0xB9, 0x01, 0x00, 0x00, 0x00, 0x90 });
            XpJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.XPaddr, 5);
            XpFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, XpJmpBytes);
        }
    }

    public static void GlowingPaint()
    {
        if (MainWindow.mw.gvp.Name == "Forza Horizon 5" && GlowingPaintFirstTime)
        {
            var InsideCodeCave = StringToBytes("0F590D490000000F110AC642F001");
            Self_Vehicle_Addrs.CodeCave9 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.GlowingPaintAddr, InsideCodeCave, 7, size: 0x256);
            GlowingPaintJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.GlowingPaintAddr, 7);
            GlowingPaintFirstTime = false;
        }
        else if (GlowingPaintFirstTime)
        {
            var InsideCodeCave = StringToBytes("0F590D49000000410F114A10");
            Self_Vehicle_Addrs.CodeCave9 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.GlowingPaintAddr, InsideCodeCave, 5, size: 0x256);
            GlowingPaintJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.GlowingPaintAddr, 5);
            GlowingPaintFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.GlowingPaintAddr, GlowingPaintJmpBytes);
        }
    }

    public static void RemoveBuildCap()
    {
        if (MainWindow.mw.gvp.Name == "Forza Horizon 5" && RemoveBuildCapFirstTime)
        {
            Self_Vehicle_Addrs.CodeCave10 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM1, StringToBytes("F30F11834C040000C7834C04000000000000"), 8, size: 0x256);
            Self_Vehicle_Addrs.CodeCave11 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM2, StringToBytes("F30F114344C7434400000000"), 5, size: 0x256);

            RemoveBuildCapJmpBytes1 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM1, 8);
            RemoveBuildCapJmpBytes2 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM2, 5);
            
            RemoveBuildCapFirstTime = false;
        }
        else if (RemoveBuildCapFirstTime)
        {
            Self_Vehicle_Addrs.CodeCave10 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM1, StringToBytes("F30F11B3DC030000C783DC03000000000000"), 8, size: 0x256);
            Self_Vehicle_Addrs.CodeCave11 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BuildCapAddrASM2, StringToBytes("F30F114330C7433000000000"), 5, size: 0x256);
            
            RemoveBuildCapJmpBytes1 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM1, 8);
            RemoveBuildCapJmpBytes2 = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BuildCapAddrASM2, 5);

            RemoveBuildCapFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, RemoveBuildCapJmpBytes1);
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, RemoveBuildCapJmpBytes2);
        }
    }
    
    public static void GetBaseAddress()
    {
        OriginalBaseAddressHookBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.BaseAddrHook, 8);
        
        if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
        {
            var InsideCodeCave = StringToBytes(BitConverter.ToString(OriginalBaseAddressHookBytes).Replace("-", string.Empty) + "4881E97005000048890D2B0000004881C170050000");
            Self_Vehicle_Addrs.CodeCave12 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BaseAddrHook, InsideCodeCave, 8, size: 0x256);
        }
        else
        {
            var InsideCaveCode = StringToBytes("488B07488BCF4881EF6005000048893D2D0000004881C760050000");
            Self_Vehicle_Addrs.CodeCave12 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.BaseAddrHook, InsideCaveCode, 6, size: 0x256);
        }

        while (MainWindow.mw.Attached)
        {
            Thread.Sleep(50);
            
            Self_Vehicle_Addrs.BaseAddrLong = MainWindow.mw.m.ReadMemory<long>((Self_Vehicle_Addrs.CodeCave12 + 65).ToString("X"));
            Self_Vehicle_Addrs.BaseAddr = Self_Vehicle_Addrs.BaseAddrLong.ToString("X");
            
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                Self_Vehicle_Addrs.AddressesFive();
            else
                Self_Vehicle_Addrs.AddressesFour();
        }
    }

    public static void FH4TuningAddressesHook()
    {
        Tuning_Addresses.TuningCodeCave1 = (IntPtr)MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook1, StringToBytes("4C893549000000498B068BD6"), 5, size: 0x256);
        Tuning_Addresses.TuningCodeCave2 = (IntPtr)MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook2, StringToBytes("4C893D49000000498B07488D5577"), 7, size: 0x256);
        Tuning_Addresses.TuningCodeCave3 = (IntPtr)MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook3, StringToBytes("51488BC848890D45000000590F28CEF30F1010"), 7, size: 0x256);
        Tuning_Addresses.TuningCodeCave4 = (IntPtr)MainWindow.mw.m.CreateDetour(Tuning_Addresses.TuningTableHook4, StringToBytes("488B0748893D46000000488D9560020000"), 10, size: 0x256);
        
        // Address reading
        while (MainWindow.mw.Attached)
        {
            Tuning_Addresses.TuningTableBase1Long = MainWindow.mw.m.ReadMemory<long>(((long)Tuning_Addresses.TuningCodeCave1 + 0x50).ToString("X"));
            Tuning_Addresses.TuningTableBase2Long = MainWindow.mw.m.ReadMemory<long>(((long)Tuning_Addresses.TuningCodeCave2 + 0x50).ToString("X"));
            Tuning_Addresses.TuningTableBase3Long = MainWindow.mw.m.ReadMemory<long>(((long)Tuning_Addresses.TuningCodeCave3 + 0x50).ToString("X"));
            Tuning_Addresses.TuningTableBase4Long = MainWindow.mw.m.ReadMemory<long>(((long)Tuning_Addresses.TuningCodeCave4 + 0x50).ToString("X")) + 400;
            Tuning_Addresses.AddressesFH4();
            Thread.Sleep(1000);
        }
    }
    
    public static void Credits()
    {
        if (CreditsFirstTime)
        {
            Self_Vehicle_Addrs.CodeCave13 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.CreditsHookAddr, StringToBytes("488B052E00000089842480000000"), 7, size: 0x256);
            CreditsJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.CreditsHookAddr, 7);
            CreditsFirstTime = false;
            return;
        }

        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr, CreditsJmpBytes);
    }

    public static void GetUnbreakableSkillComboAddr()
    {
        Self_Vehicle_Addrs.CodeCave14 = (nint)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.UnbSkillHookAddr, StringToBytes("48891D49000000488B10488BC8"), 6, size: 0x256);
        Self_Vehicle_Addrs.UnbSkillAddrLong = 0x0;

        while (Self_Vehicle_Addrs.UnbSkillAddrLong == 0x0)
        {
            Self_Vehicle_Addrs.UnbSkillAddrLong = MainWindow.mw.m.ReadMemory<long>((Self_Vehicle_Addrs.CodeCave14 + 0x50).ToString("X")); 
            Thread.Sleep(50);
        }

        Self_Vehicle_Addrs.UnbSkillAddrLong += 0x2C;
        Self_Vehicle_Addrs.UnbSkillAddr = Self_Vehicle_Addrs.UnbSkillAddrLong.ToString("X");
        MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.UnbSkillHookAddr, new byte[] { 0x48, 0x8B, 0x10, 0x48, 0x8B, 0xC8 });
            
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.UnbSkillAddr, (float)9999999999);
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 4).ToString("X"),(float)9999999999);
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 8).ToString("X"),(float)9999999999);
    }

    public static void Flyhack()
    {
        if (FlyhackFirstTime)
        {
            Self_Vehicle_Addrs.CodeCave8 = (IntPtr)MainWindow.mw.m.CreateDetour(Self_Vehicle_Addrs.RotationAddr, StringToBytes("483B0D190000000F8409000000F3440F108994000000"), 9);
            byte[] MyAddr = StringToBytes("0" + ((long)MainWindow.mw.m.GetCode(Self_Vehicle_Addrs.Rotation) - 148).ToString("X"));
            Array.Reverse(MyAddr);
            MainWindow.mw.m.WriteArrayMemory(((long)Self_Vehicle_Addrs.CodeCave8 + 32).ToString("X"), MyAddr);
            FlyhackJmpBytes = MainWindow.mw.m.ReadArrayMemory<byte>(Self_Vehicle_Addrs.RotationAddr, 9);
            FlyhackFirstTime = false;
        }
        else
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.RotationAddr, FlyhackJmpBytes);
        }
    }
    #endregion
}