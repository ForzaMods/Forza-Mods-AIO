
namespace Forza_Mods_AIO.TabForms.LiveTuningForms
{
    partial class Aero
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
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.RearTyreNUD = new System.Windows.Forms.NumericUpDown();
            this.RearTyreBar = new System.Windows.Forms.TrackBar();
            this.FrontTyreNUD = new System.Windows.Forms.NumericUpDown();
            this.LBL_RearTyres = new System.Windows.Forms.Label();
            this.FrontTyreBar = new System.Windows.Forms.TrackBar();
            this.LBL_FrontTyres = new System.Windows.Forms.Label();
            this.LBL_Aero = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.panel3.Controls.Add(this.BTN_Refresh);
            this.panel3.Controls.Add(this.RearTyreNUD);
            this.panel3.Controls.Add(this.RearTyreBar);
            this.panel3.Controls.Add(this.FrontTyreNUD);
            this.panel3.Controls.Add(this.LBL_RearTyres);
            this.panel3.Controls.Add(this.FrontTyreBar);
            this.panel3.Controls.Add(this.LBL_FrontTyres);
            this.panel3.Controls.Add(this.LBL_Aero);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(976, 386);
            this.panel3.TabIndex = 7;
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
            // RearTyreNUD
            // 
            this.RearTyreNUD.Location = new System.Drawing.Point(841, 130);
            this.RearTyreNUD.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.RearTyreNUD.Name = "RearTyreNUD";
            this.RearTyreNUD.Size = new System.Drawing.Size(120, 22);
            this.RearTyreNUD.TabIndex = 3;
            this.RearTyreNUD.ValueChanged += new System.EventHandler(this.RearTyreNUD_ValueChanged);
            // 
            // RearTyreBar
            // 
            this.RearTyreBar.LargeChange = 10;
            this.RearTyreBar.Location = new System.Drawing.Point(3, 130);
            this.RearTyreBar.Maximum = 300;
            this.RearTyreBar.Name = "RearTyreBar";
            this.RearTyreBar.Size = new System.Drawing.Size(832, 45);
            this.RearTyreBar.TabIndex = 2;
            this.RearTyreBar.Scroll += new System.EventHandler(this.RearTyreBar_Scroll);
            // 
            // FrontTyreNUD
            // 
            this.FrontTyreNUD.Location = new System.Drawing.Point(841, 68);
            this.FrontTyreNUD.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.FrontTyreNUD.Name = "FrontTyreNUD";
            this.FrontTyreNUD.Size = new System.Drawing.Size(120, 22);
            this.FrontTyreNUD.TabIndex = 3;
            this.FrontTyreNUD.ValueChanged += new System.EventHandler(this.FrontTyreNUD_ValueChanged);
            // 
            // LBL_RearTyres
            // 
            this.LBL_RearTyres.AutoSize = true;
            this.LBL_RearTyres.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_RearTyres.Location = new System.Drawing.Point(449, 100);
            this.LBL_RearTyres.Name = "LBL_RearTyres";
            this.LBL_RearTyres.Size = new System.Drawing.Size(54, 27);
            this.LBL_RearTyres.TabIndex = 1;
            this.LBL_RearTyres.Text = "Rear";
            // 
            // FrontTyreBar
            // 
            this.FrontTyreBar.LargeChange = 10;
            this.FrontTyreBar.Location = new System.Drawing.Point(3, 68);
            this.FrontTyreBar.Maximum = 300;
            this.FrontTyreBar.Name = "FrontTyreBar";
            this.FrontTyreBar.Size = new System.Drawing.Size(832, 45);
            this.FrontTyreBar.TabIndex = 2;
            this.FrontTyreBar.Scroll += new System.EventHandler(this.FrontTyreBar_Scroll);
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
            // LBL_Aero
            // 
            this.LBL_Aero.AutoSize = true;
            this.LBL_Aero.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Aero.Location = new System.Drawing.Point(449, 10);
            this.LBL_Aero.Name = "LBL_Aero";
            this.LBL_Aero.Size = new System.Drawing.Size(56, 27);
            this.LBL_Aero.TabIndex = 1;
            this.LBL_Aero.Text = "Aero";
            // 
            // Aero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 410);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Aero";
            this.Text = "LiveTuning";
            this.Load += new System.EventHandler(this.Aero_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearTyreBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontTyreBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LBL_Aero;
        private System.Windows.Forms.NumericUpDown RearTyreNUD;
        private System.Windows.Forms.TrackBar RearTyreBar;
        private System.Windows.Forms.NumericUpDown FrontTyreNUD;
        private System.Windows.Forms.Label LBL_RearTyres;
        private System.Windows.Forms.TrackBar FrontTyreBar;
        private System.Windows.Forms.Label LBL_FrontTyres;
        public System.Windows.Forms.Button BTN_Refresh;
    }
}