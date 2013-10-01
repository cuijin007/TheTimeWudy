using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev
{
    public partial class PictureShowCut : UserControl
    {
          Bitmap bitmap;
        Graphics gra;
        string fristLine = "";
        string secondLine = "";
        Color fristLineColor = Color.LightGreen;
        Color secondLineColor = Color.LightBlue;
        int wordLength = 16;
        int wordHigh = 16;
        int wordInterval = 0;
        List<CutPoint> pointList = new List<CutPoint>();
        bool lineSelected = false;
        public PictureShowCut()
        {
            InitializeComponent();
        }
        public void Init(string fristLine, string secondLine)
        {
            this.fristLine = fristLine;
            this.secondLine = secondLine;
            int wordLength = 0;
            int wordNum = 0;
            if (this.fristLine.Length >= this.secondLine.Length)
            {
                wordNum = fristLine.Length * (this.wordLength + this.wordInterval);
                if (wordNum > this.Width)
                    this.pictureBoxShow.Width = wordNum;
                else
                {
                    this.pictureBoxShow.Width = this.Width;
                }
            }
            else
            {
                wordNum = secondLine.Length * (this.wordLength + this.wordInterval);
                if (wordNum > this.Width)
                    this.pictureBoxShow.Width = wordNum;
                else
                {
                    this.pictureBoxShow.Width = this.Width;
                }
            }
            this.bitmap = new Bitmap(this.pictureBoxShow.Width, this.pictureBoxShow.Height);
            this.gra = Graphics.FromImage(this.bitmap);
        }
        protected void DrawBackGround()
        {
            gra.FillRectangle(Brushes.White, 0, 0, this.bitmap.Width, bitmap.Height);
        }
        protected void DrawBackColor()
        {
            gra.FillRectangle(Brushes.LightGreen, 0, 0, this.bitmap.Width, (int)(this.wordHigh * 1.1));
            gra.FillRectangle(Brushes.LightBlue, 0, (int)(this.wordHigh * 1.1), this.bitmap.Width, (int)(this.wordHigh));
        }
        protected void DrawWord()
        {
            PointF[] positionsFrist = new PointF[this.fristLine.Length];
            PointF[] positionsSecond = new PointF[this.secondLine.Length];
            for (int i = 0; i < positionsFrist.Length; i++)
            {
                positionsFrist[i] = new PointF(i * (this.wordLength + this.wordInterval), wordHigh);
            }
            GdiplusMethods.DrawDriverString(this.gra, this.fristLine, new Font("宋体", (float)this.wordLength, GraphicsUnit.Pixel), Brushes.Black, positionsFrist);
            for (int j = 0; j < positionsSecond.Length; j++)
            {
                positionsSecond[j] = new PointF(j * (this.wordLength + this.wordInterval), wordHigh * 2);
            }
            GdiplusMethods.DrawDriverString(this.gra, this.secondLine, new Font("宋体", (float)this.wordLength, GraphicsUnit.Pixel), Brushes.Black, positionsSecond);
        }
        protected void DrawLine()
        {
            Brush brush;
            for (int i = 0; i < this.pointList.Count; i++)
            {
                if (pointList[i].selected)
                {
                    brush = Brushes.Blue;
                }
                else
                {
                    brush = Brushes.Orange;
                }
                if (pointList[i].y == 0)
                {
                    gra.DrawLine(new Pen(brush, 3), ((float)pointList[i].x + 1) * (this.wordInterval + this.wordLength), 0, ((float)pointList[i].x + 1) * (this.wordInterval + this.wordLength), (float)(this.wordHigh * 1.1));
                }
                else
                {
                    gra.DrawLine(new Pen(brush, 3), ((float)pointList[i].x + 1) * (this.wordInterval + this.wordLength), (float)(this.wordHigh * 1.1), ((float)pointList[i].x + 1) * (this.wordInterval + this.wordLength), (float)(this.wordHigh * 2.1));
                }
            }
        }
        public void DrawPictureView()
        {
            this.pictureBoxShow.Refresh();
            this.DrawBackGround();
            this.DrawBackColor();
            this.DrawWord();
            this.DrawLine();
            this.pictureBoxShow.Image = this.bitmap;
        }
        public void CaculatePointPosition(object objectx, MouseEventArgs e)
        {
            //首先要在点击的范围内
            if (e.Y <= 2 * this.wordHigh)
            {
                int x, y;
                int lengthNow = 0;
                if (e.Y < this.wordHigh)
                {
                    y = 0;//第0行
                    lengthNow = this.fristLine.Length * (this.wordLength + this.wordInterval);
                }
                else
                {
                    y = 1;//第1行
                    lengthNow = this.secondLine.Length * (this.wordLength + this.wordInterval);
                }
                if (e.X < lengthNow)
                {
                    x = ((int)(e.X / (this.wordLength + this.wordInterval)));
                    CutPoint point = new CutPoint();
                    point.x = x;
                    point.y = y;
                    if (!this.pointList.Contains(point))
                    {
                        if (!this.pointList.Contains(point))
                        {
                            this.pointList.Add(point);
                        }
                    }
                }
            }
        }
        public CutPoint CaculatePointPosition(MouseEventArgs e)
        {
            CutPoint point = new CutPoint();
            point.x = -1;
            //首先要在点击的范围内
            if (e.Y <= 2 * this.wordHigh)
            {
                int x, y;
                int lengthNow = 0;
                if (e.Y < this.wordHigh)
                {
                    y = 0;//第0行
                    lengthNow = this.fristLine.Length * (this.wordLength + this.wordInterval);
                }
                else
                {
                    y = 1;//第1行
                    lengthNow = this.secondLine.Length * (this.wordLength + this.wordInterval);
                }
                if (e.X < lengthNow)
                {
                    x = ((int)(e.X / (this.wordLength + this.wordInterval)));
                    //CutPoint point = new CutPoint();
                    point.x = x;
                    point.y = y;
                    if (!this.pointList.Contains(point))
                    {
                        if (!this.pointList.Contains(point))
                        {
                            //this.pointList.Add(point);
                            return point;
                        }
                    }
                }
            }
            return point;
        }
        private void pictureBoxShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.CaculatePointPosition(sender, e);
                this.DrawPictureView();
            }
            if (e.Button == MouseButtons.Left)
            {
                CutPoint point = this.CaculateIfOnLine(e);
                if (point.x >= 0)
                {
                    for (int i = 0; i < this.pointList.Count; i++)
                    {
                        if (point.x == pointList[i].x && point.y == pointList[i].y)
                        {
                            this.pointList[i].selected = true;
                            this.lineSelected = true;
                            this.DrawPictureView();
                        }
                    }
                }
            }
        }

        private void pictureBoxShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.lineSelected == false)
            {
                CutPoint point = this.CaculateIfOnLine(e);
                if (point.x >= 0)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Cross;
                }
                else
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            if (this.lineSelected)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                CutPoint point = this.CaculatePointPosition(e);
                if (point.x >= 0)
                {
                    for (int i = 0; i < this.pointList.Count; i++)
                    {
                        if (this.pointList[i].selected)
                        {
                            pointList[i].x = point.x;
                            pointList[i].y = point.y;
                            this.DrawPictureView();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 判断是在哪个线上。
        /// </summary>
        /// <param name="e"></param>
        /// <returns>-1是什么都没找到，其他的值对应着该找到的值</returns>
        protected CutPoint CaculateIfOnLine(MouseEventArgs e)
        {
            CutPoint pointSave = new CutPoint();
            pointSave.x = -1;
            if (e.Y <= 2 * this.wordHigh)
            {
                int x, y;
                int lengthNow = 0;
                if (e.Y < this.wordHigh)
                {
                    y = 0;//第0行
                    lengthNow = this.fristLine.Length * (this.wordLength + this.wordInterval);
                }
                else
                {
                    y = 1;//第1行
                    lengthNow = this.secondLine.Length * (this.wordLength + this.wordInterval);
                }
                if (e.X < lengthNow)
                {
                    for (int i = 0; i < this.pointList.Count; i++)
                    {
                        if (this.pointList[i].y == y && (e.X < (this.pointList[i].x + 1) * (this.wordLength + this.wordInterval) + 5) && (e.X > (this.pointList[i].x + 1) * (this.wordLength + this.wordInterval) - 5))
                        {
                            return this.pointList[i];
                        }
                    }
                    return pointSave;
                }
            }
            return pointSave;
        }
        /// <summary>
        /// 抬起的清空操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxShow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Y < this.bitmap.Height / 2)
            {
                for (int i = 0; i < this.pointList.Count; i++)
                {
                    if (this.pointList[i].selected == true)
                    {
                        CutPoint point = new CutPoint();
                        point.x = this.pointList[i].x;
                        point.y = this.pointList[i].y;
                        point.selected = this.pointList[i].selected;
                        if (this.pointList.Contains(point))
                        {
                            this.pointList.Remove(point);
                        }
                        this.pointList[i].selected = false;
                    }
                }
                this.lineSelected = false;
            }
            else
            {
                for (int i = 0; i < this.pointList.Count; i++)
                {
                    if (this.pointList[i].selected == true)
                    {
                        this.pointList.RemoveAt(i);
                    }
                }
                this.Cursor = Cursors.Default;
                
            }
            this.DrawPictureView();
        }

        public void SpiltWord(out string[] line1, out string[] line2)
        {
            string fristLineSave = this.fristLine;
            string secondLineSave = this.secondLine;
            this.pointList.Sort(new PointSort());
            for (int i = 0; i < this.pointList.Count; i++)
            {
                if (this.pointList[i].y == 0)
                {
                    fristLineSave=fristLineSave.Insert(this.pointList[i].x+1, "$&$");
                }
                if (this.pointList[i].y == 1)
                {
                    secondLineSave=secondLineSave.Insert(this.pointList[i].x+1, "$&$");
                }
            }
            string[] spilt = new string[1];
            spilt[0] = "$&$";
            line1 = fristLineSave.Split(spilt, StringSplitOptions.None);
            line2 = secondLineSave.Split(spilt, StringSplitOptions.None);
        }



        public class PointSort : IComparer<CutPoint>
        {
            public int Compare(CutPoint x, CutPoint y)
            {
                if (x.y != y.y)
                {
                    if (x.y < y.y)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (x.x > y.x)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }

         public class CutPoint
    {
        public int x;//第几个字符的后面。位置
        public int y;//y就两种，一种是第一行，一种是第二行
        public bool selected=false;
    }
    
}
