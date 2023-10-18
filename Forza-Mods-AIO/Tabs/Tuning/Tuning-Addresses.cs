using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Tuning.DropDownTabs;

namespace Forza_Mods_AIO.Tabs.Tuning
{
    internal class Tuning_Addresses
    {
        #region Vars
        #region Tires
        public static string TireFrontLeft;
        public static string TireFrontRight;
        public static string TireRearLeft;
        public static string TireRearRight;
        #endregion
        #region Gearing
        public static string FinalDrive;
        public static string ReverseGear;
        public static string FirstGear;
        public static string SecondGear;
        public static string ThirdGear;
        public static string FourthGear;
        public static string FifthGear;
        public static string SixthGear;
        public static string SeventhGear;
        public static string EighthGear;
        public static string NinthGear;
        public static string TenthGear;
        #endregion
        #region Alignment
        // Bases
        public static long AlignmentBase;

        // Camber
        public static string CamberPos;
        public static string CamberPosStatic;
        public static string CamberNeg;
        public static string CamberNegStatic;

        // Toe
        public static string ToePos;
        public static string ToePosStatic;
        public static string ToeNeg;
        public static string ToeNegStatic;
        #endregion
        #region Springs
        // Springs
        public static string SpringFrontMin;
        public static string SpringFrontMax;
        public static string SpringRearMin;
        public static string SpringRearMax;

        // Ride Height
        public static string FrontRideHeightMin;
        public static string FrontRideHeightMax;
        public static string RearRideHeightMin;
        public static string RearRideHeightMax;

        // Restrictions
        public static string FrontRestriction;
        public static string RearRestriction;
        #endregion
        #region Damping/Antiroll Bars
        // Antiroll Bars
        public static string FrontAntirollMin;
        public static string FrontAntirollMax;
        public static string RearAntirollMin;
        public static string RearAntirollMax;

        // Rebound Stiffness
        public static string FrontReboundStiffnessMin;
        public static string FrontReboundStiffnessMax;
        public static string RearReboundStiffnessMin;
        public static string RearReboundStiffnessMax;

        // Bump Stiffness
        public static string FrontBumpStiffnessMin;
        public static string FrontBumpStiffnessMax;
        public static string RearBumpStiffnessMin;
        public static string RearBumpStiffnessMax;
        #endregion
        #region Aero
        public static string FrontAeroMin;
        public static string FrontAeroMax;
        public static string RearAeroMin;
        public static string RearAeroMax;
        #endregion
        #region Steering
        // Max angle values
        public static string AngleMax;
        public static string AngleMax2;

        // Velocity / Time Values
        public static string VelocityStraight;
        public static string VelocityTurning;
        public static string VelocityCountersteer;
        public static string VelocityDynamicPeek;
        public static string TimeToMaxSteering;
        #endregion
        #region Others
        // Wheelbase
        public static string Wheelbase;
        public static string FrontWidth;
        public static string RearWidth;

        // Spacers
        public static string FrontSpacer;
        public static string RearSpacer;

        // Rims
        public static string RimSizeFront;
        public static string RimSizeRear;
        public static string RimRadiusFront;
        public static string RimRadiusRear;
        #endregion
        
        public static long TuningTableBase1Long;
        public static long TuningTableBase2Long;
        public static long TuningTableBase3Long;
        public static long TuningTableBase4Long;
        
        public static string TuningTableHook1;
        public static string TuningTableHook2;
        public static string TuningTableHook3;
        public static string TuningTableHook4;
        
        private const int ScanAmount = 5;

        #endregion

        public static void Scan()
        {
            AlignmentBase = (long)MainWindow.mw.m.ScanForSig("3d ? ? ? ? 00 00 ? ? 00 00 5c").LastOrDefault() + 0xD;
            
            CamberNegStatic = AlignmentBase.ToString("X");
            CamberPosStatic = (AlignmentBase + 0x4).ToString("X");
            ToeNegStatic = (AlignmentBase + 0x8).ToString("X");
            ToePosStatic = (AlignmentBase + 0xC).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 1, Tuning.TBM.AOBProgressBar);

            TuningTableHook1 = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (MainWindow.mw.m.ScanForSig("0F 29 ? ? 33 F6 49 81 C7", 1).FirstOrDefault() + 21).ToString("X") : MainWindow.mw.m.ScanForSig("F3 41 0F 10 85 98 FA FF FF F3 41 0F 59 C4", 1).FirstOrDefault().ToString("X");
            UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

            TuningTableHook2 = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? MainWindow.mw.m.ScanForSig("49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B", 1).FirstOrDefault().ToString("X") : (MainWindow.mw.m.ScanForSig("0F 84 ? ? ? ? 8B 81 ? ? ? ? 83 F8 ? 74", 1).FirstOrDefault() + 1130).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);

            TuningTableHook3 = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (MainWindow.mw.m.ScanForSig("48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85", 1).FirstOrDefault() + 37).ToString("X") : MainWindow.mw.m.ScanForSig("48 8B ? 48 8B ? FF 92 ? ? ? ? 48 8B ? EB ? 49 8B", 1).FirstOrDefault().ToString("X");
            UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);
                
            TuningTableHook4 = MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (MainWindow.mw.m.ScanForSig("80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00", 1).FirstOrDefault() + 24).ToString("X") : MainWindow.mw.m.ScanForSig("F3 0F ? ? 44 88 ? ? ? 45 33", 1).FirstOrDefault().ToString("X");
            UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);

            Tuning_ASM.GetTuningBaseAddresses();
            Task.Run(() => ReadValues());
            UpdateUi.UpdateUI(true, Tuning.TBM);
        }

        private const int AeroOffset = 0x3A0;
        private const int GearOffset = 0x10B8;
        private const int TireOffset = 0x2D58;
        
        public static void Addresses()
        {
            TireFrontLeft = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0x1D9C : TireOffset)).ToString("X");
            TireFrontRight = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0x337C : (TireOffset + 2752))).ToString("X");
            TireRearLeft = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0x495C : (TireOffset + 5504))).ToString("X");
            TireRearRight = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0x5F3C : (TireOffset + 8256))).ToString("X");
            
            FinalDrive = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xC40 : 0x125C)).ToString("X");
            ReverseGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xACC : (GearOffset - 20))).ToString("X");
            FirstGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xAE0 : GearOffset)).ToString("X");
            SecondGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xAF4 : (GearOffset + 20))).ToString("X");
            ThirdGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB08 : (GearOffset + 40))).ToString("X");
            FourthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB1C : (GearOffset + 60))).ToString("X");
            FifthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB30 : (GearOffset + 80))).ToString("X");
            SixthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB44 : (GearOffset + 100))).ToString("X");
            SeventhGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB58 : (GearOffset + 120))).ToString("X");
            EighthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB6C : (GearOffset + 140))).ToString("X");
            NinthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB80 : (GearOffset + 160))).ToString("X");
            TenthGear = (TuningTableBase1Long + (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 0xB94 : (GearOffset + 180))).ToString("X");
            
            RimSizeFront = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (TuningTableBase2Long + 0x758) : (TuningTableBase3Long + 0x7D8)).ToString("X");
            RimRadiusFront = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (TuningTableBase2Long + 0x760) : (TuningTableBase3Long + 0x7DC)).ToString("X");
            RimSizeRear = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (TuningTableBase2Long + 0x75C) : (TuningTableBase3Long + 0x7E0)).ToString("X");
            RimRadiusRear = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? (TuningTableBase2Long + 0x764) : (TuningTableBase3Long + 0x7E4)).ToString("X");
            
            CamberNeg = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase3Long + 0x3E4 : TuningTableBase4Long + 0x490).ToString("X");
            CamberPos = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase3Long + 0x3E8 : TuningTableBase4Long + 0x494).ToString("X");
            ToeNeg = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase3Long + 0x3EC : TuningTableBase4Long + 0x498).ToString("X");
            ToePos = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase3Long + 0x3F0 : TuningTableBase4Long + 0x49C).ToString("X");

            FrontAntirollMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3F8 : (TuningTableBase2Long + AeroOffset + 588)).ToString("X");
            FrontAntirollMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3FC : (TuningTableBase2Long + AeroOffset + 592)).ToString("X");
            RearAntirollMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x4A4 : (TuningTableBase2Long + AeroOffset + 928)).ToString("X");
            RearAntirollMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x4A8 : (TuningTableBase2Long + AeroOffset + 932)).ToString("X");

            SpringFrontMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3AC : (TuningTableBase2Long + AeroOffset + 432)).ToString("X");
            SpringFrontMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3B0 : (TuningTableBase2Long + AeroOffset + 772)).ToString("X");
            SpringRearMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x458 : (TuningTableBase2Long + AeroOffset + 436)).ToString("X");
            SpringRearMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x45C : (TuningTableBase2Long + AeroOffset + 776)).ToString("X");

            FrontRideHeightMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x394 : (TuningTableBase2Long + AeroOffset + 400)).ToString("X");
            FrontRideHeightMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x398 : (TuningTableBase2Long + AeroOffset + 404)).ToString("X");
            RearRideHeightMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x440 : (TuningTableBase2Long + AeroOffset + 740)).ToString("X");
            RearRideHeightMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x444 : (TuningTableBase2Long + AeroOffset + 744)).ToString("X");

            FrontRestriction = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x39C : (TuningTableBase2Long + AeroOffset + 408)).ToString("X");
            RearRestriction = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x448 : (TuningTableBase2Long + AeroOffset + 748)).ToString("X");

            FrontAeroMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x234 : (TuningTableBase2Long + AeroOffset)).ToString("X");
            FrontAeroMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x23C : (TuningTableBase2Long + AeroOffset + 8)).ToString("X");
            RearAeroMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x294 : (TuningTableBase2Long + AeroOffset + 96)).ToString("X");
            RearAeroMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x29C : (TuningTableBase2Long + AeroOffset + 104)).ToString("X");

            FrontReboundStiffnessMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3D4 : (TuningTableBase2Long + AeroOffset + 480)).ToString("X");
            FrontReboundStiffnessMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3D8 : (TuningTableBase2Long + AeroOffset + 484)).ToString("X");
            RearReboundStiffnessMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x480 : (TuningTableBase2Long + AeroOffset + 820)).ToString("X");
            RearReboundStiffnessMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x484 : (TuningTableBase2Long + AeroOffset + 824)).ToString("X");

            FrontBumpStiffnessMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3B8 : (TuningTableBase2Long + AeroOffset + 444)).ToString("X");
            FrontBumpStiffnessMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x3BC : (TuningTableBase2Long + AeroOffset + 448)).ToString("X");
            RearBumpStiffnessMin = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x464 : (TuningTableBase2Long + AeroOffset + 784)).ToString("X");
            RearBumpStiffnessMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x468 : (TuningTableBase2Long + AeroOffset + 788)).ToString("X");

            Wheelbase = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0xC0 : TuningTableBase2Long + 0xD0).ToString("X");
            FrontWidth = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0xC4 : TuningTableBase2Long + 0xD4).ToString("X");
            RearWidth = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0xC8 : TuningTableBase2Long + 0xD8).ToString("X");
            FrontSpacer = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x610 : TuningTableBase2Long + AeroOffset + 1624).ToString("X");
            RearSpacer = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x614 : TuningTableBase2Long + AeroOffset + 1628).ToString("X");

            AngleMax = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x534 : TuningTableBase2Long + AeroOffset + 1156).ToString("X");
            AngleMax2 = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x538 : TuningTableBase2Long + AeroOffset + 1160).ToString("X");
            VelocityStraight = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x53C : TuningTableBase2Long + AeroOffset + 1164).ToString("X");
            VelocityTurning = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x540 : TuningTableBase2Long + AeroOffset + 1168).ToString("X");
            VelocityCountersteer = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x544 : TuningTableBase2Long + AeroOffset + 1172).ToString("X");
            VelocityDynamicPeek = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x548 : TuningTableBase2Long + AeroOffset + 1176).ToString("X");
            TimeToMaxSteering = (MainWindow.mw.gvp.Name == "Forza Horizon 4" ? TuningTableBase4Long + 0x54C : TuningTableBase2Long + AeroOffset + 1180).ToString("X");
        }
        
        private static void ReadValues()
        {
            //These can be only read once as they dont change when a car is switched
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Alignment.al.CamberNegBox.Value = MainWindow.mw.m.ReadMemory<float>(CamberNegStatic);
                Alignment.al.CamberPosBox.Value = MainWindow.mw.m.ReadMemory<float>(CamberPosStatic);
                Alignment.al.ToeNegBox.Value = MainWindow.mw.m.ReadMemory<float>(ToeNegStatic);
                Alignment.al.ToePosBox.Value = MainWindow.mw.m.ReadMemory<float>(ToePosStatic);
            });

            //Rest requires a constant reading
            while (MainWindow.mw.Attached)
            {
                Thread.Sleep(500);
                
                if (MainWindow.mw.PageFocused != "Tuning")
                    continue;
                
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    /* not finished, cba for now
                       also nested as fuck I dont like
                       might just do a for (int i = 1; i < visuals.count; ++i) loop and some visual variables idk
                    foreach (FrameworkElement Element in MainWindow.mw.GetChildren())
                    {
                        foreach (var Field in typeof(Tuning_Addresses).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
                        {
                            if (Field.Name.Contains("Tire"))
                                Element.GetType()!.GetProperty("Value")!.SetValue(Element, (float)Math.Round(MainWindow.mw.m.ReadMemory<float>((Field.GetValue(Field) as string)), 3));
                            else
                                Element.GetType()!.GetProperty("Value")!.SetValue(Element, (float)Math.Round(MainWindow.mw.m.ReadMemory<float>((Field.GetValue(Field) as string)), 3));

                        }
                    }*/
                    
                    #region Aero

                        Aero.ae.FrontAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontAeroMin), 3);
                        Aero.ae.FrontAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontAeroMax), 3);
                        Aero.ae.RearAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearAeroMin), 3);
                        Aero.ae.RearAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearAeroMax), 3);
                    
                    #endregion
                    #region Gearing

                        Gearing.g.FinalDriveBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FinalDrive), 5);
                        Gearing.g.ReverseGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(ReverseGear), 5);
                        Gearing.g.FirstGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FirstGear), 5);
                        Gearing.g.SecondGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SecondGear), 5);
                        Gearing.g.ThirdGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(ThirdGear), 5);
                        Gearing.g.FourthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FourthGear), 5);
                        Gearing.g.FifthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FifthGear), 5);
                        Gearing.g.SixthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SixthGear), 5);
                        Gearing.g.SeventhGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SeventhGear), 5);
                        Gearing.g.EighthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(EighthGear), 5);
                        Gearing.g.NinthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(NinthGear), 5);
                        Gearing.g.TenthGearBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TenthGear), 5);
                    
                    #endregion
                    #region Damping
                    
                        Damping.d.FrontAntirollMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontAntirollMin), 5);
                        Damping.d.FrontAntirollMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontAntirollMax), 5);
                        Damping.d.RearAntirollMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearAntirollMin), 5);
                        Damping.d.RearAntirollMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearAntirollMax), 5);

                        Damping.d.FrontBumpStiffnessMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontBumpStiffnessMin), 5);
                        Damping.d.FrontBumpStiffnessMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontBumpStiffnessMax), 5);
                        Damping.d.RearBumpStiffnessMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearBumpStiffnessMin), 5);
                        Damping.d.RearBumpStiffnessMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearBumpStiffnessMax), 5);

                        Damping.d.FrontReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadMemory<float>(FrontReboundStiffnessMin);
                        Damping.d.FrontReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadMemory<float>(FrontReboundStiffnessMax);
                        Damping.d.RearReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadMemory<float>(RearReboundStiffnessMin);
                        Damping.d.RearReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadMemory<float>(RearReboundStiffnessMax);
                    
                    #endregion
                    #region Others
                    
                        Others.o.WheelbaseBox.Value = MainWindow.mw.m.ReadMemory<float>(Wheelbase);
                        Others.o.RimSizeFrontBox.Value = MainWindow.mw.m.ReadMemory<float>(RimSizeFront);
                        Others.o.RimSizeRearBox.Value = MainWindow.mw.m.ReadMemory<float>(RimSizeRear);
                        Others.o.RimRadiusFrontBox.Value = MainWindow.mw.m.ReadMemory<float>(RimRadiusFront);
                        Others.o.RimRadiusRearBox.Value = MainWindow.mw.m.ReadMemory<float>(RimRadiusRear);
                        Others.o.FrontWidthBox.Value = MainWindow.mw.m.ReadMemory<float>(FrontWidth);
                        Others.o.RearWidthBox.Value = MainWindow.mw.m.ReadMemory<float>(RearWidth);
                        Others.o.FrontSpacerBox.Value = MainWindow.mw.m.ReadMemory<float>(FrontSpacer);
                        Others.o.RearSpacerBox.Value = MainWindow.mw.m.ReadMemory<float>(RearSpacer);
                    
                    #endregion
                    #region Springs
                    
                        Springs.sp.SpringFrontMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SpringFrontMin), 3);
                        Springs.sp.SpringFrontMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SpringFrontMax), 3);
                        Springs.sp.SpringRearMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SpringRearMin), 3);
                        Springs.sp.SpringRearMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(SpringRearMax), 3);

                        Springs.sp.FrontRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontRideHeightMin), 6);
                        Springs.sp.FrontRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(FrontRideHeightMax), 6);
                        Springs.sp.RearRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearRideHeightMin), 6);
                        Springs.sp.RearRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(RearRideHeightMax), 6);
                    
                    #endregion
                    #region Steering
                    
                        Steering.st.AngleMaxBox.Value = MainWindow.mw.m.ReadMemory<float>(AngleMax);
                        Steering.st.AngleMax2Box.Value = MainWindow.mw.m.ReadMemory<float>(AngleMax2);
                        Steering.st.VelocityCountersteerBox.Value = MainWindow.mw.m.ReadMemory<float>(VelocityCountersteer);
                        Steering.st.VelocityDynamicPeekBox.Value = MainWindow.mw.m.ReadMemory<float>(VelocityDynamicPeek);
                        Steering.st.VelocityStraightBox.Value = MainWindow.mw.m.ReadMemory<float>(VelocityStraight);
                        Steering.st.VelocityTurningBox.Value = MainWindow.mw.m.ReadMemory<float>(VelocityTurning);
                        Steering.st.TimeToMaxSteeringBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TimeToMaxSteering), 5);
                    
                    #endregion
                    #region Tires
                    
                        Tires.t.TireFrontLeftBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TireFrontLeft) / Tires.TireFrontLeftDivider, 5);
                        Tires.t.TireFrontRightBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TireFrontRight) / Tires.TireFrontRightDivider, 5);
                        Tires.t.TireRearLeftBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TireRearLeft) / Tires.TireRearLeftDivider, 5);
                        Tires.t.TireRearRightBox.Value = Math.Round(MainWindow.mw.m.ReadMemory<float>(TireRearRight) / Tires.TireRearRightDivider, 5);
                    
                    #endregion
                });
            }
        }
    }
}
