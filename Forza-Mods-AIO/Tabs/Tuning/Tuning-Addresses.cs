using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
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
        private static int ScanAmount = 15;
        #endregion

        public static async Task Scan()
        {
            #region Camber,toe (static) and scanning vars
            while (Base is 0 or 0xD)
                Base = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, 
                                                      MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize,
                                                      "3d ? ? ? ? 00 00 ? ? 00 00 5c", true, true, false)).FirstOrDefault() + 0xD;
            
            CamberNegStatic = Base.ToString("X");
            CamberPosStatic = (Base + 0x4).ToString("X");
            ToeNegStatic = (Base + 0x8).ToString("X");
            ToePosStatic = (Base + 0xC).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 1, TuningTableMain.TBM.AOBProgressBar);
            #endregion

            #region MS FH5
            if (MainWindow.mw.gvp.Plat == "MS" && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableBase1 = (Base - 430920).ToString("X");
                TuningTableBase2 = (Base - 539816).ToString("X");
                TuningTableBase3 = (Base - 594608).ToString("X");
                TuningTableBase4 = (Base - 4408).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 2, TuningTableMain.TBM.AOBProgressBar);

                TireFrontLeft = (TuningTableBase1 + ",0x10,0x10,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x10,0x10,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x10,0x10,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x10,0x10,0x4808");
                UpdateUi.AddProgress(ScanAmount, 3, TuningTableMain.TBM.AOBProgressBar);

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
                UpdateUi.AddProgress(ScanAmount, 4, TuningTableMain.TBM.AOBProgressBar);

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x490");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x494");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x498");
                ToePos = (TuningTableBase2 + ",0x8B0,0x49C");
                UpdateUi.AddProgress(ScanAmount, 5, TuningTableMain.TBM.AOBProgressBar);

                SpringFrontMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x528");
                SpringFrontMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x52C");
                SpringRearMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x67C");
                SpringRearMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x680");
                UpdateUi.AddProgress(ScanAmount, 6, TuningTableMain.TBM.AOBProgressBar);

                FrontRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x508");
                FrontRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x50C");
                RearRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x65C");
                RearRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x660");
                UpdateUi.AddProgress(ScanAmount, 7, TuningTableMain.TBM.AOBProgressBar);

                FrontRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x510");
                RearRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x664");
                UpdateUi.AddProgress(ScanAmount, 8, TuningTableMain.TBM.AOBProgressBar);

                FrontAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5C4");
                FrontAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5C8");
                RearAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x718");
                RearAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x71C");
                UpdateUi.AddProgress(ScanAmount, 9, TuningTableMain.TBM.AOBProgressBar);

                FrontReboundStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x558");
                FrontReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x55C");
                RearReboundStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6AC");
                RearReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x68C");
                UpdateUi.AddProgress(ScanAmount, 10, TuningTableMain.TBM.AOBProgressBar);

                FrontBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x534");
                FrontBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x538");
                RearBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x688");
                RearBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x68C");
                UpdateUi.AddProgress(ScanAmount, 11, TuningTableMain.TBM.AOBProgressBar);

                FrontAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x378");
                FrontAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x380");
                RearAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3D8");
                RearAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3E0");
                UpdateUi.AddProgress(ScanAmount, 12, TuningTableMain.TBM.AOBProgressBar);

                AngleMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x7FC");
                AngleMax2 = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x800");
                VelocityStraight = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x804");
                VelocityTurning = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x808");
                VelocityCountersteer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x80C");
                VelocityDynamicPeek = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x810");
                TimeToMaxSteering = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x814");
                UpdateUi.AddProgress(ScanAmount, 13, TuningTableMain.TBM.AOBProgressBar);

                Wheelbase = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD0");
                FrontWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD4");
                RearWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD8");
                FrontSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9D0");
                RearSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9D4");
                UpdateUi.AddProgress(ScanAmount, 14, TuningTableMain.TBM.AOBProgressBar);

                RimSizeFront = (TuningTableBase4 + ",0x150,0x300,0x7D8");
                RimSizeRear = (TuningTableBase4 + ",0x150,0x300,0x7DC");
                RimRadiusFront = (TuningTableBase4 + ",0x150,0x300,0x7E0");
                RimRadiusRear = (TuningTableBase4 + ",0x150,0x300,0x7E4");
            }
            #endregion
            #region Steam FH5
            else if (MainWindow.mw.gvp.Plat.Contains("Steam") && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                // some retarded calculations but they work so I dont care
                TuningTableBase1 = (Base - 540136 - 54680).ToString("X"); 
                TuningTableBase2 = (Base - 540136 + 256).ToString("X");
                TuningTableBase3 = (Base - 540136 + 535728).ToString("X");
                UpdateUi.AddProgress(ScanAmount, 2, TuningTableMain.TBM.AOBProgressBar);

                TireFrontLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x4808");
                UpdateUi.AddProgress(ScanAmount, 3, TuningTableMain.TBM.AOBProgressBar);

                FinalDrive = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xCEC");
                ReverseGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xB48");
                FirstGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xB5C");
                SecondGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xB70");
                ThirdGear = (TuningTableBase1 + "0x,320,0x10,0x1D0,0xB84");
                FourthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xB98");
                FifthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xBAC");
                SixthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xBC0");
                SeventhGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xBD4");
                EighthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xBE8");
                NinthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xBFC");
                TenthGear = (TuningTableBase1 + ",0x320,0x10,0x1D0,0xC10");
                UpdateUi.AddProgress(ScanAmount, 4, TuningTableMain.TBM.AOBProgressBar);

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x490");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x494");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x498");
                ToePos = (TuningTableBase2 + ",0x8B0,0x49C");
                UpdateUi.AddProgress(ScanAmount, 5, TuningTableMain.TBM.AOBProgressBar);

                SpringFrontMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x528");
                SpringFrontMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x52C");
                SpringRearMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x67C");
                SpringRearMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x680");
                UpdateUi.AddProgress(ScanAmount, 6, TuningTableMain.TBM.AOBProgressBar);

                FrontRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x508");
                FrontRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x50C");
                RearRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x65C");
                RearRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x660");
                UpdateUi.AddProgress(ScanAmount, 7, TuningTableMain.TBM.AOBProgressBar);

                FrontRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x510");
                RearRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x664");
                UpdateUi.AddProgress(ScanAmount, 8, TuningTableMain.TBM.AOBProgressBar);

                FrontAntirollMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x5C4");
                FrontAntirollMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x5C8");
                RearAntirollMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x718");
                RearAntirollMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x71C");
                UpdateUi.AddProgress(ScanAmount, 9, TuningTableMain.TBM.AOBProgressBar);

                FrontReboundStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x558");
                FrontReboundStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x55C");
                RearReboundStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x6AC");
                RearReboundStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x6B0");
                UpdateUi.AddProgress(ScanAmount, 10, TuningTableMain.TBM.AOBProgressBar);

                FrontBumpStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x534");
                FrontBumpStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x538");
                RearBumpStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x688");
                RearBumpStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x68C");
                UpdateUi.AddProgress(ScanAmount, 11, TuningTableMain.TBM.AOBProgressBar);

                FrontAeroMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x378");
                FrontAeroMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x380");
                RearAeroMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x3D8");
                RearAeroMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x3E0");
                UpdateUi.AddProgress(ScanAmount, 12, TuningTableMain.TBM.AOBProgressBar);

                AngleMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x7FC");
                AngleMax2 = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x800");
                VelocityStraight = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x804");
                VelocityTurning = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x808");
                VelocityCountersteer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x80C");
                VelocityDynamicPeek = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x810");
                TimeToMaxSteering = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x814");
                UpdateUi.AddProgress(ScanAmount, 13, TuningTableMain.TBM.AOBProgressBar);

                Wheelbase = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD0");
                FrontWidth = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD4");
                RearWidth = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD8");
                FrontSpacer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x9D0");
                RearSpacer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x9D4");
                UpdateUi.AddProgress(ScanAmount, 14, TuningTableMain.TBM.AOBProgressBar);

                RimSizeFront = (TuningTableBase3 + ",0x150,0x300,0x7D8");
                RimSizeRear = (TuningTableBase3 + ",0x150,0x300,0x7DC");
                RimRadiusFront = (TuningTableBase3 + ",0x150,0x300,0x7E0");
                RimRadiusRear = (TuningTableBase3 + ",0x150,0x300,0x7E4");
            }
            #endregion

            #region MS FH4
            else if (MainWindow.mw.gvp.Plat == "MS" && MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {

            }
            #endregion
            #region Steam FH4
            else if (MainWindow.mw.gvp.Plat == "Steam" && MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {

            }
            #endregion

            UpdateUi.AddProgress(ScanAmount, 15, TuningTableMain.TBM.AOBProgressBar);
            UpdateUi.UpdateUI(true, TuningTableMain.TBM);
            ReadValues();
        }

        private static Task ReadValues()
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
            while (true)
            {
                Thread.Sleep(5);
                // No reason to update values when not focused
                if (MainWindow.mw.Page_Focused != "TuningTableMain")
                    continue;

                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    #region Aero
                    if (UpdateUi.IsClicked["AeroButton"])
                    {
                        Aero.ae.FrontAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAeroMin, round: false), 3);
                        Aero.ae.FrontAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontAeroMax, round: false), 3);
                        Aero.ae.RearAeroMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAeroMin, round: false), 3);
                        Aero.ae.RearAeroMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearAeroMax, round: false), 3);
                    }
                    #endregion
                    #region Gearing
                    if (UpdateUi.IsClicked["GearingButton"])
                    {
                        Gearing.g.FinalDriveBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FinalDrive, round: false), 5);
                        Gearing.g.ReverseGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(ReverseGear, round: false), 5);
                        Gearing.g.FirstGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FirstGear, round: false), 5);

                        if (MainWindow.mw.m.ReadFloat(SecondGear) != 0)
                        {
                            Gearing.g.SecondGearBox.IsEnabled = true;
                            Gearing.g.SecondGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SecondGear, round: false), 5);
                        }
                        else
                            Gearing.g.SecondGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(ThirdGear) != 0)
                        {
                            Gearing.g.ThirdGearBox.IsEnabled = true;
                            Gearing.g.ThirdGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(ThirdGear, round: false), 5);
                        }
                        else
                            Gearing.g.ThirdGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(FourthGear) != 0)
                        {
                            Gearing.g.FourthGearBox.IsEnabled = true;
                            Gearing.g.FourthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FourthGear, round: false), 5);
                        }
                        else
                            Gearing.g.FourthGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(FifthGear) != 0)
                        {
                            Gearing.g.FifthGearBox.IsEnabled = true;
                            Gearing.g.FifthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FifthGear, round: false), 5);
                        }
                        else
                            Gearing.g.FifthGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(SixthGear) != 0)
                        {
                            Gearing.g.SixthGearBox.IsEnabled = true;
                            Gearing.g.SixthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SixthGear, round: false), 5);
                        }
                        else
                            Gearing.g.SixthGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(SeventhGear) != 0)
                        {
                            Gearing.g.SeventhGearBox.IsEnabled = true;
                            Gearing.g.SeventhGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SeventhGear, round: false), 5);
                        }
                        else
                            Gearing.g.SeventhGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(EighthGear) != 0)
                        {
                            Gearing.g.EighthGearBox.IsEnabled = true;
                            Gearing.g.EighthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(EighthGear, round: false), 5);
                        }
                        else
                            Gearing.g.EighthGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(NinthGear) != 0)
                        {
                            Gearing.g.NinthGearBox.IsEnabled = true;
                            Gearing.g.NinthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(NinthGear, round: false), 5);
                        }
                        else
                            Gearing.g.NinthGearBox.IsEnabled = false;

                        if (MainWindow.mw.m.ReadFloat(TenthGear) != 0)
                        {
                            Gearing.g.TenthGearBox.IsEnabled = true;
                            Gearing.g.TenthGearBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TenthGear, round: false), 5);
                        }
                        else
                            Gearing.g.TenthGearBox.IsEnabled = false;
                    }
                    #endregion
                    #region Damping
                    if (UpdateUi.IsClicked["DampingButton"])
                    {
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
                    }
                    #endregion
                    #region Others
                    if (UpdateUi.IsClicked["OthersButton"])
                    {
                        Others.o.WheelbaseBox.Value = MainWindow.mw.m.ReadFloat(Wheelbase, round: false);
                        Others.o.RimSizeFrontBox.Value = MainWindow.mw.m.ReadFloat(RimSizeFront, round: false);
                        Others.o.RimSizeRearBox.Value = MainWindow.mw.m.ReadFloat(RimSizeRear, round: false);
                        Others.o.RimRadiusFrontBox.Value = MainWindow.mw.m.ReadFloat(RimRadiusFront, round: false);
                        Others.o.RimRadiusRearBox.Value = MainWindow.mw.m.ReadFloat(RimRadiusRear, round: false);
                        Others.o.FrontWidthBox.Value = MainWindow.mw.m.ReadFloat(FrontWidth, round: false);
                        Others.o.RearWidthBox.Value = MainWindow.mw.m.ReadFloat(RearWidth, round: false);
                        Others.o.FrontSpacerBox.Value = MainWindow.mw.m.ReadFloat(FrontSpacer, round: false);
                        Others.o.RearSpacerBox.Value = MainWindow.mw.m.ReadFloat(RearSpacer, round: false);
                    }
                    #endregion
                    #region Springs
                    if (UpdateUi.IsClicked["SpringsButton"])
                    {
                        Springs.sp.SpringFrontMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringFrontMin, round: false), 3);
                        Springs.sp.SpringFrontMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringFrontMax, round: false), 3);
                        Springs.sp.SpringRearMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringRearMin, round: false), 3);
                        Springs.sp.SpringRearMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(SpringRearMax, round: false), 3);

                        Springs.sp.FrontRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontRideHeightMin, round: false), 6);
                        Springs.sp.FrontRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(FrontRideHeightMax, round: false), 6);
                        Springs.sp.RearRideHeightMinBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearRideHeightMin, round: false), 6);
                        Springs.sp.RearRideHeightMaxBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(RearRideHeightMax, round: false), 6);
                    }
                    #endregion
                    #region Steering
                    if (UpdateUi.IsClicked["SteeringButton"])
                    {
                        Steering.st.AngleMaxBox.Value = MainWindow.mw.m.ReadFloat(AngleMax, round: false);
                        Steering.st.AngleMax2Box.Value = MainWindow.mw.m.ReadFloat(AngleMax2, round: false);
                        Steering.st.VelocityCountersteerBox.Value = MainWindow.mw.m.ReadFloat(VelocityCountersteer, round: false);
                        Steering.st.VelocityDynamicPeekBox.Value = MainWindow.mw.m.ReadFloat(VelocityDynamicPeek, round: false);
                        Steering.st.VelocityStraightBox.Value = MainWindow.mw.m.ReadFloat(VelocityStraight, round: false);
                        Steering.st.VelocityTurningBox.Value = MainWindow.mw.m.ReadFloat(VelocityTurning, round: false);
                        Steering.st.TimeToMaxSteeringBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TimeToMaxSteering, round: false), 5);
                    }
                    #endregion
                    #region Tires
                    if (UpdateUi.IsClicked["TiresButton"])
                    {
                        Tires.t.TireFrontLeftBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireFrontLeft, round: false) / Tires.TireFrontLeftDivider, 5);
                        Tires.t.TireFrontRightBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireFrontRight, round: false) / Tires.TireFrontRightDivider, 5);
                        Tires.t.TireRearLeftBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireRearLeft, round: false) / Tires.TireRearLeftDivider, 5);
                        Tires.t.TireRearRightBox.Value = Math.Round(MainWindow.mw.m.ReadFloat(TireRearRight, round: false) / Tires.TireRearRightDivider, 5);
                    }
                    #endregion
                });
            }
        }
    }
}
