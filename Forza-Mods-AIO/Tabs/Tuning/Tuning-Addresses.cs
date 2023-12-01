using System;
using System.Linq;
using System.Threading.Tasks;
using Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;
using static System.Windows.Application;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Aero;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Damping;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Gearing;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Others;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Springs;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Steering;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Tires;

namespace Forza_Mods_AIO.Tabs.Tuning;

internal class TuningAddresses
{
    #region Vars
    #region Tires
    public static UIntPtr TireFrontLeft;
    public static UIntPtr TireFrontRight;
    public static UIntPtr TireRearLeft;
    public static UIntPtr TireRearRight;
    #endregion
    #region Gearing
    public static UIntPtr FinalDrive;
    public static UIntPtr ReverseGear;
    public static UIntPtr FirstGear;
    public static UIntPtr SecondGear;
    public static UIntPtr ThirdGear;
    public static UIntPtr FourthGear;
    public static UIntPtr FifthGear;
    public static UIntPtr SixthGear;
    public static UIntPtr SeventhGear;
    public static UIntPtr EighthGear;
    public static UIntPtr NinthGear;
    public static UIntPtr TenthGear;
    #endregion
    #region Alignment
    // Camber
    public static UIntPtr CamberPos;
    public static UIntPtr CamberPosStatic;
    public static UIntPtr CamberNeg;
    public static UIntPtr CamberNegStatic;

    // Toe
    public static UIntPtr ToePos;
    public static UIntPtr ToePosStatic;
    public static UIntPtr ToeNeg;
    public static UIntPtr ToeNegStatic;
    #endregion
    #region Springs
    // Springs
    public static UIntPtr SpringFrontMin;
    public static UIntPtr SpringFrontMax;
    public static UIntPtr SpringRearMin;
    public static UIntPtr SpringRearMax;

    // Ride Height
    public static UIntPtr FrontRideHeightMin;
    public static UIntPtr FrontRideHeightMax;
    public static UIntPtr RearRideHeightMin;
    public static UIntPtr RearRideHeightMax;

    // Restrictions
    public static UIntPtr FrontRestriction;
    public static UIntPtr RearRestriction;
    #endregion
    #region Damping/Antiroll Bars
    // Antiroll Bars
    public static UIntPtr FrontAntirollMin;
    public static UIntPtr FrontAntirollMax;
    public static UIntPtr RearAntirollMin;
    public static UIntPtr RearAntirollMax;

    // Rebound Stiffness
    public static UIntPtr FrontReboundStiffnessMin;
    public static UIntPtr FrontReboundStiffnessMax;
    public static UIntPtr RearReboundStiffnessMin;
    public static UIntPtr RearReboundStiffnessMax;

    // Bump Stiffness
    public static UIntPtr FrontBumpStiffnessMin;
    public static UIntPtr FrontBumpStiffnessMax;
    public static UIntPtr RearBumpStiffnessMin;
    public static UIntPtr RearBumpStiffnessMax;
    #endregion
    #region Aero
    public static UIntPtr FrontAeroMin;
    public static UIntPtr FrontAeroMax;
    public static UIntPtr RearAeroMin;
    public static UIntPtr RearAeroMax;
    #endregion
    #region Steering
    // Max angle values
    public static UIntPtr AngleMax;
    public static UIntPtr AngleMax2;

    // Velocity / Time Values
    public static UIntPtr VelocityStraight;
    public static UIntPtr VelocityTurning;
    public static UIntPtr VelocityCountersteer;
    public static UIntPtr VelocityDynamicPeek;
    public static UIntPtr TimeToMaxSteering;
    #endregion
    #region Others
    // Wheelbase
    public static UIntPtr Wheelbase;
    public static UIntPtr FrontWidth;
    public static UIntPtr RearWidth;

    // Spacers
    public static UIntPtr FrontSpacer;
    public static UIntPtr RearSpacer;

    // Rims
    public static UIntPtr RimSizeFront;
    public static UIntPtr RimSizeRear;
    public static UIntPtr RimRadiusFront;
    public static UIntPtr RimRadiusRear;
    #endregion
        
    public static UIntPtr Base1;
    public static UIntPtr Base2;
    public static UIntPtr Base3;
    public static UIntPtr Base4;
        
    public static UIntPtr TuningTableHook1;
    public static UIntPtr TuningTableHook2;
    public static UIntPtr TuningTableHook3;
    public static UIntPtr TuningTableHook4;
        
    private const string Hook1SigFh4 = "0F 29 ? ? 33 F6 49 81 C7";
    private const string Hook1SigFh5 = "F3 0F ? ? ? ? ? ? F3 0F ? ? F3 0F ? ? F3 0F ? ? 0F 57 ? F3 0F ? ? ? ? ? ? F3 0F ? ? C3";
    private const string Hook2SigFh4 = "49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B";
    private const string Hook2SigFh5 = "0F 84 ? ? ? ? 8B 81 ? ? ? ? 83 F8 ? 74";
    private const string Hook3SigFh4 = "48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85";
    private const string Hook3SigFh5 = "48 8B ? 48 8B ? FF 92 ? ? ? ? 48 8B ? EB ? 49 8B";
    private const string Hook4SigFh4 = "80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00";
    private const string Hook4SigFh5 = "41 B8 ? ? ? ? 48 8D ? ? ? ? ? 48 8D ? ? ? ? ? E8 ? ? ? ? 0F 28 ? 48 8D ? ? ? 48 8D";
    
    #endregion
    
    public static void Scan()
    {
        CamberNegStatic = Mw.M.ScanForSig("3d ? ? ? ? 00 00 ? ? 00 00 5c").LastOrDefault() + 0xD;
        CamberPosStatic = CamberNegStatic + 0x4;
        ToeNegStatic = CamberNegStatic + 0x8;
        ToePosStatic = CamberNegStatic + 0xC;

        Tuning.T!.UiManager.Index = 0;
        Tuning.T.UiManager.ScanAmount = 6;
            
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook1 = Mw.M.ScanForSig(Hook1SigFh4).FirstOrDefault() + 21;
        }
        else
        {
            TuningTableHook1 = Mw.M.ScanForSig(Hook1SigFh5).FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook2 = Mw.M.ScanForSig(Hook2SigFh4).FirstOrDefault();
        }
        else
        {
            TuningTableHook2 = Mw.M.ScanForSig(Hook2SigFh5).FirstOrDefault() + 1130;
        }
        
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook3 = Mw.M.ScanForSig(Hook3SigFh4).FirstOrDefault() + 37;
        }
        else
        {
            TuningTableHook3 = Mw.M.ScanForSig(Hook3SigFh5).FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();
                
        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook4 = Mw.M.ScanForSig(Hook4SigFh4).FirstOrDefault() + 24;
        }
        else
        {
            TuningTableHook4 = Mw.M.ScanForSig(Hook4SigFh5).FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();

        TuningAsm.GetTuningBaseAddresses();
        Task.Run(() => ReadValues());
        TuningOption.IsEnabled = true;
        Tuning.T.UiManager.AddProgress();
        Tuning.T.UiManager.ToggleUiElements(true);
    }

    private const int AeroOffset = 0x3A0;
    private const int GearOffset = 0x10B8;
    private const int TireOffset = 0x2D58;
        
    public static void Addresses()
    {
        TireFrontLeft = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0x1D9C : TireOffset);
        TireFrontRight = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0x337C : TireOffset + 2752);
        TireRearLeft = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0x495C : TireOffset + 5504);
        TireRearRight = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0x5F3C : TireOffset + 8256);
            
        FinalDrive = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xC40 : 0x125C);
        ReverseGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xACC : GearOffset - 20);
        FirstGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xAE0 : GearOffset);
        SecondGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xAF4 : GearOffset + 20);
        ThirdGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB08 : GearOffset + 40);
        FourthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB1C : GearOffset + 60);
        FifthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB30 : GearOffset + 80);
        SixthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB44 : GearOffset + 100);
        EighthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB6C : GearOffset + 140);
        SeventhGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB58 : GearOffset + 120);
        NinthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB80 : GearOffset + 160);
        TenthGear = Base1 + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 0xB94 : GearOffset + 180);
            
        RimSizeFront = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x758 : Base3 + 0x7D8;
        RimRadiusFront = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x760 : Base3 + 0x7DC;
        RimSizeRear = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x75C : Base3 + 0x7E0;
        RimRadiusRear = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x764 : Base3 + 0x7E4;
            
        CamberNeg = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3E4 : Base4 + 0x490;
        CamberPos = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3E8 : Base4 + 0x494;
        ToeNeg = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3EC : Base4 + 0x498;
        ToePos = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3F0 : Base4 + 0x49C;

        FrontAntirollMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3F8 : Base2 + AeroOffset + 588;
        FrontAntirollMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3FC : Base2 + AeroOffset + 592;
        RearAntirollMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x4A4 : Base2 + AeroOffset + 928;
        RearAntirollMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x4A8 : Base2 + AeroOffset + 932;

        SpringFrontMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3AC : Base2 + AeroOffset + 432;
        SpringFrontMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3B0 : Base2 + AeroOffset + 772;
        SpringRearMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x458 : Base2 + AeroOffset + 436;
        SpringRearMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x45C : Base2 + AeroOffset + 776;

        FrontRideHeightMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x394 : Base2 + AeroOffset + 400;
        FrontRideHeightMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x398 : Base2 + AeroOffset + 404;
        RearRideHeightMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x440 : Base2 + AeroOffset + 740;
        RearRideHeightMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x444 : Base2 + AeroOffset + 744;

        FrontRestriction = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x39C : Base2 + AeroOffset + 408;
        RearRestriction = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x448 : Base2 + AeroOffset + 748;

        FrontAeroMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x234 : Base2 + AeroOffset;
        FrontAeroMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x23C : Base2 + AeroOffset + 8;
        RearAeroMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x294 : Base2 + AeroOffset + 96;
        RearAeroMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x29C : Base2 + AeroOffset + 104;

        FrontReboundStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3D4 : Base2 + AeroOffset + 480;
        FrontReboundStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3D8 : Base2 + AeroOffset + 484;
        RearReboundStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x480 : Base2 + AeroOffset + 820;
        RearReboundStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x484 : Base2 + AeroOffset + 824;

        FrontBumpStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3B8 : Base2 + AeroOffset + 444;
        FrontBumpStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3BC : Base2 + AeroOffset + 448;
        RearBumpStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x464 : Base2 + AeroOffset + 784;
        RearBumpStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x468 : Base2 + AeroOffset + 788;

        Wheelbase = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC0 : Base2 + 0xD0;
        FrontWidth = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC4 : Base2 + 0xD4;
        RearWidth = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC8 : Base2 + 0xD8;
        FrontSpacer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x610 : Base2 + AeroOffset + 1624;
        RearSpacer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x614 : Base2 + AeroOffset + 1628;

        AngleMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x534 : Base2 + AeroOffset + 1156;
        AngleMax2 = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x538 : Base2 + AeroOffset + 1160;
        VelocityStraight = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x53C : Base2 + AeroOffset + 1164;
        VelocityTurning = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x540 : Base2 + AeroOffset + 1168;
        VelocityCountersteer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x544 : Base2 + AeroOffset + 1172;
        VelocityDynamicPeek = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x548 : Base2 + AeroOffset + 1176;
        TimeToMaxSteering = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x54C : Base2 + AeroOffset + 1180;
    }
        
    private static void ReadValues()
    {
        //These can be only read once as they dont change when a car is switched
        Current.Dispatcher.Invoke(() =>
        {
            Alignment.Al.CamberNegBox.Value = Mw.M.ReadMemory<float>(CamberNegStatic);
            Alignment.Al.CamberPosBox.Value = Mw.M.ReadMemory<float>(CamberPosStatic);
            Alignment.Al.ToeNegBox.Value = Mw.M.ReadMemory<float>(ToeNegStatic);
            Alignment.Al.ToePosBox.Value = Mw.M.ReadMemory<float>(ToePosStatic);
        });

        //Rest requires a constant reading
        while (Mw.Attached)
        {
            Task.Delay(500).Wait();
                
            Current.Dispatcher.BeginInvoke(() =>
            {
                #region Aero

                Ae.FrontAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMin), 3);
                Ae.FrontAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMax), 3);
                Ae.RearAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMin), 3);
                Ae.RearAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMax), 3);
                    
                #endregion
                #region Gearing

                G.FinalDriveBox.Value = Math.Round(Mw.M.ReadMemory<float>(FinalDrive), 5);
                G.ReverseGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ReverseGear), 5);
                G.FirstGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FirstGear), 5);
                G.SecondGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SecondGear), 5);
                G.ThirdGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ThirdGear), 5);
                G.FourthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FourthGear), 5);
                G.FifthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FifthGear), 5);
                G.SixthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SixthGear), 5);
                G.SeventhGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SeventhGear), 5);
                G.EighthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(EighthGear), 5);
                G.NinthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(NinthGear), 5);
                G.TenthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(TenthGear), 5);
                    
                #endregion
                #region Damping
                    
                D.FrontAntirollMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAntirollMin), 5);
                D.FrontAntirollMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAntirollMax), 5);
                D.RearAntirollMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAntirollMin), 5);
                D.RearAntirollMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAntirollMax), 5);

                D.FrontBumpStiffnessMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontBumpStiffnessMin), 5);
                D.FrontBumpStiffnessMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontBumpStiffnessMax), 5);
                D.RearBumpStiffnessMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearBumpStiffnessMin), 5);
                D.RearBumpStiffnessMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearBumpStiffnessMax), 5);

                D.FrontReboundStiffnessMinBox.Value = Mw.M.ReadMemory<float>(FrontReboundStiffnessMin);
                D.FrontReboundStiffnessMaxBox.Value = Mw.M.ReadMemory<float>(FrontReboundStiffnessMax);
                D.RearReboundStiffnessMinBox.Value = Mw.M.ReadMemory<float>(RearReboundStiffnessMin);
                D.RearReboundStiffnessMaxBox.Value = Mw.M.ReadMemory<float>(RearReboundStiffnessMax);
                    
                #endregion
                #region Others
                    
                O.WheelbaseBox.Value = Mw.M.ReadMemory<float>(Wheelbase);
                O.RimSizeFrontBox.Value = Mw.M.ReadMemory<float>(RimSizeFront);
                O.RimSizeRearBox.Value = Mw.M.ReadMemory<float>(RimSizeRear);
                O.RimRadiusFrontBox.Value = Mw.M.ReadMemory<float>(RimRadiusFront);
                O.RimRadiusRearBox.Value = Mw.M.ReadMemory<float>(RimRadiusRear);
                O.FrontWidthBox.Value = Mw.M.ReadMemory<float>(FrontWidth);
                O.RearWidthBox.Value = Mw.M.ReadMemory<float>(RearWidth);
                O.FrontSpacerBox.Value = Mw.M.ReadMemory<float>(FrontSpacer);
                O.RearSpacerBox.Value = Mw.M.ReadMemory<float>(RearSpacer);
                    
                #endregion
                #region Springs
                    
                Sp.SpringFrontMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringFrontMin), 3);
                Sp.SpringFrontMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringFrontMax), 3);
                Sp.SpringRearMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringRearMin), 3);
                Sp.SpringRearMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringRearMax), 3);

                Sp.FrontRideHeightMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontRideHeightMin), 6);
                Sp.FrontRideHeightMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontRideHeightMax), 6);
                Sp.RearRideHeightMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearRideHeightMin), 6);
                Sp.RearRideHeightMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearRideHeightMax), 6);
                    
                #endregion
                #region Steering
                    
                St.AngleMaxBox.Value = Mw.M.ReadMemory<float>(AngleMax);
                St.AngleMax2Box.Value = Mw.M.ReadMemory<float>(AngleMax2);
                St.VelocityCountersteerBox.Value = Mw.M.ReadMemory<float>(VelocityCountersteer);
                St.VelocityDynamicPeekBox.Value = Mw.M.ReadMemory<float>(VelocityDynamicPeek);
                St.VelocityStraightBox.Value = Mw.M.ReadMemory<float>(VelocityStraight);
                St.VelocityTurningBox.Value = Mw.M.ReadMemory<float>(VelocityTurning);
                St.TimeToMaxSteeringBox.Value = Math.Round(Mw.M.ReadMemory<float>(TimeToMaxSteering), 5);
                    
                #endregion
                #region Tires
                    
                T.TireFrontLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontLeft) / TireFrontLeftDivider, 5);
                T.TireFrontRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontRight) / TireFrontRightDivider, 5);
                T.TireRearLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearLeft) / TireRearLeftDivider, 5);
                T.TireRearRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearRight) / TireRearRightDivider, 5);
                    
                #endregion
            });
        }
    }
}