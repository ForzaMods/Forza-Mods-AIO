
namespace Forza_Mods_AIO.TabForms.LiveTuningForms
{
    partial class Tyres
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.FrontTyreNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.RearTyreNUD = new DarkUI.Controls.DarkNumericUpDown();
            this.RearTyreBar = new LimitlessUI.Slider_WOC();
            this.FrontTyreBar = new LimitlessUI.Slider_WOC();
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.LBL_RearTyres = new System.Windows.Forms.Label();
            this.LBL_FrontTyres = new System.Windows.Forms.Label();
            this.LBL_Tyres = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.panel3.Controls.Add(this.FrontTyreNUD);
            this.panel3.Controls.Add(this.RearTyreNUD);
            this.panel3.Controls.Add(this.RearTyreBar);
            this.panel3.Controls.Add(this.FrontTyreBar);
            this.panel3.Controls.Add(this.BTN_Refresh);
            this.panel3.Controls.Add(this.LBL_RearTyres);
            this.panel3.Controls.Add(this.LBL_FrontTyres);
            this.panel3.Controls.Add(this.LBL_Tyres);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(976, 386);
            this.panel3.TabIndex = 7;
            // 
            // FrontTyreNUD
            // 
            this.FrontTyreNUD.DecimalPlaces = 1;
            this.FrontTyreNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FrontTyreNUD.Location = new System.Drawing.Point(841, 68);
            this.FrontTyreNUD.Name = "FrontTyreNUD";
            this.FrontTyreNUD.Size = new System.Drawing.Size(120, 22);
            this.FrontTyreNUD.TabIndex = 41;
            this.FrontTyreNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FrontTyreNUD.ValueChanged += new System.EventHandler(this.FrontTyreNUD_ValueChanged);
            // 
            // RearTyreNUD
            // 
            this.RearTyreNUD.DecimalPlaces = 1;
            this.RearTyreNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RearTyreNUD.Location = new System.Drawing.Point(841, 119);
            this.RearTyreNUD.Name = "RearTyreNUD";
            this.RearTyreNUD.Size = new System.Drawing.Size(120, 22);
            this.RearTyreNUD.TabIndex = 41;
            this.RearTyreNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RearTyreNUD.ValueChanged += new System.EventHandler(this.RearTyreNUD_ValueChanged);
            // 
            // RearTyreBar
            // 
            this.RearTyreBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RearTyreBar.BackLineThikness = 3F;
            this.RearTyreBar.CircleSize = 10F;
            this.RearTyreBar.DrawCircle = true;
            this.RearTyreBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.RearTyreBar.FrontLineThikness = 3F;
            this.RearTyreBar.Location = new System.Drawing.Point(11, 119);
            this.RearTyreBar.MaxValue = 1000F;
            this.RearTyreBar.MinValue = 0F;
            this.RearTyreBar.Name = "RearTyreBar";
            this.RearTyreBar.Rounded = false;
            this.RearTyreBar.Size = new System.Drawing.Size(817, 23);
            this.RearTyreBar.TabIndex = 40;
            this.RearTyreBar.Text = "VelMultBar";
            this.RearTyreBar.Value = 0F;
            this.RearTyreBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.RearTyreBar_Scroll);
            // 
            // FrontTyreBar
            // 
            this.FrontTyreBar.BackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.FrontTyreBar.BackLineThikness = 3F;
            this.FrontTyreBar.CircleSize = 10F;
            this.FrontTyreBar.DrawCircle = true;
            this.FrontTyreBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(11)))), ((int)(((byte)(166)))));
            this.FrontTyreBar.FrontLineThikness = 3F;
            this.FrontTyreBar.Location = new System.Drawing.Point(11, 68);
            this.FrontTyreBar.MaxValue = 1000F;
            this.FrontTyreBar.MinValue = 0F;
            this.FrontTyreBar.Name = "FrontTyreBar";
            this.FrontTyreBar.Rounded = false;
            this.FrontTyreBar.Size = new System.Drawing.Size(817, 23);
            this.FrontTyreBar.TabIndex = 40;
            this.FrontTyreBar.Text = "VelMultBar";
            this.FrontTyreBar.Value = 0F;
            this.FrontTyreBar.OnValueChanged += new LimitlessUI.Slider_WOC.OnValueChangedEvent(this.FrontTyreBar_Scroll);
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
            this.BTN_Refresh.TabIndex = 39;
            this.BTN_Refresh.Text = "Refresh";
            this.BTN_Refresh.UseVisualStyleBackColor = false;
            this.BTN_Refresh.Click += new System.EventHandler(this.BTN_Refresh_Click);
            // 
            // LBL_RearTyres
            // 
            this.LBL_RearTyres.AutoSize = true;
            this.LBL_RearTyres.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_RearTyres.Location = new System.Drawing.Point(452, 94);
            this.LBL_RearTyres.Name = "LBL_RearTyres";
            this.LBL_RearTyres.Size = new System.Drawing.Size(54, 27);
            this.LBL_RearTyres.TabIndex = 1;
            this.LBL_RearTyres.Text = "Rear";
            // 
            // LBL_FrontTyres
            // 
            this.LBL_FrontTyres.AutoSize = true;
            this.LBL_FrontTyres.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_FrontTyres.Location = new System.Drawing.Point(449, 37);
            this.LBL_FrontTyres.Name = "LBL_FrontTyres";
            this.LBL_FrontTyres.Size = new System.Drawing.Size(61, 27);
            this.LBL_FrontTyres.TabIndex = 1;
            this.LBL_FrontTyres.Text = "Front";
            // 
            // LBL_Tyres
            // 
            this.LBL_Tyres.AutoSize = true;
            this.LBL_Tyres.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Tyres.Location = new System.Drawing.Point(449, 10);
            this.LBL_Tyres.Name = "LBL_Tyres";
            this.LBL_Tyres.Size = new System.Drawing.Size(62, 27);
            this.LBL_Tyres.TabIndex = 1;
            this.LBL_Tyres.Text = "Tyres";
            // 
            // Tyres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 410);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tyres";
            this.Text = "LiveTuning";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LBL_Tyres;
        private System.Windows.Forms.Label LBL_RearTyres;
        private System.Windows.Forms.Label LBL_FrontTyres;
        public System.Windows.Forms.Button BTN_Refresh;
        public LimitlessUI.Slider_WOC FrontTyreBar;
        public LimitlessUI.Slider_WOC RearTyreBar;
        private DarkUI.Controls.DarkNumericUpDown RearTyreNUD;
        private DarkUI.Controls.DarkNumericUpDown FrontTyreNUD;
    }
}