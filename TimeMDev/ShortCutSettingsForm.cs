using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.ShortCut;
using System.Collections;

namespace TimeMDev
{
    public delegate void UpdateInformer();


    public partial class ShortCutSettingsForm : Form
    {

        private int id;
        private bool changed = false;
        private Keys curKey;
        public UpdateInformer update;
        private string[] captions;
        private Hashtable mapping;
        private String text;

        public Hashtable Caption2id
        {
            get
            {
                return mapping;
            }
            set
            {
                if (mapping == value)
                {
                    return;
                }
                mapping = value;
                CalculateCaptions();
            }
        }
        private void CalculateCaptions()
        {
            captions = new string[mapping.Count];
            int i = 0;
            foreach (DictionaryEntry entry in mapping)//initialize data for list
            {
                captions[i++] = ShortcutUtils.Entry2ListCaption(entry);
            }
            Array.Sort(captions);
        }
        public ShortCutSettingsForm()
        {
            InitializeComponent();
        }

        private void key_KeyDown(object sender, KeyEventArgs e)
        {
            curKey = (Keys)0;
            if(e.Alt){
                curKey |= Keys.Alt;
            }
            if(e.Control){
                curKey |= Keys.Control;
            }
            if(e.Shift){
                curKey |= Keys.Shift;
            }

            curKey |= e.KeyCode;

            this.keyInp.Text = ShortcutUtils.readableShortcut(curKey);
        }

        private void ShortCutSettingsForm_Load(object sender, EventArgs e)
        {
            this.shortCutsLst.Items.AddRange(this.captions);
        }

        private void shortCutsLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!shortCutsLst.Text.Equals(""))
            {
                text = shortCutsLst.Text;
                id = (int)this.Caption2id[ShortcutUtils.Caption2Hashkey(text)];
                Keys key = ShortCuts.Get(id);
                string shortcut = ShortcutUtils.readableShortcut(key);
                if (key == (Keys)0)
                {
                    shortcut = "无";
                }
                this.keyInp.Text = shortcut;
                this.keyInp.Focus();
            }
        }

        
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (ShortCuts.Exist(curKey))
            {
                MessageBox.Show("快捷键冲突");
                this.keyInp.Text = "";
                this.keyInp.Focus();
                return;
            }
            ShortCuts.Change(id, curKey);

            CalculateCaptions();
            this.shortCutsLst.Items.Clear();
            this.shortCutsLst.Items.AddRange(captions);

            changed = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            ShortCuts.Write();
            changed = false;
        }

        private void ShortCutSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (changed)
            {
                this.saveBtn_Click(null, null);
                update();
            }
        }

    }
}
