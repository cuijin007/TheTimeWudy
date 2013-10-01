using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev
{
    class DataProcess
    {
        PictureRefresh pictureRefresh;
        ListView listView;
        int clickPositionX;//鼠标按下的位置
        int clickState;//点击的状态，是在哪个栏里面
        int slideState;//分为0,1,2三种，0为没有点中，1为点中左边，2为点中右面
        int slideCount = -1;//点中的滑块
        int totalProcessState;//分为0,1两种，0为点中，1为没点中
        int detailProcessState;//详细状态，分为0，1两种，0为没有在时间轴区域，既可以新建时间轴，1，为在时间轴上
        int detailProcessState2;//0为拖动，1为新建时间轴
        double slideStartBuffer, slideEndBuffer;//存储按下的时候那个位置的时间
        double detailSelectedTimeBuffer, detailNowTimeBuffer;//detailSelectedTimeBuffer为选择时间缓冲区，detailnowtimebuffer为现有时间暂存区
        double timeBuffer;
        public List<SingleSentence> listSingleSentence = new List<SingleSentence>();

        Cursor cursor;
        MPlayer mplayer;
        public DataProcess(PictureRefresh pictureRefresh, Cursor cursor, MPlayer mplayer, ListView listView)
        {
            this.pictureRefresh = pictureRefresh;
            this.cursor = cursor;
            this.mplayer = mplayer;
            this.mplayer.DplayerRateAction = this.GetTimeAction;
            this.listView = listView;
        }
        public void Init()
        {
            pictureRefresh.totalTime = mplayer.totalTime;
        }
        public void DataInit()
        {
            pictureRefresh.listSingleSentence = this.listSingleSentence;
            if (this.pictureRefresh.listSingleSentence == null)
            {
                pictureRefresh.listSingleSentence = new List<SingleSentence>();
                SingleSentence singleSentence = new SingleSentence();
                singleSentence.content = "请勿点击时间轴，请载入字幕文件";
                singleSentence.startTime = 0;
                singleSentence.endTime = 0.01;
                pictureRefresh.listSingleSentence.Add(singleSentence);

                SingleSentence singleSentence3 = new SingleSentence();
                singleSentence3.content = "请勿点击时间轴，请载入字幕文件";
                singleSentence3.startTime = 36000;
                singleSentence3.endTime = 36000;
                pictureRefresh.listSingleSentence.Add(singleSentence3);
            }

        }
        public void MouseDown(object sender, MouseEventArgs e)
        {
            this.clickPositionX = e.X;
            clickState = 0;
            this.timeBuffer = pictureRefresh.NowTime;
            if (e.Y > pictureRefresh.totalProgressS && e.Y < pictureRefresh.totalProgressE)
            {
                clickState = 1;//点在总轴上了
                this.TotalProcessJudge(sender, e);
            }
            if (e.Y > pictureRefresh.slideS && e.Y < pictureRefresh.slideE)
            {
                clickState = 2;//点在滑块轴上了
                this.SlideJudge(sender, e);
            }
            if (e.Y > pictureRefresh.detailS && e.Y < pictureRefresh.detailE)
            {
                clickState = 3;//点在详细信息轴上了
                this.DetailProcessJudge(sender, e);
            }
        }
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (slideState != 0)
            {
                this.SlideActionOver(sender, e);

            }
            clickState = 0;//清零
            this.totalProcessState = 0;
            this.slideEndBuffer = 0;
            this.slideStartBuffer = 0;
            this.cursor = System.Windows.Forms.Cursors.Arrow;
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (clickState == 1)
            {
                this.TotalProcessAction(sender, e);
            }
            if (clickState == 2)
            {
                this.SlideAction(sender, e);
            }
            if (clickState == 3)
            {
                this.DetailProcessAction(sender, e);
            }
        }
        public void MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                double timeSpan = this.pictureRefresh.SelectedTime - this.pictureRefresh.NowTime;//2013-4-11增加滚轮判断功能，使得不会发生越界
                if (this.pictureRefresh.width * this.pictureRefresh.UnitBelow / this.pictureRefresh.OneUnitPix < this.pictureRefresh.totalTime)
                {
                    this.pictureRefresh.UnitBelow = this.pictureRefresh.UnitBelow * 2;
                    this.pictureRefresh.NowTime = this.pictureRefresh.SelectedTime - timeSpan * 2;
                }
            }
            if (e.Delta > 0)
            {
                double timeSpan = this.pictureRefresh.SelectedTime - this.pictureRefresh.NowTime;

                this.pictureRefresh.UnitBelow = this.pictureRefresh.UnitBelow / 2;
                this.pictureRefresh.NowTime = this.pictureRefresh.SelectedTime - timeSpan / 2;

            }
        }

        private void TotalProcessJudge(object sender, MouseEventArgs e)
        {
            int startPosition = (int)((pictureRefresh.NowTime / pictureRefresh.totalTime) * pictureRefresh.width);
            int endPosition = (int)(startPosition + ((pictureRefresh.width * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix) / pictureRefresh.totalTime) * pictureRefresh.width);
            if (e.X > startPosition && e.X < endPosition)
            {
                /*
                this.cursor = System.Windows.Forms.Cursors.UpArrow;
                pictureRefresh.nowTime = this.timeBuffer + ((e.X - clickPositionX) / pictureRefresh.width) * pictureRefresh.totalTime;
            
                 */
                this.totalProcessState = 1;
            }
        }
        private void TotalProcessAction(object sender, MouseEventArgs e)
        {
            if (this.totalProcessState == 1)
            {
                double time = this.timeBuffer + ((double)(e.X - clickPositionX) * pictureRefresh.totalTime / (double)pictureRefresh.width);
                double timeLength = ((double)(this.pictureRefresh.width / this.pictureRefresh.OneUnitPix) * this.pictureRefresh.UnitBelow);
                if (time >= 0 && time <= pictureRefresh.totalTime - timeLength)
                {
                    this.pictureRefresh.NowTime = time;
                }
                if (time < 0)
                {
                    this.pictureRefresh.NowTime = 0;
                }
                if (time > pictureRefresh.totalTime - timeLength)
                {
                    this.pictureRefresh.NowTime = this.pictureRefresh.totalTime - timeLength;
                }
            }
        }
        /// <summary>
        /// 判断滑块是不是点中，点中了是左滑块还是右滑块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlideJudge(object sender, MouseEventArgs e)//判断滑块位置
        {
            slideState = 0;
            for (int i = 0; i < this.pictureRefresh.listSlideInfo.Count; i++)
            {
                if (e.X > pictureRefresh.listSlideInfo[i].startPosition && e.X < pictureRefresh.listSlideInfo[i].startPosition + 10)
                {
                    this.slideState = 1;
                    this.slideCount = pictureRefresh.listSlideInfo[i].num;
                    this.slideStartBuffer = pictureRefresh.listSingleSentence[slideCount].startTime;
                    this.slideEndBuffer = pictureRefresh.listSingleSentence[slideCount].endTime;
                }
                if (e.X > pictureRefresh.listSlideInfo[i].endPosition - 10 && e.X < pictureRefresh.listSlideInfo[i].endPosition)
                {
                    this.slideState = 2;
                    this.slideCount = pictureRefresh.listSlideInfo[i].num;
                    this.slideStartBuffer = pictureRefresh.listSingleSentence[slideCount].startTime;
                    this.slideEndBuffer = pictureRefresh.listSingleSentence[slideCount].endTime;
                }
                if (e.X > pictureRefresh.listSlideInfo[i].startPosition + 11 && e.X < pictureRefresh.listSlideInfo[i].endPosition - 11)
                {
                    this.slideState = 3;
                    this.slideCount = pictureRefresh.listSlideInfo[i].num;
                    this.slideStartBuffer = pictureRefresh.listSingleSentence[slideCount].startTime;
                    this.slideEndBuffer = pictureRefresh.listSingleSentence[slideCount].endTime;
                }
            }
        }
        /// <summary>
        /// 点中滑块的函数操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlideAction(object sender, MouseEventArgs e)
        {
            if (slideState == 1)
            {
                double time = (e.X - this.clickPositionX) * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix;
                time = time - time % 0.001;
                if (this.slideEndBuffer > (this.slideStartBuffer + time))
                    pictureRefresh.listSingleSentence[this.slideCount].startTime = this.slideStartBuffer + time;
            }
            if (slideState == 2)
            {
                double time = (e.X - this.clickPositionX) * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix;
                time = time - time % 0.001;
                if ((this.slideEndBuffer + time) > this.slideStartBuffer)
                    pictureRefresh.listSingleSentence[this.slideCount].endTime = this.slideEndBuffer + time;
            }
            if (slideState == 3)
            {
                double time = (e.X - this.clickPositionX) * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix;
                time = time - time % 0.001;
                if (this.slideEndBuffer + time < this.pictureRefresh.totalTime && this.slideStartBuffer + time > 0)
                {
                    pictureRefresh.listSingleSentence[this.slideCount].endTime = this.slideEndBuffer + time;
                    pictureRefresh.listSingleSentence[this.slideCount].startTime = this.slideStartBuffer + time;
                }
            }
            pictureRefresh.slideCount = this.slideCount;
        }

        /// <summary>
        /// 滑块处理完成，这个主要是为了处理ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SlideActionOver(object sender, MouseEventArgs e)
        {
            if (this.slideCount >= 0)
            {
                string[] item = new string[4];
                item[0] = this.slideCount + "";//2013-4-10去掉+1
                item[1] = this.TimeOut(this.pictureRefresh.listSingleSentence[slideCount].startTime);
                item[2] = this.TimeOut(this.pictureRefresh.listSingleSentence[slideCount].endTime);
                item[3] = this.pictureRefresh.listSingleSentence[slideCount].content;
                ListViewItem listViewItem = new ListViewItem(item);
                this.listView.Items.RemoveAt(slideCount - 1);//2013-4-10
                this.listView.Items.Insert(slideCount - 1, listViewItem);
            }
            this.slideCount = -1;
            this.pictureRefresh.slideCount = -1;
        }

        /// <summary>
        /// 详细轴上的判断选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailProcessJudge(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.detailProcessState2 = 0;
                this.detailNowTimeBuffer = pictureRefresh.NowTime;
            }
            if (e.Button == MouseButtons.Right)
            {
                this.pictureRefresh.temporaryTimeLine.isSelected = false;
                this.detailProcessState2 = 1;
                this.pictureRefresh.SelectedTime = this.pictureRefresh.NowTime + (e.X * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix);
                //*设置视频位置*//
                this.mplayer.SeekPosition(pictureRefresh.SelectedTime);
                //****//
                this.pictureRefresh.temporaryTimeLine.startTime = this.pictureRefresh.SelectedTime;
                int numBuffer = -1;
                if (pictureRefresh.listSlideInfo.Count > 0)
                {
                    if (this.clickPositionX < pictureRefresh.listSlideInfo[0].startPosition)
                    {
                        numBuffer = pictureRefresh.listSlideInfo[0].num;
                        pictureRefresh.temporarySlideInfo.num = numBuffer;
                    }
                    if (numBuffer < 0)
                    {
                        for (int i = 0; i < pictureRefresh.listSlideInfo.Count - 1; i++)
                        {
                            if (pictureRefresh.listSlideInfo[i].endPosition < this.clickPositionX && pictureRefresh.listSlideInfo[i + 1].startPosition > this.clickPositionX)
                            {
                                pictureRefresh.temporarySlideInfo.num = pictureRefresh.listSlideInfo[i].num;
                                numBuffer = pictureRefresh.listSlideInfo[i].num;
                            }
                        }
                    }
                    if (numBuffer < 0)
                    {
                        this.pictureRefresh.temporarySlideInfo.num = pictureRefresh.listSlideInfo[pictureRefresh.listSlideInfo.Count - 1].num;
                    }
                }
            }
        }
        /// <summary>
        /// 按在详细轴上的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailProcessAction(object sender, MouseEventArgs e)
        {
            //拖动绘制
            if (this.detailProcessState2 == 0)
            {
                double time = this.timeBuffer + (e.X - this.clickPositionX) * pictureRefresh.UnitBelow / -pictureRefresh.OneUnitPix;
                if (time >= 0 && time <= pictureRefresh.totalTime)
                {
                    this.pictureRefresh.NowTime = time;
                }
                if (time <= 0)
                {
                    this.pictureRefresh.NowTime = 0;
                }
                if (time > pictureRefresh.totalTime)
                {
                    this.pictureRefresh.NowTime = pictureRefresh.totalTime;
                }
            }
            if (this.detailProcessState2 == 1)
            {

                double endTime = this.pictureRefresh.temporaryTimeLine.startTime + (e.X - this.clickPositionX) * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix;
                if (endTime > this.pictureRefresh.temporaryTimeLine.startTime)
                {
                    this.pictureRefresh.temporaryTimeLine.isSelected = true;
                    this.pictureRefresh.temporaryTimeLine.endTime = endTime;
                }
            }
        }
        private void GetTimeAction(double time, double totalTime)
        {
            this.pictureRefresh.totalTime = totalTime;
            double showTime = pictureRefresh.width * pictureRefresh.UnitBelow / pictureRefresh.OneUnitPix;
            if (time - pictureRefresh.NowTime > showTime / 2)
            {
                double timeCut = time - pictureRefresh.SelectedTime;
                pictureRefresh.SelectedTime = time;
                pictureRefresh.NowTime += timeCut;
            }
            else
            {
                pictureRefresh.SelectedTime = time;
            }
        }
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    {
                        this.mplayer.Pause();
                        break;
                    }
                case Keys.F12:
                    {
                        this.mplayer.Forward(10);
                        if (this.pictureRefresh.SelectedTime + 10 < this.pictureRefresh.totalTime)
                            this.pictureRefresh.NowTime = this.pictureRefresh.SelectedTime + 10;
                        break;
                    }
                case Keys.F11:
                    {
                        this.mplayer.BackWard(10);
                        if (this.pictureRefresh.SelectedTime - 10 > 0)
                            this.pictureRefresh.NowTime = this.pictureRefresh.SelectedTime - 10;
                        break;
                    }
                //增加时间轴的起始位置
                case Keys.F6:
                    {
                        this.pictureRefresh.temporaryTimeLine.startTime = this.pictureRefresh.SelectedTime;
                        this.pictureRefresh.temporaryTimeLine.endTime = this.pictureRefresh.SelectedTime + 0.2;
                        this.pictureRefresh.temporaryTimeLine.isKeyBoard = true;
                        this.pictureRefresh.temporaryTimeLine.isSelected = true;
                        break;
                    }
                //增加时间轴的结束位置
                case Keys.F7:
                    {
                        if (this.pictureRefresh.temporaryTimeLine.isKeyBoard == true)
                        {
                            this.pictureRefresh.temporaryTimeLine.endTime = this.pictureRefresh.SelectedTime;
                            this.pictureRefresh.temporaryTimeLine.isSelected = false;
                            this.pictureRefresh.temporaryTimeLine.isKeyBoard = false;
                            int mark = -1;
                            int i = 0;
                            if (this.pictureRefresh.listSingleSentence.Count == 1)
                            {
                                this.pictureRefresh.listSingleSentence.Add(this.pictureRefresh.temporaryTimeLine);
                                break;
                            }
                            for (i = 0; i < this.pictureRefresh.listSingleSentence.Count; i++)
                            {
                                if (this.pictureRefresh.listSingleSentence[i].startTime > this.pictureRefresh.SelectedTime && this.pictureRefresh.listSingleSentence[i - 1].endTime < this.pictureRefresh.SelectedTime)
                                {
                                    mark = i;
                                    SingleSentence singleSentenceBuffer = new SingleSentence();
                                    singleSentenceBuffer.startTime = this.pictureRefresh.temporaryTimeLine.startTime;
                                    singleSentenceBuffer.endTime = this.pictureRefresh.SelectedTime;
                                    singleSentenceBuffer.isSelected = false;
                                    singleSentenceBuffer.isKeyBoard = false;
                                    this.pictureRefresh.listSingleSentence.Insert(i, singleSentenceBuffer);

                                    //2013-4-10增加之后在listview上也更新
                                    if (i > 0)
                                    {
                                        string[] itemStr = new string[4];
                                        itemStr[0] = i + "";
                                        itemStr[1] = this.TimeOut(this.pictureRefresh.temporaryTimeLine.startTime);
                                        itemStr[2] = this.TimeOut(this.pictureRefresh.temporaryTimeLine.endTime);
                                        itemStr[3] = this.pictureRefresh.temporaryTimeLine.content;
                                        ListViewItem item = new ListViewItem(itemStr);
                                        this.listView.Items.Insert(i - 1, item);
                                    }


                                    break;
                                }
                            }
                            if (mark == -1)
                            {
                                this.pictureRefresh.listSingleSentence.Add(this.pictureRefresh.temporaryTimeLine);
                            }
                        }
                        break;
                    }
                //右键划出来一部分，然后按f4添加
                case Keys.F4:
                    {
                        if (this.pictureRefresh.temporaryTimeLine.isSelected == true)
                        {
                            int mark = -1;
                            int i = 0;
                            if (this.pictureRefresh.listSingleSentence.Count == 1)
                            {
                                this.pictureRefresh.listSingleSentence.Add(this.pictureRefresh.temporaryTimeLine);
                                break;
                            }
                            if (this.pictureRefresh.listSingleSentence[0].startTime > this.pictureRefresh.SelectedTime)
                            {
                                mark = 0;
                                SingleSentence singleSentenceBuffer = new SingleSentence();
                                singleSentenceBuffer.startTime = this.pictureRefresh.temporaryTimeLine.startTime;
                                singleSentenceBuffer.endTime = this.pictureRefresh.temporaryTimeLine.endTime;
                                singleSentenceBuffer.isSelected = false;
                                singleSentenceBuffer.isKeyBoard = false;
                                this.pictureRefresh.listSingleSentence.Insert(mark, singleSentenceBuffer);
                            }
                            else
                            {
                                for (i = 1; i < this.pictureRefresh.listSingleSentence.Count; i++)
                                {
                                    if (this.pictureRefresh.listSingleSentence[i].startTime > this.pictureRefresh.SelectedTime && this.pictureRefresh.listSingleSentence[i - 1].endTime < this.pictureRefresh.SelectedTime)
                                    {
                                        mark = i;
                                        SingleSentence singleSentenceBuffer = new SingleSentence();
                                        singleSentenceBuffer.startTime = this.pictureRefresh.temporaryTimeLine.startTime;
                                        singleSentenceBuffer.endTime = this.pictureRefresh.temporaryTimeLine.endTime;
                                        singleSentenceBuffer.isSelected = false;
                                        singleSentenceBuffer.isKeyBoard = false;
                                        this.pictureRefresh.listSingleSentence.Insert(i, singleSentenceBuffer);

                                        //2013-4-10增加之后在listview上也更新
                                        if (i > 0)
                                        {
                                            string[] itemStr = new string[4];
                                            itemStr[0] = i + "";
                                            itemStr[1] = this.TimeOut(this.pictureRefresh.temporaryTimeLine.startTime);
                                            itemStr[2] = this.TimeOut(this.pictureRefresh.temporaryTimeLine.endTime);
                                            itemStr[3] = this.pictureRefresh.temporaryTimeLine.content;
                                            ListViewItem item = new ListViewItem(itemStr);
                                            this.listView.Items.Insert(i - 1, item);
                                        }

                                        break;
                                    }
                                }
                            }
                            if (mark == -1)
                            {
                                this.pictureRefresh.listSingleSentence.Add(this.pictureRefresh.temporaryTimeLine);
                            }
                        }
                        break;
                    }
                //删除功能2013-4-10
                case Keys.F3:
                    {
                        for (int i = 0; i < pictureRefresh.listSingleSentence.Count; i++)
                        {
                            if (pictureRefresh.listSingleSentence[i].isSelected)
                            {
                                pictureRefresh.listSingleSentence.RemoveAt(i);
                                this.listView.Items.RemoveAt(i - 1);

                            }
                        }
                        break;
                    }
            }
        }
        public void SetListSingleSentence(List<SingleSentence> listSengleSentence)
        {
            this.pictureRefresh.listSingleSentence = listSengleSentence;
            this.listSingleSentence = listSengleSentence;
        }
        /// <summary>
        /// 选择listview上双击某一项的时候的响应
        /// </summary>
        public void ListViewSelectedAction(int index)
        {
            //this.pictureRefresh.SelectedTime = this.pictureRefresh.listSingleSentence[index].startTime + 0.01;
            this.mplayer.SeekPosition(this.pictureRefresh.listSingleSentence[index].startTime + 0.01);
            this.pictureRefresh.NowTime = this.pictureRefresh.listSingleSentence[index].startTime;
        }
        public string TimeOut(double time)
        {
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            int minsec = (int)((time - (int)time) * 1000);
            string TimeStr = hour + ":" + minute + ":" + second + "," + minsec;
            return TimeStr;
        }
        public double TimeIn(string time)
        {
            string[] timeBuffer;
            string[] spiltChar = { ",", "，", ":" };
            timeBuffer = time.Split(spiltChar, StringSplitOptions.RemoveEmptyEntries);
            if (timeBuffer.Length < 4)
            {
                return -1;
            }
            else
            {
                double timedouble = Double.Parse(timeBuffer[0]) * 3600 + Double.Parse(timeBuffer[1]) * 60 + Double.Parse(timeBuffer[2]) + Double.Parse(timeBuffer[3]) * 0.001;
                return timedouble;
            }
        }
    }
}
