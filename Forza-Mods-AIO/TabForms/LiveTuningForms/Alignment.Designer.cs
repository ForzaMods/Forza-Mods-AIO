
namespace Forza_Mods_AIO.TabForms.LiveTuningForms
{
    partial class Alignment
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
            this.panel_Camber = new System.Windows.Forms.Panel();
            this.FrontCamberBar = new LimitlessUI.Slider_WOC();
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.LBL_RearCamber = new System.Windows.Forms.Label();
            this.LBL_FrontCamber = new System.Windows.Forms.Label();
            this.LBL_Camber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LBL_RearToe = new System.Windows.Forms.Label();
            this.LBL_FrontToe = new System.Windows.Forms.Label();
            this.LBL_Toe = new System.Windows.Forms.Label();
            this.RearCamberBar = new LimitlessUI.Slider_WOC();
            this.FrontToeBar = new LimitlessUI.Slider_WOC();
            this.RearToeBar = new LimitlessUI.Slider_WOC();
            this.FrontCamberNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.RearCamberNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.FrontToeNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.RearToeNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.panel_Camber.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Camber
            // 
            this.panel_Camber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.panel_Camber.Controls.Add(this.RearCamberNUD);
            this.panel_Camber.Controls.Add(this.FrontCamberNUD);
            this.panel_Camber.Controls.Add(this.RearCamberBar);
            this.panel_Camber.Controls.Add(this.FrontCamberBar);
            this.panel_Camber.Controls.Add(this.BTN_Refresh);
            this.panel_Camber.Controls.Add(this.LBL_RearCamber);
            this.panel_Camber.Controls.Add(this.LBL_FrontCamber);
            this.panel_Camber.Controls.Add(this.LBL_Camber);
            this.panel_Camber.Location = new System.Drawing.Point(12, 12);
            this.panel_Camber.Name = "panel_Camber";
            this.panel_Camber.Size = new System.Drawing.Size(976, 160);
            this.panel_Camber.TabIndex = 7;
            // 
            // FrontCamberBar
            // 
            this.FrontCamberBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.FrontCamberBar.BackLineThikness = 3F;
            this.FrontCamberBar.CircleSize = 10F;
            this.FrontCamberBar.DrawCircle = true;
            this.FrontCamberBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.FrontCamberBar.FrontLineThikness = 3F;
            this.FrontCamberBar.Location = new System.Drawing.Point(9, 68);
            this.FrontCamberBar.MaxValue = 150F;
            this.FrontCamberBar.MinValue = -150F;
            this.FrontCamberBar.Name = "FrontCamberBar";
            this.FrontCamberBar.Rounded = false;
            this.FrontCamberBar.Size = new System.Drawing.Size(820, 23);
            this.FrontCamberBar.TabIndex = 42;
            this.FrontCamberBar.Text = "VelMultBar";
            this.FrontCamberBar.Value = 0F;
            this.FrontCamberBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.FrontCamberBar_Scroll);
            // 
            // BTN_Refresh
            // 
            this.BTN_Refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BTN_Refresh.FlatAppearance.BorderSize = 0;
            this.BTN_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Refresh.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Refresh.Location = new System.Drawing.Point(802, 10);
            this.BTN_Refresh.Name = "BTN_Refresh";
            this.BTN_Refresh.Size = new System.Drawing.Size(159, 34);
            this.BTN_Refresh.TabIndex = 38;
            this.BTN_Refresh.Text = "Refresh";
            this.BTN_Refresh.UseVisualStyleBackColor = false;
            this.BTN_Refresh.Click += new System.EventHandler(this.BTN_Refresh_Click);
            // 
            // LBL_RearCamber
            // 
            this.LBL_RearCamber.AutoSize = true;
            this.LBL_RearCamber.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_RearCamber.Location = new System.Drawing.Point(452, 94);
            this.LBL_RearCamber.Name = "LBL_RearCamber";
            this.LBL_RearCamber.Size = new System.Drawing.Size(54, 27);
            this.LBL_RearCamber.TabIndex = 1;
            this.LBL_RearCamber.Text = "Rear";
            // 
            // LBL_FrontCamber
            // 
            this.LBL_FrontCamber.AutoSize = true;
            this.LBL_FrontCamber.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_FrontCamber.Location = new System.Drawing.Point(449, 37);
            this.LBL_FrontCamber.Name = "LBL_FrontCamber";
            this.LBL_FrontCamber.Size = new System.Drawing.Size(61, 27);
            this.LBL_FrontCamber.TabIndex = 1;
            this.LBL_FrontCamber.Text = "Front";
            // 
            // LBL_Camber
            // 
            this.LBL_Camber.AutoSize = true;
            this.LBL_Camber.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Camber.Location = new System.Drawing.Point(435, 10);
            this.LBL_Camber.Name = "LBL_Camber";
            this.LBL_Camber.Size = new System.Drawing.Size(86, 27);
            this.LBL_Camber.TabIndex = 1;
            this.LBL_Camber.Text = "Camber";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.RearToeNUD);
            this.panel1.Controls.Add(this.FrontToeNUD);
            this.panel1.Controls.Add(this.RearToeBar);
            this.panel1.Controls.Add(this.FrontToeBar);
            this.panel1.Controls.Add(this.LBL_RearToe);
            this.panel1.Controls.Add(this.LBL_FrontToe);
            this.panel1.Controls.Add(this.LBL_Toe);
            this.panel1.Location = new System.Drawing.Point(12, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 160);
            this.panel1.TabIndex = 7;
            // 
            // LBL_RearToe
            // 
            this.LBL_RearToe.AutoSize = true;
            this.LBL_RearToe.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_RearToe.Location = new System.Drawing.Point(452, 94);
            this.LBL_RearToe.Name = "LBL_RearToe";
            this.LBL_RearToe.Size = new System.Drawing.Size(54, 27);
            this.LBL_RearToe.TabIndex = 1;
            this.LBL_RearToe.Text = "Rear";
            // 
            // LBL_FrontToe
            // 
            this.LBL_FrontToe.AutoSize = true;
            this.LBL_FrontToe.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_FrontToe.Location = new System.Drawing.Point(449, 37);
            this.LBL_FrontToe.Name = "LBL_FrontToe";
            this.LBL_FrontToe.Size = new System.Drawing.Size(61, 27);
            this.LBL_FrontToe.TabIndex = 1;
            this.LBL_FrontToe.Text = "Front";
            // 
            // LBL_Toe
            // 
            this.LBL_Toe.AutoSize = true;
            this.LBL_Toe.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Toe.Location = new System.Drawing.Point(452, 10);
            this.LBL_Toe.Name = "LBL_Toe";
            this.LBL_Toe.Size = new System.Drawing.Size(46, 27);
            this.LBL_Toe.TabIndex = 1;
            this.LBL_Toe.Text = "Toe";
            // 
            // RearCamberBar
            // 
            this.RearCamberBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RearCamberBar.BackLineThikness = 3F;
            this.RearCamberBar.CircleSize = 10F;
            this.RearCamberBar.DrawCircle = true;
            this.RearCamberBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.RearCamberBar.FrontLineThikness = 3F;
            this.RearCamberBar.Location = new System.Drawing.Point(9, 119);
            this.RearCamberBar.MaxValue = 150F;
            this.RearCamberBar.MinValue = -150F;
            this.RearCamberBar.Name = "RearCamberBar";
            this.RearCamberBar.Rounded = false;
            this.RearCamberBar.Size = new System.Drawing.Size(820, 23);
            this.RearCamberBar.TabIndex = 42;
            this.RearCamberBar.Text = "VelMultBar";
            this.RearCamberBar.Value = 0F;
            this.RearCamberBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.RearCamberBar_Scroll);
            // 
            // FrontToeBar
            // 
            this.FrontToeBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.FrontToeBar.BackLineThikness = 3F;
            this.FrontToeBar.CircleSize = 10F;
            this.FrontToeBar.DrawCircle = true;
            this.FrontToeBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.FrontToeBar.FrontLineThikness = 3F;
            this.FrontToeBar.Location = new System.Drawing.Point(9, 68);
            this.FrontToeBar.MaxValue = 100F;
            this.FrontToeBar.MinValue = -100F;
            this.FrontToeBar.Name = "FrontToeBar";
            this.FrontToeBar.Rounded = false;
            this.FrontToeBar.Size = new System.Drawing.Size(820, 23);
            this.FrontToeBar.TabIndex = 42;
            this.FrontToeBar.Text = "VelMultBar";
            this.FrontToeBar.Value = 0F;
            this.FrontToeBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.FrontToeBar_Scroll);
            // 
            // RearToeBar
            // 
            this.RearToeBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RearToeBar.BackLineThikness = 3F;
            this.RearToeBar.CircleSize = 10F;
            this.RearToeBar.DrawCircle = true;
            this.RearToeBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.RearToeBar.FrontLineThikness = 3F;
            this.RearToeBar.Location = new System.Drawing.Point(9, 119);
            this.RearToeBar.MaxValue = 100F;
            this.RearToeBar.MinValue = -100F;
            this.RearToeBar.Name = "RearToeBar";
            this.RearToeBar.Rounded = false;
            this.RearToeBar.Size = new System.Drawing.Size(820, 23);
            this.RearToeBar.TabIndex = 42;
            this.RearToeBar.Text = "VelMultBar";
            this.RearToeBar.Value = 0F;
            this.RearToeBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.RearToeBar_Scroll);
            // 
            // FrontCamberNUD
            // 
            this.FrontCamberNUD.DecimalPlaces = 1;
            this.FrontCamberNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FrontCamberNUD.Location = new System.Drawing.Point(841, 68);
            this.FrontCamberNUD.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.FrontCamberNUD.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.FrontCamberNUD.Name = "FrontCamberNUD";
            this.FrontCamberNUD.Size = new System.Drawing.Size(120, 22);
            this.FrontCamberNUD.TabIndex = 43;
            this.FrontCamberNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FrontCamberNUD.ValueChanged += new System.EventHandler(this.FrontCamberNUD_ValueChanged);
            // 
            // RearCamberNUD
            // 
            this.RearCamberNUD.DecimalPlaces = 1;
            this.RearCamberNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RearCamberNUD.Location = new System.Drawing.Point(841, 119);
            this.RearCamberNUD.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.RearCamberNUD.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.RearCamberNUD.Name = "RearCamberNUD";
            this.RearCamberNUD.Size = new System.Drawing.Size(120, 22);
            this.RearCamberNUD.TabIndex = 43;
            this.RearCamberNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RearCamberNUD.ValueChanged += new System.EventHandler(this.RearCamberNUD_ValueChanged);
            // 
            // FrontToeNUD
            // 
            this.FrontToeNUD.DecimalPlaces = 1;
            this.FrontToeNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FrontToeNUD.Location = new System.Drawing.Point(841, 68);
            this.FrontToeNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FrontToeNUD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.FrontToeNUD.Name = "FrontToeNUD";
            this.FrontToeNUD.Size = new System.Drawing.Size(120, 22);
            this.FrontToeNUD.TabIndex = 43;
            this.FrontToeNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FrontToeNUD.ValueChanged += new System.EventHandler(this.FrontToeNUD_ValueChanged);
            // 
            // RearToeNUD
            // 
            this.RearToeNUD.DecimalPlaces = 1;
            this.RearToeNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RearToeNUD.Location = new System.Drawing.Point(841, 119);
            this.RearToeNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RearToeNUD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.RearToeNUD.Name = "RearToeNUD";
            this.RearToeNUD.Size = new System.Drawing.Size(120, 22);
            this.RearToeNUD.TabIndex = 43;
            this.RearToeNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RearToeNUD.ValueChanged += new System.EventHandler(this.RearToeNUD_ValueChanged);
            // 
            // Alignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 410);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Camber);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Alignment";
            this.Text = "LiveTuning";
            this.panel_Camber.ResumeLayout(false);
            this.panel_Camber.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Camber;
        private System.Windows.Forms.Label LBL_Camber;
        private System.Windows.Forms.Label LBL_RearCamber;
        private System.Windows.Forms.Label LBL_FrontCamber;
        public System.Windows.Forms.Button BTN_Refresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LBL_RearToe;
        private System.Windows.Forms.Label LBL_FrontToe;
        private System.Windows.Forms.Label LBL_Toe;
        public LimitlessUI.Slider_WOC FrontCamberBar;
        public LimitlessUI.Slider_WOC RearCamberBar;
        public LimitlessUI.Slider_WOC FrontToeBar;
        public LimitlessUI.Slider_WOC RearToeBar;
        private DarkUI.Controls.DarkNumericUpDown FrontCamberNUD;
        private DarkUI.Controls.DarkNumericUpDown RearCamberNUD;
        private DarkUI.Controls.DarkNumericUpDown RearToeNUD;
        private DarkUI.Controls.DarkNumericUpDown FrontToeNUD;
    }
}