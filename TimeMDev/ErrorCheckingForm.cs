using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.Notification;
using TimeMDev.HandleRecord;

namespace TimeMDev
{
    public partial class ErrorCheckingForm : Form
    {
        Form1 formMain;
        List<SingleSentence> listSingleSentence;
        public ErrorCheckingForm(Form1 formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            this.listSingleSentence = formMain.DataProcessGet.listSingleSentence;
        }

        private void checkAll_Click(object sender, EventArgs e)
        {
            this.listViewShow.Items.Clear();
            this.CheckAllFuntion();
        }


        private void closeForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listViewShow.Items.Count; i++)
            {
                this.listViewShow.Items[i].Selected = true;
            }
        }

        private void handleOverlap_Click(object sender, EventArgs e)
        {
            this.formMain.commandManage.CommandRun(new HandleOverLapRecord(this.formMain.DataProcessGet.listSingleSentence,this.formMain.listView1));
            this.formMain.listView1.YYRefresh();
        }
        /// <summary>
        /// 检查所有的字幕，找出有问题的部分
        /// </summary>
        private void CheckAllFuntion()
        {
            double compareBigTime = 0;
            double compareSmallTime=0;
            int linesCountNum = 0;
            int sentenceLength=0;
            try
            {
                compareBigTime = TimeLineReadWrite.TimeIn(this.timeLengthBigBox.Text);
                compareSmallTime = TimeLineReadWrite.TimeIn(this.timeLengthSmallBox.Text);
                linesCountNum = Int32.Parse(this.linesCountBox.Text);
                sentenceLength=Int32.Parse(this.textLengthBox.Text);
            }
            catch
            {
                MessageBox.Show("拼写格式有问题");
                return;
            }
            for (int i = 1; i < this.listSingleSentence.Count-1; i++)
            {
                if (this.textLengthCheck.Checked)
                {
                    this.CheckSentenceLength(i, listSingleSentence[i],sentenceLength);
                }
                if (this.startBigerCheck.Checked)
                {
                    this.CheckStartBiger(i, listSingleSentence[i]);
                }
                if (this.overLapCheck.Checked)
                {
                    this.CheckOverlap(i, this.listSingleSentence[i]);
                }
                if (this.emptyCheck.Checked)
                {
                    this.CheckEmpty(i, this.listSingleSentence[i]);
                }
                if (this.timeLengthBigCheck.Checked)
                {
                    this.CheckTimeLengthBig(i, this.listSingleSentence[i],compareBigTime);
                }
                if (this.timeLengthSmallCheck.Checked)
                {
                    this.CheckTimeLengthSmall(i, this.listSingleSentence[i], compareSmallTime);
                }
                if (this.linesCountCheck.Checked)
                {
                    this.CheckLinesCount(i, this.listSingleSentence[i],sentenceLength);
                }
                if (this.fullWidthSymbolCheck.Checked)
                {
                    this.CheckFullWidthSymbol(i, this.listSingleSentence[i]);
                }
                if (this.chineseContainsEnglishCheck.Checked)
                {
                    this.CheckSentenceEnglish(i, this.listSingleSentence[i]);
                }
            }
        }
        /// <summary>
        /// 检查中文中是否有英文
        /// </summary>
        /// <param name="index">序号</param>
        /// <param name="?">值</param>
        private void CheckSentenceEnglish(int index,SingleSentence sentence)
        {
            string chinese;
            string english2;
            List<string> english;
            
            //CCHandle.GetEnglishFromChinese(sentence.content, out english);//cuijin---修改于2014-5-15
            //if (english.Count > 1||(english.Count==1&&!english[0].Equals("")))
            //{
            //    this.AddNewItemInListview(index, "中文行中有英文", this.chineseContainEnglishColor.BackColor);
            //}
            CCHandle.SpiltRuleByEnter(sentence.content, out chinese, out english2);
            chinese = CCHandle.TrimEnterEnd(chinese);
            if (chinese.Length > 0)
            {
                CCHandle.GetEnglishFromChinese(chinese+"中文", out english);
                if (english.Count > 1 || (english.Count == 1 && !english[0].Equals("")))
                {
                    this.AddNewItemInListview(index, "中文行中有英文", this.chineseContainEnglishColor.BackColor);
                }
            }
            
        }
        /// <summary>
        /// 检查中文字符的长度
        /// </summary>
        private void CheckSentenceLength(int index,SingleSentence sentence,int length)
        {
            string chinese, english;
            CCHandle.SpiltRule(sentence.content, out chinese, out english);
            if (chinese.Length > length)
            {
                this.AddNewItemInListview(index, "长度超长", this.textLengthColor.BackColor);
            }
        }
        /// <summary>
        /// 插入一个新行
        /// </summary>
        /// <param name="index"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        private void AddNewItemInListview(int index, string content, Color color)
        {
            string[] str = new string[2];
            str[0] = index + "";
            str[1] = content;
            ListViewItem item = new ListViewItem(str);
            item.BackColor = color;
            this.listViewShow.Items.Add(item);
        }
        /// <summary>
        /// 开始时间大于结束时间
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        private void CheckStartBiger(int index,SingleSentence sentence)
        {
            if (sentence.startTime > sentence.endTime)
            {
                this.AddNewItemInListview(index, "开始时间大于结束时间", this.startBigerColor.BackColor);
            }
        }
        /// <summary>
        /// 检查是否有重叠
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        private void CheckOverlap(int index,SingleSentence sentence)
        {
            int count=0;
            for (int i = 0; i < this.listSingleSentence.Count; i++)
            {
                if (sentence.startTime > this.listSingleSentence[i].startTime && sentence.startTime < this.listSingleSentence[i].endTime)
                {
                    count++;
                }
                if (sentence.endTime > this.listSingleSentence[i].startTime && sentence.endTime < this.listSingleSentence[i].endTime)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                this.AddNewItemInListview(index, "重叠", this.overLapColor.BackColor);
            }
        }
        /// <summary>
        /// 检查空行
        /// </summary>
        /// <param name="index"></param>
        /// <param name="singleSentence"></param>
        private void CheckEmpty(int index, SingleSentence singleSentence)
        {
            if (singleSentence.content.Equals(""))
            {
                this.AddNewItemInListview(index, "空行", this.emptyColor.BackColor);
            }
        }
        /// <summary>
        /// 检查过长时间轴
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        /// <param name="length"></param>
        private void CheckTimeLengthBig(int index, SingleSentence sentence,double length)
        {
            if (sentence.endTime - sentence.startTime > length)
            {
                this.AddNewItemInListview(index, "时间轴过长", this.timeLengthBigColor.BackColor);
            }
        }
        /// <summary>
        /// 检查时间轴过短
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        /// <param name="length"></param>
        private void CheckTimeLengthSmall(int index, SingleSentence sentence, double length)
        {
            if (sentence.endTime - sentence.startTime < length)
            {
                this.AddNewItemInListview(index, "时间轴过短", this.timeLengthSmallColor.BackColor);
            }
        }
        /// <summary>
        /// 检查行数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        private void CheckLinesCount(int index,SingleSentence sentence,int length)
        {
            string[] spilt=new string[1];
            spilt[0]="\r\n";
            if (sentence.content.Split(spilt, StringSplitOptions.None).Length > length)
            {
                this.AddNewItemInListview(index, "超过"+length+"行", this.timeLengthSmallColor.BackColor);
            }
        }
        /// <summary>
        /// 检查全角符号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="sentence"></param>
        private void CheckFullWidthSymbol(int index, SingleSentence sentence)
        {
            if (CCHandle.JudgeFullWidthSymbol(sentence.content))
            {
                this.AddNewItemInListview(index, "有全角符号", this.fullWidthSymbolColor.BackColor);
            }
        }

        private void listViewShow_ItemActivate(object sender, EventArgs e)
        {
            
            //NotificationCenter.SendMessage("yyListView", "SetSelectedByIndex", selectedIndex);
            //NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", selectedIndex[selectedIndex.Count - 1]);
        }

        private void listViewShow_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
           
            List<int> list = new List<int>();
            for (int i = 0; i < this.listViewShow.Items.Count; i++)
            {
                if (this.listViewShow.Items[i].Selected)
                {
                    //list.Add(this.formMain.listView1.YYGetShowPosition(int.Parse(this.listViewShow.Items[i].SubItems[0].Text)));
                    list.Add(int.Parse(this.listViewShow.Items[i].SubItems[0].Text));
                }
            }
            NotificationCenter.SendMessage("yyListView", "SetSelectedByIndex", list);
            NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", int.Parse(this.listViewShow.Items[e.ItemIndex].SubItems[0].Text));
        }
    }
}
