using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms
{
    public partial class LiveTuning : Form
    {
        LiveTuningForms.Tyres Tyres = new LiveTuningForms.Tyres() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        LiveTuningForms.Gears Gears = new LiveTuningForms.Gears() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        LiveTuningForms.Alignment Alignment = new LiveTuningForms.Alignment() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public LiveTuning()
        {
            InitializeComponent();
            this.TabHolder.Controls.Add(Tyres);
            Tyres.Visible = true;
        }
        #region Pressure Addresses
        public static string FrontTyrePressureAddr;
        public static string RearTyrePressureAddr;
        #endregion
        #region Gear Addresses
        public static string Gear1Addr;
        public static string Gear2Addr;
        public static string Gear3Addr;
        public static string Gear4Addr;
        public static string Gear5Addr;
        public static string Gear6Addr;
        public static string Gear7Addr;
        public static string Gear8Addr;
        public static string Gear9Addr;
        public static string GearFinalAddr;
        #endregion
        #region Toe Addresses
        #region Front Toe Addresses
        public static string FrontLeftToeAddr;
        public static string FrontRightToeAddr;
        #endregion
        #region Rear Left Toe Addresses
        public static string RearLeftToeAddr1;
        public static string RearLeftToeAddr2;
        public static string RearLeftToeAddr3;
        public static string RearLeftToeAddr4;
        public static string RearLeftToeAddr5;
        public static string RearLeftToeAddr6;
        public static string RearLeftToeAddr7;
        public static string RearLeftToeAddr8;
        public static string RearLeftToeAddr9;
        public static string RearLeftToeAddr10;
        public static string RearLeftToeAddr11;
        public static string RearLeftToeAddr12;
        public static string RearLeftToeAddr13;
        public static string RearLeftToeAddr14;
        public static string RearLeftToeAddr15;
        public static string RearLeftToeAddr16;
        public static string RearLeftToeAddr17;
        public static string RearLeftToeAddr18;
        public static string RearLeftToeAddr19;
        public static string RearLeftToeAddr20;
        public static string RearLeftToeAddr21;
        public static string RearLeftToeAddr22;
        public static string RearLeftToeAddr23;
        public static string RearLeftToeAddr24;
        public static string RearLeftToeAddr25;
        public static string RearLeftToeAddr26;
        public static string RearLeftToeAddr27;
        public static string RearLeftToeAddr28;
        public static string RearLeftToeAddr29;
        public static string RearLeftToeAddr30;
        public static string RearLeftToeAddr31;
        #endregion
        #region Rear Right Toe Addresses
        public static string RearRightToeAddr1;
        public static string RearRightToeAddr2;
        public static string RearRightToeAddr3;
        public static string RearRightToeAddr4;
        public static string RearRightToeAddr5;
        public static string RearRightToeAddr6;
        public static string RearRightToeAddr7;
        public static string RearRightToeAddr8;
        public static string RearRightToeAddr9;
        public static string RearRightToeAddr10;
        public static string RearRightToeAddr11;
        public static string RearRightToeAddr12;
        public static string RearRightToeAddr13;
        public static string RearRightToeAddr14;
        public static string RearRightToeAddr15;
        public static string RearRightToeAddr16;
        public static string RearRightToeAddr17;
        public static string RearRightToeAddr18;
        public static string RearRightToeAddr19;
        public static string RearRightToeAddr20;
        public static string RearRightToeAddr21;
        public static string RearRightToeAddr22;
        public static string RearRightToeAddr23;
        public static string RearRightToeAddr24;
        public static string RearRightToeAddr25;
        public static string RearRightToeAddr26;
        public static string RearRightToeAddr27;
        public static string RearRightToeAddr28;
        public static string RearRightToeAddr29;
        public static string RearRightToeAddr30;
        #endregion
        #endregion
        #region Camber Addresses
        #region Front Left Camber Addresses
        public static string FrontLeftCamber1Addr;
        public static string FrontLeftCamber2Addr;
        public static string FrontLeftCamber3Addr;
        public static string FrontLeftCamber4Addr;
        public static string FrontLeftCamber5Addr;
        public static string FrontLeftCamber6Addr;
        public static string FrontLeftCamber7Addr;
        public static string FrontLeftCamber8Addr;
        public static string FrontLeftCamber9Addr;
        public static string FrontLeftCamber10Addr;
        public static string FrontLeftCamber11Addr;
        public static string FrontLeftCamber12Addr;
        public static string FrontLeftCamber13Addr;
        public static string FrontLeftCamber14Addr;
        public static string FrontLeftCamber15Addr;
        public static string FrontLeftCamber16Addr;
        public static string FrontLeftCamber17Addr;
        public static string FrontLeftCamber18Addr;
        public static string FrontLeftCamber19Addr;
        public static string FrontLeftCamber20Addr;
        public static string FrontLeftCamber21Addr;
        public static string FrontLeftCamber22Addr;
        public static string FrontLeftCamber23Addr;
        public static string FrontLeftCamber24Addr;
        public static string FrontLeftCamber25Addr;
        public static string FrontLeftCamber26Addr;
        #endregion
        #region Front Right Camber Addresses
        public static string FrontRightCamber1Addr;
        public static string FrontRightCamber2Addr;
        public static string FrontRightCamber3Addr;
        public static string FrontRightCamber4Addr;
        public static string FrontRightCamber5Addr;
        public static string FrontRightCamber6Addr;
        public static string FrontRightCamber7Addr;
        public static string FrontRightCamber8Addr;
        public static string FrontRightCamber9Addr;
        public static string FrontRightCamber10Addr;
        public static string FrontRightCamber11Addr;
        public static string FrontRightCamber12Addr;
        public static string FrontRightCamber13Addr;
        public static string FrontRightCamber14Addr;
        public static string FrontRightCamber15Addr;
        public static string FrontRightCamber16Addr;
        public static string FrontRightCamber17Addr;
        public static string FrontRightCamber18Addr;
        public static string FrontRightCamber19Addr;
        public static string FrontRightCamber20Addr;
        public static string FrontRightCamber21Addr;
        public static string FrontRightCamber22Addr;
        public static string FrontRightCamber23Addr;
        public static string FrontRightCamber24Addr;
        public static string FrontRightCamber25Addr;
        public static string FrontRightCamber26Addr;
        #endregion
        #region Rear Left Camber Addresses
        public static string RearLeftCamber1Addr;
        public static string RearLeftCamber2Addr;
        public static string RearLeftCamber3Addr;
        public static string RearLeftCamber4Addr;
        public static string RearLeftCamber5Addr;
        public static string RearLeftCamber6Addr;
        public static string RearLeftCamber7Addr;
        public static string RearLeftCamber8Addr;
        public static string RearLeftCamber9Addr;
        public static string RearLeftCamber10Addr;
        public static string RearLeftCamber11Addr;
        public static string RearLeftCamber12Addr;
        public static string RearLeftCamber13Addr;
        public static string RearLeftCamber14Addr;
        public static string RearLeftCamber15Addr;
        public static string RearLeftCamber16Addr;
        public static string RearLeftCamber17Addr;
        public static string RearLeftCamber18Addr;
        public static string RearLeftCamber19Addr;
        public static string RearLeftCamber20Addr;
        public static string RearLeftCamber21Addr;
        public static string RearLeftCamber22Addr;
        public static string RearLeftCamber23Addr;
        public static string RearLeftCamber24Addr;
        public static string RearLeftCamber25Addr;
        public static string RearLeftCamber26Addr;
        public static string RearLeftCamber27Addr;
        public static string RearLeftCamber28Addr;
        public static string RearLeftCamber29Addr;
        public static string RearLeftCamber30Addr;
        public static string RearLeftCamber31Addr;
        public static string RearLeftCamber32Addr;
        #endregion
        #region Rear Right Camber Addresses
        public static string RearRightCamber1Addr;
        public static string RearRightCamber2Addr;
        public static string RearRightCamber3Addr;
        public static string RearRightCamber4Addr;
        public static string RearRightCamber5Addr;
        public static string RearRightCamber6Addr;
        public static string RearRightCamber7Addr;
        public static string RearRightCamber8Addr;
        public static string RearRightCamber9Addr;
        public static string RearRightCamber10Addr;
        public static string RearRightCamber11Addr;
        public static string RearRightCamber12Addr;
        public static string RearRightCamber13Addr;
        public static string RearRightCamber14Addr;
        public static string RearRightCamber15Addr;
        public static string RearRightCamber16Addr;
        public static string RearRightCamber17Addr;
        public static string RearRightCamber18Addr;
        public static string RearRightCamber19Addr;
        public static string RearRightCamber20Addr;
        public static string RearRightCamber21Addr;
        public static string RearRightCamber22Addr;
        public static string RearRightCamber23Addr;
        public static string RearRightCamber24Addr;
        public static string RearRightCamber25Addr;
        public static string RearRightCamber26Addr;
        public static string RearRightCamber27Addr;
        public static string RearRightCamber28Addr;
        public static string RearRightCamber29Addr;
        public static string RearRightCamber30Addr;
        #endregion
        #endregion
        public static void Addresses()
        {
            #region pressure
            FrontTyrePressureAddr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x337C");
            RearTyrePressureAddr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x495C");
            #endregion
            #region gears
            GearFinalAddr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xC18");
            Gear1Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xAE0");
            Gear2Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xAF4");
            Gear3Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB08");
            Gear4Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB1C");
            Gear5Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB30");
            Gear6Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB44");
            Gear7Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB58");
            Gear8Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB6C");
            Gear9Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xB80");
            #endregion
            #region Toe
                #region Front Toe
                FrontLeftToeAddr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x1DB8");
                FrontRightToeAddr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3398");
                #endregion
                #region Rear Left Toe
                RearLeftToeAddr1 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6EA4");
                RearLeftToeAddr2 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6B24");
                RearLeftToeAddr3 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6224");
                RearLeftToeAddr4 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6EA4");
                RearLeftToeAddr5 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6DA4");
                RearLeftToeAddr6 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6E24");
                RearLeftToeAddr7 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6D24");
                RearLeftToeAddr8 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6CA4");
                RearLeftToeAddr9 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6C24");
                RearLeftToeAddr10 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6BA4");
                RearLeftToeAddr11 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6AA4");
                RearLeftToeAddr12 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6A24");
                RearLeftToeAddr13 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x69A4");
                RearLeftToeAddr14 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6924");
                RearLeftToeAddr15 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x68A4");
                RearLeftToeAddr16 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6824");
                RearLeftToeAddr17 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x67A4");
                RearLeftToeAddr18 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6724");
                RearLeftToeAddr19 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x66A4");
                RearLeftToeAddr20 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6624");
                RearLeftToeAddr21 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x65A4");
                RearLeftToeAddr22 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6524");
                RearLeftToeAddr23 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x64A4");
                RearLeftToeAddr24 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6424");
                RearLeftToeAddr25 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x63A4");
                RearLeftToeAddr26 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6324");
                RearLeftToeAddr27 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x62A4");
                RearLeftToeAddr28 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x61A4");
                RearLeftToeAddr29 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6124");
                RearLeftToeAddr30 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x60A4");
                RearLeftToeAddr31 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6024");
            #endregion
                #region Rear Right Toe
                RearRightToeAddr1 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x58C4");
                RearRightToeAddr2 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5844");
                RearRightToeAddr3 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x57C4");
                RearRightToeAddr4 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5744");
                RearRightToeAddr5 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x56C4");
                RearRightToeAddr6 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5644");
                RearRightToeAddr7 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x55C4");
                RearRightToeAddr8 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5544");
                RearRightToeAddr9 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x54C4");
                RearRightToeAddr10 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5444");
                RearRightToeAddr11 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x53C4");
                RearRightToeAddr12 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5344");
                RearRightToeAddr13 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x52C4");
                RearRightToeAddr14 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5244");
                RearRightToeAddr15 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x51C4");
                RearRightToeAddr16 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5144");
                RearRightToeAddr17 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x50C4");
                RearRightToeAddr18 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5044");
                RearRightToeAddr19 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4FC4");
                RearRightToeAddr20 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4F44");
                RearRightToeAddr21 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4EC4");
                RearRightToeAddr22 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4E44");
                RearRightToeAddr23 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4DC4");
                RearRightToeAddr24 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4D44");
                RearRightToeAddr25 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4CC4");
                RearRightToeAddr26 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4C44");
                RearRightToeAddr27 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4BC4");
                RearRightToeAddr28 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4B44");
                RearRightToeAddr28 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4B44");
                RearRightToeAddr29 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4AC4");
                RearRightToeAddr30 = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4A44");
                #endregion
            #endregion
            #region Camber
            #region Front Left Camber
            FrontLeftCamber1Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2D08");
                FrontLeftCamber2Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2C88");
                FrontLeftCamber3Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2C08");
                FrontLeftCamber4Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2B88");
                FrontLeftCamber5Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2B08");
                FrontLeftCamber6Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2A88");
                FrontLeftCamber7Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2A08");
                FrontLeftCamber8Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2988");
                FrontLeftCamber9Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2908");
                FrontLeftCamber10Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2888");
                FrontLeftCamber11Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2808");
                FrontLeftCamber12Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2788");
                FrontLeftCamber13Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2708");
                FrontLeftCamber14Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2688");
                FrontLeftCamber15Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2608");
                FrontLeftCamber16Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2588");
                FrontLeftCamber17Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2508");
                FrontLeftCamber18Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2488");
                FrontLeftCamber19Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2408");
                FrontLeftCamber20Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2388");
                FrontLeftCamber21Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2288");
                FrontLeftCamber22Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2308");
                FrontLeftCamber23Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x1F88");
                FrontLeftCamber24Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x1F08");
                FrontLeftCamber25Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x1E88");
                FrontLeftCamber26Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x2088");
                #endregion
                #region Front Right Camber
                FrontRightCamber1Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x42E8");
                FrontRightCamber2Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4268");
                FrontRightCamber3Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x41E8");
                FrontRightCamber4Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4168");
                FrontRightCamber5Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x40E8");
                FrontRightCamber6Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4068");
                FrontRightCamber7Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3FE8");
                FrontRightCamber8Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3F68");
                FrontRightCamber9Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3EE8");
                FrontRightCamber10Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3E68");
                FrontRightCamber11Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3DE8");
                FrontRightCamber12Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3D68");
                FrontRightCamber13Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3CE8");
                FrontRightCamber14Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3C68");
                FrontRightCamber15Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3BE8");
                FrontRightCamber16Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3B68");
                FrontRightCamber17Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3AE8");
                FrontRightCamber18Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3A68");
                FrontRightCamber19Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x39E8");
                FrontRightCamber20Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3968");
                FrontRightCamber21Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x38E8");
                FrontRightCamber22Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3868");
                FrontRightCamber23Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x37E8");
                FrontRightCamber24Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3768");
                FrontRightCamber25Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x36E8");
                FrontRightCamber26Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x3668");
                #endregion
                #region Rear Left Camber
                RearLeftCamber1Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6EA8");
                RearLeftCamber2Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6B28");
                RearLeftCamber3Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6228");
                RearLeftCamber4Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6EA8");
                RearLeftCamber5Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6DA8");
                RearLeftCamber6Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6E28");
                RearLeftCamber7Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6D28");
                RearLeftCamber8Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6CA8");
                RearLeftCamber9Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6C28");
                RearLeftCamber10Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6BA8");
                RearLeftCamber11Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6AA8");
                RearLeftCamber12Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6A28");
                RearLeftCamber13Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x69A8");
                RearLeftCamber14Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6928");
                RearLeftCamber15Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x68A8");
                RearLeftCamber16Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6828");
                RearLeftCamber17Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x67A8");
                RearLeftCamber18Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6728");
                RearLeftCamber19Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x66A8");
                RearLeftCamber20Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6628");
                RearLeftCamber21Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x65A8");
                RearLeftCamber22Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6528");
                RearLeftCamber23Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x64A8");
                RearLeftCamber24Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6428");
                RearLeftCamber25Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x63A8");
                RearLeftCamber26Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6328");
                RearLeftCamber27Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x62A8");
                RearLeftCamber28Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x61A8");
                RearLeftCamber29Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6128");
                RearLeftCamber30Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x60A8");
                RearLeftCamber31Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x6028");
                RearLeftCamber32Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5F5C");
                #endregion
                #region Rear Right Camber
                RearRightCamber1Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x58C8");
                RearRightCamber2Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5848");
                RearRightCamber3Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x57C8");
                RearRightCamber4Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5748");
                RearRightCamber5Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x56C8");
                RearRightCamber6Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5648");
                RearRightCamber7Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x55C8");
                RearRightCamber8Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5548");
                RearRightCamber9Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x54C8");
                RearRightCamber10Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5448");
                RearRightCamber11Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x53C8");
                RearRightCamber12Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5348");
                RearRightCamber13Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x52C8");
                RearRightCamber14Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5248");
                RearRightCamber15Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x51C8");
                RearRightCamber16Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5148");
                RearRightCamber17Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x50C8");
                RearRightCamber18Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x5048");
                RearRightCamber19Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4FC8");
                RearRightCamber20Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4F48");
                RearRightCamber21Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4EC8");
                RearRightCamber22Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4E48");
                RearRightCamber23Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4DC8");
                RearRightCamber24Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4D48");
                RearRightCamber25Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4CC8");
                RearRightCamber26Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4C48");
                RearRightCamber27Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4BC8");
                RearRightCamber28Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4B48");
                RearRightCamber29Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4AC8");
                RearRightCamber30Addr = (Speedhack.BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0x4A48");
                #endregion
            #endregion
        }
        public void ClearColours()
        {
            BTN_Tyres.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Tyres.BackColor = Color.FromArgb(28, 28, 28);
            BTN_Gears.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Gears.BackColor = Color.FromArgb(28, 28, 28);
            BTN_Alignment.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Alignment.BackColor = Color.FromArgb(28, 28, 28);
            BTN_ARB.BackColor = Color.FromArgb(28, 28, 28);
            Panel_ARB.BackColor = Color.FromArgb(28, 28, 28);
            BTN_Springs.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Springs.BackColor = Color.FromArgb(28, 28, 28);
            BTN_Damping.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Damping.BackColor = Color.FromArgb(28, 28, 28);
        }
        public void ClearTabItems()
        {
            Tyres.Visible = false;
            Gears.Visible = false;
            Alignment.Visible = false;
            //Saveswapper.Visible = false;
            //LiveTuning.Visible = false;
            //Speedhack.Visible = false;
        }
        private void BTN_Tyres_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Tyres.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Tyres.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            this.TabHolder.Controls.Add(Tyres);
            Tyres.Visible = true;
        }

        private void BTN_Gears_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Gears.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Gears.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            this.TabHolder.Controls.Add(Gears);
            Gears.Visible = true;
        }

        private void BTN_Alignment_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Alignment.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Alignment.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            this.TabHolder.Controls.Add(Alignment);
            Alignment.Visible = true;
        }

        private void BTN_ARB_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_ARB.BackColor = Color.FromArgb(45, 45, 48);
            Panel_ARB.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(ARB);
            //ARB.Visible = true;
        }

        private void BTN_Springs_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Springs.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Springs.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(Springs);
            //Springs.Visible = true;
        }

        private void BTN_Damping_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Damping.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Damping.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(Damping);
            //Damping.Visible = true;
        }
        private void BTN_Aero_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Aero.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Aero.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(Damping);
            //Damping.Visible = true;
        }
        private void BTN_Brake_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Brake.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Brake.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(Damping);
            //Damping.Visible = true;
        }
        private void BTN_Differential_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_Differential.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Differential.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            //this.TabHolder.Controls.Add(Damping);
            //Damping.Visible = true;
        }
        private void BTN_Tyres_MouseEnter(object sender, EventArgs e)
        {
            if (Tyres.Visible == false)
                Panel_Tyres.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_Tyres_MouseLeave(object sender, EventArgs e)
        {
            if (Tyres.Visible == false)
                Panel_Tyres.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_Gears_MouseEnter(object sender, EventArgs e)
        {
            if (Gears.Visible == false)
                Panel_Gears.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_Gears_MouseLeave(object sender, EventArgs e)
        {
            if (Gears.Visible == false)
                Panel_Gears.BackColor = Color.FromArgb(28, 28, 28);
        }
    }
}
