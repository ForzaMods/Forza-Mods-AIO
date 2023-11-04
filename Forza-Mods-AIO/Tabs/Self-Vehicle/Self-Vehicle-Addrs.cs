using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

internal class Self_Vehicle_Addrs
{
    int ScanAmount = 37;
    
    #region Addresses - Longs

    public static long RotationAddrLong;
    public static long UnbSkillHookLong, UnbSkillBase;
    public static long BaseAddrHookLong;
    public static long TimeNOPAddrLong;
    public static long CheckPointxASMAddrLong;
    public static long WayPointxASMAddrLong; 
    public static long Car1AddrLong, Car2AddrLong, Wall1AddrLong, Wall2AddrLong;
    public static long OOBnopAddrLong;
    public static long SuperCarAddrLong;
    public static long SunRGBAddrLong;
    public static long DiscoverRoadsAddrLong;
    public static long WaterAddrLong;
    public static long AIXAobAddrLong;
    public static long XPaddrLong, XPAmountaddrLong, CreditsHookAddrLong;
    public static long GlowingPaintAddrLong;
    public static long BuildCapAddrASM1Long, BuildCapAddrASM2Long;
    #endregion

    #region Addresses - AOB's

    public static string UnbreakableSkillComboSig;
    public static string BaseAddrHookAob;
    public static string SunRGBAob;
    public static string Car1Aob, Car2Aob, Wall1Aob, Wall2Aob;
    public static string TimeAob;
    public static string CheckPointxASMAob, WayPointxASMAob;
    public static string XPAob, XPAmountAob, CreditsASMAob;
    public static string OOBAob;
    public static string SuperCarAob;
    public static string DiscoverRoadsAob;
    public static string WaterAob;
    public static string AIXAob;
    public static string CameraSpeedBaseAob, CameraBaseAob, CameraShutterSpeedAob, CameraNoClipSig;
    public static string GlowingPaintSig;
    public static string BuildCap1Sig, BuildCap2Sig;

    #endregion

    #region Addresses

    public static string RotationAddr;
    public static string UnbSkillHookAddr, WorldCollisionThreshold, CarCollisionThreshold, SmashableCollisionTolerance;
    public static string BaseAddrHook;
    public static string BuildCapAddrASM1, BuildCapAddrASM2;
    public static string Car1Addr, Car2Addr, Wall1Addr, Wall2Addr;
    public static string TimeNOPAddr, TimeAddr;
    public static string InRaceAddr, InPauseAddr, InHouseAddr;
    public static string CheckPointxASMAddr, WayPointxASMAddr;
    public static string TimeAddrAddr;
    public static string OOBnopAddr;
    public static string SuperCarAddr;
    public static string SunRedAddr, SunBlueAddr, SunGreenAddr;
    public static string DiscoverRoadsAddr;
    public static string WaterAddr;
    public static string AIXAobAddr;
    public static string XPaddr, XPAmountaddr, CreditsHookAddr;
    public static string GlowingPaintAddr;

    #endregion

    #region Addresses - FOV

    public static string ChaseMin, ChaseMax;
    public static string FarChaseMin, FarChaseMax;
    public static string DriverMin, DriverMax;
    public static string HoodMin, HoodMax;
    public static string BumperMin, BumperMax;

    public static long FovHookAddrLong;
    public static string FovHookAddr;

    #endregion

    #region Addresses - Stats

    private static long StatsScanStartAddr;
    private static long StatsScanEndAddr;

    public static List<string> Addresses = new();

    #endregion

    #region Offsets + AOB's

    private static void Aobs()
    {
        UnbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        CreditsASMAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        Car1Aob = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        Car2Aob = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        Wall1Aob = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        Wall2Aob = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        TimeAob = "20 F2 0F 11 43 08 48 83";
        CheckPointxASMAob = "0F 28 ? ? ? ? ? 0F 29 ? ? 0F 29 ? ? C3 90 48 8B ? 55";
        WayPointxASMAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50 C1 83 E0 07 3C 07";
        XPAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83 BD C0 00 00 00";
        XPAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        OOBAob = "0F 11 ? ? ? ? ? 0F 5C ? 0F 59 ? 0F 28 ? 0F C6 CA ? F3 0F";
        SuperCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2 ? 0F 11 ? ? 48 83 C1 ? E8 ? ? ? ? 0F 10";
        DiscoverRoadsAob = "00 96 42 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 40 1C 45 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 03 00";
        WaterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00";
        AIXAob = "48 89 ? ? ? 57 48 83 EC ? 0F 10 ? 48 8B ? 0F 11 ? ? 48 8B";
        GlowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        BuildCap1Sig = "F3 0F 11 B3 DC 03 00 00 E8";
        BuildCap2Sig = "F3 0F 11 43 30 FF 04 88 EB 61";
        CameraSpeedBaseAob = "cd ? 4c 3f cd ? 4c 3f 0a d7 13 40";
        CameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        CameraShutterSpeedAob = "C8 ? ? ? 7F ? 00 18 A4";
        GlowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        CameraNoClipSig = "74 ? 00 00 00 00 00 00 00 00 00 00 06 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 ? ? ? ? ? 00 70";
        SunRGBAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        BaseAddrHookAob = "F3 0F 10 81 90 01 00 00 C3";
    }

    private static void AobsFive()
    {
        UnbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        CreditsASMAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        BaseAddrHookAob = "48 63 ? 48 69 D0 ? ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? ? ? ? C3 C3 40";
        SunRGBAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        XPAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83";
        XPAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        Car1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 45 0F ? ? 4C 8B";
        Wall1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 90";
        Wall2Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 0F";
        TimeAob = "20 F2 0F 11 43 08 48 83";
        SuperCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2";
        WayPointxASMAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        OOBAob = "48 83 EC ? 0F 10 ? 41 0F ? ? 0F 10"; // + 83 OR + 0x53
        CheckPointxASMAob = "33 C0 48 89 ? 48 89 ? ? 48 E9 ? ? ? ? 90 40 F3";
        DiscoverRoadsAob = "00 96 ? ? ? ? 42 88";
        WaterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00 80 3F";
        AIXAob = "0F 11 41 50 0F 28 EB";
        CameraSpeedBaseAob = "54 00 52 ? 41 00 ? ? 4B 00 ? 00 00 00 00 00 05";
        CameraShutterSpeedAob = "C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? 00 00";
        CameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        CameraNoClipSig = "54 48 ? 5F 57 69 6E 56 ? ? ? ? 00 00 00 00 0A 00";
        BuildCap1Sig = "E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 0F 2E";
        BuildCap2Sig = "F3 0F ? ? ? 48 8B ? ? ? 48 8B ? ? ? 48 83 C4 ? 5F C3 85 FF";
        GlowingPaintSig = "0F 11 0A C6 42 F0 01";
    }

    #endregion

    #region FH5 Scan
    public void FH5_Scan()
    {
        AobsFive();
        
        ScanAmount = 27;

        var ScanIndex = 0;
        var Finished = 0;
        
        Task.Run(() =>
        {
            try
            {
                XPaddrLong = (long)MainWindow.mw.m.ScanForSig(XPAob).FirstOrDefault();
                XPaddr = XPaddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                XPAmountaddrLong = (long)MainWindow.mw.m.ScanForSig(XPAmountAob).FirstOrDefault();
                XPAmountaddr = XPAmountaddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                SunRGBAddrLong = (long)MainWindow.mw.m.ScanForSig(SunRGBAob).LastOrDefault();
                SunRedAddr = SunRGBAddrLong.ToString("X");
                SunBlueAddr = (SunRGBAddrLong + 4).ToString("X");
                SunGreenAddr = (SunRGBAddrLong + 8).ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Car1AddrLong = (long)MainWindow.mw.m.ScanForSig(Car1Aob).FirstOrDefault();
                Car1Addr = Car1AddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Wall1AddrLong = (long)MainWindow.mw.m.ScanForSig(Wall1Aob).FirstOrDefault();
                Wall1Addr = Wall1AddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                Wall2AddrLong = (long)MainWindow.mw.m.ScanForSig(Wall2Aob).FirstOrDefault();
                Wall2Addr = Wall2AddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                SuperCarAddrLong = (long)MainWindow.mw.m.ScanForSig(SuperCarAob).LastOrDefault();
                SuperCarAddr = SuperCarAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                WaterAddrLong = (long)MainWindow.mw.m.ScanForSig(WaterAob).FirstOrDefault() + 309;
                WaterAddr = WaterAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                CreditsHookAddrLong = (long)MainWindow.mw.m.ScanForSig(CreditsASMAob).FirstOrDefault();
                CreditsHookAddr = CreditsHookAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                RotationAddrLong = (long)MainWindow.mw.m.ScanForSig("F3 44 0F 10 89 ? ? 00 00 F3 44 0F 10 B9").LastOrDefault();
                RotationAddr = RotationAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                BaseAddrHookLong = (long)MainWindow.mw.m.ScanForSig(BaseAddrHookAob).FirstOrDefault() - 279;
                BaseAddrHook = BaseAddrHookLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                Finished++;
            }
            catch
            {
            }
        });

        Task.Run(() =>
        {
            try
            {
                UnbSkillHookLong = (long)MainWindow.mw.m.ScanForSig(UnbreakableSkillComboSig).FirstOrDefault() + 9;
                UnbSkillHookAddr = UnbSkillHookLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                GlowingPaintAddrLong = (long)MainWindow.mw.m.ScanForSig(GlowingPaintSig).FirstOrDefault();
                GlowingPaintAddr = GlowingPaintAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                FovHookAddrLong = (long)MainWindow.mw.m.ScanForSig("0F 10 ? B0 ? 0F 28 ? ? ? F3 0F").FirstOrDefault();
                FovHookAddr = FovHookAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                TimeNOPAddrLong = (long)MainWindow.mw.m.ScanForSig(TimeAob).FirstOrDefault() + 1;
                TimeNOPAddr = TimeNOPAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                WayPointxASMAddrLong = (long)MainWindow.mw.m.ScanForSig(WayPointxASMAob).FirstOrDefault();
                WayPointxASMAddr = WayPointxASMAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                DiscoverRoadsAddrLong = (long)MainWindow.mw.m.ScanForSig(DiscoverRoadsAob).FirstOrDefault();
                DiscoverRoadsAddr = DiscoverRoadsAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                BuildCapAddrASM1Long = (long)MainWindow.mw.m.ScanForSig(BuildCap1Sig).FirstOrDefault() + 25;
                BuildCapAddrASM1 = BuildCapAddrASM1Long.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                BuildCapAddrASM2Long = (long)MainWindow.mw.m.ScanForSig(BuildCap2Sig).FirstOrDefault();
                BuildCapAddrASM2 = BuildCapAddrASM2Long.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                
                CheckPointxASMAddrLong = (long)MainWindow.mw.m.ScanForSig("0F 10 89 60 02 00 00 0F 29").FirstOrDefault();
                CheckPointxASMAddr = CheckPointxASMAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                AIXAobAddrLong = (long)MainWindow.mw.m.ScanForSig(AIXAob).FirstOrDefault();
                AIXAobAddr = AIXAobAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                OOBnopAddrLong = (long)MainWindow.mw.m.ScanForSig(OOBAob).FirstOrDefault() + 83;
                OOBnopAddr = OOBnopAddrLong.ToString("X");
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                PhotoCamEntity._Speed = MainWindow.mw.m.ScanForSig(CameraSpeedBaseAob).FirstOrDefault();
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                PhotoCamEntity._MainPhotoCamEntity = MainWindow.mw.m.ScanForSig(CameraBaseAob).FirstOrDefault() - 53;
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                PhotoCamEntity._Speed = MainWindow.mw.m.ScanForSig(CameraShutterSpeedAob).FirstOrDefault();
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

                PhotoCamEntity._NoClip = MainWindow.mw.m.ScanForSig(CameraNoClipSig).FirstOrDefault() + 1656;
                ScanIndex++;
                UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
                Finished++;
            }
            catch
            {
            }
        });

        Task.Run(() =>
        {
            FovLimitersScan();
            ++ScanIndex;
            ++Finished;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
        });
        
        while (Finished != 3)
        {
            Thread.Sleep(1000);
        }

        Task.Run(() => Self_Vehicle_ASM.GetBaseAddress());
        Overlay.Overlay.SelfVehicleOption.IsEnabled = true;
        UpdateUi.UpdateUI(true, Self_Vehicle.sv);
    }
    #endregion

    #region FH4 Scan
    public void FH4_Scan()
    {
        ScanAmount = 27;
        
        Aobs();

        var ScanIndex = 0;
        var Finished = 0;
        
        Task.Run(() =>
        {
            SunRGBAddrLong = (long)MainWindow.mw.m.ScanForSig(SunRGBAob).LastOrDefault();
            SunRedAddr = SunRGBAddrLong.ToString("X");
            SunBlueAddr = (SunRGBAddrLong + 4).ToString("X");
            SunGreenAddr = (SunRGBAddrLong + 8).ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CreditsHookAddrLong = (long)MainWindow.mw.m.ScanForSig(CreditsASMAob).FirstOrDefault();
            CreditsHookAddr = CreditsHookAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            XPaddrLong = (long)MainWindow.mw.m.ScanForSig(XPAob).FirstOrDefault();
            XPaddr = XPaddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            XPAmountaddrLong = (long)MainWindow.mw.m.ScanForSig(XPAmountAob).FirstOrDefault();
            XPAmountaddr = XPAmountaddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Car1AddrLong = (long)MainWindow.mw.m.ScanForSig(Car1Aob).FirstOrDefault() + 106;
            Car1Addr = Car1AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Car2AddrLong = (long)MainWindow.mw.m.ScanForSig(Car2Aob).FirstOrDefault() - 411;
            Car2Addr = Car2AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Wall1AddrLong = (long)MainWindow.mw.m.ScanForSig(Wall1Aob).FirstOrDefault() + 401;
            Wall1Addr = Wall1AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Wall2AddrLong = (long)MainWindow.mw.m.ScanForSig(Wall2Aob).FirstOrDefault() - 446;
            Wall2Addr = Wall2AddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            RotationAddrLong = (long)MainWindow.mw.m.ScanForSig("F3 44 0F 10 89 ? ? 00 00 F3 44 0F 10 B9").LastOrDefault();
            RotationAddr = RotationAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            BaseAddrHookLong = (long)MainWindow.mw.m.ScanForSig(BaseAddrHookAob).FirstOrDefault();
            BaseAddrHook = BaseAddrHookLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            Finished++;
        });

        Task.Run(() =>
        {
            GlowingPaintAddrLong = (long)MainWindow.mw.m.ScanForSig(GlowingPaintSig).FirstOrDefault();
            GlowingPaintAddr = GlowingPaintAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            UnbSkillHookLong = (long)MainWindow.mw.m.ScanForSig(UnbreakableSkillComboSig).FirstOrDefault() + 9;
            UnbSkillHookAddr = UnbSkillHookLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            TimeNOPAddrLong = (long)MainWindow.mw.m.ScanForSig(TimeAob).FirstOrDefault() + 1;
            TimeNOPAddr = TimeNOPAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            CheckPointxASMAddrLong = (long)MainWindow.mw.m.ScanForSig(CheckPointxASMAob).FirstOrDefault();
            CheckPointxASMAddr = CheckPointxASMAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            WayPointxASMAddrLong = (long)MainWindow.mw.m.ScanForSig(WayPointxASMAob).FirstOrDefault();
            WayPointxASMAddr = WayPointxASMAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            OOBnopAddrLong = (long)MainWindow.mw.m.ScanForSig(OOBAob).FirstOrDefault();
            OOBnopAddr = OOBnopAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            SuperCarAddrLong = (long)MainWindow.mw.m.ScanForSig(SuperCarAob).FirstOrDefault();
            SuperCarAddr = SuperCarAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            DiscoverRoadsAddrLong = (long)MainWindow.mw.m.ScanForSig(DiscoverRoadsAob).FirstOrDefault();
            DiscoverRoadsAddr = DiscoverRoadsAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            WaterAddrLong = (long)MainWindow.mw.m.ScanForSig(WaterAob).FirstOrDefault() + 309;
            WaterAddr = WaterAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            AIXAobAddrLong = (long)MainWindow.mw.m.ScanForSig(AIXAob).FirstOrDefault() + 16;
            AIXAobAddr = AIXAobAddrLong.ToString("X");
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            PhotoCamEntity._Speed = MainWindow.mw.m.ScanForSig(CameraSpeedBaseAob).FirstOrDefault();
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
            
            PhotoCamEntity._MainPhotoCamEntity = MainWindow.mw.m.ScanForSig(CameraBaseAob).FirstOrDefault() - 665;
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar); 
            
            PhotoCamEntity._ShutterSpeed = MainWindow.mw.m.ScanForSig(CameraShutterSpeedAob).FirstOrDefault() - 0x31;
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
         
            PhotoCamEntity._NoClip = MainWindow.mw.m.ScanForSig(CameraNoClipSig).FirstOrDefault() + 1532;
            ScanIndex++;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);

            FovPage._fovPage.Dispatcher.Invoke(() =>
            {
                FovPage._fovPage.FovSwitch.IsEnabled = false;
                FovPage._fovPage.FovNum.IsEnabled = false;
                FovPage._fovPage.FovSlider.IsEnabled = false;
            });
            
            Overlay.SelfCarMenu.FovMenu.FovMenu.FovLock.IsEnabled = false;
            Finished++;
        });
        
        Task.Run(() =>
        {
            FovLimitersScan();
            ++ScanIndex;
            ++Finished;
            UpdateUi.AddProgress(ScanAmount, ScanIndex, Self_Vehicle.sv.AOBProgressBar);
        });

        while (Finished != 3)
        {
            Thread.Sleep(1000);
        }

        UpdateUi.UpdateUI(true, Self_Vehicle.sv);
        Overlay.Overlay.SelfVehicleOption.IsEnabled = true;
        Task.Run(() => Self_Vehicle_ASM.GetBaseAddress());
    }
    #endregion

    private void FovLimitersScan()
    {
        var bases1 = MainWindow.mw.m.ScanForSig("90 40 CD CC 8C 40 1F 85 2B 3F 00 00 00 40").ToList();
        var bases2 = MainWindow.mw.m.ScanForSig("CD CC 4C 3E 00 50 43 47 00 00 34 42 00 00 20").ToList();
        var base3 = MainWindow.mw.m.ScanForSig("CD ? 4C 3E ? ? ? 47 00 ? 34 ? 00 00 20 42 ? 00 A0").FirstOrDefault() - 0x20;
        
        ChaseMin = (bases1.FirstOrDefault() - 10).ToString("X");
        ChaseMax = ((bases1.FirstOrDefault() - 10) + 4).ToString("X");
        FarChaseMin = (bases1.LastOrDefault() - 10).ToString("X");
        FarChaseMax = ((bases1.LastOrDefault() - 10) + 4).ToString("X");
        DriverMin = (base3 - 4).ToString("X");
        DriverMax = base3.ToString("X");
        BumperMin = ((bases2.FirstOrDefault() - 0x20) - 4).ToString("X");
        BumperMax = (bases2.FirstOrDefault() - 0x20).ToString("X");
        HoodMin = ((bases2.LastOrDefault() - 0x20) - 4).ToString("X");
        HoodMax = (bases2.LastOrDefault() - 0x20).ToString("X");
        FovPage._fovPage!.Dispatcher.Invoke(() => { FovPage._fovPage.UpdateValues(); });
    }
}