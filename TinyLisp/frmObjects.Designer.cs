namespace TinyLisp
{
    partial class frmObjects
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
            this.lvObjects = new System.Windows.Forms.ListView();
            this.clName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timRenew = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lvObjects
            // 
            this.lvObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clName,
            this.clType});
            this.lvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvObjects.FullRowSelect = true;
            this.lvObjects.GridLines = true;
            this.lvObjects.Location = new System.Drawing.Point(0, 0);
            this.lvObjects.Name = "lvObjects";
            this.lvObjects.Size = new System.Drawing.Size(204, 311);
            this.lvObjects.TabIndex = 0;
            this.lvObjects.UseCompatibleStateImageBehavior = false;
            this.lvObjects.View = System.Windows.Forms.View.Details;
            // 
            // clName
            // 
            this.clName.Text = "Имя";
            this.clName.Width = 80;
            // 
            // clType
            // 
            this.clType.Text = "Тип";
            this.clType.Width = 90;
            // 
            // timRenew
            // 
            this.timRenew.Interval = 1000;
            this.timRenew.Tick += new System.EventHandler(this.timRenew_Tick);
            // 
            // frmObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 311);
            this.Controls.Add(this.lvObjects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmObjects";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Объекты окружения";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmObjects_FormClosed);
            this.Load += new System.EventHandler(this.frmObjects_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvObjects;
        private System.Windows.Forms.ColumnHeader clName;
        private System.Windows.Forms.ColumnHeader clType;
        private System.Windows.Forms.Timer timRenew;
    }
}