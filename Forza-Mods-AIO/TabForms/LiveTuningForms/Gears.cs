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
    public partial class Gears : Form
    {
        public static Gears g;
        public static float FinalGearVal;
        public static float Gear1Val;
        public static float Gear2Val;
        public static float Gear3Val;
        public static float Gear4Val;
        public static float Gear5Val;
        public static float Gear6Val;
        public static float Gear7Val;
        public static float Gear8Val;
        public static float Gear9Val;
        public Gears()
        {
            InitializeComponent();
            g = this;
        }
        public void GearsRefresh()
        {
            FinalGearVal = MainWindow.m.ReadFloat(LiveTuning.GearFinalAddr); FinalGearNUD.Value = Convert.ToDecimal(FinalGearVal); FinalGearBar.Value = Convert.ToInt32(FinalGearVal *100);
            Gear1Val = MainWindow.m.ReadFloat(LiveTuning.Gear1Addr); Gear1NUD.Value = Convert.ToDecimal(Gear1Val); Gear1Bar.Value = Convert.ToInt32(Gear1Val * 100);
            Gear2Val = MainWindow.m.ReadFloat(LiveTuning.Gear2Addr); Gear2NUD.Value = Convert.ToDecimal(Gear2Val); Gear2Bar.Value = Convert.ToInt32(Gear2Val * 100);
            Gear3Val = MainWindow.m.ReadFloat(LiveTuning.Gear3Addr); Gear3NUD.Value = Convert.ToDecimal(Gear3Val); Gear3Bar.Value = Convert.ToInt32(Gear3Val * 100);
            Gear4Val = MainWindow.m.ReadFloat(LiveTuning.Gear4Addr); Gear4NUD.Value = Convert.ToDecimal(Gear4Val); Gear4Bar.Value = Convert.ToInt32(Gear4Val * 100);
            Gear5Val = MainWindow.m.ReadFloat(LiveTuning.Gear5Addr); Gear5NUD.Value = Convert.ToDecimal(Gear5Val); Gear5Bar.Value = Convert.ToInt32(Gear5Val * 100);
            Gear6Val = MainWindow.m.ReadFloat(LiveTuning.Gear6Addr); Gear6NUD.Value = Convert.ToDecimal(Gear6Val); Gear6Bar.Value = Convert.ToInt32(Gear6Val * 100);
            Gear7Val = MainWindow.m.ReadFloat(LiveTuning.Gear7Addr); Gear7NUD.Value = Convert.ToDecimal(Gear7Val); Gear7Bar.Value = Convert.ToInt32(Gear7Val * 100);
            Gear8Val = MainWindow.m.ReadFloat(LiveTuning.Gear8Addr); Gear8NUD.Value = Convert.ToDecimal(Gear8Val); Gear8Bar.Value = Convert.ToInt32(Gear8Val * 100);
            Gear9Val = MainWindow.m.ReadFloat(LiveTuning.Gear9Addr); Gear9NUD.Value = Convert.ToDecimal(Gear9Val); Gear9Bar.Value = Convert.ToInt32(Gear9Val * 100);
        }
        private void FinalGearBar_Scroll(object sender, EventArgs e)
        {
            FinalGearNUD.Value = Convert.ToDecimal(FinalGearBar.Value) / 100;
            FinalGearVal = (float)FinalGearNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.GearFinalAddr, "float", FinalGearVal.ToString());
        }
        private void FinalGearNUD_ValueChanged(object sender, EventArgs e)
        {
            FinalGearBar.Value = Convert.ToInt32(FinalGearNUD.Value * 100);
            FinalGearVal = (float)FinalGearNUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.GearFinalAddr, "float", FinalGearVal.ToString());
        }
        private void Gear1Bar_Scroll(object sender, EventArgs e)
        {
            Gear1NUD.Value = Convert.ToDecimal(Gear1Bar.Value) / 100;
            Gear1Val = (float)Gear1NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear1Addr, "float", Gear1Val.ToString());
        }
        private void Gear1NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear1Bar.Value = Convert.ToInt32(Gear1NUD.Value * 100);
            Gear1Val = (float)Gear1NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear1Addr, "float", Gear1Val.ToString());
        }
        private void Gear2Bar_Scroll(object sender, EventArgs e)
        {
            Gear2NUD.Value = Convert.ToDecimal(Gear2Bar.Value) / 100;
            Gear2Val = (float)Gear2NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear2Addr, "float", Gear2Val.ToString());
        }
        private void Gear2NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear2Bar.Value = Convert.ToInt32(Gear2NUD.Value * 100);
            Gear2Val = (float)Gear2NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear2Addr, "float", Gear2Val.ToString());
        }
        private void Gear3Bar_Scroll(object sender, EventArgs e)
        {
            Gear3NUD.Value = Convert.ToDecimal(Gear3Bar.Value) / 100;
            Gear3Val = (float)Gear3NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear3Addr, "float", Gear3Val.ToString());
        }
        private void Gear3NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear3Bar.Value = Convert.ToInt32(Gear3NUD.Value * 100);
            Gear3Val = (float)Gear3NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear3Addr, "float", Gear3Val.ToString());
        }
        private void Gear4Bar_Scroll(object sender, EventArgs e)
        {
            Gear4NUD.Value = Convert.ToDecimal(Gear4Bar.Value) / 100;
            Gear4Val = (float)Gear4NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear4Addr, "float", Gear4Val.ToString());
        }
        private void Gear4NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear4Bar.Value = Convert.ToInt32(Gear4NUD.Value * 100);
            Gear4Val = (float)Gear4NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear4Addr, "float", Gear4Val.ToString());
        }
        private void Gear5Bar_Scroll(object sender, EventArgs e)
        {
            Gear5NUD.Value = Convert.ToDecimal(Gear5Bar.Value) / 100;
            Gear5Val = (float)Gear5NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear5Addr, "float", Gear5Val.ToString());
        }
        private void Gear5NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear5Bar.Value = Convert.ToInt32(Gear5NUD.Value * 100);
            Gear5Val = (float)Gear5NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear5Addr, "float", Gear5Val.ToString());
        }
        private void Gear6Bar_Scroll(object sender, EventArgs e)
        {
            Gear6NUD.Value = Convert.ToDecimal(Gear6Bar.Value) / 100;
            Gear6Val = (float)Gear6NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear6Addr, "float", Gear6Val.ToString());
        }
        private void Gear6NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear6Bar.Value = Convert.ToInt32(Gear6NUD.Value * 100);
            Gear6Val = (float)Gear6NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear6Addr, "float", Gear6Val.ToString());
        }
        private void Gear7Bar_Scroll(object sender, EventArgs e)
        {
            Gear7NUD.Value = Convert.ToDecimal(Gear7Bar.Value) / 100;
            Gear7Val = (float)Gear7NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear7Addr, "float", Gear7Val.ToString());
        }
        private void Gear7NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear7Bar.Value = Convert.ToInt32(Gear7NUD.Value * 100);
            Gear7Val = (float)Gear7NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear7Addr, "float", Gear7Val.ToString());
        }
        private void Gear8Bar_Scroll(object sender, EventArgs e)
        {
            Gear8NUD.Value = Convert.ToDecimal(Gear8Bar.Value) / 100;
            Gear8Val = (float)Gear8NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear8Addr, "float", Gear8Val.ToString());
        }
        private void Gear8NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear8Bar.Value = Convert.ToInt32(Gear8NUD.Value * 100);
            Gear8Val = (float)Gear8NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear8Addr, "float", Gear8Val.ToString());
        }
        private void Gear9Bar_Scroll(object sender, EventArgs e)
        {
            Gear9NUD.Value = Convert.ToDecimal(Gear9Bar.Value) / 100;
            Gear9Val = (float)Gear9NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear9Addr, "float", Gear9Val.ToString());
        }
        private void Gear9NUD_ValueChanged(object sender, EventArgs e)
        {
            Gear9Bar.Value = Convert.ToInt32(Gear9NUD.Value * 100);
            Gear9Val = (float)Gear9NUD.Value;
            MainWindow.m.WriteMemory(LiveTuning.Gear9Addr, "float", Gear9Val.ToString());
        }
        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            GearsRefresh();
        }
    }
}
