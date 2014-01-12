using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.FileReadWriteFloder;
using System.IO;

namespace TimeMDev
{
    public partial class ExportForm : Form
    {
        AssInfo assInfo = new AssInfo();
        TimeLineReadWrite timeLineReadWrite;
        Encoding encoding;
        bool assChange = false;
        string subOutName;
        public ExportForm(TimeLineReadWrite timeLineReadWrite,string name)
        {
            InitializeComponent();
            this.timeLineReadWrite = timeLineReadWrite;
            subOutName = name;
        }

        private void setANSI_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.SelectedIndices.Count; i++)
            {
                this.listViewShow.Items[this.listViewShow.SelectedIndices[i]].SubItems[1].Text = "ANSI";
            }
        }

        private void setUnicode_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.SelectedIndices.Count; i++)
            {
                this.listViewShow.Items[this.listViewShow.SelectedIndices[i]].SubItems[1].Text = "UNICODE";
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.Items.Count; i++)
            {
                this.listViewShow.Items[i].Checked = true;
            }
            this.listViewShow.Refresh();
        }

        private void selectedOther_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.Items.Count; i++)
            {
                if (!this.listViewShow.Items[i].Checked)
                {
                    this.listViewShow.Items[i].Checked = true;
                }
                else
                {
                    this.listViewShow.Items[i].Checked = false;
                }
            }
            this.listViewShow.Refresh();
        }

        private void setAss_Click(object sender, EventArgs e)
        {
            if ((new SetAssForm(this.assInfo)).ShowDialog() == DialogResult.OK)
            {
                this.assChange = true;
            }
        }

        /// <summary>
        /// 确认按钮，判断语言，判断中英文组合，判断编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.CheckedIndices.Count; i++)
            {
                Encoding encoding=Encoding.Default;
                int chinese=0;
                int english=0;
                string path=this.pathOut.Text;
                string[] spilt=new string[1];
                spilt[0]="\\";
                //string[] names = this.timeLineReadWrite.filePath.Split(spilt,StringSplitOptions.RemoveEmptyEntries);
                //path +=names[names.Length - 1];
                //path=path.Replace(".srt","");
                //path=path.Replace(".ass","");
                path += Path.GetFileNameWithoutExtension(this.subOutName);

                if (this.listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[1].Text.Equals("ANSI"))
                {
                    encoding = Encoding.Default;                    
                }
                if (this.listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[1].Text.Equals("UNICODE"))
                {
                    encoding = Encoding.Unicode;
                }
                if (this.listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[1].Text.Equals("UTF-8"))
                {
                    encoding = Encoding.UTF8;
                }
                if (listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[0].Text.Contains("英文"))
                {
                    english = 1;
                    path = path + this.englishEnd.Text;
                }
                if (listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[0].Text.Contains("简体"))
                {
                    chinese = 1;
                    path = path + this.chineseSEnd.Text;
                }
                if (listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[0].Text.Contains("繁体"))
                {
                    chinese = 2;
                    path = path + this.chineseTEnd.Text;
                }

                if (listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[0].Text.Contains("Srt"))
                {
                    this.timeLineReadWrite.Write(new FileWriteMutiLanguage(path + ".srt", chinese, english, 0));
                }
                if (listViewShow.Items[this.listViewShow.CheckedIndices[i]].SubItems[0].Text.Contains("Ass"))
                {
                    if (this.assChange)
                    {
                        this.timeLineReadWrite.Write(new FileWriteMutiLanguage(path + ".ass", chinese, english, 1,this.assInfo));
                    }
                    else
                    {
                        this.timeLineReadWrite.Write(new FileWriteMutiLanguage(path + ".ass", chinese, english, 1));
                    }
                }
            }
            MessageBox.Show("导出完成");
                
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setUTF8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.SelectedIndices.Count; i++)
            {
                this.listViewShow.Items[this.listViewShow.SelectedIndices[i]].SubItems[1].Text = "UTF-8";
            }
        }

        private void selectePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.pathOut.Text = fbd.SelectedPath+"\\";
            }
        }
    }
}
