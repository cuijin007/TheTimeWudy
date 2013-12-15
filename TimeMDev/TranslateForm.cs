using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.HandleRecord;

namespace TimeMDev
{
    public partial class TranslateForm : Form
    {
        TranslateFormPara translateFormPara;
        public TranslateForm(TranslateFormPara translateFormPara)
        {
            InitializeComponent();
            this.translateFormPara = translateFormPara;
            this.nowTime.Text = TimeLineReadWrite.TimeOutAss(translateFormPara.time);
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.SetPara();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.SetPara();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void SetPara()
        {
            this.translateFormPara.time = TimeLineReadWrite.TimeInAss(this.nowTime.Text);
            if (this.selectedLine.Checked)
            {
                this.translateFormPara.state = TranslateFormPara.SELECTEDLINE;
            }
            if (this.allLine.Checked)
            {
                this.translateFormPara.state = TranslateFormPara.ALLLINE;
            }
            if (this.betweenCheckdLine.Checked)
            {
                this.translateFormPara.state = TranslateFormPara.BETWEENSELECTED;
            }
            if (this.afterThisLine.Checked)
            {
                this.translateFormPara.state = TranslateFormPara.AFTERSELECTED;
            }
        }
    }
    public class TranslateFormPara
    {
        public double time;
        public int state;
        public const int SELECTEDLINE= 0;
        public const int ALLLINE =1;
        public const int BETWEENSELECTED= 2;
        public const int AFTERSELECTED =3;
        public bool isConfirm=false;
    }
}
