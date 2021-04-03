using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Windows.Forms;
using Memory;
using GlobalLowLevelHooks;
using IniParser;
using IniParser.Model;
using Forza_Mods_AIO.TabForms;

namespace Forza_Mods_AIO
{
    public partial class AddCars : Form
    {
        public AddCars()
        {
            InitializeComponent();
        }
        Mem m = new Mem();
        private void TB_ACAutoshow_CheckStateChanged(object sender, EventArgs e)
        {
            if (TB_ACAutoshow.Checked == false)
            {
                m.WriteMemory("base+4C4B7EC", "string", "0");
            }
            else
            {
                m.WriteMemory("base+4C4B7EC", "string", "1");
            }
        }
        private void TB_RemoveCars_CheckedChanged(object sender, EventArgs e)
        {
            if (TB_RemoveCars.Checked == false)
            {
                m.WriteMemory("base+4C91934", "string", "t");
                m.WriteMemory("base+4C238ED", "string", "n");
            }
            else
            {
                m.WriteMemory("base+4C91934", "string", "6");
                m.WriteMemory("base+4C238ED", "string", "9");
            }
        }

        private void AddCars_Load(object sender, EventArgs e)
        {

        }
    }
}
