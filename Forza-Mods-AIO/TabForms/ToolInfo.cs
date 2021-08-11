using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Forza_Mods_AIO.TabForms;
using System;
using System.Runtime.InteropServices;
using System.IO;
using IniParser;
using IniParser.Model;
using System.Drawing;
using MechanikaDesign.WinForms.UI.ColorPicker;
using System.Collections.Generic;

namespace Forza_Mods_AIO
{
    public partial class ToolInfo : Form
    {
        public bool VolStart = false;
        public float? vol = null;
        public static ToolInfo t;
        HslColor colorHsl;
        Color colorRgb;
        bool lockUpdates = false;
        public float Rainbowspeed;
        public string RainbowColour;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public ToolInfo()
        {
            t = this;
            InitializeComponent();
            VersionLabel.Text += MainWindow.CurrVer.ToString();
            File.Delete(Path.Combine(Path.GetTempPath(), "FH4_Stats.csv"));
            File.Delete(Path.Combine(Path.GetTempPath(), "FH4_Cars.csv"));
            //Mute.Enabled = false;
        }

        private void DraffsYTLink_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.youtube.com/channel/UCwQ8XprkEbBJ3UaBYT_F8jA");
        }
        private void UCPostLink_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.unknowncheats.me/forum/other-games/415227-fh4-speed-hack.html");
        }
        private void DiscordLink_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://discord.gg/PQNxeYWUy9");
        }

        private void Mute_CheckedChanged(object sender, EventArgs e)
        {
            if(Mute.Checked == false)
            {
                VolStart = false;
                //((Telerik.WinControls.Primitives.FillPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = Color.FromArgb(45, 45, 48);
                ((Telerik.WinControls.Primitives.BorderPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                Volumeworker.CancelAsync();
                try
                {
                    VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                    VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                }
                catch { }
            }
            else
            {
                //((Telerik.WinControls.Primitives.FillPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                ((Telerik.WinControls.Primitives.BorderPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                VolStart = true;
                Volumeworker.RunWorkerAsync();
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
        }

        private void Volumeworker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool g2g = false;
            while (VolStart)
            {
                try
                { var yeet = MainWindow.m.ReadByte(Speedhack.PastIntroAddr); g2g = true; }
                catch
                { g2g = false; }
                while (MainWindow.m.OpenProcess("ForzaHorizon4") && Speedhack.PastIntroAddr != null && Mute.Enabled && g2g)
                {
                    var yeet2 = MainWindow.m.ReadByte(Speedhack.PastIntroAddr);
                    if (MainWindow.m.ReadByte(Speedhack.PastIntroAddr) == 1)
                    {
                        if ((DateTime.Now - Process.GetProcessesByName("ForzaHorizon4")[0].StartTime).TotalSeconds >= 25)
                        {
                            VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                            VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                        }
                    }
                    else
                    {
                        while (MainWindow.m.ReadByte(Speedhack.PastIntroAddr) == 0)
                        {
                            if ((DateTime.Now - Process.GetProcessesByName("ForzaHorizon4")[0].StartTime).TotalSeconds >= 25)
                            {
                                VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, true);
                                VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                            }
                            if (Volumeworker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            Thread.Sleep(1);
                        }
                        Thread.Sleep(20000);
                        if ((DateTime.Now - Process.GetProcessesByName("ForzaHorizon4")[0].StartTime).TotalSeconds >= 25)
                        {
                            VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                            VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                        }
                    }
                    if (Volumeworker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    Thread.Sleep(1);
                }
                if (Volumeworker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(1);
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
        private void ColourPickerBox_ToggleStateChanged(object sender, EventArgs e)
        {
            if(ColourPickerBox.Checked)
            {
                if (!lockUpdates)
                {
                    ColourPicker.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ColourSlider.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ColourPicker.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                    ColourSlider.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                }
                //((Telerik.WinControls.Primitives.FillPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                ((Telerik.WinControls.Primitives.BorderPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                RainbowBox.Visible = false;
                RainbowSpeed.Visible = false;
                ColourPicker.Visible = true;
                ColourSlider.Visible = true;
            }
            else
            {
                //((Telerik.WinControls.Primitives.FillPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = Color.FromArgb(45, 45, 48);
                ((Telerik.WinControls.Primitives.BorderPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                RainbowBox.Visible = true;
                RainbowSpeed.Visible = true;
                ColourPicker.Visible = false;
                ColourSlider.Visible = false;
            }
        }

        private void ColourSlider_ColorChanged(object sender, MechanikaDesign.WinForms.UI.ColorPicker.ColorChangedEventArgs args)
        {
            if(!lockUpdates)
            {
                HslColor colorHSL = ColourSlider.ColorHSL;
                colorHsl = colorHSL;
                colorRgb = colorHsl.RgbValue;
                lockUpdates = true;
                ColourPicker.ColorHSL = colorHsl;
                lockUpdates = false;
                MainWindow.ThemeColour = ColorTranslator.ToHtml(colorRgb);
                UpdateThemeColour(ColorTranslator.FromHtml(MainWindow.ThemeColour));
            }
        }

        private void ColourPicker_ColorChanged(object sender, ColorChangedEventArgs args)
        {
            if(!lockUpdates)
            {
                HslColor colorHSL = ColourPicker.ColorHSL;
                colorHsl = colorHSL;
                colorRgb = colorHsl.RgbValue;
                lockUpdates = true;
                ColourSlider.ColorHSL = colorHsl;
                lockUpdates = false;
                MainWindow.ThemeColour = ColorTranslator.ToHtml(colorRgb);
                UpdateThemeColour(ColorTranslator.FromHtml(MainWindow.ThemeColour));
            }
        }
        public void UpdateThemeColour(Color color)
        {
            int bow = 0;
            if(RainbowBox.Checked)
            {
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = "Rainbow";
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
            else if (ColourPickerBox.Checked)
            {
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
                //((Telerik.WinControls.Primitives.FillPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = color;
                ((Telerik.WinControls.Primitives.BorderPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            }
            else
            {
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = SettingsParser.ReadFile(SettingsPath);
                string TC = Settings["Settings"]["Theme Colour"]; MainWindow.ThemeColour = TC;
            }
            if (RainbowBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)RainbowBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Mute.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor = color;
            /*
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            if (luminance > 0.5)
                bow = 0;
            else
                bow = 255;
            ((Telerik.WinControls.Primitives.CheckPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2)).ForeColor = Color.FromArgb(bow, bow, bow);
            ((Telerik.WinControls.Primitives.CheckPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2)).ForeColor = Color.FromArgb(bow, bow, bow);
            ((Telerik.WinControls.Primitives.CheckPrimitive)RainbowBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2)).ForeColor = Color.FromArgb(bow, bow, bow);
            */
            if(MainWindow.main.ToolInfo.Visible)
                MainWindow.main.Panel_Info.BackColor = color;
            if (MainWindow.main.AddCars.Visible)
                MainWindow.main.Panel_AddCars.BackColor = color;
            if (MainWindow.main.StatsEditor.Visible)
                MainWindow.main.Panel_StatsEditor.BackColor = color;
            if (MainWindow.main.Saveswapper.Visible)
                MainWindow.main.Panel_Saveswap.BackColor = color;
            if (MainWindow.main.LiveTuning.Visible)
                MainWindow.main.Panel_LiveTuning.BackColor = color;
            if (MainWindow.main.speedhack.Visible)
                MainWindow.main.Panel_Speedhack.BackColor = color;
            if(Speedhack.s.TB_SHCarNoClip.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TB_SHCarNoClip.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TB_SHWallNoClip.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TB_SHWallNoClip.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.VelHackButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.VelHackButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.WheelSpeedButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.WheelSpeedButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TurnAssistButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TurnAssistButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.SuperBreakButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.SuperBreakButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.StopAllWheelsButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.StopAllWheelsButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.CheckpointBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.CheckpointBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.AutoWayPoint.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.AutoWayPoint.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TimeCheckBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TimeCheckBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TimerButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TimerButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.Bypassoob.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.Bypassoob.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.SuperCarBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.SuperCarBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            DraffsYTLink.ForeColor = color;
            UCPostLink.ForeColor = color;
            DiscordLink.ForeColor = color;
        }

        private void RainbowBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RainbowBox.Checked)
            {
                ColourPickerBox.Enabled = false;
                RainbowSpeed.Enabled = true;
                RainbowWorker.RunWorkerAsync();
            }
            else
            {
                //((Telerik.WinControls.Primitives.FillPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = Color.FromArgb(45, 45, 48);
                ((Telerik.WinControls.Primitives.BorderPrimitive)RainbowBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = RainbowColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
                MainWindow.ThemeColour = RainbowColour;
                UpdateThemeColour(ColorTranslator.FromHtml(RainbowColour));
                ColourPickerBox.Enabled = true;
                RainbowSpeed.Enabled = false;
                RainbowWorker.CancelAsync();
            }
        }

        private void RainbowWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            float i = 0;
            while (RainbowBox.Checked)
            {
                Color rainbow = Rainbow(i);
                t.UpdateThemeColour(rainbow);
                Thread.Sleep(11);
                i += (float)(Rainbowspeed / 10000);
                RainbowColour = ColorTranslator.ToHtml(rainbow);
                if (RainbowWorker.CancellationPending)
                    e.Cancel = true;
            }
        }

        private void RainbowSpeed_ValueChanged(object sender, EventArgs e)
        {
            Rainbowspeed = (float)RainbowSpeed.Value;
        }
    }
}
