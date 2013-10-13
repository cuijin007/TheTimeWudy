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
    public partial class AddMutiLinesForm : Form
    {
        AddMutiLinesParameter addMutiLinesParameter;
        public AddMutiLinesForm(AddMutiLinesParameter addMutiLinesParameter)
        {
            InitializeComponent();
            this.addMutiLinesParameter = addMutiLinesParameter;
            this.addMutiLinesParameter.isConfirm = false;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.addMutiLinesParameter.lineCount = (int)this.lineCount.Value;
                this.addMutiLinesParameter.timeSpiltLength = TimeLineReadWrite.TimeIn(this.timeSpiltText.Text);
                if (this.blankLine.Checked)
                {
                    addMutiLinesParameter.isBlank = true;
                }
                if (this.nonBlankLine.Checked)
                {
                    addMutiLinesParameter.isBlank = false;
                }
                addMutiLinesParameter.isConfirm = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("请查卡是否格式错误");
            }
            
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fristTimeLength_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fristTimeLength.Checked)
            {
                this.timeSpiltText.Enabled = true;
            }
            else
            {
                this.timeSpiltText.Enabled = false;
            }
        }
    }
}
