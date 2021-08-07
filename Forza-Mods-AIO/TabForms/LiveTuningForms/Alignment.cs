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
        public static bool refresh = false;
        public Alignment()
        {
            InitializeComponent();
            a = this;
        }
        public void SetFrontCamber()
        {
            #region Front Left
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber1Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00871621)) - Convert.ToDecimal(0.0431509)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber2Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00871833)) - Convert.ToDecimal(0.0371244)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber3Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872007)) - Convert.ToDecimal(0.031837)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber4Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872169)) - Convert.ToDecimal(0.0271458)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber5Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872212)) - Convert.ToDecimal(0.0229444)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber6Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872301)) - Convert.ToDecimal(0.0191538)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber7Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872322)) - Convert.ToDecimal(0.015713)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber8Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872357)) - Convert.ToDecimal(0.0125718)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber9Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872409)) - Convert.ToDecimal(0.009688)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber10Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872444)) - Convert.ToDecimal(0.0070312)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber11Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872413)) - Convert.ToDecimal(0.0045742)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber12Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087241)) - Convert.ToDecimal(0.0022942)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber13Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872415)) - Convert.ToDecimal(0.000172)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber14Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872441)) + Convert.ToDecimal(0.0018032)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber15Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872424)) + Convert.ToDecimal(0.0036484)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber16Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872419)) + Convert.ToDecimal(0.0053728)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber17Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087244)) + Convert.ToDecimal(0.0069844)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber18Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872409)) + Convert.ToDecimal(0.008493)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber19Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087239)) + Convert.ToDecimal(0.0099024)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber20Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872363)) + Convert.ToDecimal(0.0112172)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber21Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872351)) + Convert.ToDecimal(0.0124412)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber22Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872399)) + Convert.ToDecimal(0.0135738)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber23Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872332)) + Convert.ToDecimal(0.0146208)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber24Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00878616)) + Convert.ToDecimal(0.0157508)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber25Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872306)) + Convert.ToDecimal(0.0164402)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontLeftCamber26Addr, "float", Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872263)) + Convert.ToDecimal(0.0172082)).ToString());
            #endregion
            #region Front Right
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber1Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00871621)) - Convert.ToDecimal(0.0431509))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber2Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00871833)) - Convert.ToDecimal(0.0371244))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber3Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872007)) - Convert.ToDecimal(0.031837))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber4Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872169)) - Convert.ToDecimal(0.0271458))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber5Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872212)) - Convert.ToDecimal(0.0229444))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber6Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872301)) - Convert.ToDecimal(0.0191538))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber7Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872322)) - Convert.ToDecimal(0.015713))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber8Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872357)) - Convert.ToDecimal(0.0125718))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber9Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872409)) - Convert.ToDecimal(0.009688))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber10Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872444)) - Convert.ToDecimal(0.0070312))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber11Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872413)) - Convert.ToDecimal(0.0045742))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber12Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087241)) - Convert.ToDecimal(0.0022942))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber13Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872415)) - Convert.ToDecimal(0.000172))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber14Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872441)) + Convert.ToDecimal(0.0018032))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber15Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872424)) + Convert.ToDecimal(0.0036484))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber16Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872419)) + Convert.ToDecimal(0.0053728))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber17Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087244)) + Convert.ToDecimal(0.0069844))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber18Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872409)) + Convert.ToDecimal(0.008493))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber19Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.0087239)) + Convert.ToDecimal(0.0099024))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber20Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872363)) + Convert.ToDecimal(0.0112172))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber21Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872351)) + Convert.ToDecimal(0.0124412))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber22Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872399)) + Convert.ToDecimal(0.0135738))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber23Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872332)) + Convert.ToDecimal(0.0146208))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber24Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00878616)) + Convert.ToDecimal(0.0157508))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber25Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872306)) + Convert.ToDecimal(0.0164402))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.FrontRightCamber26Addr, "float", (0 - Convert.ToSingle((FrontCamberNUD.Value * Convert.ToDecimal(0.00872263)) + Convert.ToDecimal(0.0172082))).ToString());
            #endregion
        }
        public void SetRearCamber()
        {
            #region Rear Left
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber1Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00864161)) - Convert.ToDecimal(0.135588)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber2Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00866471)) - Convert.ToDecimal(0.11511)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber3Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00868118)) - Convert.ToDecimal(0.097658)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber4Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00869279)) - Convert.ToDecimal(0.082588)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber5Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00870235)) - Convert.ToDecimal(0.06946)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber6Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00870853)) - Convert.ToDecimal(0.057952)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber7Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871353)) - Convert.ToDecimal(0.047816)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber8Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871978)) - Convert.ToDecimal(0.0309478)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber9Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872157)) - Convert.ToDecimal(0.0239562)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber10Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872247)) - Convert.ToDecimal(0.0177876)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber11Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087235)) - Convert.ToDecimal(0.012372)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber12Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872385)) - Convert.ToDecimal(0.0076452)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber13Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872413)) - Convert.ToDecimal(0.0035642)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber14Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872462)) - Convert.ToDecimal(0.0000545914)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber15Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872457)) + Convert.ToDecimal(0.0028042)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber16Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872431)) + Convert.ToDecimal(0.0051334)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber17Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872428)) + Convert.ToDecimal(0.0069038)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber18Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872416)) + Convert.ToDecimal(0.0081136)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber19Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872343)) + Convert.ToDecimal(0.00875)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber20Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872354)) + Convert.ToDecimal(0.0087808)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber21Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872328)) + Convert.ToDecimal(0.0081652)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber22Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872347)) + Convert.ToDecimal(0.006836)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber23Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872334)) + Convert.ToDecimal(0.0046994)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber24Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087231)) + Convert.ToDecimal(0.0016134)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber25Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872184)) - Convert.ToDecimal(0.0083574)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber26Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872151)) - Convert.ToDecimal(0.0161206)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber27Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087189)) - Convert.ToDecimal(0.026927)).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearLeftCamber28Addr, "float", Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871331)) - Convert.ToDecimal(0.0431266 )).ToString());
            #endregion
            #region Rear Right
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber1Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00864161)) - Convert.ToDecimal(0.135588))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber2Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00866471)) - Convert.ToDecimal(0.11511))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber3Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00868118)) - Convert.ToDecimal(0.097658))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber4Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00869279)) - Convert.ToDecimal(0.082588))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber5Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00870235)) - Convert.ToDecimal(0.06946))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber6Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00870853)) - Convert.ToDecimal(0.057952))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber7Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871353)) - Convert.ToDecimal(0.047816))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber8Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871978)) - Convert.ToDecimal(0.0309478))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber9Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872157)) - Convert.ToDecimal(0.0239562))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber10Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872247)) - Convert.ToDecimal(0.0177876))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber11Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087235)) - Convert.ToDecimal(0.012372))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber12Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872385)) - Convert.ToDecimal(0.0076452))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber13Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872413)) - Convert.ToDecimal(0.0035642))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber14Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872462)) - Convert.ToDecimal(0.0000545914))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber15Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872457)) + Convert.ToDecimal(0.0028042))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber16Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872431)) + Convert.ToDecimal(0.0051334))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber17Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872428)) + Convert.ToDecimal(0.0069038))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber18Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872416)) + Convert.ToDecimal(0.0081136))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber19Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872343)) + Convert.ToDecimal(0.00875))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber20Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872354)) + Convert.ToDecimal(0.0087808))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber21Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872328)) + Convert.ToDecimal(0.0081652))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber22Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872347)) + Convert.ToDecimal(0.006836))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber23Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872334)) + Convert.ToDecimal(0.0046994))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber24Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087231)) + Convert.ToDecimal(0.0016134))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber25Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872184)) - Convert.ToDecimal(0.0083574))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber26Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00872151)) - Convert.ToDecimal(0.0161206))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber27Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.0087189)) - Convert.ToDecimal(0.026927))).ToString());
            MainWindow.m.WriteMemory(LiveTuning.RearRightCamber28Addr, "float", (0 - Convert.ToSingle((RearCamberNUD.Value * Convert.ToDecimal(0.00871331)) - Convert.ToDecimal(0.0431266))).ToString());
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
            refresh = true;
            FrontCamberVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.FrontLeftCamber1Addr, round: false) + 0.0431509) / 0.00871621); FrontCamberNUD.Value = Convert.ToDecimal(FrontCamberVal); FrontCamberBar.Value = Convert.ToInt32(FrontCamberVal * 10);
            RearCamberVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.RearLeftCamber1Addr, round: false) + 0.135588) / 0.00864161); RearCamberNUD.Value = Convert.ToDecimal(RearCamberVal); RearCamberBar.Value = Convert.ToInt32(RearCamberVal * 10);
            refresh = false;
        }
        public void ToeRefresh()
        {
            refresh = true;
            FrontToeVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.FrontLeftToeAddr, round: false) + 0.00000001481075884) * 57.295776347772800370292061126347); FrontToeNUD.Value = Convert.ToDecimal(FrontToeVal); FrontToeBar.Value = Convert.ToInt32(FrontToeVal * 10);
            RearToeVal = Convert.ToSingle((MainWindow.m.ReadFloat(LiveTuning.RearRightToeAddr1, round: false) + 0.000002134096121) * 114.61758396931828138337574705036); RearToeNUD.Value = Convert.ToDecimal(RearToeVal); RearToeBar.Value = Convert.ToInt32(RearToeVal * 10);
            refresh = false;
        }
        private void FrontCamberBar_Scroll(object sender, EventArgs e)
        {
            if(!refresh)
            {
                FrontCamberNUD.Value = Convert.ToDecimal(FrontCamberBar.Value) / 10;
            }
        }
        private void FrontCamberNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                FrontCamberBar.Value = Convert.ToInt32(FrontCamberNUD.Value * 10);
                SetFrontCamber();
            }
        }
        private void RearCamberBar_Scroll(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearCamberNUD.Value = Convert.ToDecimal(RearCamberBar.Value) / 10;
            }
        }
        private void RearCamberNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearCamberBar.Value = Convert.ToInt32(RearCamberNUD.Value * 10);
                SetRearCamber();
            }
        }
        private void FrontToeBar_Scroll(object sender, EventArgs e)
        {
            if (!refresh)
            {
                FrontToeNUD.Value = Convert.ToDecimal(FrontToeBar.Value) / 10;
                FrontToeVal = Convert.ToSingle((FrontToeNUD.Value / Convert.ToDecimal(57.295776347772800370292061126347)) - Convert.ToDecimal(0.00000001481075884));
                NegFrontToeVal = 0 - FrontToeVal;
                SetToe();
            }
        }
        private void FrontToeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                FrontToeBar.Value = Convert.ToInt32(FrontToeNUD.Value * 10);
                FrontToeVal = Convert.ToSingle((FrontToeNUD.Value / Convert.ToDecimal(57.295776347772800370292061126347)) - Convert.ToDecimal(0.00000001481075884));
                NegFrontToeVal = 0 - FrontToeVal;
                SetToe();
            }
        }
        private void RearToeBar_Scroll(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearToeNUD.Value = Convert.ToDecimal(RearToeBar.Value) / 10;
                RearToeVal = Convert.ToSingle((RearToeNUD.Value / Convert.ToDecimal(114.61758396931828138337574705036)) - Convert.ToDecimal(0.000002134096121));
                NegRearToeVal = 0 - RearToeVal;
                SetToe();
            }
        }
        private void RearToeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearToeBar.Value = Convert.ToInt32(RearToeNUD.Value * 10);
                RearToeVal = Convert.ToSingle((RearToeNUD.Value / Convert.ToDecimal(114.61758396931828138337574705036)) - Convert.ToDecimal(0.000002134096121));
                NegRearToeVal = 0 - RearToeVal;
                SetToe();
            }
        }
        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            CamberRefresh();
            ToeRefresh();
        }
    }
}
