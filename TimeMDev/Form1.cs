using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TimeMDev.HandleRecord;
using TimeMDev.Notification;
using TimeMDev.ConfigSave;
using TimeMDev.FileReadWriteFloder;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using System.Collections;
using Microsoft.Win32;
using TimeMDev.ShortCut;

namespace TimeMDev
{
    public partial class Form1 : Form
    {
        PictureRefresh pictureRefresh;
        double timeBuffer;
        int startLocationX;
        bool startMark = false;
        DataProcess dataProcess;
        /// <summary>
        /// 最近打开的文件
        /// </summary>
        RecentFile recentFile;
        public DataProcess DataProcessGet
        {
            get
            {
                return this.dataProcess;
            }
        }
        MPlayer mplayer;
        TimeLineReadWrite timeLineReadWrite = new TimeLineReadWrite();
        public CommandManage commandManage = new CommandManage(10);
        string originalSubtitlePath, moviePath, movieName,temporarySubtitlePath;

        public int interval;
        private Hashtable caption2id;
        public Form1()
        {
            InitializeComponent();
            this.InitClass();            
        }

        public void InitClass()
        {
            this.pictureRefresh = new PictureRefresh(this);
            //this.mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());//给Mplayer初始化播放的输出位置
            MPlayer.Wid = this.videoPlayPanel.Handle.ToInt32();
            this.mplayer = MPlayer.GetMPlayer();
            this.mplayer.DplayerRateMovieTrackAction = this.RefreshTrackBar;
            dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
            dataProcess.DataInit();
            this.rateShow.MouseWheel += new MouseEventHandler(rateShow_MouseWheel);
            originalSubtitlePath = "";
            moviePath = "";
            //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory+"\\save\\noname.srt";
            temporarySubtitlePath = "d:\\yyets~c~.srt";

            //初始的时候禁止listviewmenu显示
            //this.SetListviewMenuEnable(false);
            this.NotificationInit();
            //this.recentFile.OnItemClickAction += new OnItemClick(recentFile_OnItemClickAction);
            this.commandManage.AfterRunCommandFunction += new AfterRunCommandFuncionD(commandManage_AfterRunCommandFunction);
            this.commandManage.AfterRunCommandFunction += new AfterRunCommandFuncionD(commandManage_SaveAutoFunction);
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "yyetsTMConfig.xml"))
            {
                this.dockManager1.RestoreLayoutFromXml(System.AppDomain.CurrentDomain.BaseDirectory + "yyetsTMConfig.xml");
            }
            this.DoubleBuffered = true;
        }
        /// <summary>
        /// 自动保存
        /// </summary>
        void commandManage_SaveAutoFunction()
        {
            if(this.saveAutoCheckItem2.Checked)
            {
                if (commandManage.functionTime % SetSaveAuto.GetAutoSaveFunctionTime() == 0)
                {
                    string path = timeLineReadWrite.filePath;
                    path = this.originalSubtitlePath;
                    string pathBuf=SetSaveAuto.GetAutoSavePath()+"\\"+Path.GetFileNameWithoutExtension(path)+SetSaveAuto.GetAutoSaveCount();
                    if (path.EndsWith(".ass"))
                    {
                        timeLineReadWrite.Write(new FileWriteAss(Encoding.Default,pathBuf+".ass"));
                    }
                    if(path.EndsWith(".srt"))
                    {
                        timeLineReadWrite.Write(new FileWriteSrt(Encoding.Default,pathBuf+".srt"));
                    }
                    //this.timeLineReadWrite.filePath=path;
                }
            }
        }
        /// <summary>
        /// 每个command执行完之后的动作
        /// </summary>
        void commandManage_AfterRunCommandFunction()
        {
            string path = this.timeLineReadWrite.filePath;
            //this.timeLineReadWrite.Write(new FileWriteAss(this.timeLineReadWrite.encoding,path,TimeLineReadWrite.GetAssInfo()));
            if (this.originalSubtitlePath.EndsWith(".ass"))
            {
                path = path.Replace(".srt", ".ass");
                this.timeLineReadWrite.Write(new FileWriteAss(ChooseEncodingForm.GetAutoLoadSubEncoding(), path));
            }
            else if(this.originalSubtitlePath.EndsWith(".srt"))
            {
                path=path.Replace(".ass",".srt");
                this.timeLineReadWrite.Write(new FileWriteSrt(ChooseEncodingForm.GetAutoLoadSubEncoding(), path));
            }
            //this.timeLineReadWrite.WriteAllTimeline();
            //this.mplayer.LoadTimeLine(this.timeLineReadWrite.filePath);
            this.mplayer.LoadTimeLine(path);
        }

        void recentFile_OnItemClickAction(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = e.Item.Caption.Replace("|", "");
            if (File.Exists(path))
            {
                dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
                dataProcess.DataInit();
                this.dataProcess.Init();
                pictureRefresh.Start();
                //this.mplayer.Pause();//要求打开的时候是停止的。
                this.mplayer.StepPlay();//要求打开的时候是停止的。
                this.timeLineReadWrite = new TimeLineReadWrite();
                this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, path, false);
                if (path.EndsWith("srt"))
                {
                    //this.timeLineReadWrite.ReadAllTimeline();
                    this.timeLineReadWrite.Read(new FileReadSrt());
                }
                if (path.EndsWith("ass"))
                {
                    //this.timeLineReadWrite.ReadAllTimeLineAss();
                    this.timeLineReadWrite.Read(new FileReadAss());
                }
                this.originalSubtitlePath = path;//保存一个初始值。
                if (this.moviePath.Equals(""))
                {
                    //还要修改后缀
                    //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
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
        
        /// <summary>
        /// 通知中心初始化,将控件初始化进入通知中心
        /// </summary>
        public void NotificationInit()
        {
            NotificationCenter.AddNotificationCell("timeEditEnd", new TextBoxNotify(this.timeEditEnd));
            NotificationCenter.AddNotificationCell("timeEditStart", new TextBoxNotify(this.timeEditStart));
            NotificationCenter.AddNotificationCell("numEdit", new TextBoxNotify(this.numEdit));
            NotificationCenter.AddNotificationCell("contentEdit", new TextBoxNotify(this.contentEdit));
            NotificationCenter.AddNotificationCell("yyListView", new YYListViewNotification(this.listView1));
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
                    //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                    this.mplayer = MPlayer.GetMPlayer();
                }
                else
                {
                    //mplayer.Clear();
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
                    //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                    this.mplayer = MPlayer.GetMPlayer();
                    this.mplayer.DplayerRateAction = this.dataProcess.GetTimeAction;
                    //dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1,this.commandManage);
                    dataProcess.DataInit();
                }
                else
                {
                    //mplayer.Clear();
                    //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                    this.mplayer = MPlayer.GetMPlayer();
                    this.dataProcess.mplayer = this.mplayer;
                    this.mplayer.DplayerRateAction = this.dataProcess.GetTimeAction;
                    //dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1,this.commandManage);
                    dataProcess.DataInit();
                }
                this.mplayer.StartPlay(dialog.FileName);
                this.mplayer.GetTotalTime();
                this.dataProcess.Init();
                pictureRefresh.Start();
                this.rateShow.Focus();
               // this.mplayer.Pause();//要求打开的时候是停止的。
                this.mplayer.StepPlay();//要求打开的时候是停止的。
                this.moviePath = Path.GetDirectoryName(dialog.FileName);
                this.movieName = Path.GetFileNameWithoutExtension(dialog.FileName);
                //到时候还要根据是什么类型的进行修改
                //this.temporarySubtitlePath = this.moviePath + "\\" + this.movieName+".srt";
                if (Config.DefaultConfig["TimeMBufPath"].Equals(""))
                {
                    this.temporarySubtitlePath = "d:\\" + "TimeMBuf" + ".srt";
                }
                else
                {
                    this.temporarySubtitlePath = Config.DefaultConfig["TimeMBufPath"] + "TimeMBuf" + ".srt";
                }
                this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                //this.mplayer.LoadTimeLine(timeLineReadWrite.filePath);
                this.reloadSub_Click(null, null);
                ////////***************/////////2014-1-9
                string subPath = CCHandle.GetMovieSub(dialog.FileName);
                if (subPath != null)
                {
                    if (MessageBox.Show("是否自动搜索并载入新字幕?\r\n【如确定，则现有字幕不保存直接关闭】", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.DragLoadSub(subPath);
                    }
                }
                this.reloadSub_Click(null, null);
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
                //this.nowTimeShow.Text = this.TimeOut(this.movieTrack.Value);
                this.nowTimeLineShow.Text = TimeLineReadWrite.TimeOut(this.movieTrack.Value);
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
            //if ((new IfSaveForm(this.timeLineReadWrite, this.originalSubtitlePath)).ShowDialog() != DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.dockManager1.SaveLayoutToXml(System.AppDomain.CurrentDomain.BaseDirectory + "yyetsTMConfig.xml");
            this.pictureRefresh.runMark = false;
            this.mplayer.getPositionMark = false;
            this.mplayer.MPlayerQuit();
            
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
            //this.timeLineReadWrite.WriteAllTimeline();
            //this.timeLineReadWrite.WriteAllTimelineAss();
            //2013-10-3尝试一下看会不会崩溃
            if (mplayer != null)
            {
                //2013-10-11测试屏蔽，最终要放出来。
                //this.mplayer.LoadTimeLine(this.temporarySubtitlePath);
                //this.mplayer.RemoveTimeLine();
            }

            //this.dataProcess._Save();
        }
        void rateShow_MouseWheel(object sender, MouseEventArgs e)
        {
            this.dataProcess.MouseWheel(sender, e);
        }

        /// <summary>
        /// 打开字幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "字幕文件|*.srt;*.ass";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1,this.commandManage);
                dataProcess.DataInit();
                this.dataProcess.Init();
                pictureRefresh.Start();
                this.mplayer.Pause();//要求打开的时候是停止的。
                
                this.timeLineReadWrite = new TimeLineReadWrite();
                this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, dialog.FileName, false);
                if (dialog.FileName.EndsWith("srt"))
                {
                    //this.timeLineReadWrite.ReadAllTimeline();
                    this.timeLineReadWrite.Read(new FileReadSrt());
                }
                if (dialog.FileName.EndsWith("ass"))
                {
                    //this.timeLineReadWrite.ReadAllTimeLineAss();
                    this.timeLineReadWrite.Read(new FileReadAss());
                }
                this.originalSubtitlePath = dialog.FileName;//保存一个初始值。
                if (this.moviePath.Equals(""))
                {
                    //还要修改后缀
                    //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                }
                else
                {
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                }
                this.rateShow.Focus();
                this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());

                //使能 listviewmenu
                //this.SetListviewMenuEnable(true);
                //增加最近打开的文件
                this.recentFile.AddRecentFile(dialog.FileName);
                //this.mplayer.LoadTimeLine(timeLineReadWrite.filePath);
                this.reloadSub_Click(null, null);
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
            this.listView1.yyItems.Clear();
            this.listView1.YYRefresh();
            this.InitListView();
            string[] showBuffer = new string[this.listView1.Columns.Count];
            ItemsCollect = new List<ListViewItem>();
            ListViewItem[] itemsCollect2 = new ListViewItem[listSingleSentence.Count];
            //for (int i = 1; i < listSingleSentence.Count - 1; i++)
            for (int i = 1; i < listSingleSentence.Count; i++)
            {
                //showBuffer[0] = i + 1 + "";
                showBuffer[0] = i + "";//2013-4-10
                showBuffer[1] = TimeLineReadWrite.TimeOut(listSingleSentence[i].startTime);
                showBuffer[2] = TimeLineReadWrite.TimeOut(listSingleSentence[i].endTime);
                showBuffer[3] = listSingleSentence[i].content;
                showBuffer[4] = listSingleSentence[i].everyLineLength;
                showBuffer[5] = TimeLineReadWrite.TimeOut(listSingleSentence[i].timeLength);
                showBuffer[6] = listSingleSentence[i].lineNum+"";
                ListViewItem item = new ListViewItem(showBuffer);
                item.Checked = listSingleSentence[i].Checked;
                //this.listView1.yyItems.Add(new ListViewItem(showBuffer));
                this.listView1.yyItems.Add(item);//2014-1-10增加标记功能
            }

            this.listView1.YYRefresh();
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
            this.listView1.Columns.Add("内容", this.listView1.Width - 330);
            //2013-11-25添加
            this.listView1.Columns.Add("字符", 40);
            this.listView1.Columns.Add("时长", 40);
            this.listView1.Columns.Add("行数", 40);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
           // this.listView1.SignItem();
            //this.listView1.DoubleClickAction1 =new DoubleClickActionD(mplayer.SeekPosition);

                //mplayer.SeekPosition(TimeLineReadWrite.TimeIn(this.listView1.yyItems[this.listView1.SelectedIndices[0]].SubItems[1].Text));
           // this.listView1.ActiveTimeStart = TimeLineReadWrite.TimeIn(this.listView1.yyItems[this.listView1.SelectedIndices[0]].SubItems[1].Text);
           // this.listView1.ActiveTimeEnd = TimeLineReadWrite.TimeIn(this.listView1.yyItems[this.listView1.SelectedIndices[0]].SubItems[2].Text);
            this.listView1.SeekTimeAction = new DDoubleClickActionSeekTime(mplayer.SeekPosition);
            this.listView1.timeStart = TimeLineReadWrite.TimeIn(this.listView1.yyItems[this.listView1.SelectedIndices[0]].SubItems[1].Text);
            this.listView1.timeEnd = TimeLineReadWrite.TimeIn(this.listView1.yyItems[this.listView1.SelectedIndices[0]].SubItems[2].Text);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int columnNum = e.Column;
            if (e.Column == 0||e.Column==6)
            {
                this.listView1.SortStyle = 2;
            }
            else if (e.Column == 3||e.Column==4)
            {
                columnNum = 3;
                this.listView1.SortStyle = 1;
            }
            else if(e.Column==2||e.Column==1||e.Column==5)
            {
                this.listView1.SortStyle = 0;
            }
            //this.listView1.ColumnToSort = e.Column;
            if (this.listView1.Order ==SortOrder.Ascending)
            {
                this.listView1.Order = SortOrder.Descending;
            }
            else if (this.listView1.Order == SortOrder.Descending)
            {
                this.listView1.Order = SortOrder.Ascending;
            }
            if (this.listView1.Order == SortOrder.None)
            {
                this.listView1.Order = SortOrder.Ascending;
            }
            //this.listView1.YYSort();
            ListViewSort sort = new ListViewSort(this.listView1, columnNum, this.listView1.SortStyle, this.listView1.Order);
            this.commandManage.CommandRun(sort);
        }

        private void listViewMenu_Opening(object sender, CancelEventArgs e)
        {
            if (this.listView1.Columns.Count == 0)
            {
                for (int i = 0; i < this.listViewMenu.Items.Count; i++)
                {
                    this.listViewMenu.Items[i].Enabled = false;
                }
                    return;
            }
            else
            {
                for (int i = 0; i < this.listViewMenu.Items.Count; i++)
                {
                    this.listViewMenu.Items[i].Enabled = true;
                }
                if(this.mplayer.IsHaveMovie())
                {
                    this.alignNowLineContext.Enabled=false;
                    this.alignAfterLineContext.Enabled=false;
                }
            }
        }

        private void ccRemoveDuplicateItemL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CCHandle.HandleCCLongitudinal(this.timeLineReadWrite.GetListSingleSentence());
            this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
            this.listView1.YYRefresh();
        }

        private void ccRemoveDuplicateItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CCHandle.HandleCCLatitude(this.timeLineReadWrite.GetListSingleSentence());
            this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
            this.listView1.YYRefresh();
        }

        private void ccSubtitleHandleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CCHandle.DeleteRemark(this.timeLineReadWrite.GetListSingleSentence());
            this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
            this.listView1.YYRefresh();
        }

        ///以下，ListViewMenu的响应函数
        private void addOneLineContext_Click(object sender, EventArgs e)
        {
            /*
            if(this.listView1.SelectedIndices.Count>0)
            {
                //this.listView1.Items.Insert(this.listView1.SelectedItems[this.listView1.SelectedItems.Count-1]
                //this.listView1.yyItems.Insert(this.listView1.SelectedItemsthis.listView1.SelectedItems[this.listView1.SelectedItems.Count-1]
                int index = this.listView1.SelectedIndices[this.listView1.SelectedIndices.Count - 1];
                index++;
                SingleSentence singleSentence=new SingleSentence();
                singleSentence.startTime = this.dataProcess.listSingleSentence[index].endTime;
                singleSentence.endTime = singleSentence.startTime + 1;
                this.dataProcess.listSingleSentence.Insert(index+1, singleSentence);
                this.SetListViewData(this.dataProcess.listSingleSentence);
                this.listView1.YYRefresh();
           }
             * */
            if (this.listView1.SelectedIndices.Count > 0)
            {
                AddBlankRecord addBlankRecord = new AddBlankRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], 1, 1, true);
                this.commandManage.CommandRun(addBlankRecord);
                this.listView1.YYRefresh();
            }
        }

        private void addMutiLineContext_Click(object sender, EventArgs e)
        {
            
            int indexSave;
            if (this.listView1.SelectedIndices.Count > 0)
            {
                indexSave = this.listView1.SelectedIndices[0];
            }
            AddMutiLinesParameter addMutiLinesParameter = new AddMutiLinesParameter();
            (new AddMutiLinesForm(addMutiLinesParameter)).ShowDialog();
            int index = this.listView1.yyItems.Count-1; 
            if (this.listView1.SelectedIndices.Count > 0)
            {
                index = this.listView1.SelectedIndices[this.listView1.SelectedIndices.Count - 1];
            }
            AddBlankRecord addBlankRecord = new AddBlankRecord(this.dataProcess.listSingleSentence, this.listView1, index, addMutiLinesParameter.lineCount, addMutiLinesParameter.timeSpiltLength, addMutiLinesParameter.isBlank);
            this.commandManage.CommandRun(addBlankRecord);
            this.listView1.YYRefresh();
            /*
            index++;
            double startTime= this.dataProcess.listSingleSentence[index].endTime;
            if (addMutiLinesParameter.isConfirm == true)
            {
                for (int i = 0; i < addMutiLinesParameter.lineCount; i++)
                {
                    SingleSentence singleSentence = new SingleSentence();
                    singleSentence.startTime = startTime;
                    singleSentence.endTime = startTime+addMutiLinesParameter.timeSpiltLength;
                    if (addMutiLinesParameter.isBlank)
                    {
                        singleSentence.content = "";
                    }
                    else
                    {
                        singleSentence.content = "空白内容行" + i;
                    }
                    this.dataProcess.listSingleSentence.Insert(index + 1+i, singleSentence);
                    //this.SetListViewData(this.dataProcess.listSingleSentence);
                    startTime += addMutiLinesParameter.timeSpiltLength;
                    
                    //index++;
                }

                this.listView1.YYInsertBlank(index, addMutiLinesParameter.lineCount, addMutiLinesParameter.timeSpiltLength, addMutiLinesParameter.isBlank);
                this.listView1.yyItems[index].Focused = true;
                this.listView1.YYRefresh();
            }
             */
        }

        private void deleteLineContext_Click(object sender, EventArgs e)
        {
            List<HandleRecordBass> listCommand=new List<HandleRecordBass>();
            for(int i=this.listView1.SelectedIndices.Count-1;i>=0;i--)
            {
                DeleteRecord deleteRecord = new DeleteRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[i]);
                listCommand.Add(deleteRecord);
            }
            this.commandManage.CommandRun(listCommand);
            this.listView1.YYRefresh();
       }

        private void zeroTime_Click(object sender, EventArgs e)
        {
            List<HandleRecordBass> commands = new List<HandleRecordBass>();
            for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            {
                ZeroRecord zeroRecord = new ZeroRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[i], 1);
                commands.Add(zeroRecord);
            }
            this.commandManage.CommandRun(commands);
            this.listView1.YYRefresh();
        }

        private void zeroContentContext_Click(object sender, EventArgs e)
        {
            List<HandleRecordBass> commands = new List<HandleRecordBass>();
            for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            {
                ZeroRecord zeroRecord = new ZeroRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[i], 0);
                commands.Add(zeroRecord);
            }
            this.commandManage.CommandRun(commands);
            this.listView1.YYRefresh();
        }

        private void undoContext_Click(object sender, EventArgs e)
        {
            //this.dataProcess._Undo();
            //这里是什么情况
            this.commandManage.Undo();
        }
        /// <summary>
        /// 自定义拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customSpiltContext_Click(object sender, EventArgs e)
        {
            List<HandleRecordBass> listCommand = new List<HandleRecordBass>();
            if (this.listView1.SelectedIndices.Count > 0)
            {
                SpiltParameter spiltParameter = new SpiltParameter();

                int index = Int32.Parse(this.listView1.yyItems[this.listView1.SelectedIndices[0]].Text);
                spiltParameter.beforeSpilt = this.dataProcess.listSingleSentence[index].content;
                this.Capture = false;
                (new CustomSpilt(spiltParameter)).ShowDialog();
                if (spiltParameter.confirm == true)
                {
                    //拆分之后进行新行的插入和旧行的删除
                    double startTime = this.dataProcess.listSingleSentence[index].startTime;
                    double endTime = this.dataProcess.listSingleSentence[index].endTime;
                    double timeSpilt = endTime - startTime;
                    int count = 0;
                    if (spiltParameter.afterSpiltFristLine.Length > spiltParameter.afterSpiltSecondLine.Length)
                    {
                        count = spiltParameter.afterSpiltFristLine.Length;
                    }
                    else
                    {
                        count = spiltParameter.afterSpiltSecondLine.Length;
                    }

                    int showPosition = this.listView1.SelectedIndices[0];
                    DeleteRecord deleteRecord = new DeleteRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0]);
                    listCommand.Add(deleteRecord);
                    for (int i = 0; i < count; i++)
                    {
                        SingleSentence singleSentence = new SingleSentence();
                        string content = "";
                        if (spiltParameter.afterSpiltFristLine.Length > i)
                        {
                            content += spiltParameter.afterSpiltFristLine[i];
                            content += "\r\n";
                        }
                        if (spiltParameter.afterSpiltSecondLine.Length > i)
                        {
                            content += spiltParameter.afterSpiltSecondLine[i];
                        }
                        singleSentence.startTime = startTime + ((endTime - startTime) / count) * i;
                        singleSentence.endTime = startTime + ((endTime - startTime) / count) * (i + 1);
                        singleSentence.content = content;
                        InsertRecord insertRecord = new InsertRecord(this.dataProcess.listSingleSentence, this.listView1, showPosition + i, index + i, singleSentence);
                        listCommand.Add(insertRecord);
                    }
                    this.commandManage.CommandRun(listCommand);
                }
                this.listView1.YYRefresh();
            }
        }

        private void copyContext_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                CopyRecord copyRecord = new CopyRecord(this.dataProcess, this.listView1, this.listView1.SelectedIndices);
                this.commandManage.CommandRunNoRedo(copyRecord);
                this.listView1.YYRefresh();
            }
        }

        private void cutContext_Click(object sender, EventArgs e)
        {
            CutRecord cutRecord = new CutRecord(this.dataProcess, this.listView1, this.listView1.SelectedIndices);
            this.commandManage.CommandRun(cutRecord);
            this.listView1.YYRefresh();
        }

        private void pasteContext_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                PasteRecord pasteRecord = new PasteRecord(this.dataProcess, this.listView1, this.listView1.SelectedIndices[0] + 1);
                this.commandManage.CommandRun(pasteRecord);
                this.listView1.YYRefresh();
            }
        }

        private void checkAllContext_Click(object sender, EventArgs e)
        {
            this.selectItem_ItemClick(null, null);
        }

        private void createEngNameItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnglishNameChange englishNameChange = new EnglishNameChange(this.dataProcess.listSingleSentence, this.originalSubtitlePath + "namelist.txt");
            englishNameChange.CreateAndWriteNameList();
        }

        private void replaceEngNameItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnglishNameChange englishNameChange = new EnglishNameChange(this.dataProcess.listSingleSentence, this.originalSubtitlePath+"namelist.txt");
            englishNameChange.ReadAndReplaceAllName();
            this.SetListViewData(this.dataProcess.listSingleSentence);
            this.listView1.YYRefresh();
        }

        private void findErrorItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new ErrorCheckingForm(this)).Show();
        }

        private void revocationItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.commandManage.Undo();
            this.listView1.YYRefresh();
        }

        private void redoItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.commandManage.Redo();
            this.listView1.YYRefresh();
        }

        private void replaceItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new ReplaceForm(this.commandManage, this)).Show();
        }

        private void findItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new FindForm(this)).Show();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            List<int> listIndex = new List<int>();
            for (int i = 0; i < this.listView1.SelectedIndices.Count;i++)
            {
                listIndex.Add(this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[i]));
            }
            this.pictureRefresh.SelectedIndexWidthChange=listIndex;
            if (listIndex.Count > 0)
            {
                //把坐标时间弄过去
                this.pictureRefresh.NowTime = this.dataProcess.listSingleSentence[
                  listIndex[listIndex.Count-1]
                    ].startTime;
            }
        }
       
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.recentFile = new RecentFile(this.fileMenu);
            this.recentFile.OnItemClickAction += new OnItemClick(recentFile_OnItemClickAction);

            this.bindShortCuts();
            this.ReadAllCheck();
        }

        private void fileSplitSaveItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new FileCutForm(this.timeLineReadWrite)).ShowDialog();
        }

        private void exportMutiSaveItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new ExportForm(this.timeLineReadWrite)).ShowDialog();
        }

        private void addSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "字幕文件|*.srt;*.ass";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.commandManage.CommandRun(new AdditionalSubtitle(this.timeLineReadWrite,this.listView1,dialog.FileName));
            }
            this.listView1.YYRefresh();
        }

        private void newSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "字幕文件|*.srt;*.ass";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddMutiLinesParameter addMutiLinesParameter = new AddMutiLinesParameter();
                AddMutiLinesForm addMutiLinesForm = new AddMutiLinesForm(addMutiLinesParameter);
                addMutiLinesForm.ShowDialog();
                if (addMutiLinesParameter.isConfirm)
                {
                    dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
                    dataProcess.DataInit();
                    this.dataProcess.Init();
                    pictureRefresh.Start();
                    this.mplayer.Pause();//要求打开的时候是停止的。
                    this.timeLineReadWrite = new TimeLineReadWrite();
                    this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, dialog.FileName, false);
                    this.timeLineReadWrite.filePath = dialog.FileName;
                    //dataProcess.DataInit();

                    this.originalSubtitlePath = dialog.FileName;//保存一个初始值。
                    if (this.moviePath.Equals(""))
                    {
                        //还要修改后缀
                        //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
                        this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                    }
                    else
                    {
                        this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                    }
                    this.rateShow.Focus();
                    this.timeLineReadWrite.listSingleSentence.Clear();
                    //2013-12-10 新增
                    this.timeLineReadWrite.listSingleSentence.Add(new SingleSentence());
                    for (int i = 0; i < addMutiLinesParameter.lineCount; i++)
                    {
                        SingleSentence sentence = new SingleSentence();
                        sentence.startTime = 0 + i * addMutiLinesParameter.timeSpiltLength;
                        sentence.endTime = sentence.startTime + addMutiLinesParameter.timeSpiltLength;
                        this.timeLineReadWrite.listSingleSentence.Add(sentence);
                    }
                    //2014-1-11去掉最后一行多余的
                    //this.timeLineReadWrite.listSingleSentence.Add(new SingleSentence());
                    
                    this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
                    if (dialog.FileName.EndsWith("srt"))
                    {
                        this.timeLineReadWrite.Write(new FileWriteSrt());
                    }
                    if (dialog.FileName.EndsWith("ass"))
                    {
                        this.timeLineReadWrite.Write(new FileWriteAss());
                    }

                    this.recentFile.AddRecentFile(dialog.FileName);
                }
            }

            this.rateShow.Focus();
            this.listView1.YYRefresh();
        }

        private void confirmChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.timeEditEnd.Text.Equals("") && !this.timeEditStart.Text.Equals("") && !this.numEdit.Text.Equals(""))
                {
                    SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[Int32.Parse(this.numEdit.Text)]);
                    if (sentence.startTime != TimeLineReadWrite.TimeIn(timeEditStart.Text) ||
                    sentence.endTime != TimeLineReadWrite.TimeIn(timeEditEnd.Text) ||
                    !sentence.content.Equals(contentEdit.Text)
                    )
                    {
                        sentence.startTime = TimeLineReadWrite.TimeIn(timeEditStart.Text);
                        sentence.endTime = TimeLineReadWrite.TimeIn(timeEditEnd.Text);
                        sentence.content = contentEdit.Text;
                        ChangeRecord record = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.YYGetShowPosition(Int16.Parse(this.numEdit.Text)), sentence);
                        this.commandManage.CommandRun(record);
                    }
                    this.listView1.YYRefresh();
                }
            }
            catch
            {
                //MessageBox.Show("输入格式有问题");
            }


        }

        private void saveSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            string buf=this.timeLineReadWrite.filePath;
            this.timeLineReadWrite.filePath=this.originalSubtitlePath;
            this.timeLineReadWrite.Write(new FileWriteSrt(Encoding.Default));
            this.timeLineReadWrite.filePath = buf;
            this.commandManage.NeedSave = false;
        
             */
            if (this.originalSubtitlePath.EndsWith(".srt"))
            {
                this.timeLineReadWrite.Write(new FileWriteSrt(Encoding.Default, this.originalSubtitlePath));
            }
            if (this.originalSubtitlePath.EndsWith(".ass"))
            {
                this.timeLineReadWrite.Write(new FileWriteAss(Encoding.Default, this.originalSubtitlePath));
            }
            this.timeLineReadWrite.DeleteBuf(this.timeLineReadWrite.filePath);
        }

        private void saveAsItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if ((new SaveAsForm(this.timeLineReadWrite)).ShowDialog() == DialogResult.OK)
            //{
            //    this.commandManage.NeedSave = false;
            //}
            string buf = this.timeLineReadWrite.filePath;
            SaveFileDialog dialog = new SaveFileDialog();
            if (originalSubtitlePath.EndsWith(".srt"))
            {
                dialog.FileName = "newSub.srt";
            }
            if (originalSubtitlePath.EndsWith(".ass"))
            {
                dialog.FileName = "newSub.ass";
            }
            dialog.Filter = "字幕文件|*.srt;*.ass";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".srt"))
                {
                    this.timeLineReadWrite.filePath = dialog.FileName;
                    this.timeLineReadWrite.Write(new FileWriteSrt(Encoding.UTF8));
                }
                if (dialog.FileName.EndsWith(".ass"))
                {
                    this.timeLineReadWrite.filePath = dialog.FileName;
                    this.timeLineReadWrite.Write(new FileWriteAss(Encoding.UTF8));
                }
                MessageBox.Show("保存成功");
                this.commandManage.NeedSave = false;
            }
            this.timeLineReadWrite.filePath = buf;
        }
        /// <summary>
        /// 平移时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translationTimeItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {

                TranslateFormPara translateFormPara = new TranslateFormPara();
                translateFormPara.time = this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;

                if (new TranslateForm(translateFormPara).ShowDialog() == DialogResult.OK)
                {
                    List<HandleRecordBass> command = new List<HandleRecordBass>();
                    double Diff=translateFormPara.time-this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                    if (translateFormPara.state == TranslateFormPara.SELECTEDLINE)//只更改选中行
                    {
                        SingleSentence singleSentence=CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])]);
                        singleSentence.startTime+=Diff;
                        singleSentence.endTime+=Diff;
                        command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence,this.listView1,this.listView1.SelectedIndices[0],singleSentence));
                    }
                     if (translateFormPara.state == TranslateFormPara.AFTERSELECTED)//在选中的行之后
                    {
                         for(int i=this.listView1.SelectedIndices[0];i<this.listView1.yyItems.Count;i++)
                         {
                            SingleSentence singleSentence=CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                            singleSentence.startTime+=Diff;
                            singleSentence.endTime+=Diff;
                            command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence,this.listView1,i,singleSentence));
                         }
                     }
                     if (translateFormPara.state == TranslateFormPara.ALLLINE)//所有行
                     {
                         for (int i = 0; i < this.listView1.yyItems.Count; i++)
                         {
                             SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                             singleSentence.startTime += Diff;
                             singleSentence.endTime += Diff;
                             command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                         }
                     }
                     if (translateFormPara.state == TranslateFormPara.BETWEENSELECTED)//两个被选中行之间的所有的时间的更改
                     {
                         int startPosition = 0;
                         int endPosition = this.listView1.yyItems.Count-1;
                         for (int i = this.listView1.SelectedIndices[0]; i >= 0; i--)
                         {
                             if (this.listView1.yyItems[i].Checked)
                             {
                                 startPosition = i;
                                 break;
                             }
                         }
                         for (int i = this.listView1.SelectedIndices[0]; i < this.listView1.yyItems.Count; i++)
                         {
                             if (this.listView1.yyItems[i].Checked)
                             {
                                 endPosition = i;
                                 break;
                             }
                         }

                         for (int i = startPosition; i <= endPosition; i++)
                         {
                             SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                             singleSentence.startTime += Diff;
                             singleSentence.endTime += Diff;
                             command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                         }
                     }
                     this.commandManage.CommandRun(command);
                     this.listView1.YYRefresh();
                }
            }
            this.listView1.YYRefresh();
        }

        private void listView1_DoubleClickAction(int showPosition)
        {
            bool state;
            if (this.listView1.yyItems[showPosition].Checked)
            {
                state = false;
            }
            else
            {
                state = true;
            }
            this.commandManage.CommandRun(new SetSelectedState(this.timeLineReadWrite.listSingleSentence, this.listView1, showPosition, state));
            this.listView1.YYRefresh();
        }

        private void nowLineStartItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.mplayer != null&&this.listView1.SelectedIndices.Count>0)
            {
                SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])]);
                singleSentence.startTime = this.mplayer.nowTime+this.GetDelayTime();
                if (singleSentence.endTime < singleSentence.startTime)
                {
                    singleSentence.endTime = singleSentence.startTime;
                }
                ChangeRecord changeRecord = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], singleSentence);
                this.commandManage.CommandRun(changeRecord);
            }
            this.listView1.YYRefresh();
        }

        private void nowLineEndItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.mplayer != null && this.listView1.SelectedIndices.Count > 0)
            {
                SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])]);
                singleSentence.endTime = this.mplayer.nowTime + this.GetDelayTime();
                ChangeRecord changeRecord = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], singleSentence);
                this.commandManage.CommandRun(changeRecord);

                if (this.listView1.SelectedIndices[0] < this.listView1.yyItems.Count - 1)
                {
                    List<int> list = new List<int>();
                    list.Add(this.listView1.SelectedIndices[0] + 1);
                    //NotificationCenter.SendMessage("yyListView", "SetSelectedByIndex", list);
                    int index = this.listView1.SelectedIndices[0] + 1;
                    this.listView1.YYClearSelected();
                    this.listView1.YYSetSelected(index);
                    NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", this.listView1.SelectedIndices[0]+1);
                }
            }
            this.listView1.YYRefresh();
        }

        private void onlyNowLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(this.listView1.SelectedIndices.Count>0)
            {
                SingleSentence singleSentence=CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])]);
                double diff = this.mplayer.nowTime - singleSentence.startTime;
                singleSentence.startTime += diff;
                singleSentence.endTime += diff;
                this.commandManage.CommandRun(new ChangeRecord(this.timeLineReadWrite.listSingleSentence,this.listView1,this.listView1.SelectedIndices[0],singleSentence));
            }
            this.listView1.YYRefresh();
        }

        private void afterNowLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                List<HandleRecordBass> command = new List<HandleRecordBass>();
                Double Diff = this.mplayer.nowTime - this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                for (int i = this.listView1.SelectedIndices[0]; i < this.listView1.yyItems.Count; i++)
                {
                    SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                    singleSentence.startTime += Diff;
                    singleSentence.endTime += Diff;
                    command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                }
                this.commandManage.CommandRun(command);
            }
            this.listView1.YYRefresh();
        }

        private void allLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                List<HandleRecordBass> command = new List<HandleRecordBass>();
                Double Diff = this.mplayer.nowTime - this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                for (int i = 0; i < this.listView1.yyItems.Count; i++)
                {
                    SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                    singleSentence.startTime += Diff;
                    singleSentence.endTime += Diff;
                    command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                }
                this.commandManage.CommandRun(command);
            }
            this.listView1.YYRefresh();
        }

        private void lastSignItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                List<HandleRecordBass> command = new List<HandleRecordBass>();
                Double Diff = this.mplayer.nowTime - this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                int startPosition = 0;
                for (int i = this.listView1.SelectedIndices[0]; i >= 0; i--)
                {
                    if (this.listView1.yyItems[i].Checked)
                    {
                        startPosition = i;
                        break;
                    }
                }
                for (int i = startPosition; i < this.listView1.SelectedIndices[0]; i++)
                    {
                        SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                        singleSentence.startTime += Diff;
                        singleSentence.endTime += Diff;
                        command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                    }
                this.commandManage.CommandRun(command);
            }
            this.listView1.YYRefresh();
        }

        private void alignLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                List<HandleRecordBass> command = new List<HandleRecordBass>();
                Double Diff = this.mplayer.nowTime - this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                int startPosition = 0;
                int endPosition = this.listView1.yyItems.Count - 1;
                for (int i = this.listView1.SelectedIndices[0]; i >= 0; i--)
                {
                    if (this.listView1.yyItems[i].Checked)
                    {
                        startPosition = i;
                        break;
                    }
                }
                for (int i = this.listView1.SelectedIndices[0]; i < this.listView1.yyItems.Count; i++)
                {
                    if (this.listView1.yyItems[i].Checked)
                    {
                        endPosition = i;
                        break;
                    }
                }
                for (int i = startPosition; i <endPosition; i++)
                    {
                        SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                        singleSentence.startTime += Diff;
                        singleSentence.endTime += Diff;
                        command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                    }
                this.commandManage.CommandRun(command);
            }
            this.listView1.YYRefresh();
        }

        private void extendShowItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExtendFormPara extendFormPara = new ExtendFormPara();
            ExtendForm extendForm = new ExtendForm(extendFormPara);
            List<HandleRecordBass> command = new List<HandleRecordBass>();
            if (extendForm.ShowDialog() == DialogResult.OK)
            {
                 if (this.listView1.SelectedIndices.Count > 0)
                {
                    for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
                    {
                        SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])]);
                        singleSentence.endTime += extendFormPara.time;
                        ChangeRecord changeRecord = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], singleSentence);
                        command.Add(changeRecord);
                    }
                }
            }
            this.commandManage.CommandRun(command);
            this.listView1.YYRefresh();
        }

        private void changeUpandDownItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<HandleRecordBass> command = new List<HandleRecordBass>();
            for (int i = 0; i < this.listView1.yyItems.Count; i++)
            {
                SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                string english, chinese;
                CCHandle.SpiltRule(sentence.content, out chinese, out english);
                chinese = CCHandle.TrimEnterStart(chinese);
                chinese = CCHandle.TrimEnterEnd(chinese);
                english = CCHandle.TrimEnterStart(english);
                english = CCHandle.TrimEnterEnd(english);
                if (sentence.content.StartsWith(chinese))
                {
                    sentence.content = english + "\r\n" + chinese;
                }
                else
                {
                    sentence.content = chinese + "\r\n" + english;
                }
                ChangeRecord changeRecord = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, sentence);
                command.Add(changeRecord);
            }
            this.commandManage.CommandRun(command);
            this.listView1.YYRefresh();
        }

        private void addEditorItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TimeMDev.InsertEditorForm1.InsertEditorForm1Para para=new InsertEditorForm1.InsertEditorForm1Para();
            if ((new InsertEditorForm1(para)).ShowDialog() == DialogResult.OK)
            {
                (new InsertEditorForm2(para.time, this.timeLineReadWrite, this.listView1, this.commandManage)).ShowDialog();
            }
        }

        private void toolStripLoadFile_Click(object sender, EventArgs e)
        {
            this.openVideoItem_ItemClick(null, null);
        }

        private void toolStripPlay_Click(object sender, EventArgs e)
        {
            this.pauseVideoItem_ItemClick(null, null);
        }

        private void toolStripPause_Click(object sender, EventArgs e)
        {
            this.pauseVideoItem_ItemClick(null, null);
        }

        private void toolStripBack5_Click(object sender, EventArgs e)
        {
            this.backOneSecItem_ItemClick(null, null);
        }

        private void toolStripForward5_Click(object sender, EventArgs e)
        {
            this.forwardOneSecItem_ItemClick(null, null);
        }

        private void toolStripBack10_Click(object sender, EventArgs e)
        {
            this.back5SecItem_ItemClick(null, null);
        }

        private void toolStripForward10_Click(object sender, EventArgs e)
        {
            this.forward5SecItem_ItemClick(null, null);
        }

        private void barButtonItemOneFrame_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.mplayer.StepPlay();
        }

        private void toolStrip1Step_Click(object sender, EventArgs e)
        {
            this.barButtonItemOneFrame_ItemClick(null, null);
        }

        private void toolStripSyn_Click(object sender, EventArgs e)
        {
            //this.subSyncItem_ItemClick(null, null);
            this.subSyncItem_CheckedChanged(null, null);
        }

        private void subSyncItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   
            
        }

        private void toolStripAlignNow_Click(object sender, EventArgs e)
        {
            this.onlyNowLineItem_ItemClick(null, null);
        }

        private void toolStripAlignAfter_Click(object sender, EventArgs e)
        {
            this.afterNowLineItem_ItemClick(null, null);
        }

        private void toolStripAlighAll_Click(object sender, EventArgs e)
        {
            this.allLineItem_ItemClick(null, null);
        }

        private void toolStripAlighStart_Click(object sender, EventArgs e)
        {
            this.nowLineStartItem_ItemClick(null, null);
        }

        private void toolStripAlighEnd_Click(object sender, EventArgs e)
        {
            this.nowLineEndItem_ItemClick(null, null);
        }

        private void addOneLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.addOneLineContext_Click(null, null);
        }

        private void toolStripAddOne_Click(object sender, EventArgs e)
        {
            this.addOneLineItem_ItemClick(null, null);
        }

        private void addMutiLineItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.addMutiLineContext_Click(null, null);
        }

        private void toolStripAddMuti_Click(object sender, EventArgs e)
        {
            this.addMutiLineItem_ItemClick(null, null);
        }

        private void toolStripSign_Click(object sender, EventArgs e)
        {
            List<HandleRecordBass> command = new List<HandleRecordBass>();
            for (int i = 0; i < this.listView1.SelectedIndices.Count; i++)
            {
                bool state;
                if (this.listView1.yyItems[this.listView1.SelectedIndices[i]].Checked)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }
                command.Add(new SetSelectedState(this.timeLineReadWrite.listSingleSentence,this.listView1,this.listView1.SelectedIndices[i],state));
            }
            this.commandManage.CommandRun(command);
            this.listView1.YYRefresh();
        }

        private void toolStripSaveAuto_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSync_Click(object sender, EventArgs e)
        {

        }

        private void subSyncItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {

                MPlayer.RefreshMark = this.subSyncItem.Checked;
                this.toolStripSyn.Checked = this.subSyncItem.Checked;    
        }

        private void autoModeItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void merageAccurateLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        /// <summary>
        /// 自动更改的标记位
        /// </summary>
        int autoChangePos = 0;
        private void nextItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.autoModeItem.Checked)
            {
                if (autoChangePos == 0)
                {
                    this.nowLineStartItem_ItemClick(null, null);
                    autoChangePos = 1;
                }
                else
                {
                    this.nowLineEndItem_ItemClick(null, null);
                    autoChangePos = 0;
                }
            }
        }

        private void openSubtitleLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.loadSubtitleItem_ItemClick(null, null);
        }

        private void openVideoLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.openVideoItem_ItemClick(null, null);
        }

        private void newSubtitleLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.newSubtitleItem_ItemClick(null, null);
        }

        private void loadTxtLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.loadTranslationItem_ItemClick(null, null);
        }

        private void loadTranslationItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadTranslationFormPara para = new LoadTranslationFormPara();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "翻译稿|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                para.path = dialog.FileName;
                if ((new LoadTranslationForm(para)).ShowDialog() == DialogResult.OK)
                {
                        dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
                        dataProcess.DataInit();
                        this.dataProcess.Init();
                        pictureRefresh.Start();
                        //this.mplayer.Pause();//要求打开的时候是停止的。
                        this.mplayer.StepPlay();//要求打开的时候是停止的。
                        this.timeLineReadWrite = new TimeLineReadWrite();
                        this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, dialog.FileName, false);
                        string pathSave = timeLineReadWrite.filePath;
                        
                        this.timeLineReadWrite.filePath = para.path;
                        this.timeLineReadWrite.Read(new FileReadOridinary(para.isBlank));
                        this.timeLineReadWrite.filePath = pathSave;

                        this.rateShow.Focus();
                        this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());
                        this.listView1.YYRefresh();
                }
            }
        }

        private void addSubtitleLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.addSubtitleItem_ItemClick(null, null);
        }

        private void ccHandleLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.ccSubtitleHandleItem_ItemClick(null, null);
        }

        private void exportSubtitleItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.exportMutiSaveItem_ItemClick(null, null);
        }

        private void saveSubtitleLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.saveSubtitleItem_ItemClick(null, null);
        }

        private void merageToEndLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.combineOneLineItem_ItemClick(null, null);
        }

        private void combineOneLineContext_Click(object sender, EventArgs e)
        {
            this.combineOneLineItem_ItemClick(null, null);
        }

        private void combineOneLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                this.commandManage.CommandRun(new MergeToOne(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], this.listView1.SelectedIndices, true));
            }
        }

        private void subtitleCombineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.commandManage.CommandRun(new RemoveRecordEnter(this.timeLineReadWrite.listSingleSentence,this.listView1,this.listView1.SelectedIndices));
            this.listView1.YYRefresh();
        }

        private void combineMutiLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                this.commandManage.CommandRun(new MergeToOne(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], this.listView1.SelectedIndices, false));
            }
        }

        private void combineMutiLineContext_Click(object sender, EventArgs e)
        {
            this.combineMutiLineItem_ItemClick(null, null);
        }

        private void merageToNewLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.combineMutiLineItem_ItemClick(null, null);
        }

        private void cutLineLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.customSpiltContext_Click(null, null);
        }

        private void alignNowLineLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.onlyNowLineItem_ItemClick(null, null);
        }

        private void alignNextLineLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.afterNowLineItem_ItemClick(null, null);
        }

        private void alignAllLineLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.allLineItem_ItemClick(null, null);
        }

        private void alignToBeforeSignLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.lastSignItem_ItemClick(null, null);
        }

        private void alignToAfterSignLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                List<HandleRecordBass> command = new List<HandleRecordBass>();
                Double Diff = this.mplayer.nowTime - this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(this.listView1.SelectedIndices[0])].startTime;
                int endPosition = 0;
                for (int i = this.listView1.SelectedIndices[0]; i < this.listView1.SelectedIndices.Count; i++)
                {
                    if (this.listView1.yyItems[i].Checked)
                    {
                        endPosition = i;
                        break;
                    }
                }
                for (int i = this.listView1.SelectedIndices[0]; i <= endPosition; i++)
                {
                    SingleSentence singleSentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[this.listView1.YYGetRealPosition(i)]);
                    singleSentence.startTime += Diff;
                    singleSentence.endTime += Diff;
                    command.Add(new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, i, singleSentence));
                }
                this.commandManage.CommandRun(command);
            }
            this.listView1.YYRefresh();
        }

        private void translationTimeLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.translationTimeItem_ItemClick(null, null);
        }

        private void mergeTwoLanLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
          
        }

        private void findErrorLink_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.findErrorItem_ItemClick(null, null);
        }

        private void functionPanel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            this.FunctionActiveItem.Checked = false;
        }

        private void videoPanel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            this.videoActiveItem.Checked = false;
        }

        private void modifySubtitlePanel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            this.subtitleEditItem.Checked = false;
        }

        private void picTimeLinePanel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            this.subtitleShowActiveItem.Checked = false;
        }

        private void FunctionActiveItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!this.FunctionActiveItem.Checked)
            {
                this.functionPanel.Visibility = DockVisibility.Hidden;
            }
            else
            {
                this.functionPanel.Visibility = DockVisibility.Visible;
            }
        }

        private void subtitleEditItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!this.subtitleEditItem.Checked)
            {
                this.modifySubtitlePanel.Visibility = DockVisibility.Hidden;
            }
            else
            {
                this.modifySubtitlePanel.Visibility = DockVisibility.Visible;
            }
        }

        private void videoActiveItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!this.videoActiveItem.Checked)
            {
                this.videoPanel.Visibility = DockVisibility.Hidden;
            }
            else
            {
                this.videoPanel.Visibility = DockVisibility.Visible;
            }
        }

        private void subtitleShowActiveItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!this.subtitleShowActiveItem.Checked)
            {
                this.picTimeLinePanel.Visibility = DockVisibility.Hidden;
            }
            else
            {
                this.picTimeLinePanel.Visibility = DockVisibility.Visible;
            }
        }

        private void barButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
        {
            //(new SetShortCutForm(this.topBar)).ShowDialog();
            //this.topBar.
           // this.fileMenu.ItemLinks
        }

        private void closeItem_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mplayer.getPositionMark = false;
            if (commandManage.NeedSave)
            {
                if ((new IfSaveForm(this.timeLineReadWrite, this.originalSubtitlePath)).ShowDialog() != DialogResult.Cancel)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            this.SaveAllCheck();
        }
        /// <summary>
        /// 返回设置值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool GetConfigCheck(string str)
        {
            if (str.Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置设置值
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        private string SetConfigCheck(bool mark)
        {
            if (mark)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 存储所有选项
        /// </summary>
        private void SaveAllCheck()
        {
            Config.DefaultConfig["AutoSaveCheck"] = this.SetConfigCheck(this.saveAutoCheckItem2.Checked);
            Config.DefaultConfig["subSyncItem"] =this.SetConfigCheck(this.subSyncItem.Checked);
            Config.DefaultConfig["hideEffectsItem"] = this.SetConfigCheck(this.hideEffectsItem.Checked);
        }
        /// <summary>
        /// 读取所有选项
        /// </summary>
        private void ReadAllCheck()
        {
            this.saveAutoCheckItem2.Checked = this.GetConfigCheck(Config.DefaultConfig["AutoSaveCheck"]);
            this.subSyncItem.Checked = this.GetConfigCheck(Config.DefaultConfig["subSyncItem"]);
            this.hideEffectsItem.Checked = this.GetConfigCheck(Config.DefaultConfig["hideEffectsItem"]);
            this.videoActiveItem.Checked = this.GetPanelCheck(this.videoPanel);
            this.FunctionActiveItem.Checked = this.GetPanelCheck(this.functionPanel);
            this.subtitleShowActiveItem.Checked = this.GetPanelCheck(this.picTimeLinePanel);
            this.subtitleEditItem.Checked = this.GetPanelCheck(this.modifySubtitlePanel);

        }
        /// <summary>
        /// 获取panel是显示还是隐藏
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private bool GetPanelCheck(DockPanel panel)
        {
            if (panel.Visibility == DockVisibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void videoPanel_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void videoPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            this.DragLoadAll(s);
            //path = s[0];
            //if (mplayer == null)
            //{
            //    //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
            //    this.mplayer = MPlayer.GetMPlayer();
            //    dataProcess.DataInit();
            //}
            //else
            //{
            //    //mplayer.Clear();
            //    //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
            //    this.mplayer = MPlayer.GetMPlayer();
            //    this.dataProcess.mplayer = this.mplayer;
            //    dataProcess.DataInit();
            //}
            //this.mplayer.StartPlay(path);
            //this.mplayer.GetTotalTime();
            //this.dataProcess.Init();
            //pictureRefresh.Start();
            //this.rateShow.Focus();
            //this.mplayer.StepPlay();//要求打开的时候是停止的。
            //this.moviePath = Path.GetDirectoryName(path);
            //this.movieName = Path.GetFileNameWithoutExtension(path);
            ////到时候还要根据是什么类型的进行修改
            ////this.temporarySubtitlePath = this.moviePath + "\\" + this.movieName+".srt";
            //this.temporarySubtitlePath = "d:\\" + this.movieName + ".srt";
            //this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            this.DragLoadAll(s);
            // string path = "";
            //string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //path = s[0];
            //if (path.EndsWith(".srt") || path.EndsWith(".ass"))
            //{
            //    dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
            //    dataProcess.DataInit();
            //    this.dataProcess.Init();
            //    pictureRefresh.Start();
            //    this.mplayer.Pause();//要求打开的时候是停止的。

            //    this.timeLineReadWrite = new TimeLineReadWrite();
            //    this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, path, false);
            //    if (path.EndsWith("srt"))
            //    {
            //        this.timeLineReadWrite.ReadAllTimeline();
            //    }
            //    if (path.EndsWith("ass"))
            //    {
            //        this.timeLineReadWrite.ReadAllTimeLineAss();
            //    }
            //    this.originalSubtitlePath = path;//保存一个初始值。
            //    if (this.moviePath.Equals(""))
            //    {
            //        //还要修改后缀
            //        //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
            //        this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
            //    }
            //    else
            //    {
            //        this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
            //    }
            //    this.rateShow.Focus();
            //    this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());

            //    //使能 listviewmenu
            //    //this.SetListviewMenuEnable(true);
            //    //增加最近打开的文件
            //    this.recentFile.AddRecentFile(path);
            //}
            //else
            //{
            //    MessageBox.Show("请拖入.srt或.ass字幕");
            //}
            
            //this.rateShow.Focus();
        }
        private void DragLoadAll(string[] path)
        {
            bool ifDragSub = false;
            string moviePath=null;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].EndsWith(".avi") || path[i].EndsWith(".mkv") || path[i].EndsWith(".mp4") || path[i].EndsWith(".rmvb"))
                {
                    this.DragLoadMovie(path[i]);
                    moviePath=path[i];
                }
                if (path[i].EndsWith(".srt") || path[i].EndsWith(".ass"))
                {
                    this.DragLoadSub(path[i]);
                    ifDragSub = true;
                }
            }
            string subPath = CCHandle.GetMovieSub(moviePath);
            if (ifDragSub == false && moviePath != null&&subPath!=null)
            {
                if (MessageBox.Show("是否自动搜索并载入新字幕?\r\n【如确定，则现有字幕不保存直接关闭】", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.DragLoadSub(subPath);
                }

            }
            this.reloadSub_Click(null, null);
        }
        /// <summary>
        /// 拖拽读取字幕
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private bool DragLoadSub(string path)
        {
            if (path.EndsWith(".srt") || path.EndsWith(".ass"))
            {
                dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1, this.commandManage);
                dataProcess.DataInit();
                this.dataProcess.Init();
                pictureRefresh.Start();
                this.mplayer.Pause();//要求打开的时候是停止的。

                this.timeLineReadWrite = new TimeLineReadWrite();
                this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, path, false);
                if (path.EndsWith("srt"))
                {
                    //this.timeLineReadWrite.ReadAllTimeline();
                    this.timeLineReadWrite.Read(new FileReadSrt());
                }
                if (path.EndsWith("ass"))
                {
                    //this.timeLineReadWrite.ReadAllTimeLineAss();
                    this.timeLineReadWrite.Read(new FileReadAss());
                }
                this.originalSubtitlePath = path;//保存一个初始值。
                if (this.moviePath.Equals(""))
                {
                    //还要修改后缀
                    //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\save\\noname.srt";
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                }
                else
                {
                    this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
                }
                this.rateShow.Focus();
                this.SetListViewData(this.timeLineReadWrite.GetListSingleSentence());

                //使能 listviewmenu
                //this.SetListviewMenuEnable(true);
                //增加最近打开的文件
                this.recentFile.AddRecentFile(path);
            }
            else
            {
                //MessageBox.Show("请拖入.srt或.ass字幕");
                return false;
            }

            this.rateShow.Focus();
            return true;
        }
        /// <summary>
        /// 拖拽读取电影
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private bool DragLoadMovie(string path)
        {
            if (mplayer == null)
            {
                //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                this.mplayer = MPlayer.GetMPlayer();
                dataProcess.DataInit();
            }
            else
            {
                //mplayer.Clear();
                //mplayer = new MPlayer(this.videoPlayPanel.Handle.ToInt32());
                this.mplayer = MPlayer.GetMPlayer();
                this.dataProcess.mplayer = this.mplayer;
                dataProcess.DataInit();
            }
            this.mplayer.StartPlay(path);
            this.mplayer.GetTotalTime();
            this.dataProcess.Init();
            pictureRefresh.Start();
            this.rateShow.Focus();
            this.mplayer.StepPlay();//要求打开的时候是停止的。
            this.moviePath = Path.GetDirectoryName(path);
            this.movieName = Path.GetFileNameWithoutExtension(path);
            //到时候还要根据是什么类型的进行修改
            //this.temporarySubtitlePath = this.moviePath + "\\" + this.movieName+".srt";
            //this.temporarySubtitlePath = "d:\\" + this.movieName + ".srt";
            //this.temporarySubtitlePath = "d:\\" + "TimeMBuf" + ".srt";
            if (Config.DefaultConfig["TimeMBufPath"].Equals(""))
            {
                this.temporarySubtitlePath = "d:\\" + "TimeMBuf" + ".srt";
            }
            else
            {
                this.temporarySubtitlePath = Config.DefaultConfig["TimeMBufPath"] + "TimeMBuf" + ".srt";
            }
            this.timeLineReadWrite.filePath = this.temporarySubtitlePath;
            return true;
        }
        
        private void shorCutItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShortCutSettingsForm shortcutSettings = new ShortCutSettingsForm();
            shortcutSettings.Caption2id = this.caption2id;
            shortcutSettings.update = new UpdateInformer(this.bindShortCuts);
            shortcutSettings.ShowDialog();
            this.bindShortCuts();
        }
        public void bindShortCuts()
        {
            //bind shortcuts

            caption2id = new Hashtable();
            Hashtable categoryCount = new Hashtable();

            String sub = "";
            foreach (BarItem item in this.barManager1.Items)
            {
                if (item is BarButtonItem)
                {
                    if (item.Id > 0)
                    {
                        int order = 0;
                        if (item.Category.Name != "error")
                        {
                            sub = item.Category.Name + " - ";
                            if (categoryCount.ContainsKey(sub))
                            {
                                order = (int)categoryCount[sub] + 1;
                            }
                            else
                            {
                                order = 1;
                            }
                            categoryCount[sub] = order;
                        }
                        else
                        {
                            sub = "Category Bug,pls report to us.";
                        }
                        if (item.Visibility == BarItemVisibility.Always)
                        {
                            caption2id.Add(sub + this.get2Dig(order) + item.Caption, item.Id);//for display in shortcut settings
                            //shown as menuindex menu - commandindex command
                        }
                    }
                    Keys key = ShortCuts.Get(item.Id);
                    if (key != (Keys)0)
                    {
                        item.ItemShortcut = new BarShortcut(key);
                    }

                }
            }
        }
        /// <summary>
        /// 补齐
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private String get2Dig(int val)
        {
            String result = val.ToString();
            if (result.Length == 1)
            {
                result = "0" + result;
            }
            return result;
        }

        private void webSiteItem_ItemClick(object sender, ItemClickEventArgs e)
        {
             RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
             string s = key.GetValue("").ToString();
             System.Diagnostics.Process.Start(s.Substring(0, s.Length - 5), "http://www.yyets.com");
        }

        private void to23FPSItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.commandManage.CommandRun(new Turn25To23FPS(this.timeLineReadWrite.listSingleSentence, this.listView1));
        }

        private void to25FPSItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.commandManage.CommandRun(new Turn23To25FPS(this.timeLineReadWrite.listSingleSentence, this.listView1));
        }

        private void deleteItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if(this.listView1.SelectedIndices.Count>0)
            //{
            //this.commandManage.CommandRun(new DeleteRecord(this.timeLineReadWrite.listSingleSentence,this.listView1,this.listView1.SelectedIndices[0]));
            //}
            this.deleteLineContext_Click(null, null);
        }

        private void lastLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listView1.VisiblePosition + 1 < this.listView1.yyItems.Count)
            {
                NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", this.listView1.VisiblePosition+1);
            }
        }

        private void nextLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listView1.VisiblePosition - 1 >=0)
            {
                NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", this.listView1.VisiblePosition - 1);
            }
        }

        private void videoPlayPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void clearTimeItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.zeroTime_Click(null, null);
        }

        private void clearContentItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.zeroContentContext_Click(null, null);
        }


        #region 判断在listview选中几个值的情况下，哪些快捷键需要显示
        /// <summary>
        /// 设置按钮是否是可执行的
        /// </summary>
        private void SetItemEnable()
        {
            if (this.listView1.yyItems.Count == 0)
            {
                this.addOneLineItem.Enabled = false;
                this.addOneLineContext.Enabled = false;
                this.addMutiLineItem.Enabled = false;
                this.addMutiLineContext.Enabled = false;
                this.moveTimeContext.Enabled = false;
                
            }
        }
        #endregion

        private void moveTimeContext_Click(object sender, EventArgs e)
        {
            this.translationTimeItem_ItemClick(null, null);
        }


        #region 刷新TrackBar
        public void RefreshTrackBar(double time,double totalTime)
        {
            if (time < totalTime)
            {
                this.movieTrack.Invoke(new DRefreshTrackBar(this.RefreshTrackBarAction), time, totalTime);
            }
            else
            {
                this.movieTrack.Invoke(new DRefreshTrackBar(this.RefreshTrackBarAction), time, time);
            }
        }
        public delegate void DRefreshTrackBar(double time, double totalTime);
        public void RefreshTrackBarAction(double time,double totalTime)
        {
            if (!this.movieTrack.Focused)
            {
                this.movieTrack.Maximum = (int)totalTime;
                this.movieTrack.Value = (int)time;
                this.interval = (int)totalTime / 100;
                this.movieTrack.Refresh();
            }
        }
        #endregion

        private void videoPlayPanel_Click(object sender, EventArgs e)
        {
            this.mplayer.Pause();
        }

        private void confirmChangeItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[Int32.Parse(this.numEdit.Text)]);
                sentence.startTime = TimeLineReadWrite.TimeIn(timeEditStart.Text);
                sentence.endTime = TimeLineReadWrite.TimeIn(timeEditEnd.Text);
                sentence.content = contentEdit.Text;
                ChangeRecord record = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.YYGetShowPosition(Int16.Parse(this.numEdit.Text)), sentence);
                this.commandManage.CommandRun(record);
                this.listView1.YYRefresh();
            }
            catch
            {
                MessageBox.Show("输入格式有问题");
            }

        }

        private void selectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            NativeMethods.SelectAllItems(this.listView1);
            this.listView1.YYRefresh();
        }

        private void customSplitItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.customSpiltContext_Click(null, null);
        }

        private void contentEdit_Leave(object sender, EventArgs e)
        {
            this.confirmChange_Click(null, null);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveAutoItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void viewEffectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            (new SetAssForm()).ShowDialog();
            this.commandManage_AfterRunCommandFunction();
        }

        private double GetDelayTime()
        {
            string time = Config.DefaultConfig["DelayTime"];
            try
            {
                return Double.Parse(time);
            }
            catch
            {
                return 0;
            }
        }

        private void saveAutoCheckItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            
        }

        private void delayTimeItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            (new SetDelayTime()).ShowDialog();
        }

        private void loadSubEncodingItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            (new ChooseEncodingForm()).ShowDialog();
        }

        private void hideEffectsItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.listView1.IsShowEffect = !this.hideEffectsItem.Checked;
            this.listView1.YYRefresh();
        }

        private void saveAutoItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSaveAuto setSaveAuto = new SetSaveAuto();
            setSaveAuto.ShowDialog();
        }

        private void reloadSubItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            string path = this.timeLineReadWrite.filePath;
            if (this.originalSubtitlePath.EndsWith(".ass"))
            {
                path = path.Replace(".srt", ".ass");
                this.timeLineReadWrite.Write(new FileWriteAss(ChooseEncodingForm.GetAutoLoadSubEncoding(), path));
            }
            else if (this.originalSubtitlePath.EndsWith(".srt"))
            {
                path = path.Replace(".ass", ".srt");
                this.timeLineReadWrite.Write(new FileWriteSrt(ChooseEncodingForm.GetAutoLoadSubEncoding(), path));
            }
            this.mplayer.LoadTimeLine(path);
        }

        private void toolStripSyn_CheckedChanged(object sender, EventArgs e)
        {

                MPlayer.RefreshMark =this.toolStripSyn.Checked;
                this.subSyncItem.Checked = this.toolStripSyn.Checked;
        }

        private void reloadSub_Click(object sender, EventArgs e)
        {
            this.reloadSubItem_ItemClick(null, null);
        }
    }
}
