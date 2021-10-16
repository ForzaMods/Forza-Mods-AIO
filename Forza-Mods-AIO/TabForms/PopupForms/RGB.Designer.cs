
namespace Forza_Mods_AIO.TabForms.PopupForms
{
    partial class RGB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RGB));
            this.LBL_Title = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BTN_Close = new System.Windows.Forms.Button();
            this.RedLBL = new System.Windows.Forms.Label();
            this.GreenLBL = new System.Windows.Forms.Label();
            this.BlueLBL = new System.Windows.Forms.Label();
            this.RedBar = new LimitlessUI.Slider_WOC();
            this.BlueBar = new LimitlessUI.Slider_WOC();
            this.GreenBar = new LimitlessUI.Slider_WOC();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SunAids = new Telerik.WinControls.UI.RadCheckBox();
            this.SunAidsWorker = new System.ComponentModel.BackgroundWorker();
            this.AidsSpeed = new DarkUI.Controls.DarkNumericUpDown();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SunAids)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AidsSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // LBL_Title
            // 
            this.LBL_Title.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_Title.AutoSize = true;
            this.LBL_Title.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Title.Font = new System.Drawing.Font("Open Sans", 16F);
            this.LBL_Title.ForeColor = System.Drawing.Color.White;
            this.LBL_Title.Location = new System.Drawing.Point(88, 0);
            this.LBL_Title.Margin = new System.Windows.Forms.Padding(0);
            this.LBL_Title.Name = "LBL_Title";
            this.LBL_Title.Size = new System.Drawing.Size(103, 30);
            this.LBL_Title.TabIndex = 8;
            this.LBL_Title.Text = "Sun RGB";
            this.LBL_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.LBL_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.LBL_Title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Black;
            this.TopPanel.Controls.Add(this.BTN_Close);
            this.TopPanel.Controls.Add(this.LBL_Title);
            this.TopPanel.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.TopPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(279, 35);
            this.TopPanel.TabIndex = 9;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // BTN_Close
            // 
            this.BTN_Close.BackColor = System.Drawing.Color.Black;
            this.BTN_Close.FlatAppearance.BorderSize = 0;
            this.BTN_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BTN_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Close.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.ForeColor = System.Drawing.Color.Red;
            this.BTN_Close.Location = new System.Drawing.Point(244, 0);
            this.BTN_Close.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(35, 35);
            this.BTN_Close.TabIndex = 9;
            this.BTN_Close.TabStop = false;
            this.BTN_Close.Text = "X";
            this.BTN_Close.UseVisualStyleBackColor = false;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // RedLBL
            // 
            this.RedLBL.AutoSize = true;
            this.RedLBL.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedLBL.Location = new System.Drawing.Point(27, 51);
            this.RedLBL.Name = "RedLBL";
            this.RedLBL.Size = new System.Drawing.Size(35, 19);
            this.RedLBL.TabIndex = 23;
            this.RedLBL.Text = "Red";
            // 
            // GreenLBL
            // 
            this.GreenLBL.AutoSize = true;
            this.GreenLBL.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenLBL.Location = new System.Drawing.Point(12, 70);
            this.GreenLBL.Name = "GreenLBL";
            this.GreenLBL.Size = new System.Drawing.Size(50, 19);
            this.GreenLBL.TabIndex = 23;
            this.GreenLBL.Text = "Green";
            // 
            // BlueLBL
            // 
            this.BlueLBL.AutoSize = true;
            this.BlueLBL.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueLBL.Location = new System.Drawing.Point(23, 89);
            this.BlueLBL.Name = "BlueLBL";
            this.BlueLBL.Size = new System.Drawing.Size(39, 19);
            this.BlueLBL.TabIndex = 23;
            this.BlueLBL.Text = "Blue";
            // 
            // RedBar
            // 
            this.RedBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.RedBar.BackLineThikness = 3F;
            this.RedBar.CircleSize = 10F;
            this.RedBar.DrawCircle = true;
            this.RedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.RedBar.FrontLineThikness = 3F;
            this.RedBar.Location = new System.Drawing.Point(68, 51);
            this.RedBar.MaxValue = 1E+10F;
            this.RedBar.MinValue = 0F;
            this.RedBar.Name = "RedBar";
            this.RedBar.Rounded = false;
            this.RedBar.Size = new System.Drawing.Size(182, 19);
            this.RedBar.TabIndex = 37;
            this.RedBar.Text = "RedBar";
            this.RedBar.Value = 3.921569E+09F;
            this.RedBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.RedBar_OnValueChanged);
            // 
            // BlueBar
            // 
            this.BlueBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BlueBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BlueBar.BackLineThikness = 3F;
            this.BlueBar.CircleSize = 10F;
            this.BlueBar.DrawCircle = true;
            this.BlueBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.BlueBar.FrontLineThikness = 3F;
            this.BlueBar.Location = new System.Drawing.Point(68, 89);
            this.BlueBar.MaxValue = 1E+10F;
            this.BlueBar.MinValue = 0F;
            this.BlueBar.Name = "BlueBar";
            this.BlueBar.Rounded = false;
            this.BlueBar.Size = new System.Drawing.Size(182, 19);
            this.BlueBar.TabIndex = 37;
            this.BlueBar.Text = "BlueBar";
            this.BlueBar.Value = 3.921569E+09F;
            this.BlueBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.BlueBar_OnValueChanged);
            // 
            // GreenBar
            // 
            this.GreenBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.GreenBar.BackLineThikness = 3F;
            this.GreenBar.CircleSize = 10F;
            this.GreenBar.DrawCircle = true;
            this.GreenBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.GreenBar.FrontLineThikness = 3F;
            this.GreenBar.Location = new System.Drawing.Point(68, 70);
            this.GreenBar.MaxValue = 1E+10F;
            this.GreenBar.MinValue = 0F;
            this.GreenBar.Name = "GreenBar";
            this.GreenBar.Rounded = false;
            this.GreenBar.Size = new System.Drawing.Size(182, 19);
            this.GreenBar.TabIndex = 37;
            this.GreenBar.Text = "GreenBar";
            this.GreenBar.Value = 3.921569E+09F;
            this.GreenBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.GreenBar_OnValueChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ResetButton.FlatAppearance.BorderSize = 0;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetButton.Location = new System.Drawing.Point(7, 118);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 34);
            this.ResetButton.TabIndex = 38;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SunAids
            // 
            this.SunAids.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.SunAids.Font = new System.Drawing.Font("Open Sans", 10F);
            this.SunAids.Location = new System.Drawing.Point(190, 124);
            this.SunAids.Name = "SunAids";
            this.SunAids.Size = new System.Drawing.Size(92, 20);
            this.SunAids.TabIndex = 39;
            this.SunAids.Text = "Aids mode";
            this.SunAids.ThemeName = "FluentDark";
            this.SunAids.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.SunAids_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.SunAids.GetChildAt(0))).DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.SunAids.GetChildAt(0))).Text = "Aids mode";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).LineLimit = false;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Open Sans", 10F);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).CheckPrimitiveStyle = Telerik.WinControls.Enumerations.CheckPrimitiveStyleEnum.Win8;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).AutoSize = true;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Image = null;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).ForeColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.SunAids.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Enabled = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.SunAids.GetChildAt(0).GetChildAt(2))).BackColor = System.Drawing.Color.Transparent;
            // 
            // SunAidsWorker
            // 
            this.SunAidsWorker.WorkerReportsProgress = true;
            this.SunAidsWorker.WorkerSupportsCancellation = true;
            this.SunAidsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SunAidsWorker_DoWork);
            // 
            // AidsSpeed
            // 
            this.AidsSpeed.Location = new System.Drawing.Point(88, 124);
            this.AidsSpeed.Name = "AidsSpeed";
            this.AidsSpeed.Size = new System.Drawing.Size(96, 22);
            this.AidsSpeed.TabIndex = 40;
            this.AidsSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AidsSpeed.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // RGB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(279, 158);
            this.ControlBox = false;
            this.Controls.Add(this.AidsSpeed);
            this.Controls.Add(this.SunAids);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.BlueBar);
            this.Controls.Add(this.GreenBar);
            this.Controls.Add(this.RedBar);
            this.Controls.Add(this.BlueLBL);
            this.Controls.Add(this.GreenLBL);
            this.Controls.Add(this.RedLBL);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RGB";
            this.Text = "Error";
            this.Load += new System.EventHandler(this.RGB_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SunAids)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AidsSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label LBL_Title;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label RedLBL;
        private System.Windows.Forms.Label GreenLBL;
        private System.Windows.Forms.Label BlueLBL;
        public LimitlessUI.Slider_WOC RedBar;
        public LimitlessUI.Slider_WOC BlueBar;
        public LimitlessUI.Slider_WOC GreenBar;
        private System.Windows.Forms.Button ResetButton;
        public System.Windows.Forms.Button BTN_Close;
        public Telerik.WinControls.UI.RadCheckBox SunAids;
        private System.ComponentModel.BackgroundWorker SunAidsWorker;
        public DarkUI.Controls.DarkNumericUpDown AidsSpeed;
    }
}