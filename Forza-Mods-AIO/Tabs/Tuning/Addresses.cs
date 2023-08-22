﻿using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private static int ScanAmount = 15;
        #endregion

        public static async Task Scan()
        {
            #region Camber,toe (static) and scanning vars
            Dispatcher dispatcher = Application.Current.Dispatcher;
            long ScanStart = (long)MainWindow.mw.gvp.Process.MainModule.BaseAddress;
            long ScanEnd = (long)(MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize);
            Base = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 a0 ? ? ? ? ? ? ? ? c0 00 ? a0", true, true, false)).FirstOrDefault();
            CamberNegStatic = Base.ToString("X");
            CamberPosStatic = (Base + 0x4).ToString("X");
            ToeNegStatic = (Base + 0x8).ToString("X");
            ToePosStatic = (Base + 0xC).ToString("X");
            UpdateUi.AddProgress(ScanAmount, 1, TuningTableMain.TBM.AOBProgressBar);
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
                TuningTableBase2 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 00 00 00 00 E8 CA 04 B2 C0 6D 9D 40 80 2F ? ? ? 0? 00 00 FF FF FF FF FF FF FF FF FF FF FF FF", true, true, false)).FirstOrDefault() + 0x100).ToString("X");
                TuningTableBase3 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");
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

                FrontReboundStiffnesMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x558");
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
                AngleVelocityStraight = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x804");
                AngleVelocityTurning = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x808");
                AngleVelocityCountersteer = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x80C");
                AngleVelocityDynamicPeek = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x810");
                AngleTimeToMaxSteering = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x814");
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

        private static async Task ReadValues()
        {
            Dispatcher dispatcher = Application.Current.Dispatcher;

            //These can be only read once as they dont change when a car is switched
            dispatcher.BeginInvoke((Action)delegate ()
            {
                Alignment.al.CamberNegBox.Value = MainWindow.mw.m.ReadFloat(Addresses.CamberNegStatic);
                Alignment.al.CamberPosBox.Value = MainWindow.mw.m.ReadFloat(Addresses.CamberPosStatic);
                Alignment.al.ToeNegBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ToeNegStatic);
                Alignment.al.ToePosBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ToePosStatic);
            });

            //Rest requires a constant reading
            while (true)
            {
                Thread.Sleep(500);
                if (MainWindow.mw.Page_Focused != "TuningTableMain")
                    continue;

                dispatcher.BeginInvoke((Action)delegate ()
                {
                    #region Aero
                    Aero.ae.FrontAeroMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAeroMin);
                    Aero.ae.FrontAeroMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAeroMax);
                    Aero.ae.RearAeroMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAeroMin);
                    Aero.ae.RearAeroMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAeroMax);
                    #endregion
                    #region Gearing
                    Gearing.g.FinalDriveRatioBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FinalDrive);
                    Gearing.g.ReverseGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ReverseGear);
                    Gearing.g.FirstGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FirstGear);
                    Gearing.g.SecondGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SecondGear);
                    Gearing.g.ThirdGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ThirdGear);
                    Gearing.g.FourthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FourthGear);

                    if (MainWindow.mw.m.ReadFloat(Addresses.FifthGear) != 0)
                    {
                        Gearing.g.FifthGearBox.IsEnabled = true;
                        Gearing.g.FifthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FifthGear);
                    }
                    else
                        Gearing.g.FifthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.SixthGear) != 0)
                    {
                        Gearing.g.SixthGearBox.IsEnabled = true;
                        Gearing.g.SixthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SixthGear);
                    }
                    else
                        Gearing.g.SixthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.SeventhGear) != 0)
                    {
                        Gearing.g.SeventhGearBox.IsEnabled = true;
                        Gearing.g.SeventhGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SeventhGear);
                    }
                    else
                        Gearing.g.SeventhGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.EighthGear) != 0)
                    {
                        Gearing.g.EighthBox.IsEnabled = true;
                        Gearing.g.EighthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.EighthGear);
                    }
                    else
                        Gearing.g.EighthBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.NinthGear) != 0)
                    {
                        Gearing.g.NinthGearBox.IsEnabled = true;
                        Gearing.g.NinthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.NinthGear);
                    }
                    else
                        Gearing.g.NinthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.TenthGear) != 0)
                    {
                        Gearing.g.TenthGearBox.IsEnabled = true;
                        Gearing.g.TenthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TenthGear);
                    }
                    else
                        Gearing.g.TenthGearBox.IsEnabled = false;
                    #endregion
                    #region Damping
                    Damping.d.FrontAntirollBarsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAntirollMin);
                    Damping.d.FrontAntirollBarsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAntirollMax);
                    Damping.d.RearAntirollBarsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAntirollMin);
                    Damping.d.RearAntirollBarsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAntirollMax);

                    Damping.d.FrontBumpStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontBumpStiffnessMin);
                    Damping.d.FrontBumpStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontBumpStiffnessMax);
                    Damping.d.RearBumpStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearBumpStiffnessMin);
                    Damping.d.RearBumpStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearBumpStiffnessMax);

                    Damping.d.FrontReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontReboundStiffnesMin);
                    Damping.d.FrontReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontReboundStiffnessMax);
                    Damping.d.RearReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearReboundStiffnessMin);
                    Damping.d.RearReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearReboundStiffnessMax);
                    #endregion
                    #region Others
                    Others.o.WheelbaseBox.Value = MainWindow.mw.m.ReadFloat(Addresses.Wheelbase);
                    Others.o.RimSizeFrontBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimSizeFront);
                    Others.o.RimSizeRearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimSizeRear);
                    Others.o.RimRadiusFrontBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimRadiusFront);
                    Others.o.RimRadiusRearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimRadiusRear);
                    Others.o.FrontWidthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontWidth);
                    Others.o.RearWidthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearWidth);
                    Others.o.FrontSpacerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontSpacer);
                    Others.o.RearSpacerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearSpacer);
                    #endregion
                    #region Springs
                    Springs.sp.FrontSpringsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringFrontMin);
                    Springs.sp.FrontSpringsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringFrontMax);
                    Springs.sp.RearSpringsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringRearMin);
                    Springs.sp.RearSpringsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringRearMax);

                    Springs.sp.FrontRideHeightMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontRideHeightMin);
                    Springs.sp.FrontRideHeightMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontRideHeightMax);
                    Springs.sp.RearRideHeightMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearRideHeightMin);
                    Springs.sp.RearRideHeightMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearRideHeightMax);
                    #endregion
                    #region Steering
                    Steering.st.AngleBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleMax);
                    Steering.st.Angle2Box.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleMax2);
                    Steering.st.VelocityCountersteerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityCountersteer);
                    Steering.st.VelocityDynamicPeekBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityDynamicPeek);
                    Steering.st.VelocityStraightBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityStraight);
                    Steering.st.VelocityTurningBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityTurning);
                    Steering.st.VelocityTimeBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleTimeToMaxSteering);
                    #endregion
                    #region Tires
                    Tires.t.FrontLeftTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireFrontLeft);
                    Tires.t.FrontRightTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireFrontRight);
                    Tires.t.RearLeftTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireRearLeft);
                    Tires.t.RearRightTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireRearRight);
                    #endregion
                });
            }
        }
    }
}
