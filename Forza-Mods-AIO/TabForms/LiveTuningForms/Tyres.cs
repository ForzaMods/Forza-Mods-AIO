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
    public partial class Tyres : Form
    {
        public static Tyres t;
        public static float FrontTyreVal;
        public static float RearTyreVal;
        public Tyres()
        {
            InitializeComponent();
            t = this;
        }
        public void TyreRefresh()
        {
            FrontTyreVal = MainWindow.m.ReadFloat(LiveTuning.FrontTyrePressureAddr); FrontTyreNUD.Value = Convert.ToDecimal(FrontTyreVal); FrontTyreBar.Value = Convert.ToInt32(FrontTyreVal * 10);
            RearTyreVal = MainWindow.m.ReadFloat(LiveTuning.RearTyrePressureAddr); RearTyreNUD.Value = Convert.ToDecimal(RearTyreVal); RearTyreBar.Value = Convert.ToInt32(RearTyreVal * 10);
        }
        private void FrontTyreBar_Scroll(LimitlessUI.Slider_WOC slider, float value)
        {
            FrontTyreNUD.Value = Convert.ToDecimal(FrontTyreBar.Value) / 10;
            FrontTyreVal = (float)FrontTyreNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.FrontTyrePressureAddr, "float", FrontTyreVal.ToString());
        }
        private void FrontTyreNUD_ValueChanged(object sender, EventArgs e)
        {
            FrontTyreBar.Value = Convert.ToInt32(FrontTyreVal * 10);
            RearTyreVal = (float)RearTyreNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.FrontTyrePressureAddr, "float", FrontTyreVal.ToString());
        }
        private void RearTyreBar_Scroll(LimitlessUI.Slider_WOC slider, float value)
        {
            RearTyreNUD.Value = Convert.ToDecimal(RearTyreBar.Value) / 10;
            RearTyreVal = (float)RearTyreNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.RearTyrePressureAddr, "float", RearTyreVal.ToString());
        }
        private void RearTyreNUD_ValueChanged(object sender, EventArgs e)
        {
            RearTyreBar.Value = Convert.ToInt32(RearTyreVal * 10);
            RearTyreVal = (float)RearTyreNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.RearTyrePressureAddr, "float", RearTyreVal.ToString());
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            TyreRefresh();
        }
    }
}
