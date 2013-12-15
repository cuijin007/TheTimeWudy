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
    public partial class ExtendForm : Form
    {
        ExtendFormPara extendFormPara;
        public ExtendForm(ExtendFormPara extendFormPara)
        {
            InitializeComponent();
            this.extendFormPara=extendFormPara;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.extendFormPara.time=TimeLineReadWrite.TimeInAss(this.nowTime.Text);
            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            this.Close();
        }
    }

    public class ExtendFormPara
    {
        public double time;
    }
}
