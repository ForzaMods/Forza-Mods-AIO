
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
            this.TXT_InfoTab.Size = new System.Drawing.Size(998, 369);
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
            // InitialBGworker
            // 
            this.InitialBGworker.WorkerReportsProgress = true;
            this.InitialBGworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InitialBGworker_DoWork);
            this.InitialBGworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.InitialBGworker_ProgressChanged);
            // 
            // ToolInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 445);
            this.Controls.Add(this.AOBScanProgress);
            this.Controls.Add(this.TXT_InfoTab);
            this.Controls.Add(this.LBL_Attached);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolInfo";
            this.Text = "ToolInfo";
            this.Load += new System.EventHandler(this.ToolInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar AOBScanProgress;
        private System.Windows.Forms.RichTextBox TXT_InfoTab;
        private System.Windows.Forms.Label LBL_Attached;
        private System.ComponentModel.BackgroundWorker CheckAttachedworker;
        private System.ComponentModel.BackgroundWorker InitialBGworker;
    }
}