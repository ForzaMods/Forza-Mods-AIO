
namespace Forza_Mods_AIO.TabForms.PopupForms
{
    partial class Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Controls));
            this.BTN_Close = new System.Windows.Forms.Button();
            this.LBL_Title = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.SpeedhackLabel = new System.Windows.Forms.Label();
            this.BrakeLabel = new System.Windows.Forms.Label();
            this.JumpLabel = new System.Windows.Forms.Label();
            this.Keyb1 = new System.Windows.Forms.Label();
            this.Cont1 = new System.Windows.Forms.Label();
            this.XBChangeSpeed = new System.Windows.Forms.Button();
            this.KBChangeSpeed = new System.Windows.Forms.Button();
            this.KBChangeBrake = new System.Windows.Forms.Button();
            this.XBChangeBrake = new System.Windows.Forms.Button();
            this.Cont2 = new System.Windows.Forms.Label();
            this.Keyb2 = new System.Windows.Forms.Label();
            this.KBChangeJump = new System.Windows.Forms.Button();
            this.XBChangeJump = new System.Windows.Forms.Button();
            this.Cont3 = new System.Windows.Forms.Label();
            this.Keyb3 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_Close
            // 
            this.BTN_Close.BackColor = System.Drawing.Color.Black;
            this.BTN_Close.FlatAppearance.BorderSize = 0;
            this.BTN_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.BTN_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Close.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Bold);
            this.BTN_Close.ForeColor = System.Drawing.Color.Red;
            this.BTN_Close.Location = new System.Drawing.Point(692, 0);
            this.BTN_Close.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(35, 35);
            this.BTN_Close.TabIndex = 9;
            this.BTN_Close.TabStop = false;
            this.BTN_Close.Text = "X";
            this.BTN_Close.UseVisualStyleBackColor = false;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // LBL_Title
            // 
            this.LBL_Title.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_Title.AutoSize = true;
            this.LBL_Title.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Title.Font = new System.Drawing.Font("Open Sans", 16F);
            this.LBL_Title.ForeColor = System.Drawing.Color.White;
            this.LBL_Title.Location = new System.Drawing.Point(252, 0);
            this.LBL_Title.Margin = new System.Windows.Forms.Padding(0);
            this.LBL_Title.Name = "LBL_Title";
            this.LBL_Title.Size = new System.Drawing.Size(223, 30);
            this.LBL_Title.TabIndex = 8;
            this.LBL_Title.Text = "Forza Mods AIO Tool";
            this.LBL_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.LBL_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.LBL_Title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Black;
            this.TopPanel.Controls.Add(this.LBL_Title);
            this.TopPanel.Controls.Add(this.BTN_Close);
            this.TopPanel.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.TopPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(727, 35);
            this.TopPanel.TabIndex = 8;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // SpeedhackLabel
            // 
            this.SpeedhackLabel.AutoSize = true;
            this.SpeedhackLabel.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedhackLabel.Location = new System.Drawing.Point(25, 46);
            this.SpeedhackLabel.Name = "SpeedhackLabel";
            this.SpeedhackLabel.Size = new System.Drawing.Size(156, 27);
            this.SpeedhackLabel.TabIndex = 35;
            this.SpeedhackLabel.Text = "Wheel/Vel Hack";
            // 
            // BrakeLabel
            // 
            this.BrakeLabel.AutoSize = true;
            this.BrakeLabel.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrakeLabel.Location = new System.Drawing.Point(249, 46);
            this.BrakeLabel.Name = "BrakeLabel";
            this.BrakeLabel.Size = new System.Drawing.Size(246, 27);
            this.BrakeLabel.TabIndex = 35;
            this.BrakeLabel.Text = "Super Brake/Stop Wheels";
            // 
            // JumpLabel
            // 
            this.JumpLabel.AutoSize = true;
            this.JumpLabel.Font = new System.Drawing.Font("Open Sans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JumpLabel.Location = new System.Drawing.Point(596, 46);
            this.JumpLabel.Name = "JumpLabel";
            this.JumpLabel.Size = new System.Drawing.Size(60, 27);
            this.JumpLabel.TabIndex = 35;
            this.JumpLabel.Text = "Jump";
            // 
            // Keyb1
            // 
            this.Keyb1.AutoSize = true;
            this.Keyb1.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keyb1.Location = new System.Drawing.Point(12, 85);
            this.Keyb1.Name = "Keyb1";
            this.Keyb1.Size = new System.Drawing.Size(73, 19);
            this.Keyb1.TabIndex = 38;
            this.Keyb1.Text = "Keyboard";
            // 
            // Cont1
            // 
            this.Cont1.AutoSize = true;
            this.Cont1.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cont1.Location = new System.Drawing.Point(13, 125);
            this.Cont1.Name = "Cont1";
            this.Cont1.Size = new System.Drawing.Size(76, 19);
            this.Cont1.TabIndex = 39;
            this.Cont1.Text = "Controller";
            // 
            // XBChangeSpeed
            // 
            this.XBChangeSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.XBChangeSpeed.FlatAppearance.BorderSize = 0;
            this.XBChangeSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XBChangeSpeed.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XBChangeSpeed.Location = new System.Drawing.Point(91, 120);
            this.XBChangeSpeed.Name = "XBChangeSpeed";
            this.XBChangeSpeed.Size = new System.Drawing.Size(97, 27);
            this.XBChangeSpeed.TabIndex = 40;
            this.XBChangeSpeed.UseVisualStyleBackColor = false;
            this.XBChangeSpeed.Click += new System.EventHandler(this.XBChangeSpeed_Click);
            // 
            // KBChangeSpeed
            // 
            this.KBChangeSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.KBChangeSpeed.FlatAppearance.BorderSize = 0;
            this.KBChangeSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KBChangeSpeed.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KBChangeSpeed.Location = new System.Drawing.Point(91, 85);
            this.KBChangeSpeed.Name = "KBChangeSpeed";
            this.KBChangeSpeed.Size = new System.Drawing.Size(97, 27);
            this.KBChangeSpeed.TabIndex = 41;
            this.KBChangeSpeed.UseVisualStyleBackColor = false;
            this.KBChangeSpeed.Click += new System.EventHandler(this.KBChangeSpeed_Click);
            // 
            // KBChangeBrake
            // 
            this.KBChangeBrake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.KBChangeBrake.FlatAppearance.BorderSize = 0;
            this.KBChangeBrake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KBChangeBrake.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KBChangeBrake.Location = new System.Drawing.Point(360, 85);
            this.KBChangeBrake.Name = "KBChangeBrake";
            this.KBChangeBrake.Size = new System.Drawing.Size(97, 27);
            this.KBChangeBrake.TabIndex = 41;
            this.KBChangeBrake.UseVisualStyleBackColor = false;
            this.KBChangeBrake.Click += new System.EventHandler(this.KBChangeBrake_Click);
            // 
            // XBChangeBrake
            // 
            this.XBChangeBrake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.XBChangeBrake.FlatAppearance.BorderSize = 0;
            this.XBChangeBrake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XBChangeBrake.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XBChangeBrake.Location = new System.Drawing.Point(360, 120);
            this.XBChangeBrake.Name = "XBChangeBrake";
            this.XBChangeBrake.Size = new System.Drawing.Size(97, 27);
            this.XBChangeBrake.TabIndex = 40;
            this.XBChangeBrake.UseVisualStyleBackColor = false;
            this.XBChangeBrake.Click += new System.EventHandler(this.XBChangeBrake_Click);
            // 
            // Cont2
            // 
            this.Cont2.AutoSize = true;
            this.Cont2.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cont2.Location = new System.Drawing.Point(282, 125);
            this.Cont2.Name = "Cont2";
            this.Cont2.Size = new System.Drawing.Size(76, 19);
            this.Cont2.TabIndex = 39;
            this.Cont2.Text = "Controller";
            // 
            // Keyb2
            // 
            this.Keyb2.AutoSize = true;
            this.Keyb2.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keyb2.Location = new System.Drawing.Point(281, 85);
            this.Keyb2.Name = "Keyb2";
            this.Keyb2.Size = new System.Drawing.Size(73, 19);
            this.Keyb2.TabIndex = 38;
            this.Keyb2.Text = "Keyboard";
            // 
            // KBChangeJump
            // 
            this.KBChangeJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.KBChangeJump.FlatAppearance.BorderSize = 0;
            this.KBChangeJump.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KBChangeJump.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KBChangeJump.Location = new System.Drawing.Point(610, 86);
            this.KBChangeJump.Name = "KBChangeJump";
            this.KBChangeJump.Size = new System.Drawing.Size(97, 27);
            this.KBChangeJump.TabIndex = 41;
            this.KBChangeJump.UseVisualStyleBackColor = false;
            this.KBChangeJump.Click += new System.EventHandler(this.KBChangeJump_Click);
            // 
            // XBChangeJump
            // 
            this.XBChangeJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.XBChangeJump.FlatAppearance.BorderSize = 0;
            this.XBChangeJump.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XBChangeJump.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XBChangeJump.Location = new System.Drawing.Point(610, 121);
            this.XBChangeJump.Name = "XBChangeJump";
            this.XBChangeJump.Size = new System.Drawing.Size(97, 27);
            this.XBChangeJump.TabIndex = 40;
            this.XBChangeJump.UseVisualStyleBackColor = false;
            this.XBChangeJump.Click += new System.EventHandler(this.XBChangeJump_Click);
            // 
            // Cont3
            // 
            this.Cont3.AutoSize = true;
            this.Cont3.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cont3.Location = new System.Drawing.Point(532, 126);
            this.Cont3.Name = "Cont3";
            this.Cont3.Size = new System.Drawing.Size(76, 19);
            this.Cont3.TabIndex = 39;
            this.Cont3.Text = "Controller";
            // 
            // Keyb3
            // 
            this.Keyb3.AutoSize = true;
            this.Keyb3.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keyb3.Location = new System.Drawing.Point(531, 86);
            this.Keyb3.Name = "Keyb3";
            this.Keyb3.Size = new System.Drawing.Size(73, 19);
            this.Keyb3.TabIndex = 38;
            this.Keyb3.Text = "Keyboard";
            // 
            // Controls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(727, 169);
            this.ControlBox = false;
            this.Controls.Add(this.Keyb3);
            this.Controls.Add(this.Keyb2);
            this.Controls.Add(this.Keyb1);
            this.Controls.Add(this.Cont3);
            this.Controls.Add(this.Cont2);
            this.Controls.Add(this.Cont1);
            this.Controls.Add(this.XBChangeJump);
            this.Controls.Add(this.XBChangeBrake);
            this.Controls.Add(this.XBChangeSpeed);
            this.Controls.Add(this.KBChangeJump);
            this.Controls.Add(this.KBChangeBrake);
            this.Controls.Add(this.KBChangeSpeed);
            this.Controls.Add(this.JumpLabel);
            this.Controls.Add(this.BrakeLabel);
            this.Controls.Add(this.SpeedhackLabel);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Controls";
            this.Text = "Controls";
            this.Load += new System.EventHandler(this.Controls_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button BTN_Close;
        public System.Windows.Forms.Label LBL_Title;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label SpeedhackLabel;
        private System.Windows.Forms.Label BrakeLabel;
        private System.Windows.Forms.Label JumpLabel;
        private System.Windows.Forms.Label Keyb1;
        private System.Windows.Forms.Label Cont1;
        private System.Windows.Forms.Label Cont2;
        private System.Windows.Forms.Label Keyb2;
        private System.Windows.Forms.Label Cont3;
        private System.Windows.Forms.Label Keyb3;
        public System.Windows.Forms.Button KBChangeSpeed;
        public System.Windows.Forms.Button XBChangeSpeed;
        public System.Windows.Forms.Button KBChangeBrake;
        public System.Windows.Forms.Button XBChangeBrake;
        public System.Windows.Forms.Button KBChangeJump;
        public System.Windows.Forms.Button XBChangeJump;
    }
}