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
    public partial class Aero : Form
    {
        public static Aero a;
        public static float FrontAeroVal1;
        public static float FrontAeroVal2;
        public static float RearAeroVal1;
        public static float RearAeroVal2;

        public static bool refresh = false;
        public Aero()
        {
            InitializeComponent();
            a = this;
        }
        private void Aero_Load(object sender, EventArgs e)
        {
            TyreRefresh();
        }
        public void TyreRefresh()
        {
            refresh = true;
            FrontAeroVal1 = MainWindow.m.ReadFloat(LiveTuning.FrontAeroAddr1, round: false); float temp1 = (float)(FrontAeroVal1 - 0.0000036960244535639f) / 0.0000216469f; FrontTyreNUD.Value = Decimal.Truncate(Convert.ToDecimal(Math.Pow(temp1, 0.99880143827407111466240511386336))); FrontTyreBar.Value = Convert.ToInt32(FrontTyreNUD.Value);
            RearAeroVal1 = MainWindow.m.ReadFloat(LiveTuning.RearAeroAddr1, round: false); float temp2 = (float)(RearAeroVal1 - 0.00000405121540567f) / 0.0000217005f; RearTyreNUD.Value = Decimal.Truncate(Convert.ToDecimal(Math.Pow(temp2, 0.99927052938724468528553563984994))); RearTyreBar.Value = Convert.ToInt32(RearTyreNUD.Value);
            refresh = false;
        }
        private void FrontTyreBar_Scroll(object sender, EventArgs e)
        {
            if (!refresh)
            {
                FrontTyreNUD.Value = Convert.ToDecimal(FrontTyreBar.Value);
                FrontAeroVal1 = (float)(0.0000216469 * Math.Pow((float)FrontTyreBar.Value, 1.0012) + 0.0000036960244535639);
                FrontAeroVal2 = (float)(0.000002835987870591 * Math.Pow((float)FrontTyreBar.Value, 1.07359) + 0.0000167811);
                MainWindow.m.WriteMemory(LiveTuning.FrontAeroAddr1, "float", FrontAeroVal1.ToString());
                MainWindow.m.WriteMemory(LiveTuning.FrontAeroAddr2, "float", FrontAeroVal2.ToString());
            }
        }
        private void FrontTyreNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                FrontTyreBar.Value = Convert.ToInt32(FrontTyreNUD.Value);
                FrontAeroVal1 = (float)(0.0000216469 * Math.Pow((float)FrontTyreNUD.Value, 1.0012) + 0.0000036960244535639);
                FrontAeroVal2 = (float)(0.000002835987870591 * Math.Pow((float)FrontTyreNUD.Value, 1.07359) + 0.0000167811);
                MainWindow.m.WriteMemory(LiveTuning.FrontAeroAddr1, "float", FrontAeroVal1.ToString());
                MainWindow.m.WriteMemory(LiveTuning.FrontAeroAddr2, "float", FrontAeroVal2.ToString());
            }
        }
        private void RearTyreBar_Scroll(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearTyreNUD.Value = Convert.ToDecimal(RearTyreBar.Value);
                RearAeroVal1 = (float)(0.0000217005 * Math.Pow((float)RearTyreBar.Value, 1.00073) + 0.00000405121540567);
                RearAeroVal2 = (float)(0.000005722553881182 * Math.Pow((float)RearTyreBar.Value, 1.04474) - 0.00050714);
                MainWindow.m.WriteMemory(LiveTuning.RearAeroAddr1, "float", RearAeroVal1.ToString());
                MainWindow.m.WriteMemory(LiveTuning.RearAeroAddr2, "float", RearAeroVal2.ToString());
            }
        }
        private void RearTyreNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!refresh)
            {
                RearTyreBar.Value = Convert.ToInt32(RearTyreNUD.Value);
                RearAeroVal1 = (float)(0.0000217005 * Math.Pow((float)RearTyreNUD.Value, 1.00073) + 0.00000405121540567);
                RearAeroVal2 = (float)(0.000005722553881182 * Math.Pow((float)RearTyreNUD.Value, 1.04474) - 0.00050714);
                MainWindow.m.WriteMemory(LiveTuning.RearAeroAddr1, "float", RearAeroVal1.ToString());
                MainWindow.m.WriteMemory(LiveTuning.RearAeroAddr2, "float", RearAeroVal2.ToString());
            }
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            TyreRefresh();
        }
    }
}
