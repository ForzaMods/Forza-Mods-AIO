using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Memory;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

internal class Self_Vehicle_Addrs
{
    int ScanAmount = 37;

    #region Addresses - Longs

    public static long UnbSkillHookLong;
    public static long UnbSkillAddrLong = 0;
    public static long CreditsHookAddrLong;
    public static long BaseAddrHookLong;
    public static long TimeNOPAddrLong;
    public static long CheckPointxASMAddrLong;
    public static long WayPointxASMAddrLong;
    public static long BaseAddrLong;
    public static long Base2AddrLong;
    public static long Base3AddrLong;
    public static long Base4AddrLong;
    public static long Car1AddrLong;
    public static long Car2AddrLong;
    public static long Wall1AddrLong;
    public static long Wall2AddrLong;
    public static long FOVnopOutAddrLong;
    public static long FOVnopInAddrLong;
    public static long FirstPersonAddrLong;
    public static long DashAddrLong;
    public static long FrontAddrLong;
    public static long LowAddrLong;
    public static long BonnetAddrLong;
    public static long CurrentIDAddrLong;
    public static long OOBnopAddrLong;
    public static long SuperCarAddrLong;
    public static long WorldRGBAddrLong;
    public static long FOVJmpAddrLong;
    public static long DiscoverRoadsAddrLong;
    public static long WaterAddrLong;
    public static long AIXAobAddrLong;
    public static long CosmeticUnlockAddrLong;
    public static long HornAsmAddrLong;
    public static long XPaddrLong;
    public static long XPAmountaddrLong;
    public static long CameraSpeedBase;
    public static long CameraBase;
    public static long CameraShutterSpeed;
    public static long GlowingPaintAddrLong;
    public static long BuildCapAddrASM1Long;
    public static long BuildCapAddrASM2Long;
    public static long NoClipAddrLong;
    #endregion

    #region Addresses - AOB's

    public static string UnbreakableSkillComboSig;
    public static string BaseAddrHookAob;
    public static string BaseAob;
    public static string Base2Aob;
    public static string RGBAob;
    public static string Car1Aob;
    public static string Car2Aob;
    public static string Wall1Aob;
    public static string Wall2Aob;
    public static string FOVOutAob;
    public static string FOVInAob;
    public static string FOVJmpAob;
    public static string TimeAob;
    public static string CheckPointxASMAob;
    public static string WayPointxASMAob;
    public static string FirstPersonAob;
    public static string DashAob;
    public static string LowAob;
    public static string BonnetAob;
    public static string FrontAob;
    public static string XPAob;
    public static string XPAmountAob;
    public static string CurrentIDAob;
    public static string OOBAob;
    public static string SuperCarAob;
    public static string DLCPatchAob;
    public static string CarIdAob;
    public static string DiscoverRoadsAob;
    public static string WaterAob;
    public static string AIXAob;
    public static string CosmeticUnlockAob;
    public static string HornAsmAob;
    public static string CameraSpeedBaseAob;
    public static string CameraBaseAob;
    public static string CameraShutterSpeedAob;
    public static string GlowingPaintSig;
    public static string NoClipSig;
    public static string BuildCap1Sig;
    public static string BuildCap2Sig;
    public static string CreditsASMAob;

    #endregion

    #region Addresses
    
    public static string UnbSkillHookAddr;
    public static string UnbSkillAddr;
    public static string CreditsHookAddr;
    public static string BaseAddrHook;
    public static string BuildCapAddrASM1;
    public static string BuildCapAddrASM2;
    public static string BaseAddr;
    public static string Base2Addr;
    public static string Base3Addr;
    public static string Base4Addr;
    public static string Car1Addr;
    public static string Car2Addr;
    public static string FOVnopOutAddr;
    public static string FOVnopInAddr;
    public static string TimeNOPAddr;
    public static string TimeAddr;
    public static string Wall1Addr;
    public static string Wall2Addr;
    public static string FrontLeftAddr;
    public static string FrontRightAddr;
    public static string BackLeftAddr;
    public static string BackRightAddr;
    public static string OnGroundAddr;
    public static string InRaceAddr;
    public static string PastStartAddr;
    public static string PastIntroAddr = null;
    public static string xVelocityAddr;
    public static string yVelocityAddr;
    public static string zVelocityAddr;
    public static string xAddr;
    public static string yAddr;
    public static string zAddr;
    public static string CheckPointxAddr;
    public static string CheckPointyAddr;
    public static string CheckPointzAddr;
    public static string CheckPointxASMAddr;
    public static string WayPointxAddr;
    public static string WayPointyAddr;
    public static string WayPointzAddr;
    public static string WayPointxASMAddr;
    public static string YawAddr;
    public static string RollAddr;
    public static string PitchAddr;
    public static string yAngVelAddr;
    public static string GasAddr;
    public static string GravityAddr;
    public static string WeirdAddr;
    public static string FOVHighAddr;
    public static string FOVInAddr;
    public static string FirstPersonAddr;
    public static string DashAddr;
    public static string FrontAddr;
    public static string BonnetAddr;
    public static string LowAddr;
    public static string LowCompare;
    public static string CurrentIDAddr;
    public static string TimeAddrAddr;
    public static string allocationstring;
    public static string OOBnopAddr;
    public static string SuperCarAddr;
    public static string WorldRGBAddr;
    public static string InPauseAddr;
    public static string InHouseAddr;
    public static string TestAddr;
    public static string FOVJmpAddr;
    public static string CheckPointBaseAddr = null;
    public static string WayPointBaseAddr = null;
    public static string DiscoverRoadsAddr = null;
    public static string TotalXpAddr = null;
    public static string WaterAddr = null;
    public static string AIXAobAddr = null;
    public static string CosmeticUnlockAddr = null;
    public static string HornAsmAddr = null;
    public static string XPaddr = null;
    public static string XPAmountaddr = null;
    public static string TurnAndZoomSpeed;
    public static string MovementSpeed;
    public static string ShutterSpeed;
    public static string Samples;
    public static string SamplesMultiplier;
    public static string ApertureScale;
    public static string CarInFocus;
    public static string TimeSlice;
    public static string NoClipAddr;
    public static string MaxHeightAddr;
    public static string MinFovAddr;
    public static string GlowingPaintAddr;

    #endregion

    #region Addresses - CodeCaves
    public static IntPtr CCBA = (IntPtr)0;
    public static IntPtr CCBA2 = (IntPtr)0;
    public static IntPtr CCBA3 = (IntPtr)0;
    public static IntPtr CCBA4 = (IntPtr)0;
    public static IntPtr CCBA5 = (IntPtr)0;
    public static IntPtr CCBA6 = (IntPtr)0;
    public static IntPtr CCBA7 = (IntPtr)0;
    public static IntPtr CCBA8 = (IntPtr)0;
    public static IntPtr CCBA9 = (IntPtr)0;
    public static IntPtr CCBA10 = (IntPtr)0;
    public static IntPtr CCBA11 = (IntPtr)0;
    public static IntPtr CCBA12 = (IntPtr)0;
    public static IntPtr CCBA13 = (IntPtr)0;
    public static IntPtr CCBA14 = (IntPtr)0;
    public static IntPtr CodeCave = (IntPtr)0;
    public static IntPtr CodeCave2 = (IntPtr)0;
    public static IntPtr CodeCave3 = (IntPtr)0;
    public static IntPtr CodeCave4 = (IntPtr)0;
    public static IntPtr CodeCave5 = (IntPtr)0;
    public static IntPtr CodeCave6 = (IntPtr)0;
    public static IntPtr CodeCave7 = (IntPtr)0;
    public static IntPtr CodeCave8 = (IntPtr)0;
    public static IntPtr CodeCave9 = (IntPtr)0;
    public static IntPtr CodeCave10 = (IntPtr)0;
    public static IntPtr CodeCave11 = (IntPtr)0;
    public static IntPtr CodeCave12 = (IntPtr)0;
    public static IntPtr CodeCave13 = (IntPtr)0;
    public static IntPtr CodeCave14 = (IntPtr)0;
    #endregion

    #region Addresses - FOV

    private static long FovScanEndAddr;
    private static long FovScanStartAddr;

    public static List<string> RearAddresses = new();
    public static List<string> RestAddresses = new();

    #endregion

    #region Addresses - Stats

    private static long StatsScanEndAddr;
    private static long StatsScanStartAddr;

    public static List<string> Addresses = new();

    #endregion

    #region Offsets + AOB's

    private static void AddressesFour()
    {
        FrontLeftAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD18,0xC");
        FrontRightAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD20,0xC");
        BackLeftAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD38,0xC");
        BackRightAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD30,0xC");
        OnGroundAddr = (BaseAddr + ",0x550,0x260,0x258,0x4B0,0x640,0x368,0x10C");
        InRaceAddr = (Base2Addr + ",0x80,0x8,0x38,0x58,0x28,0x18,0x230");
        xVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x540");
        yVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x53C");
        zVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x538");
        xAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x520");
        yAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x51C");
        zAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x518");
        YawAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x3FC");
        RollAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x418");
        PitchAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x410");
        yAngVelAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x52C");
        GasAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD18,-0x53C");
        PastStartAddr = (Base2Addr + ",0x80,0x8,0x38,0x58,0x28,0x18,0x5C");
        InPauseAddr = (Base2Addr + ",0x80,0x8,0x38,0x58,0x28,0x18,0x3D8");
        FOVHighAddr = (BaseAddr + ",0x568,0x270,0x258,0xB8,0x348,0x70,0x5B0");
        WeirdAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x554");
        GravityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x558");
        TurnAndZoomSpeed = (CameraSpeedBase - 0x4).ToString("X");
        MovementSpeed = (CameraSpeedBase + 0x4).ToString("X");
        Samples = (CameraBase).ToString("X");
        SamplesMultiplier = (CameraBase + 0xC).ToString("X");
        ApertureScale = (CameraBase + 0x20).ToString("X");
        CarInFocus = (CameraBase + 0x30).ToString("X");
        TimeSlice = (CameraBase + 0x38).ToString("X");
        NoClipAddr = (CameraBase - 440064).ToString("X");
        MinFovAddr = (CameraBase - 440048).ToString("X");
        MaxHeightAddr = (CameraBase - 440532).ToString("X");
        Application.Current.Dispatcher.Invoke(() =>
        {
            CameraPage.CamPage.CarInFocusBox.Value = MainWindow.mw.m.ReadFloat(CarInFocus);
            CameraPage.CamPage.SamplesBox.Value = MainWindow.mw.m.ReadInt(Samples);
            CameraPage.CamPage.TimeSliceBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TimeSlice), 5);
            CameraPage.CamPage.ShutterSpeedBox.Value = MainWindow.mw.m.ReadFloat(ShutterSpeed);
            CameraPage.CamPage.ApertureScaleBox.Value = MainWindow.mw.m.ReadFloat(ApertureScale);
            TeleportsPage.t.TeleportBox.Items.Clear();
            TeleportsPage.t.TeleportBox.Items.Add("Waypoint");
            TeleportsPage.t.TeleportBox.Items.Add("Adventure Park");
            TeleportsPage.t.TeleportBox.Items.Add("Ambleside");
            TeleportsPage.t.TeleportBox.Items.Add("Beach");
            TeleportsPage.t.TeleportBox.Items.Add("Broadway");
            TeleportsPage.t.TeleportBox.Items.Add("Dam");
            TeleportsPage.t.TeleportBox.Items.Add("Edinburgh");
            TeleportsPage.t.TeleportBox.Items.Add("Festival");
            TeleportsPage.t.TeleportBox.Items.Add("Greendale Airstrip");
            TeleportsPage.t.TeleportBox.Items.Add("Lake Island");
            TeleportsPage.t.TeleportBox.Items.Add("Mortimer Gardens");
            TeleportsPage.t.TeleportBox.Items.Add("Quarry");
            TeleportsPage.t.TeleportBox.Items.Add("Railyard");
            TeleportsPage.t.TeleportBox.Items.Add("Start of Motorway");
            TeleportsPage.t.TeleportBox.Items.Add("Top of Mountain");
        });
    }

    public static void AddressesFive()
    {
        FrontLeftAddr = (BaseAddrLong + 0x26C0).ToString("X");
        FrontRightAddr = (BaseAddrLong + 0x3180).ToString("X");
        BackLeftAddr = (BaseAddrLong + 0x4700).ToString("X");
        BackRightAddr = (BaseAddrLong + 0x3C40).ToString("X");
        xVelocityAddr = (BaseAddrLong + 0x20).ToString("X");
        yVelocityAddr = (BaseAddrLong + 0x24).ToString("X");
        zVelocityAddr = (BaseAddrLong + 0x28).ToString("X");
        xAddr = (BaseAddrLong + 0x50).ToString("X");
        yAddr = (BaseAddrLong + 0x54).ToString("X");
        zAddr = (BaseAddrLong + 0x58).ToString("X");
        WeirdAddr = (BaseAddrLong + 0xC).ToString("X");
        GravityAddr = (BaseAddrLong + 0x8).ToString("X");
        OnGroundAddr = (BaseAddrLong + 0x1A4C).ToString("X");
        GasAddr = (BaseAddrLong + 0x1A08).ToString("X");
        YawAddr = (BaseAddrLong + 0xF0).ToString("X");
        RollAddr = (BaseAddrLong + 0xF4).ToString("X");
        PitchAddr = (BaseAddrLong + 0x108).ToString("X");
        yAngVelAddr = (BaseAddrLong + 0x34).ToString("X");
        InRaceAddr = (Base2Addr + ",0x50,0x3D8");
        InPauseAddr = (Base2Addr + ",0x50,0x480");
        InHouseAddr = (Base2Addr + ",0x50,0x368");
        TestAddr = (Base2Addr + ",0x50,0x218");
        FOVHighAddr = (Base3Addr + ",0x538,0xF0,0xD80,0x6F0");
        TurnAndZoomSpeed = (CameraSpeedBase + 0x2E0).ToString("X");
        MovementSpeed = (CameraSpeedBase + 0x2DC).ToString("X");
        Samples = (CameraBase).ToString("X");
        SamplesMultiplier = (CameraBase + 0xC).ToString("X");
        ApertureScale = (CameraBase + 0x20).ToString("X");
        CarInFocus = (CameraBase + 0x30).ToString("X");
        TimeSlice = (CameraBase + 0x38).ToString("X");
        ShutterSpeed = (CameraShutterSpeed - 149).ToString("X");
        NoClipAddr = NoClipAddrLong.ToString("X");
        MinFovAddr = (NoClipAddrLong - 392).ToString("X");
        MaxHeightAddr = (NoClipAddrLong - 0x190).ToString("X");
        Application.Current.Dispatcher.Invoke(() =>
        {
            CameraPage.CamPage.CarInFocusBox.Value = MainWindow.mw.m.ReadFloat(CarInFocus);
            CameraPage.CamPage.SamplesBox.Value = MainWindow.mw.m.ReadInt(Samples);
            CameraPage.CamPage.TimeSliceBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TimeSlice), 5);
            CameraPage.CamPage.ShutterSpeedBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(ShutterSpeed), 5);
            CameraPage.CamPage.ApertureScaleBox.Value = MainWindow.mw.m.ReadFloat(ApertureScale);
            if (TeleportsPage.t.TeleportBox.Items.Contains("Guanajuato (Main City)"))
                return;
            TeleportsPage.t.TeleportBox.Items.Clear();
            TeleportsPage.t.TeleportBox.Items.Add("Waypoint");
            TeleportsPage.t.TeleportBox.Items.Add("Airstrip");
            TeleportsPage.t.TeleportBox.Items.Add("Bridge");
            TeleportsPage.t.TeleportBox.Items.Add("Dirt Circuit");
            TeleportsPage.t.TeleportBox.Items.Add("Dunes");
            TeleportsPage.t.TeleportBox.Items.Add("Golf Course");
            TeleportsPage.t.TeleportBox.Items.Add("Guanajuato (Main City)");
            TeleportsPage.t.TeleportBox.Items.Add("Motorway");
            TeleportsPage.t.TeleportBox.Items.Add("Mulege");
            TeleportsPage.t.TeleportBox.Items.Add("Playa Azul");
            TeleportsPage.t.TeleportBox.Items.Add("River");
            TeleportsPage.t.TeleportBox.Items.Add("Stadium");
            TeleportsPage.t.TeleportBox.Items.Add("Temple");
            TeleportsPage.t.TeleportBox.Items.Add("Temple Drag");
            TeleportsPage.t.TeleportBox.Items.Add("Top Of Volcano");
        });
    }

    private static void Aobs()
    {
        UnbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        CreditsASMAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        BaseAob = "43 3a 5c 57 ? 4e 44 4f 57 53 5c 53 59 53 54 45 4d 33 32 5c 44";
        Car1Aob = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        Car2Aob = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        Wall1Aob = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        Wall2Aob = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        FOVOutAob = "4C 8D ? ? ? 0F 29 ? ? ? F3 0F";
        FOVInAob = "48 81 EC ? ? ? ? 48 8B ? E8 ? ? ? ? 48 8B ? ? 48 8B";
        TimeAob = "20 F2 0F 11 43 08 48 83";
        CheckPointxASMAob = "0F 28 ? ? ? ? ? 0F 29 ? ? 0F 29 ? ? C3 90 48 8B ? 55";
        WayPointxASMAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50 C1 83 E0 07 3C 07";
        FirstPersonAob = "80 00 80 82 43";
        DashAob = "3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 01 ?? 00 00 00 00 00 00 00 00 A0 40";
        LowAob = "80 CD CC 4C 3E CD CC CC";
        BonnetAob = "00 80 3E 63 B8 1E 3F 00 00 80 3F";
        //Front = "A0 41 01 00 8C 42 00 00 11 43 00 00 3E 43 00 00 00 80 00 00 00 80 00 00 80 3E 7B 14 2E 3F";
        FrontAob = "80 3E 7B 14 2E 3F 00 00 80 3F";
        XPAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83 BD C0 00 00 00";
        XPAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        CurrentIDAob = "00 00 50 4C 41 59 45 52 5F 43 41 52 00 00";
        OOBAob = "0F 11 ? ? ? ? ? 0F 5C ? 0F 59 ? 0F 28 ? 0F C6 CA ? F3 0F";
        SuperCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2 ? 0F 11 ? ? 48 83 C1 ? E8 ? ? ? ? 0F 10";
        DiscoverRoadsAob = "00 96 42 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 40 1C 45 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 03 00";
        WaterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00";
        AIXAob = "48 89 ? ? ? 57 48 83 EC ? 0F 10 ? 48 8B ? 0F 11 ? ? 48 8B";
        CameraShutterSpeedAob = "c8 ? ? ? 7f ? 00 18 a4";
        GlowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        BuildCap1Sig = "F3 0F 11 B3 DC 03 00 00 E8";
        BuildCap2Sig = "F3 0F 11 43 30 FF 04 88 EB 61";
        CameraSpeedBaseAob = "cd ? 4c 3f cd ? 4c 3f 0a d7 13 40";
        CameraBaseAob = "27 01 00 00 70";
        CameraShutterSpeedAob = "f0 75 ? 95 f6 7f ? 00 fe";
        GlowingPaintSig = "41 0f 11 4a ? 41 c6 02";
    }

    private static void AobsSteam()
    {
        BaseAob = "7F ? 00 01 00 00 00 03 00 00 00 01 00 00 00 00 00 00 00 64";
    }

    private static void AobsFive()
    {
        UnbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        CreditsASMAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        BaseAddrHookAob = "48 63 ? 48 69 D0 ? ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? ? ? ? C3 C3 40";
        //BaseAob = "7F ? 00 AC ? ? ? ? ? 6E 27 00 24";
        RGBAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        //Base2Aob = "E0 ? 41 ? E1 ? 00 00";
        XPAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83";
        XPAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        Car1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 45 0F ? ? 4C 8B";
        Wall1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 90";
        Wall2Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 0F";
        TimeAob = "20 F2 0F 11 43 08 48 83";
        SuperCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2";
        WayPointxASMAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        FOVJmpAob = "0F 2F ? ? ? 0F 10 ? ? 0F 28 ? 0F 10";
        //OOBaob = "0F 28 ? 0F 28 ? 0F C6 D1 ? 0F 59 ? ? ? ? ? 0F C6 C1 ? 0F 59 ? ? ? ? ? 0F C6 C9 ? 0F 59 ? ? ? ? ? 0F 58 ? 0F 58 ? 0F 58 ? ? 0F 11";
        OOBAob = "48 83 EC ? 0F 10 ? 41 0F ? ? 0F 10"; // + 83 OR + 0x53
        //CheckPointxASMsig = "48 85 ? 74 ? 48 ? ? ? ? ? C7 0F";
        CheckPointxASMAob = "33 C0 48 89 ? 48 89 ? ? 48 E9 ? ? ? ? 90 40 F3";
        CarIdAob = "00 B0 ? ? ? ? 7F ? 00 D8 6E";
        DiscoverRoadsAob = "00 96 ? ? ? ? 42 88";
        WaterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00 80 3F";
        AIXAob = "0F 11 41 50 0F 28 EB";
        CosmeticUnlockAob = "8B 73 58 8B 43 64";
        HornAsmAob = "8B FA 48 8B D9 E8 ? ? ? ? 39 7B 10";
        //TotalXpAddr = (Base3Addr + ",0xEE8,0x408,0x70,0x28,0x30,0x20,0x270");
        CameraSpeedBaseAob = "54 00 52 ? 41 00 ? ? 4B 00 ? 00 00 00 00 00 05";
        CameraShutterSpeedAob = "C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? 00 00";
        CameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        NoClipSig = "54 48 ? 5F 57 69 6E 56 ? ? ? ? 00 00 00 00 0A 00";
        CurrentIDAob = "00 00 50 4C 41 59 45 52 5F 43 41 52 00 00 00 00 00 00 0A";
        BuildCap1Sig = "E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 0F 2E";
        BuildCap2Sig = "F3 0F ? ? ? 48 8B ? ? ? 48 8B ? ? ? 48 83 C4 ? 5F C3 85 FF";
        GlowingPaintSig = "0F 11 0A C6 42 F0 01";
    }

    #endregion

    #region FH5 Scan
    public Task FH5_Scan()
    {
        int ScanIndex = 0;

        AobsFive();
        ScanAmount = 39;

        long ScanStart = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
        long ScanEnd = ScanStart + MainWindow.mw.gvp.Process.MainModule!.ModuleMemorySize;

        Thread HigherPriorityScans = new Thread(async () =>
        {
            try
            {
                BaseAddrHookLong = (await MainWindow.mw.m.AoBScan(ScanStart,ScanEnd, BaseAddrHookAob, true, true)).FirstOrDefault() - 279;
                BaseAddrHook = BaseAddrHookLong.ToString("X");
                assembly.OriginalBaseAddressHookBytes = MainWindow.mw.m.ReadBytes(BaseAddrHook, 8);
                MainWindow.mw.m.ChangeProtection(BaseAddrHook, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                XPaddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, XPAob, false, true)).FirstOrDefault();
                XPaddr = XPaddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(XPaddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                XPAmountaddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, XPAmountAob, false, true)).FirstOrDefault();
                XPAmountaddr = XPAmountaddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(XPAmountaddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                WorldRGBAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, RGBAob, true, false)).LastOrDefault();
                WorldRGBAddr = WorldRGBAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(WorldRGBAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Car1AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Car1Aob, true, true)).FirstOrDefault();
                Car1Addr = Car1AddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(Car1Addr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Wall1AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Wall1Aob, true, true)).FirstOrDefault();
                Wall1Addr = Wall1AddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(Wall1Addr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Wall2AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Wall2Aob, true, true)).FirstOrDefault();
                Wall2Addr = Wall2AddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(Wall2Addr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                SuperCarAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, SuperCarAob, false, true)).LastOrDefault();
                SuperCarAddr = SuperCarAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(SuperCarAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                WaterAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, WaterAob, true, true)).FirstOrDefault() + 309;
                WaterAddr = WaterAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(WaterAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                CreditsHookAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CreditsASMAob, false, true)).FirstOrDefault();
                CreditsHookAddr = CreditsHookAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(CreditsHookAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            }
            catch
            {
            }
        });

        Thread LowerPriorityScans = new Thread(async () =>
        {
            try
            {
                UnbSkillHookLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, UnbreakableSkillComboSig, false, true)).FirstOrDefault() + 9;
                UnbSkillHookAddr = UnbSkillHookLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                GlowingPaintAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, GlowingPaintSig, false, true)).FirstOrDefault();
                GlowingPaintAddr = GlowingPaintAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                FOVJmpAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, FOVJmpAob, true, true)).FirstOrDefault() + 3;
                FOVJmpAddr = FOVJmpAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(FOVJmpAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                TimeNOPAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, TimeAob, true, true)).FirstOrDefault() + 1;
                TimeNOPAddr = TimeNOPAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(TimeNOPAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                WayPointxASMAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, WayPointxASMAob, true, true)).FirstOrDefault();
                WayPointxASMAddr = WayPointxASMAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(WayPointxASMAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                DiscoverRoadsAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, DiscoverRoadsAob, true, true)).FirstOrDefault();
                DiscoverRoadsAddr = DiscoverRoadsAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(DiscoverRoadsAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                BuildCapAddrASM1Long = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, BuildCap1Sig, false, true)).FirstOrDefault() + 25;
                BuildCapAddrASM1 = BuildCapAddrASM1Long.ToString("X");
                MainWindow.mw.m.ChangeProtection(BuildCapAddrASM1, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                BuildCapAddrASM2Long = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, BuildCap2Sig, false, true)).FirstOrDefault();
                BuildCapAddrASM2 = BuildCapAddrASM2Long.ToString("X");
                MainWindow.mw.m.ChangeProtection(BuildCapAddrASM2, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CheckPointxASMAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "0F 10 89 60 02 00 00 0F 29", true,true)).FirstOrDefault();
                CheckPointxASMAddr = CheckPointxASMAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(CheckPointxASMAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                AIXAobAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, AIXAob, true, true)).FirstOrDefault();
                AIXAobAddr = AIXAobAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(AIXAobAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                OOBnopAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, OOBAob, false, true)).FirstOrDefault() + 83;
                OOBnopAddr = OOBnopAddrLong.ToString("X");
                MainWindow.mw.m.ChangeProtection(OOBnopAddr, Imps.MemoryProtection.ExecuteReadWrite, out _);
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CameraSpeedBase = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraSpeedBaseAob, true,true)).FirstOrDefault();
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CameraBase = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraBaseAob, true, true)).FirstOrDefault() - 53;
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CameraShutterSpeed = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraShutterSpeedAob, true,true)).FirstOrDefault();
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                NoClipAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, NoClipSig, true, true)).FirstOrDefault() + 1656;
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CurrentIDAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CurrentIDAob, true, true,false)).FirstOrDefault() + 42;
                CurrentIDAddr = CurrentIDAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                AddressesFive();
                Task.Run(() => assembly.GetBaseAddress(CodeCave12));
            }
            catch
            {
            }
        });

        Thread CodeCaves = new Thread(() =>
        {
            try
            {
                CCBA12 = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
                CodeCave12 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA12, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave12 == (IntPtr)0)
                {
                    CCBA12 += 500000;
                    CodeCave12 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA12, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                CCBA = CCBA12;
                CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave == (IntPtr)0)
                {
                    CCBA += 500000;
                    CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA2 = CCBA;
                CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave2 == (IntPtr)0)
                {
                    CCBA2 += 500000;
                    CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA3 = CCBA2;
                CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave3 == (IntPtr)0)
                {
                    CCBA3 += 500000;
                    CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA4 = CCBA3;
                CodeCave4 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA4, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave4 == (IntPtr)0)
                {
                    CCBA4 += 500000;
                    CodeCave4 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA4, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA5 = CCBA4;
                CodeCave5 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave5 == (IntPtr)0)
                {
                    CCBA5 += 500000;
                    CodeCave5 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA6 = CCBA5;
                CodeCave6 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave6 == (IntPtr)0)
                {
                    CCBA6 += 500000;
                    CodeCave6 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA6, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA7 = CCBA6;
                CodeCave7 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA7, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave7 == (IntPtr)0)
                {
                    CCBA7 += 500000;
                    CodeCave7 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA7, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA8 = CCBA7;
                CodeCave8 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA8, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave8 == (IntPtr)0)
                {
                    CCBA8 += 500000;
                    CodeCave8 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA8, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA10 = CCBA8;
                CodeCave10 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA10, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave10 == (IntPtr)0)
                {
                    CCBA10 += 500000;
                    CodeCave10 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA10, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                CCBA11 = CCBA10;
                CodeCave11 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA11, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave11 == (IntPtr)0)
                {
                    CCBA11 += 500000;
                    CodeCave11 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA11, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                // Credits hook
                CCBA13 = CCBA11;
                CodeCave13 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA13, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave13 == (IntPtr)0)
                {
                    CCBA13 += 500000;
                    CodeCave13 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA13, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                // Unbreakable skill score
                CCBA14 = CCBA11;
                CodeCave14 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA14, 0x256,
                    assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave14 == (IntPtr)0)
                {
                    CCBA14 += 500000;
                    CodeCave14 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA14, 0x256,
                        assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                // glowing paint
                CCBA9 = CCBA14;
                CodeCave9 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA9, 0x58, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave9 == (IntPtr)0)
                {
                    CCBA9 += 500000;
                    CodeCave9 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA9, 0x58, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            }
            catch
            {
            }
        });

        HigherPriorityScans.Priority = ThreadPriority.Highest;
        CodeCaves.Priority = ThreadPriority.AboveNormal;
        
        HigherPriorityScans.Start();
        LowerPriorityScans.Start();
        CodeCaves.Start();
        
        return Task.CompletedTask;
    }
    #endregion

    #region FH4 Scan
    public Task FH4_Scan()
    {
        ScanAmount = 32;
        
        Aobs();
        
        if (MainWindow.mw.gvp.Plat.Contains("Steam"))
            AobsSteam();

        long ScanStart = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
        long ScanEnd = ScanStart + MainWindow.mw.gvp.Process.MainModule!.ModuleMemorySize;

        int ScanIndex = 0;
        
        Thread HigherPriorityScans = new Thread(async () =>
        {
            if (MainWindow.mw.gvp.Plat == "MS")
            {                                
                BaseAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, BaseAob, true, true)).FirstOrDefault() + 10656;
                BaseAddr = BaseAddrLong.ToString("X");
                Base2AddrLong = BaseAddrLong + 4512;
                Base2Addr = Base2AddrLong.ToString("X");
                Base3AddrLong = BaseAddrLong - 18496;
                Base3Addr = Base3AddrLong.ToString("X");
                Base4AddrLong = BaseAddrLong - 58296;
                Base4Addr = Base4AddrLong.ToString("X");
                WorldRGBAddrLong = BaseAddrLong - 422832;
                WorldRGBAddr = WorldRGBAddrLong.ToString("X");
            }
            else
            {
                BaseAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, BaseAob, true, true)).FirstOrDefault() - 501;
                BaseAddr = BaseAddrLong.ToString("X");
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CreditsHookAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CreditsASMAob, true, true)).FirstOrDefault();
            CreditsHookAddr = CreditsHookAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            XPaddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, XPAob, true, true)).FirstOrDefault();
            XPaddr = XPaddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            XPAmountaddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, XPAmountAob, true, true)).FirstOrDefault();
            XPAmountaddr = XPAmountaddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Car1AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Car1Aob, true, true)).FirstOrDefault() + 106;
            Car1Addr = Car1AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Car2AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Car2Aob, true, true)).FirstOrDefault() - 411;
            Car2Addr = Car2AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Wall1AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Wall1Aob, true, true)).FirstOrDefault() + 401;
            Wall1Addr = Wall1AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Wall2AddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, Wall2Aob, true, true)).FirstOrDefault() - 446;
            Wall2Addr = Wall2AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
        });

        Thread LowerPriorityScans = new Thread(async () =>
        {
            GlowingPaintAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, GlowingPaintSig, true, true)).FirstOrDefault();
            GlowingPaintAddr = GlowingPaintAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            UnbSkillHookLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, UnbreakableSkillComboSig, true, true)).FirstOrDefault() + 9;
            UnbSkillHookAddr = UnbSkillHookLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            FOVnopOutAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, FOVOutAob, true, true)).FirstOrDefault() + 123;
            FOVnopOutAddr = FOVnopOutAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            FOVnopInAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, FOVInAob, true, true)).FirstOrDefault() + 1383;
            FOVnopInAddr = FOVnopInAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            TimeNOPAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, TimeAob, true, true)).FirstOrDefault() + 1;
            TimeNOPAddr = TimeNOPAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CheckPointxASMAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CheckPointxASMAob, true, true)).FirstOrDefault();
            CheckPointxASMAddr = CheckPointxASMAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            WayPointxASMAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, WayPointxASMAob, true, true)).FirstOrDefault();
            WayPointxASMAddr = WayPointxASMAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CurrentIDAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CurrentIDAob, true, true)).FirstOrDefault() + 42;
            CurrentIDAddr = CurrentIDAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            OOBnopAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, OOBAob, true, true)).FirstOrDefault();
            OOBnopAddr = OOBnopAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            SuperCarAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, SuperCarAob, true, true)).FirstOrDefault();
            SuperCarAddr = SuperCarAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            DiscoverRoadsAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, DiscoverRoadsAob, true, true)).FirstOrDefault();
            DiscoverRoadsAddr = DiscoverRoadsAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            WaterAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, WaterAob, true, true)).FirstOrDefault() + 309;
            WaterAddr = WaterAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            AIXAobAddrLong = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, AIXAob, true, true)).FirstOrDefault() + 16;
            AIXAobAddr = AIXAobAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CameraSpeedBase = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraSpeedBaseAob, true, true)).FirstOrDefault();
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CameraBase = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraBaseAob, true, true)).FirstOrDefault();
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);       
            
            CameraShutterSpeed = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, CameraShutterSpeedAob, true, true)).FirstOrDefault();
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            AddressesFour();
        });
        
        Thread CodeCaves = new Thread(() =>
        {
            CCBA = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
            CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave == (IntPtr)0)
            {
                CCBA += 500000;
                CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            CCBA2 = CCBA;
            CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave2 == (IntPtr)0)
            {
                CCBA2 += 500000;
                CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            CCBA3 = CCBA2;
            CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave3 == (IntPtr)0)
            {
                CCBA3 += 500000;
                CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            CCBA4 = CCBA3;
            CodeCave4 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave4 == (IntPtr)0)
            {
                CCBA4 += 500000;
                CodeCave4 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            CCBA5 = CCBA4;
            CodeCave5 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave5 == (IntPtr)0)
            {
                CCBA5 += 500000;
                CodeCave5 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            CCBA6 = CCBA5;
            CodeCave6 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave6 == (IntPtr)0)
            {
                CCBA6 += 500000;
                CodeCave6 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA6, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            // This one is glowing paint I think
            CCBA9 = CCBA6;
            CodeCave9 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA9, 0x58, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave9 == (IntPtr)0)
            {
                CCBA9 += 500000;
                CodeCave9 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA9, 0x58, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            // Credits hook
            CCBA13 = CCBA9;
            CodeCave13 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA13, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave13 == (IntPtr)0)
            {
                CCBA13 += 500000;
                CodeCave13 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA13, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            // Unbreakable skill score
            CCBA14 = CCBA13;
            CodeCave14 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA14, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            while (CodeCave14 == (IntPtr)0)
            {
                CCBA14 += 500000;
                CodeCave14 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA14, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
            }
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
        });
        
        HigherPriorityScans.Priority = ThreadPriority.Highest;
        CodeCaves.Priority = ThreadPriority.AboveNormal;
        
        HigherPriorityScans.Start();
        LowerPriorityScans.Start();
        CodeCaves.Start();
        
        return Task.CompletedTask;
    }
    #endregion
}