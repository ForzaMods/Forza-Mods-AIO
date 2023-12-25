using System;
using System.Linq;
using System.Threading.Tasks;
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
    public static UIntPtr Base4;
        
    public static UIntPtr TuningTableHook1;
    public static UIntPtr TuningTableHook2;
    public static UIntPtr TuningTableHook3;
    public static UIntPtr TuningTableHook4;
        
    private const string Hook1SigFh4 = "0F 29 ? ? 33 F6 49 81 C7";
    private const string Hook2SigFh4 = "49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B";
    private const string Hook3SigFh4 = "48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85";
    private const string Hook4SigFh4 = "80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00";
    
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
            SelfVehicleAddresses.BaseAddrHook = Mw.M.ScanForSig("48 63 ? 48 69 D0 ? ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? ? ? ? C3 C3 40").FirstOrDefault() - 279;
        }
        
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook2 = Mw.M.ScanForSig(Hook2SigFh4).FirstOrDefault();
        }
        else
        {
            Base2 = Mw.M.ScanForSig("00 00 00 00 00 00 00 00 E8 ? ? ? ? 6D 9D").FirstOrDefault() + 256;
        }
        
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook3 = Mw.M.ScanForSig(Hook3SigFh4).FirstOrDefault() + 37;
        }
        else
        {
            Base3 = Mw.M.ScanForSig("00 00 00 00 FF FF FF FF 10 ? ? ? ? 0? 00 00 00 ? ? ? ? 0? 00 00 ? ? ? ? ? 0? 00 00 00").FirstOrDefault() + 8;
        }
        
        Tuning.T.UiManager.AddProgress();
                
        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            TuningTableHook4 = Mw.M.ScanForSig(Hook4SigFh4).FirstOrDefault() + 24;
        }
        else
        {
            Base4 = Mw.M.ScanForSig("D0 84 ? ? ? ? ? 00 00 00 80").FirstOrDefault();
        }
        
        Tuning.T.UiManager.AddProgress();

        if (Mw.Gvp.Name.Contains('4'))
        {
            TuningAsm.GetTuningBaseAddresses();
        }
        else
        {
            Addresses();
        }
        
        Task.Run(() => ReadValues());
        TuningOption.IsEnabled = true;
        Tuning.T.UiManager.AddProgress();
        Tuning.T.UiManager.ToggleUiElements(true);
    }

    private const int GearOffset = 0x10B8;
    private const int TireOffset = 0x2D58;
        
    public static void Addresses()
    {
        TireFrontLeft = Base1 + 0x1D9C;
        TireFrontRight = Base1 + 0x337C;
        TireRearLeft = Base1 + 0x495C;
        TireRearRight = Base1 + 0x5F3C;
            
        FinalDrive = Base1 + 0xC40;
        ReverseGear = Base1 + 0xACC;
        FirstGear = Base1 +  0xAE0;
        SecondGear = Base1 +  0xAF4;
        ThirdGear = Base1 + 0xB08;
        FourthGear = Base1 + 0xB1C;
        FifthGear = Base1 +0xB30;
        SixthGear = Base1 + 0xB44;
        EighthGear = Base1 +0xB6C;
        SeventhGear = Base1 +0xB58;
        NinthGear = Base1 +0xB80;
        TenthGear = Base1 + 0xB94;
            
        RimSizeFront = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x758 : Mw.M.GetCode(Base4.ToString("X") + ",0x150,0x300,0x7D8");
        RimRadiusFront = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x760 : Mw.M.GetCode(Base4.ToString("X") + ",0x150,0x300,0x7E0");
        RimSizeRear = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x75C : Mw.M.GetCode(Base4.ToString("X") + ",0x150,0x300,0x7DC");
        RimRadiusRear = Mw.Gvp.Name == "Forza Horizon 4" ? Base2 + 0x764 : Mw.M.GetCode(Base4.ToString("X") + ",0x150,0x300,0x7E4");

        CamberNeg = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3E4 : Mw.M.GetCode(Base2.ToString("X") + ",0x8B0,0x490");
        CamberPos = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3E8 : Mw.M.GetCode(Base2.ToString("X") + ",0x8B0,0x494");
        ToeNeg = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3EC : Mw.M.GetCode(Base2.ToString("X") + ",0x8B0,0x498");
        ToePos = Mw.Gvp.Name == "Forza Horizon 4" ? Base3 + 0x3F0 : Mw.M.GetCode(Base2.ToString("X") + ",0x8B0,0x49C");

        FrontAntirollMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3F8 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x5EC");
        FrontAntirollMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3FC : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x5F0");
        RearAntirollMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x4A4 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x740");
        RearAntirollMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x4A8 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x744");

        SpringFrontMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3AC : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x550");
        SpringFrontMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3B0 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x554");
        SpringRearMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x458 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6A4");
        SpringRearMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x45C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6A8");

        FrontRideHeightMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x394 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x530");
        FrontRideHeightMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x398 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x534");
        RearRideHeightMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x440 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x684");
        RearRideHeightMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x444 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x688");

        FrontRestriction = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x39C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x538");
        RearRestriction = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x448 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x68C");

        FrontAeroMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x234 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x3A0");
        FrontAeroMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x23C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x3A8");
        RearAeroMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x294 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x400");
        RearAeroMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x29C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x408");

        FrontReboundStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3D4 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x580");
        FrontReboundStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3D8 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x584");
        RearReboundStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x480 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6D4");
        RearReboundStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x484 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6D8");

        FrontBumpStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3B8 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x55C");
        FrontBumpStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x3BC : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x560");
        RearBumpStiffnessMin = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x464 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6B0");
        RearBumpStiffnessMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x468 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x6B4");

        Wheelbase = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC0 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0xD0");
        FrontWidth = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC4 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0xD4");
        RearWidth = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0xC8 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0xD8");
        FrontSpacer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x610 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x9FC");
        RearSpacer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x614 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0xA00");

        AngleMax = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x534 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x828");
        AngleMax2 = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x538 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x82C");
        VelocityStraight = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x53C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x830");
        VelocityTurning = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x540 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x834");
        VelocityCountersteer = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x544 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x838");
        VelocityDynamicPeek = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x548 : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x83C");
        TimeToMaxSteering = Mw.Gvp.Name == "Forza Horizon 4" ? Base4 + 0x54C : Mw.M.GetCode(Base3.ToString("X") + ",0x330,0x8,0x1E0,0x840");
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

        if (Mw.Gvp.Name.Contains('5'))
        {
            CarEntity.Hook();
        }

        //Rest requires a constant reading
        while (Mw.Attached)
        {
            Task.Delay(500).Wait();
                
            Current.Dispatcher.Invoke(() =>
            {
                #region Aero

                Ae.FrontAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMin), 3);
                Ae.FrontAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(FrontAeroMax), 3);
                Ae.RearAeroMinBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMin), 3);
                Ae.RearAeroMaxBox.Value = Math.Round(Mw.M.ReadMemory<float>(RearAeroMax), 3);
                    
                #endregion
                #region Gearing

                if (Mw.Gvp.Name.Contains('5'))
                {
                    G.FinalDriveBox.Value = Math.Round(Mw.M.ReadMemory<float>(FinalDrive = CarEntity.PlayerCarEntity + 0x125C), 5);
                    G.ReverseGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ReverseGear = CarEntity.PlayerCarEntity + GearOffset - 20), 5);
                    G.FirstGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FirstGear = CarEntity.PlayerCarEntity + GearOffset), 5);
                    G.SecondGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SecondGear =CarEntity.PlayerCarEntity + GearOffset + 20), 5);
                    G.ThirdGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(ThirdGear = CarEntity.PlayerCarEntity + GearOffset + 40), 5);
                    G.FourthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FourthGear = CarEntity.PlayerCarEntity + GearOffset + 60), 5);
                    G.FifthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(FifthGear = CarEntity.PlayerCarEntity + GearOffset + 80), 5);
                    G.SixthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SixthGear = CarEntity.PlayerCarEntity + GearOffset + 100), 5);
                    G.SeventhGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(SeventhGear = CarEntity.PlayerCarEntity + GearOffset + 120), 5);
                    G.EighthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(EighthGear = CarEntity.PlayerCarEntity + GearOffset + 140), 5);
                    G.NinthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(NinthGear = CarEntity.PlayerCarEntity + GearOffset + 160), 5);
                    G.TenthGearBox.Value = Math.Round(Mw.M.ReadMemory<float>(TenthGear = CarEntity.PlayerCarEntity + GearOffset + 180), 5);
                }
                else
                {
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
                }
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
                    
                Others.O.WheelbaseBox.Value = Mw.M.ReadMemory<float>(Wheelbase);
                Others.O.RimSizeFrontBox.Value = Mw.M.ReadMemory<float>(RimSizeFront);
                Others.O.RimSizeRearBox.Value = Mw.M.ReadMemory<float>(RimSizeRear);
                Others.O.RimRadiusFrontBox.Value = Mw.M.ReadMemory<float>(RimRadiusFront);
                Others.O.RimRadiusRearBox.Value = Mw.M.ReadMemory<float>(RimRadiusRear);
                Others.O.FrontWidthBox.Value = Mw.M.ReadMemory<float>(FrontWidth);
                Others.O.RearWidthBox.Value = Mw.M.ReadMemory<float>(RearWidth);
                Others.O.FrontSpacerBox.Value = Mw.M.ReadMemory<float>(FrontSpacer);
                Others.O.RearSpacerBox.Value = Mw.M.ReadMemory<float>(RearSpacer);
                    
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

                if (Mw.Gvp.Name.Contains('5'))
                {
                    T.TireFrontLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontLeft = CarEntity.PlayerCarEntity + TireOffset) / TireFrontLeftDivider, 5);
                    T.TireFrontRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontRight = CarEntity.PlayerCarEntity + TireOffset + 2752) / TireFrontRightDivider, 5);
                    T.TireRearLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearLeft = CarEntity.PlayerCarEntity + TireOffset + 5504) / TireRearLeftDivider, 5);
                    T.TireRearRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearRight = CarEntity.PlayerCarEntity + TireOffset + 8256) / TireRearRightDivider, 5);
                }
                else
                {
                    T.TireFrontLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontLeft) / TireFrontLeftDivider, 5);
                    T.TireFrontRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireFrontRight) / TireFrontRightDivider, 5);
                    T.TireRearLeftBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearLeft) / TireRearLeftDivider, 5);
                    T.TireRearRightBox.Value = Math.Round(Mw.M.ReadMemory<float>(TireRearRight) / TireRearRightDivider, 5);
                }
                #endregion
            });
        }
    }
}