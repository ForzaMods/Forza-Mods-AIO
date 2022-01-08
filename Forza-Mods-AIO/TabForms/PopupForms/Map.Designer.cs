
namespace Forza_Mods_AIO.TabForms.PopupForms
{
    partial class Map
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map));
            this.BTN_Close = new System.Windows.Forms.Button();
            this.LBL_Title = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.MapBox = new System.Windows.Forms.PictureBox();
            this.UpdateMapWorker = new System.ComponentModel.BackgroundWorker();
            this.CoordsLabel = new System.Windows.Forms.Label();
            this.MouseCoordsLabel = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapBox)).BeginInit();
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
            this.BTN_Close.Location = new System.Drawing.Point(1009, 0);
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
            this.LBL_Title.Location = new System.Drawing.Point(411, 0);
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
            this.TopPanel.Size = new System.Drawing.Size(1044, 35);
            this.TopPanel.TabIndex = 8;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // MapBox
            // 
            this.MapBox.BackColor = System.Drawing.Color.White;
            this.MapBox.BackgroundImage = global::Forza_Mods_AIO.Properties.Resources.RectangleMap;
            this.MapBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MapBox.Location = new System.Drawing.Point(10, 45);
            this.MapBox.Name = "MapBox";
            this.MapBox.Size = new System.Drawing.Size(1024, 512);
            this.MapBox.TabIndex = 9;
            this.MapBox.TabStop = false;
            this.MapBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MapBox_MouseClick);
            // 
            // UpdateMapWorker
            // 
            this.UpdateMapWorker.WorkerReportsProgress = true;
            this.UpdateMapWorker.WorkerSupportsCancellation = true;
            this.UpdateMapWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateMapWorker_DoWork);
            // 
            // CoordsLabel
            // 
            this.CoordsLabel.AutoSize = true;
            this.CoordsLabel.Location = new System.Drawing.Point(21, 57);
            this.CoordsLabel.Name = "CoordsLabel";
            this.CoordsLabel.Size = new System.Drawing.Size(16, 30);
            this.CoordsLabel.TabIndex = 10;
            this.CoordsLabel.Text = "X:\r\nZ:";
            // 
            // MouseCoordsLabel
            // 
            this.MouseCoordsLabel.AutoSize = true;
            this.MouseCoordsLabel.Location = new System.Drawing.Point(21, 102);
            this.MouseCoordsLabel.Name = "MouseCoordsLabel";
            this.MouseCoordsLabel.Size = new System.Drawing.Size(54, 30);
            this.MouseCoordsLabel.TabIndex = 10;
            this.MouseCoordsLabel.Text = "Mouse X:\r\nMouse Z:";
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1044, 566);
            this.ControlBox = false;
            this.Controls.Add(this.MouseCoordsLabel);
            this.Controls.Add(this.CoordsLabel);
            this.Controls.Add(this.MapBox);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Map";
            this.Text = "Map";
            this.Activated += new System.EventHandler(this.Map_Activated);
            this.Load += new System.EventHandler(this.Map_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button BTN_Close;
        public System.Windows.Forms.Label LBL_Title;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox MapBox;
        private System.ComponentModel.BackgroundWorker UpdateMapWorker;
        public System.Windows.Forms.Label CoordsLabel;
        public System.Windows.Forms.Label MouseCoordsLabel;
    }
}