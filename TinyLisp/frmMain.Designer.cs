namespace TinyLisp
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.фвйлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.программаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.очиститьВыводToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miSlowPainting = new System.Windows.Forms.ToolStripMenuItem();
            this.окружениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьОбъектыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.новоеОкружениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.ofdOpenProgram = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveProgram = new System.Windows.Forms.SaveFileDialog();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClearOutput = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEnvObjects = new System.Windows.Forms.ToolStripButton();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.splSplitter = new System.Windows.Forms.Splitter();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.msMain.SuspendLayout();
            this.tsTools.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фвйлToolStripMenuItem,
            this.программаToolStripMenuItem,
            this.окружениеToolStripMenuItem,
            this.toolStripMenuItem4});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(699, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "&Файл";
            // 
            // фвйлToolStripMenuItem
            // 
            this.фвйлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.фвйлToolStripMenuItem.Name = "фвйлToolStripMenuItem";
            this.фвйлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.фвйлToolStripMenuItem.Text = "&Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.NewDocumentHS;
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.новыйToolStripMenuItem.Text = "&Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.openHS;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.открытьToolStripMenuItem.Text = "О&ткрыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.saveHS;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.сохранитьToolStripMenuItem.Text = "Со&хранить...";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.выходToolStripMenuItem.Text = "В&ыход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // программаToolStripMenuItem
            // 
            this.программаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выполнитьToolStripMenuItem,
            this.toolStripMenuItem3,
            this.очиститьВыводToolStripMenuItem,
            this.toolStripMenuItem2,
            this.miSlowPainting});
            this.программаToolStripMenuItem.Name = "программаToolStripMenuItem";
            this.программаToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.программаToolStripMenuItem.Text = "Прог&рамма";
            // 
            // выполнитьToolStripMenuItem
            // 
            this.выполнитьToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.PlayHS;
            this.выполнитьToolStripMenuItem.Name = "выполнитьToolStripMenuItem";
            this.выполнитьToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.выполнитьToolStripMenuItem.Text = "Вы&полнить";
            this.выполнитьToolStripMenuItem.Click += new System.EventHandler(this.выполнитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(270, 6);
            // 
            // очиститьВыводToolStripMenuItem
            // 
            this.очиститьВыводToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.Erase;
            this.очиститьВыводToolStripMenuItem.Name = "очиститьВыводToolStripMenuItem";
            this.очиститьВыводToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.очиститьВыводToolStripMenuItem.Text = "О&чистить вывод";
            this.очиститьВыводToolStripMenuItem.Click += new System.EventHandler(this.очиститьВыводToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(270, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // miSlowPainting
            // 
            this.miSlowPainting.Checked = true;
            this.miSlowPainting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miSlowPainting.Name = "miSlowPainting";
            this.miSlowPainting.Size = new System.Drawing.Size(273, 22);
            this.miSlowPainting.Text = "Медленное &рисование (Черепашка)";
            this.miSlowPainting.Visible = false;
            // 
            // окружениеToolStripMenuItem
            // 
            this.окружениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.показатьОбъектыToolStripMenuItem,
            this.toolStripMenuItem5,
            this.новоеОкружениеToolStripMenuItem});
            this.окружениеToolStripMenuItem.Name = "окружениеToolStripMenuItem";
            this.окружениеToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.окружениеToolStripMenuItem.Text = "Окруже&ние";
            // 
            // показатьОбъектыToolStripMenuItem
            // 
            this.показатьОбъектыToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.AppWindow;
            this.показатьОбъектыToolStripMenuItem.Name = "показатьОбъектыToolStripMenuItem";
            this.показатьОбъектыToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.показатьОбъектыToolStripMenuItem.Text = "Пока&зать объекты...";
            this.показатьОбъектыToolStripMenuItem.Click += new System.EventHandler(this.показатьОбъектыToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 6);
            // 
            // новоеОкружениеToolStripMenuItem
            // 
            this.новоеОкружениеToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.NewWindow;
            this.новоеОкружениеToolStripMenuItem.Name = "новоеОкружениеToolStripMenuItem";
            this.новоеОкружениеToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.новоеОкружениеToolStripMenuItem.Text = "Новое окру&жение";
            this.новоеОкружениеToolStripMenuItem.Click += new System.EventHandler(this.новоеОкружениеToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem,
            this.toolStripMenuItem6,
            this.оПрограммеToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem4.Text = "&?";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Image = global::TinyLisp.Properties.Resources.Help;
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(155, 6);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.оПрограммеToolStripMenuItem.Text = "&О программе...";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // rtbSource
            // 
            this.rtbSource.AutoWordSelection = true;
            this.rtbSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbSource.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbSource.Location = new System.Drawing.Point(0, 0);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbSource.Size = new System.Drawing.Size(695, 323);
            this.rtbSource.TabIndex = 1;
            this.rtbSource.Text = "";
            this.rtbSource.TextChanged += new System.EventHandler(this.rtbSource_TextChanged);
            this.rtbSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbSource_KeyPress);
            this.rtbSource.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rtbSource_PreviewKeyDown);
            // 
            // ofdOpenProgram
            // 
            this.ofdOpenProgram.Filter = "Текстовые файлы|*.txt";
            // 
            // sfdSaveProgram
            // 
            this.sfdSaveProgram.Filter = "Текстовые файлы|*.txt";
            // 
            // tsTools
            // 
            this.tsTools.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbRun,
            this.tsbStop,
            this.toolStripSeparator3,
            this.tsbClearOutput,
            this.toolStripSeparator2,
            this.tsbEnvObjects});
            this.tsTools.Location = new System.Drawing.Point(0, 24);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(699, 43);
            this.tsTools.TabIndex = 3;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(40, 40);
            this.tsbNew.Text = "Новый файл";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(40, 40);
            this.tsbOpen.Text = "Открыть файл";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(40, 40);
            this.tsbSave.Text = "Сохранить файл";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // tsbRun
            // 
            this.tsbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbRun.Image")));
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(40, 40);
            this.tsbRun.Text = "Запуск";
            this.tsbRun.Click += new System.EventHandler(this.tsbRun_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Enabled = false;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(40, 40);
            this.tsbStop.Text = "Остановка";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // tsbClearOutput
            // 
            this.tsbClearOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearOutput.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearOutput.Image")));
            this.tsbClearOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearOutput.Name = "tsbClearOutput";
            this.tsbClearOutput.Size = new System.Drawing.Size(40, 40);
            this.tsbClearOutput.Text = "Очистить вывод";
            this.tsbClearOutput.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // tsbEnvObjects
            // 
            this.tsbEnvObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEnvObjects.Image = ((System.Drawing.Image)(resources.GetObject("tsbEnvObjects.Image")));
            this.tsbEnvObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnvObjects.Name = "tsbEnvObjects";
            this.tsbEnvObjects.Size = new System.Drawing.Size(40, 40);
            this.tsbEnvObjects.Text = "Объекты окружения";
            this.tsbEnvObjects.Click += new System.EventHandler(this.tsbEnvObjects_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlContainer.Controls.Add(this.splSplitter);
            this.pnlContainer.Controls.Add(this.tbResult);
            this.pnlContainer.Controls.Add(this.rtbSource);
            this.pnlContainer.Location = new System.Drawing.Point(0, 70);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(699, 485);
            this.pnlContainer.TabIndex = 4;
            // 
            // splSplitter
            // 
            this.splSplitter.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splSplitter.Location = new System.Drawing.Point(0, 323);
            this.splSplitter.Margin = new System.Windows.Forms.Padding(5);
            this.splSplitter.MinExtra = 50;
            this.splSplitter.MinSize = 50;
            this.splSplitter.Name = "splSplitter";
            this.splSplitter.Size = new System.Drawing.Size(695, 3);
            this.splSplitter.TabIndex = 5;
            this.splSplitter.TabStop = false;
            this.splSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splSplitter_SplitterMoved);
            // 
            // tbResult
            // 
            this.tbResult.BackColor = System.Drawing.SystemColors.Window;
            this.tbResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResult.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbResult.Location = new System.Drawing.Point(0, 323);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(695, 158);
            this.tbResult.TabIndex = 4;
            this.tbResult.TabStop = false;
            this.tbResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbResult_KeyPress);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 551);
            this.Controls.Add(this.tsTools);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.pnlContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.msMain;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Интерпретатор";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsTools.ResumeLayout(false);
            this.tsTools.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem фвйлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem программаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выполнитьToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog ofdOpenProgram;
        private System.Windows.Forms.ToolStripMenuItem очиститьВыводToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miSlowPainting;
        private System.Windows.Forms.SaveFileDialog sfdSaveProgram;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Splitter splSplitter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbEnvObjects;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem окружениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новоеОкружениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьОбъектыToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbClearOutput;
    }
}

