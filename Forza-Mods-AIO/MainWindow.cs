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
        public DiscordRpcClient RPCclient = new DiscordRpcClient("841090098837323818");
        public ToolInfo ToolInfo = new ToolInfo() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public AddCars AddCars = new AddCars() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public StatsEditor StatsEditor = new StatsEditor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public Saveswapper Saveswapper = new Saveswapper() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public LiveTuning LiveTuning = new LiveTuning() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public Speedhack Speedhack = new Speedhack() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public bool IsRPCInitialized = false; public bool FirstLoad = true;
        public static string ThemeColour = "#960ba6";
        DialogResult UpdateYesNo;
        Version NewVer = null;
        public static Version CurrVer = new Version("0.0.0.1");
        string DL = null;
        long sig = 0;
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
            RPCclient.Initialize();
            DiscordRPC.Button[] Buttons = new DiscordRPC.Button[]
            {
                new DiscordRPC.Button() { Label = "Discord Link", Url = "https://discord.gg/N3m6E5V" },
                new DiscordRPC.Button() { Label = "Download", Url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ" }
            };
            RPCclient.SetPresence(new RichPresence()
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
                    SmallImageText = "reading info"
                }
            });
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            ToolInfo.AOBScanProgress.Hide();
            using (WebClient client = new WebClient())
            {
                string Response = client.DownloadString(@"https://yeethan69.github.io/aioUpdate.txt");
                string[] VerAndLink = Response.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
                NewVer = new Version(VerAndLink[0].Split('|').Last());
            }
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
                RPCclient.UpdateDetails("reading info");
                RPCclient.UpdateSmallAsset("home", "Info");
            }

            if (!File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata"))
            {
                try
                {
                    using (var client = new WebClient())
                    {
                         client.DownloadFile("https://cdn.discordapp.com/attachments/788949255749500958/874046559887687720/xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip");
                        /*remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release
 * remove this before release*/
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\1");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\2");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\3");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\2021-07-22 18-07-30-691");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\2021-07-22 17-33-56");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                        File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini");
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
                    //try
                    //{
                        using (var client = new WebClient())
                        {
                            client.DownloadFile("https://cdn.discordapp.com/attachments/788949255749500958/874046559887687720/xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip");
                            /*remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release
                             * remove this before release*/
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\1");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\2");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\3");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\2021-07-22 18-07-30-691");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups\2021-07-22 17-33-56");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                            File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Settings.ini");
                        ZipFile.ExtractToDirectory(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\xW2ye4iaCGSekMth.zip", @"C:\Users\" + Environment.UserName + @"\Documents\");
                        }
                    //}
                    /*catch
                    {
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS");
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\MS\Backups");
                        File.Create(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\SaveMetadata");
                    }*/
                }

            }
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
                SettingsParser.SaveFile(SettingsPath, Settings);
                Settings = SettingsParser.ReadFile(SettingsPath);
                float Speed = Convert.ToSingle(Settings["Settings"]["Rainbow Speed"]);
                string TC = Settings["Settings"]["Theme Colour"];
                if (Speed > 10)
                    Speed = 10;
                else if (Speed < 1)
                    Speed = 1;
                ToolInfo.Rainbowspeed = Speed;
                ToolInfo.RainbowSpeed.Value = (decimal)Speed;
                ToolInfo.VolNum.Value = int.Parse(Settings["Settings"]["Volume"]);
                if (bool.Parse(Settings["Settings"]["Volume Control"]))
                    ToolInfo.Mute.Checked = true;
                if (TC == "Rainbow")
                    ToolInfo.RainbowBox.Checked = true;
                else
                {
                    ToolInfo.RainbowSpeed.Enabled = false;
                    MainWindow.ThemeColour = TC;
                    ToolInfo.UpdateThemeColour(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourSlider.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                    ToolInfo.ColourSlider.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                }
            }
            else
            {
                var SettingsParser = new FileIniDataParser();
                IniData Settings = SettingsParser.ReadFile(SettingsPath);
                string TC = Settings["Settings"]["Theme Colour"];
                float Speed = Convert.ToSingle(Settings["Settings"]["Rainbow Speed"]);
                if (Speed > 10)
                    Speed = 10;
                else if (Speed < 1)
                    Speed = 1;
                ToolInfo.Rainbowspeed = Speed;
                ToolInfo.RainbowSpeed.Value = (decimal)Speed;
                ToolInfo.VolNum.Value = int.Parse(Settings["Settings"]["Volume"]);
                if (bool.Parse(Settings["Settings"]["Volume Control"]))
                    ToolInfo.Mute.Checked = true;
                if (TC == "Rainbow")
                    ToolInfo.RainbowBox.Checked = true;
                else
                {
                    ToolInfo.RainbowSpeed.Enabled = false;
                    MainWindow.ThemeColour = TC;
                    ToolInfo.UpdateThemeColour(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourSlider.ColorHSL = new HslColor(ColorTranslator.FromHtml(MainWindow.ThemeColour));
                    ToolInfo.ColourPicker.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                    ToolInfo.ColourSlider.ColorRGB = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
            dragCursorPoint = System.Windows.Forms.Cursor.Position;
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
                if (!m.OpenProcess("ForzaHorizon4"))
                {
                    Speedhack.IsAttached = false;
                    ToolInfo.AOBScanProgress.Hide();
                    InitialBGworker.ReportProgress(0);
                    Thread.Sleep(1000);
                    continue;
                }
                else if (Speedhack.done == false)
                {
                    DisableButtons();
                    if(count)
                    {
                        //ToolInfo.AOBScanProgress.Show();
                        AoBscan();
                    }
                    count = false;
                }
                else
                {
                    Speedhack.IsAttached = true;
                    Thread.Sleep(500);
                    InitialBGworker.ReportProgress(0);
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
            if (Speedhack.IsAttached)
            {
                if (Speedhack.done == true)
                {
                    ToolInfo.LBL_Attached.Text = "Attached to FH4";
                    ToolInfo.LBL_Attached.ForeColor = Color.Green;
                    if (m.ReadByte(Speedhack.PastStartAddr) == 1)
                    {
                        Speedhack.CheckpointBox.Enabled = true;
                        Speedhack.AutoWayPoint.Enabled = true;
                        Speedhack.TimeCheckBox.Enabled = true;
                        Speedhack.XPBox.Enabled = true;
                        Speedhack.TimerButton.Enabled = true;
                    }
                    EnableButtons();
                    ToolInfo.AOBScanProgress.Hide();
                }
            }
            else
            {
                ToolInfo.LBL_Attached.Text = "Not Attached to FH4";
                if(!ToolInfo.Visible && !Saveswapper.Visible)
                {
                    ToolInfo.LBL_Attached.ForeColor = Color.Red;
                    DisableButtons();
                    ClearColours();
                    BTN_TabInfo.BackColor = Color.FromArgb(45, 45, 48);
                    Panel_Info.BackColor = Color.FromArgb(150, 11, 166);
                    ClearTabItems();
                    this.TabHolder.Controls.Add(ToolInfo);
                    ToolInfo.Visible = true;
                }
                RPCclient.UpdateDetails("Reading Info");
                RPCclient.UpdateSmallAsset("home", "reading info");
                RPCclient.SynchronizeState();
                Speedhack.FrontAddr = "0"; Speedhack.DashAddr = "0"; Speedhack.LowAddr = "0"; Speedhack.BonnetAddr = "0"; Speedhack.FirstPersonAddr = "0";
                Speedhack.FrontAddrLong = 0; Speedhack.DashAddrLong = 0; Speedhack.LowAddrLong = 0; Speedhack.BonnetAddrLong = 0; Speedhack.FirstPersonAddrLong = 0;
                Speedhack.cycles = 0;
            }
        }
        private void InitialBGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }

        public async void AoBscan()
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon4")[0];
            SigScanSharp Sigscan = new SigScanSharp(TargetProcess.Handle);
            Sigscan.SelectModule(TargetProcess.MainModule);
            while (Speedhack.done == false)
            {
                if (TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                {
                    if (!ToolInfo.AOBScanProgress.Visible)
                        ToolInfo.AOBScanProgress.Show();
                    //long lTime;
                    Thread.Sleep(1);
                    if (Speedhack.Base2Addr == "3B40" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0")
                    {
                        Speedhack.Base2AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Base, true, true)).FirstOrDefault() + 15168;
                        Speedhack.Base2Addr = Speedhack.Base2AddrLong.ToString("X");
                        //Speedhack.Base2AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) + 15168;
                        //Speedhack.Base2Addr = Speedhack.Base2AddrLong.ToString("X");
                    }
                    else if (Speedhack.BaseAddr == "29A0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 6; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.VolumeSetup();
                        Speedhack.BaseAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Base, true, true)).FirstOrDefault() + 10656;
                        Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                    }
                    else if (Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 13; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Base3AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Base, true, true)).FirstOrDefault() - 3328;
                        Speedhack.Base3Addr = Speedhack.Base3AddrLong.ToString("X");
                    }
                    else if (Speedhack.Base4Addr == "BA18" || Speedhack.Base4Addr == null || Speedhack.Base4Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 19; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Base4AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Base, true, true)).FirstOrDefault() - 47640;
                        Speedhack.Base4Addr = Speedhack.Base4AddrLong.ToString("X");
                    }
                    else if (Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 25; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Car1AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Car1, true, true)).FirstOrDefault() + 106;
                        Speedhack.Car1Addr = Speedhack.Car1AddrLong.ToString("X");
                    }
                    else if (Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 31; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Car2AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Car2, true, true)).FirstOrDefault() - 411;
                        Speedhack.Car2Addr = Speedhack.Car2AddrLong.ToString("X");
                    }
                    else if (Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 38; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Wall1AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Wall1, true, true)).FirstOrDefault() + 401;
                        Speedhack.Wall1Addr = Speedhack.Wall1AddrLong.ToString("X");
                    }
                    else if (Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 44; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.Wall2AddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Wall2, true, true)).FirstOrDefault() - 446;
                        Speedhack.Wall2Addr = Speedhack.Wall2AddrLong.ToString("X");
                    }
                    else if (Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 50; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        //ToolInfo.Mute.Enabled = true;
                        Speedhack.FOVnopOutAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.FOVOutsig, true, true)).FirstOrDefault() + 123;
                        Speedhack.FOVnopOutAddr = Speedhack.FOVnopOutAddrLong.ToString("X");
                    }
                    else if (Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 56; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.FOVnopInAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.FOVInsig, true, true)).FirstOrDefault() + 1383;
                        Speedhack.FOVnopInAddr = Speedhack.FOVnopInAddrLong.ToString("X");
                    }
                    else if (Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 62; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.TimeNOPAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.Timesig, true, true)).FirstOrDefault() + 1;
                        Speedhack.TimeNOPAddr = Speedhack.TimeNOPAddrLong.ToString("X");
                    }
                    else if (Speedhack.CheckPointxASMAddr == null || Speedhack.CheckPointxASMAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 69; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.CheckPointxASMAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.CheckPointxASMsig, true, true)).FirstOrDefault();
                        Speedhack.CheckPointxASMAddr = Speedhack.CheckPointxASMAddrLong.ToString("X");
                    }
                    else if (Speedhack.WayPointxASMAddr == null || Speedhack.WayPointxASMAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 75; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.WayPointxASMAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.WayPointxASMsig, true, true)).FirstOrDefault();
                        Speedhack.WayPointxASMAddr = Speedhack.WayPointxASMAddrLong.ToString("X");
                    }
                    else if (Speedhack.XPaddr == null || Speedhack.XPaddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 81; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.XPaddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.XPaob, true, true)).FirstOrDefault();
                        Speedhack.XPaddr = Speedhack.XPaddrLong.ToString("X");
                    }
                    else if (Speedhack.XPAmountaddr == null || Speedhack.XPAmountaddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 88; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.XPAmountaddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.XPAmountaob, true, true)).FirstOrDefault();
                        Speedhack.XPAmountaddr = Speedhack.XPAmountaddrLong.ToString("X");
                    }
                    else if (Speedhack.CurrentIDAddr == "2A" || Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "0")
                    {
                        for (int i = ToolInfo.AOBScanProgress.Value1; i <= 94; i++)
                        { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                        Speedhack.CurrentIDAddrLong = (await m.AoBScan(0x7FF000000000, 0x7FFFF0000000, Speedhack.CurrentIDaob, true, true)).FirstOrDefault() + 42;
                        Speedhack.CurrentIDAddr = Speedhack.CurrentIDAddrLong.ToString("X");
                    }
                }
                else
                {

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
                    || Speedhack.CurrentIDAddr == "2A" || Speedhack.CurrentIDAddr == null || Speedhack.CurrentIDAddr == "0")
                    )
                {
                    ;
                }
                else
                {
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
                    Speedhack.FOVScan_BTN.Show(); Speedhack.FOVScan_bar.Hide(); Speedhack.FOV.Hide();
                    for (int i = ToolInfo.AOBScanProgress.Value1; i <= 100; i++)
                    { Thread.Sleep(15); ToolInfo.AOBScanProgress.Value1 = i; }
                    LiveTuning.Addresses();
                    Speedhack.Addresses();
                    if (m.ReadByte(Speedhack.PastStartAddr) == 0)
                    {
                        Speedhack.CheckpointBox.Enabled = false;
                        Speedhack.AutoWayPoint.Enabled = false;
                        Speedhack.TimeCheckBox.Enabled = false;
                        Speedhack.XPBox.Enabled = false;
                        Speedhack.TimerButton.Enabled = false;
                    }
                    Speedhack.ReadSpeedDefaultValues();
                    Speedhack.done = true;
                }
                Thread.Sleep(1);
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
            Speedhack.Visible = false;
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
            BTN_TabAddCars.Enabled = true;
            BTN_TabStatsEditor.Enabled = true;
            BTN_TabLiveTuning.Enabled = true;
            BTN_TabSpeedhack.Enabled = true;
        }
        private void BTN_Close_Click(object sender, EventArgs e)
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
            if (Speedhack.done)
            {
                m.WriteBytes(Speedhack.Wall1Addr, Jmp1before);
                m.WriteBytes(Speedhack.Wall2Addr, Jmp2before);
                m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                m.WriteBytes(Speedhack.Car2Addr, Jmp4before);
                m.WriteBytes(Speedhack.TimeNOPAddr, NOPBefore);
                m.WriteBytes(Speedhack.FOVnopOutAddr, nopoutbefore);
                m.WriteBytes(Speedhack.FOVnopInAddr, nopinbefore);
                MainWindow.m.WriteBytes(Speedhack.XPaddr, XPGiveBefore);
                MainWindow.m.WriteBytes(Speedhack.XPAmountaddr, Normal);
                MainWindow.m.WriteBytes(Speedhack.WayPointxASMAddr, WayPointCodeBefore);
                try { m.WriteBytes(Speedhack.CheckPointxASMAddr, original); } catch { }
            }
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
            RPCclient.UpdateDetails("Reading Info");
            RPCclient.UpdateSmallAsset("home", "reading info");
            RPCclient.SynchronizeState();
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
                RPCclient.UpdateDetails("Adding Cars");
                RPCclient.UpdateSmallAsset("car", "adding cars");
                RPCclient.SynchronizeState();
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
                ClearColours();
                BTN_TabStatsEditor.BackColor = Color.FromArgb(45, 45, 48);
                if (ThemeColour != "Rainbow")
                    Panel_StatsEditor.BackColor = ColorTranslator.FromHtml(ThemeColour);
                ClearTabItems();
                this.TabHolder.Controls.Add(StatsEditor);
                StatsEditor.Visible = true;
                RPCclient.UpdateDetails("Editing Stats");
                RPCclient.UpdateSmallAsset("stats", "editing stats");
                RPCclient.SynchronizeState();
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
            RPCclient.UpdateDetails("Save Swapping (For Free)");
            RPCclient.UpdateSmallAsset("save", "save swapping");
            RPCclient.SynchronizeState();

        }
        private void BTN_TabLiveTuning_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                ClearColours();
                BTN_TabLiveTuning.BackColor = Color.FromArgb(45, 45, 48);
                if (ThemeColour != "Rainbow")
                    Panel_LiveTuning.BackColor = ColorTranslator.FromHtml(ThemeColour);
                ClearTabItems();
                TabHolder.Controls.Add(LiveTuning);
                LiveTuning.Visible = true;
                TabForms.LiveTuningForms.Tyres.t.TyreRefresh();
                TabForms.LiveTuningForms.Gears.g.GearsRefresh();
                TabForms.LiveTuningForms.Alignment.a.CamberRefresh();
                TabForms.LiveTuningForms.Alignment.a.ToeRefresh();
                RPCclient.UpdateDetails("Live Tuning");
                RPCclient.UpdateSmallAsset("tuning", "tuning");
                RPCclient.SynchronizeState();
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
                TabHolder.Controls.Add(Speedhack);
                Speedhack.Visible = true;
                RPCclient.UpdateDetails("Vroooming");
                RPCclient.UpdateSmallAsset("speed", "Vroom");
                RPCclient.SynchronizeState();
                if(FirstLoad)
                    Speedhack.ReadSpeedDefaultValues(); Speedhack.SHReset(); FirstLoad = false;
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
            if (Speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabSpeedhack_MouseLeave(object sender, EventArgs e)
        {
            if (Speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(28, 28, 28);
        }
    }
}
