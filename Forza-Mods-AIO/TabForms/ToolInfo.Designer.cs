
namespace Forza_Mods_AIO
{
    partial class ToolInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolInfo));
            this.AOBScanProgress = new System.Windows.Forms.ProgressBar();
            this.TXT_InfoTab = new System.Windows.Forms.RichTextBox();
            this.LBL_Attached = new System.Windows.Forms.Label();
            this.CheckAttachedworker = new System.ComponentModel.BackgroundWorker();
            this.InitialBGworker = new System.ComponentModel.BackgroundWorker();
            this.DraffsYT = new System.Windows.Forms.Label();
            this.UCPost = new System.Windows.Forms.Label();
            this.Discord = new System.Windows.Forms.Label();
            this.DraffsYTLink = new System.Windows.Forms.Label();
            this.UCPostLink = new System.Windows.Forms.Label();
            this.DiscordLink = new System.Windows.Forms.Label();
            this.Mute = new System.Windows.Forms.CheckBox();
            this.Volumeworker = new System.ComponentModel.BackgroundWorker();
            this.VolNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.VolNum)).BeginInit();
            this.SuspendLayout();
            // 
            // AOBScanProgress
            // 
            this.AOBScanProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.AOBScanProgress.Location = new System.Drawing.Point(10, 27);
            this.AOBScanProgress.Name = "AOBScanProgress";
            this.AOBScanProgress.Size = new System.Drawing.Size(159, 23);
            this.AOBScanProgress.TabIndex = 18;
            // 
            // TXT_InfoTab
            // 
            this.TXT_InfoTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TXT_InfoTab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXT_InfoTab.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TXT_InfoTab.Font = new System.Drawing.Font("Open Sans", 13F);
            this.TXT_InfoTab.ForeColor = System.Drawing.Color.White;
            this.TXT_InfoTab.Location = new System.Drawing.Point(1, 73);
            this.TXT_InfoTab.Margin = new System.Windows.Forms.Padding(0);
            this.TXT_InfoTab.Name = "TXT_InfoTab";
            this.TXT_InfoTab.ReadOnly = true;
            this.TXT_InfoTab.Size = new System.Drawing.Size(998, 195);
            this.TXT_InfoTab.TabIndex = 17;
            this.TXT_InfoTab.TabStop = false;
            this.TXT_InfoTab.Text = resources.GetString("TXT_InfoTab.Text");
            // 
            // LBL_Attached
            // 
            this.LBL_Attached.AutoSize = true;
            this.LBL_Attached.Font = new System.Drawing.Font("Open Sans", 12F);
            this.LBL_Attached.ForeColor = System.Drawing.Color.Red;
            this.LBL_Attached.Location = new System.Drawing.Point(6, 2);
            this.LBL_Attached.Margin = new System.Windows.Forms.Padding(3);
            this.LBL_Attached.Name = "LBL_Attached";
            this.LBL_Attached.Size = new System.Drawing.Size(163, 22);
            this.LBL_Attached.TabIndex = 16;
            this.LBL_Attached.Text = "Not Attached to FH4";
            // 
            // DraffsYT
            // 
            this.DraffsYT.AutoSize = true;
            this.DraffsYT.Font = new System.Drawing.Font("Open Sans", 13F);
            this.DraffsYT.Location = new System.Drawing.Point(-3, 282);
            this.DraffsYT.Name = "DraffsYT";
            this.DraffsYT.Size = new System.Drawing.Size(141, 24);
            this.DraffsYT.TabIndex = 19;
            this.DraffsYT.Text = "Draffs Youtube:";
            // 
            // UCPost
            // 
            this.UCPost.AutoSize = true;
            this.UCPost.Font = new System.Drawing.Font("Open Sans", 13F);
            this.UCPost.Location = new System.Drawing.Point(-3, 306);
            this.UCPost.Name = "UCPost";
            this.UCPost.Size = new System.Drawing.Size(299, 24);
            this.UCPost.TabIndex = 19;
            this.UCPost.Text = "Weebthans UnknownCheats post: ";
            // 
            // Discord
            // 
            this.Discord.AutoSize = true;
            this.Discord.Font = new System.Drawing.Font("Open Sans", 13F);
            this.Discord.Location = new System.Drawing.Point(-3, 330);
            this.Discord.Name = "Discord";
            this.Discord.Size = new System.Drawing.Size(182, 24);
            this.Discord.TabIndex = 19;
            this.Discord.Text = "Forza Mods Discord:";
            // 
            // DraffsYTLink
            // 
            this.DraffsYTLink.AutoSize = true;
            this.DraffsYTLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DraffsYTLink.Font = new System.Drawing.Font("Open Sans", 13F);
            this.DraffsYTLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.DraffsYTLink.Location = new System.Drawing.Point(133, 282);
            this.DraffsYTLink.Name = "DraffsYTLink";
            this.DraffsYTLink.Size = new System.Drawing.Size(556, 24);
            this.DraffsYTLink.TabIndex = 19;
            this.DraffsYTLink.Text = "https://www.youtube.com/channel/UCwQ8XprkEbBJ3UaBYT_F8jA\r\n";
            this.DraffsYTLink.Click += new System.EventHandler(this.DraffsYTLink_Click);
            // 
            // UCPostLink
            // 
            this.UCPostLink.AutoSize = true;
            this.UCPostLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UCPostLink.Font = new System.Drawing.Font("Open Sans", 13F);
            this.UCPostLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.UCPostLink.Location = new System.Drawing.Point(285, 306);
            this.UCPostLink.Name = "UCPostLink";
            this.UCPostLink.Size = new System.Drawing.Size(703, 24);
            this.UCPostLink.TabIndex = 19;
            this.UCPostLink.Text = "https://www.unknowncheats.me/forum/other-games/415227-fh4-speed-hack.html";
            this.UCPostLink.Click += new System.EventHandler(this.UCPostLink_Click);
            // 
            // DiscordLink
            // 
            this.DiscordLink.AutoSize = true;
            this.DiscordLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DiscordLink.Font = new System.Drawing.Font("Open Sans", 13F);
            this.DiscordLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.DiscordLink.Location = new System.Drawing.Point(174, 330);
            this.DiscordLink.Name = "DiscordLink";
            this.DiscordLink.Size = new System.Drawing.Size(284, 24);
            this.DiscordLink.TabIndex = 19;
            this.DiscordLink.Text = "https://discord.gg/PQNxeYWUy9";
            this.DiscordLink.Click += new System.EventHandler(this.DiscordLink_Click);
            // 
            // Mute
            // 
            this.Mute.AutoSize = true;
            this.Mute.Location = new System.Drawing.Point(911, 31);
            this.Mute.Name = "Mute";
            this.Mute.Size = new System.Drawing.Size(84, 19);
            this.Mute.TabIndex = 20;
            this.Mute.Text = "Mute Forza";
            this.Mute.UseVisualStyleBackColor = true;
            this.Mute.CheckedChanged += new System.EventHandler(this.Mute_CheckedChanged);
            // 
            // Volumeworker
            // 
            this.Volumeworker.WorkerReportsProgress = true;
            this.Volumeworker.WorkerSupportsCancellation = true;
            this.Volumeworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Volumeworker_DoWork);
            // 
            // VolNum
            // 
            this.VolNum.Location = new System.Drawing.Point(911, 5);
            this.VolNum.Name = "VolNum";
            this.VolNum.Size = new System.Drawing.Size(84, 22);
            this.VolNum.TabIndex = 21;
            // 
            // ToolInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 445);
            this.Controls.Add(this.VolNum);
            this.Controls.Add(this.Mute);
            this.Controls.Add(this.UCPostLink);
            this.Controls.Add(this.Discord);
            this.Controls.Add(this.UCPost);
            this.Controls.Add(this.DiscordLink);
            this.Controls.Add(this.DraffsYTLink);
            this.Controls.Add(this.DraffsYT);
            this.Controls.Add(this.AOBScanProgress);
            this.Controls.Add(this.TXT_InfoTab);
            this.Controls.Add(this.LBL_Attached);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolInfo";
            this.Text = "ToolInfo";
            ((System.ComponentModel.ISupportInitialize)(this.VolNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar AOBScanProgress;
        public System.Windows.Forms.RichTextBox TXT_InfoTab;
        public System.Windows.Forms.Label LBL_Attached;
        public System.ComponentModel.BackgroundWorker CheckAttachedworker;
        public System.ComponentModel.BackgroundWorker InitialBGworker;
        private System.Windows.Forms.Label DraffsYT;
        private System.Windows.Forms.Label UCPost;
        private System.Windows.Forms.Label Discord;
        private System.Windows.Forms.Label DraffsYTLink;
        private System.Windows.Forms.Label UCPostLink;
        private System.Windows.Forms.Label DiscordLink;
        private System.Windows.Forms.CheckBox Mute;
        private System.ComponentModel.BackgroundWorker Volumeworker;
        private System.Windows.Forms.NumericUpDown VolNum;
    }
}