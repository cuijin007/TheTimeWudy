using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev
{
    public partial class LoadTranslationForm : Form
    {
        LoadTranslationFormPara para;
        public LoadTranslationForm(LoadTranslationFormPara para)
        {
            InitializeComponent();
            this.para = para;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if (this.byLine.Checked)
            {
                this.para.isBlank = false;
            }
            if (this.byBlank.Checked)
            {
                this.para.isBlank = true;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
    public class LoadTranslationFormPara
    {
        public bool isBlank;
        public string path;
    }
}
