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
    public partial class SetDelayTime : Form
    {
        double time = 0;
        public SetDelayTime()
        {
            InitializeComponent();
        }
        private void Init()
        {
            try
            {
                time = Double.Parse(Config.DefaultConfig["DelayTime"]);
            }
            catch
            {
 
            }
            if (time < 0)
            {
                this.radioMin.Checked = true;
                this.timeShow.Text = TimeLineReadWrite.TimeOut(-1 * time);
            }
            else
            {
                this.radioPlus.Checked = true;
                this.timeShow.Text = TimeLineReadWrite.TimeOut(time);
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            try
            {
                time = TimeLineReadWrite.TimeIn(this.timeShow.Text);
                Config.DefaultConfig["DelayTime"] = time.ToString();
            }
            catch
            {
                MessageBox.Show("格式不符合要求");
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
