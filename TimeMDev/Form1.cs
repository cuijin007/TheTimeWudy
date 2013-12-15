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
        CommandManage commandManage = new CommandManage(10);
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
            dataProcess = new DataProcess(pictureRefresh, this.Cursor, mplayer, listView1,this.commandManage);
            dataProcess.DataInit();
            this.rateShow.MouseWheel+=new MouseEventHandler(rateShow_MouseWheel);
            originalSubtitlePath = "";
            moviePath = "";
            //temporarySubtitlePath = System.AppDomain.CurrentDomain.BaseDirectory+"\\save\\noname.srt";
            temporarySubtitlePath = "d:\\yyets~c~.srt";

            //初始的时候禁止listviewmenu显示
            //this.SetListviewMenuEnable(false);
            this.NotificationInit();
            //this.recentFile.OnItemClickAction += new OnItemClick(recentFile_OnItemClickAction);
            this.commandManage.AfterRunCommandFunction += new AfterRunCommandFuncionD(commandManage_AfterRunCommandFunction);
        }
        /// <summary>
        /// 每个command执行完之后的动作
        /// </summary>
        void commandManage_AfterRunCommandFunction()
        {
         //   this.timeLineReadWrite.Write(new FileWriteSrt());
            this.mplayer.LoadTimeLine(this.timeLineReadWrite.filePath);
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
                this.mplayer.Pause();//要求打开的时候是停止的。

                this.timeLineReadWrite = new TimeLineReadWrite();
                this.timeLineReadWrite.Init(this.pictureRefresh.listSingleSentence, path, false);
                if (path.EndsWith("srt"))
                {
                    this.timeLineReadWrite.ReadAllTimeline();
                }
                if (path.EndsWith("ass"))
                {
                    this.timeLineReadWrite.ReadAllTimeLineAss();
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
                //this.temporarySubtitlePath = this.moviePath + "\\" + this.movieName+".srt";
                this.temporarySubtitlePath = "d:\\" + this.movieName + ".srt";
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
            if ((new IfSaveForm(this.timeLineReadWrite, this.originalSubtitlePath)).ShowDialog() != DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
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
                    this.timeLineReadWrite.ReadAllTimeline();
                }
                if (dialog.FileName.EndsWith("ass"))
                {
                    this.timeLineReadWrite.ReadAllTimeLineAss();
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
            for (int i = 1; i < listSingleSentence.Count - 1; i++)
            {
                //showBuffer[0] = i + 1 + "";
                showBuffer[0] = i + "";//2013-4-10
                showBuffer[1] = TimeOut(listSingleSentence[i].startTime);
                showBuffer[2] = TimeOut(listSingleSentence[i].endTime);
                showBuffer[3] = listSingleSentence[i].content;
                showBuffer[4] = listSingleSentence[i].everyLineLength;
                showBuffer[5] = TimeLineReadWrite.TimeOut(listSingleSentence[i].timeLength);
                showBuffer[6] = listSingleSentence[i].lineNum+"";
               
                this.listView1.yyItems.Add(new ListViewItem(showBuffer));
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
            else if (this.listView1.Order == SortOrder.Descending)
            {
                this.listView1.Order = SortOrder.Ascending;
            }
            if (this.listView1.Order == SortOrder.None)
            {
                this.listView1.Order = SortOrder.Ascending;
            }
            //this.listView1.YYSort();
            ListViewSort sort = new ListViewSort(this.listView1, e.Column, this.listView1.SortStyle, this.listView1.Order);
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
                AddBlankRecord addBlankRecord = new AddBlankRecord(this.dataProcess.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], 1, 1000, true);
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
            for(int i=0;i<this.listView1.SelectedIndices.Count;i++)
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
            CopyRecord copyRecord = new CopyRecord(this.dataProcess, this.listView1, this.listView1.SelectedIndices);
            this.commandManage.CommandRunNoRedo(copyRecord);
            this.listView1.YYRefresh();
        }

        private void cutContext_Click(object sender, EventArgs e)
        {
            CutRecord cutRecord = new CutRecord(this.dataProcess, this.listView1, this.listView1.SelectedIndices);
            this.commandManage.CommandRun(cutRecord);
            this.listView1.YYRefresh();
        }

        private void pasteContext_Click(object sender, EventArgs e)
        {
            PasteRecord pasteRecord=new PasteRecord(this.dataProcess,this.listView1,this.listView1.SelectedIndices[0]+1);
            this.commandManage.CommandRun(pasteRecord);
            this.listView1.YYRefresh();
        }

        private void checkAllContext_Click(object sender, EventArgs e)
        {

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
                    this.timeLineReadWrite.listSingleSentence.Add(new SingleSentence());
                    
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
                SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.timeLineReadWrite.listSingleSentence[Int32.Parse(this.numEdit.Text)]);
                sentence.startTime = TimeLineReadWrite.TimeInAss(timeEditStart.Text);
                sentence.endTime = TimeLineReadWrite.TimeInAss(timeEditEnd.Text);
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

        private void saveSubtitleItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string buf=this.timeLineReadWrite.filePath;
            this.timeLineReadWrite.filePath=this.originalSubtitlePath;
            this.timeLineReadWrite.Write(new FileWriteSrt(Encoding.Default));
            this.timeLineReadWrite.filePath = buf;
        }

        private void saveAsItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            (new SaveAsForm(this.timeLineReadWrite)).ShowDialog();
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
                singleSentence.startTime = this.mplayer.nowTime;
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
                singleSentence.endTime = this.mplayer.nowTime;
                ChangeRecord changeRecord = new ChangeRecord(this.timeLineReadWrite.listSingleSentence, this.listView1, this.listView1.SelectedIndices[0], singleSentence);
                this.commandManage.CommandRun(changeRecord);
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
            if (extendForm.DialogResult == DialogResult.OK)
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

       
    }
}
