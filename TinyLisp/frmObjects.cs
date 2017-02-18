using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TinyLisp.Objects;

namespace TinyLisp
{
    /// <summary>
    /// Окно отображения объектов в окружении интерпретатора
    /// </summary>
    public partial class frmObjects : Form
    {
        // Описания типов объектов
        private Dictionary<string, string> typesDesc;

        long prevObjectsHashSum;

        private void InitializeDescriptionsDictionary()
        {
            typesDesc = new Dictionary<string,string> { 
                { "NumberObject", "Число" },
                { "StringObject", "Строка" },
                { "LogicObject", "Логика" },
                { "SymbolObject", "Символ" },
                { "LambdaObject", "Лямбда" },
                { "ListObject", "Список" }
            };
        }

        private string GetObjectDescription(BaseObject anObject)
        {
            string typeName = anObject.ToString();
            if (typesDesc.ContainsKey(typeName))
                return typesDesc[typeName];
            else
                return "Другое";
        }

        private long CalculateObjectsHashSum()
        {
            long objectsHashSum = 0;
            foreach (BaseObject global in LispEnvironment.Variables.Values)
            {
                objectsHashSum += global.GetHashCode();
            }
            return objectsHashSum;
        }

        private void UpdateObjectsList()
        {
            long nowSum = CalculateObjectsHashSum();
            if (nowSum != prevObjectsHashSum)
            {
                // Объекты окружения изменились - отобразить
                lvObjects.Items.Clear();
                foreach (string objectKey in LispEnvironment.Variables.Keys)
                {
                    BaseObject item = LispEnvironment.Variables[objectKey];
                    ListViewItem lItem = new ListViewItem(new string[] { 
                        objectKey, GetObjectDescription(item) 
                    });
                    lvObjects.Items.Add(lItem);
                }
                prevObjectsHashSum = nowSum;
            }
        }

        private void timRenew_Tick(object sender, EventArgs e)
        {
            UpdateObjectsList();
        }

        private void frmObjects_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            timRenew.Enabled = false;
        }

        private void frmObjects_Load(object sender, EventArgs e)
        {
            InitializeDescriptionsDictionary();
            UpdateObjectsList();
            prevObjectsHashSum = 0;
            timRenew.Enabled = true;
        }

        public frmObjects()
        {
            InitializeComponent();
        }
    }
}
