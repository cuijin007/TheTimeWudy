using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars;

namespace TimeMDev
{
    public partial class SetShortCutForm : Form
    {
        Bar topBar;
        Keys keys1, keys2;
        int count = 0;
        public SetShortCutForm(Bar topBar)
        {
            InitializeComponent();
            this.topBar = topBar;
            this.InitMainCombo();
            this.mainCombo.SelectedIndex = 0;
            this.InitSubItemCombo();
            this.subCombo.SelectedIndex = 0;
            this.SetOldKey();
        }

        private void addShortCut_Click(object sender, EventArgs e)
        {
            if (this.count == 1)
            {
                this.topBar.ItemLinks[this.mainCombo.SelectedIndex].Links[this.subCombo.SelectedIndex].ItemShortcut = new BarShortcut(keys1);
            }
            if (this.count == 2)
            {
                this.topBar.ItemLinks[this.mainCombo.SelectedIndex].Links[this.subCombo.SelectedIndex].ItemShortcut = new BarShortcut(keys1, keys2);
            }
        }

        private void clearShortCut_Click(object sender, EventArgs e)
        {
            this.topBar.ItemLinks[this.mainCombo.SelectedIndex].Links[this.subCombo.SelectedIndex].ItemShortcut = new BarShortcut();
        }

        private void InitMainCombo()
        {
            this.mainCombo.Items.Clear();
            for (int i = 0; i < this.topBar.ItemLinks.Count;i++)
            {
                this.mainCombo.Items.Add(this.topBar.ItemLinks[i].Caption);
            }
        }
        private void InitSubItemCombo()
        {
            this.subCombo.Items.Clear();
            for (int i = 0; i < this.topBar.ItemLinks.Count; i++)
            {
                this.subCombo.Items.Add(this.topBar.ItemLinks[this.mainCombo.SelectedIndex].Links[i].Caption);
            }
        }
        private void SetOldKey()
        {
            this.nowKey.Text=this.topBar.ItemLinks[this.mainCombo.SelectedIndex].Links[this.subCombo.SelectedIndex].ShortCutDisplayText;
        }
        private void SetShortCutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.count == 0)
            {
                keys1 = e.KeyCode;
                this.newKey.Text = keys1.ToString();
            }
            if (this.count == 1)
            {
                keys2 = e.KeyCode;
                this.newKey.Text = keys1.ToString() + "+" + keys2.ToString();
            }
            this.count++;
            if (this.count == 1)
            {
                this.count = 0;
            }
        }

        private void SetShortCutForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.count = 0;
        }

        private void SetShortCutForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void mainCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetOldKey();
        }
    }
}
