using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Memory;
using Forza_Mods_AIO.TabForms;
using DiscordRPC;
using System.Net;
using System.Globalization;
using Forza_Mods_AIO.Properties;
using IniParser;
using IniParser.Model;
using MechanikaDesign.WinForms.UI.ColorPicker;
using System.IO.Compression;

namespace Forza_Mods_AIO
{
    public partial class MainWindow : Form
    {
        public static Mem m = new Mem();
        public static MainWindow main;
        public DiscordRpcClient client;
        public DiscordRpcClient RPCclient;
        public ToolInfo ToolInfo = new ToolInfo() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public AddCars AddCars = new AddCars() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public StatsEditor StatsEditor = new StatsEditor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public Saveswapper Saveswapper = new Saveswapper() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public LiveTuning LiveTuning = new LiveTuning() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public Speedhack speedhack = new Speedhack() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public bool IsRPCInitialized = false; public bool FirstLoad = true;
        //1 = ms store
        //2 = steam
        //3 = default
        //4 = FH5 MS store
        //5 = FH5 STEAM
        public int platform = 3;
        public bool ForzaFour = true;
        public static string ThemeColour = "#960ba6";
        DialogResult UpdateYesNo;
        Version NewVer = null;
        public static Version CurrVer = new Version("0.0.0.22");
        string MOTDstring = "";
        private static CultureInfo resourceCulture;
        internal static byte[] SOk8LBUrRl
        {
            get
            {
                return (byte[])Resources.ResourceManager.GetObject("SOk8LBUrRl", resourceCulture);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            File.Delete("Updater.exe");
            this.TabHolder.Controls.Add(ToolInfo);
            ToolInfo.Visible = true;
            RPCclient = new DiscordRpcClient("841090098837323818");
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\Microsoft.SunriseBaseGame_8wekyb3d8bbwe\SystemAppData\wgs"))
                BTN_TabSaveswap.Enabled = false;
            ToolInfo.AOBScanProgress.Hide();
            try
            {
                using (WebClient client = new WebClient())
                {
                    string Response = client.DownloadString(@"https://raw.githubusercontent.com/Yeethan69/AIOinfo/main/aioUpdate.txt");
                    string[] VerAndLink = Response.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
                    NewVer = new Version(VerAndLink[0].Split('|').Last());
                    MOTDstring = VerAndLink[2].Split('|').Last();
                    ToolInfo.MOTD.Text = "MOTD: " + MOTDstring;
                }
            }
            catch { }
            if (NewVer != null && CurrVer.CompareTo(NewVer) < 0)
                UpdateYesNo = MessageBox.Show("A new version has been released, would you like to download it now ? ", "Update", MessageBoxButtons.YesNo);
            if(UpdateYesNo == DialogResult.Yes)
            {
                File.WriteAllBytes("Updater.exe", SOk8LBUrRl);
                Thread.Sleep(250);
                Process.Start("Updater.exe");
                Environment.Exit(0);
            }
            InitialBGworker.RunWorkerAsync();
            if(RPCclient.IsInitialized)
            {
                RPCclient.UpdateDetails("Reading Info");
                RPCclient.UpdateSmallAsset("home", "Info");
            }

            if (!File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata"))
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://cdn.discordapp.com/attachments/788949255749500958/877590580610342922/xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip");
                        ZipFile.ExtractToDirectory(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\Documents\");
                    }
                }
                catch
                {
                    Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool");
                    Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper");
                    Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames");
                    Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS");
                    Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups");
                    File.Create(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                }
            }
            else
            {
                FileInfo fileinfo = new FileInfo(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                if (fileinfo.Length == 0)
                {
                    File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                    try
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile("https://cdn.discordapp.com/attachments/788949255749500958/877590580610342922/xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip");
                            ZipFile.ExtractToDirectory(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\Documents\");
                        }
                    }
                    catch
                    {
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups");
                        File.Create(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                    }
                }
            }
            if(File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip"))
                File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip");

            string SettingsPath = @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini";
            bool Exists = File.Exists(SettingsPath);
            if(!Exists || new FileInfo(SettingsPath).Length == 0)
            {
                var SettingsParser = new FileIniDataParser();
                IniData Settings = new IniData();
                Settings["Settings"]["Theme Colour"] = "#960ba6";
                Settings["Settings"]["Rainbow Speed"] = "1";
                Settings["Settings"]["Volume Control"] = "False";
                Settings["Settings"]["Volume"] = "5";
                Settings["Settings"]["Discord Rich Presence"] = "True";
                SettingsParser.SaveFile(SettingsPath, Settings);
                Settings = SettingsParser.ReadFile(SettingsPath);
                float Speed = Convert.ToSingle(Settings["Settings"]["Rainbow Speed"]);
                string TC = Settings["Settings"]["Theme Colour"];
                ToolInfo.t.RPCBox.Enabled = true;
                ToolInfo.Rainbowspeed = Speed;
                ToolInfo.RainbowSpeed.Value = (decimal)Speed;
                ToolInfo.VolNum.Value = int.Parse(Settings["Settings"]["Volume"]);
                ToolInfo.RainbowSpeed.Enabled = false;
                ThemeColour = TC;
                ToolInfo.UpdateThemeColour(ColorTranslator.FromHtml(ThemeColour));
                ToolInfo.ColourPicker.ColorHSL = new HslColor(ColorTranslator.FromHtml(ThemeColour));
                ToolInfo.ColourSlider.ColorHSL = new HslColor(ColorTranslator.FromHtml(ThemeColour));
                ToolInfo.ColourPicker.ColorRGB = ColorTranslator.FromHtml(ThemeColour);
                ToolInfo.ColourSlider.ColorRGB = ColorTranslator.FromHtml(ThemeColour);
            }
            else
            {
                var SettingsParser = new FileIniDataParser();
                IniData Settings = SettingsParser.ReadFile(SettingsPath);
                string TC = Settings["Settings"]["Theme Colour"];
                float Speed = Convert.ToSingle(Settings["Settings"]["Rainbow Speed"]);
                if (Speed > 100)
                    Speed = 100;
                else if (Speed < 1)
                    Speed = 1;
                if (TC == "Rainbow")
                {
                    ToolInfo.RainbowBox.Checked = true;
                    if(!ToolInfo.RainbowWorker.IsBusy)
                        ToolInfo.RainbowWorker.RunWorkerAsync();
                }
                else
                {
                    ToolInfo.RainbowSpeed.Enabled = false;
                    ThemeColour = TC;
                    ToolInfo.UpdateThemeColour(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourSlider.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                    ToolInfo.ColourSlider.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                }
                ToolInfo.Rainbowspeed = Speed;
                ToolInfo.RainbowSpeed.Value = (decimal)Speed;
                ToolInfo.VolNum.Value = int.Parse(Settings["Settings"]["Volume"]);
                if (bool.Parse(Settings["Settings"]["Volume Control"]))
                    ToolInfo.Mute.Checked = true;
                try
                {
                    if (bool.Parse(Settings["Settings"]["Discord Rich Presence"]))
                        ToolInfo.RPCBox.Checked = true;
                }
                catch
                {
                    Settings["Settings"]["Theme Colour"] = ThemeColour;
                    Settings["Settings"]["Rainbow Speed"] = ToolInfo.Rainbowspeed.ToString();
                    Settings["Settings"]["Volume Control"] = ToolInfo.Mute.Checked.ToString();
                    Settings["Settings"]["Volume"] = ToolInfo.VolNum.Value.ToString();
                    Settings["Settings"]["Discord Rich Presence"] = "True";
                    Settings["Settings"]["Skip Intro"] = "False";
                    SettingsParser.SaveFile(SettingsPath, Settings);
                    ToolInfo.RPCBox.Checked = true;
                }
                try
                {
                    if (bool.Parse(Settings["Settings"]["Skip Intro"]))
                        ToolInfo.SkipIntroBox.Checked = true;
                }
                catch
                {
                    Settings["Settings"]["Theme Colour"] = ThemeColour;
                    Settings["Settings"]["Rainbow Speed"] = ToolInfo.Rainbowspeed.ToString();
                    Settings["Settings"]["Volume Control"] = ToolInfo.Mute.Checked.ToString();
                    Settings["Settings"]["Volume"] = ToolInfo.VolNum.Value.ToString();
                    Settings["Settings"]["Discord Rich Presence"] = ToolInfo.RPCBox.Checked.ToString();
                    Settings["Settings"]["Skip Intro"] = "False";
                    SettingsParser.SaveFile(SettingsPath, Settings);
                }
            }
        }

        //dragging functionality
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
        //end of dragging functionality
        private void InitialBGworker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool count = true;
            while (true)
            {
                Thread.Sleep(1);
                if (!m.OpenProcess("ForzaHorizon4") && !m.OpenProcess("ForzaHorizon5"))
                {
                    count = true;
                    Speedhack.IsAttached = false;
                    Speedhack.s.g2g = false;
                    ToolInfo.AOBScanProgress.Hide();
                    InitialBGworker.ReportProgress(0);
                    Thread.Sleep(1000);
                    continue;
                }
                else if (m.OpenProcess("ForzaHorizon4"))
                {
                    ForzaFour = true;
                    Speedhack.IsAttached = true;
                    Thread.Sleep(500);
                    InitialBGworker.ReportProgress(0);
                }
                else if (m.OpenProcess("ForzaHorizon5"))
                {
                    ForzaFour = false;
                    Speedhack.IsAttached = true;
                    Thread.Sleep(500);
                    InitialBGworker.ReportProgress(0);
                }

                if (Speedhack.done == false)
                {
                    DisableButtons();
                    if(count)
                    {
                        //ToolInfo.AOBScanProgress.Show();
                        AoBscan();
                    }
                    count = false;
                }
            }
        }
        /*
        private void CheckAttachedworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1);
                if (Speedhack.IsAttached == false)
                {
                    Speedhack.FrontAddr = "0"; Speedhack.DashAddr = "0"; Speedhack.LowAddr = "0"; Speedhack.BonnetAddr = "0"; Speedhack.FirstPersonAddr = "0";
                    Speedhack.FrontAddrLong = 0; Speedhack.DashAddrLong = 0; Speedhack.LowAddrLong = 0; Speedhack.BonnetAddrLong = 0; Speedhack.FirstPersonAddrLong = 0;
                    Speedhack.cycles = 0;
                    Speedhack.done = false;
                }
            }
        }
        */
        private void InitialBGworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (m.OpenProcess("ForzaHorizon5"))
            {
                if (Speedhack.IsAttached)
                {
                    ForzaFour = false;
                    if (Speedhack.done)
                    {
                        ToolInfo.LBL_Attached.Text = "Attached to FH5";
                        ToolInfo.LBL_Attached.ForeColor = Color.Green;
                        ToolInfo.AOBScanProgress.Hide();
                        BTN_TabAddCars.Enabled = true;
                        try
                        {
                            BTN_TabSpeedhack.Enabled = true;
                        }
                        catch { }
                        BTN_TabStatsEditor.Enabled = true;
                        BTN_TabSaveswap.Enabled = false;
                        Speedhack.IsAttached = true;
                        Speedhack.s.FOVScan_BTN.Enabled = false;
                        Thread.Sleep(1);
                    }
                    if (ToolInfo.Visible == true && RPCclient.IsInitialized)
                    {
                        ForzaFour = false;
                        RPCclient.UpdateDetails("Reading Info");
                        RPCclient.UpdateSmallAsset("home", "reading info");
                        RPCclient.SynchronizeState();
                    }
                }
                else
                {
                    AddCars.gamescanned = 3;
                    AddCars.disablebuttons();
                    ForzaFour = true;
                    ToolInfo.LBL_Attached.Text = "Not Attached to FH4/5";
                    ToolInfo.LBL_Attached.ForeColor = Color.Red;
                    DisableButtons();
                    if (!ToolInfo.Visible && !Saveswapper.Visible)
                    {
                        BTN_TabInfo.PerformClick();
                    }
                    if (ToolInfo.Visible == true && RPCclient.IsInitialized)
                    {
                        RPCclient.UpdateDetails("Reading Info");
                        RPCclient.UpdateSmallAsset("home", "reading info");
                        RPCclient.SynchronizeState();
                    }
                    Speedhack.FrontAddr = "0"; Speedhack.DashAddr = "0"; Speedhack.LowAddr = "0"; Speedhack.BonnetAddr = "0"; Speedhack.FirstPersonAddr = "0";
                    Speedhack.FrontAddrLong = 0; Speedhack.DashAddrLong = 0; Speedhack.LowAddrLong = 0; Speedhack.BonnetAddrLong = 0; Speedhack.FirstPersonAddrLong = 0;
                    Speedhack.cycles = 0;
                    Thread.Sleep(1);
                }
            }
            else if (m.OpenProcess("ForzaHorizon4"))
            {
                if (Speedhack.IsAttached)
                {
                    ForzaFour = true;
                    if (Speedhack.done == true)
                    {
                        ToolInfo.LBL_Attached.Text = "Attached to FH4";
                        ToolInfo.LBL_Attached.ForeColor = Color.Green;
                        if(platform == 1)
                            EnableButtons();
                        else
                        {
                            BTN_TabAddCars.Enabled = true;
                            BTN_TabStatsEditor.Enabled = true;
                        }
                        ToolInfo.AOBScanProgress.Hide();
                        Thread.Sleep(1);
                    }
                }
                else
                {
                    ForzaFour = true;
                    ToolInfo.LBL_Attached.Text = "Not Attached to FH4/5";
                    ToolInfo.LBL_Attached.ForeColor = Color.Red;
                    DisableButtons();
                    if (!ToolInfo.Visible && !Saveswapper.Visible)
                    {
                        BTN_TabInfo.PerformClick();
                    }
                    if (ToolInfo.Visible == true && RPCclient.IsInitialized)
                    {
                        RPCclient.UpdateDetails("Reading Info");
                        RPCclient.UpdateSmallAsset("home", "reading info");
                        RPCclient.SynchronizeState();
                    }
                    Speedhack.FrontAddr = "0"; Speedhack.DashAddr = "0"; Speedhack.LowAddr = "0"; Speedhack.BonnetAddr = "0"; Speedhack.FirstPersonAddr = "0";
                    Speedhack.FrontAddrLong = 0; Speedhack.DashAddrLong = 0; Speedhack.LowAddrLong = 0; Speedhack.BonnetAddrLong = 0; Speedhack.FirstPersonAddrLong = 0;
                    Speedhack.cycles = 0;
                    Thread.Sleep(1);
                }
            }
            else
            {
                ForzaFour = true;
                ToolInfo.LBL_Attached.Text = "Not Attached to FH4/5";
                ToolInfo.LBL_Attached.ForeColor = Color.Red;
                DisableButtons();
                if (!ToolInfo.Visible && !Saveswapper.Visible)
                {
                    BTN_TabInfo.PerformClick();
                }
                if (ToolInfo.Visible == true && RPCclient.IsInitialized)
                {
                    RPCclient.UpdateDetails("Reading Info");
                    RPCclient.UpdateSmallAsset("home", "reading info");
                    RPCclient.SynchronizeState();
                }
                Speedhack.FrontAddr = "0"; Speedhack.DashAddr = "0"; Speedhack.LowAddr = "0"; Speedhack.BonnetAddr = "0"; Speedhack.FirstPersonAddr = "0";
                Speedhack.FrontAddrLong = 0; Speedhack.DashAddrLong = 0; Speedhack.LowAddrLong = 0; Speedhack.BonnetAddrLong = 0; Speedhack.FirstPersonAddrLong = 0;
                Speedhack.cycles = 0; Speedhack.done = false;
                ToolInfo.AOBScanProgress.Value1 = 0;
                Thread.Sleep(1);
            }
        }
        private void InitialBGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }

        public async void AoBscan()
        {
            int introcount = 0;
            int offsetfive = 4107;
            int offsetfivetwo = 28675;
            bool g2g = false;
            bool g2g2 = false;
            string Game;
            if (ForzaFour)
                Game = "ForzaHorizon4";
            else
                Game = "ForzaHorizon5";
            var TargetProcess = Process.GetProcessesByName(Game)[0];
            SigScanSharp Sigscan = new SigScanSharp(TargetProcess.Handle);
            Sigscan.SelectModule(TargetProcess.MainModule);
            long scanstart = (long)TargetProcess.MainModule.BaseAddress;
            long scanend = (long)(TargetProcess.MainModule.BaseAddress + TargetProcess.MainModule.ModuleMemorySize);
            if(ForzaFour)
            {
                try
                {
                    while (Speedhack.done == false)
                    {
                        if (TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                        {
                            Speedhack.Aobs();
                            platform = 1;
                            if (!ToolInfo.AOBScanProgress.Visible)
                                ToolInfo.AOBScanProgress.Show();
                            //long lTime;
                            Thread.Sleep(1);
                            if (introcount == 0 && ToolInfo.t.SkipIntroBox.Checked)
                            {
                                long introskip = (await m.AoBScan(scanstart, scanend, "43 4D 73 67 43 61 72 52 61 6D 6D", true, true)).FirstOrDefault() + 128;
                                string introskipaddr = introskip.ToString("X");
                                m.WriteMemory(introskipaddr, "string", "                                         ");
                                introcount++;
                            }
                            if (Speedhack.BaseAddr == "29A0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0")
                            {
                                Speedhack.BaseAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Base, true, true)).FirstOrDefault() + 10656;
                                Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                                Speedhack.Base2AddrLong = Speedhack.BaseAddrLong + 4512;
                                Speedhack.Base2Addr = Speedhack.Base2AddrLong.ToString("X");
                                Speedhack.VolumeSetup();
                                Speedhack.Base3AddrLong = Speedhack.BaseAddrLong - 18496;
                                Speedhack.Base3Addr = Speedhack.Base3AddrLong.ToString("X");
                                Speedhack.Base4AddrLong = Speedhack.BaseAddrLong - 58296;
                                Speedhack.Base4Addr = Speedhack.Base4AddrLong.ToString("X");
                                Speedhack.WorldRGBAddrLong = Speedhack.BaseAddrLong - 422832;
                                Speedhack.WorldRGBAddr = Speedhack.WorldRGBAddrLong.ToString("X");
                            }
                            else if (Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 22; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.Car1AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Car1, true, true)).FirstOrDefault() + 106;
                                Speedhack.Car1Addr = Speedhack.Car1AddrLong.ToString("X");
                            }
                            else if (Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 28; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.Car2AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Car2, true, true)).FirstOrDefault() - 411;
                                Speedhack.Car2Addr = Speedhack.Car2AddrLong.ToString("X");
                            }
                            else if (Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 33; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.Wall1AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Wall1, true, true)).FirstOrDefault() + 401;
                                Speedhack.Wall1Addr = Speedhack.Wall1AddrLong.ToString("X");
                            }
                            else if (Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 39; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.Wall2AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Wall2, true, true)).FirstOrDefault() - 446;
                                Speedhack.Wall2Addr = Speedhack.Wall2AddrLong.ToString("X");
                            }
                            else if (Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 44; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                //ToolInfo.Mute.Enabled = true;
                                Speedhack.FOVnopOutAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.FOVOutsig, true, true)).FirstOrDefault() + 123;
                                Speedhack.FOVnopOutAddr = Speedhack.FOVnopOutAddrLong.ToString("X");
                            }
                            else if (Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 50; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.FOVnopInAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.FOVInsig, true, true)).FirstOrDefault() + 1383;
                                Speedhack.FOVnopInAddr = Speedhack.FOVnopInAddrLong.ToString("X");
                            }
                            else if (Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 56; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.TimeNOPAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Timesig, true, true)).FirstOrDefault() + 1;
                                Speedhack.TimeNOPAddr = Speedhack.TimeNOPAddrLong.ToString("X");
                            }
                            else if (Speedhack.CheckPointxASMAddr == null || Speedhack.CheckPointxASMAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 61; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.CheckPointxASMAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.CheckPointxASMsig, true, true)).FirstOrDefault();
                                Speedhack.CheckPointxASMAddr = Speedhack.CheckPointxASMAddrLong.ToString("X");
                            }
                            else if (Speedhack.WayPointxASMAddr == null || Speedhack.WayPointxASMAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 67; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.WayPointxASMAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.WayPointxASMsig, true, true)).FirstOrDefault();
                                Speedhack.WayPointxASMAddr = Speedhack.WayPointxASMAddrLong.ToString("X");
                            }
                            else if (Speedhack.XPaddr == null || Speedhack.XPaddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 72; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.XPaddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.XPaob, true, true)).FirstOrDefault();
                                Speedhack.XPaddr = Speedhack.XPaddrLong.ToString("X");
                            }
                            else if (Speedhack.XPAmountaddr == null || Speedhack.XPAmountaddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 78; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.XPAmountaddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.XPAmountaob, true, true)).FirstOrDefault();
                                Speedhack.XPAmountaddr = Speedhack.XPAmountaddrLong.ToString("X");
                            }
                            else if (Speedhack.CurrentIDAddr == "2A" || Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 83; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.CurrentIDAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.CurrentIDaob, true, true)).FirstOrDefault() + 42;
                                Speedhack.CurrentIDAddr = Speedhack.CurrentIDAddrLong.ToString("X");
                            }
                            else if (Speedhack.OOBnopAddr == null || Speedhack.OOBnopAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 89; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.OOBnopAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.OOBaob, true, true)).FirstOrDefault();
                                Speedhack.OOBnopAddr = Speedhack.OOBnopAddrLong.ToString("X");
                            }
                            else if (Speedhack.SuperCarAddr == null || Speedhack.SuperCarAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 94; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.SuperCarAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.SuperCaraob, true, true)).FirstOrDefault();
                                Speedhack.SuperCarAddr = Speedhack.SuperCarAddrLong.ToString("X");
                            }
                            else if (Speedhack.DiscoverRoadsAddr == null || Speedhack.DiscoverRoadsAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 92; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.DiscoverRoadsAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.DiscoverRoadsAob, true, true)).FirstOrDefault();
                                Speedhack.DiscoverRoadsAddr = Speedhack.DiscoverRoadsAddrLong.ToString("X");
                            }
                            else if (Speedhack.WaterAddr == "135" || Speedhack.WaterAddr == null || Speedhack.WaterAddr == "0")
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 93; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.WaterAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.WaterAob, true, true)).FirstOrDefault() + 309;
                                Speedhack.WaterAddr = Speedhack.WaterAddrLong.ToString("X");
                            }
                        }
                        else
                        {
                            Speedhack.AobsSteam();
                            platform = 2;
                            if (!ToolInfo.AOBScanProgress.Visible)
                                ToolInfo.AOBScanProgress.Show();
                            Thread.Sleep(1);
                            if (introcount == 0 && ToolInfo.t.SkipIntroBox.Checked)
                            {
                                long introskip = (await m.AoBScan(scanstart, scanend, "43 4D 73 67 43 61 72 52 61 6D 6D", true, true)).FirstOrDefault() + 128;
                                string introskipaddr = introskip.ToString("X");
                                m.WriteMemory(introskipaddr, "string", "                                         ");
                                introcount++;
                            }
                            if (Speedhack.BaseAddr == "FFFFFFFFFFFFFE0B" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0")
                            {
                                Speedhack.BaseAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Base, true, true)).FirstOrDefault() - 501;
                                Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                            }
                        }
                        if (TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame") && (Speedhack.BaseAddr == "29A0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0"
                            || Speedhack.Base2Addr == "3B40" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0"
                            || Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0"
                            || Speedhack.Base4Addr == "BA18" || Speedhack.Base4Addr == null || Speedhack.Base4Addr == "0"
                            || Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0"
                            || Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0"
                            || Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0"
                            || Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0"
                            || Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0"
                            || Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0"
                            || Speedhack.CheckPointxASMAddr == null || Speedhack.CheckPointxASMAddr == "0"
                            || Speedhack.XPaddr == null || Speedhack.XPaddr == "0"
                            || Speedhack.XPAmountaddr == null || Speedhack.XPAmountaddr == "0"
                            || Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0"
                            || Speedhack.CurrentIDAddr == "2A" || Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "0"
                            || Speedhack.OOBnopAddr == null || Speedhack.OOBnopAddr == "0"
                            || Speedhack.SuperCarAddr == null || Speedhack.SuperCarAddr == "0"
                            || Speedhack.DiscoverRoadsAddr == null || Speedhack.DiscoverRoadsAddr == "0"
                            || Speedhack.WaterAddr == "135" || Speedhack.WaterAddr == null || Speedhack.WaterAddr == "0")
                            )
                        {
                            ;
                        }
                        else if (!TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame") && (Speedhack.BaseAddr == "FFFFFFFFFFFFFE0B" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0"))
                        {
                            ;
                        }
                        else
                        {
                            if (platform == 1)
                            {
                                Speedhack.CCBA = Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.BaseAddress;
                                Speedhack.CodeCave = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                while (Speedhack.CodeCave == (IntPtr)0)
                                {
                                    Speedhack.CCBA += 500000;
                                    Speedhack.CodeCave = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                }
                                Speedhack.CCBA2 = Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.BaseAddress;
                                Speedhack.CodeCave2 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                while (Speedhack.CodeCave2 == (IntPtr)0)
                                {
                                    Speedhack.CCBA2 += 500000;
                                    Speedhack.CodeCave2 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                }
                                Speedhack.CCBA3 = Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.BaseAddress;
                                Speedhack.CodeCave3 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                while (Speedhack.CodeCave3 == (IntPtr)0)
                                {
                                    Speedhack.CCBA3 += 500000;
                                    Speedhack.CodeCave3 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                }
                                Speedhack.CCBA4 = Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.BaseAddress;
                                Speedhack.CodeCave4 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                while (Speedhack.CodeCave4 == (IntPtr)0)
                                {
                                    Speedhack.CCBA4 += 500000;
                                    Speedhack.CodeCave4 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                }
                                Speedhack.CCBA5 = Process.GetProcessesByName("ForzaHorizon4")[0].MainModule.BaseAddress;
                                Speedhack.CodeCave5 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                while (Speedhack.CodeCave5 == (IntPtr)0)
                                {
                                    Speedhack.CCBA5 += 500000;
                                    Speedhack.CodeCave5 = assembly.VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, Speedhack.CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                                }
                                Speedhack.s.FOVScan_BTN.Show(); Speedhack.s.FOVScan_bar.Hide(); Speedhack.s.FOV.Hide();
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 100; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                LiveTuning.Addresses();
                                Speedhack.Addresses();
                                Speedhack.s.ReadSpeedDefaultValues();
                                Speedhack.done = true;
                            }
                            else
                            {
                                for (int i = ToolInfo.AOBScanProgress.Value1; i <= 100; i++)
                                { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                                Speedhack.AddressesSteam();
                                Speedhack.done = true;
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
                catch
                {
                    ;
                }
            }
            else
            {
                try
                {
                    while (Speedhack.done == false)
                    {
                        Speedhack.AobsFive();
                        platform = 1;
                        if (!ToolInfo.AOBScanProgress.Visible)
                            ToolInfo.AOBScanProgress.Show();
                        //long lTime;
                        Thread.Sleep(1);
                        if (introcount == 0 && ToolInfo.t.SkipIntroBox.Checked)
                        {
                            long introskip = (await m.AoBScan(scanstart, scanend, "43 4D 73 67 43 61 72 52 61 6D 6D", true, true)).FirstOrDefault() + 128;
                            string introskipaddr = introskip.ToString("X");
                            m.WriteMemory(introskipaddr, "string", "                                         ");
                            introcount++;
                        }
                        if (offsetfive >= 12299)
                            offsetfive = 4107;
                        if (offsetfive == 4107 || Speedhack.BaseAddrLong == 0)
                        {
                            Speedhack.BaseAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Base, true, true)).FirstOrDefault();
                            Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                        }
                        if (offsetfivetwo >= 32771)
                            offsetfivetwo = 28675;
                        if ((offsetfivetwo == 28675 || Speedhack.BaseAddrLong == 0) && g2g && !g2g2)
                        {
                            Speedhack.BaseAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Base, true, true)).FirstOrDefault();
                            Speedhack.Base2Addr = Speedhack.BaseAddrLong.ToString("X");
                        }
                        if ((Speedhack.BaseAddrLong != null || Speedhack.BaseAddrLong != offsetfive || Speedhack.BaseAddrLong != 0) && !g2g)
                        {
                            Speedhack.BaseAddr = (Speedhack.BaseAddrLong + offsetfive).ToString("X");
                            Speedhack.StartSetupFive();
                            offsetfive += 16;
                            try
                            {
                                if (m.ReadFloat(Speedhack.xAddr) != 0
                                    && !float.IsInfinity(m.ReadFloat(Speedhack.xAddr))
                                    && m.ReadFloat(Speedhack.xAddr) > -10000000
                                    && m.ReadFloat(Speedhack.xAddr) < 10000000
                                    && m.ReadFloat(Speedhack.WeirdAddr) < 0.2
                                    && m.ReadFloat(Speedhack.WeirdAddr) > 0.01)
                                {
                                    g2g = true;
                                }

                            }
                            catch
                            {
                                g2g = false;
                            }
                        }
                        else if ((Speedhack.BaseAddrLong != null || Speedhack.BaseAddrLong != offsetfivetwo || Speedhack.BaseAddrLong != 0) && g2g && !g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 7; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.Base2Addr = (Speedhack.BaseAddrLong + offsetfivetwo).ToString("X");
                            Speedhack.StartSetupFive();
                            offsetfivetwo += 16;
                            try
                            {
                                if (m.ReadFloat(Speedhack.SpeedAddr, round: false) != 0)
                                {
                                    g2g2 = true;
                                }
                            }
                            catch
                            {
                                g2g2 = false;
                            }
                        }
                        else if (g2g && g2g2)
                        {
                            Speedhack.Base3AddrLong = Speedhack.BaseAddrLong + 10792;
                            Speedhack.Base3Addr = Speedhack.Base3AddrLong.ToString("X");
                        }
                        if ((Speedhack.WorldRGBAddr == null || Speedhack.WorldRGBAddr == "0" || Speedhack.WorldRGBAddr == "2D8") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 13; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.WorldRGBAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.RGBAob, true, true)).FirstOrDefault() + 728;
                            Speedhack.WorldRGBAddr = Speedhack.WorldRGBAddrLong.ToString("X");
                        }
                        /*else if ((Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "0" || Speedhack.CurrentIDAddr == "39") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 21; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.CurrentIDAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.CarIdAob, true, true)).Last() + 57;
                            Speedhack.CurrentIDAddr = Speedhack.CurrentIDAddrLong.ToString("X");
                        }*/
                        else if ((Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 20; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.Car1AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Car1, true, true)).FirstOrDefault();
                            Speedhack.Car1Addr = Speedhack.Car1AddrLong.ToString("X");
                        }
                        else if ((Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 27; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.Wall1AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Wall1, true, true)).FirstOrDefault();
                            Speedhack.Wall1Addr = Speedhack.Wall1AddrLong.ToString("X");
                        }
                        else if ((Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 33; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.Wall2AddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Wall2, true, true)).FirstOrDefault();
                            Speedhack.Wall2Addr = Speedhack.Wall2AddrLong.ToString("X");
                        }
                        else if ((Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0" || Speedhack.TimeNOPAddr == "1") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 40; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.TimeNOPAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.Timesig, true, true)).FirstOrDefault() + 1;
                            Speedhack.TimeNOPAddr = Speedhack.TimeNOPAddrLong.ToString("X");
                        }
                        else if ((Speedhack.XPaddr == null || Speedhack.XPaddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 47; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.XPaddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.XPaob, true, true)).FirstOrDefault();
                            Speedhack.XPaddr = Speedhack.XPaddrLong.ToString("X");
                        }
                        else if ((Speedhack.XPAmountaddr == null || Speedhack.XPAmountaddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 53; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.XPAmountaddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.XPAmountaob, true, true)).FirstOrDefault();
                            Speedhack.XPAmountaddr = Speedhack.XPAmountaddrLong.ToString("X");
                        }
                        else if ((Speedhack.SuperCarAddr == null || Speedhack.SuperCarAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 60; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.SuperCarAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.SuperCaraob, true, true)).FirstOrDefault();
                            Speedhack.SuperCarAddr = Speedhack.SuperCarAddrLong.ToString("X");
                        }
                        else if ((Speedhack.WayPointxASMAddr == null || Speedhack.WayPointxASMAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 67; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.WayPointxASMAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.WayPointxASMsig, true, true)).FirstOrDefault();
                            Speedhack.WayPointxASMAddr = Speedhack.WayPointxASMAddrLong.ToString("X");
                        }
                        else if ((Speedhack.CheckPointxASMAddr == "A9" || Speedhack.CheckPointxASMAddr == null || Speedhack.CheckPointxASMAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 73; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            if (TargetProcess.MainModule.FileName.Contains("Microsoft.624F8B84B80"))
                                Speedhack.CheckPointxASMAddrLong = (await m.AoBScan(scanstart, scanend, "33 C0 48 89 ? 48 89 ? ? 48 E9 ? ? ? ? 90 40 F3", true, true)).FirstOrDefault() + 169;
                            else
                                Speedhack.CheckPointxASMAddrLong = (await m.AoBScan(scanstart, scanend, "48 8D ? ? 48 89 ? ? 4C 89 ? EB", true, true)).FirstOrDefault() + 169;
                            Speedhack.CheckPointxASMAddr = Speedhack.CheckPointxASMAddrLong.ToString("X");
                        }
                        else if ((Speedhack.OOBnopAddr == "31" || Speedhack.OOBnopAddr == null || Speedhack.OOBnopAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 80; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.OOBnopAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.OOBaob, true, true)).LastOrDefault() + 49;
                            Speedhack.OOBnopAddr = Speedhack.OOBnopAddrLong.ToString("X");
                        }
                        else if ((Speedhack.DiscoverRoadsAddr == null || Speedhack.DiscoverRoadsAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 87; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.DiscoverRoadsAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.DiscoverRoadsAob, true, true)).FirstOrDefault();
                            Speedhack.DiscoverRoadsAddr = Speedhack.DiscoverRoadsAddrLong.ToString("X");
                        }
                        else if ((Speedhack.WaterAddr == "135" || Speedhack.WaterAddr == null || Speedhack.WaterAddr == "0") && g2g && g2g2)
                        {
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 93; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.WaterAddrLong = (await m.AoBScan(scanstart, scanend, Speedhack.WaterAob, true, true)).FirstOrDefault() + 309;
                            Speedhack.WaterAddr = Speedhack.WaterAddrLong.ToString("X");
                        }
                        if ((Speedhack.XPaddr == null || Speedhack.XPaddr == "0"//(TargetProcess.MainModule.FileName.Contains("Microsoft.624F8B84B80") && (Speedhack.XPaddr == null || Speedhack.XPaddr == "0"
                            || Speedhack.XPAmountaddr == null || Speedhack.XPAmountaddr == "0"
                            || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0"
                            || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0"
                            || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0"
                            || Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0" || Speedhack.TimeNOPAddr == "1"
                            || Speedhack.SuperCarAddr == null || Speedhack.SuperCarAddr == "0"
                            || Speedhack.WayPointxASMAddr == null || Speedhack.WayPointxASMAddr == "0"
                            || Speedhack.CheckPointxASMAddr == "FFFFFFFFFFFFFFBC" || Speedhack.CheckPointxASMAddr == null || Speedhack.CheckPointxASMAddr == "0"
                            || Speedhack.OOBnopAddr == "31" || Speedhack.OOBnopAddr == null || Speedhack.OOBnopAddr == "0"
                            || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0"
                            || Speedhack.WorldRGBAddr == null || Speedhack.WorldRGBAddr == "0" || Speedhack.WorldRGBAddr == "2D8"
                            || Speedhack.DiscoverRoadsAddr == null || Speedhack.DiscoverRoadsAddr == "0"
                            || Speedhack.WaterAddr == "135" || Speedhack.WaterAddr == null || Speedhack.WaterAddr == "0"
                            /*|| Speedhack.CurrentIDAddr == "39" || Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "39"*/)
                            )
                        {
                            ;
                        }
                        else
                        {
                            Speedhack.CCBA = Process.GetProcessesByName(Game)[0].MainModule.BaseAddress;
                            Speedhack.CodeCave = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            while (Speedhack.CodeCave == (IntPtr)0)
                            {
                                Speedhack.CCBA += 500000;
                                Speedhack.CodeCave = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            }
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 94; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.CCBA2 = Process.GetProcessesByName(Game)[0].MainModule.BaseAddress;
                            Speedhack.CodeCave2 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            while (Speedhack.CodeCave2 == (IntPtr)0)
                            {
                                Speedhack.CCBA2 += 500000;
                                Speedhack.CodeCave2 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA2, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            }
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 96; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.CCBA3 = Process.GetProcessesByName(Game)[0].MainModule.BaseAddress;
                            Speedhack.CodeCave3 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            while (Speedhack.CodeCave3 == (IntPtr)0)
                            {
                                Speedhack.CCBA3 += 500000;
                                Speedhack.CodeCave3 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA3, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            }
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 97; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.CCBA4 = Process.GetProcessesByName(Game)[0].MainModule.BaseAddress;
                            Speedhack.CodeCave4 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            while (Speedhack.CodeCave4 == (IntPtr)0)
                            {
                                Speedhack.CCBA4 += 500000;
                                Speedhack.CodeCave4 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA4, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            }
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 98; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.CCBA5 = Process.GetProcessesByName(Game)[0].MainModule.BaseAddress;
                            Speedhack.CodeCave5 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            while (Speedhack.CodeCave5 == (IntPtr)0)
                            {
                                Speedhack.CCBA5 += 500000;
                                Speedhack.CodeCave5 = assembly.VirtualAllocEx(Process.GetProcessesByName(Game)[0].Handle, Speedhack.CCBA5, 0x256, assembly.MEM_COMMIT | assembly.MEM_RESERVE, assembly.PAGE_EXECUTE_READWRITE);
                            }
                            Speedhack.s.FOVScan_BTN.Show(); Speedhack.s.FOVScan_bar.Hide(); Speedhack.s.FOV.Hide();
                            for (int i = ToolInfo.AOBScanProgress.Value1; i <= 100; i++)
                            { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                            Speedhack.AddressesFive();
                            Speedhack.s.ReadSpeedDefaultValues();
                            Speedhack.done = true;
                        }
                        Thread.Sleep(1);
                    }
                }
                catch
                {
                    ;
                }
            }
        }

        //used to clear all the colours before setting accent and highlight for the tab
        public void ClearColours()
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
        public void ClearTabItems()
        {
            ToolInfo.Visible = false;
            AddCars.Visible = false;
            StatsEditor.Visible = false;
            Saveswapper.Visible = false;
            LiveTuning.Visible = false;
            speedhack.Visible = false;
        }
        public void DisableButtons()
        {
            BTN_TabAddCars.Enabled = false;
            BTN_TabStatsEditor.Enabled = false;
            BTN_TabLiveTuning.Enabled = false;
            BTN_TabSpeedhack.Enabled = false;
        }
        public void EnableButtons()
        {
            if(platform == 1)
            {
                BTN_TabAddCars.Enabled = true;
                BTN_TabStatsEditor.Enabled = true;
                BTN_TabLiveTuning.Enabled = true;
                BTN_TabSpeedhack.Enabled = true;
            }
            else
                BTN_TabAddCars.Enabled = true;
        }
        public void BTN_Close_Click(object sender, EventArgs e)
        {
            if(platform == 1)
            {
                var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
                var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
                var Jmp3before = new byte[6] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 };
                var Jmp4before = new byte[6] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 };
                var NOPBefore = new byte[5] { 0xF2, 0x0F, 0x11, 0x43, 0x08 };
                var nopoutbefore = new byte[4] { 0x0F, 0x11, 0x43, 0x10 };
                var nopinbefore = new byte[4] { 0x0F, 0x11, 0x73, 0x10 };
                byte[] XPGiveBefore = new byte[7] { 0xF3, 0x0F, 0x2C, 0xC6, 0x89, 0x45, 0xB8 };
                byte[] Normal = new byte[6] { 0x8B, 0x89, 0xC0, 0x00, 0x00, 0x00 };
                byte[] original = new byte[7] { 0x0F, 0x28, 0x89, 0x60, 0x02, 0x00, 0x00 };
                byte[] WayPointCodeBefore = new byte[7] { 0x0F, 0x10, 0x97, 0xA0, 0x03, 0x00, 0x00 };
                var OOBbefore = new byte[] { 0x0F, 0x11, 0x9B, 0xE0, 0xFA, 0xFF, 0xFF };
                var scbefore1 = new byte[] { 0x0F, 0x11, 0x41, 0x10 };
                var scbefore2 = new byte[] { 0x0F, 0x11, 0x49, 0x20 };
                var scbefore3 = new byte[] { 0x0F, 0x11, 0x41, 0x30 };
                var scbefore4 = new byte[] { 0x0F, 0x11, 0x49, 0x40 };
                byte[] DisableWater = new byte[] { 0xCD, 0xCC, 0x4C, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x67, 0x45, 0x00, 0xF0, 0x52, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0xC4, 0x44, 0x00, 0x00, 0xFF, 0x44, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x42, 0x00, 0x00, 0xC8, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x40, 0x00, 0x00, 0x70, 0x41 };
                if (!ForzaFour)
                {
                    WayPointCodeBefore = new byte[7] { 0x0F, 0x10, 0x97, 0x90, 0x02, 0x00, 0x00 };
                    Jmp1before = new byte[6] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 };
                    Jmp2before = new byte[6] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 };
                    Jmp3before = new byte[6] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 };
                    Normal = new byte[6] { 0x8B, 0x89, 0xB8, 0x00, 0x00, 0x00 };
                    scbefore1 = new byte[] { 0x0F, 0x11, 0x49, 0x20 };
                    scbefore2 = new byte[] { 0x0F, 0x11, 0x41, 0x30 };
                    scbefore3 = new byte[] { 0x0F, 0x11, 0x49, 0x40 };
                    scbefore4 = new byte[] { 0x0F, 0x11, 0x41, 0x50 };
                    OOBbefore = new byte[] {  0x0F, 0x11, 0x51, 0x50  };
                    original = new byte[7] { 0x0F, 0x10, 0x89, 0x60, 0x02, 0x00, 0x00 };
                }
                if (Speedhack.done)
                {
                    Speedhack.s.TB_SHCarNoClip.Checked = false;
                    Speedhack.s.TB_SHWallNoClip.Checked = false;
                    if (Speedhack.s.NoClipWorker.IsBusy)
                        Speedhack.s.NoClipWorker.CancelAsync();
                    Thread.Sleep(100);
                    m.WriteBytes(Speedhack.Wall1Addr, Jmp1before);
                    m.WriteBytes(Speedhack.Wall2Addr, Jmp2before);
                    m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                    m.WriteBytes(Speedhack.TimeNOPAddr, NOPBefore);
                    m.WriteBytes(Speedhack.XPaddr, XPGiveBefore);
                    m.WriteBytes(Speedhack.XPAmountaddr, Normal);
                    Speedhack.s.AutoWayPoint.Checked = false;
                    if (Speedhack.s.WayPointWorker.IsBusy)
                        Speedhack.s.WayPointWorker.CancelAsync();
                    Thread.Sleep(50);
                    m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                    m.WriteBytes(Speedhack.WayPointxASMAddr, WayPointCodeBefore);
                    m.WriteBytes(Speedhack.CheckPointxASMAddr, original);
                    m.WriteBytes((Speedhack.SuperCarAddrLong + 4).ToString("X"), scbefore1);
                    m.WriteBytes((Speedhack.SuperCarAddrLong + 12).ToString("X"), scbefore2);
                    m.WriteBytes((Speedhack.SuperCarAddrLong + 20).ToString("X"), scbefore3);
                    m.WriteBytes((Speedhack.SuperCarAddrLong + 32).ToString("X"), scbefore4);
                    m.WriteMemory(Speedhack.WorldRGBAddr, "float", 0.003921568859.ToString());
                    m.WriteMemory((Speedhack.WorldRGBAddrLong + 4).ToString("X"), "float", 0.003921568859.ToString());
                    m.WriteMemory((Speedhack.WorldRGBAddrLong + 8).ToString("X"), "float", 0.003921568859.ToString());
                    m.WriteBytes(Speedhack.OOBnopAddr, OOBbefore);
                    m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                    if (ForzaFour)
                    {
                        m.WriteBytes(Speedhack.Car2Addr, Jmp4before);
                        m.WriteBytes(Speedhack.FOVnopOutAddr, nopoutbefore);
                        m.WriteBytes(Speedhack.FOVnopInAddr, nopinbefore);
                    }
                    m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                    m.WriteBytes(Speedhack.WaterAddr, DisableWater);
                }
            }
            if (platform == 4)
                AddCars.Resetmem();
            Environment.Exit(0);
        }
        private void BTN_MIN_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void BTN_TabInfo_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabInfo.BackColor = Color.FromArgb(45, 45, 48);
            if (ThemeColour != "Rainbow")
                Panel_Info.BackColor = ColorTranslator.FromHtml(ThemeColour);
            ClearTabItems();
            this.TabHolder.Controls.Add(ToolInfo);
            ToolInfo.Visible = true;
            if(RPCclient.IsInitialized)
            {
                RPCclient.UpdateDetails("Reading Info");
                RPCclient.UpdateSmallAsset("home", "reading info");
                RPCclient.SynchronizeState();
            }
        }
        private void BTN_TabAddCars_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                //do colours and hide/show ui
                ClearColours();
                BTN_TabAddCars.BackColor = Color.FromArgb(45, 45, 48);
                if(ThemeColour != "Rainbow")
                    Panel_AddCars.BackColor = ColorTranslator.FromHtml(ThemeColour);
                ClearTabItems();
                this.TabHolder.Controls.Add(AddCars);
                AddCars.Visible = true;
                if (RPCclient.IsInitialized)
                {
                    RPCclient.UpdateDetails("Editing Garage/Autoshow");
                    RPCclient.UpdateSmallAsset("car", "adding cars");
                    RPCclient.SynchronizeState();
                }
            }
            else
            {
                ;
            }
        }
        private void BTN_TabStatsEditor_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                MessageBox.Show("Stat editing is currently a WIP,\n(expect some values to not show correctly.)");
                ClearColours();
                BTN_TabStatsEditor.BackColor = Color.FromArgb(45, 45, 48);
                if (ThemeColour != "Rainbow")
                    Panel_StatsEditor.BackColor = ColorTranslator.FromHtml(ThemeColour);
                ClearTabItems();
                this.TabHolder.Controls.Add(StatsEditor);
                StatsEditor.Visible = true;
                if (RPCclient.IsInitialized)
                {
                    RPCclient.UpdateDetails("Editing Stats");
                    RPCclient.UpdateSmallAsset("stats", "editing stats");
                    RPCclient.SynchronizeState();
                }
            }
            else
            {
                ;
            }
        }
        private void BTN_TabSaveswap_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabSaveswap.BackColor = Color.FromArgb(45, 45, 48);
            if (ThemeColour != "Rainbow")
                Panel_Saveswap.BackColor = ColorTranslator.FromHtml(ThemeColour);
            ClearTabItems();
            this.TabHolder.Controls.Add(Saveswapper);
            Saveswapper.Visible = true;
            if (RPCclient.IsInitialized)
            {
                RPCclient.UpdateDetails("Save Swapping (For Free)");
                RPCclient.UpdateSmallAsset("save", "save swapping");
                RPCclient.SynchronizeState();
            }
        }
        private void BTN_TabLiveTuning_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                MessageBox.Show("Live Tuning is currently a wip poc.\nSome values wont display as what they really are.\n(Expect bugs.)");
                ClearColours();
                BTN_TabLiveTuning.BackColor = Color.FromArgb(45, 45, 48);
                if (ThemeColour != "Rainbow")
                {
                    Panel_LiveTuning.BackColor = ColorTranslator.FromHtml(ThemeColour);
                    LiveTuning.l.Panel_Tyres.BackColor = ColorTranslator.FromHtml(ThemeColour);
                }
                ClearTabItems();
                TabHolder.Controls.Add(LiveTuning);
                LiveTuning.Visible = true;
                TabForms.LiveTuningForms.Tyres.t.TyreRefresh();
                TabForms.LiveTuningForms.Gears.g.GearsRefresh();
                TabForms.LiveTuningForms.Alignment.a.CamberRefresh();
                TabForms.LiveTuningForms.Alignment.a.ToeRefresh();
                if (RPCclient.IsInitialized)
                {
                    RPCclient.UpdateDetails("Live Tuning");
                    RPCclient.UpdateSmallAsset("tuning", "tuning");
                    RPCclient.SynchronizeState();
                }
            }
            else
            {
                ;
            }
        }
        private void BTN_TabSpeedhack_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                ClearColours();
                BTN_TabSpeedhack.BackColor = Color.FromArgb(45, 45, 48);
                if (ThemeColour != "Rainbow")
                    Panel_Speedhack.BackColor = ColorTranslator.FromHtml(ThemeColour);
                ClearTabItems();
                TabHolder.Controls.Add(speedhack);
                speedhack.Visible = true;
                if (RPCclient.IsInitialized)
                {
                    RPCclient.UpdateDetails("Vroooming");
                    RPCclient.UpdateSmallAsset("speed", "Vroom");
                    RPCclient.SynchronizeState();
                }
                if(FirstLoad)
                    speedhack.ReadSpeedDefaultValues(); speedhack.SHReset(); FirstLoad = false;
            }
            else
            {
                ;
            }
        }
        private void BTN_TabInfo_MouseEnter(object sender, EventArgs e)
        {
            if (ToolInfo.Visible==false)
                Panel_Info.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabInfo_MouseLeave(object sender, EventArgs e)
        {
            if (ToolInfo.Visible == false)
                Panel_Info.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabAddCars_MouseEnter(object sender, EventArgs e)
        {
            if (AddCars.Visible == false)
                Panel_AddCars.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabAddCars_MouseLeave(object sender, EventArgs e)
        {
            if (AddCars.Visible == false)
                Panel_AddCars.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabStatsEditor_MouseEnter(object sender, EventArgs e)
        {
            if (StatsEditor.Visible == false)
                Panel_StatsEditor.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabStatsEditor_MouseLeave(object sender, EventArgs e)
        {
            if (StatsEditor.Visible == false)
                Panel_StatsEditor.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabSaveswap_MouseEnter(object sender, EventArgs e)
        {
            if (Saveswapper.Visible == false)
                Panel_Saveswap.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabSaveswap_MouseLeave(object sender, EventArgs e)
        {
            if (Saveswapper.Visible == false)
                Panel_Saveswap.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabLiveTuning_MouseEnter(object sender, EventArgs e)
        {
            if (LiveTuning.Visible == false)
                Panel_LiveTuning.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabLiveTuning_MouseLeave(object sender, EventArgs e)
        {
            if (LiveTuning.Visible == false)
                Panel_LiveTuning.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabSpeedhack_MouseEnter(object sender, EventArgs e)
        {
            if (speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabSpeedhack_MouseLeave(object sender, EventArgs e)
        {
            if (speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(28, 28, 28);
        }
    }
}
