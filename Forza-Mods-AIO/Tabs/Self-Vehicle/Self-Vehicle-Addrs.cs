using System;
using System.Collections.Generic;
using System.Linq;
using Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.UpdateUi;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicle;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

internal class SelfVehicleAddresses
{
    
    #region Addresses

    public static UIntPtr RotationAddrLong;
    public static UIntPtr UnbSkillHook;
    public static UIntPtr WorldCollisionThreshold, CarCollisionThreshold, SmashableCollisionTolerance;
    public static UIntPtr BaseAddrHook;
    public static UIntPtr TimeNopAddr, TimeAddr;
    public static UIntPtr CheckPointXAsmAddr, WayPointXAsmAddr;
    public static UIntPtr Car1Addr, Car2Addr, Wall1Addr, Wall2Addr;
    public static UIntPtr OobNopAddr;
    public static UIntPtr SuperCarAddr;
    public static UIntPtr DiscoverRoadsAddr;
    public static UIntPtr WaterAddr;
    public static UIntPtr AiXAddr;
    public static UIntPtr XpAddr, XpAmountAddr, CreditsHookAddr;
    public static UIntPtr GlowingPaintAddr;
    public static UIntPtr BuildCapAddrAsm1, BuildCapAddrAsm2;
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

    private static string? _unbreakableSkillComboSig;
    private static string? _baseAddrHookAob;
    private static string? _sunRgbAob;
    private static string? _car1Aob, _car2Aob, _wall1Aob, _wall2Aob;
    private static string? _timeAob;
    private static string? _checkPointXAsmAob, _wayPointXAsmAob;
    private static string? _xpAob, _creditsAsmAob;
    private static string? _oobAob;
    private static string? _superCarAob;
    private static string? _discoverRoadsAob;
    private static string? _waterAob;
    private static string? _aixAob;
    private static string? _cameraSpeedBaseAob, _cameraBaseAob, _cameraShutterSpeedAob, _cameraNoClipSig;
    private static string? _glowingPaintSig;
    private static string? _buildCap1Sig, _buildCap2Sig;
    private static string? _seasonalSig, _seriesSig;
    

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
        _checkPointXAsmAob = "0F 28 ? ? ? ? ? 0F 29 ? ? 0F 29 ? ? C3 90 48 8B ? 55";
        _wayPointXAsmAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50 C1 83 E0 07 3C 07";
        _xpAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83 BD C0 00 00 00";
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
    }

    private static void SignaturesFive()
    {
        _unbreakableSkillComboSig = "48 8B ? ? E8 ? ? ? ? 48 8B ? 48 8B ? FF 92 ? ? ? ? 84 C0 0F 85";
        _creditsAsmAob = "89 84 24 80 00 00 00 4C 8D ? ? ? ? ? 48 8B";
        _baseAddrHookAob = "48 63 ? 48 69 D0 ? ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? ? ? ? C3 C3 40";
        _sunRgbAob = "81 80 80 3B 81 80 80 3B 81 80 80 3B 81 80 80 3B";
        _xpAob = "F3 0F ? ? 89 45 ? 48 8D ? ? ? ? ? 41 83";
        _car1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 45 0F ? ? 4C 8B";
        _wall1Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 90";
        _wall2Aob = "0F 84 ? ? ? ? 4C 8B ? ? ? ? ? ? 49 83 C1 ? 66 44 ? ? ? ? ? ? ? 49 83 C0 ? 66 44 ? ? ? ? ? ? ? 45 0F ? ? 4D 8B ? 0F";
        _timeAob = "20 F2 0F 11 43 08 48 83";
        _superCarAob = "0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 0F 11 ? ? 0F 10 ? ? 48 83 C2";
        _wayPointXAsmAob = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        _oobAob = "48 83 EC ? 0F 10 ? 41 0F ? ? 0F 10"; // + 83 OR + 0x53
        _checkPointXAsmAob = "33 C0 48 89 ? 48 89 ? ? 48 E9 ? ? ? ? 90 40 F3";
        _discoverRoadsAob = "00 96 ? ? ? ? 42 88";
        _waterAob = "3D ? ? ? ? 00 00 A0 ? ? ? ? ? ? ? ? 3F 00 00 80 3F";
        _aixAob = "0F 11 41 50 0F 28 EB";
        _cameraSpeedBaseAob = "54 00 52 ? 41 00 ? ? 4B 00 ? 00 00 00 00 00 05";
        _cameraShutterSpeedAob = "C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? C0 79 C4 ? 00 00";
        _cameraBaseAob = "00 80 ? ? ? ? 3C ? 00 80 ? ? ? ? C0";
        _cameraNoClipSig = "54 48 ? 5F 57 69 6E 56 ? ? ? ? 00 00 00 00 0A 00";
        _buildCap1Sig = "E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 0F 2E";
        _buildCap2Sig = "F3 0F ? ? ? 48 8B ? ? ? 48 8B ? ? ? 48 83 C4 ? 5F C3 85 FF";
        _glowingPaintSig = "0F 11 0A C6 42 F0 01";
        _seasonalSig = "F3 0F ? ? ? 48 8B ? ? ? 48 83 C4 ? 5F C3 E8";
        _seriesSig = "48 89 ? ? ? 48 89 ? ? ? 4C 8B ? 44 89 ? ? ? 48 BB";
    }

    #endregion

    #region FH5 Scan
    public void FH5_Scan()
    {
        SignaturesFive();

        const int scanAmount = 28;
        var scanIndex = 0;

        XpAddr = Mw.M.ScanForSig(_xpAob).FirstOrDefault();
        XpAmountAddr = XpAddr - 80;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        var sunRgbAddr = Mw.M.ScanForSig(_sunRgbAob).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        Car1Addr = Mw.M.ScanForSig(_car1Aob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        Wall1Addr = Mw.M.ScanForSig(_wall1Aob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        Wall2Addr = Mw.M.ScanForSig(_wall2Aob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        SuperCarAddr = Mw.M.ScanForSig(_superCarAob).LastOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        WaterAddr = Mw.M.ScanForSig(_waterAob).FirstOrDefault() + 309;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        CreditsHookAddr = Mw.M.ScanForSig(_creditsAsmAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        RotationAddrLong = Mw.M.ScanForSig("F3 44 0F 10 89 ? ? 00 00 F3 44 0F 10 B9").LastOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        BaseAddrHook = Mw.M.ScanForSig(_baseAddrHookAob).FirstOrDefault() - 279;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        UnbSkillHook = Mw.M.ScanForSig(_unbreakableSkillComboSig).FirstOrDefault() + 9;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        GlowingPaintAddr = Mw.M.ScanForSig(_glowingPaintSig).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        FovHookAddr = Mw.M.ScanForSig("0F 10 ? B0 ? 0F 28 ? ? ? F3 0F").FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        TimeNopAddr = Mw.M.ScanForSig(_timeAob).FirstOrDefault() + 1;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        WayPointXAsmAddr = Mw.M.ScanForSig(_wayPointXAsmAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        DiscoverRoadsAddr = Mw.M.ScanForSig(_discoverRoadsAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        BuildCapAddrAsm1 = Mw.M.ScanForSig(_buildCap1Sig).FirstOrDefault() + 25;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        BuildCapAddrAsm2 = Mw.M.ScanForSig(_buildCap2Sig).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        CheckPointXAsmAddr = Mw.M.ScanForSig("0F 10 89 60 02 00 00 0F 29").FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        AiXAddr = Mw.M.ScanForSig(_aixAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        OobNopAddr = Mw.M.ScanForSig(_oobAob).FirstOrDefault() + 83;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        PhotoCamEntity.Speed = Mw.M.ScanForSig(_cameraSpeedBaseAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(_cameraBaseAob).FirstOrDefault() - 53;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        PhotoCamEntity.Speed = Mw.M.ScanForSig(_cameraShutterSpeedAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        PhotoCamEntity._NoClip = Mw.M.ScanForSig(_cameraNoClipSig).FirstOrDefault() + 1656;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        SeasonalAddr = Mw.M.ScanForSig(_seasonalSig).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        SeriesAddr = Mw.M.ScanForSig(_seriesSig).FirstOrDefault() - 23;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        FovLimitersScan();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Overlay.Overlay.SelfVehicleOption.IsEnabled = true;
        UpdateUI(true, Sv);  
    }
    #endregion

    #region FH4 Scan
    public void FH4_Scan()
    {
        Signatures();

        const int scanAmount = 27;
        var scanIndex = 0;
        
        var sunRgbAddr = Mw.M.ScanForSig(_sunRgbAob).LastOrDefault();
        SunRedAddr = sunRgbAddr;
        SunBlueAddr = sunRgbAddr + 4;
        SunGreenAddr = sunRgbAddr + 8;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        CreditsHookAddr = Mw.M.ScanForSig(_creditsAsmAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        XpAddr = Mw.M.ScanForSig(_xpAob).FirstOrDefault();
        XpAmountAddr = XpAddr - 80;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Car1Addr = Mw.M.ScanForSig(_car1Aob).FirstOrDefault() + 106;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Car2Addr = Mw.M.ScanForSig(_car2Aob).FirstOrDefault() - 411;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Wall1Addr = Mw.M.ScanForSig(_wall1Aob).FirstOrDefault() + 401;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Wall2Addr = Mw.M.ScanForSig(_wall2Aob).FirstOrDefault() - 446;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        RotationAddrLong = Mw.M.ScanForSig("F3 44 0F 10 89 ? ? 00 00 F3 44 0F 10 B9").LastOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        BaseAddrHook = Mw.M.ScanForSig(_baseAddrHookAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        GlowingPaintAddr = Mw.M.ScanForSig(_glowingPaintSig).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        UnbSkillHook = Mw.M.ScanForSig(_unbreakableSkillComboSig).FirstOrDefault() + 9;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        TimeNopAddr = Mw.M.ScanForSig(_timeAob).FirstOrDefault() + 1;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        CheckPointXAsmAddr = Mw.M.ScanForSig(_checkPointXAsmAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        WayPointXAsmAddr = Mw.M.ScanForSig(_wayPointXAsmAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        OobNopAddr = Mw.M.ScanForSig(_oobAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        SuperCarAddr = Mw.M.ScanForSig(_superCarAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        DiscoverRoadsAddr = Mw.M.ScanForSig(_discoverRoadsAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        WaterAddr = Mw.M.ScanForSig(_waterAob).FirstOrDefault() + 309;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        AiXAddr = Mw.M.ScanForSig(_aixAob).FirstOrDefault() + 16;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        PhotoCamEntity.Speed = Mw.M.ScanForSig(_cameraSpeedBaseAob).FirstOrDefault();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        PhotoCamEntity.MainPhotoCamEntity = Mw.M.ScanForSig(_cameraBaseAob).FirstOrDefault() - 665;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar); 
        
        PhotoCamEntity._ShutterSpeed = Mw.M.ScanForSig(_cameraShutterSpeedAob).FirstOrDefault() - 0x31;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        PhotoCamEntity._NoClip = Mw.M.ScanForSig(_cameraNoClipSig).FirstOrDefault() + 1532;
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);

        FovMenu.FovLock.IsEnabled = false;
        
        FovLimitersScan();
        AddProgress(scanAmount, ref scanIndex, Sv.AOBProgressBar);
        
        Overlay.Overlay.SelfVehicleOption.IsEnabled = true;
        UpdateUI(true, Sv);
    }
    #endregion

    private void FovLimitersScan()
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
        FovPage._FovPage!.Dispatcher.Invoke(() => { FovPage._FovPage.UpdateValues(); });
    }
}