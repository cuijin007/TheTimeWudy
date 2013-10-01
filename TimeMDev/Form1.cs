using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TimeMDev
{
    public partial class Form1 : Form
    {
        PictureRefresh pictureRefresh;
        double timeBuffer;
        int startLocationX;
        bool startMark = false;
        DataProcess dataProcess;
        MPlayer mplayer;
        TimeLineReadWrite timeLineReadWrite = new TimeLineReadWrite();

        string originalSubtitlePath, moviePath, movieName,temporarySubtitlePath;

        public int interval;
        public Form1()
        {
            InitializeComponent();
            this.InitClass();            
        }

        public void InitClass()
        {
            this.pictureRefresh = new PictureRefresh(this);
            this.mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());//给Mplayer初始化播放的输出位置
            dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1);
            dataProcess.DataInit();
            this.rateShow.MouseWheel+=new MouseEventHandler(rateShow_MouseWheel);
            originalSubtitlePath = "";
            moviePath = "";
            temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory+"\\save\\noname.srt";
        }
        private void startMplayerFristTimeLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.StartMplayerFristTime();
        }
        /// <summary>
        /// 首次打开Mplayer
        /// </summary>
        public void StartMplayerFristTime()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "所有文件|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (mplayer == null)
                {
                    mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                }
                else
                {
                    mplayer.Clear();
                }
                this.mplayer.FristTimeStart(dialog.FileName);
            }
        }

        private void openVideoItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "所有文件|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (mplayer == null)
                {
                    mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                    //dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1);
                    dataProcess.DataInit();
                }
                else
                {
                    mplayer.Clear();
                    mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                    //dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1);
                    dataProcess.DataInit();
                }
                this.mplayer.StartPlay(dialog.FileName);
                this.mplayer.GetTotalTime();
                this.dataProcess.Init();
                pictureRefresh.Start();
                this.rateShow.Focus();
                this.mplayer.Pause();//要求打开的时候是停止的。

                this.moviePath = Path.GetDirectoryName(dialog.FileName);
                this.movieName = Path.GetFileNameWithoutExtension(dialog.FileName);
                //到时候还要根据是什么类型的进行修改
                this.temporarySubtitlePath = this.moviePath + "\\" + this.movieName+".srt";
                this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
            }

        }

        private void movieTrack_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.mplayer != null)
            {
                this.mplayer.Pause();
            }
            this.nowTrackerPosition = this.movieTrack.Value;
        }

        private void movieTrack_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.mplayer != null)
            {
                this.mplayer.Pause();
                this.mplayer.SeekPosition(this.movieTrack.Value);
                this.rateShow.Focus();
            }
        }

        int nowTrackerPosition;
        private void movieTrack_Scroll(object sender, EventArgs e)
        {
            if (this.mplayer != null)
            {
                if ((this.movieTrack.Value - nowTrackerPosition) > this.interval || (this.movieTrack.Value - nowTrackerPosition) < -this.interval)
                {
                    this.mplayer.SeekPosition(this.movieTrack.Value);
                    this.nowTrackerPosition = this.movieTrack.Value;
                }
                this.nowTimeShow.Text = this.TimeOut(this.movieTrack.Value);
            }
        }
        
        private void pauseVideoItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mplayer != null)
            {
                this.mplayer.Pause();
            }
        }

        private void stopVideoItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mplayer != null)
            {
                this.mplayer.Pause();
            }
        }

        private void forwardOneSecItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.Forward(1);
        }

        private void backOneSecItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.BackWard(1);
        }

        private void forward5SecItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.Forward(5);
        }

        private void back5SecItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.BackWard(5);
        }

        private void quitItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.getPositionMark = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.pictureRefresh.runMark = false;
            this.mplayer.getPositionMark = false;
        }

        private void rateShow_Click(object sender, EventArgs e)
        {
            this.rateShow.Focus();
        }
        private void rateShow_MouseMove(object sender, MouseEventArgs e)
        {
            this.dataProcess.MouseMove(sender, e);
        }
        private void rateShow_MouseDown(object sender, MouseEventArgs e)
        {
            this.dataProcess.MouseDown(sender, e);
        }

        private void rateShow_MouseUp(object sender, MouseEventArgs e)
        {
            this.dataProcess.MouseUp(sender, e);
            this.timeLineReadWrite.WriteAllTimeline();
        }
        void rateShow_MouseWheel(object sender, MouseEventArgs e)
        {
            this.dataProcess.MouseWheel(sender, e);
        }

        private string TimeOut(double time)
        {
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            int minsec = (int)((time - (int)time) * 1000);
            string TimeStr = hour + ":" + minute + ":" + second + "," + minsec;
            return TimeStr;
        }
        /// <summary>
        /// 打开字幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "字幕文件|*.srt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1);
                dataProcess.DataInit();
                this.dataProcess.Init();
                pictureRefresh.Start();
                this.mplayer.Pause();//要求打开的时候是停止的。

                this.timeLineReadWrite = new TimeLineReadWrite();
                this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, dialog.FileName, false);
                this.timeLineReadWrite.ReadAllTimeline();
                this.originalSubtitlePath = dialog.FileName;//保存一个初始值。
                if (this.moviePath.Equals(""))
                {
                    //还要修改后缀
                    temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
                }
                else
                {
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                }
                this.rateShow.Focus();
                this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());

            }
            this.rateShow.Focus();
        }



        List<ListViewItem> ItemsCollect;
        /// <summary>
        /// 给listview赋值[这里是赋值，以后刷新的时候就是按前面的序号进行刷新了。]
        /// </summary>
        /// <param name="listSingleSentence"></param>
        public void SetListViewData(List<SingleSentence> listSingleSentence)
        {
            this.listView1.Clear();
            this.InitListView();
            string[] showBuffer = new string[5];
            ItemsCollect = new List<ListViewItem>();
            ListViewItem[] itemsCollect2 = new ListViewItem[listSingleSentence.Count];
            for (int i = 1; i < listSingleSentence.Count - 1; i++)
            {
               

                //showBuffer[0] = i + 1 + "";
                showBuffer[0] = i + "";//2013-4-10
                showBuffer[1] = TimeOut(listSingleSentence[i].startTime);
                showBuffer[2] = TimeOut(listSingleSentence[i].endTime);
                showBuffer[3] = listSingleSentence[i].content;
                showBuffer[4] = "0";
                //this.listView1.Items.Add(new ListViewItem(showBuffer));
                ItemsCollect.Add(new ListViewItem(showBuffer));
                //itemsCollect2[i-1] = new ListViewItem(showBuffer);
            }
           // this.listView1.Items.AddRange(itemsCollect2);
            this.listView1.VirtualListSize = ItemsCollect.Count;
            this.listView1.VirtualMode = true;
            this.listView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
        }

        void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //throw new NotImplementedException();
            if (this.ItemsCollect == null || this.ItemsCollect.Count == 0)
            {
                return;
            }
            else
            {
                e.Item = this.ItemsCollect[e.ItemIndex];
                if (e.ItemIndex == this.ItemsCollect.Count)
                {
                    //this.ItemsCollect = null;
                }
            }
        }
        /// <summary>
        /// 初始化那个listview
        /// </summary>
        public void InitListView()
        {
            this.listView1.Columns.Add("序号", 50);
            this.listView1.Columns.Add("开始", 80);
            this.listView1.Columns.Add("停止", 80);
            this.listView1.Columns.Add("内容", this.listView1.Width - 240);
            this.listView1.Columns.Add("标记", 0);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
           // this.listView1.SignItem();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                this.listView1.SortStyle = 2;
            }
            else if (e.Column == 3)
            {
                this.listView1.SortStyle = 1;
            }
            else
            {
                this.listView1.SortStyle = 0;
            }
            this.listView1.ColumnToSort = e.Column;
            if (this.listView1.Order ==SortOrder.Ascending)
            {
                this.listView1.Order = SortOrder.Descending;
            }
            else
            {
                this.listView1.Order = SortOrder.Ascending;
            }
            this.listView1.Sort();
        }

        private void listViewMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ccRemoveDuplicateItemL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CCHandle.HandleCCLongitudinal(this.timeLineReadWrite.GetListSingleSentence());
            this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listView1.VirtualListSize--;
            List<ListViewItem> items=new List<ListViewItem>();
            items.Add(ItemsCollect[0]);
            this.ItemsCollect.Remove(ItemsCollect[0]);
            this.listView1.Invalidate();
        }
    }
}
