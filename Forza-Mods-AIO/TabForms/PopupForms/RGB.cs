using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public static RGB r;
        string Game;
        public RGB()
        {
            InitializeComponent();
            r = this;
        }
        private void RGB_Load(object sender, EventArgs e)
        {
            if (MainWindow.main.ForzaFour)
                Game = "ForzaHorizon4";
            else
                Game = "ForzaHorizon5";
            if (Owner != null)
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);
            if(MainWindow.main.ForzaFour)
            {
                RedBar.Value = MainWindow.m.ReadFloat(Speedhack.WorldRGBAddr, round: false) * 1000000000000;
                GreenBar.Value = MainWindow.m.ReadFloat((Speedhack.WorldRGBAddrLong + 4).ToString("X"), round: false) * 1000000000000;
                BlueBar.Value = MainWindow.m.ReadFloat((Speedhack.WorldRGBAddrLong + 8).ToString("X"), round: false) * 1000000000000;
            }
            else
            {
                if(Process.GetProcessesByName(Game)[0].MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                {
                    RedBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960).ToString("X"), round: false) * 1000000000000;
                    GreenBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 4).ToString("X"), round: false) * 1000000000000;
                    BlueBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 8).ToString("X"), round: false) * 1000000000000;
                }
                else
                {
                    RedBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968).ToString("X"), round: false) * 1000000000000;
                    GreenBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 4).ToString("X"), round: false) * 1000000000000;
                    BlueBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 8).ToString("X"), round: false) * 1000000000000;
                }
            }
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
            RedBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960).ToString("X"), round: false) * 1000000000000;
        }

        private void GreenBar_OnValueChanged(LimitlessUI.Slider_WOC slider, float value)
        {
            GreenBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 4).ToString("X"), round: false) * 1000000000000;
        }

        private void BlueBar_OnValueChanged(LimitlessUI.Slider_WOC slider, float value)
        {
            BlueBar.Value = MainWindow.m.ReadFloat(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 8).ToString("X"), round: false) * 1000000000000;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            RedBar.Value = (float)3.921569E+09;
            GreenBar.Value = (float)3.921569E+09;
            BlueBar.Value = (float)3.921569E+09;
            if (MainWindow.main.ForzaFour)
            {
                MainWindow.m.WriteMemory(Speedhack.WorldRGBAddr, "float", ((float)3.921569E+09 / 1000000000000).ToString());
                MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 4).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 8).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
            }
            else
            {
                if (Process.GetProcessesByName(Game)[0].MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                {
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 4).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 8).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320 + 4).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320 + 8).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                }
                else
                {
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 4).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 8).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448 + 4).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                    MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448 + 8).ToString("X"), "float", ((float)3.921569E+09 / 1000000000000).ToString());
                }
            }
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void SunAids_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (SunAids.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                SunAidsWorker.RunWorkerAsync();
                RedBar.Enabled = false;
                GreenBar.Enabled = false;
                BlueBar.Enabled = false;
            }
            else
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                SunAidsWorker.CancelAsync();
                RedBar.Enabled = true;
                GreenBar.Enabled = true;
                BlueBar.Enabled = true;
            }
        }

        private void SunAidsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            float i = 0;
            while (SunAids.Checked)
            {
                Color rainbow = Rainbow(i);
                RedBar.Value = ((float)rainbow.R / 255) * (float)1E+10;
                GreenBar.Value = ((float)rainbow.G / 255) * (float)1E+10;
                BlueBar.Value = ((float)rainbow.B / 255) * (float)1E+10;
                if (MainWindow.main.ForzaFour)
                {
                    MainWindow.m.WriteMemory(Speedhack.WorldRGBAddr, "float", (RedBar.Value / 1000000000000).ToString());
                    MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
                    MainWindow.m.WriteMemory((Speedhack.WorldRGBAddrLong + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
                }
                else
                {
                    if (Process.GetProcessesByName(Game)[0].MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                    {
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960).ToString("X"), "float", (RedBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 147792960 + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320).ToString("X"), "float", (RedBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320 + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 96632320 + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
                    }
                    else
                    {
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968).ToString("X"), "float", (RedBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 151675968 + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448).ToString("X"), "float", (RedBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448 + 4).ToString("X"), "float", (GreenBar.Value / 1000000000000).ToString());
                        MainWindow.m.WriteMemory(((long)Process.GetProcessesByName(Game)[0].MainModule.BaseAddress + 99480448 + 8).ToString("X"), "float", (BlueBar.Value / 1000000000000).ToString());
                    }
                }
                Thread.Sleep(10);
                i += (float)(AidsSpeed.Value / 10000);
                if (SunAidsWorker.CancellationPending)
                    e.Cancel = true;
            }
        }

        public static Color Rainbow(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }
    }
}
