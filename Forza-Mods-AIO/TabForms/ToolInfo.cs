using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Memory;
using Forza_Mods_AIO.TabForms;
using System;
using System.Runtime.InteropServices;

namespace Forza_Mods_AIO
{
    public partial class ToolInfo : Form
    {
        public bool VolStart = false;
        public float? vol = null;
        public static ToolInfo t;
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public ToolInfo()
        {
            t = this;
            InitializeComponent();
        }

        private void DraffsYTLink_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.youtube.com/channel/UCwQ8XprkEbBJ3UaBYT_F8jA");
        }
        private void UCPostLink_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.unknowncheats.me/forum/other-games/415227-fh4-speed-hack.html");
        }
        private void DiscordLink_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://discord.gg/PQNxeYWUy9");
        }

        private void Mute_CheckedChanged(object sender, System.EventArgs e)
        {
            if(Mute.Checked == false)
            {
                VolStart = false;
                Volumeworker.CancelAsync();
                VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
            }
            else
            {
                VolStart = true;
                Volumeworker.RunWorkerAsync();
            }
        }

        private void Volumeworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(VolStart)
            {
                while (MainWindow.m.OpenProcess("ForzaHorizon4") && Speedhack.PastIntroAddr != null)
                {
                    if (MainWindow.m.ReadByte(Speedhack.PastIntroAddr) == 1)
                    {
                        VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                        VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                    }
                    else
                    {
                        while (MainWindow.m.ReadByte(Speedhack.PastIntroAddr) == 0)
                        {
                            VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, true);
                            VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
                            if (Volumeworker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                            Thread.Sleep(1);
                        }
                        Thread.Sleep(20000);
                        VolumeMixer.SetApplicationMute(Process.GetProcessesByName("ForzaHorizon4")[0].Id, false);
                        VolumeMixer.SetApplicationVolume(Process.GetProcessesByName("ForzaHorizon4")[0].Id, (float)VolNum.Value);
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
    }
}
