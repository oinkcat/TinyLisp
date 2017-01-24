namespace TinyLisp
{
    partial class frmGraphics
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGraphics));
            this.tiUpdater = new System.Windows.Forms.Timer(this.components);
            this.cmPictureOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSavePicture = new System.Windows.Forms.SaveFileDialog();
            this.cmPictureOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tiUpdater
            // 
            this.tiUpdater.Interval = 50;
            this.tiUpdater.Tick += new System.EventHandler(this.tiUpdater_Tick);
            // 
            // cmPictureOptions
            // 
            this.cmPictureOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem});
            this.cmPictureOptions.Name = "cmPictureOptions";
            this.cmPictureOptions.Size = new System.Drawing.Size(163, 26);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.saveHS;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьToolStripMenuItem.Text = "Со&хранить как...";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // sfdSavePicture
            // 
            this.sfdSavePicture.Filter = "PNG изображения|*.png";
            this.sfdSavePicture.Title = "Сохранение рисунка";
            // 
            // frmGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(634, 472);
            this.ContextMenuStrip = this.cmPictureOptions;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGraphics";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графика";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGraphics_FormClosing);
            this.Load += new System.EventHandler(this.frmGraphics_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmGraphics_Paint);
            this.cmPictureOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tiUpdater;
        private System.Windows.Forms.ContextMenuStrip cmPictureOptions;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdSavePicture;
    }
}