using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Memory;
using Forza_Mods_AIO.TabForms;

namespace Forza_Mods_AIO
{
    public partial class ToolInfo : Form
    {
        public ToolInfo()
        {
            InitializeComponent();
        }
        Mem m = new Mem();
        private void InitialBGworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1);
                if (!m.OpenProcess("ForzaHorizon4"))
                {
                    Speedhack.IsAttached = false;
                    InitialBGworker.ReportProgress(0);
                    Thread.Sleep(1000);
                    continue;
                }
                if (Speedhack.done == false)
                {
                    //MainWindow.DisableButtons();
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
                    LBL_Attached.Text = "Attached to FH4";
                    LBL_Attached.ForeColor = Color.Green;
                    //MainWindow.EnableButtons();
                    AOBScanProgress.Hide();
                }
            }
            else
            {
                LBL_Attached.Text = "Not Attached to FH4";
                LBL_Attached.ForeColor = Color.Red;
                //MainWindow.DisableButtons();
            }
        }
        private void InitialBGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }
        public void AoBscan()
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon4")[0];
            SigScanSharp Sigscan = new SigScanSharp(TargetProcess.Handle);
            Sigscan.SelectModule(TargetProcess.MainModule);

            if (Speedhack.done == false)
            {
                AOBScanProgress.Show();
                long lTime;
                if (Speedhack.BaseAddr == "1DD0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0")
                {
                    Speedhack.BaseAddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) + 7632;
                    Speedhack.BaseAddr = Speedhack.BaseAddrLong.ToString("X");
                }
                else if (Speedhack.Base2Addr == "2F70" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0")
                {
                    AOBScanProgress.Value = 13;
                    Speedhack.Base2AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) + 12144;
                    Speedhack.Base2Addr = Speedhack.Base2AddrLong.ToString("X");
                }
                else if (Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0")
                {
                    AOBScanProgress.Value = 25;
                    Speedhack.Base3AddrLong = (long)Sigscan.FindPattern(Speedhack.Base, out lTime) - 3328;
                    Speedhack.Base3Addr = Speedhack.Base3AddrLong.ToString("X");
                }
                else if (Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0")
                {
                    AOBScanProgress.Value = 38;
                    Speedhack.Car1AddrLong = (long)Sigscan.FindPattern(Speedhack.Car1, out lTime) + 106;
                    Speedhack.Car1Addr = Speedhack.Car1AddrLong.ToString("X");
                }
                else if (Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0")
                {
                    AOBScanProgress.Value = 50;
                    Speedhack.Car2AddrLong = (long)Sigscan.FindPattern(Speedhack.Car2, out lTime) - 411;
                    Speedhack.Car2Addr = Speedhack.Car2AddrLong.ToString("X");
                }
                else if (Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0")
                {
                    AOBScanProgress.Value = 63;
                    Speedhack.Wall1AddrLong = (long)Sigscan.FindPattern(Speedhack.Wall1, out lTime) + 401;
                    Speedhack.Wall1Addr = Speedhack.Wall1AddrLong.ToString("X");
                }
                else if (Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0")
                {
                    AOBScanProgress.Value = 75;
                    Speedhack.Wall2AddrLong = (long)Sigscan.FindPattern(Speedhack.Wall2, out lTime) - 446;
                    Speedhack.Wall2Addr = Speedhack.Wall2AddrLong.ToString("X");
                }
                else if (Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0")
                {
                    AOBScanProgress.Value = 80;
                    Speedhack.FOVnopOutAddrLong = (long)Sigscan.FindPattern(Speedhack.FOVOutsig, out lTime) + 123;
                    Speedhack.FOVnopOutAddr = Speedhack.FOVnopOutAddrLong.ToString("X");
                }
                else if (Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0")
                {
                    AOBScanProgress.Value = 85;
                    Speedhack.FOVnopInAddrLong = (long)Sigscan.FindPattern(Speedhack.FOVInsig, out lTime) + 1383;
                    Speedhack.FOVnopInAddr = Speedhack.FOVnopInAddrLong.ToString("X");
                }
                if (Speedhack.BaseAddr == "1DD0" || Speedhack.BaseAddr == null || Speedhack.BaseAddr == "0"
                    || Speedhack.Base2Addr == "2F70" || Speedhack.Base2Addr == null || Speedhack.Base2Addr == "0"
                    || Speedhack.Base3Addr == "FFFFFFFFFFFFF300" || Speedhack.Base3Addr == null || Speedhack.Base3Addr == "0"
                    || Speedhack.Car1Addr == "6A" || Speedhack.Car1Addr == null || Speedhack.Car1Addr == "0"
                    || Speedhack.Car2Addr == "FFFFFFFFFFFFFE65" || Speedhack.Car2Addr == null || Speedhack.Car2Addr == "0"
                    || Speedhack.Wall1Addr == "191" || Speedhack.Wall1Addr == null || Speedhack.Wall1Addr == "0"
                    || Speedhack.Wall2Addr == "FFFFFFFFFFFFFE42" || Speedhack.Wall2Addr == null || Speedhack.Wall2Addr == "0"
                    || Speedhack.FOVnopInAddr == "567" || Speedhack.FOVnopInAddr == null || Speedhack.FOVnopInAddr == "0"
                    || Speedhack.FOVnopOutAddr == "7B" || Speedhack.FOVnopOutAddr == null || Speedhack.FOVnopOutAddr == "0")
                {
                    ;
                }
                else
                {
                    AOBScanProgress.Value = 100;
                    Speedhack.Addresses();
                    Speedhack.done = true;
                }
                Thread.Sleep(1);
            }
        }

        private void ToolInfo_Load(object sender, System.EventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }
    }

}
