using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
        public static long Base;

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

        public static string TuningTableBase1 = "0";
        public static string TuningTableBase2 = "0";
        public static string TuningTableBase3 = "0";
        public static string TuningTableBase4 = "0";
        public static long TuningTableHookBase1;
        public static long TuningTableHookBase2;
        public static long TuningTableHookBase3;
        public static long TuningTableBase1FH4 = 0;
        public static long TuningTableBase2FH4 = 0;
        public static long TuningTableBase3FH4 = 0;
        private static int ScanAmount = 15;
        #endregion

        public static async Task Scan()
        {
            long ScanStart = MainWindow.mw.gvp.Process.MainModule!.BaseAddress;
            long ScanEnd = ScanStart + MainWindow.mw.gvp.Process.MainModule!.ModuleMemorySize;

            Base = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "3d ? ? ? ? 00 00 ? ? 00 00 5c", true, true, false)).LastOrDefault() + 0xD;
            
            CamberNegStatic = Base.ToString("X");
            CamberPosStatic = (Base + 0x4).ToString("X");
            ToeNegStatic = (Base + 0x8).ToString("X");
            ToePosStatic = (Base + 0xC).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 1, Tuning.TBM.AOBProgressBar);

            #region FH5
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                ScanAmount = 15;
                List<bool> ScanBools = new();

                Task.Run(async () => { TuningTableBase1 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault() - 6).ToString("X"); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase2 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 00 00 00 00 E8 ? ? ? ? 6D 9D", true, true, false)).FirstOrDefault() + 0x100).ToString("X"); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase3 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 FF FF FF FF 10 ? ? ? ? ? 00 00 00 ? ? ? ? ? 00 00 ? ? ? ? ? ? 00 00 00", true, true, false)).FirstOrDefault() + 0x8).ToString("X"); ScanBools.Add(true); });
                Task.Run(async () => { TuningTableBase4 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? ? ? 00 00 00 80", true, true, false)).FirstOrDefault().ToString("X"); ScanBools.Add(true); });

                while (ScanBools.Count < 4)
                    continue;
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
                ScanAmount = 3;
                
                TuningTableHookBase1 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "0F 29 ? ? 33 F6 49 81 C7", true, true, true)).FirstOrDefault() + 21;
                var CCBA = MainWindow.mw.gvp.Process.MainModule.BaseAddress;
                var CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave == 0)
                {
                    CCBA += 500000;
                    CodeCave = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }


                UpdateUi.AddProgress(ScanAmount, 1, Tuning.TBM.AOBProgressBar);

                TuningTableHookBase2 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "49 8B ? E8 ? ? ? ? 84 C0 0F 85 ? ? ? ? 41 38", true, true, true)).FirstOrDefault() + 30;
                var CCBA2 = CCBA;
                var CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave2 == 0)
                {
                    CCBA2 += 500000;
                    CodeCave2 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);

                TuningTableHookBase3 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "48 8D ? ? ? 0F 29 ? ? ? 0F 28 ? E8 ? ? ? ? 48 85", true, true, true)).FirstOrDefault() + 37;
                var CCBA3 = CCBA2;
                var CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                while (CodeCave3 == 0)
                {
                    CCBA3 += 500000;
                    CodeCave3 = assembly.VirtualAllocEx(MainWindow.mw.gvp.Process.Handle, CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                }

                UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);

                Task.Run(() => assembly.FH4TuningAddressesHook(CodeCave, CodeCave2, CodeCave3));
            }
            #endregion


            double Value = 0;

            while (Value <= 95)
            {
                Application.Current.Dispatcher.Invoke(() => Value = Tuning.TBM.AOBProgressBar.Value);
                continue;
            }
            
            UpdateUi.UpdateUI(true, Tuning.TBM);
            ReadValues();
        }

        public static void AddressesFH4()
        {
            TireFrontLeft = (TuningTableBase1FH4 + 0x1D9C).ToString("X");
            TireFrontRight = (TuningTableBase1FH4 + 0x337C).ToString("X");
            TireRearLeft = (TuningTableBase1FH4 + 0x495C).ToString("X");
            TireRearRight = (TuningTableBase1FH4 + 0x5F3C).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 2, Tuning.TBM.AOBProgressBar);
            
            FinalDrive = (TuningTableBase1FH4 + 0xC40).ToString("X");
            ReverseGear = (TuningTableBase1FH4 + 0xACC).ToString("X");
            FirstGear = (TuningTableBase1FH4 + 0xAE0).ToString("X");
            SecondGear = (TuningTableBase1FH4 + 0xAF4).ToString("X");
            ThirdGear = (TuningTableBase1FH4 + 0xB08).ToString("X");
            FourthGear = (TuningTableBase1FH4 + 0xB1C).ToString("X");
            FifthGear = (TuningTableBase1FH4 + 0xB30).ToString("X");
            SixthGear = (TuningTableBase1FH4 + 0xB44).ToString("X");
            SeventhGear = (TuningTableBase1FH4 + 0xB58).ToString("X");
            EighthGear = (TuningTableBase1FH4 + 0xB6C).ToString("X");
            NinthGear = (TuningTableBase1FH4 + 0xB80).ToString("X");
            TenthGear = (TuningTableBase1FH4 + 0xB94).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 3, Tuning.TBM.AOBProgressBar);
            
            RimSizeFront = (TuningTableBase2FH4 + 0x758).ToString("X");
            RimRadiusFront = (TuningTableBase2FH4 + 0x760).ToString("X");
            RimRadiusRear = (TuningTableBase2FH4 + 0x75C).ToString("X");
            RimRadiusRear = (TuningTableBase2FH4 + 0x764).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 4, Tuning.TBM.AOBProgressBar);
            
            CamberNeg = (TuningTableBase3FH4 + 0x3E4).ToString("X");
            CamberPos = (TuningTableBase3FH4 + 0x3E8).ToString("X");
            ToeNeg = (TuningTableBase3FH4 + 0x3EC).ToString("X");
            ToePos = (TuningTableBase3FH4 + 0x3F0).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 5, Tuning.TBM.AOBProgressBar);
        }
        
        private static void ReadValues()
        {
            //These can be only read once as they dont change when a car is switched
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Alignment.al.CamberNegBox.Value = MainWindow.mw.m.ReadFloat(CamberNegStatic);
                Alignment.al.CamberPosBox.Value = MainWindow.mw.m.ReadFloat(CamberPosStatic);
                Alignment.al.ToeNegBox.Value = MainWindow.mw.m.ReadFloat(ToeNegStatic);
                Alignment.al.ToePosBox.Value = MainWindow.mw.m.ReadFloat(ToePosStatic);
            });

            //Rest requires a constant reading
            while (MainWindow.mw.Attached)
            {
                Thread.Sleep(5);
                
                Application.Current.Dispatcher.Invoke((Action)delegate ()
                {
                    #region Aero

                        Aero.ae.FrontAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAeroMin, round: false), 3);
                        Aero.ae.FrontAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAeroMax, round: false), 3);
                        Aero.ae.RearAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAeroMin, round: false), 3);
                        Aero.ae.RearAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAeroMax, round: false), 3);
                    
                    #endregion
                    #region Gearing

                        Gearing.g.FinalDriveBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FinalDrive, round: false), 5);
                        Gearing.g.ReverseGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(ReverseGear, round: false), 5);
                        Gearing.g.FirstGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FirstGear, round: false), 5);
                        Gearing.g.SecondGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SecondGear, round: false), 5);
                        Gearing.g.ThirdGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(ThirdGear, round: false), 5);
                        Gearing.g.FourthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FourthGear, round: false), 5);
                        Gearing.g.FifthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FifthGear, round: false), 5);
                        Gearing.g.SixthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SixthGear, round: false), 5);
                        Gearing.g.SeventhGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SeventhGear, round: false), 5);
                        Gearing.g.EighthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(EighthGear, round: false), 5);
                        Gearing.g.NinthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(NinthGear, round: false), 5);
                        Gearing.g.TenthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TenthGear, round: false), 5);
                    
                    #endregion
                    #region Damping
                    
                        Damping.d.FrontAntirollMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAntirollMin, round: false), 5);
                        Damping.d.FrontAntirollMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAntirollMax, round: false), 5);
                        Damping.d.RearAntirollMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAntirollMin, round: false), 5);
                        Damping.d.RearAntirollMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAntirollMax, round: false), 5);

                        Damping.d.FrontBumpStiffnessMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontBumpStiffnessMin, round: false), 5);
                        Damping.d.FrontBumpStiffnessMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontBumpStiffnessMax, round: false), 5);
                        Damping.d.RearBumpStiffnessMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearBumpStiffnessMin, round: false), 5);
                        Damping.d.RearBumpStiffnessMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearBumpStiffnessMax, round: false), 5);

                        Damping.d.FrontReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(FrontReboundStiffnessMin, round: false);
                        Damping.d.FrontReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(FrontReboundStiffnessMax, round: false);
                        Damping.d.RearReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(RearReboundStiffnessMin, round: false);
                        Damping.d.RearReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(RearReboundStiffnessMax, round: false);
                    
                    #endregion
                    #region Others
                    
                        Others.o.WheelbaseBox.Value = MainWindow.mw.m.ReadFloat(Wheelbase, round: false);
                        Others.o.RimSizeFrontBox.Value = MainWindow.mw.m.ReadFloat(RimSizeFront, round: false);
                        Others.o.RimSizeRearBox.Value = MainWindow.mw.m.ReadFloat(RimSizeRear, round: false);
                        Others.o.RimRadiusFrontBox.Value = MainWindow.mw.m.ReadFloat(RimRadiusFront, round: false);
                        Others.o.RimRadiusRearBox.Value = MainWindow.mw.m.ReadFloat(RimRadiusRear, round: false);
                        Others.o.FrontWidthBox.Value = MainWindow.mw.m.ReadFloat(FrontWidth, round: false);
                        Others.o.RearWidthBox.Value = MainWindow.mw.m.ReadFloat(RearWidth, round: false);
                        Others.o.FrontSpacerBox.Value = MainWindow.mw.m.ReadFloat(FrontSpacer, round: false);
                        Others.o.RearSpacerBox.Value = MainWindow.mw.m.ReadFloat(RearSpacer, round: false);
                    
                    #endregion
                    #region Springs
                    
                        Springs.sp.SpringFrontMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringFrontMin, round: false), 3);
                        Springs.sp.SpringFrontMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringFrontMax, round: false), 3);
                        Springs.sp.SpringRearMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringRearMin, round: false), 3);
                        Springs.sp.SpringRearMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringRearMax, round: false), 3);

                        Springs.sp.FrontRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontRideHeightMin, round: false), 6);
                        Springs.sp.FrontRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontRideHeightMax, round: false), 6);
                        Springs.sp.RearRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearRideHeightMin, round: false), 6);
                        Springs.sp.RearRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearRideHeightMax, round: false), 6);
                    
                    #endregion
                    #region Steering
                    
                        Steering.st.AngleMaxBox.Value = MainWindow.mw.m.ReadFloat(AngleMax, round: false);
                        Steering.st.AngleMax2Box.Value = MainWindow.mw.m.ReadFloat(AngleMax2, round: false);
                        Steering.st.VelocityCountersteerBox.Value = MainWindow.mw.m.ReadFloat(VelocityCountersteer, round: false);
                        Steering.st.VelocityDynamicPeekBox.Value = MainWindow.mw.m.ReadFloat(VelocityDynamicPeek, round: false);
                        Steering.st.VelocityStraightBox.Value = MainWindow.mw.m.ReadFloat(VelocityStraight, round: false);
                        Steering.st.VelocityTurningBox.Value = MainWindow.mw.m.ReadFloat(VelocityTurning, round: false);
                        Steering.st.TimeToMaxSteeringBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TimeToMaxSteering, round: false), 5);
                    
                    #endregion
                    #region Tires
                    
                        Tires.t.TireFrontLeftBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireFrontLeft, round: false) / Tires.TireFrontLeftDivider, 5);
                        Tires.t.TireFrontRightBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireFrontRight, round: false) / Tires.TireFrontRightDivider, 5);
                        Tires.t.TireRearLeftBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireRearLeft, round: false) / Tires.TireRearLeftDivider, 5);
                        Tires.t.TireRearRightBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireRearRight, round: false) / Tires.TireRearRightDivider, 5);
                    
                    #endregion
                });
            }
        }
    }
}
