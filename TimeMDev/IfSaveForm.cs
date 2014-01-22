using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.FileReadWriteFloder;

namespace TimeMDev
{
    public partial class IfSaveForm : Form
    {
        TimeLineReadWrite timeLineReadWrite;
        string originalPath;
        public IfSaveForm(TimeLineReadWrite timeLineReadWrite,string originalPath)
        {
            InitializeComponent();
            this.timeLineReadWrite = timeLineReadWrite;
            this.originalPath = originalPath;
        }

        private void save_Click(object sender, EventArgs e)
        {
            if(this.originalPath.EndsWith(".srt"))
            {
                this.timeLineReadWrite.Write(new FileWriteSrt(SetSaveEncoding.GetSaveSubEncoding(), this.originalPath));
            }
            if (this.originalPath.EndsWith(".ass"))
            {
                this.timeLineReadWrite.Write(new FileWriteAss(SetSaveEncoding.GetSaveSubEncoding(), this.originalPath));
            }
            this.timeLineReadWrite.DeleteBuf(this.timeLineReadWrite.filePath);
            this.Close();
            this.DialogResult=DialogResult.OK;
        }

        private void notSave_Click(object sender, EventArgs e)
        {
            this.timeLineReadWrite.DeleteBuf(this.timeLineReadWrite.filePath);
            this.Close();
            this.DialogResult = DialogResult.No ;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
