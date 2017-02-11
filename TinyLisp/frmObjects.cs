using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TinyLisp.Objects;

namespace TinyLisp
{
    public partial class frmObjects : Form
    {
        private string[] langObjects = { 
                "NumberObject", "StringObject", "LogicObject", 
                "SymbolObject", "LambdaObject", "ListObject" 
        };

        private string[] objectsDesc = { 
            "Число", "Строка", "Логика", 
            "Символ", "Лямбда", "Список"
        };

        long prevObjectsHashSum;

        public frmObjects()
        {
            InitializeComponent();
        }

        private string GetObjectDescription(BaseObject anObject)
        {
            string description = "Другое";
            string objType = anObject.ToString();
            System.Diagnostics.Debug.Print(objType);
            for (int i = 0; i < langObjects.Length; i++)
            {
                if (objType == langObjects[i])
                {
                    description = objectsDesc[i];
                    break;
                }
            }
            return description;
        }

        private long calculateObjectsHashSum()
        {
            long objectsHashSum = 0;
            foreach (BaseObject global in LispEnvironment.globals.Values)
            {
                objectsHashSum += global.GetHashCode();
            }
            return objectsHashSum;
        }

        private void UpdateObjectsList()
        {
            long nowSum = calculateObjectsHashSum();
            if (nowSum != prevObjectsHashSum)
            {
                lvObjects.Items.Clear();
                foreach (string objectKey in LispEnvironment.globals.Keys)
                {
                    BaseObject item = LispEnvironment.globals[objectKey];
                    ListViewItem lItem = new ListViewItem(new string[] { objectKey, GetObjectDescription(item) });
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
            UpdateObjectsList();
            prevObjectsHashSum = 0;
            timRenew.Enabled = true;
        }
    }
}
