using System;
using System.IO;
using System.IO.Compression;
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
using DiscordRPC;
using System.Net;

namespace Forza_Mods_AIO
{
    public partial class MainWindow : Form
    {
        public static Mem m = new Mem();
        public static MainWindow Main;
        public DiscordRpcClient client;
        public DiscordRpcClient RPCclient = new DiscordRpcClient("841090098837323818");
        ToolInfo ToolInfo = new ToolInfo() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        AddCars AddCars = new AddCars() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        StatsEditor StatsEditor = new StatsEditor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        Saveswapper Saveswapper = new Saveswapper() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        LiveTuning LiveTuning = new LiveTuning() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        Speedhack Speedhack = new Speedhack() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public bool IsRPCInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            this.TabHolder.Controls.Add(ToolInfo);
            ToolInfo.Visible = true;
            RPCclient.Initialize();
            DiscordRPC.Button[] Buttons = new DiscordRPC.Button[]
            {
                new DiscordRPC.Button() { Label = "Discord Link", Url = "https://discord.gg/N3m6E5V1" },
                //new DiscordRPC.Button() { Label = "Download", Url = "" }
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
            InitialBGworker.RunWorkerAsync();
            if(RPCclient.IsInitialized)
            {
                RPCclient.UpdateDetails("On info screen");
                RPCclient.UpdateSmallAsset("home", "Info");
            }
            if (! Directory.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool"))
            {
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool");
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper");
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames");
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://pixeldrain.com/api/file/Nr4R4wrR", @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\saves.zip");
                    ZipFile.ExtractToDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\saves.zip", @"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\");
                    File.Delete(@"C:\Users\" + Environment.UserName + @"\Documents\Forza Mods Tool\Saveswapper\Savegames\saves.zip");
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
                if (Speedhack.done == false)
                {
                    DisableButtons();
                    AoBscan();
                }
                else
                {
                    Speedhack.IsAttached = true;
                    Thread.Sleep(500);
                    InitialBGworker.ReportProgress(0);
                }
            }
        }
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
        private void InitialBGworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                if (Speedhack.done == true)
                {
                    ToolInfo.LBL_Attached.Text = "Attached to FH4";
                    ToolInfo.LBL_Attached.ForeColor = Color.Green;
                    EnableButtons();
                    ToolInfo.AOBScanProgress.Hide();
                }
            }
            else
            {
                ToolInfo.LBL_Attached.Text = "Not Attached to FH4";
                ToolInfo.LBL_Attached.ForeColor = Color.Red;
                DisableButtons();
            }
        }
        private void InitialBGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }

        public void AoBscan()
        {
            var TargetProcess = System.Diagnostics.Process.GetProcessesByName("ForzaHorizon4")[0];
            SigScanSharp Sigscan = new SigScanSharp(TargetProcess.Handle);
            Sigscan.SelectModule(TargetProcess.MainModule);

            if (Speedhack.done == false)
            {
                ToolInfo.AOBScanProgress.Show();
                long lTime;
                if (Speedhack.BaseAddr == "29A0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0")
                {
                    Speedhack.BaseAddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) + 10656;
                    Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                }
                else if (Speedhack.Base2Addr == "3B40" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 9;
                    Speedhack.Base2AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) + 15168;
                    Speedhack.Base2Addr = Speedhack.Base2AddrLong.ToString("X");
                }
                else if (Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 18;
                    Speedhack.Base3AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) - 3328;
                    Speedhack.Base3Addr = Speedhack.Base3AddrLong.ToString("X");
                }
                else if (Speedhack.Base4Addr == "BA18" || Speedhack.Base4Addr == null || Speedhack.Base4Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 27;
                    Speedhack.Base4AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) - 47640;
                    Speedhack.Base4Addr = Speedhack.Base4AddrLong.ToString("X");
                }
                else if (Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 36;
                    Speedhack.Car1AddrLong = (long)Sigscan.FindPattern(Speedhack.Car1, out lTime) + 106;
                    Speedhack.Car1Addr = Speedhack.Car1AddrLong.ToString("X");
                }
                else if (Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 45;
                    Speedhack.Car2AddrLong = (long)Sigscan.FindPattern(Speedhack.Car2, out lTime) - 411;
                    Speedhack.Car2Addr = Speedhack.Car2AddrLong.ToString("X");
                }
                else if (Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 54;
                    Speedhack.Wall1AddrLong = (long)Sigscan.FindPattern(Speedhack.Wall1, out lTime) + 401;
                    Speedhack.Wall1Addr = Speedhack.Wall1AddrLong.ToString("X");
                }
                else if (Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 63;
                    Speedhack.Wall2AddrLong = (long)Sigscan.FindPattern(Speedhack.Wall2, out lTime) - 446;
                    Speedhack.Wall2Addr = Speedhack.Wall2AddrLong.ToString("X");
                }
                else if (Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 72;
                    Speedhack.FOVnopOutAddrLong = (long)Sigscan.FindPattern(Speedhack.FOVOutsig, out lTime) + 123;
                    Speedhack.FOVnopOutAddr = Speedhack.FOVnopOutAddrLong.ToString("X");
                }
                else if (Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 81;
                    Speedhack.FOVnopInAddrLong = (long)Sigscan.FindPattern(Speedhack.FOVInsig, out lTime) + 1383;
                    Speedhack.FOVnopInAddr = Speedhack.FOVnopInAddrLong.ToString("X");
                }
                else if (Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0")
                {
                    ToolInfo.AOBScanProgress.Value = 90;
                    Speedhack.TimeNOPAddrLong = (long)Sigscan.FindPattern(Speedhack.Timesig, out lTime);
                    Speedhack.TimeNOPAddr = Speedhack.TimeNOPAddrLong.ToString("X");
                }
                if (Speedhack.BaseAddr == "29A0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0"
                    || Speedhack.Base2Addr == "3B40" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0"
                    || Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0"
                    || Speedhack.Base4Addr == "BA18" || Speedhack.Base4Addr == null || Speedhack.Base4Addr == "0"
                    || Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0"
                    || Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0"
                    || Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0"
                    || Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0"
                    || Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0"
                    || Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0"
                    || Speedhack.TimeNOPAddr == null || Speedhack.TimeNOPAddr == "0")
                {
                    ;
                }
                else
                {
                    Speedhack.FOVScan_BTN.Show(); Speedhack.FOVScan_bar.Hide(); Speedhack.FOV.Hide();
                    ToolInfo.AOBScanProgress.Value = 100;
                    LiveTuning.Addresses();
                    Speedhack.Addresses();
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
            if (Speedhack.done)
            {
                m.WriteBytes(Speedhack.Wall1Addr, Jmp1before);
                m.WriteBytes(Speedhack.Wall2Addr, Jmp2before);
                m.WriteBytes(Speedhack.Car1Addr, Jmp3before);
                m.WriteBytes(Speedhack.Car2Addr, Jmp4before);
                m.WriteBytes(Speedhack.TimeNOPAddr, NOPBefore);
                m.WriteBytes(Speedhack.FOVnopOutAddr, nopoutbefore);
                m.WriteBytes(Speedhack.FOVnopInAddr, nopinbefore);
            }
            this.Close();
        }
        private void BTN_MIN_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void BTN_TabInfo_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabInfo.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Info.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            this.TabHolder.Controls.Add(ToolInfo);
            ToolInfo.Visible = true;
            RPCclient.UpdateDetails("Reading Info");
            RPCclient.UpdateSmallAsset("home", "reading info");
        }
        private void BTN_TabAddCars_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                //do colours and hide/show ui
                ClearColours();
                BTN_TabAddCars.BackColor = Color.FromArgb(45, 45, 48);
                Panel_AddCars.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                this.TabHolder.Controls.Add(AddCars);
                AddCars.Visible = true;
                RPCclient.UpdateDetails("Adding Cars");
                RPCclient.UpdateSmallAsset("car", "adding cars");
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
                Panel_StatsEditor.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                this.TabHolder.Controls.Add(StatsEditor);
                StatsEditor.Visible = true;
                RPCclient.UpdateDetails("Editing Stats");
                RPCclient.UpdateSmallAsset("stats", "editing stats");
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
            Panel_Saveswap.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            this.TabHolder.Controls.Add(Saveswapper);
            Saveswapper.Visible = true;
            RPCclient.UpdateDetails("Save Swapping (For Free)");
            RPCclient.UpdateSmallAsset("save", "save swapping");

        }
        private void BTN_TabLiveTuning_Click(object sender, EventArgs e)
        {
            if (Speedhack.IsAttached)
            {
                ClearColours();
                BTN_TabLiveTuning.BackColor = Color.FromArgb(45, 45, 48);
                Panel_LiveTuning.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                this.TabHolder.Controls.Add(LiveTuning);
                LiveTuning.Visible = true;
                TabForms.LiveTuningForms.Tyres.t.TyreRefresh();
                TabForms.LiveTuningForms.Gears.g.GearsRefresh();
                TabForms.LiveTuningForms.Alignment.a.CamberRefresh();
                TabForms.LiveTuningForms.Alignment.a.ToeRefresh();
                RPCclient.UpdateDetails("Live Tuning");
                RPCclient.UpdateSmallAsset("tuning", "tuning");
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
                Panel_Speedhack.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                TabHolder.Controls.Add(Speedhack);
                Speedhack.Visible = true;
                Speedhack.ReadSpeedDefaultValues();
                Speedhack.SHReset();
                RPCclient.UpdateDetails("Vroooming");
                RPCclient.UpdateSmallAsset("speed", "Vroom");
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
