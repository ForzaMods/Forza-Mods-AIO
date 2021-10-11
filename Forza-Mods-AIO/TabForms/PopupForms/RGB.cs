using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms.PopupForms
{
    public partial class RGB : Form
    {
        public RGB()
        {
            InitializeComponent();
        }
        private void RGB_Load(object sender, EventArgs e)
        {
            Left = MainWindow.main.Left + ((MainWindow.main.Width - Width) / 2);
            Top = MainWindow.main.Top + ((MainWindow.main.Height - Height) / 2);
            RedBar.Value = MainWindow.m.ReadFloat(Speedhack.WorldRGBAddr, round:false) * 1000000000000;
            GreenBar.Value = MainWindow.m.ReadFloat((Speedhack.WorldRGBAddrLong + 4).ToString("X"), round: false) * 1000000000000;
            BlueBar.Value = MainWindow.m.ReadFloat((Speedhack.WorldRGBAddrLong + 8).ToString("X"), round: false) * 1000000000000;
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void TopPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TopPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TopPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = false;
        }

        private void RedBar_OnValueChanged(LimitlessUI.Slider_WOC slider, float value)
        {
            MainWindow.m.WriteMemory(Speedhack.WorldRGBAddr, "float", (RedBar.Value/1000000000000).ToString());
        }

        private void GreenBar_OnValueChanged(LimitlessUI.Slider_WOC slider, float value)
        {
            MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
        }

        private void BlueBar_OnValueChanged(LimitlessUI.Slider_WOC slider, float value)
        {
            MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            RedBar.Value = (float)3.921569E+09;
            GreenBar.Value = (float)3.921569E+09;
            BlueBar.Value = (float)3.921569E+09;
            MainWindow.m.WriteMemory(Speedhack.WorldRGBAddr, "float", (RedBar.Value / 1000000000000).ToString());
            MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
            MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
