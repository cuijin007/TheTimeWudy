using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.ShortCut;

namespace TimeMDev
{
    public delegate void UpdateInformer();

    public partial class ShortCutSettingsForm : Form
    {

        private int id;
        private bool changed = false;
        private Keys curKey;
        public UpdateInformer update;
        
        public ShortCutSettingsForm()
        {
            InitializeComponent();
            shortcuts = ShortCuts.getShortCuts();
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

            this.keyInp.Text = this.readableShortcut(curKey);
        }

        private void ShortCutSettingsForm_Load(object sender, EventArgs e)
        {
            this.shortCutsLst.Items.AddRange(this.captions);
        }

        private void shortCutsLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = (int)this.Caption2id[shortCutsLst.Text];
            Keys key = this.shortcuts.getShortCut(id);
            string shortcut = this.readableShortcut(key);
            if(key == (Keys)0)
            {
                shortcut = "无";
            }
            this.keyInp.Text = shortcut;
        }

        private String readableShortcut(Keys key)
        {
            return key.ToString().Replace(",", " +");
        }
        private void okBtn_Click(object sender, EventArgs e)
        {
            shortcuts.changeKey(id, curKey);
            changed = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            ShortCuts.write(shortcuts);
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
