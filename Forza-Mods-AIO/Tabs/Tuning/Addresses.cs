using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
{
    internal class Addresses
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
        public static long CamberBaseStatic;
        public static long ToeBaseStatic;

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
        public static string FrontReboundStiffnesMin;
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
        public static string AngleVelocityStraight;
        public static string AngleVelocityTurning;
        public static string AngleVelocityCountersteer;
        public static string AngleVelocityDynamicPeek;
        public static string AngleTimeToMaxSteering;
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

        public static string TuningTableBase1;
        public static string TuningTableBase2;
        public static string TuningTableBase3;
        public static string TuningTableBase4;
        #endregion

        public static async void TuningTable(int ver)
        {
            #region Camber,toe (static) and scanning vars
            Dispatcher dispatcher = Application.Current.Dispatcher;
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon" + ver.ToString())[0];
            long ScanStart = (long)TargetProcess.MainModule.BaseAddress;
            long ScanEnd = (long)(TargetProcess.MainModule.BaseAddress + TargetProcess.MainModule.ModuleMemorySize);
            
            CamberBaseStatic = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 ? ? 00 00 ? 4? 00 00 ?0 C? 00 00 ? 4? 00 00 80 3F", true, true, false)).FirstOrDefault();
            ToeBaseStatic = (CamberBaseStatic + 0x8);
            await dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 3; });
            Thread.Sleep(100);

            CamberNegStatic = CamberBaseStatic.ToString("X");
            CamberPosStatic = (CamberBaseStatic + 0x4).ToString("X");
            ToeNegStatic = (ToeBaseStatic).ToString("X");
            ToePosStatic = (ToeBaseStatic + 0x4).ToString("X");
            await dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 6; });
            Thread.Sleep(100);
            #endregion

            #region MS FH5
            if (MainWindow.mw.gvp.Plat == "MS" && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableBase1 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "?0 ? ? ? ? 0? 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");
                TuningTableBase2 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 00 00 00 00 E8 CA 04 B2 C0 6D 9D 40 80 2F ? ? ? 0? 00 00 FF FF FF FF FF FF FF FF FF FF FF FF", true, true, false)).FirstOrDefault() + 0x100).ToString("X");
                TuningTableBase3 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 FF FF FF FF 10 ? ? ? ? 0? 00 00 00 ? ? ? ? 0? 00 00 ? ? ? ? ? 0? 00 00 00 ?", true, true, false)).FirstOrDefault() + 0x8).ToString("X");
                TuningTableBase4 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 12; });
                Thread.Sleep(100);

                TireFrontLeft = (TuningTableBase1 + ",0x10,0x10,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x10,0x10,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x10,0x10,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x10,0x10,0x4808");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 18; });
                Thread.Sleep(100);

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
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 24; });
                Thread.Sleep(100);

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x490");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x494");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x498");
                ToePos = (TuningTableBase2 + ",0x8B0,0x49C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 30; });
                Thread.Sleep(100);

                SpringFrontMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x528");
                SpringFrontMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x52C");
                SpringRearMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x67C");
                SpringRearMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x680");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 36; });
                Thread.Sleep(100);

                FrontRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x508");
                FrontRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x50C");
                RearRideHeightMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x65C");
                RearRideHeightMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x660");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 42; });
                Thread.Sleep(100);

                FrontRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x510");
                RearRestriction = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x664");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 48; });
                Thread.Sleep(100);

                FrontAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5C4");
                FrontAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x5C8");
                RearAntirollMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x718");
                RearAntirollMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x71C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 56; });
                Thread.Sleep(100);

                FrontReboundStiffnesMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x558");
                FrontReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x55C");
                RearReboundStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x6AC");
                RearReboundStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x68C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 62; });
                Thread.Sleep(100);

                FrontBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x534");
                FrontBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x538");
                RearBumpStiffnessMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x688");
                RearBumpStiffnessMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x68C");
                dispatcher.BeginInvoke((Action)delegate () {TuningTableMain.TBM.AOBProgressBar.Value = 68; });
                Thread.Sleep(100);

                FrontAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x378");
                FrontAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x380");
                RearAeroMin = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3D8");
                RearAeroMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x3E0");
                dispatcher.BeginInvoke((Action)delegate () {TuningTableMain.TBM.AOBProgressBar.Value = 74; });
                Thread.Sleep(100);

                AngleMax = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x7FC");
                AngleMax2 = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x800");
                AngleVelocityStraight = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x804");
                AngleVelocityTurning = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x808");
                AngleVelocityCountersteer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x80C");
                AngleVelocityDynamicPeek = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x810");
                AngleTimeToMaxSteering = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x814");
                dispatcher.BeginInvoke((Action)delegate () {TuningTableMain.TBM.AOBProgressBar.Value = 80; });
                Thread.Sleep(100);

                Wheelbase = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD0");
                FrontWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD4");
                RearWidth = (TuningTableBase3 + ",0x330,0x8,0x1E0,0xD8");
                FrontSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9D0");
                RearSpacer = (TuningTableBase3 + ",0x330,0x8,0x1E0,0x9D4");
                dispatcher.BeginInvoke((Action)delegate () {TuningTableMain.TBM.AOBProgressBar.Value = 86; });
                Thread.Sleep(100);

                RimSizeFront = (TuningTableBase4 + ",0x150,0x300,0x7D8");
                RimSizeRear = (TuningTableBase4 + ",0x150,0x300,0x7DC");
                RimRadiusFront = (TuningTableBase4 + ",0x150,0x300,0x7E0");
                RimRadiusRear = (TuningTableBase4 + ",0x150,0x300,0x7E4");
                dispatcher.BeginInvoke((Action)delegate () {TuningTableMain.TBM.AOBProgressBar.Value = 100; });
                Thread.Sleep(100);
            }
            #endregion
            #region Steam FH5
            else if (MainWindow.mw.gvp.Plat == "Steam" && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableBase1 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 FF FF FF FF 10 ? ? ? ? 0? 00 00 00 ? ? ? ? 0? 00 00 ? ? ? ? ? 0? 00 00 00 ?", true, true, false)).FirstOrDefault() + 0x8).ToString("X");
                TuningTableBase2 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "90 2E ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 ? ? ? ? ? 0? 00 00", true, true, false)).FirstOrDefault().ToString("X");
                TuningTableBase3 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 12; });
                Thread.Sleep(100);

                TireFrontLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x4808");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 18; });
                Thread.Sleep(100);

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
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 24; });
                Thread.Sleep(100);

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x490");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x494");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x498");
                ToePos = (TuningTableBase2 + ",0x8B0,0x49C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 30; });
                Thread.Sleep(100);

                SpringFrontMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x528");
                SpringFrontMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x52C");
                SpringRearMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x67C");
                SpringRearMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x680");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 36; });
                Thread.Sleep(100);

                FrontRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x508");
                FrontRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x50C");
                RearRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x65C");
                RearRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x660");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 42; });
                Thread.Sleep(100);

                FrontRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x510");
                RearRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x664");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 48; });
                Thread.Sleep(100);

                FrontAntirollMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x5C4");
                FrontAntirollMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x5C8");
                RearAntirollMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x718");
                RearAntirollMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x71C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 56; });
                Thread.Sleep(100);

                FrontReboundStiffnesMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x558");
                FrontReboundStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x55C");
                RearReboundStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x6AC");
                RearReboundStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x6B0");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 62; });
                Thread.Sleep(100);

                FrontBumpStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x534");
                FrontBumpStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x538");
                RearBumpStiffnessMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x688");
                RearBumpStiffnessMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x68C");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 68; });
                Thread.Sleep(100);

                FrontAeroMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x378");
                FrontAeroMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x380");
                RearAeroMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x3D8");
                RearAeroMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x3E0");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 74; });
                Thread.Sleep(100);

                AngleMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x7FC");
                AngleMax2 = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x800");
                AngleVelocityStraight = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x804");
                AngleVelocityTurning = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x808");
                AngleVelocityCountersteer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x80C");
                AngleVelocityDynamicPeek = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x810");
                AngleTimeToMaxSteering = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x814");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 80; });
                Thread.Sleep(100);

                Wheelbase = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD0");
                FrontWidth = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD4");
                RearWidth = (TuningTableBase1 + ",0x340,0x30,0x1E0,0xD8");
                FrontSpacer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x9D0");
                RearSpacer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x9D4");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 86; });
                Thread.Sleep(100);

                RimSizeFront = (TuningTableBase3 + ",0x150,0x300,0x7D8");
                RimSizeRear = (TuningTableBase3 + ",0x150,0x300,0x7DC");
                RimRadiusFront = (TuningTableBase3 + ",0x150,0x300,0x7E0");
                RimRadiusRear = (TuningTableBase3 + ",0x150,0x300,0x7E4");
                dispatcher.BeginInvoke((Action)delegate () { TuningTableMain.TBM.AOBProgressBar.Value = 100; });
                Thread.Sleep(100);
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
        }
    }
}
