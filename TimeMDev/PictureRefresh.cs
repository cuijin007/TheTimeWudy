using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using TimeMDev.Notification;

namespace TimeMDev
{
   public class PictureRefresh
    {
        public delegate void DRefreshAction();
        DRefreshAction refreshAction;
        Thread RefreshPictureThread;
        PictureBox pictureBox;
        Bitmap bitmap;
        Graphics graphics;
        Form1 form1;
        public bool runMark = true;
        bool isRefreshContentShow = true;
        public int slideCount=-1;//从dataProcess赋值给他，他知道选的是哪个
       

        #region 外界选中和刷新部分的变量初始化
        /// <summary>
       /// 存储刷新的list
       /// </summary>
       private List<int> selectedIndex=new List<int>();
       /// <summary>
       /// 刷新标志
       /// </summary>
       bool selectedRefreshMark;//是不是要刷新的标志
       /// <summary>
       /// 获取选中的那些条目的消息(不设置更新标志)
       /// </summary>
       public List<int> SelectedIndexInfo
        {
            get 
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
            }
        }
       /// <summary>
       /// 获取选中的条目，（设置更新，外界会随着更新）
       /// </summary>
       public List<int> SelectedIndexWidthChange
       {
           get
           {
               this.selectedRefreshMark = true;
               return this.selectedIndex;
           }
           set
           {
               this.selectedRefreshMark = true;
               this.selectedIndex = value;
           }
       }
       /// <summary>
       /// 是否要跟踪刷新
       /// </summary>
       public bool isTrackFollow=false;
        #endregion
       public List<SingleSentence> listSingleSentence=new List<SingleSentence>();
       public List<SlideInfo> listSlideInfo = new List<SlideInfo>();
       public SingleSentence temporaryTimeLine = new SingleSentence();
       public SlideInfo temporarySlideInfo = new SlideInfo();
       public double NowTime
       {
           get 
           {
               return this.nowTime;
           }
           set 
           {
               if (value < 0)
               {
                   this.nowTime = 0;
               }
               else if ((value + (width / oneUnitPix) * UnitBelow) <= this.totalTime)
               {
                   this.nowTime = value;
               }
               else if ((value + (width / oneUnitPix) * UnitBelow) > this.totalTime)
               {
                   this.nowTime = this.totalTime - (width / oneUnitPix) * UnitBelow;
               }
           }
       }
       double nowTime;//现在播放的时间
       double oneUnitPix = 80;
        public double OneUnitPix
        {
            get
            {
                return oneUnitPix;
            }
            set
            {
                this.oneUnitPix=value;
                this.unitBelow=width/(oneUnitPix*10);
            }
        }
        private double unitBelow=1;//每一个坐标之间的差值，恒定显示十个坐标
        public double UnitBelow
        {
            get { return this.unitBelow; }
            set 
            {
                this.unitBelow = value;
             
            }
        }
       public double totalTime=4000;//总时间 
       public int height, width;//显示区域的长度
        double selectedTime;
        public double SelectedTime
        {
            get { return selectedTime; }
            set { selectedTime = value; }
        }
        public int totalProgressS,totalProgressE,cutS,cutE,slideS,slideE,detailS,detailE,timeS,timeE;//差不多先分着几部分,都是y值
        private Color colorBackGroundMain,colorCut,colorMainProgress,colorTime,colorSlide,colorDetail,colorDetailSelected,colorDetailLine,colorWord,colorWordSelected,colorTimeWord,colorLine;
        private Brush  brushBackGroundMain,brushCut,brushMainProgress,brushTime,brushSlide,brushDetail,brushDetailSelected,brushDetailLine,brushWord,brushWordSelected,brushTimeWord,brushLine;
        public PictureRefresh(Form1 form)
        {
            this.form1 = form;
            this.pictureBox = form1.rateShow;
            this.InitConstant(pictureBox);
            this.height = pictureBox.Height;
            this.width = pictureBox.Width;

            bitmap = new Bitmap(this.pictureBox.Width, this.pictureBox.Height);
            this.graphics = Graphics.FromImage(this.bitmap);
            this.refreshAction = new DRefreshAction(this.RefreshAction);
            this.RefreshPictureThread = new Thread(new ThreadStart(this.DrawBitmap));
            
        }        
        public void Start()
        {
            if (!this.RefreshPictureThread.IsAlive)
            {
                this.RefreshPictureThread.Start();
            }
        }
        public void RefreshAction()
        {
            this.pictureBox.Image = this.bitmap;
            this.RefreshOtherControl();
            this.pictureBox.Refresh();
            //增加判断是否要刷新界面   2013-4-9 崔进
            this.isRefreshContentShow = IsRefreshContentShow();
            //this.form1.Refresh();
        }
        public void InitConstant(PictureBox pictureBox)
        {
            this.height=pictureBox.Height;
            this.width=pictureBox.Width;
            this.totalProgressS=0;
            this.totalProgressE=(int)(this.totalProgressS+height*0.1);
            this.cutS=totalProgressE;
            this.cutE=(int)(cutS+height*0.05);
            this.slideS=this.cutE;
            this.slideE=(int)(this.slideS+height*0.15);
            this.detailS=this.slideE;
            this.detailE=(int)(this.detailS+height*0.45);
            this.timeS=this.detailE;
            this.timeE=this.height;
            

            this.colorBackGroundMain=Color.FromArgb(53,54,57);
            this.colorCut=Color.FromArgb(98,108,123);
            this.colorMainProgress=Color.FromArgb(53,54,57);
            this.colorTime=Color.FromArgb(65,71,81);
            this.colorSlide=Color.FromArgb(75,243,167);
            this.colorDetail=Color.DarkGray;
            this.colorDetailSelected=Color.White;
            this.colorDetailLine=Color.YellowGreen;
            this.colorWord=Color.White;
            this.colorWordSelected=Color.Black;
            this.colorLine=Color.YellowGreen;

            this.brushBackGroundMain=new SolidBrush(this.colorBackGroundMain);
            this.brushCut=new SolidBrush(this.colorCut);
            this.brushMainProgress=new SolidBrush(this.colorMainProgress);
            this.brushTime=new SolidBrush(this.colorTime);
            this.brushSlide=new SolidBrush(this.colorSlide);
            this.brushDetail=new SolidBrush(this.colorDetail);
            this.brushDetailSelected=new SolidBrush(this.colorDetailSelected);
            this.brushDetailLine=new SolidBrush(this.colorDetailLine);
            this.brushWord=new SolidBrush(this.colorWord);
            this.brushWordSelected=new SolidBrush(this.colorWordSelected);
            this.brushLine=new SolidBrush(this.colorLine);
        }
       /// <summary>
       /// 绘制界面
       /// </summary>
       private void DrawBitmap()
        {
            while (this.runMark)
            {
                if (this.height != pictureBox.Height || this.width != pictureBox.Width)
                {
                    bitmap = new Bitmap(this.pictureBox.Width, this.pictureBox.Height);
                    this.graphics = Graphics.FromImage(this.bitmap);
                    this.InitConstant(this.pictureBox);
                }
                try
                {
                    //刷新picturebox的
                    this.DrawBackGround();
                    this.DrawBelowTime();
                    this.DrawTotalProgress();
                    this.DrawSelectedLine();
                    this.DrawDetailWord();
                    this.DrawAllSlide();
                    //刷新listview的，以及下面的各个控件
                    this.DrawTemporaryTimeLine();
                    this.form1.BeginInvoke(this.refreshAction);
                }
                catch(Exception ee)
                {

                }
                Thread.Sleep(50);
            }
        }
        private void DrawBackGround()
        {
            graphics.FillRectangle(brushBackGroundMain,0,0,width,height);
            graphics.FillRectangle(brushCut,0,cutS,width,cutE-cutS);
            graphics.FillRectangle(brushTime,0,timeS,width,timeE-timeS);
            graphics.DrawLine(new Pen(brushDetailLine),0,timeS,width,timeS);
            graphics.DrawLine(new Pen(brushDetailLine),0,slideS,width,slideS);
        }
        private void DrawBelowTime()
        {
            double startPosition = 0;
            if ((NowTime*10 / (unitBelow / 10)) % 1 == 0)
            {
                startPosition = 0;
            }
            else
            {
                startPosition =(1 - ((NowTime/ (unitBelow / 10)) % 1)) * (oneUnitPix/10);

            }
            
            for (double i = startPosition; i < this.width; i = i + oneUnitPix / 10)
            {
              
                graphics.DrawLine(new Pen(this.brushLine), (int)i, timeS, (int)i, timeS+10);
            }
            //上面是在画小坐标
            //下面要开始画大坐标
            double startTime=0;
            if ((NowTime / unitBelow) % 1 == 0)
            {
                startPosition = 0;
                startTime=NowTime;
            }
            else
            {
                startPosition=(1-(NowTime/unitBelow)%1)*(oneUnitPix);
                startTime=((int)(NowTime/unitBelow)+1)*unitBelow;
            }

            StringFormat stringFormat = new StringFormat();
            //stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            
            for(double i=startPosition;i<this.width;i=i+oneUnitPix)
            {
                string startTimeStr = this.TimeOut(startTime);
                graphics.DrawString(startTimeStr + "", new Font("方正雅黑", 8), brushLine, (int)i, timeS + 10,stringFormat);
                graphics.FillEllipse(brushLine, (int)i-4, timeS-4,8,8);
                startTime += unitBelow;
            }
        }
        private void DrawTotalProgress()
        {
            double timeShowOut = (this.width / this.oneUnitPix)*unitBelow;
            int startPosition =(int)( (NowTime / totalTime) * this.width);
            int endPosition = (int)(((NowTime + timeShowOut) / totalTime) * this.width);
            graphics.FillRectangle(brushSlide, startPosition, totalProgressS, endPosition - startPosition, totalProgressE - totalProgressS);
        }
        private void DrawSelectedLine()
        {
            int startPosition = 0;
            //total【总进度上面的】
            if ((selectedTime - NowTime) >= 0)
            {
                startPosition=(int)((selectedTime / totalTime) * width);
                Pen pen = new Pen(Color.LightGray, 2);
                pen.DashStyle = DashStyle.DashDot;
                graphics.DrawLine(pen, startPosition, totalProgressS, startPosition, totalProgressE);
            }
          
            //detail上面的线
            startPosition = 0;
            if ((selectedTime - NowTime) >= 0)
            {
                startPosition =(int)( (((selectedTime - NowTime) / this.unitBelow) * oneUnitPix));
                Pen pen2 = new Pen(Color.LightGray, 2);
                pen2.DashStyle = DashStyle.DashDot;
                graphics.DrawLine(pen2, startPosition, cutE, startPosition, detailE);
            }
         
        }
       /// <summary>
       /// 绘制具体的位置以及上面的字符
       /// </summary>
       private void DrawDetailWord()
        {
            int startNum = 0;
            int endNum = 0;
            this.listSlideInfo.Clear();
            for (int i = 0; i < this.listSingleSentence.Count; i++)
            {
                if (this.listSingleSentence[i].endTime >= this.NowTime)
                {
                    startNum = i;
                    i = listSingleSentence.Count + 200;
                }
            }
            endNum = startNum;

            for (int i = startNum; i < this.listSingleSentence.Count; i++)
            {
                if (this.listSingleSentence[i].startTime > this.NowTime + (width / oneUnitPix) * unitBelow)
                {
                    endNum = i - 1;
                    i = listSingleSentence.Count + 200;
                }
                else
                {
                    if (i >= this.listSingleSentence.Count - 1)
                    {
                        endNum = i;
                    }
                }
              
            }
            //以上是将起始的要显示的台词，和要停止的的显示的台词的序号都找到了
            //下一步就开始遍历这段数组，然后将台词显示出来
            //for (int i = startNum; i <= endNum; i++)
            for (int i = 0; i < this.listSingleSentence.Count;i++)
            {
                //遍历所有的数组，而不是找起始位和停止位。2013-10-14
                if (this.listSingleSentence[i].endTime >= this.NowTime || this.listSingleSentence[i].startTime > this.NowTime + (width / oneUnitPix) * unitBelow)
                {
                    ///一下对判断是不是选中进行了判断。
                    if (this.selectedTime <= this.listSingleSentence[i].endTime && this.selectedTime >= this.listSingleSentence[i].startTime)
                    {
                        this.listSingleSentence[i].isSelected = true;
                    }
                    else
                    {
                        this.listSingleSentence[i].isSelected = false;
                    }
                    //以上是将是不是选中，搞出来了，选中的反白显示
                    int startPosition = 0;
                    int endPosition = 0;
                    startPosition = (int)((this.listSingleSentence[i].startTime - NowTime) * this.oneUnitPix / unitBelow);
                    if (startPosition < 0)
                    {
                        startPosition = 0;
                    }
                    endPosition = (int)((this.listSingleSentence[i].endTime - NowTime) * this.oneUnitPix / unitBelow);
                    if (endPosition > this.width)
                    {
                        endPosition = width;
                    }
                    if (endPosition >= startPosition)//只有末尾比开始大，所有的工作才有意义..2013-3-8
                    {
                        graphics.DrawLine(new Pen(brushDetailLine, 1), startPosition, cutE, startPosition, timeS);
                        graphics.DrawLine(new Pen(brushDetailLine, 1), endPosition, cutE, endPosition, timeS);
                        if (this.listSingleSentence[i].isSelected)
                        {
                            graphics.FillRectangle(brushDetailSelected, startPosition, detailS, endPosition - startPosition, detailE - detailS);
                        }
                        else
                        {
                            graphics.FillRectangle(brushDetail, startPosition, detailS, endPosition - startPosition, detailE - detailS);
                        }
                        ///增加新的选中项,如果在选中队列中则置成另一种颜色
                        if (this.selectedIndex.Contains(i))
                        {
                            graphics.FillRectangle(brushDetailSelected, startPosition, detailS, endPosition - startPosition, detailE - detailS);
                        }
                        //以上是绘制图形，现在是绘制图像
                        string str = listSingleSentence[i].content;
                        SizeF sizeF = graphics.MeasureString(listSingleSentence[i].content, new Font("微软雅黑", 10));
                        if (sizeF.Width > (endPosition - startPosition))
                        {
                            int wordNum = (int)(str.Length * (endPosition - startPosition) / sizeF.Width);
                            str = str.Remove(wordNum);
                        }
                        if (this.listSingleSentence[i].isSelected)
                        {
                            graphics.DrawString(str, new Font("微软雅黑", 10), brushWordSelected, startPosition, detailS + 10);
                        }
                        else
                        {
                            graphics.DrawString(str, new Font("微软雅黑", 10), brushWord, startPosition, detailS + 10);
                        }
                        //以上是绘制文字
                        //以下是为了绘制滑块做准备
                        SlideInfo info = new SlideInfo();
                        info.startPosition = startPosition;
                        info.endPosition = endPosition;
                        info.num = i;
                        this.listSlideInfo.Add(info);

                    }
                }
            }

        }
        private void DrawAllSlide()
        {
            for (int i = 0; i < listSlideInfo.Count; i++)
            {
              
                if (listSlideInfo[i].startPosition>0)
                graphics.FillRectangle(brushDetailLine, listSlideInfo[i].startPosition, slideS, 10, slideE - slideS);
                if (listSlideInfo[i].endPosition < width)
                    graphics.FillRectangle(Brushes.Red, listSlideInfo[i].endPosition - 10, slideS, 10, slideE - slideS);
               
            }
        }
        private void DrawTemporaryTimeLine()
        {
            if (this.temporaryTimeLine.isSelected)
            {
                int startPosition =(int)( (temporaryTimeLine.startTime - this.NowTime) * OneUnitPix / unitBelow);
                int endPosition = (int)((temporaryTimeLine.endTime - this.NowTime) * OneUnitPix / unitBelow);
                if (startPosition < 0)
                {
                    startPosition = 0;
                }
                if (endPosition > width)
                {
                    endPosition = width;
                }
                if (endPosition >= startPosition)
                {
                    graphics.FillRectangle(brushDetailSelected, startPosition, detailS, endPosition - startPosition, detailE - detailS);
                    string str = "起始:" + temporaryTimeLine.startTime;
                    SizeF sizef = graphics.MeasureString(str, new Font("微软雅黑", 10));
                    if (sizef.Width > (endPosition - startPosition))
                    {
                        int count = (int)(str.Length*(endPosition - startPosition) / sizef.Width);
                        str = str.Remove(count);

                    }
                    graphics.DrawString(str, new Font("微软雅黑", 10), brushWordSelected, startPosition, detailS + 10);

                    str = "结束:" + temporaryTimeLine.endTime;
                    sizef = graphics.MeasureString(str, new Font("微软雅黑", 10));
                    if (sizef.Width > (endPosition - startPosition))
                    {
                        int count = (int)(str.Length*(endPosition -startPosition) / sizef.Width);
                        str = str.Remove(count);

                    }
                    graphics.DrawString(str, new Font("微软雅黑", 10), brushWordSelected, startPosition, detailS + 30);
                }
            }
        }
        private void DrawAddTemporaryTimeLine()
        {
            
        }
        private void RefreshOtherControl()
        {
            if (!this.form1.movieTrack.Focused)
            {
                this.form1.nowTimeShow.Text = this.TimeOut(this.selectedTime);
            }
            this.TimeLineOut();
        }
        private string TimeOut(double time)
        {
           time = ((double)((int)(time*1000)))/1000;
           int hour = (int)time / 3600;
           int minute = (int)(time - 3600 * hour) / 60;
           int second = (int)(time - 3600 * hour - 60 * minute);
           int minsec = (int)((time - (int)time) * 1000);
           string TimeStr = hour + ":" + minute + ":" + second + "," + minsec;
           return TimeStr;
        }
       /// <summary>
       /// 刷新控件
       /// </summary>
       private void TimeLineOut()
        {
            int count = -1;
            for (int i = 0; i < this.listSingleSentence.Count; i++)
            {
                if (listSingleSentence[i].startTime < this.selectedTime && listSingleSentence[i].endTime > this.selectedTime)
                {
                    listSingleSentence[i].isSelected = true;
                    count = i;
                }
                else
                {
                    listSingleSentence[i].isSelected = false;
                }
            }
            //确定控件都该显示什么
            if (count >= 0)
            {
                if (this.form1.timeEditStart.Focused || this.form1.timeEditEnd.Focused || this.form1.contentEdit.Focused||this.form1.confirmChange.Focused)
                {
                    //这已经在判断了。如果是被选中的话，那些区域应该是不更新的。
                }
                else
                {
                    this.form1.nowTimeLineShow.Text = listSingleSentence[count].content;
                    this.form1.contentEdit.Text = listSingleSentence[count].content;
                    if (this.slideCount >= 0)
                    {
                        count = this.slideCount;
                    }
                    this.form1.timeEditStart.Text = this.TimeOut(listSingleSentence[count].startTime);
                    this.form1.timeEditEnd.Text = this.TimeOut(listSingleSentence[count].endTime);
                    //this.form1.numEdit.Text = count + 1 + "";
                    this.form1.numEdit.Text = count+"";//修改于2013-4-10
                }
            }
            else
            {
                this.form1.nowTimeLineShow.Text = "";
            }
            if (this.slideCount >= 0)
            {
                if (this.form1.timeEditStart.Focused || this.form1.timeEditEnd.Focused || this.form1.contentEdit.Focused || this.form1.confirmChange.Focused)
                {
                    //这已经在判断了。如果是被选中的话，那些区域应该是不更新的。
                    
                }
                else
                {
                    this.form1.contentEdit.Text = listSingleSentence[this.slideCount].content;
                    this.form1.timeEditStart.Text = this.TimeOut(listSingleSentence[this.slideCount].startTime);
                    this.form1.timeEditEnd.Text = this.TimeOut(listSingleSentence[this.slideCount].endTime);
                    //this.form1.numEdit.Text = this.slideCount + 1 + "";
                    this.form1.numEdit.Text = this.slideCount + "";//2013-4-10
                }
            }


            int realItemPosition = this.GetRealNum(count);
            if (realItemPosition>=0)
            {
                if (realItemPosition + 5 < this.form1.listView1.yyItems.Count)
                {
                    form1.listView1.yyItems[realItemPosition].EnsureVisible();
                }
                else
                {
                    form1.listView1.yyItems[realItemPosition].EnsureVisible();
                }
                this.form1.listView1.yyItems[realItemPosition].Selected = true;
                //this.form1.listView1.Refresh();
                //form1.listView1.yyItems[count].BackColor = Color.AliceBlue;
            }

           // //确定看到的listView的位置的
           //if (count >= 0&&count<this.form1.listView1.yyItems.Count)
           // {
           //     if (count + 5 < this.form1.listView1.yyItems.Count)
           //     {
           //         form1.listView1.yyItems[count+5].EnsureVisible();
           //     }
           //     else
           //     {
           //         form1.listView1.yyItems[count].EnsureVisible();
           //     }
               //2013-8-4等待转换
            //再次选中的时候，是为了进行要先计算出来是哪个行要被选中，然后再去做标记

               
                //form1.listView1.yyItems[count].BackColor = Color.AliceBlue;
            //}
            
            //增加trackbar的更新
           if (!this.form1.movieTrack.Focused)
           {
               this.form1.movieTrack.Maximum = (int)totalTime;
               this.form1.movieTrack.Value = (int)this.selectedTime;
               this.form1.interval = (int)totalTime / 100;
               //this.form1.movieTrack.SmallChange = (int)totalTime / 10;
           }
           // this.form1.movieTrack.Maximum = (int)totalTime;
           
        }

        private int GetRealNum(int count)
        {
            string countStr=count+"";
            for (int i = 0; i < form1.listView1.yyItems.Count; i++)
            {
                //if (form1.listView1.yyItems[i].SubItems[0].Text.Equals(countStr))
                if (form1.listView1.yyItems[i].SubItems[0].Text.Equals(countStr))
                {
                    return i;
                }
            }
            return -1;
        }
       /// <summary>
       /// 判断是不是要刷新listview的函数
       /// </summary>
       /// <returns></returns>
        public bool IsRefreshContentShow()
        {
            
            if (this.form1.numEdit.Focused
                || this.form1.contentEdit.Focused
                || this.form1.timeEditStart.Focused
                || this.form1.timeEditEnd.Focused)
            {
                return false;
            }
            return true;
        }

        int selectedShowSave = 0;
       /// <summary>
       /// 设置除了pictureShow以外的其他的那些控件
       /// </summary>
        private void SetFormControl()
        {
            int selectedShow = 0;
            List<int> selected2Show = new List<int>();
            ///首先判定哪个singleSentence是现在正在读取的
            for (int i = 0; i < this.listSingleSentence.Count; i++)
            {
                if (listSingleSentence[i].startTime < this.selectedTime && listSingleSentence[i].endTime > this.selectedTime)
                {
                    listSingleSentence[i].isSelected = true;
                    selectedShow = i;
                }
                else
                {
                    listSingleSentence[i].isSelected = false;
                }
            }
            if (!(this.selectedShowSave == selectedShow) && this.isTrackFollow)
            {
                /*
                this.selectedShowSave = selectedShow;
                this.form1.timeEditStart.Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedShow].startTime);
                this.form1.timeEditEnd.Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedShow].endTime);
                this.form1.numEdit.Text = selectedShow + "";
                this.form1.contentEdit.Text = this.listSingleSentence[selectedShow].content;
                this.form1.listView1.YYClearSelected();
                int showPosition = this.form1.listView1.YYGetShowPosition(selectedShow);
                if (showPosition >= 0)
                {
                    this.form1.listView1.YYSetSelected(showPosition);
                    this.form1.listView1.YYEnsurVisible(showPosition);
                }
            }
           if (this.selectedRefreshMark) 
           {
               this.form1.listView1.YYClearSelected();
               //if(this.)
           }
                 **/
                NotificationCenter.SendMessage("timeEditStart", "SetText", TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedShow].startTime));
                NotificationCenter.SendMessage("timeEditEnd", "SetText", TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedShow].endTime));
                NotificationCenter.SendMessage("contentEdit", "SetText", this.listSingleSentence[selectedShow].content);
                NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", selectedShow);
            }
            if (this.selectedRefreshMark)
            {
                if (this.selectedIndex.Count > 0)
                {
                    NotificationCenter.SendMessage("timeEditStart", "SetText", TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedIndex[selectedIndex.Count-1]].startTime));
                    NotificationCenter.SendMessage("timeEditEnd", "SetText", TimeLineReadWrite.TimeOut(this.listSingleSentence[selectedIndex[selectedIndex.Count - 1]].endTime));
                    NotificationCenter.SendMessage("contentEdit", "SetText", this.listSingleSentence[selectedIndex[selectedIndex.Count-1]].content);
                    NotificationCenter.SendMessage("yyListView", "SetSelectedByIndex", selectedIndex[selectedIndex.Count-1]);
                    NotificationCenter.SendMessage("yyListView", "EnsureVisibleByIndex", selectedIndex[selectedIndex.Count-1]);
                }
            }

            this.form1.movieTrack.Maximum = (int)totalTime;
            this.form1.movieTrack.Value = (int)this.selectedTime;
            this.form1.interval = (int)totalTime / 100;

        }

        #region 一些类里面的小工具
        /// <summary>
       /// 比较两个list
       /// </summary>
       /// <returns></returns>
        private bool CompareList(List<int> a,List<int> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (!b.Contains(a[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        # endregion

        }
       
    }
    

















/*
 * 
   private void DrawBackGround()
        {
            this.graphics.FillRectangle(Brushes.Black, 0, 0, this.width, this.height);
            this.graphics.FillRectangle(Brushes.DimGray, 0, (int)(this.height * 0.8), (int)(this.width), (int)(this.height * 0.2));
            graphics.DrawLine(Pens.YellowGreen, 0, (int)(this.height * 0.1), (int)(this.width), (int)(this.height * 0.1));
            graphics.DrawLine(Pens.YellowGreen, 0, (int)(this.height * 0.8), (int)(this.width), (int)(this.height * 0.8));
        }
        private void DrawTimeBelow()
        {
            int rateStartPosition = (int)((this.nowTime * 10 - (int)this.nowTime * 10) * 0.1 * this.oneSecondPix);
            for (int i = 0; i < this.width / (this.oneSecondPix / 10); i++)
            {
                graphics.DrawLine(Pens.YellowGreen, (int)(rateStartPosition +i * (this.oneSecondPix / 10)), (int)(this.height * 0.8), (int)(rateStartPosition + i * (this.oneSecondPix / 10)), (int)(this.height * 0.8 + 5));
            }
            rateStartPosition = (int)((this.nowTime - (int)this.nowTime) * this.oneSecondPix);
            
            int nowTimeShow;
            if (this.nowTime - (int)this.nowTime > 0)
            {
                nowTimeShow = (int)(this.nowTime + 1);
            }
            else
            {
                nowTimeShow = (int)this.nowTime;
            }
            for(double i=0;i<this.width;i=i+this.oneSecondPix)
            {
                graphics.DrawString(nowTimeShow+"", new Font("微软雅黑", 10), Brushes.GreenYellow, (int)(rateStartPosition + i), (int)(this.height * 0.8 + 10));
                nowTimeShow++;
            }
            
        }
        private void DrawSentence()
        {
            int startSentence = 0;
            int endSentence = 0;
            //找到需要显示的部分
            if (this.listSingleSentence.Count > 0)
            {
                for (int i = 0; i < this.listSingleSentence.Count; i++)
                {
                    if (this.nowTime < this.listSingleSentence[i].endTime)
                    {
                        startSentence = i;
                        i = this.listSingleSentence.Count + 200;
                    }
                }
                endSentence = startSentence;
                for (int i = startSentence; i < this.listSingleSentence.Count; i++)
                {
                    if (this.listSingleSentence[i].startTime > (this.nowTime + this.width / this.oneSecondPix))
                    {
                        if (i > 0)
                        {
                            endSentence = i - 1;
                            i = this.listSingleSentence.Count + 200;
                        }
                    }
                    else
                    {
                        if (i == this.listSingleSentence.Count - 1)
                        {
                            endSentence = this.listSingleSentence.Count - 1;
                        }
                    }
                }
                //找到需要显示的区域
                for (int i = startSentence; i <= endSentence; i++)
                {
                    int startDrawPosition = 0;
                    int endDrawPosition = 0;
                    if (this.listSingleSentence[i].startTime <= this.nowTime)
                    {
                        startDrawPosition = 0;
                    }
                    else
                    {
                        startDrawPosition = (int)((this.listSingleSentence[i].startTime - this.nowTime) * this.oneSecondPix);
                    }
                    if (this.listSingleSentence[i].endTime >= this.nowTime + this.width / this.oneSecondPix)
                    {
                        endDrawPosition = this.width;
                    }
                    else
                    {
                        endDrawPosition = (int)((this.listSingleSentence[i].endTime - this.nowTime) * this.oneSecondPix);
                    }
                    //绘制图形
                    if (this.listSingleSentence[i].isSelected == true)
                    {
                        this.graphics.FillRectangle(Brushes.ForestGreen, (int)startDrawPosition, (int)(this.height * 0.2),(int)( endDrawPosition - startDrawPosition), (int)(this.height * 0.6));
                        this.graphics.DrawString(listSingleSentence[i].content, new Font("方正雅黑", 8), Brushes.White, (int)startDrawPosition, (int)(this.height * 0.2 + 10));
                    }
                    else
                    {
                        this.graphics.FillRectangle(Brushes.White, (int)startDrawPosition, (int)(this.height * 0.2), (int)(endDrawPosition - startDrawPosition), (int)(this.height * 0.6));
                        this.graphics.DrawString(listSingleSentence[i].content, new Font("方正雅黑", 8), Brushes.Black, (int)startDrawPosition, (int)(this.height * 0.2 + 10));
                    }
                }

            }
        }
 * 
 */


