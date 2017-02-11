using System;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using TinyLisp.Objects;

namespace TinyLisp
{
    public partial class frmMain : Form
    {
        private Parser myParser;

        private Thread executingThread;
        private BaseFunctions.OutputFunction AddTextToTextBox;

        public delegate void VoidFunc();
        public static VoidFunc ShowGraphicsForm;
        private VoidFunc ExecutionEnded;

        public static VoidFunc BeginUserInput;
        public static string EnteredString;

        private frmObjects ObjectsForm = null;

        private int SplittedSize;

        const int TAB_SIZE = 4;
        
        const string PROGRAM_NAME = "TinyLisp";

        #region Callbacks
        public void ShowGraphics()
        {
            TurtlesManager.GraphicForm.Show(this);
        }

        private void AddText(string Text)
        {
            tbResult.Text += Text;
            tbResult.SelectionStart = tbResult.Text.Length;
            tbResult.ScrollToCaret();
        }

        private void BeginInput()
        {
            tbResult.BackColor = Color.PaleGoldenrod;
            tbResult.ReadOnly = false;
            EnteredString = "";
            AddText("\r\n");
            tbResult.Focus();
        }

        private void EndInput()
        {
            tbResult.BackColor = Color.White;
            tbResult.ReadOnly = true;
            if(tbResult.Lines.Length > 0)
                EnteredString = tbResult.Lines[tbResult.Lines.Length - 1].Trim();
            AddText("\r\n");
            BaseFunctions.InputCompleted.Set();
        }

        private void WriteToOutput(string Text)
        {
            this.Invoke(AddTextToTextBox, Text);
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void StartExecution(object Code)
        {
            try
            {
                ListObject parsedList = myParser.Parse((string)Code);
                LispEnvironment env = LispEnvironment.MainEnvironment;
                parsedList.Eval(env, null);
                if (env.LastResult != null)
                    WriteToOutput(env.LastResult.ToString());
            }
            catch (ThreadAbortException)
            {
                MessageBox.Show("Выполнение программы было прервано", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception e)
            {
                string errorMessage = "В процессе выполнения программы произошла ошибка.\nВыполнение будет прервано.\n\nПричина: {0}";
                MessageBox.Show(String.Format(errorMessage, e.Message), "Ошибка в программе", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GC.Collect();
            this.Invoke(ExecutionEnded);
        }

        private void ExecutionCompleted()
        {
            tsbRun.Enabled = true;
            tsbStop.Enabled = false;
            this.Text = PROGRAM_NAME;
            if (tbResult.ReadOnly == false)
                EndInput();
        }

        private void ExecuteProgram()
        {
            bool threadIsFree = executingThread == null || executingThread.ThreadState != ThreadState.Running;
            if (rtbSource.Text.Length > 0 && threadIsFree)
            {
                this.Text = String.Format("{0} [Выполнение программы]", PROGRAM_NAME);
                tsbRun.Enabled = false;
                tsbStop.Enabled = true;
                executingThread = new Thread(new ParameterizedThreadStart(StartExecution));
                executingThread.Start(rtbSource.Text);
            }
        }

        private void SaveSplittedPosition()
        {
            int splitterPos = splSplitter.SplitPosition;
            SplittedSize = pnlContainer.Height - splitterPos - splSplitter.Height;
        }

        private void BindDelegates()
        {
            this.AddTextToTextBox = AddText;
            ShowGraphicsForm = ShowGraphics;
            ExecutionEnded = ExecutionCompleted;
            BeginUserInput = BeginInput;
            BaseFunctions.Output = WriteToOutput;
        }

        private void InitializeEnvironment()
        {
            LispEnvironment.MainEnvironment = new LispEnvironment();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = PROGRAM_NAME;
            myParser = new Parser();
            BindDelegates();
            InitializeEnvironment();
            InitializeColorizer();
            SaveSplittedPosition();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                ExecuteProgram();
            }
            else if (e.KeyData == Keys.F2)
            {
                tbResult.Clear();
            }
        }

        #region Menu commands
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteProgram();
        }

        private void ClearOutput()
        {
            tbResult.Clear();
        }

        private void очиститьВыводToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearOutput();
        }

        private void PrepareForNewFile()
        {
            InitializeEnvironment();
            rtbSource.Rtf = "";
            if (TurtlesManager.GraphicForm != null)
                TurtlesManager.GraphicForm.Hide();
        }

        private void NewFile()
        {
            if (rtbSource.Modified)
            {
                if (MessageBox.Show("Сохранить изменения в текущем файле?", "Новый файл", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
            rtbSource.Clear();
            PrepareForNewFile();
        }

        private void OpenFile()
        {
            if (ofdOpenProgram.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofdOpenProgram.FileName;
                string Contents = File.ReadAllText(fileName);
                PrepareForNewFile();
                rtbSource.Text = Contents;
                rtbSource.SuspendLayout();
                ColorizeTextBox();
                rtbSource.ResumeLayout();
                rtbSource.Modified = false;
            }
        }

        private void SaveFile()
        {
            bool canSave = rtbSource.Modified;
            if (canSave)
            {
                if (sfdSaveProgram.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = sfdSaveProgram.FileName;
                    string Contents = rtbSource.Text;
                    File.WriteAllText(fileName, Contents, Encoding.UTF8);
                    rtbSource.Modified = false;
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        #endregion

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            ClearOutput();
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            ExecuteProgram();
        }

        private void tsbStop_Click(object sender, EventArgs e)
        {
            if (executingThread != null && executingThread.ThreadState != ThreadState.Aborted)
            {
                executingThread.Abort();
                ExecutionEnded();
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void splSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveSplittedPosition();
        }

        private void tbResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbResult.ReadOnly == false)
            {
                if (e.KeyChar == '\r')
                {
                    EndInput();
                }
            }
        }

        private void новоеОкружениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Создание нового окружения удалит определения всех пользовательских объектов из памяти.\nПродолжить?";
            if (MessageBox.Show(message, "Новое окружение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LispEnvironment.MainEnvironment = null;
                LispEnvironment.MainEnvironment = new LispEnvironment();
                GC.Collect();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutMessage = String.Format("{0} v. {1}", PROGRAM_NAME, Application.ProductVersion);
            MessageBox.Show(aboutMessage, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowEnvObjects()
        {
            if (ObjectsForm == null || ObjectsForm.IsDisposed)
            {
                ObjectsForm = new frmObjects();
                ObjectsForm.Show(this);
            }
        }

        private void показатьОбъектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEnvObjects();
        }

        private void tsbEnvObjects_Click(object sender, EventArgs e)
        {
            ShowEnvObjects();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            очиститьВыводToolStripMenuItem_Click(null, null);
        }
    }
}
