using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.FovMenu;
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

    public static UIntPtr SkillPointsAddr;
    public static UIntPtr SpinsAddr;
    public static UIntPtr StatsEditorAobHook;
    public static UIntPtr BackfireTimeAddr, BackfireTypeAddr;
    public static UIntPtr HeadlightAddr;
    public static UIntPtr TimeScaleAddr;
    public static UIntPtr DriftScoreAddr;
    public static UIntPtr SkillScoreAddr;
    public static UIntPtr CleanlinessAddr;
    public static UIntPtr SkillTreeAddr, SkillCostAddr;
    public static UIntPtr ScaleAddr, SellFactorAddr;
    public static UIntPtr FlyhackHookAddr;
    public static UIntPtr UnbSkillHook;
    public static UIntPtr WorldCollisionThreshold, CarCollisionThreshold, SmashableCollisionTolerance;
    public static UIntPtr BaseAddrHook;
    public static UIntPtr TimeNopAddr, TimeAddr;
    public static UIntPtr WayPointXAsmAddr;
    public static UIntPtr Car1Addr, Car2Addr, Wall1Addr, Wall2Addr;
    public static UIntPtr OobNopAddr;
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

    #region Signatures

    private static string? _driftScoreSig;
    private static string? _unbreakableSkillComboSig;
    private static string? _baseAddrHookAob;
    private static string? _sunRgbAob;
    private static string? _car1Aob, _car2Aob, _wall1Aob, _wall2Aob;
    private static string? _timeAob;
    private static string? _wayPointXAsmAob;
    private static string? _xpAob, _xpAmountAob, _creditsAsmAob;
    private static string? _oobAob;
    private static string? _superCarAob;
    private static string? _discoverRoadsAob;
    private static string? _waterAob;
    private static string? _aixAob;
    private static string? _cameraSpeedBaseAob, _cameraBaseAob, _cameraShutterSpeedAob, _cameraNoClipSig;
    private static string? _glowingPaintSig;
    private static string? _buildCap1Sig, _buildCap2Sig;
    private static string? _seasonalSig, _seriesSig;
    private static string? _scaleSig, _sellFactorSig;
    private static string? _skillTreeSig;
    private static string? _cleanlinessSig;
    private static string? _skillScoreSig;
    private static string? _skillCostSig;
    private static string? _timeScaleSig;
    private static string? _headlightSig;
    private static string? _creditsCompareSig;
    private static string? _backfireSig;
    private static string? _flyhackSig;
    private static string? _statsEditorSig;
    private static string? _spinsSig;
    private static string? _skillPointsSig;

    #endregion

    #region Addresses - Stats

    private static long _statsScanStartAddr;
    private static long _statsScanEndAddr;

    public static List<string> Addresses = new();

    #endregion

    #region Offsets + AOB's

    private static void Signatures()
    {
        _unbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        _creditsAsmAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        _car1Aob = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        _car2Aob = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        _wall1Aob = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        _wall2Aob = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        _timeAob = "20 F2 0F 11 43 08 48 83";
        _wayPointXAsmAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50 C1 83 E0 07 3C 07";
        _xpAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83 BD C0 00 00 00";
        _xpAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        _oobAob = "0F 11 ? ? ? ? ? 0F 5C ? 0F 59 ? 0F 28 ? 0F C6 CA ? F3 0F";
        _superCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2 ? 0F 11 ? ? 48 83 C1 ? E8 ? ? ? ? 0F 10";
        _discoverRoadsAob = "00 96 42 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 40 1C 45 ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? 03 00";
        _waterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00";
        _aixAob = "48 89 ? ? ? 57 48 83 EC ? 0F 10 ? 48 8B ? 0F 11 ? ? 48 8B";
        _glowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        _buildCap1Sig = "F3 0F 11 B3 DC 03 00 00 E8";
        _buildCap2Sig = "F3 0F 11 43 30 FF 04 88 EB 61";
        _cameraSpeedBaseAob = "cd ? 4c 3f cd ? 4c 3f 0a d7 13 40";
        _cameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        _cameraShutterSpeedAob = "C8 ? ? ? 7F ? 00 18 A4";
        _glowingPaintSig = "41 0f 11 4a ? 41 c6 02";
        _cameraNoClipSig = "74 ? 00 00 00 00 00 00 00 00 00 00 06 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 ? ? ? ? ? 00 70";
        _sunRgbAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        _baseAddrHookAob = "F3 0F 10 81 90 01 00 00 C3";
        _seriesSig = "48 89 ? ? ? 48 89 ? ? ? 4C 8B ? 44 89 ? ? ? 48 BB";
        _scaleSig = "0F 5B ? F3 0F ? ? ? F3 0F ? ? 48 85";
        _sellFactorSig = "48 8B ? ? E8 ? ? ? ? 8B B8";
        _cleanlinessSig = "F3 0F 10 88 80 8C 00 00";
        _skillScoreSig = "FF 50 ? 8B 78 ? 48 85";
    }

    private static void SignaturesFive()
    {
        _driftScoreSig = "48 8D ? ? ? ? ? E8 ? ? ? ? F3 0F ? ? ? ? 45 33";
        _unbreakableSkillComboSig = "F3 0F ? ? ? ? ? ? F6 C2";
        _creditsAsmAob = "5F 48 FF ? 48 8B ? ? E8";
        _baseAddrHookAob = "48 63 ? 48 69 D0 ? ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? ? ? ? C3 C3 40";
        _sunRgbAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        _xpAob = "41 83 BF 88 00 00 00";
        _xpAmountAob = "8B 89 ? ? ? ? 85 C9 0F 8E";
        _car1Aob = "4C 8B ? ? ? ? ? ? 49 83 C0 ? 66 44";
        _wall1Aob = "45 0F ? ? 4D 8B ? 90";
        _wall2Aob = "45 0F ? ? 4D 8B ? 0F 1F";
        _timeAob = "20 F2 0F 11 43 08 48 83";
        _superCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2";
        _wayPointXAsmAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        _oobAob = "48 83 EC ? 0F 10 ? 41 0F ? ? 0F 10"; // + 83 OR + 0x53
        _waterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00 80";
        _aixAob = "0F 11 ? ? 48 8B ? 0F 10 ? ? 0F 11 ? ? ? ? ? 0F 10";
        _cameraSpeedBaseAob = "54 00 52 ? 41 00 ? ? 4B 00 ? 00 00 00 00 00 05";
        _cameraShutterSpeedAob = "C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? 00 00";
        _cameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        _cameraNoClipSig = "54 48 ? 5F 57 69 6E 56 ? ? ? ? 00 00 00 00 0A 00";
        _buildCap1Sig = "E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 0F 2E";
        _buildCap2Sig = "5F C3 85 FF 48 8B";
        _glowingPaintSig = "0F 11 0A C6 42 F0 01";
        _seasonalSig = "74 ? F3 0F ? ? ? 44 8B";
        _seriesSig = "48 89 ? ? ? 48 89 ? ? ? 4C 8B ? 44 89 ? ? ? 48 BB";
        _scaleSig = "48 8B ? ? E8 ? ? ? ? 90 48 85 ? 74 ? 8B C5";
        _sellFactorSig = "44 8B ? ? ? ? ? 33 D2 48 8B ? ? ? ? ? E8 ? ? ? ? 90";
        _skillTreeSig = "F3 0F ? ? ? 33 D2 48 8B ? ? E8 ? ? ? ? 0F 5A";
        _cleanlinessSig = "F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? B9 ? ? ? ? E8";
        _skillScoreSig = "48 8B ? ? E8 ? ? ? ? 8B 78";
        _skillCostSig = "48 89 5C 24 08 57 48 83 EC 20 48 8B 79 18 33 D2 48 8B 4F 30";
        _timeScaleSig = "74 ? 48 8B ? 48 8B ? FF 90 ? ? ? ? F3 0F ? ? ? ? ? ? F3 0F";
        _headlightSig = "0F 10 ? ? F3 44 ? ? ? ? ? ? ? 83 7B 48";
        _creditsCompareSig = "48 89 ? ? ? 57 48 83 EC ? 48 8D ? ? E8 ? ? ? ? 48 8B";
        _backfireSig = "48 8B ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? E8 ? ? ? ? 0F 28";
        _flyhackSig = "F3 44 ? ? ? ? ? ? ? F3 44 ? ? ? ? ? ? ? F3 44 ? ? ? ? ? ? ? F3 41 ? ? ? F3 41";
        _statsEditorSig = "48 8B 5F 08 80 7B 19 00 75 22 48 8D 4B 20 48 8B D5 E8 74";
        _spinsSig = "48 89 5C 24 08 57 48 83 EC 20 48 8B FA 33 D2 48 8B 4F 10";
        _skillPointsSig = "0F 4F ? 48 8B ? ? E8 ? ? ? ? 48 8B";
    }

    #endregion

    public static void Scan()
    {
        switch (Mw.Gvp.Name)
        {
            case "Forza Horizon 5":
            {
                Task.Run(() => FH5_Scan());
                break;
            }
            case "Forza Horizon 4":
            {
                Task.Run(() => FH4_Scan());
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    #region FH5 Scan
    private static void FH5_Scan()
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SignaturesFive();

        Sv.UiManager.Index = 0;
        Sv.UiManager.ScanAmount = 39;

        XpAddr = Mw.M.ScanForSig(_xpAob).FirstOrDefault() - 14;
        XpAmountAddr = Mw.M.ScanForSig(_xpAmountAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        var sunRgbAddr = Mw.M.ScanForSig(_sunRgbAob).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        Sv.UiManager.AddProgress();

        Car1Addr = Mw.M.ScanForSig(_car1Aob).FirstOrDefault() - 18;
        Sv.UiManager.AddProgress();

        Wall1Addr = Mw.M.ScanForSig(_wall1Aob).FirstOrDefault() - 40;
        Sv.UiManager.AddProgress();

        Wall2Addr = Mw.M.ScanForSig(_wall2Aob).FirstOrDefault() - 40;
        Sv.UiManager.AddProgress();

        WaterAddr = Mw.M.ScanForSig(_waterAob).FirstOrDefault() + 553;
        Sv.UiManager.AddProgress();
        
        CreditsHookAddr = Mw.M.ScanForSig(_creditsAsmAob).FirstOrDefault() + 13;
        Sv.UiManager.AddProgress();

        FlyhackHookAddr = Mw.M.ScanForSig(_flyhackSig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        BaseAddrHook = Mw.M.ScanForSig(_baseAddrHookAob).FirstOrDefault() - 279;
        Sv.UiManager.AddProgress();

        UnbSkillHook = Mw.M.ScanForSig(_unbreakableSkillComboSig).FirstOrDefault() + 13;
        Sv.UiManager.AddProgress();
        
        GlowingPaintAddr = Mw.M.ScanForSig(_glowingPaintSig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        FovHookAddr = Mw.M.ScanForSig("0F 10 ? B0 ? 0F 28 ? ? ? F3 0F").FirstOrDefault();
        Sv.UiManager.AddProgress();

        TimeNopAddr = Mw.M.ScanForSig(_timeAob).FirstOrDefault() + 1;
        Sv.UiManager.AddProgress();

        WayPointXAsmAddr = Mw.M.ScanForSig(_wayPointXAsmAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        BuildAddr1 = Mw.M.ScanForSig(_buildCap1Sig).FirstOrDefault() + 25;
        Sv.UiManager.AddProgress();

        BuildAddr2 = Mw.M.ScanForSig(_buildCap2Sig).FirstOrDefault() - 19;
        Sv.UiManager.AddProgress();

        AiXAddr = Mw.M.ScanForSig(_aixAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        //OobNopAddr = Mw.M.ScanForSig(_oobAob).FirstOrDefault() + 83;
        Sv.UiManager.AddProgress();

        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(_cameraSpeedBaseAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(_cameraBaseAob).FirstOrDefault() - 53;
        Sv.UiManager.AddProgress();

        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(_cameraShutterSpeedAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        PhotoCamEntity.NoClipBase = Mw.M.ScanForSig(_cameraNoClipSig).FirstOrDefault() + 1656;
        Sv.UiManager.AddProgress();

        SeasonalAddr = Mw.M.ScanForSig(_seasonalSig).FirstOrDefault() + 2;
        Sv.UiManager.AddProgress();

        SeriesAddr = Mw.M.ScanForSig(_seriesSig).FirstOrDefault() - 23;
        Sv.UiManager.AddProgress();

        ScaleAddr = Mw.M.ScanForSig(_scaleSig).FirstOrDefault() - 7;
        Sv.UiManager.AddProgress();

        SellFactorAddr = Mw.M.ScanForSig(_sellFactorSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        SkillTreeAddr = Mw.M.ScanForSig(_skillTreeSig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        FovLimitersScan();
        Sv.UiManager.AddProgress();

        CleanlinessAddr = Mw.M.ScanForSig(_cleanlinessSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        SkillScoreAddr = Mw.M.ScanForSig(_skillScoreSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();

        SkillCostAddr = Mw.M.ScanForSig(_skillCostSig).FirstOrDefault() + 39;
        Sv.UiManager.AddProgress();

        DriftScoreAddr = Mw.M.ScanForSig(_driftScoreSig).FirstOrDefault() - 62;
        Sv.UiManager.AddProgress();

        TimeScaleAddr = Mw.M.ScanForSig(_timeScaleSig).FirstOrDefault() + 22;
        Sv.UiManager.AddProgress();

        HeadlightAddr = Mw.M.ScanForSig(_headlightSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        CreditsCompareAddr = Mw.M.ScanForSig(_creditsCompareSig).FirstOrDefault() - 140;
        Sv.UiManager.AddProgress();
    
        BackfireTimeAddr = Mw.M.ScanForSig(_backfireSig).FirstOrDefault() + 4;
        BackfireTypeAddr = BackfireTimeAddr + 125;
        Sv.UiManager.AddProgress();

        //StatsEditorAobHook = Mw.M.ScanForSig(_statsEditorSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        SpinsAddr = Mw.M.ScanForSig(_spinsSig).FirstOrDefault() + 28;
        Sv.UiManager.AddProgress();

        SkillPointsAddr = Mw.M.ScanForSig(_skillPointsSig).FirstOrDefault() - 6;
        Sv.UiManager.AddProgress();
        
        SelfVehicleOption.IsEnabled = true;
        Sv.UiManager.ToggleUiElements(true);
        Sv.Dispatcher.BeginInvoke((Action)delegate
        {
            Sv.StatsButton.IsEnabled = false;
            Shp.SuperCarSwitch.IsEnabled = false;
            UnlocksPage.Unlocks.DiscoverRoadsSwitch.IsEnabled = false;
            EnvironmentPage.Environment.OOBSwitch.IsEnabled = false;
        });
    }
    #endregion

    #region FH4 Scan
    private static void FH4_Scan()
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        Signatures();

        Sv.UiManager.Index = 0;
        Sv.UiManager.ScanAmount = 29;
        
        var sunRgbAddr = Mw.M.ScanForSig(_sunRgbAob).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        Sv.UiManager.AddProgress();
        
        CreditsHookAddr = Mw.M.ScanForSig(_creditsAsmAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        XpAddr = Mw.M.ScanForSig(_xpAob).FirstOrDefault();
        XpAmountAddr = Mw.M.ScanForSig(_xpAmountAob).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        Car1Addr = Mw.M.ScanForSig(_car1Aob).FirstOrDefault() + 106;
        Sv.UiManager.AddProgress();
        
        Car2Addr = Mw.M.ScanForSig(_car2Aob).FirstOrDefault() - 411;
        Sv.UiManager.AddProgress();
        
        Wall1Addr = Mw.M.ScanForSig(_wall1Aob).FirstOrDefault() + 401;
        Sv.UiManager.AddProgress();
        
        Wall2Addr = Mw.M.ScanForSig(_wall2Aob).FirstOrDefault() - 446;
        Sv.UiManager.AddProgress();
        
        FlyhackHookAddr = Mw.M.ScanForSig("F3 44 0F 10 89 ? ? 00 00 F3 44 0F 10 B9").LastOrDefault();
        Sv.UiManager.AddProgress();
        
        BaseAddrHook = Mw.M.ScanForSig(_baseAddrHookAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        GlowingPaintAddr = Mw.M.ScanForSig(_glowingPaintSig).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        UnbSkillHook = Mw.M.ScanForSig(_unbreakableSkillComboSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();
        
        TimeNopAddr = Mw.M.ScanForSig(_timeAob).FirstOrDefault() + 1;
        Sv.UiManager.AddProgress();
        
        WayPointXAsmAddr = Mw.M.ScanForSig(_wayPointXAsmAob).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        //OobNopAddr = Mw.M.ScanForSig(_oobAob).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        SuperCarAddr = Mw.M.ScanForSig(_superCarAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        DiscoverRoadsAddr = Mw.M.ScanForSig(_discoverRoadsAob).FirstOrDefault();
        Sv.UiManager.AddProgress();

        WaterAddr = Mw.M.ScanForSig(_waterAob).FirstOrDefault() + 269;
        Sv.UiManager.AddProgress();

        AiXAddr = Mw.M.ScanForSig(_aixAob).FirstOrDefault() + 16;
        Sv.UiManager.AddProgress();
        
        PhotoCamEntity.SpeedBase = Mw.M.ScanForSig(_cameraSpeedBaseAob).FirstOrDefault();
        Sv.UiManager.AddProgress();
        
        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(_cameraBaseAob).FirstOrDefault() - 665;
        Sv.UiManager.AddProgress(); 
        
        PhotoCamEntity.ShutterSpeedBase = Mw.M.ScanForSig(_cameraShutterSpeedAob).FirstOrDefault() - 0x31;
        Sv.UiManager.AddProgress();
        
        PhotoCamEntity.NoClipBase = Mw.M.ScanForSig(_cameraNoClipSig).FirstOrDefault() + 1532;
        Sv.UiManager.AddProgress();

        FovMenu.FovLock.IsEnabled = false;
        
        FovLimitersScan();
        Sv.UiManager.AddProgress();
        
        ScaleAddr = Mw.M.ScanForSig(_scaleSig).FirstOrDefault() + 3;
        Sv.UiManager.AddProgress();
        
        SellFactorAddr  = Mw.M.ScanForSig(_sellFactorSig).FirstOrDefault() + 9;
        Sv.UiManager.AddProgress();

        CleanlinessAddr = Mw.M.ScanForSig(_cleanlinessSig).FirstOrDefault();
        Sv.UiManager.AddProgress();

        SkillScoreAddr = Mw.M.ScanForSig(_skillScoreSig).FirstOrDefault() + 3;
        Sv.UiManager.AddProgress();

        SelfVehicleOption.IsEnabled = true;
        Sv.UiManager.ToggleUiElements(true);
        
        Sv.Dispatcher.BeginInvoke((Action)delegate
        {
            Sv.BackFireButton.IsEnabled = false;
            Sv.StatsButton.IsEnabled = false;
            EnvironmentPage.Environment.OOBSwitch.IsEnabled = false;
        });
    }
    #endregion

    private static void FovLimitersScan()
    {
        var bases1 = Mw.M.ScanForSig("90 40 CD CC 8C 40 1F 85 2B 3F 00 00 00 40").ToList();
        var bases2 = Mw.M.ScanForSig("CD CC 4C 3E 00 50 43 47 00 00 34 42 00 00 20").ToList();
        var base3 = Mw.M.ScanForSig("CD ? 4C 3E ? ? ? 47 00 ? 34 ? 00 00 20 42 ? 00 A0").FirstOrDefault() - 0x20;
        
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