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
using Forza_Mods_AIO.TabForms.LiveTuningForms;
using Forza_Mods_AIO.Properties;
using System.Web;
using DiscordRPC;
using Forza_Mods_AIO.TabForms.PopupForms;

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
            DonoPic.Image = Resources.bmac_grey;
            DonoPic2.Image = Resources.paypal;
            DonoPicD.Image = Resources.bmac_grey;
            DonoPic2D.Image = Resources.paypal;
            //Mute.Enabled = false;
        }

        private void DraffsYTLink_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.youtube.com/c/comamnds/");
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
            if(!Mute.Checked)
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
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
                if (!Volumeworker.IsBusy)
                    Volumeworker.RunWorkerAsync();
                else while (Volumeworker.IsBusy)
                {
                    Thread.Sleep(1);
                    if (!Volumeworker.IsBusy)
                    {
                        Volumeworker.RunWorkerAsync();
                        break;
                    }
                    if (!Mute.Checked)
                        break;
                }
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
                                VolStart = false;
                                e.Cancel = true;
                                return;
                            }
                            Thread.Sleep(10);
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
                        VolStart = false;
                        e.Cancel = true;
                        return;
                    }
                    Thread.Sleep(10);
                }
                if (Volumeworker.CancellationPending)
                {
                    VolStart = false;
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(150);
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
                SkipIntroBox.Visible = false;
                ColourPicker.Visible = true;
                ColourSlider.Visible = true;
            }
            else
            {
                //((Telerik.WinControls.Primitives.FillPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0)).BackColor = Color.FromArgb(45, 45, 48);
                ((Telerik.WinControls.Primitives.BorderPrimitive)ColourPickerBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                RainbowBox.Visible = true;
                RainbowSpeed.Visible = true;
                SkipIntroBox.Visible = true;
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
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
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
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
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
            if (RPCBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)RPCBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (RainbowBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)RainbowBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Mute.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Mute.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (SkipIntroBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)SkipIntroBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(0)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AOBScanProgress.GetChildAt(0).GetChildAt(1)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(0)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(0)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(1)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(1)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(1)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)StatsEditor.s.SendProgress.GetChildAt(0).GetChildAt(1)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(0)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(0)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(1)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(1)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(1)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)Speedhack.s.FOVScan_bar.GetChildAt(0).GetChildAt(1)).BackColor = color;
            StatsEditor.s.waitingBarIndicatorElement1.BackColor = color;
            StatsEditor.s.waitingBarIndicatorElement1.BackColor2 = color;
            StatsEditor.s.waitingBarIndicatorElement1.BackColor3 = color;
            StatsEditor.s.waitingBarIndicatorElement1.BackColor4 = color;
            StatsEditor.s.waitingBarIndicatorElement2.BackColor = color;
            StatsEditor.s.waitingBarIndicatorElement2.BackColor2 = color;
            StatsEditor.s.waitingBarIndicatorElement2.BackColor3 = color;
            StatsEditor.s.waitingBarIndicatorElement2.BackColor4 = color;
            Speedhack.s.JumpAmountBar.ForeColor = color;
            Speedhack.s.VelMultBar.ForeColor = color;
            Speedhack.s.FOVBar.ForeColor = color;
            Tyres.t.FrontTyreBar.ForeColor = color;
            Tyres.t.RearTyreBar.ForeColor = color;
            Gears.g.FinalGearBar.ForeColor = color;
            Gears.g.Gear1Bar.ForeColor = color;
            Gears.g.Gear2Bar.ForeColor = color;
            Gears.g.Gear3Bar.ForeColor = color;
            Gears.g.Gear4Bar.ForeColor = color;
            Gears.g.Gear5Bar.ForeColor = color;
            Gears.g.Gear6Bar.ForeColor = color;
            Gears.g.Gear7Bar.ForeColor = color;
            Gears.g.Gear8Bar.ForeColor = color;
            Gears.g.Gear9Bar.ForeColor = color;
            Alignment.a.FrontCamberBar.ForeColor = color;
            Alignment.a.RearCamberBar.ForeColor = color;
            Alignment.a.FrontToeBar.ForeColor = color;
            Alignment.a.RearToeBar.ForeColor = color;
            RGB.r.RedBar.ForeColor = color;
            RGB.r.GreenBar.ForeColor = color;
            RGB.r.BlueBar.ForeColor = color;
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
            if (MainWindow.main.ToolInfo.Visible)
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
            if (LiveTuning.l.Tyres.Visible)
                LiveTuning.l.Panel_Tyres.BackColor = color;
            if (LiveTuning.l.Gears.Visible)
                LiveTuning.l.Panel_Gears.BackColor = color;
            if (LiveTuning.l.Alignment.Visible)
                LiveTuning.l.Panel_Alignment.BackColor = color;
            if (Speedhack.s.TB_SHCarNoClip.Checked)
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
            if (Speedhack.s.XPBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.XPBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TimeCheckBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TimeCheckBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.TimerButton.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.TimerButton.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.Bypassoob.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.Bypassoob.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.SuperCarBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.SuperCarBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.FOV.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.FOV.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.JumpHackToggle.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.JumpHackToggle.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.WeirdSet.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.WeirdSet.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.GravitySet.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.GravitySet.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.DiscoverRoadsBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.DiscoverRoadsBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Speedhack.s.WaterBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Speedhack.s.WaterBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (Saveswapper.s.TB_Backup.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)Saveswapper.s.TB_Backup.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_AllCars.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_AllCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_RareCars.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_LegoPaint.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_LegoPaint.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_RemoveCars.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_RemoveCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_Decals.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_Decals.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_FreeCars.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_FreeCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_SeriesFix.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_SeriesFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_Traffic.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_Traffic.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_ThumbsFix.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_ThumbsFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_Presets.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_Presets.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.Box_ClearGarage.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.Box_ClearGarage.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.FreePerfBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.FreePerfBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            if (AddCars.a.FreeVisBox.Checked)
                ((Telerik.WinControls.Primitives.BorderPrimitive)AddCars.a.FreeVisBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(0)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(0)).BackColor = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(1)).BackColor2 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(1)).BackColor3 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(1)).BackColor4 = color;
            ((Telerik.WinControls.UI.ProgressIndicatorElement)AddCars.a.ScanProgress.GetChildAt(0).GetChildAt(1)).BackColor = color;
            DraffsYTLink.ForeColor = color;
            UCPostLink.ForeColor = color;
            DiscordLink.ForeColor = color;
            AddCars.a.Url.ForeColor = color;
        }
        private void RPCBox_ToggleStateChanged(object sender, EventArgs e)
        {
            if (RPCBox.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)RPCBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                MainWindow.main.RPCclient = new DiscordRpcClient("841090098837323818");
                MainWindow.main.RPCclient.Initialize();
                DiscordRPC.Button[] Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button() { Label = "Discord Link", Url = "https://discord.gg/N3m6E5V" },
                    new DiscordRPC.Button() { Label = "Download", Url = "https://github.com/Yeethan69/AIO" }
                };
                MainWindow.main.RPCclient.SetPresence(new RichPresence()
                {
                    Buttons = Buttons,
                    Details = "Reading Info",
                    State = "Being Epic",
                    Timestamps = Timestamps.Now,
                    Assets = new Assets()
                    {
                        LargeImageKey = "aio2",
                        LargeImageText = "Forza Mods AIO",
                        SmallImageKey = "home",
                        SmallImageText = "Reading Info"
                    }
                });
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
            else
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)RPCBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                MainWindow.main.RPCclient.Deinitialize();
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
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
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
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
                Thread.Sleep(10);
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

        private void RainbowBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip.Show("Sets theme to rainbow", RainbowBox);
        }

        private void DonoPic_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.buymeacoffee.com/Yeethan69");
        }
        private void DonoPic2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"https://www.paypal.com/donate?hosted_button_id=DACQKRJ4HTZRN\"");
        }
        private void DonoPic_ClickD(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://www.buymeacoffee.com/comamnds");
        }
        private void DonoPic2_ClickD(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"https://www.paypal.com/donate?hosted_button_id=H37GURADQ2SXU\"");
        }

        private void SkipIntroBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (SkipIntroBox.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)SkipIntroBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
            else
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)SkipIntroBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = Color.FromArgb(30, 30, 33);
                string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = MainWindow.ThemeColour;
                Settings["Settings"]["Rainbow Speed"] = Rainbowspeed.ToString();
                Settings["Settings"]["Volume Control"] = Mute.Checked.ToString();
                Settings["Settings"]["Volume"] = VolNum.Value.ToString();
                Settings["Settings"]["Discord Rich Presence"] = RPCBox.Checked.ToString();
                Settings["Settings"]["Skip Intro"] = SkipIntroBox.Checked.ToString();
                SettingsParser.WriteFile(SettingsPath, Settings);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            MainWindow.main.BTN_TabInfo_Click(sender, e);
            MainWindow.main.ClearColours();
            MainWindow.main.ClearTabItems();
            MainWindow.main.ToolInfo.Visible = false;
            MainWindow.main.ToolInfo.Visible = true;
        }

        public void RefreshVisible(bool Bool)
        {
            Refresh.Visible = Bool;
        }
    }
}
