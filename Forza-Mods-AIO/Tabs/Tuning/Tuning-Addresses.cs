using System;
using System.Collections.Generic;
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

        private static string TuningTableBase1 = "0";
        private static string TuningTableBase2 = "0";
        private static string TuningTableBase3 = "0";
        private static string TuningTableBase4 = "0";
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
        
        private static int ScanAmount = 15;
        #endregion

        public static void Scan()
        {
            long ScanStart = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
            long ScanEnd = ScanStart + MainWindow.mw.gvp.Process.MainModule!.ModuleMemorySize;

            AlignmentBase = (long)MainWindow.mw.m.ScanForSig("3d ? ? ? ? 00 00 ? ? 00 00 5c").LastOrDefault() + 0xD;
            
            CamberNegStatic = AlignmentBase.ToString("X");
            CamberPosStatic = (AlignmentBase + 0x4).ToString("X");
            ToeNegStatic = (AlignmentBase + 0x8).ToString("X");
            ToePosStatic = (AlignmentBase + 0xC).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 1, Tuning.TBM.AOBProgressBar);

            #region FH5
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                ScanAmount = 15;
                List<bool> ScanBools = new();

                Task.Run(async () => { TuningTableBase1Long = (long)((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault() - 6); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase2Long = (long)((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 00 00 00 00 E8 ? ? ? ? 6D 9D", true, true, false)).FirstOrDefault() + 0x100); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase3Long = (long)((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 FF FF FF FF 10 ? ? ? ? ? 00 00 00 ? ? ? ? ? 00 00 ? ? ? ? ? ? 00 00 00", true, true, false)).FirstOrDefault() + 0x8); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase4Long = (long)(await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? ? ? 00 00 00 80", true, true, false)).FirstOrDefault(); ScanBools.Add(true); });

                while (ScanBools.Count < 4)
                    Thread.Sleep(50);
                
                TuningTableBase1 = TuningTableBase1Long.ToString("X");
                TuningTableBase2 = TuningTableBase2Long.ToString("X");
                TuningTableBase3 = TuningTableBase3Long.ToString("X");
                TuningTableBase4 = TuningTableBase4Long.ToString("X");

                
                UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

                TireFrontLeft = (TuningTableBase1 + ",0x10,0x10,0x27E8");
                TireFrontRight = (TuningTableBase1 + ",0x10,0x10,0x32A8");
                TireRearRight = (TuningTableBase1 + ",0x10,0x10,0x3D68");
                TireRearLeft = (TuningTableBase1 + ",0x10,0x10,0x4828");
                UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);

                FinalDrive = (TuningTableBase1 + ",0x10,0x10,0xCEC");
                ReverseGear = (TuningTableBase1 + ",0x10,0x10,0xB48");
                FirstGear = (TuningTableBase1 + ",0x10,0x10,0xB5C");
                SecondGear = (TuningTableBase1 + ",0x10,0x10,0xB70");
                ThirdGear = (TuningTableBase1 + ",0x10,0x10,0xB84");
                FourthGear = (TuningTableBase1 + ",0x10,0x10,0xB98");
                FifthGear = (TuningTableBase1 + ",0x10,0x10,0xBAC");
                SixthGear = (TuningTableBase1 + ",0x10,0x10,0xBC0");
                SeventhGear = (TuningTableBase1 + ",0x10,0x10,0xBD4");
                EighthGear = (TuningTableBase1 + ",0x10,0x10,0xBE8");
                NinthGear = (TuningTableBase1 + ",0x10,0x10,0xBFC");
                TenthGear = (TuningTableBase1 + ",0x10,0x10,0xC10");
                UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x490");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x494");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x498");
                ToePos = (TuningTableBase2 + ",0x8B0,0x49C");
                UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);

                SpringFrontMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x550");
                SpringFrontMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x554");
                SpringRearMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6A4");
                SpringRearMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6A8");
                UpdateUi.AddProgress(ScanAmount, 6, Tuning.TBM.AOBProgressBar);

                FrontRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x530");
                FrontRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x534");
                RearRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x684");
                RearRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x688");
                UpdateUi.AddProgress(ScanAmount, 7, Tuning.TBM.AOBProgressBar);

                FrontRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x538");
                RearRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x68C");
                UpdateUi.AddProgress(ScanAmount, 8, Tuning.TBM.AOBProgressBar);

                FrontAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5EC");
                FrontAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5F0");
                RearAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x740");
                RearAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x744");
                UpdateUi.AddProgress(ScanAmount, 9, Tuning.TBM.AOBProgressBar);

                FrontReboundStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x580");
                FrontReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x584");
                RearReboundStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6D4");
                RearReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6D8");
                UpdateUi.AddProgress(ScanAmount, 10, Tuning.TBM.AOBProgressBar);

                FrontBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x55C");
                FrontBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x560");
                RearBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6B0");
                RearBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6B4");
                UpdateUi.AddProgress(ScanAmount, 11, Tuning.TBM.AOBProgressBar);

                FrontAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3A0");
                FrontAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3A8");
                RearAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x400");
                RearAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x408");
                UpdateUi.AddProgress(ScanAmount, 12, Tuning.TBM.AOBProgressBar);

                AngleMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x824");
                AngleMax2 = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x828");
                VelocityStraight = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x82C");
                VelocityTurning = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x830");
                VelocityCountersteer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x834");
                VelocityDynamicPeek = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x838");
                TimeToMaxSteering = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x83C");
                UpdateUi.AddProgress(ScanAmount, 13, Tuning.TBM.AOBProgressBar);

                Wheelbase = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD0");
                FrontWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD4");
                RearWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD8");
                FrontSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9F8");
                RearSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9FC");
                UpdateUi.AddProgress(ScanAmount, 14, Tuning.TBM.AOBProgressBar);

                RimSizeFront = (TuningTableBase4 + ",0x150,0x300,0x7D8");
                RimSizeRear = (TuningTableBase4 + ",0x150,0x300,0x7DC");
                RimRadiusFront = (TuningTableBase4 + ",0x150,0x300,0x7E0");
                RimRadiusRear = (TuningTableBase4 + ",0x150,0x300,0x7E4");
                UpdateUi.AddProgress(ScanAmount, 15, Tuning.TBM.AOBProgressBar);
            }
            #endregion

            #region FH4
            else if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                ScanAmount = 5;
                
                TuningTableHook1 = (MainWindow.mw.m.ScanForSig("0F 29 ? ? 33 F6 49 81 C7", 1).FirstOrDefault() + 21).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

                TuningTableHook2 = MainWindow.mw.m.ScanForSig("49 8B ? 48 8D ? ? 49 8B ? FF 90 ? ? ? ? 44 0F ? ? 41 8B", 1).FirstOrDefault().ToString("X");;
                UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);

                TuningTableHook3 = (MainWindow.mw.m.ScanForSig("48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85", 1).FirstOrDefault() + 37).ToString("X");;
                UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);
                
                TuningTableHook4 = (MainWindow.mw.m.ScanForSig("80 78 39 ? 0F 84 ? ? ? ? 48 83 BF 50 87 00 00", 1).FirstOrDefault() + 24).ToString("X");;
                UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);
                
                Task.Run(() => ASM.FH4TuningAddressesHook());
            }
            #endregion
            
            double Value = 0;

            while (Value <= 95)
            {
                Application.Current.Dispatcher.Invoke(() => Value = Tuning.TBM.AOBProgressBar.Value);
                Thread.Sleep(50);
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
                Thread.Sleep(5);
                
                Application.Current.Dispatcher.Invoke((Action)delegate ()
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
