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
        public static ToolInfo t;
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
    }
}
