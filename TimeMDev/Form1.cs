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

namespace TimeMDev
{
    public partial class Form1 : Form
    {
        PictureRefresh pictureRefresh;
        double timeBuffer;
        int startLocationX;
        bool startMark = false;
        DataProcess dataProcess;
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
            this.timeLineReadWrite.WriteAllTimelineAss();
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
            int index = this.listView1.SelectedIndices[this.listView1.SelectedIndices.Count - 1];
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
       
    }
}
