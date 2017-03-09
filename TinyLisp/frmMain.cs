using System;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TinyLisp.Objects;

namespace TinyLisp
{
    /// <summary>
    /// Главное окно программы
    /// </summary>
    public partial class frmMain : Form, IUICallbacks
    {
        private Thread executionThread;

        /// <summary>
        /// Последняя введенная пользователем строка
        /// </summary>
        public static string LastEnteredString { get; private set; }

        private frmObjects ObjectsForm = null;

        private int SplittedSize;

        const int TAB_SIZE = 4;
        
        const string APP_NAME = "TinyLisp";

        /// <summary>
        /// Функции взаимодействия с интерфейсом
        /// </summary>
        public static IUICallbacks UI { get; private set; }

        #region Функции обратного вызова
        public void ShowGraphics()
        {
            this.Invoke(new Action(() => TurtlesManager.GraphicForm.Show(this) ));
        }

        public void AppendOutputText(string text)
        {
            this.Invoke(new Action(() => {
                tbResult.Text += text;
                tbResult.SelectionStart = tbResult.Text.Length;
                tbResult.ScrollToCaret();
            }));
        }

        public void BeginInput()
        {
            this.Invoke(new Action(() => {
                tbResult.BackColor = Color.PaleGoldenrod;
                tbResult.ReadOnly = false;
                LastEnteredString = String.Empty;
                AppendOutputText(Environment.NewLine);
                tbResult.Focus();
            }));
        }
        #endregion

        private void EndInput()
        {
            tbResult.BackColor = Color.White;
            tbResult.ReadOnly = true;
            if(tbResult.Lines.Length > 0)
                LastEnteredString = tbResult.Lines[tbResult.Lines.Length - 1].Trim();
            AppendOutputText(Environment.NewLine);
            BaseFunctions.InputCompleted.Set();
        }

        private void StartExecution(object code)
        {
            try
            {
                Parser codeParser = new Parser();
                ListObject parsedList = codeParser.Parse((string)code);
                LispEnvironment env = LispEnvironment.Current;
                parsedList.Eval(env, null);

                if (env.LastResult != null)
                {
                    string execResult = env.LastResult.ToString();
                    AppendOutputText(execResult);
                }
            }
            catch (ThreadAbortException)
            {
                MessageBox.Show("Выполнение программы было прервано", "Внимание", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception e)
            {
                string errorMessage = "В процессе выполнения программы произошла ошибка.\n" +
                                      "Выполнение будет прервано.\n\nПричина: {0}";
                MessageBox.Show(String.Format(errorMessage, e.Message), "Ошибка в программе", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GC.Collect();
            this.Invoke(new Action(() => ExecutionCompleted()));
        }

        private void ExecutionCompleted()
        {
            tsbRun.Enabled = true;
            tsbStop.Enabled = false;
            this.Text = APP_NAME;
            if (tbResult.ReadOnly == false)
                EndInput();
        }

        private void ExecuteProgram()
        {
            bool threadIsFree = executionThread == null || 
                                executionThread.ThreadState != ThreadState.Running;

            if (rtbSource.Text.Length > 0 && threadIsFree)
            {
                this.Text = String.Format("{0} [Выполнение программы]", APP_NAME);
                tsbRun.Enabled = false;
                tsbStop.Enabled = true;
                executionThread = new Thread(new ParameterizedThreadStart(StartExecution));
                executionThread.Start(rtbSource.Text);
            }
        }

        private void SaveSplittedPosition()
        {
            int splitterPos = splSplitter.SplitPosition;
            SplittedSize = pnlContainer.Height - splitterPos - splSplitter.Height;
        }

        private void InitializeEnvironment()
        {
            LispEnvironment.Current = new LispEnvironment();
        }

        private void ClearOutput()
        {
            tbResult.Clear();
        }

        private void ShowEnvObjects()
        {
            if (ObjectsForm == null || ObjectsForm.IsDisposed)
            {
                ObjectsForm = new frmObjects();
                ObjectsForm.Show(this);
            }
        }

        #region Действия с файлами
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
                DialogResult result = MessageBox.Show("Сохранить изменения в текущем файле?", "Новый файл", 
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
            rtbSource.Clear();
            PrepareForNewFile();
        }

        private void OpenFile()
        {
            if (ofdOpenProgram.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofdOpenProgram.FileName;
                string Contents = File.ReadAllText(fileName);

                PrepareForNewFile();
                rtbSource.Text = Contents;
                tbResult.Focus();
                ColorizeTextBox();
                rtbSource.Focus();
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
        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;
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

        #region Команды меню
        private void newScriptItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }
        
        private void exitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void executeItem_Click(object sender, EventArgs e)
        {
            ExecuteProgram();
        }

        private void clearOutputItem_Click(object sender, EventArgs e)
        {
            ClearOutput();
        }

        private void openScriptItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveScriptItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void newEnvItem_Click(object sender, EventArgs e)
        {
            string message = "Создание нового окружения удалит определения " +
                             "всех пользовательских объектов из памяти.\nПродолжить?";
            DialogResult result = MessageBox.Show(message, "Новое окружение",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LispEnvironment.Current = null;
                LispEnvironment.Current = new LispEnvironment();
                GC.Collect();
            }
        }

        private void aboutItem_Click(object sender, EventArgs e)
        {
            string aboutMessage = String.Format("{0} v. {1}", APP_NAME, Application.ProductVersion);
            MessageBox.Show(aboutMessage, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showObjectsItem_Click(object sender, EventArgs e)
        {
            ShowEnvObjects();
        }
        #endregion

        #region Кнопки панели инструментов
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
            if (executionThread != null && executionThread.ThreadState != ThreadState.Aborted)
            {
                executionThread.Abort();
                ExecutionCompleted();
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void tsbEnvObjects_Click(object sender, EventArgs e)
        {
            ShowEnvObjects();
        }

        private void clearOutput_Click(object sender, EventArgs e)
        {
            clearOutputItem_Click(null, null);
        }
        #endregion

        public frmMain()
        {
            UI = this;
            InitializeComponent();
        }
    }
}
