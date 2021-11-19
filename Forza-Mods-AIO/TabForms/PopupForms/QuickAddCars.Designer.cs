
namespace Forza_Mods_AIO.TabForms.PopupForms
{
    partial class QuickAddCars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickAddCars));
            this.BTN_Close = new System.Windows.Forms.Button();
            this.LBL_Title = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.AddAll = new Telerik.WinControls.UI.RadCheckBox();
            this.AddRare = new Telerik.WinControls.UI.RadCheckBox();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddRare)).BeginInit();
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
            this.BTN_Close.Location = new System.Drawing.Point(230, 0);
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
            this.LBL_Title.Location = new System.Drawing.Point(7, 0);
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
            this.TopPanel.Size = new System.Drawing.Size(265, 35);
            this.TopPanel.TabIndex = 8;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // AddAll
            // 
            this.AddAll.Font = new System.Drawing.Font("Open Sans", 10F);
            this.AddAll.Location = new System.Drawing.Point(12, 47);
            this.AddAll.Name = "AddAll";
            this.AddAll.Size = new System.Drawing.Size(98, 20);
            this.AddAll.TabIndex = 30;
            this.AddAll.Text = "Add all cars";
            this.AddAll.ThemeName = "FluentDark";
            this.AddAll.CheckStateChanged += new System.EventHandler(this.AddAll_CheckStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.AddAll.GetChildAt(0))).Text = "Add all cars";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).LineLimit = false;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Open Sans", 10F);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).CheckPrimitiveStyle = Telerik.WinControls.Enumerations.CheckPrimitiveStyleEnum.Win8;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).AutoSize = true;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Image = null;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).ForeColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddAll.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Enabled = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddAll.GetChildAt(0).GetChildAt(2))).BackColor = System.Drawing.Color.Transparent;
            // 
            // AddRare
            // 
            this.AddRare.Font = new System.Drawing.Font("Open Sans", 10F);
            this.AddRare.Location = new System.Drawing.Point(147, 47);
            this.AddRare.Name = "AddRare";
            this.AddRare.Size = new System.Drawing.Size(109, 20);
            this.AddRare.TabIndex = 30;
            this.AddRare.Text = "Add rare cars";
            this.AddRare.ThemeName = "FluentDark";
            this.AddRare.CheckStateChanged += new System.EventHandler(this.AddRare_CheckStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.AddRare.GetChildAt(0))).Text = "Add rare cars";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).LineLimit = false;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Open Sans", 10F);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).CheckPrimitiveStyle = Telerik.WinControls.Enumerations.CheckPrimitiveStyleEnum.Win8;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).AutoSize = true;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.CheckPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Image = null;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).ForeColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.ImagePrimitive)(this.AddRare.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(3))).Enabled = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.AddRare.GetChildAt(0).GetChildAt(2))).BackColor = System.Drawing.Color.Transparent;
            // 
            // QuickAddCars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(265, 81);
            this.ControlBox = false;
            this.Controls.Add(this.AddRare);
            this.Controls.Add(this.AddAll);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickAddCars";
            this.Text = "Saveswapper Help";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddRare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button BTN_Close;
        public System.Windows.Forms.Label LBL_Title;
        private System.Windows.Forms.Panel TopPanel;
        public Telerik.WinControls.UI.RadCheckBox AddAll;
        public Telerik.WinControls.UI.RadCheckBox AddRare;
    }
}