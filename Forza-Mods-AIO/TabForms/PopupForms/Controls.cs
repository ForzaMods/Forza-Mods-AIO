using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forza_Mods_AIO.TabForms.PopupForms
{
    public partial class Controls : Form
    {
        public static Controls c;
        public Controls()
        {
            InitializeComponent();
            c = this;
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void TopPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TopPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TopPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = false;
        }
        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #region KB
        private void KBChangeSpeed_Click(object sender, EventArgs e)
        {
            Speedhack.s.KeyboardChangeWorker.RunWorkerAsync(argument: 1);
        }
        private void KBChangeSpeed_Enter(object sender, EventArgs e)
        {
            if(!Speedhack.s.KeyboardChangeWorker.IsBusy)
            {
                KBChangeSpeed.Text = "Change Key";
            }
        }
        private void KBChangeSpeed_Leave(object sender, EventArgs e)
        {
            if (!Speedhack.s.KeyboardChangeWorker.IsBusy)
            {
                KBChangeSpeed.Text = Speedhack.KBSpeedKey;
            }
        }
        private void KBChangeBrake_Click(object sender, EventArgs e)
        {
            Speedhack.s.KeyboardChangeWorker.RunWorkerAsync(argument: 2);
        }
        private void KBChangeBrake_Enter(object sender, EventArgs e)
        {

        }
        private void KBChangeBrake_Leave(object sender, EventArgs e)
        {
           if (!Speedhack.s.KeyboardChangeWorker.IsBusy)
            {
                KBChangeBrake.Text = Speedhack.KBBrakeKey;
            }
        }
        private void KBChangeJump_Click(object sender, EventArgs e)
        {
            Speedhack.s.KeyboardChangeWorker.RunWorkerAsync(argument: 3);
        }
        private void KBChangeJump_Enter(object sender, EventArgs e)
        {
           if (!Speedhack.s.KeyboardChangeWorker.IsBusy)
            {
                KBChangeJump.Text = "Change Key";
            }
        }
        private void KBChangeJump_Leave(object sender, EventArgs e)
        {
            if (!Speedhack.s.KeyboardChangeWorker.IsBusy)
            {
                KBChangeJump.Text = Speedhack.KBJumpKey;
            }
        }
        #endregion
        #region XB
        private void XBChangeSpeed_Click(object sender, EventArgs e)
        {
            if (!Speedhack.s.ControllerChangeWorker.IsBusy)
                Speedhack.s.ControllerChangeWorker.RunWorkerAsync(argument: 1);
        }
        private void XBChangeSpeed_Enter(object sender, EventArgs e)
        {

        }
        private void XBChangeSpeed_Leave(object sender, EventArgs e)
        {

        }
        private void XBChangeBrake_Click(object sender, EventArgs e)
        {
            if (!Speedhack.s.ControllerChangeWorker.IsBusy)
                Speedhack.s.ControllerChangeWorker.RunWorkerAsync(argument: 2);
        }
        private void XBChangeBrake_Enter(object sender, EventArgs e)
        {

        }
        private void XBChangeBrake_Leave(object sender, EventArgs e)
        {

        }
        private void XBChangeJump_Click(object sender, EventArgs e)
        {
            if (!Speedhack.s.ControllerChangeWorker.IsBusy)
                Speedhack.s.ControllerChangeWorker.RunWorkerAsync(argument: 3);
        }
        private void XBChangeJump_Enter(object sender, EventArgs e)
        {

        }
        private void XBChangeJump_Leave(object sender, EventArgs e)
        {

        }
        #endregion
        private void Controls_Load(object sender, EventArgs e)
        {
            KBChangeSpeed.Text = Speedhack.KBSpeedKey;
            KBChangeBrake.Text = Speedhack.KBBrakeKey;
            KBChangeJump.Text = Speedhack.KBJumpKey;
            XBChangeSpeed.Text = Speedhack.XBSpeedKey;
            XBChangeBrake.Text = Speedhack.XBBrakeKey;
            XBChangeJump.Text = Speedhack.XBJumpKey;
        }
    }
}
