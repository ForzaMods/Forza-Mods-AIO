using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.Self_Vehicle;
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

        public static IntPtr TuningCodeCave1;
        public static IntPtr TuningCodeCave2;
        public static IntPtr TuningCodeCave3;
        public static IntPtr TuningCodeCave4;

        private const string TireOffsetAob = "F3 0F ? ? ? ? ? ? 48 8D ? ? ? ? ? 48 8D ? ? ? ? ? E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 48 8D";

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

            #region FH5
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableHook1 = MainWindow.mw.m.ScanForSig("F3 41 0F 10 85 98 FA FF FF F3 41 0F 59 C4", 1).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

                TuningTableHook2 = (MainWindow.mw.m.ScanForSig("0F 84 ? ? ? ? 8B 81 ? ? ? ? 83 F8 ? 74", 1).FirstOrDefault() + 1130).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);
                
                TuningTableHook3 = MainWindow.mw.m.ScanForSig("48 8B ? 48 8B ? FF 92 ? ? ? ? 48 8B ? EB ? 49 8B", 1).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);

                TuningTableHook4 = MainWindow.mw.m.ScanForSig("F3 0F ? ? 44 88 ? ? ? 45 33", 1).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);
                
                Task.Run(() => ASM.FH5TuningAddressesHook());
            }
            #endregion

            #region FH4
            else if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                TuningTableHook1 = (MainWindow.mw.m.ScanForSig("0F 29 ? ? 33 F6 49 81 C7", 1).FirstOrDefault() + 21).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

                TuningTableHook2 = MainWindow.mw.m.ScanForSig("49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B", 1).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);

                TuningTableHook3 = (MainWindow.mw.m.ScanForSig("48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85", 1).FirstOrDefault() + 37).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);
                
                TuningTableHook4 = (MainWindow.mw.m.ScanForSig("80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00", 1).FirstOrDefault() + 24).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);
                
                Task.Run(() => ASM.FH4TuningAddressesHook());
            }
            #endregion
            
            double Value = 0;

            while (Value < 100)
            {
                Application.Current.Dispatcher.Invoke(() => Value = Tuning.TBM.AOBProgressBar.Value);
                Thread.Sleep(250);
            }
            
            UpdateUi.UpdateUI(true, Tuning.TBM);
            Task.Run(() => ReadValues());
        }

        public static void AddressesFH4()
        {
            TireFrontLeft = (TuningTableBase1Long + 0x1D9C).ToString("X");
            TireFrontRight = (TuningTableBase1Long + 0x337C).ToString("X");
            TireRearLeft = (TuningTableBase1Long + 0x495C).ToString("X");
            TireRearRight = (TuningTableBase1Long + 0x5F3C).ToString("X");
            
            FinalDrive = (TuningTableBase1Long + 0xC40).ToString("X");
            ReverseGear = (TuningTableBase1Long + 0xACC).ToString("X");
            FirstGear = (TuningTableBase1Long + 0xAE0).ToString("X");
            SecondGear = (TuningTableBase1Long + 0xAF4).ToString("X");
            ThirdGear = (TuningTableBase1Long + 0xB08).ToString("X");
            FourthGear = (TuningTableBase1Long + 0xB1C).ToString("X");
            FifthGear = (TuningTableBase1Long + 0xB30).ToString("X");
            SixthGear = (TuningTableBase1Long + 0xB44).ToString("X");
            SeventhGear = (TuningTableBase1Long + 0xB58).ToString("X");
            EighthGear = (TuningTableBase1Long + 0xB6C).ToString("X");
            NinthGear = (TuningTableBase1Long + 0xB80).ToString("X");
            TenthGear = (TuningTableBase1Long + 0xB94).ToString("X");
            
            RimSizeFront = (TuningTableBase2Long + 0x758).ToString("X");
            RimRadiusFront = (TuningTableBase2Long + 0x760).ToString("X");
            RimSizeRear = (TuningTableBase2Long + 0x75C).ToString("X");
            RimRadiusRear = (TuningTableBase2Long + 0x764).ToString("X");
            
            CamberNeg = (TuningTableBase3Long + 0x3E4).ToString("X");
            CamberPos = (TuningTableBase3Long + 0x3E8).ToString("X");
            ToeNeg = (TuningTableBase3Long + 0x3EC).ToString("X");
            ToePos = (TuningTableBase3Long + 0x3F0).ToString("X");

            FrontAntirollMin = (TuningTableBase4Long + 0x3F8).ToString("X");
            FrontAntirollMax = (TuningTableBase4Long + 0x3FC).ToString("X");
            RearAntirollMin = (TuningTableBase4Long + 0x4A4).ToString("X");
            RearAntirollMax = (TuningTableBase4Long + 0x4A8).ToString("X");

            SpringFrontMin = (TuningTableBase4Long + 0x3AC).ToString("X");
            SpringFrontMax = (TuningTableBase4Long + 0x3B0).ToString("X");
            SpringRearMin = (TuningTableBase4Long + 0x458).ToString("X");
            SpringRearMax = (TuningTableBase4Long + 0x45C).ToString("X");

            FrontRideHeightMin = (TuningTableBase4Long + 0x394).ToString("X");
            FrontRideHeightMax = (TuningTableBase4Long + 0x398).ToString("X");
            RearRideHeightMin = (TuningTableBase4Long + 0x440).ToString("X");
            RearRideHeightMax = (TuningTableBase4Long + 0x444).ToString("X");

            FrontRestriction = (TuningTableBase4Long + 0x39C).ToString("X");
            RearRestriction = (TuningTableBase4Long + 0x448).ToString("X");

            FrontAeroMin = (TuningTableBase4Long + 0x234).ToString("X");
            FrontAeroMax = (TuningTableBase4Long + 0x23C).ToString("X");
            RearAeroMin = (TuningTableBase4Long + 0x294).ToString("X");
            RearAeroMax = (TuningTableBase4Long + 0x29C).ToString("X");

            FrontReboundStiffnessMin = (TuningTableBase4Long + 0x3D4).ToString("X");
            FrontReboundStiffnessMax = (TuningTableBase4Long + 0x3D8).ToString("X");
            RearReboundStiffnessMin = (TuningTableBase4Long + 0x480).ToString("X");
            RearReboundStiffnessMax = (TuningTableBase4Long + 0x484).ToString("X");

            FrontBumpStiffnessMin = (TuningTableBase4Long + 0x3B8).ToString("X");
            FrontBumpStiffnessMax = (TuningTableBase4Long + 0x3BC).ToString("X");
            RearBumpStiffnessMin = (TuningTableBase4Long + 0x464).ToString("X");
            RearBumpStiffnessMax = (TuningTableBase4Long + 0x468).ToString("X");

            Wheelbase = (TuningTableBase4Long + 0xC0).ToString("X");
            FrontWidth = (TuningTableBase4Long + 0xC4).ToString("X");
            RearWidth = (TuningTableBase4Long + 0xC8).ToString("X");
            FrontSpacer = (TuningTableBase4Long + 0x610).ToString("X");
            RearSpacer = (TuningTableBase4Long + 0x614).ToString("X");

            AngleMax = (TuningTableBase4Long + 0x534).ToString("X");
            AngleMax2 = (TuningTableBase4Long + 0x538).ToString("X");
            VelocityStraight = (TuningTableBase4Long + 0x53C).ToString("X");
            VelocityTurning = (TuningTableBase4Long + 0x540).ToString("X");
            VelocityCountersteer = (TuningTableBase4Long + 0x544).ToString("X");
            VelocityDynamicPeek = (TuningTableBase4Long + 0x548).ToString("X");
            TimeToMaxSteering = (TuningTableBase4Long + 0x54C).ToString("X");
        }


        private static bool AddressesFirstTime = true;
        private static int TireOffset;
        private static int GearingOffset;
        private static int FinalDriveOffset;
        private static int AeroOffset;
        private static int RimsOffset;
        private static int AlignmentOffset;
        
        public static void AddressesFH5()
        {
            // made this so I dont have to update the offset each update
            // in the place where Im adding something to ***Offset it doesnt change in any update from my test so it should be fine
            // probably the stupidest thing I've ever done but it works so I dont care
            
            if (AddressesFirstTime)
            {
                var GetTireOffset = MainWindow.mw.m.ScanForSig(TireOffsetAob, 1).FirstOrDefault() + 4;
                TireOffset = MainWindow.mw.m.ReadMemory<int>(GetTireOffset);

                var GetGearingOffset = MainWindow.mw.m.ScanForSig("48 8D ? ? F3 0F ? ? ? ? ? ? ? 0F 2F ? 73", 1).FirstOrDefault() + 9;
                GearingOffset = MainWindow.mw.m.ReadMemory<int>(GetGearingOffset);

                var GetFinalDriveOffset = MainWindow.mw.m.ScanForSig("F3 0F ? ? ? ? ? ? F3 0F ? ? 48 8B ? ? F3 0F ? ? 48 8B ? F3 0F", 1).FirstOrDefault() + 4;
                FinalDriveOffset = MainWindow.mw.m.ReadMemory<int>(GetFinalDriveOffset);

                var GetAeroOffset = MainWindow.mw.m.ScanForSig("41 8B ? ? ? ? ? 89 42 ? C7 42 14 ? ? ? ? C3 CC CC CC CC CC CC 48 8B", 1).FirstOrDefault() - 0x21;
                AeroOffset = MainWindow.mw.m.ReadMemory<int>(GetAeroOffset);

                var GetRimsOffset = MainWindow.mw.m.ScanForSig("F3 0F ? ? ? ? ? ? EB ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? 8B D7", 1).FirstOrDefault() + 4;
                RimsOffset = MainWindow.mw.m.ReadMemory<int>(GetRimsOffset);

                var GetAlignmentOffset = MainWindow.mw.m.ScanForSig("48 8B ? ? ? ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? F3 0F ? ? 48 8B").FirstOrDefault() + 19;
                AlignmentOffset = MainWindow.mw.m.ReadMemory<int>(GetAlignmentOffset);
                
                AddressesFirstTime = false;
            }
            
            TireFrontLeft = (TuningTableBase1Long + TireOffset).ToString("X");
            TireFrontRight = (TuningTableBase1Long + TireOffset + 2752).ToString("X");
            TireRearRight = (TuningTableBase1Long + TireOffset + 5504).ToString("X");
            TireRearLeft = (TuningTableBase1Long + TireOffset + 8256).ToString("X");

            FinalDrive = (TuningTableBase1Long + FinalDriveOffset).ToString("X");
            ReverseGear = (TuningTableBase1Long + GearingOffset - 20).ToString("X");
            FirstGear = (TuningTableBase1Long + GearingOffset).ToString("X");
            SecondGear = (TuningTableBase1Long + GearingOffset + 20).ToString("X");
            ThirdGear = (TuningTableBase1Long + GearingOffset + 40).ToString("X");
            FourthGear = (TuningTableBase1Long + GearingOffset + 60).ToString("X");
            FifthGear = (TuningTableBase1Long + GearingOffset + 80).ToString("X");
            SixthGear = (TuningTableBase1Long + GearingOffset + 100).ToString("X");
            SeventhGear = (TuningTableBase1Long + GearingOffset + 120).ToString("X");
            EighthGear = (TuningTableBase1Long + GearingOffset + 140).ToString("X");
            NinthGear = (TuningTableBase1Long + GearingOffset + 160).ToString("X");
            TenthGear = (TuningTableBase1Long + GearingOffset + 180).ToString("X");

            FrontAeroMin = (TuningTableBase2Long + AeroOffset).ToString("X");
            FrontAeroMax = (TuningTableBase2Long + AeroOffset + 8).ToString("X");
            RearAeroMin = (TuningTableBase2Long + AeroOffset + 96).ToString("X");
            RearAeroMax = (TuningTableBase2Long + AeroOffset + 104).ToString("X");

            AngleMax = (TuningTableBase2Long + AeroOffset + 1156).ToString("X");
            AngleMax2 = (TuningTableBase2Long + AeroOffset + 1160).ToString("X");
            VelocityStraight = (TuningTableBase2Long + AeroOffset + 1164).ToString("X");
            VelocityTurning = (TuningTableBase2Long + AeroOffset + 1168).ToString("X");
            VelocityCountersteer = (TuningTableBase2Long + AeroOffset + 1172).ToString("X");
            VelocityDynamicPeek = (TuningTableBase2Long + AeroOffset + 1176).ToString("X");
            TimeToMaxSteering = (TuningTableBase2Long + AeroOffset + 1180).ToString("X");

            Wheelbase = (TuningTableBase2Long + AeroOffset - 720).ToString("X");
            FrontWidth = (TuningTableBase2Long + AeroOffset - 716).ToString("X");
            RearWidth = (TuningTableBase2Long + AeroOffset - 712).ToString("X");
            FrontSpacer = (TuningTableBase2Long + AeroOffset + 1624).ToString("X");
            RearSpacer = (TuningTableBase2Long + AeroOffset + 1628).ToString("X");

            SpringFrontMin = (TuningTableBase2Long + AeroOffset + 432).ToString("X");
            SpringFrontMax = (TuningTableBase2Long + AeroOffset + 772).ToString("X");
            SpringRearMin = (TuningTableBase2Long + AeroOffset + 436).ToString("X");
            SpringRearMax = (TuningTableBase2Long + AeroOffset + 776).ToString("X");

            FrontRideHeightMin = (TuningTableBase2Long + AeroOffset + 400).ToString("X");
            FrontRideHeightMax = (TuningTableBase2Long + AeroOffset + 404).ToString("X");
            RearRideHeightMin = (TuningTableBase2Long + AeroOffset + 740).ToString("X");
            RearRideHeightMax = (TuningTableBase2Long + AeroOffset + 744).ToString("X");
            
            FrontRestriction = (TuningTableBase2Long + AeroOffset + 408).ToString("X");
            RearRestriction = (TuningTableBase2Long + AeroOffset + 748).ToString("X");

            FrontBumpStiffnessMin = (TuningTableBase2Long + AeroOffset + 444).ToString("X");
            FrontBumpStiffnessMax = (TuningTableBase2Long + AeroOffset + 448).ToString("X");
            RearBumpStiffnessMin = (TuningTableBase2Long + AeroOffset + 784).ToString("X");
            RearBumpStiffnessMax = (TuningTableBase2Long + AeroOffset + 788).ToString("X");

            FrontReboundStiffnessMin = (TuningTableBase2Long + AeroOffset + 480).ToString("X");
            FrontReboundStiffnessMax = (TuningTableBase2Long + AeroOffset + 484).ToString("X");
            RearReboundStiffnessMin = (TuningTableBase2Long + AeroOffset + 820).ToString("X");
            RearReboundStiffnessMax = (TuningTableBase2Long + AeroOffset + 824).ToString("X");

            FrontAntirollMin = (TuningTableBase2Long + AeroOffset + 588).ToString("X");
            FrontAntirollMax = (TuningTableBase2Long + AeroOffset + 592).ToString("X");
            RearAntirollMin = (TuningTableBase2Long + AeroOffset + 928).ToString("X");
            RearAntirollMax = (TuningTableBase2Long + AeroOffset + 932).ToString("X");

            RimSizeFront = (TuningTableBase3Long + RimsOffset).ToString("X");
            RimSizeRear = (TuningTableBase3Long + RimsOffset + 4).ToString("X");
            RimRadiusFront = (TuningTableBase3Long + RimsOffset + 8).ToString("X");
            RimRadiusRear = (TuningTableBase3Long + RimsOffset + 12).ToString("X");

            CamberNeg = (TuningTableBase4Long + AlignmentOffset).ToString("X");
            CamberPos = (TuningTableBase4Long + AlignmentOffset + 4).ToString("X");
            ToeNeg = (TuningTableBase4Long + AlignmentOffset + 8).ToString("X");
            ToePos = (TuningTableBase4Long + AlignmentOffset + 12).ToString("X");
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
