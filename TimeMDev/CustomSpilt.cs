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
    public partial class CustomSpilt : Form
    {
        SpiltParameter spiltParameter;
        public CustomSpilt(SpiltParameter spiltParameter)
        {
            InitializeComponent();
            this.spiltParameter = spiltParameter;
            this.pictureShowCut.Capture = true;
            this.Init();
            this.pictureShowCut.DrawPictureView();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.spiltParameter.confirm = true;
            this.pictureShowCut.SpiltWord(out this.spiltParameter.afterSpiltFristLine, out this.spiltParameter.afterSpiltSecondLine);
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.spiltParameter.confirm = false;
            this.Close();
        }
        private void Init()
        {
            string timeLengthStr = TimeLineReadWrite.TimeOut(this.spiltParameter.timeLength);
            this.timeLength.Text = timeLengthStr;
            string chinese = "";
            string english = "";
            CCHandle.SpiltRule(this.spiltParameter.beforeSpilt, out chinese, out english);
            chinese = chinese.Replace("\r\n","");
            english = english.Replace("\r\n","");
            this.pictureShowCut.Capture = true;
            if (this.spiltParameter.beforeSpilt.StartsWith(chinese))
            {
                this.pictureShowCut.Init(chinese, english);
            }
            else
            {
                this.pictureShowCut.Init(english, chinese);
            }
        }
    }
}
