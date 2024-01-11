using System;
using System.Linq;
using System.Threading.Tasks;
using Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.FovMenu;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicle;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

internal class SelfVehicleAddresses
{
    #region Addresses

    public static UIntPtr GravityProtectAddr;
    public static UIntPtr AccelProtectAddr;
    public static UIntPtr SkillPointsAddr;
    public static UIntPtr SpinsAddr;
    public static UIntPtr BackfireTimeAddr, BackfireTypeAddr;
    public static UIntPtr HeadlightAddr;
    public static UIntPtr TimeScaleAddr;
    public static UIntPtr DriftScoreAddr;
    public static UIntPtr SkillScoreAddr;
    public static UIntPtr CleanlinessAddr;
    public static UIntPtr SkillTreeAddr, SkillCostAddr;
    public static UIntPtr ScaleAddr, SellFactorAddr;
    public static UIntPtr UnbSkillHook;
    public static UIntPtr WorldCollisionThreshold, CarCollisionThreshold, SmashAbleCollisionTolerance;
    public static UIntPtr BaseAddrHook;
    public static UIntPtr TimeNopAddr, TimeAddr;
    public static UIntPtr WayPointXAsmAddr;
    public static UIntPtr Car1Addr, Car2Addr, Wall1Addr, Wall2Addr;
    public static UIntPtr SuperCarAddr;
    public static UIntPtr DiscoverRoadsAddr;
    public static UIntPtr WaterAddr;
    public static UIntPtr AiXAddr;
    public static UIntPtr XpAddr, XpAmountAddr, CreditsHookAddr, CreditsCompareAddr;
    public static UIntPtr GlowingPaintAddr;
    public static UIntPtr BuildAddr1, BuildAddr2;
    public static UIntPtr SeasonalAddr, SeriesAddr;
    public static UIntPtr FovHookAddr;
    public static UIntPtr SunRedAddr, SunBlueAddr, SunGreenAddr;
    public static UIntPtr ChaseMin, ChaseMax;
    public static UIntPtr FarChaseMin, FarChaseMax;
    public static UIntPtr DriverMin, DriverMax;
    public static UIntPtr HoodMin, HoodMax;
    public static UIntPtr BumperMin, BumperMax;

    #endregion

    public static void Scan()
    {
        switch (Mw.Gvp.Type)
        {
            case GameVerPlat.GameType.Fh5:
            {
                Task.Run(FH5_Scan);
                break;
            }
            case GameVerPlat.GameType.Fh4:
            {
                Task.Run(FH4_Scan);
                break;
            }
            case GameVerPlat.GameType.Fm8:
            {
                Task.Run(FM8_Scan);
                break;
            }
            case GameVerPlat.GameType.None:
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void FM8_Scan()
    {
        if (!Mw.Attached || Mw.Gvp.Type != GameVerPlat.GameType.Fm8)
        {
            return;
        }

        Sv.UiManager.Index = 0;
        Sv.UiManager.ScanAmount = 6;

        const string massProtectSig = "74 ? F3 0F ? ? 0F 29";
        var massProtectAddresses = Mw.M.ScanForSig(massProtectSig);
        var protectAddresses = massProtectAddresses as UIntPtr[] ?? massProtectAddresses.ToArray();
        GravityProtectAddr = protectAddresses.FirstOrDefault() + 19;
        AccelProtectAddr = protectAddresses.FirstOrDefault() + 35;
        Sv.UiManager.AddProgress();
        
        const string baseAddrSig = "0F 2F ? ? ? ? ? 72 ? 0F 2F ? ? ? ? ? 72 ? 33 DB";
        BaseAddrHook = Mw.M.ScanForSig(baseAddrSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string wall1Sig = "0F 84 ? ? ? ? 85 DB 0F 84 ? ? ? ? 4C 8B";
        Wall1Addr = Mw.M.ScanForSig(wall1Sig).FirstOrDefault() + 8;
        Sv.UiManager.AddProgress();

        const string wall2Sig = "45 0F ? ? 44 8B ? 66 0F";
        Wall2Addr = Mw.M.ScanForSig(wall2Sig).FirstOrDefault() - 40;
        Sv.UiManager.AddProgress();

        const string car1Sig = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 45 0F ? ? 4C 8B";
        Car1Addr = Mw.M.ScanForSig(car1Sig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        const string cameraBaseSig = "48 ? 6E 64 62 ? ? 6B 65 00 ? 00 00 00 00 00 09";
        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(cameraBaseSig).FirstOrDefault() + 264;
        Sv.UiManager.AddProgress();
        
        Sv.Dispatcher.Invoke(() =>
        {
            Sv.HandlingButton.IsEnabled = true;
            Sv.PhotomodeButton.IsEnabled = true;
            Shp.WaterDragSwitch.IsEnabled = false;
            Shp.SuperCarSwitch.IsEnabled = false;
        });
    }

    #region FH5 Scan

    private static void FH5_Scan()
    {
        if (!Mw.Attached || Mw.Gvp.Type != GameVerPlat.GameType.Fh5)
        {
            return;
        }

        Sv.UiManager.Index = 0;
        Sv.UiManager.ScanAmount = 39;

        const string xpAob = "41 83 BF 88 00 00 00";
        XpAddr = Mw.M.ScanForSig(xpAob).FirstOrDefault() - 14;
        Sv.UiManager.AddProgress();

        const string xpAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        XpAmountAddr = Mw.M.ScanForSig(xpAmountAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string sunRgbAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        var sunRgbAddr = Mw.M.ScanForSig(sunRgbAob).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        Sv.UiManager.AddProgress();

        const string car1Aob = "4C 8B ? ? ? ? ? ? 49 83 C0 ? 66 44";
        Car1Addr = Mw.M.ScanForSig(car1Aob).FirstOrDefault() - 18;
        Sv.UiManager.AddProgress();

        const string wall1Aob = "45 0F ? ? 4D 8B ? 90";
        Wall1Addr = Mw.M.ScanForSig(wall1Aob).FirstOrDefault() - 40;
        Sv.UiManager.AddProgress();

        const string wall2Aob = "45 0F ? ? 4D 8B ? 0F 1F";
        Wall2Addr = Mw.M.ScanForSig(wall2Aob).FirstOrDefault() - 40;
        Sv.UiManager.AddProgress();

        const string waterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00 80";
        WaterAddr = Mw.M.ScanForSig(waterAob).FirstOrDefault() + 553;
        Sv.UiManager.AddProgress();

        const string creditsSig = "5F 48 FF ? 48 8B ? ? E8";
        CreditsHookAddr = Mw.M.ScanForSig(creditsSig).FirstOrDefault() + 13;
        Sv.UiManager.AddProgress();

        const string baseAddrSig = "0F 2F ? ? ? ? ? 72 ? 0F 2F ? ? ? ? ? 72 ? 0F 2F";
        BaseAddrHook = Mw.M.ScanForSig(baseAddrSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string unbSkillSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        UnbSkillHook = Mw.M.ScanForSig(unbSkillSig).FirstOrDefault() + 13;
        Sv.UiManager.AddProgress();

        const string glowingPaintSig = "0F 11 0A C6 42 F0 01";
        GlowingPaintAddr = Mw.M.ScanForSig(glowingPaintSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string fovHookSig = "0F 10 ? B0 ? 0F 28 ? ? ? F3 0F";
        FovHookAddr = Mw.M.ScanForSig(fovHookSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string timeSig = "20 F2 0F 11 43 08 48 83";
        TimeNopAddr = Mw.M.ScanForSig(timeSig).FirstOrDefault() + 1;
        Sv.UiManager.AddProgress();

        const string waypointSig = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        WayPointXAsmAddr = Mw.M.ScanForSig(waypointSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string build1Sig = "E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 0F 2E";
        BuildAddr1 = Mw.M.ScanForSig(build1Sig).FirstOrDefault() + 25;
        Sv.UiManager.AddProgress();

        const string build2Sig = "5F C3 85 FF 48 8B";
        BuildAddr2 = Mw.M.ScanForSig(build2Sig).FirstOrDefault() - 19;
        Sv.UiManager.AddProgress();

        const string aiXSig = "0F 11 ? ? 48 8B ? 0F 10 ? ? 0F 11 ? ? ? ? ? 0F 10";
        AiXAddr = Mw.M.ScanForSig(aiXSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string speedBaseSig = "54 00 52 ? 41 00 ? ? 4B 00 ? 00 00 00 00 00 05";
        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(speedBaseSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string cameraBaseSig = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(cameraBaseSig).FirstOrDefault() - 53;
        Sv.UiManager.AddProgress();

        const string shutterSpeedSig = "C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? 00 00";
        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(shutterSpeedSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string cameraNoClipSig = "54 48 ? 5F 57 69 6E 56 ? ? ? ? 00 00 00 00 0A 00";
        PhotoCamEntity.NoClipBase = Mw.M.ScanForSig(cameraNoClipSig).FirstOrDefault() + 1656;
        Sv.UiManager.AddProgress();

        const string seasonalSig = "74 ? F3 0F ? ? ? 44 8B";
        SeasonalAddr = Mw.M.ScanForSig(seasonalSig).FirstOrDefault() + 2;
        Sv.UiManager.AddProgress();

        const string seriesSig = "48 89 ? ? ? 48 89 ? ? ? 4C 8B ? 44 89 ? ? ? 48 BB";
        SeriesAddr = Mw.M.ScanForSig(seriesSig).FirstOrDefault() - 23;
        Sv.UiManager.AddProgress();

        const string scaleSig = "48 8B ? ? E8 ? ? ? ? 90 48 85 ? 74 ? 8B C5";
        ScaleAddr = Mw.M.ScanForSig(scaleSig).FirstOrDefault() - 7;
        Sv.UiManager.AddProgress();

        const string sellFactorSig = "44 8B ? ? ? ? ? 33 D2 48 8B ? ? ? ? ? E8 ? ? ? ? 90";
        SellFactorAddr = Mw.M.ScanForSig(sellFactorSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string skillTreeSig = "F3 0F ? ? ? 33 D2 48 8B ? ? E8 ? ? ? ? 0F 5A";
        SkillTreeAddr = Mw.M.ScanForSig(skillTreeSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        FovLimitersScan();
        Sv.UiManager.AddProgress();

        const string cleanlinessSig = "F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? B9 ? ? ? ? E8";
        CleanlinessAddr = Mw.M.ScanForSig(cleanlinessSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string skillScoreSig = "48 8B ? ? E8 ? ? ? ? 8B 78";
        SkillScoreAddr = Mw.M.ScanForSig(skillScoreSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();

        const string skillCostSig = "48 89 5C 24 08 57 48 83 EC 20 48 8B 79 18 33 D2 48 8B 4F 30";
        SkillCostAddr = Mw.M.ScanForSig(skillCostSig).FirstOrDefault() + 39;
        Sv.UiManager.AddProgress();

        const string driftScoreSig = "48 8D ? ? ? ? ? E8 ? ? ? ? F3 0F ? ? ? ? 45 33";
        DriftScoreAddr = Mw.M.ScanForSig(driftScoreSig).FirstOrDefault() - 62;
        Sv.UiManager.AddProgress();

        const string timeScaleSig = "74 ? 48 8B ? 48 8B ? FF 90 ? ? ? ? F3 0F ? ? ? ? ? ? F3 0F";
        TimeScaleAddr = Mw.M.ScanForSig(timeScaleSig).FirstOrDefault() + 22;
        Sv.UiManager.AddProgress();

        const string headlightSig = "0F 10 ? ? F3 44 ? ? ? ? ? ? ? 83 7B 48";
        HeadlightAddr = Mw.M.ScanForSig(headlightSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string creditsCompareSig = "48 89 ? ? ? 57 48 83 EC ? 48 8D ? ? E8 ? ? ? ? 48 8B";
        CreditsCompareAddr = Mw.M.ScanForSig(creditsCompareSig).FirstOrDefault() - 140;
        Sv.UiManager.AddProgress();

        const string backfireSig = "48 8B ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? E8 ? ? ? ? 0F 28";
        BackfireTimeAddr = Mw.M.ScanForSig(backfireSig).FirstOrDefault() + 4;
        BackfireTypeAddr = BackfireTimeAddr + 125;
        Sv.UiManager.AddProgress();

        const string spinsSig = "48 89 5C 24 08 57 48 83 EC 20 48 8B FA 33 D2 48 8B 4F 10";
        SpinsAddr = Mw.M.ScanForSig(spinsSig).FirstOrDefault() + 28;
        Sv.UiManager.AddProgress();

        const string skillPointSig = "0F 4F ? 48 8B ? ? E8 ? ? ? ? 48 8B";
        SkillPointsAddr = Mw.M.ScanForSig(skillPointSig).FirstOrDefault() - 6;
        Sv.UiManager.AddProgress();

        SelfVehicleOption.IsEnabled = true;
        Sv.UiManager.ToggleUiElements(true);
        Sv.Dispatcher.BeginInvoke((Action)delegate
        {
            Sv.StatsButton.IsEnabled = false;
            Shp.SuperCarSwitch.IsEnabled = false;
            UnlocksPage.Unlocks.DiscoverRoadsSwitch.IsEnabled = false;
        });
    }

    #endregion

    #region FH4 Scan

    private static void FH4_Scan()
    {
        if (!Mw.Attached || Mw.Gvp.Type != GameVerPlat.GameType.Fh4)
        {
            return;
        }

        Sv.UiManager.Index = 0;
        Sv.UiManager.ScanAmount = 29;

        const string sunRgbSig = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        var sunRgbAddr = Mw.M.ScanForSig(sunRgbSig).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        Sv.UiManager.AddProgress();

        const string creditsHookSig = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        CreditsHookAddr = Mw.M.ScanForSig(creditsHookSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string xpSig = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83 BD C0 00 00 00";
        XpAddr = Mw.M.ScanForSig(xpSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string xpAmountSig = "8B 89 ? ? ? ? 85 C9 0F 8E";
        XpAmountAddr = Mw.M.ScanForSig(xpAmountSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string car1Sig = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        Car1Addr = Mw.M.ScanForSig(car1Sig).FirstOrDefault() + 106;
        Sv.UiManager.AddProgress();

        const string car2Sig = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        Car2Addr = Mw.M.ScanForSig(car2Sig).FirstOrDefault() - 411;
        Sv.UiManager.AddProgress();

        const string wall1Sig = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        Wall1Addr = Mw.M.ScanForSig(wall1Sig).FirstOrDefault() + 401;
        Sv.UiManager.AddProgress();

        const string wall2Sig =
            "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        Wall2Addr = Mw.M.ScanForSig(wall2Sig).FirstOrDefault() - 446;
        Sv.UiManager.AddProgress();

        const string baseHookSig = "F3 0F 10 81 90 01 00 00 C3";
        BaseAddrHook = Mw.M.ScanForSig(baseHookSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string glowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        GlowingPaintAddr = Mw.M.ScanForSig(glowingPaintSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string unbSkillHookSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        UnbSkillHook = Mw.M.ScanForSig(unbSkillHookSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();

        const string timeSig = "20 F2 0F 11 43 08 48 83";
        TimeNopAddr = Mw.M.ScanForSig(timeSig).FirstOrDefault() + 1;
        Sv.UiManager.AddProgress();

        const string waypointSig = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50 C1 83 E0 07 3C 07";
        WayPointXAsmAddr = Mw.M.ScanForSig(waypointSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string superCarSig =
            "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2 ? 0F 11 ? ? 48 83 C1 ? E8 ? ? ? ? 0F 10";
        SuperCarAddr = Mw.M.ScanForSig(superCarSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string discoverRoadsSig =
            "00 96 42 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 40 1C 45 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 03 00";
        DiscoverRoadsAddr = Mw.M.ScanForSig(discoverRoadsSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string waterSig = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00";
        WaterAddr = Mw.M.ScanForSig(waterSig).FirstOrDefault() + 269;
        Sv.UiManager.AddProgress();

        const string aiXSig = "48 89 ? ? ? 57 48 83 EC ? 0F 10 ? 48 8B ? 0F 11 ? ? 48 8B";
        AiXAddr = Mw.M.ScanForSig(aiXSig).FirstOrDefault() + 16;
        Sv.UiManager.AddProgress();

        const string speedBaseSig = "cd ? 4c 3f cd ? 4c 3f 0a d7 13 40";
        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(speedBaseSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string cameraBaseSig = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(cameraBaseSig).FirstOrDefault() - 665;
        Sv.UiManager.AddProgress();

        const string shutterSpeedSig = "C8 ? ? ? 7F ? 00 18 A4";
        PhotoCamEntity.ShutterSpeedBase = Mw.M.ScanForSig(shutterSpeedSig).FirstOrDefault() - 0x31;
        Sv.UiManager.AddProgress();

        const string cameraNoClipSig =
            "74 ? 00 00 00 00 00 00 00 00 00 00 06 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 ? ? ? ? ? 00 70";
        PhotoCamEntity.NoClipBase = Mw.M.ScanForSig(cameraNoClipSig).FirstOrDefault() + 1532;
        Sv.UiManager.AddProgress();

        FovMenu.FovLock.IsEnabled = false;

        FovLimitersScan();
        Sv.UiManager.AddProgress();

        const string scaleSig = "0F 5B ? F3 0F ? ? ? F3 0F ? ? 48 85";
        ScaleAddr = Mw.M.ScanForSig(scaleSig).FirstOrDefault() + 3;
        Sv.UiManager.AddProgress();

        const string sellFactorSig = "48 8B ? ? E8 ? ? ? ? 8B B8";
        SellFactorAddr = Mw.M.ScanForSig(sellFactorSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();

        const string cleanlinessSig = "F3 0F 10 88 80 8C 00 00";
        CleanlinessAddr = Mw.M.ScanForSig(cleanlinessSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string skillScoreSig = "FF 50 ? 8B 78 ? 48 85";
        SkillScoreAddr = Mw.M.ScanForSig(skillScoreSig).FirstOrDefault() + 3;
        Sv.UiManager.AddProgress();

        const string build1Sig = "F3 0F 11 B3 DC 03 00 00 E8";
        BuildAddr1 = Mw.M.ScanForSig(build1Sig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        const string build2Sig = "F3 0F 11 43 30 FF 04 88 EB 61";
        BuildAddr2 = Mw.M.ScanForSig(build2Sig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        SelfVehicleOption.IsEnabled = true;
        Sv.UiManager.ToggleUiElements(true);

        Sv.Dispatcher.BeginInvoke((Action)delegate
        {
            Sv.BackFireButton.IsEnabled = false;
            Sv.StatsButton.IsEnabled = false;
        });
    }

    #endregion

    private static void FovLimitersScan()
    {
        const string base1Sig = "90 40 CD CC 8C 40 1F 85 2B 3F 00 00 00 40";
        const string base2Sig = "CD CC 4C 3E 00 50 43 47 00 00 34 42 00 00 20";
        const string base3Sig = "CD ? 4C 3E ? ? ? 47 00 ? 34 ? 00 00 20 42 ? 00 A0";
        var bases1 = Mw.M.ScanForSig(base1Sig).ToList();
        var bases2 = Mw.M.ScanForSig(base2Sig).ToList();
        var base3 = Mw.M.ScanForSig(base3Sig).FirstOrDefault() - 0x20;

        ChaseMin = bases1.FirstOrDefault() - 10;
        ChaseMax = bases1.FirstOrDefault() - 10 + 4;
        FarChaseMin = bases1.LastOrDefault() - 10;
        FarChaseMax = bases1.LastOrDefault() - 10 + 4;
        DriverMin = base3 - 4;
        DriverMax = base3;
        BumperMin = bases2.FirstOrDefault() - 0x20 - 4;
        BumperMax = bases2.FirstOrDefault() - 0x20;
        HoodMin = bases2.LastOrDefault() - 0x20 - 4;
        HoodMax = bases2.LastOrDefault() - 0x20;
        FovPage.Fov.Dispatcher.Invoke(() => FovPage.Fov.UpdateValues());
    }
}