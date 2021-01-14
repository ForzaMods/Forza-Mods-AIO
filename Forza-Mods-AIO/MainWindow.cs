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

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
        //dragging functionality
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        //end of dragging functionality

        //used to clear all the colours before setting accent and highlight for the tab
        private void ClearColours()
        {
            BTN_TabInfo.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Info.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabAddCars.BackColor = Color.FromArgb(28, 28, 28);
            Panel_AddCars.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabStatsEditor.BackColor = Color.FromArgb(28, 28, 28);
            Panel_StatsEditor.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabSaveswap.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Saveswap.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabLiveTuning.BackColor = Color.FromArgb(28, 28, 28);
            Panel_LiveTuning.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabSpeedhack.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Speedhack.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void ClearTabItems()
        {
            Tab_1Info.Hide();
            Tab_2AddCars.Hide();
            Tab_3StatsEditor.Hide();
            Tab_4Saveswap.Hide();
            Tab_5LiveTuning.Hide();
            Tab_6Speedhack.Hide();
        }
        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_TabInfo_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabInfo.BackColor = Color.FromArgb(45,45,48);
            Panel_Info.BackColor = Color.FromArgb(150,11,166);
            ClearTabItems();
            Tab_1Info.Show();
        }

        private void BTN_TabAddCars_Click(object sender, EventArgs e)
        {
            //do colours and hide/show ui
            ClearColours();
            BTN_TabAddCars.BackColor = Color.FromArgb(45, 45, 48);
            Panel_AddCars.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_2AddCars.Show();
        }

        private void BTN_TabStatsEditor_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabStatsEditor.BackColor = Color.FromArgb(45, 45, 48);
            Panel_StatsEditor.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_3StatsEditor.Show();
        }

        private void BTN_TabSaveswap_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabSaveswap.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Saveswap.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_4Saveswap.Show();
        }

        private void BTN_TabLiveTuning_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabLiveTuning.BackColor = Color.FromArgb(45, 45, 48);
            Panel_LiveTuning.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_5LiveTuning.Show();
        }

        private void BTN_TabSpeedhack_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabSpeedhack.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Speedhack.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_6Speedhack.Show();
        }


    }
}
