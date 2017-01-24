using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TinyLisp
{
    public partial class frmGraphics : Form
    {
        Bitmap Canvas = null;

        public frmGraphics()
        {
            InitializeComponent();
        }

        public Graphics GetGraphics()
        {
            Canvas = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            Graphics formGraphics = Graphics.FromImage(Canvas);
            return formGraphics;
        }

        private void tiUpdater_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void frmGraphics_Paint(object sender, PaintEventArgs e)
        {
            const int LENGTH = 5;
            Turtle current = TurtlesManager.CurrentTurtle;
            if (current != null && Canvas != null)
            {
                int tX = (int)current.X;
                int tY = (int)current.Y;
                double tRot = current.Rotation;
                int leX = tX + (int)(Math.Cos(tRot) * LENGTH * 2);
                int leY = tY + (int)(Math.Sin(tRot) * LENGTH * 2);
                e.Graphics.DrawImage(Canvas, 0, 0);
                e.Graphics.DrawEllipse(Pens.Black, tX - LENGTH, tY - LENGTH, LENGTH * 2, LENGTH * 2);
                e.Graphics.DrawLine(Pens.Black, tX, tY, leX, leY);
            }
        }

        private void frmGraphics_Load(object sender, EventArgs e)
        {
            tiUpdater.Enabled = true;
        }

        private void frmGraphics_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                tiUpdater.Enabled = false;
                this.Hide();
                this.Owner.Focus();
                e.Cancel = true;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSavePicture.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Canvas.Save(sfdSavePicture.FileName);
            }
        }
    }
}
