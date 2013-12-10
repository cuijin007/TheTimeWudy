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
    public partial class SaveAsForm : Form
    {
        TimeLineReadWrite timeLineReadWrite;
        public SaveAsForm(TimeLineReadWrite timeLineReadWrite)
        {
            InitializeComponent();
            this.Init();
            this.timeLineReadWrite = timeLineReadWrite;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            string buf=this.timeLineReadWrite.filePath;
           SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "字幕文件|*.srt;*.ass";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if(dialog.FileName.EndsWith(".srt"))
                {
                    this.timeLineReadWrite.filePath = dialog.FileName;
                    this.timeLineReadWrite.Write(new FileWriteSrt(this.GetEncoding()));
                }
                if (dialog.FileName.EndsWith(".ass"))
                {
                    this.timeLineReadWrite.filePath = dialog.FileName;
                    this.timeLineReadWrite.Write(new FileWriteAss(this.GetEncoding()));
                }
            }
            this.timeLineReadWrite.filePath = buf;
            MessageBox.Show("保存成功");
            this.Close();
        }
        /// <summary>
        /// 初始化comboBox的内容
        /// </summary>
        private void Init()
        {
            this.comboEncodingChoose.SelectedIndex = 0;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 获得选择的编码
        /// </summary>
        /// <returns></returns>
        private Encoding GetEncoding()
        {
            if (this.comboEncodingChoose.Text.Equals("UNICODE"))
            {
                return Encoding.Unicode;
            }
            if (this.comboEncodingChoose.Text.Equals("UTF-8"))
            {
                return Encoding.UTF8;
            }
            return Encoding.Default;
        }
    }
}
