using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;

namespace Forza_Mods_AIO
{
    public partial class MainWindow : Form
    {
        Mem m = new Mem();

        public MainWindow()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
