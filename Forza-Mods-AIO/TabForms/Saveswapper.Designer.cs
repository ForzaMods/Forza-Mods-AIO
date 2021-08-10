
namespace Forza_Mods_AIO.TabForms
{
    partial class Saveswapper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Saveswapper));
            this.LST_Accounts = new System.Windows.Forms.ListBox();
            this.LST_Savegames = new System.Windows.Forms.ListBox();
            this.TB_Backup = new System.Windows.Forms.CheckBox();
            this.TXT_SaveInfo = new System.Windows.Forms.RichTextBox();
            this.BTN_SwapSave = new System.Windows.Forms.Button();
            this.BTN_Backup = new System.Windows.Forms.Button();
            this.LBL_Account = new System.Windows.Forms.Label();
            this.GamertagResolve = new System.ComponentModel.BackgroundWorker();
            this.HiddenRadio = new System.Windows.Forms.RadioButton();
            this.GamebarAttach = new System.ComponentModel.BackgroundWorker();
            this.BTN_ACCRefresh = new System.Windows.Forms.Button();
            this.BTN_Help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LST_Accounts
            // 
            this.LST_Accounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.LST_Accounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LST_Accounts.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LST_Accounts.ForeColor = System.Drawing.Color.White;
            this.LST_Accounts.FormattingEnabled = true;
            this.LST_Accounts.ItemHeight = 18;
            this.LST_Accounts.Location = new System.Drawing.Point(12, 300);
            this.LST_Accounts.Name = "LST_Accounts";
            this.LST_Accounts.Size = new System.Drawing.Size(236, 126);
            this.LST_Accounts.TabIndex = 30;
            // 
            // LST_Savegames
            // 
            this.LST_Savegames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.LST_Savegames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LST_Savegames.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LST_Savegames.ForeColor = System.Drawing.Color.White;
            this.LST_Savegames.FormattingEnabled = true;
            this.LST_Savegames.ItemHeight = 18;
            this.LST_Savegames.Location = new System.Drawing.Point(254, 12);
            this.LST_Savegames.Name = "LST_Savegames";
            this.LST_Savegames.Size = new System.Drawing.Size(234, 414);
            this.LST_Savegames.TabIndex = 31;
            this.LST_Savegames.SelectedIndexChanged += new System.EventHandler(this.LST_Savegames_SelectedIndexChanged);
            // 
            // TB_Backup
            // 
            this.TB_Backup.AutoSize = true;
            this.TB_Backup.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Backup.Location = new System.Drawing.Point(35, 68);
            this.TB_Backup.Name = "TB_Backup";
            this.TB_Backup.Size = new System.Drawing.Size(186, 23);
            this.TB_Backup.TabIndex = 33;
            this.TB_Backup.Text = "Backup while swapping";
            this.TB_Backup.UseVisualStyleBackColor = true;
            // 
            // TXT_SaveInfo
            // 
            this.TXT_SaveInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TXT_SaveInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXT_SaveInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TXT_SaveInfo.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_SaveInfo.ForeColor = System.Drawing.Color.White;
            this.TXT_SaveInfo.Location = new System.Drawing.Point(491, 1);
            this.TXT_SaveInfo.Margin = new System.Windows.Forms.Padding(0);
            this.TXT_SaveInfo.Name = "TXT_SaveInfo";
            this.TXT_SaveInfo.ReadOnly = true;
            this.TXT_SaveInfo.Size = new System.Drawing.Size(500, 425);
            this.TXT_SaveInfo.TabIndex = 34;
            this.TXT_SaveInfo.TabStop = false;
            this.TXT_SaveInfo.Text = resources.GetString("TXT_SaveInfo.Text");
            // 
            // BTN_SwapSave
            // 
            this.BTN_SwapSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BTN_SwapSave.FlatAppearance.BorderSize = 0;
            this.BTN_SwapSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_SwapSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_SwapSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_SwapSave.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_SwapSave.ForeColor = System.Drawing.Color.White;
            this.BTN_SwapSave.Location = new System.Drawing.Point(12, 12);
            this.BTN_SwapSave.Name = "BTN_SwapSave";
            this.BTN_SwapSave.Size = new System.Drawing.Size(236, 50);
            this.BTN_SwapSave.TabIndex = 27;
            this.BTN_SwapSave.TabStop = false;
            this.BTN_SwapSave.Text = "Swap Saves";
            this.BTN_SwapSave.UseVisualStyleBackColor = false;
            this.BTN_SwapSave.Click += new System.EventHandler(this.BTN_SwapSave_Click);
            // 
            // BTN_Backup
            // 
            this.BTN_Backup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BTN_Backup.FlatAppearance.BorderSize = 0;
            this.BTN_Backup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_Backup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_Backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Backup.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_Backup.ForeColor = System.Drawing.Color.White;
            this.BTN_Backup.Location = new System.Drawing.Point(12, 97);
            this.BTN_Backup.Name = "BTN_Backup";
            this.BTN_Backup.Size = new System.Drawing.Size(236, 50);
            this.BTN_Backup.TabIndex = 35;
            this.BTN_Backup.TabStop = false;
            this.BTN_Backup.Text = "Backup Save";
            this.BTN_Backup.UseVisualStyleBackColor = false;
            this.BTN_Backup.Click += new System.EventHandler(this.BTN_Backup_Click);
            // 
            // LBL_Account
            // 
            this.LBL_Account.AutoSize = true;
            this.LBL_Account.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Account.Location = new System.Drawing.Point(12, 278);
            this.LBL_Account.Name = "LBL_Account";
            this.LBL_Account.Size = new System.Drawing.Size(116, 19);
            this.LBL_Account.TabIndex = 36;
            this.LBL_Account.Text = "Choose Account";
            // 
            // GamertagResolve
            // 
            this.GamertagResolve.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GamertagResolve_DoWork);
            // 
            // HiddenRadio
            // 
            this.HiddenRadio.AutoSize = true;
            this.HiddenRadio.Checked = true;
            this.HiddenRadio.Font = new System.Drawing.Font("Open Sans", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HiddenRadio.Location = new System.Drawing.Point(731, 319);
            this.HiddenRadio.Name = "HiddenRadio";
            this.HiddenRadio.Size = new System.Drawing.Size(21, 13);
            this.HiddenRadio.TabIndex = 37;
            this.HiddenRadio.TabStop = true;
            this.HiddenRadio.Text = " ";
            this.HiddenRadio.UseVisualStyleBackColor = true;
            // 
            // GamebarAttach
            // 
            this.GamebarAttach.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GamebarAttach_DoWork);
            this.GamebarAttach.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GamebarAttach_RunWorkerCompleted);
            // 
            // BTN_ACCRefresh
            // 
            this.BTN_ACCRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BTN_ACCRefresh.FlatAppearance.BorderSize = 0;
            this.BTN_ACCRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_ACCRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_ACCRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_ACCRefresh.Font = new System.Drawing.Font("Open Sans", 8F);
            this.BTN_ACCRefresh.ForeColor = System.Drawing.Color.White;
            this.BTN_ACCRefresh.Location = new System.Drawing.Point(130, 267);
            this.BTN_ACCRefresh.Name = "BTN_ACCRefresh";
            this.BTN_ACCRefresh.Size = new System.Drawing.Size(118, 30);
            this.BTN_ACCRefresh.TabIndex = 38;
            this.BTN_ACCRefresh.TabStop = false;
            this.BTN_ACCRefresh.Text = "Refresh Accounts";
            this.BTN_ACCRefresh.UseVisualStyleBackColor = false;
            this.BTN_ACCRefresh.Click += new System.EventHandler(this.BTN_ACCRefresh_Click);
            // 
            // BTN_Help
            // 
            this.BTN_Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BTN_Help.FlatAppearance.BorderSize = 0;
            this.BTN_Help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_Help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Help.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_Help.ForeColor = System.Drawing.Color.White;
            this.BTN_Help.Location = new System.Drawing.Point(12, 211);
            this.BTN_Help.Name = "BTN_Help";
            this.BTN_Help.Size = new System.Drawing.Size(236, 50);
            this.BTN_Help.TabIndex = 39;
            this.BTN_Help.TabStop = false;
            this.BTN_Help.Text = "Help/Use Guide";
            this.BTN_Help.UseVisualStyleBackColor = false;
            this.BTN_Help.Click += new System.EventHandler(this.BTN_Help_Click);
            // 
            // Saveswapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 445);
            this.Controls.Add(this.BTN_Help);
            this.Controls.Add(this.BTN_ACCRefresh);
            this.Controls.Add(this.LBL_Account);
            this.Controls.Add(this.BTN_Backup);
            this.Controls.Add(this.TXT_SaveInfo);
            this.Controls.Add(this.TB_Backup);
            this.Controls.Add(this.LST_Savegames);
            this.Controls.Add(this.LST_Accounts);
            this.Controls.Add(this.BTN_SwapSave);
            this.Controls.Add(this.HiddenRadio);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Saveswapper";
            this.Text = "Saveswapper";
            this.Load += new System.EventHandler(this.Saveswapper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox LST_Accounts;
        private System.Windows.Forms.ListBox LST_Savegames;
        private System.Windows.Forms.CheckBox TB_Backup;
        private System.Windows.Forms.RichTextBox TXT_SaveInfo;
        private System.Windows.Forms.Button BTN_SwapSave;
        private System.Windows.Forms.Button BTN_Backup;
        private System.Windows.Forms.Label LBL_Account;
        private System.ComponentModel.BackgroundWorker GamertagResolve;
        private System.Windows.Forms.RadioButton HiddenRadio;
        private System.ComponentModel.BackgroundWorker GamebarAttach;
        private System.Windows.Forms.Button BTN_ACCRefresh;
        private System.Windows.Forms.Button BTN_Help;
    }
}