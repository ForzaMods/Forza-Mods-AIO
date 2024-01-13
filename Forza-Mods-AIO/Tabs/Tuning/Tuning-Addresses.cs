using System;
using System.Linq;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;
using static System.Windows.Application;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Aero;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Damping;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Gearing;
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
        
    public static UIntPtr TuningHook1;
    public static UIntPtr TuningHook2;
    public static UIntPtr TuningHook3;
       
    
    #endregion
    
    public static void Scan()
    {
        if (Mw.Gvp.Name.Contains('5'))
        {
            const string sig = "3d ? ? ? ? 00 00 ? ? 00 00 5c 42";
            CamberNegStatic = Mw.M.ScanForSig(sig).LastOrDefault() + 0xD;
        }
        else
        {
            const string sig = "3d ? ? ? ? 00 00 ? ? 00 00 5c";
            CamberNegStatic = Mw.M.ScanForSig(sig).LastOrDefault() + 0xD;
        }        
        
        CamberPosStatic = CamberNegStatic + 0x4;
        ToeNegStatic = CamberNegStatic + 0x8;
        ToePosStatic = CamberNegStatic + 0xC;

        Tuning.T.UiManager.Index = 0;
        Tuning.T.UiManager.ScanAmount = 6;
            
        Tuning.T.UiManager.AddProgress();

        var isFh4 = Mw.Gvp.Type == GameVerPlat.GameType.Fh4;
        
        if (isFh4)
        {
            const string sig = "F3 0F 10 81 90 01 00 00 C3";
            SelfVehicleAddresses.BaseAddrHook = Mw.M.ScanForSig(sig).FirstOrDefault();
        }
        else
        {
            const string sig = "0F 2F ? ? ? ? ? 72 ? 0F 2F ? ? ? ? ? 72 ? 0F 2F";
            SelfVehicleAddresses.BaseAddrHook = Mw.M.ScanForSig(sig).FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();

        if (isFh4)
        {   
            const string sig = "49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B";
            TuningHook1 = Mw.M.ScanForSig(sig).FirstOrDefault();
        }
        else
        {
            const string sig = "00 00 00 00 00 00 00 00 E8 ? ? ? ? 6D 9D";
            Base1 = Mw.M.ScanForSig(sig).FirstOrDefault() + 256;
        }
        
        Tuning.T.UiManager.AddProgress();

        if (isFh4)
        {
            const string sig = "48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85";
            TuningHook2 = Mw.M.ScanForSig(sig).FirstOrDefault() + 37;
        }
        else
        {
            const string sig = "00 00 00 00 FF FF FF FF 10 ? ? ? ? 0? 00 00 00 ? ? ? ? 0? 00 00 ? ? ? ? ? 0? 00 00 00";
            Base2 = Mw.M.ScanForSig(sig).FirstOrDefault() + 8;
        }
        
        Tuning.T.UiManager.AddProgress();
                
        if (isFh4)
        {
            const string sig = "80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00";
            TuningHook3 = Mw.M.ScanForSig(sig).FirstOrDefault() + 24;
        }
        else
        {
            Base3 = Mw.M.ScanForSig("D0 84 ? ? ? ? ? 00 00 00 80").FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();

        if (isFh4)
        {
            TuningAsm.GetTuningBaseAddresses();
        }
        
        Task.Run(ReadValues);
        
        TuningOption.IsEnabled = true;
        Tuning.T.UiManager.AddProgress();
        Tuning.T.UiManager.ToggleUiElements(true);
    }

    private const int GearOffset = 0x10B8;
    private const int TireOffset = 0x2D58;
        
    private static void Addresses()
    {
        var isFh4 = Mw.Gvp.Type == GameVerPlat.GameType.Fh4;
        var fh5Base2 = Base1.ToString("X");
        var fh5Base3 = Base2.ToString("X");
        var fh5Base4 = Base3.ToString("X");
        CarEntity.Hook();
        
        TireFrontLeft = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0x1D9C : TireOffset);
        TireFrontRight = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0x337C : TireOffset + 0xAC0);
        TireRearLeft = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0x495C : TireOffset + 0x1580);
        TireRearRight = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0x5F3C : TireOffset + 0x2040);
            
        FinalDrive =  CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xC40 : 0x125C);
        ReverseGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xACC : GearOffset);
        FirstGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xAE0 : GearOffset + 20);
        SecondGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xAF4 : GearOffset + 40);
        ThirdGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB08 : GearOffset + 60);
        FourthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB1C : GearOffset + 80);
        FifthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB30 : GearOffset + 100);
        SixthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB44 : GearOffset + 120);
        SeventhGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB58 : GearOffset + 140);
        EighthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB6C : GearOffset + 160);
        NinthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB80 : GearOffset + 180);
        TenthGear = CarEntity.PlayerCarEntity + (UIntPtr)(isFh4 ? 0xB94 : GearOffset + 200);
        
        RimSizeFront = isFh4 ? Base1 + 0x758 : Mw.M.GetCode(fh5Base4 + ",0x150,0x300,0x7D8");
        RimRadiusFront = isFh4 ? Base1 + 0x760 : Mw.M.GetCode(fh5Base4 + ",0x150,0x300,0x7E0");
        RimSizeRear = isFh4 ? Base1 + 0x75C : Mw.M.GetCode(fh5Base4 + ",0x150,0x300,0x7DC");
        RimRadiusRear = isFh4 ? Base1 + 0x764 : Mw.M.GetCode(fh5Base4 + ",0x150,0x300,0x7E4");

        CamberNeg = isFh4 ? Base2 + 0x3E4 : Mw.M.GetCode(fh5Base2 + ",0x8B0,0x490");
        CamberPos = isFh4 ? Base2 + 0x3E8 : Mw.M.GetCode(fh5Base2 + ",0x8B0,0x494");
        ToeNeg = isFh4 ? Base2 + 0x3EC : Mw.M.GetCode(fh5Base2 + ",0x8B0,0x498");
        ToePos = isFh4 ? Base2 + 0x3F0 : Mw.M.GetCode(fh5Base2 + ",0x8B0,0x49C");

        FrontAntirollMin = isFh4 ? Base3 + 0x3F8 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x5F0");
        FrontAntirollMax = isFh4 ? Base3 + 0x3FC : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x5F4");
        RearAntirollMin = isFh4 ? Base3 + 0x4A4 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x744");
        RearAntirollMax = isFh4 ? Base3 + 0x4A8 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x748");

        SpringFrontMin = isFh4 ? Base3 + 0x3AC : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x550");
        SpringFrontMax = isFh4 ? Base3 + 0x3B0 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x554");
        SpringRearMin = isFh4 ? Base3 + 0x458 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6A4");
        SpringRearMax = isFh4 ? Base3 + 0x45C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6A8");

        FrontRideHeightMin = isFh4 ? Base3 + 0x394 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x534");
        FrontRideHeightMax = isFh4 ? Base3 + 0x398 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x538");
        RearRideHeightMin = isFh4 ? Base3 + 0x440 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x688");
        RearRideHeightMax = isFh4 ? Base3 + 0x444 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x68C");

        FrontRestriction = isFh4 ? Base3 + 0x39C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x53C");
        RearRestriction = isFh4 ? Base3 + 0x448 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x690");

        FrontAeroMin = isFh4 ? Base3 + 0x234 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x3A4");
        FrontAeroMax = isFh4 ? Base3 + 0x23C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x3AC");
        RearAeroMin = isFh4 ? Base3 + 0x294 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x404");
        RearAeroMax = isFh4 ? Base3 + 0x29C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x40C");

        FrontReboundStiffnessMin = isFh4 ? Base3 + 0x3D4 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x580");
        FrontReboundStiffnessMax = isFh4 ? Base3 + 0x3D8 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x584");
        RearReboundStiffnessMin = isFh4 ? Base3 + 0x484 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6D8");
        RearReboundStiffnessMax = isFh4 ? Base3 + 0x480 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6DC");

        FrontBumpStiffnessMin = isFh4 ? Base3 + 0x3B8 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x560");
        FrontBumpStiffnessMax = isFh4 ? Base3 + 0x3BC : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x564");
        RearBumpStiffnessMin = isFh4 ? Base3 + 0x464 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6B4");
        RearBumpStiffnessMax = isFh4 ? Base3 + 0x468 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x6B8");

        Wheelbase = isFh4 ? Base3 + 0xC0 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0xD0");
        FrontWidth = isFh4 ? Base3 + 0xC4 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0xD4");
        RearWidth = isFh4 ? Base3 + 0xC8 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0xD8");
        FrontSpacer = isFh4 ? Base3 + 0x610 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x9FC");
        RearSpacer = isFh4 ? Base3 + 0x614 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0xA00");

        AngleMax = isFh4 ? Base3 + 0x534 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x828");
        AngleMax2 = isFh4 ? Base3 + 0x538 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x82C");
        VelocityStraight = isFh4 ? Base3 + 0x53C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x830");
        VelocityTurning = isFh4 ? Base3 + 0x540 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x834");
        VelocityCountersteer = isFh4 ? Base3 + 0x544 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x838");
        VelocityDynamicPeek = isFh4 ? Base3 + 0x548 : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x83C");
        TimeToMaxSteering = isFh4 ? Base3 + 0x54C : Mw.M.GetCode(fh5Base3 + ",0x330,0x8,0x1E0,0x840");
    }
        
    private static async Task ReadValues()
    {
        while (Mw.Attached)
        {
            await Task.Delay(250);
            Addresses();

            Current.Dispatcher.Invoke(() =>
            {
                ReadAlignmentValues();
                ReadAeroValues();
                ReadGearingValues();
                ReadDampingValues();
                ReadOthersValues();
                ReadSpringsValues();
                ReadSteeringValues();
                ReadTiresValues();
            });
        }
    }
    
    private static void ReadAlignmentValues()
    {
        Alignment.Al.CodeChange = true;
        Alignment.Al.CamberNegBox.Value = Mw.M.ReadMemory<float>(CamberNegStatic);
        Alignment.Al.CamberPosBox.Value = Mw.M.ReadMemory<float>(CamberPosStatic);
        Alignment.Al.ToeNegBox.Value = Mw.M.ReadMemory<float>(ToeNegStatic);
        Alignment.Al.ToePosBox.Value = Mw.M.ReadMemory<float>(ToePosStatic);
        Alignment.Al.CodeChange = false;
    }

    private static void ReadAeroValues()
    {
        Ae.CodeChange = true;
        Ae.FrontAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMin), 3);
        Ae.FrontAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMax), 3);
        Ae.RearAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMin), 3);
        Ae.RearAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMax), 3);
        Ae.CodeChange = false;
    }
    
    private static void ReadGearingValues()
    {
        G.CodeChange = true;
        G.FinalDriveBox.Value = Math.Round(Mw.M.ReadMemory<float>(FinalDrive), 3);
        G.ReverseGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ReverseGear), 3);
        G.FirstGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FirstGear), 3);
        G.SecondGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SecondGear), 3);
        G.ThirdGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ThirdGear), 3);
        G.FourthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FourthGear), 3);
        G.FifthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FifthGear), 3);
        G.SixthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SixthGear), 3);
        G.SeventhGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SeventhGear), 3);
        G.EighthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(EighthGear), 3);
        G.NinthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(NinthGear), 3);
        G.TenthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(TenthGear), 3);
        G.CodeChange = false;
    }

    private static void ReadDampingValues()
    {
        
        D.CodeChange = true;
        D.FrontAntirollMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAntirollMin), 3);
        D.FrontAntirollMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAntirollMax), 3);
        D.RearAntirollMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAntirollMin), 3);
        D.RearAntirollMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAntirollMax), 3);

        D.FrontBumpStiffnessMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontBumpStiffnessMin), 3);
        D.FrontBumpStiffnessMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontBumpStiffnessMax), 3);
        D.RearBumpStiffnessMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearBumpStiffnessMin), 3);
        D.RearBumpStiffnessMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearBumpStiffnessMax), 3);

        D.FrontReboundStiffnessMinBox.Value = Mw.M.ReadMemory<float>(FrontReboundStiffnessMin);
        D.FrontReboundStiffnessMaxBox.Value = Mw.M.ReadMemory<float>(FrontReboundStiffnessMax);
        D.RearReboundStiffnessMinBox.Value = Mw.M.ReadMemory<float>(RearReboundStiffnessMin);
        D.RearReboundStiffnessMaxBox.Value = Mw.M.ReadMemory<float>(RearReboundStiffnessMax);

        D.CodeChange = false;

    }

    private static void ReadOthersValues()
    {
        Others.O.CodeChange = true;
        Others.O.WheelbaseBox.Value = Math.Round(Mw.M.ReadMemory<float>(Wheelbase), 3);
        Others.O.RimSizeFrontBox.Value = Math.Round(Mw.M.ReadMemory<float>(RimSizeFront),3);
        Others.O.RimSizeRearBox.Value = Math.Round(Mw.M.ReadMemory<float>(RimSizeRear),3);
        Others.O.RimRadiusFrontBox.Value = Math.Round(Mw.M.ReadMemory<float>(RimRadiusFront),3);
        Others.O.RimRadiusRearBox.Value = Math.Round(Mw.M.ReadMemory<float>(RimRadiusRear),3);
        Others.O.FrontWidthBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontWidth),3);
        Others.O.RearWidthBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearWidth),3);
        Others.O.FrontSpacerBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontSpacer),3);
        Others.O.RearSpacerBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearSpacer),3);
        Others.O.CodeChange = false;
    }
    
    private static void ReadSpringsValues()
    {
        Sp.CodeChange = true;
        Sp.SpringFrontMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringFrontMin), 3);
        Sp.SpringFrontMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringFrontMax), 3);
        Sp.SpringRearMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringRearMin), 3);
        Sp.SpringRearMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(SpringRearMax), 3);
        Sp.CodeChange = false;

        Sp.RideHeightCodeChange = true;
        Sp.FrontRideHeightMinBox.Value = ConvertGameValueToUnit(Sp.FrontRideHeightMinUnitBox,FrontRideHeightMin);
        Sp.FrontRideHeightMaxBox.Value = ConvertGameValueToUnit(Sp.FrontRideHeightMaxUnitBox,FrontRideHeightMax);
        Sp.RearRideHeightMinBox.Value = ConvertGameValueToUnit(Sp.RearRideHeightMinUnitBox,RearRideHeightMin);
        Sp.RearRideHeightMaxBox.Value = ConvertGameValueToUnit(Sp.RearRideHeightMaxUnitBox,RearRideHeightMax);
        Sp.RideHeightCodeChange = false;
    }

    private static void ReadSteeringValues()
    {
        St.CodeChange = true;
        St.AngleMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(AngleMax), 3);
        St.AngleMax2Box.Value = Math.Round(Mw.M.ReadMemory<float>(AngleMax2), 3);
        St.VelocityCountersteerBox.Value = Math.Round(Mw.M.ReadMemory<float>(VelocityCountersteer), 3);
        St.VelocityDynamicPeekBox.Value = Math.Round(Mw.M.ReadMemory<float>(VelocityDynamicPeek), 3);
        St.VelocityStraightBox.Value = Math.Round(Mw.M.ReadMemory<float>(VelocityStraight), 3);
        St.VelocityTurningBox.Value = Math.Round(Mw.M.ReadMemory<float>(VelocityTurning), 3);
        St.TimeToMaxSteeringBox.Value = Math.Round(Mw.M.ReadMemory<float>(TimeToMaxSteering), 3);
        St.CodeChange = false;
    }
    
    private static void ReadTiresValues()
    {
        T.CodeChange = true;
        T.TireFrontLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontLeft) / TireFrontLeftDivider, 3);
        T.TireFrontRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontRight) / TireFrontRightDivider, 3);
        T.TireRearLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearLeft) / TireRearLeftDivider, 3);
        T.TireRearRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearRight) / TireRearRightDivider, 3);
        T.CodeChange = false;
    }
}