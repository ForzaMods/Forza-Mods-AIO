using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms.LiveTuningForms
{
    public partial class Alignment : Form
    {
        public static Alignment a;
        public static float FrontCamberVal;
        public static float RearCamberVal;
        public static float NegFrontCamberVal;
        public static float NegRearCamberVal;
        public static float FrontToeVal;
        public static float NegFrontToeVal;
        public static float RearToeVal;
        public static float NegRearToeVal;
        public Alignment()
        {
            InitializeComponent();
            a = this;
        }
        public void SetCamber()
        {
            #region Front Left
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber1Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber2Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber3Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber4Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber5Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber6Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber7Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber8Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber9Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber10Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber11Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber12Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber13Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber14Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber15Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber16Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber17Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber18Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber19Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber20Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber21Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber22Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber23Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber24Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber25Addr, "float", FrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber26Addr, "float", FrontCamberVal.ToString());
            #endregion
            #region Rear Left
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber1Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber2Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber3Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber4Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber5Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber6Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber7Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber8Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber9Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber10Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber11Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber12Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber13Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber14Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber15Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber16Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber17Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber18Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber19Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber20Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber21Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber22Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber23Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber24Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber25Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber26Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber27Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber28Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber29Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber30Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber31Addr, "float", RearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber32Addr, "float", RearCamberVal.ToString());
            #endregion
            #region Front Right
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber1Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber2Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber3Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber4Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber5Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber6Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber7Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber8Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber9Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber10Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber11Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber12Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber13Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber14Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber15Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber16Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber17Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber18Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber19Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber20Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber21Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber22Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber23Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber24Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber25Addr, "float", NegFrontCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber26Addr, "float", NegFrontCamberVal.ToString());
            #endregion
            #region Rear Right
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber1Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber2Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber3Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber4Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber5Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber6Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber7Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber8Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber9Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber10Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber11Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber12Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber13Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber14Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber15Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber16Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber17Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber18Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber19Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber20Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber21Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber22Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber23Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber24Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber25Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber26Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber27Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber28Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber29Addr, "float", NegRearCamberVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber30Addr, "float", NegRearCamberVal.ToString());
            #endregion
        }
        public void SetToe()
        {
            #region Front
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftToeAddr, "float", NegFrontToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightToeAddr, "float", FrontToeVal.ToString());
            #endregion
            #region Rear Left
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr1, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr2, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr3, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr4, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr5, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr6, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr7, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr8, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr9, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr10, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr11, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr12, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr13, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr14, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr15, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr16, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr17, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr18, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr19, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr20, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr21, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr22, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr23, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr24, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr25, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr26, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr27, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr28, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr29, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr30, "float", NegRearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftToeAddr31, "float", NegRearToeVal.ToString());
            #endregion
            #region Rear Right
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr1, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr2, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr3, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr4, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr5, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr6, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr7, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr8, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr9, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr10, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr11, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr12, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr13, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr14, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr15, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr16, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr17, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr18, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr19, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr20, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr21, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr22, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr23, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr24, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr25, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr26, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr27, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr28, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr29, "float", RearToeVal.ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightToeAddr30, "float", RearToeVal.ToString());
            #endregion
        }
        public void CamberRefresh()
        {
            FrontCamberVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.FrontLeftCamber1Addr) + 0.02725616843) * 114.60233013789604449450136702072); FrontCamberNUD.Value = Convert.ToDecimal(FrontCamberVal); FrontCamberBar.Value = Convert.ToInt32(FrontCamberVal * 10);
            RearCamberVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.RearLeftCamber1Addr) + 0.01430380344) * 114.60389582695653381140516563262); RearCamberNUD.Value = Convert.ToDecimal(RearCamberVal); RearCamberBar.Value = Convert.ToInt32(RearCamberVal * 10);
        }
        public void ToeRefresh()
        {
            FrontToeVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.FrontLeftToeAddr) + 0.00000001481075884) * 57.295776347772800370292061126347); FrontToeNUD.Value = Convert.ToDecimal(FrontToeVal); FrontToeBar.Value = Convert.ToInt32(FrontToeVal * 10);
            RearToeVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.RearRightToeAddr1) + 0.000002134096121) * 114.61758396931828138337574705036); RearToeNUD.Value = Convert.ToDecimal(RearToeVal); RearToeBar.Value = Convert.ToInt32(RearToeVal * 10);
        }
        private void FrontCamberBar_Scroll(object sender, EventArgs e)
        {
            FrontCamberNUD.Value = Convert.ToDecimal(FrontCamberBar.Value) / 10;
            FrontCamberVal = Convert.ToSingle((FrontCamberNUD.Value / Convert.ToDecimal(114.60233013789604449450136702072)) - Convert.ToDecimal(0.02725616843));
            NegFrontCamberVal = 0 - FrontCamberVal;
            SetCamber();
        }
        private void FrontCamberNUD_ValueChanged(object sender, EventArgs e)
        {
            FrontCamberBar.Value = Convert.ToInt32(FrontCamberNUD.Value * 10);
            FrontCamberVal = Convert.ToSingle((FrontCamberNUD.Value / Convert.ToDecimal(114.60233013789604449450136702072)) - Convert.ToDecimal(0.02725616843));
            NegFrontCamberVal = 0 - FrontCamberVal;
            SetCamber();
        }
        private void RearCamberBar_Scroll(object sender, EventArgs e)
        {
            RearCamberNUD.Value = Convert.ToDecimal(RearCamberBar.Value) / 10;
            RearCamberVal = Convert.ToSingle((RearCamberNUD.Value / Convert.ToDecimal(114.60389582695653381140516563262)) - Convert.ToDecimal(0.01430380344));
            NegRearCamberVal = 0 - RearCamberVal;
            SetCamber();
        }
        private void RearCamberNUD_ValueChanged(object sender, EventArgs e)
        {
            RearCamberBar.Value = Convert.ToInt32(RearCamberNUD.Value * 10);
            RearCamberVal = Convert.ToSingle((RearCamberNUD.Value / Convert.ToDecimal(114.60389582695653381140516563262)) - Convert.ToDecimal(0.01430380344));
            NegRearCamberVal = 0 - RearCamberVal;
            SetCamber();
        }
        private void FrontToeBar_Scroll(object sender, EventArgs e)
        {
            FrontToeNUD.Value = Convert.ToDecimal(FrontToeBar.Value) / 10;
            FrontToeVal = Convert.ToSingle((FrontToeNUD.Value / Convert.ToDecimal(57.295776347772800370292061126347)) - Convert.ToDecimal(0.00000001481075884));
            NegFrontToeVal = 0 - FrontToeVal;
            SetToe();
        }
        private void FrontToeNUD_ValueChanged(object sender, EventArgs e)
        {
            FrontToeBar.Value = Convert.ToInt32(FrontToeNUD.Value * 10);
            FrontToeVal = Convert.ToSingle((FrontToeNUD.Value / Convert.ToDecimal(57.295776347772800370292061126347)) - Convert.ToDecimal(0.00000001481075884));
            NegFrontToeVal = 0 - FrontToeVal;
            SetToe();
        }
        private void RearToeBar_Scroll(object sender, EventArgs e)
        {
            RearToeNUD.Value = Convert.ToDecimal(RearToeBar.Value) / 10;
            RearToeVal = Convert.ToSingle((RearToeNUD.Value / Convert.ToDecimal(114.61758396931828138337574705036)) - Convert.ToDecimal(0.000002134096121));
            NegRearToeVal = 0 - RearToeVal;
            SetToe();
        }
        private void RearToeNUD_ValueChanged(object sender, EventArgs e)
        {
            RearToeBar.Value = Convert.ToInt32(RearToeNUD.Value * 10);
            RearToeVal = Convert.ToSingle((RearToeNUD.Value / Convert.ToDecimal(114.61758396931828138337574705036)) - Convert.ToDecimal(0.000002134096121));
            NegRearToeVal = 0 - RearToeVal;
            SetToe();
        }


        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            CamberRefresh();
            ToeRefresh();
        }
    }
}
