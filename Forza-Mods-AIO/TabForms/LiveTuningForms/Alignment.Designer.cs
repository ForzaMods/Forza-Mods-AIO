
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
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.RearCamberNUD = new System.Windows.Forms.NumericUpDown();
            this.RearCamberBar = new System.Windows.Forms.TrackBar();
            this.FrontCamberNUD = new System.Windows.Forms.NumericUpDown();
            this.LBL_RearCamber = new System.Windows.Forms.Label();
            this.FrontCamberBar = new System.Windows.Forms.TrackBar();
            this.LBL_FrontCamber = new System.Windows.Forms.Label();
            this.LBL_Camber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RearToeNUD = new System.Windows.Forms.NumericUpDown();
            this.RearToeBar = new System.Windows.Forms.TrackBar();
            this.FrontToeNUD = new System.Windows.Forms.NumericUpDown();
            this.LBL_RearToe = new System.Windows.Forms.Label();
            this.FrontToeBar = new System.Windows.Forms.TrackBar();
            this.LBL_FrontToe = new System.Windows.Forms.Label();
            this.LBL_Toe = new System.Windows.Forms.Label();
            this.panel_Camber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Camber
            // 
            this.panel_Camber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.panel_Camber.Controls.Add(this.BTN_Refresh);
            this.panel_Camber.Controls.Add(this.RearCamberNUD);
            this.panel_Camber.Controls.Add(this.RearCamberBar);
            this.panel_Camber.Controls.Add(this.FrontCamberNUD);
            this.panel_Camber.Controls.Add(this.LBL_RearCamber);
            this.panel_Camber.Controls.Add(this.FrontCamberBar);
            this.panel_Camber.Controls.Add(this.LBL_FrontCamber);
            this.panel_Camber.Controls.Add(this.LBL_Camber);
            this.panel_Camber.Location = new System.Drawing.Point(12, 12);
            this.panel_Camber.Name = "panel_Camber";
            this.panel_Camber.Size = new System.Drawing.Size(976, 160);
            this.panel_Camber.TabIndex = 7;
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
            this.RearCamberNUD.TabIndex = 3;
            this.RearCamberNUD.ValueChanged += new System.EventHandler(this.RearCamberNUD_ValueChanged);
            // 
            // RearCamberBar
            // 
            this.RearCamberBar.LargeChange = 10;
            this.RearCamberBar.Location = new System.Drawing.Point(3, 119);
            this.RearCamberBar.Maximum = 150;
            this.RearCamberBar.Minimum = -150;
            this.RearCamberBar.Name = "RearCamberBar";
            this.RearCamberBar.Size = new System.Drawing.Size(832, 45);
            this.RearCamberBar.TabIndex = 2;
            this.RearCamberBar.Scroll += new System.EventHandler(this.RearCamberBar_Scroll);
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
            this.FrontCamberNUD.TabIndex = 3;
            this.FrontCamberNUD.ValueChanged += new System.EventHandler(this.FrontCamberNUD_ValueChanged);
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
            // FrontCamberBar
            // 
            this.FrontCamberBar.LargeChange = 10;
            this.FrontCamberBar.Location = new System.Drawing.Point(3, 68);
            this.FrontCamberBar.Maximum = 150;
            this.FrontCamberBar.Minimum = -150;
            this.FrontCamberBar.Name = "FrontCamberBar";
            this.FrontCamberBar.Size = new System.Drawing.Size(832, 45);
            this.FrontCamberBar.TabIndex = 2;
            this.FrontCamberBar.Scroll += new System.EventHandler(this.FrontCamberBar_Scroll);
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
            this.panel1.Controls.Add(this.RearToeBar);
            this.panel1.Controls.Add(this.FrontToeNUD);
            this.panel1.Controls.Add(this.LBL_RearToe);
            this.panel1.Controls.Add(this.FrontToeBar);
            this.panel1.Controls.Add(this.LBL_FrontToe);
            this.panel1.Controls.Add(this.LBL_Toe);
            this.panel1.Location = new System.Drawing.Point(12, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 160);
            this.panel1.TabIndex = 7;
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
            this.RearToeNUD.TabIndex = 3;
            this.RearToeNUD.ValueChanged += new System.EventHandler(this.RearToeNUD_ValueChanged);
            // 
            // RearToeBar
            // 
            this.RearToeBar.LargeChange = 10;
            this.RearToeBar.Location = new System.Drawing.Point(3, 119);
            this.RearToeBar.Maximum = 100;
            this.RearToeBar.Minimum = -100;
            this.RearToeBar.Name = "RearToeBar";
            this.RearToeBar.Size = new System.Drawing.Size(832, 45);
            this.RearToeBar.TabIndex = 2;
            this.RearToeBar.Scroll += new System.EventHandler(this.RearToeBar_Scroll);
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
            this.FrontToeNUD.TabIndex = 3;
            this.FrontToeNUD.ValueChanged += new System.EventHandler(this.FrontToeNUD_ValueChanged);
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
            // FrontToeBar
            // 
            this.FrontToeBar.LargeChange = 10;
            this.FrontToeBar.Location = new System.Drawing.Point(3, 68);
            this.FrontToeBar.Maximum = 100;
            this.FrontToeBar.Minimum = -100;
            this.FrontToeBar.Name = "FrontToeBar";
            this.FrontToeBar.Size = new System.Drawing.Size(832, 45);
            this.FrontToeBar.TabIndex = 2;
            this.FrontToeBar.Scroll += new System.EventHandler(this.FrontToeBar_Scroll);
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
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearCamberBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontCamberBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearToeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontToeBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Camber;
        private System.Windows.Forms.Label LBL_Camber;
        private System.Windows.Forms.NumericUpDown RearCamberNUD;
        private System.Windows.Forms.TrackBar RearCamberBar;
        private System.Windows.Forms.NumericUpDown FrontCamberNUD;
        private System.Windows.Forms.Label LBL_RearCamber;
        private System.Windows.Forms.TrackBar FrontCamberBar;
        private System.Windows.Forms.Label LBL_FrontCamber;
        public System.Windows.Forms.Button BTN_Refresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown RearToeNUD;
        private System.Windows.Forms.TrackBar RearToeBar;
        private System.Windows.Forms.NumericUpDown FrontToeNUD;
        private System.Windows.Forms.Label LBL_RearToe;
        private System.Windows.Forms.TrackBar FrontToeBar;
        private System.Windows.Forms.Label LBL_FrontToe;
        private System.Windows.Forms.Label LBL_Toe;
    }
}