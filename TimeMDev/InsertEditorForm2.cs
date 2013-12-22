using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.HandleRecord;
using TimeMDev.Notification;

namespace TimeMDev
{
    public partial class InsertEditorForm2 : Form
    {
        double SpiltTime;
        TimeLineReadWrite timeLineReadWrite;
        YYListView yyListView;
        CommandManage commandManage;
        public InsertEditorForm2(double SpiltTime,TimeLineReadWrite timeLineReadWrite,YYListView yyListView,CommandManage commandManage)
        {
            InitializeComponent();
            this.SpiltTime = SpiltTime;
            this.timeLineReadWrite = timeLineReadWrite;
            this.yyListView = yyListView;
            this.commandManage = commandManage;
            this.Init();
        }

        private void confirm_Click(object sender, EventArgs e)
        {

            int showPosition=this.yyListView.YYGetShowPosition(Int32.Parse(this.listViewShow.Items[this.listViewShow.SelectedIndices[0]].SubItems[0].Text));
            int realPosition=Int32.Parse(this.listViewShow.Items[this.listViewShow.SelectedIndices[0]].SubItems[0].Text);
            SingleSentence singleSentence=new SingleSentence();
            singleSentence.startTime=TimeLineReadWrite.TimeIn(this.startTimeBox.Text);
            singleSentence.endTime=TimeLineReadWrite.TimeIn(this.endtimeBox.Text);
            singleSentence.content=this.contentBox.Text;
            InsertRecord insertRecrod = new InsertRecord(this.timeLineReadWrite.listSingleSentence, this.yyListView, showPosition, realPosition, singleSentence);
            this.commandManage.CommandRun(insertRecrod);
            this.yyListView.YYRefresh();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Init()
        {
            this.listViewShow.Items.Clear();
            for (int i = 1; i < this.timeLineReadWrite.listSingleSentence.Count-2; i++)
            {
                if (this.timeLineReadWrite.listSingleSentence[i + 1].startTime - this.timeLineReadWrite.listSingleSentence[i].endTime>=this.SpiltTime)
                {
                    string[] str = new string[2];
                    str[0] = (i + 1)+"";
                    str[1] = TimeLineReadWrite.TimeOut(this.timeLineReadWrite.listSingleSentence[i + 1].startTime - this.timeLineReadWrite.listSingleSentence[i].endTime);
                    this.listViewShow.Items.Add(new ListViewItem(str));
                }
            }
        }

        private void listViewShow_ItemActivate(object sender, EventArgs e)
        {
            
            
        }

        private void listViewShow_Click(object sender, EventArgs e)
        {
            if (this.listViewShow.SelectedIndices.Count > 0)
            {
                int realPosition = Int32.Parse(this.listViewShow.Items[this.listViewShow.SelectedIndices[0]].Text);
                this.startTimeBox.Text = TimeLineReadWrite.TimeOut(this.timeLineReadWrite.listSingleSentence[realPosition - 1].endTime);
                this.endtimeBox.Text = TimeLineReadWrite.TimeOut(this.timeLineReadWrite.listSingleSentence[realPosition - 1].endTime + 10);
               // this.yyListView.YYEnsurVisible(this.yyListView.YYGetShowPosition(realPosition));
                NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", this.yyListView.YYGetShowPosition(realPosition));
                this.yyListView.YYRefresh();
            }
        }
    }
}
