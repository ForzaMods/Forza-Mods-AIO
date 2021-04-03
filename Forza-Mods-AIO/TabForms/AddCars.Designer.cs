
namespace Forza_Mods_AIO
{
    partial class AddCars
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
            this.TB_RemoveCars = new System.Windows.Forms.CheckBox();
            this.TB_ACAutoshow = new System.Windows.Forms.CheckBox();
            this.TXT_ACGuide = new System.Windows.Forms.RichTextBox();
            this.LBL_ACCarList = new System.Windows.Forms.Label();
            this.LSTBOX_ACListSelect = new System.Windows.Forms.ComboBox();
            this.LBL_ACSortingMethod = new System.Windows.Forms.Label();
            this.LSTBOX_ACSortSelect = new System.Windows.Forms.ComboBox();
            this.LST_ACCarSelect = new System.Windows.Forms.ListBox();
            this.BTN_ACAddCar = new System.Windows.Forms.Button();
            this.TB_ACManualID = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TB_RemoveCars
            // 
            this.TB_RemoveCars.AutoSize = true;
            this.TB_RemoveCars.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_RemoveCars.Location = new System.Drawing.Point(12, 416);
            this.TB_RemoveCars.Name = "TB_RemoveCars";
            this.TB_RemoveCars.Size = new System.Drawing.Size(136, 23);
            this.TB_RemoveCars.TabIndex = 33;
            this.TB_RemoveCars.Text = "Remove All Cars";
            this.TB_RemoveCars.UseVisualStyleBackColor = true;
            // 
            // TB_ACAutoshow
            // 
            this.TB_ACAutoshow.AutoSize = true;
            this.TB_ACAutoshow.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_ACAutoshow.Location = new System.Drawing.Point(12, 379);
            this.TB_ACAutoshow.Name = "TB_ACAutoshow";
            this.TB_ACAutoshow.Size = new System.Drawing.Size(183, 23);
            this.TB_ACAutoshow.TabIndex = 32;
            this.TB_ACAutoshow.Text = "Change Autoshow Cars";
            this.TB_ACAutoshow.UseVisualStyleBackColor = true;
            // 
            // TXT_ACGuide
            // 
            this.TXT_ACGuide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TXT_ACGuide.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXT_ACGuide.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TXT_ACGuide.Font = new System.Drawing.Font("Open Sans", 13F);
            this.TXT_ACGuide.ForeColor = System.Drawing.Color.White;
            this.TXT_ACGuide.Location = new System.Drawing.Point(651, 6);
            this.TXT_ACGuide.Margin = new System.Windows.Forms.Padding(0);
            this.TXT_ACGuide.Name = "TXT_ACGuide";
            this.TXT_ACGuide.ReadOnly = true;
            this.TXT_ACGuide.Size = new System.Drawing.Size(347, 344);
            this.TXT_ACGuide.TabIndex = 25;
            this.TXT_ACGuide.TabStop = false;
            this.TXT_ACGuide.Text = "Maybe put a how to use guide here or something idk\n\n\n\n\nI would recommend using th" +
    "e Change Autoshow Cars toggle as it is more stable and easier to use\n";
            // 
            // LBL_ACCarList
            // 
            this.LBL_ACCarList.AutoSize = true;
            this.LBL_ACCarList.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_ACCarList.Location = new System.Drawing.Point(10, 97);
            this.LBL_ACCarList.Name = "LBL_ACCarList";
            this.LBL_ACCarList.Size = new System.Drawing.Size(129, 19);
            this.LBL_ACCarList.TabIndex = 31;
            this.LBL_ACCarList.Text = "Displayed Car List";
            // 
            // LSTBOX_ACListSelect
            // 
            this.LSTBOX_ACListSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.LSTBOX_ACListSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LSTBOX_ACListSelect.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LSTBOX_ACListSelect.ForeColor = System.Drawing.Color.White;
            this.LSTBOX_ACListSelect.FormattingEnabled = true;
            this.LSTBOX_ACListSelect.Items.AddRange(new object[] {
            "item 1",
            "item 2",
            "item 3",
            "item 4",
            "item 5",
            "item 6"});
            this.LSTBOX_ACListSelect.Location = new System.Drawing.Point(3, 119);
            this.LSTBOX_ACListSelect.Name = "LSTBOX_ACListSelect";
            this.LSTBOX_ACListSelect.Size = new System.Drawing.Size(299, 26);
            this.LSTBOX_ACListSelect.TabIndex = 30;
            // 
            // LBL_ACSortingMethod
            // 
            this.LBL_ACSortingMethod.AutoSize = true;
            this.LBL_ACSortingMethod.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_ACSortingMethod.Location = new System.Drawing.Point(10, 24);
            this.LBL_ACSortingMethod.Name = "LBL_ACSortingMethod";
            this.LBL_ACSortingMethod.Size = new System.Drawing.Size(113, 19);
            this.LBL_ACSortingMethod.TabIndex = 29;
            this.LBL_ACSortingMethod.Text = "Sorting Method";
            // 
            // LSTBOX_ACSortSelect
            // 
            this.LSTBOX_ACSortSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.LSTBOX_ACSortSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LSTBOX_ACSortSelect.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LSTBOX_ACSortSelect.ForeColor = System.Drawing.Color.White;
            this.LSTBOX_ACSortSelect.FormattingEnabled = true;
            this.LSTBOX_ACSortSelect.Items.AddRange(new object[] {
            "item 1",
            "item 2",
            "item 3",
            "item 4",
            "item 5",
            "item 6"});
            this.LSTBOX_ACSortSelect.Location = new System.Drawing.Point(3, 46);
            this.LSTBOX_ACSortSelect.Name = "LSTBOX_ACSortSelect";
            this.LSTBOX_ACSortSelect.Size = new System.Drawing.Size(299, 26);
            this.LSTBOX_ACSortSelect.TabIndex = 28;
            // 
            // LST_ACCarSelect
            // 
            this.LST_ACCarSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.LST_ACCarSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LST_ACCarSelect.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LST_ACCarSelect.ForeColor = System.Drawing.Color.White;
            this.LST_ACCarSelect.FormattingEnabled = true;
            this.LST_ACCarSelect.ItemHeight = 18;
            this.LST_ACCarSelect.Items.AddRange(new object[] {
            "car 1",
            "car 2",
            "car 3",
            "car 4",
            "car 5",
            "car 6"});
            this.LST_ACCarSelect.Location = new System.Drawing.Point(308, 7);
            this.LST_ACCarSelect.Name = "LST_ACCarSelect";
            this.LST_ACCarSelect.Size = new System.Drawing.Size(340, 432);
            this.LST_ACCarSelect.TabIndex = 27;
            // 
            // BTN_ACAddCar
            // 
            this.BTN_ACAddCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(33)))));
            this.BTN_ACAddCar.FlatAppearance.BorderSize = 0;
            this.BTN_ACAddCar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.BTN_ACAddCar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.BTN_ACAddCar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_ACAddCar.Font = new System.Drawing.Font("Open Sans", 11F);
            this.BTN_ACAddCar.ForeColor = System.Drawing.Color.White;
            this.BTN_ACAddCar.Location = new System.Drawing.Point(746, 379);
            this.BTN_ACAddCar.Margin = new System.Windows.Forms.Padding(0);
            this.BTN_ACAddCar.Name = "BTN_ACAddCar";
            this.BTN_ACAddCar.Size = new System.Drawing.Size(150, 50);
            this.BTN_ACAddCar.TabIndex = 26;
            this.BTN_ACAddCar.TabStop = false;
            this.BTN_ACAddCar.Text = "Add Car";
            this.BTN_ACAddCar.UseVisualStyleBackColor = false;
            // 
            // TB_ACManualID
            // 
            this.TB_ACManualID.AutoSize = true;
            this.TB_ACManualID.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_ACManualID.Location = new System.Drawing.Point(751, 353);
            this.TB_ACManualID.Name = "TB_ACManualID";
            this.TB_ACManualID.Size = new System.Drawing.Size(148, 23);
            this.TB_ACManualID.TabIndex = 24;
            this.TB_ACManualID.Text = "Manually Input ID";
            this.TB_ACManualID.UseVisualStyleBackColor = true;
            // 
            // AddCars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1000, 445);
            this.Controls.Add(this.TB_RemoveCars);
            this.Controls.Add(this.TB_ACAutoshow);
            this.Controls.Add(this.TXT_ACGuide);
            this.Controls.Add(this.LBL_ACCarList);
            this.Controls.Add(this.LSTBOX_ACListSelect);
            this.Controls.Add(this.LBL_ACSortingMethod);
            this.Controls.Add(this.LSTBOX_ACSortSelect);
            this.Controls.Add(this.LST_ACCarSelect);
            this.Controls.Add(this.BTN_ACAddCar);
            this.Controls.Add(this.TB_ACManualID);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddCars";
            this.Text = "AddCars";
            this.Load += new System.EventHandler(this.AddCars_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TB_RemoveCars;
        private System.Windows.Forms.CheckBox TB_ACAutoshow;
        private System.Windows.Forms.RichTextBox TXT_ACGuide;
        private System.Windows.Forms.Label LBL_ACCarList;
        private System.Windows.Forms.ComboBox LSTBOX_ACListSelect;
        private System.Windows.Forms.Label LBL_ACSortingMethod;
        private System.Windows.Forms.ComboBox LSTBOX_ACSortSelect;
        private System.Windows.Forms.ListBox LST_ACCarSelect;
        private System.Windows.Forms.Button BTN_ACAddCar;
        private System.Windows.Forms.CheckBox TB_ACManualID;
    }
}