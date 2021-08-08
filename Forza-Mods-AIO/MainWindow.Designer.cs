
namespace Forza_Mods_AIO
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BTN_MIN = new System.Windows.Forms.Button();
            this.LBL_Title = new System.Windows.Forms.Label();
            this.BTN_TabInfo = new System.Windows.Forms.Button();
            this.BTN_TabAddCars = new System.Windows.Forms.Button();
            this.BTN_TabLiveTuning = new System.Windows.Forms.Button();
            this.BTN_TabSaveswap = new System.Windows.Forms.Button();
            this.BTN_TabStatsEditor = new System.Windows.Forms.Button();
            this.BTN_TabSpeedhack = new System.Windows.Forms.Button();
            this.BTN_Close = new System.Windows.Forms.Button();
            this.Panel_Info = new System.Windows.Forms.Panel();
            this.Panel_StatsEditor = new System.Windows.Forms.Panel();
            this.Panel_Speedhack = new System.Windows.Forms.Panel();
            this.Panel_AddCars = new System.Windows.Forms.Panel();
            this.Panel_Saveswap = new System.Windows.Forms.Panel();
            this.Panel_LiveTuning = new System.Windows.Forms.Panel();
            this.TabHolder = new System.Windows.Forms.Panel();
            this.CheckAttachedworker = new System.ComponentModel.BackgroundWorker();
            this.InitialBGworker = new System.ComponentModel.BackgroundWorker();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Black;
            this.TopPanel.Controls.Add(this.BTN_MIN);
            this.TopPanel.Controls.Add(this.LBL_Title);
            this.TopPanel.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.TopPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1000, 35);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // BTN_MIN
            // 
            this.BTN_MIN.BackColor = System.Drawing.Color.Black;
            this.BTN_MIN.FlatAppearance.BorderSize = 0;
            this.BTN_MIN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BTN_MIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MIN.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold);
            this.BTN_MIN.ForeColor = System.Drawing.Color.Coral;
            this.BTN_MIN.Location = new System.Drawing.Point(930, 0);
            this.BTN_MIN.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_MIN.Name = "BTN_MIN";
            this.BTN_MIN.Size = new System.Drawing.Size(35, 35);
            this.BTN_MIN.TabIndex = 16;
            this.BTN_MIN.TabStop = false;
            this.BTN_MIN.Text = "___";
            this.BTN_MIN.UseVisualStyleBackColor = false;
            this.BTN_MIN.Click += new System.EventHandler(this.BTN_MIN_Click);
            // 
            // LBL_Title
            // 
            this.LBL_Title.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_Title.AutoSize = true;
            this.LBL_Title.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Title.Font = new System.Drawing.Font("Open Sans", 16F);
            this.LBL_Title.ForeColor = System.Drawing.Color.White;
            this.LBL_Title.Location = new System.Drawing.Point(389, 0);
            this.LBL_Title.Margin = new System.Windows.Forms.Padding(0);
            this.LBL_Title.Name = "LBL_Title";
            this.LBL_Title.Size = new System.Drawing.Size(223, 30);
            this.LBL_Title.TabIndex = 8;
            this.LBL_Title.Text = "Forza Mods AIO Tool";
            this.LBL_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.LBL_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.LBL_Title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // BTN_TabInfo
            // 
            this.BTN_TabInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BTN_TabInfo.FlatAppearance.BorderSize = 0;
            this.BTN_TabInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabInfo.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabInfo.ForeColor = System.Drawing.Color.White;
            this.BTN_TabInfo.Location = new System.Drawing.Point(0, 35);
            this.BTN_TabInfo.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabInfo.Name = "BTN_TabInfo";
            this.BTN_TabInfo.Size = new System.Drawing.Size(172, 30);
            this.BTN_TabInfo.TabIndex = 1;
            this.BTN_TabInfo.TabStop = false;
            this.BTN_TabInfo.Text = "Tool Information";
            this.BTN_TabInfo.UseVisualStyleBackColor = false;
            this.BTN_TabInfo.Click += new System.EventHandler(this.BTN_TabInfo_Click);
            this.BTN_TabInfo.MouseEnter += new System.EventHandler(this.BTN_TabInfo_MouseEnter);
            this.BTN_TabInfo.MouseLeave += new System.EventHandler(this.BTN_TabInfo_MouseLeave);
            // 
            // BTN_TabAddCars
            // 
            this.BTN_TabAddCars.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BTN_TabAddCars.Enabled = false;
            this.BTN_TabAddCars.FlatAppearance.BorderSize = 0;
            this.BTN_TabAddCars.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabAddCars.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabAddCars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabAddCars.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabAddCars.ForeColor = System.Drawing.Color.White;
            this.BTN_TabAddCars.Location = new System.Drawing.Point(172, 35);
            this.BTN_TabAddCars.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabAddCars.Name = "BTN_TabAddCars";
            this.BTN_TabAddCars.Size = new System.Drawing.Size(166, 30);
            this.BTN_TabAddCars.TabIndex = 2;
            this.BTN_TabAddCars.TabStop = false;
            this.BTN_TabAddCars.Text = "Add Cars";
            this.BTN_TabAddCars.UseVisualStyleBackColor = false;
            this.BTN_TabAddCars.Click += new System.EventHandler(this.BTN_TabAddCars_Click);
            this.BTN_TabAddCars.MouseEnter += new System.EventHandler(this.BTN_TabAddCars_MouseEnter);
            this.BTN_TabAddCars.MouseLeave += new System.EventHandler(this.BTN_TabAddCars_MouseLeave);
            // 
            // BTN_TabLiveTuning
            // 
            this.BTN_TabLiveTuning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BTN_TabLiveTuning.Enabled = false;
            this.BTN_TabLiveTuning.FlatAppearance.BorderSize = 0;
            this.BTN_TabLiveTuning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabLiveTuning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabLiveTuning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabLiveTuning.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabLiveTuning.ForeColor = System.Drawing.Color.White;
            this.BTN_TabLiveTuning.Location = new System.Drawing.Point(670, 35);
            this.BTN_TabLiveTuning.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabLiveTuning.Name = "BTN_TabLiveTuning";
            this.BTN_TabLiveTuning.Size = new System.Drawing.Size(166, 30);
            this.BTN_TabLiveTuning.TabIndex = 3;
            this.BTN_TabLiveTuning.TabStop = false;
            this.BTN_TabLiveTuning.Text = "Live Tuning";
            this.BTN_TabLiveTuning.UseVisualStyleBackColor = false;
            this.BTN_TabLiveTuning.Click += new System.EventHandler(this.BTN_TabLiveTuning_Click);
            this.BTN_TabLiveTuning.MouseEnter += new System.EventHandler(this.BTN_TabLiveTuning_MouseEnter);
            this.BTN_TabLiveTuning.MouseLeave += new System.EventHandler(this.BTN_TabLiveTuning_MouseLeave);
            // 
            // BTN_TabSaveswap
            // 
            this.BTN_TabSaveswap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BTN_TabSaveswap.FlatAppearance.BorderSize = 0;
            this.BTN_TabSaveswap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabSaveswap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabSaveswap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabSaveswap.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabSaveswap.ForeColor = System.Drawing.Color.White;
            this.BTN_TabSaveswap.Location = new System.Drawing.Point(504, 35);
            this.BTN_TabSaveswap.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabSaveswap.Name = "BTN_TabSaveswap";
            this.BTN_TabSaveswap.Size = new System.Drawing.Size(166, 30);
            this.BTN_TabSaveswap.TabIndex = 4;
            this.BTN_TabSaveswap.TabStop = false;
            this.BTN_TabSaveswap.Text = "Saveswapper";
            this.BTN_TabSaveswap.UseVisualStyleBackColor = false;
            this.BTN_TabSaveswap.Click += new System.EventHandler(this.BTN_TabSaveswap_Click);
            this.BTN_TabSaveswap.MouseEnter += new System.EventHandler(this.BTN_TabSaveswap_MouseEnter);
            this.BTN_TabSaveswap.MouseLeave += new System.EventHandler(this.BTN_TabSaveswap_MouseLeave);
            // 
            // BTN_TabStatsEditor
            // 
            this.BTN_TabStatsEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BTN_TabStatsEditor.Enabled = false;
            this.BTN_TabStatsEditor.FlatAppearance.BorderSize = 0;
            this.BTN_TabStatsEditor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabStatsEditor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabStatsEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabStatsEditor.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabStatsEditor.ForeColor = System.Drawing.Color.White;
            this.BTN_TabStatsEditor.Location = new System.Drawing.Point(338, 35);
            this.BTN_TabStatsEditor.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabStatsEditor.Name = "BTN_TabStatsEditor";
            this.BTN_TabStatsEditor.Size = new System.Drawing.Size(166, 30);
            this.BTN_TabStatsEditor.TabIndex = 5;
            this.BTN_TabStatsEditor.TabStop = false;
            this.BTN_TabStatsEditor.Text = "Stats Editor";
            this.BTN_TabStatsEditor.UseVisualStyleBackColor = false;
            this.BTN_TabStatsEditor.Click += new System.EventHandler(this.BTN_TabStatsEditor_Click);
            this.BTN_TabStatsEditor.MouseEnter += new System.EventHandler(this.BTN_TabStatsEditor_MouseEnter);
            this.BTN_TabStatsEditor.MouseLeave += new System.EventHandler(this.BTN_TabStatsEditor_MouseLeave);
            // 
            // BTN_TabSpeedhack
            // 
            this.BTN_TabSpeedhack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BTN_TabSpeedhack.Enabled = false;
            this.BTN_TabSpeedhack.FlatAppearance.BorderSize = 0;
            this.BTN_TabSpeedhack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_TabSpeedhack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_TabSpeedhack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_TabSpeedhack.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_TabSpeedhack.ForeColor = System.Drawing.Color.White;
            this.BTN_TabSpeedhack.Location = new System.Drawing.Point(836, 35);
            this.BTN_TabSpeedhack.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_TabSpeedhack.Name = "BTN_TabSpeedhack";
            this.BTN_TabSpeedhack.Size = new System.Drawing.Size(164, 30);
            this.BTN_TabSpeedhack.TabIndex = 6;
            this.BTN_TabSpeedhack.TabStop = false;
            this.BTN_TabSpeedhack.Text = "Cool Shit";
            this.BTN_TabSpeedhack.UseVisualStyleBackColor = false;
            this.BTN_TabSpeedhack.Click += new System.EventHandler(this.BTN_TabSpeedhack_Click);
            this.BTN_TabSpeedhack.MouseEnter += new System.EventHandler(this.BTN_TabSpeedhack_MouseEnter);
            this.BTN_TabSpeedhack.MouseLeave += new System.EventHandler(this.BTN_TabSpeedhack_MouseLeave);
            // 
            // BTN_Close
            // 
            this.BTN_Close.BackColor = System.Drawing.Color.Black;
            this.BTN_Close.FlatAppearance.BorderSize = 0;
            this.BTN_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BTN_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Close.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.ForeColor = System.Drawing.Color.Red;
            this.BTN_Close.Location = new System.Drawing.Point(965, 0);
            this.BTN_Close.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(35, 35);
            this.BTN_Close.TabIndex = 7;
            this.BTN_Close.TabStop = false;
            this.BTN_Close.Text = "X";
            this.BTN_Close.UseVisualStyleBackColor = false;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // Panel_Info
            // 
            this.Panel_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.Panel_Info.Location = new System.Drawing.Point(0, 65);
            this.Panel_Info.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Info.Name = "Panel_Info";
            this.Panel_Info.Size = new System.Drawing.Size(172, 5);
            this.Panel_Info.TabIndex = 9;
            // 
            // Panel_StatsEditor
            // 
            this.Panel_StatsEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Panel_StatsEditor.Location = new System.Drawing.Point(338, 65);
            this.Panel_StatsEditor.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_StatsEditor.Name = "Panel_StatsEditor";
            this.Panel_StatsEditor.Size = new System.Drawing.Size(166, 5);
            this.Panel_StatsEditor.TabIndex = 10;
            // 
            // Panel_Speedhack
            // 
            this.Panel_Speedhack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Panel_Speedhack.Location = new System.Drawing.Point(834, 65);
            this.Panel_Speedhack.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Speedhack.Name = "Panel_Speedhack";
            this.Panel_Speedhack.Size = new System.Drawing.Size(166, 5);
            this.Panel_Speedhack.TabIndex = 11;
            // 
            // Panel_AddCars
            // 
            this.Panel_AddCars.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Panel_AddCars.Location = new System.Drawing.Point(172, 65);
            this.Panel_AddCars.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_AddCars.Name = "Panel_AddCars";
            this.Panel_AddCars.Size = new System.Drawing.Size(166, 5);
            this.Panel_AddCars.TabIndex = 11;
            // 
            // Panel_Saveswap
            // 
            this.Panel_Saveswap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Panel_Saveswap.Location = new System.Drawing.Point(504, 65);
            this.Panel_Saveswap.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Saveswap.Name = "Panel_Saveswap";
            this.Panel_Saveswap.Size = new System.Drawing.Size(166, 5);
            this.Panel_Saveswap.TabIndex = 11;
            // 
            // Panel_LiveTuning
            // 
            this.Panel_LiveTuning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Panel_LiveTuning.Location = new System.Drawing.Point(670, 65);
            this.Panel_LiveTuning.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_LiveTuning.Name = "Panel_LiveTuning";
            this.Panel_LiveTuning.Size = new System.Drawing.Size(166, 5);
            this.Panel_LiveTuning.TabIndex = 11;
            // 
            // TabHolder
            // 
            this.TabHolder.Location = new System.Drawing.Point(0, 70);
            this.TabHolder.Margin = new System.Windows.Forms.Padding(0);
            this.TabHolder.Name = "TabHolder";
            this.TabHolder.Size = new System.Drawing.Size(1000, 445);
            this.TabHolder.TabIndex = 15;
            // 
            // InitialBGworker
            // 
            this.InitialBGworker.WorkerReportsProgress = true;
            this.InitialBGworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InitialBGworker_DoWork);
            this.InitialBGworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.InitialBGworker_ProgressChanged);
            this.InitialBGworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InitialBGworker_RunWorkerCompleted);
            // 
            // MainWindow
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 515);
            this.ControlBox = false;
            this.Controls.Add(this.TabHolder);
            this.Controls.Add(this.Panel_Saveswap);
            this.Controls.Add(this.Panel_AddCars);
            this.Controls.Add(this.Panel_LiveTuning);
            this.Controls.Add(this.Panel_Speedhack);
            this.Controls.Add(this.Panel_StatsEditor);
            this.Controls.Add(this.Panel_Info);
            this.Controls.Add(this.BTN_Close);
            this.Controls.Add(this.BTN_TabSpeedhack);
            this.Controls.Add(this.BTN_TabStatsEditor);
            this.Controls.Add(this.BTN_TabSaveswap);
            this.Controls.Add(this.BTN_TabLiveTuning);
            this.Controls.Add(this.BTN_TabAddCars);
            this.Controls.Add(this.BTN_TabInfo);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Forza Mods AIO";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button BTN_TabInfo;
        public System.Windows.Forms.Button BTN_TabAddCars;
        public System.Windows.Forms.Button BTN_TabLiveTuning;
        public System.Windows.Forms.Button BTN_TabSaveswap;
        public System.Windows.Forms.Button BTN_TabStatsEditor;
        public System.Windows.Forms.Button BTN_TabSpeedhack;
        public System.Windows.Forms.Button BTN_Close;
        public System.Windows.Forms.Label LBL_Title;
        public System.Windows.Forms.Panel Panel_Info;
        public System.Windows.Forms.Panel Panel_StatsEditor;
        public System.Windows.Forms.Panel Panel_Speedhack;
        public System.Windows.Forms.Panel Panel_AddCars;
        public System.Windows.Forms.Panel Panel_Saveswap;
        public System.Windows.Forms.Panel Panel_LiveTuning;
        public System.Windows.Forms.Button BTN_MIN;
        public System.Windows.Forms.Panel TabHolder;
        public System.ComponentModel.BackgroundWorker CheckAttachedworker;
        public System.ComponentModel.BackgroundWorker InitialBGworker;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}

