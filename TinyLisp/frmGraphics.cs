using System;
using System.Drawing;
using System.Windows.Forms;

namespace TinyLisp
{
    /// <summary>
    /// Форма для вывода графики
    /// </summary>
    public partial class frmGraphics : Form
    {
        private Bitmap canvas;

        /// <summary>
        /// Выдать контекст вывода графики
        /// </summary>
        /// <returns>Текущий графический контекст</returns>
        public Graphics GetGraphics()
        {
            if(canvas == null)
            {
                Rectangle bounds = ClientRectangle;
                canvas = new Bitmap(bounds.Width, bounds.Height);
            }

            Graphics formGraphics = Graphics.FromImage(canvas);
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
            if (current != null && canvas != null)
            {
                int tX = (int)current.X;
                int tY = (int)current.Y;
                double tRot = current.Rotation;
                int leX = tX + (int)(Math.Cos(tRot) * LENGTH * 2);
                int leY = tY + (int)(Math.Sin(tRot) * LENGTH * 2);
                e.Graphics.DrawImage(canvas, 0, 0);
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

        private void saveItem_Click(object sender, EventArgs e)
        {
            if (sfdSavePicture.ShowDialog() == DialogResult.OK)
            {
                canvas.Save(sfdSavePicture.FileName);
            }
        }

        public frmGraphics()
        {
            InitializeComponent();
        }
    }
}
