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
    public partial class FileCutForm : Form
    {
        TimeLineReadWrite timeLineReadWrite;
        public FileCutForm(TimeLineReadWrite timeLineReadWrite)
        {
            InitializeComponent();
            this.timeLineReadWrite = timeLineReadWrite;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            int count=0;
            try
            {
                count = Int32.Parse(this.fileCount.Text);
                timeLineReadWrite.Write(new FileWriteSegmentation(this.timeLineReadWrite.filePath, count, 0));
                MessageBox.Show("分割完毕");
            }
            catch
            {
                MessageBox.Show("输入有误");
            }
           
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
