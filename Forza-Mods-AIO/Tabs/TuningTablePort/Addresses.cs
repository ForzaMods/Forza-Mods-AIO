using System.Diagnostics;
using System.Linq;

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
        #endregion

        public async static void TuningTable()
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon5")[0];
            long ScanStart = (long)TargetProcess.MainModule.BaseAddress;
            long ScanEnd = (long)(TargetProcess.MainModule.BaseAddress + TargetProcess.MainModule.ModuleMemorySize);
            #region Camber
            CamberBaseStatic = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 ? ? 00 00 ? 4? 00 00 ?0 C? 00 00 ? 4? 00 00 80 3F", true, true, false)).FirstOrDefault();
            ToeBaseStatic = (CamberBaseStatic + 0x8);

            CamberNegStatic = CamberBaseStatic.ToString("X");
            CamberPosStatic = (CamberBaseStatic + 0x4).ToString("X");
            ToeNegStatic = (ToeBaseStatic).ToString("X");
            ToePosStatic = (ToeBaseStatic + 0x4).ToString("X");
            #endregion

            if (MainWindow.mw.gvp.Plat == "MS" && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableBase1 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "?0 ? ? ? ? 0? 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");
                //TuningTableBase2 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "", true, true, false)).FirstOrDefault().ToString("X");
                //TuningTableBase3 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "", true, true, false)).FirstOrDefault().ToString("X");

                TireFrontLeft = (TuningTableBase1 + ",0x10,0x10,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x10,0x10,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x10,0x10,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x10,0x10,0x4808");

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
            }

            else if (MainWindow.mw.gvp.Plat == "Steam" && MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                TuningTableBase1 = ((await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "00 00 00 00 FF FF FF FF 10 ? ? ? ? 0? 00 00 00 ? ? ? ? 0? 00 00 ? ? ? ? ? 0? 00 00 00 ?", true, true, false)).FirstOrDefault() + 0x8).ToString("X");
                TuningTableBase2 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "90 2E ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 ? ? ? ? ? 0? 00 00", true, true, false)).FirstOrDefault().ToString("X");
                TuningTableBase3 = (await MainWindow.mw.m.AoBScan(ScanStart, ScanEnd, "D0 84 ? ? ? 0? 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00", true, true, false)).FirstOrDefault().ToString("X");

                TireFrontLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x27C8");
                TireFrontRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3288");
                TireRearRight = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x3D48");
                TireRearLeft = (TuningTableBase1 + ",0x320,0x10,0x1D0,0x4808");

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

                CamberNeg = (TuningTableBase2 + ",0x8B0,0x498");
                CamberPos = (TuningTableBase2 + ",0x8B0,0x49C");
                ToeNeg = (TuningTableBase2 + ",0x8B0,0x4A0");
                ToePos = (TuningTableBase2 + ",0x8B0,0x4A4");

                SpringFrontMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x528");
                SpringFrontMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x52C");
                SpringRearMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x67C");
                SpringRearMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x680");

                FrontRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x508");
                FrontRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x50C");
                RearRideHeightMin = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x65C");
                RearRideHeightMax = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x660");

                FrontRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x510");
                RearRestriction = (TuningTableBase1 + ",0x340,0x30,0x1E0,0x664");
            }
        }
    }
}
