﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TimeMDev
{
    public delegate void DDoubleClickAction(int showPosition);
    public delegate void DDoubleClickActionSeekTime(double time);
    public class YYListView:ListView,RedoUndoInterface,CopyPasteInterface
    {
        public List<int> SignPosition;//变颜色的位置
        public Brush Selected;
        public Brush NoSelected;
        public Brush VisibleBrush;
        public Brush Singular;
        public Brush Plural;
        public Brush OtherSelected;
        public Brush SeletedLineColor = Brushes.Blue;
        public Color TextSelected = Color.LightYellow;
        public List<ListViewItem> yyItems = new List<ListViewItem>();
        public event DDoubleClickAction DoubleClickAction;
        public DDoubleClickActionSeekTime SeekTimeAction;
        public double timeStart;
        public double timeEnd;
        int itemHeight=1;
        YYListViewColumnSorter yyListViewColumnSorter = new YYListViewColumnSorter();
        YYListViewColumnSorterVirtual yyListViewColumnSorterVirtual = new YYListViewColumnSorterVirtual();
        private System.ComponentModel.IContainer components;
       public int selectedLine;
        public int SortStyle
       {
           get { return yyListViewColumnSorterVirtual.SortSytle; }
           set { this.yyListViewColumnSorterVirtual.SortSytle = value; }
       }
       public int ColumnToSort
       {
           get { return yyListViewColumnSorterVirtual.SortColumn; }
           set { this.yyListViewColumnSorterVirtual.SortColumn = value; }
       }
       public SortOrder Order
       {
           get { return yyListViewColumnSorterVirtual.Order; }
           set { this.yyListViewColumnSorterVirtual.Order = value; }
       }
       /// <summary>
       /// 设置显示焦点的位置
       /// </summary>
        public int VisiblePosition
       {
           get;
           set;
       }
        /// <summary>
        /// 是否显示特效
        /// </summary>
        public bool IsShowEffect
        {
            get
            {
                return this.isShowEffect;
            }
            set
            {
                this.isShowEffect = value;
            }
        }
        private bool isShowEffect=false;
        public YYListView()
        {
            this.OwnerDraw = true;
            this.SignPosition = new List<int>();
            this.Selected = Brushes.Orange;
            this.NoSelected = Brushes.Gray;
            this.Singular = Brushes.LightBlue;
            this.Plural = Brushes.White;
            this.OtherSelected = Brushes.BlueViolet;
            this.VisibleBrush = Brushes.LightBlue;
            this.ListViewItemSorter = yyListViewColumnSorter;

            this.VirtualMode = true;
            this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(YYListView_RetrieveVirtualItem);
            //双缓冲
            SetStyle(ControlStyles.DoubleBuffer |ControlStyles.OptimizedDoubleBuffer |ControlStyles.AllPaintingInWmPaint, true);

            UpdateStyles(); 
        }
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
            Graphics g = e.Graphics;
            Rectangle bounds = e.Bounds;
        }
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (View != View.Details)
            {
                e.DrawDefault = true;
            }
        }
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
            this.itemHeight = e.Bounds.Height;
            Rectangle bounds=e.Bounds;
            Graphics g=e.Graphics;            
            if (e.ColumnIndex == 0)
            {
                if (e.Item.Checked)
                {
                    g.FillRectangle(this.Selected, bounds);
                }
                else
                {
                    g.FillRectangle(this.NoSelected, bounds);
                }
                if (e.ItemIndex == this.VisiblePosition)
                {
                    g.FillRectangle(this.VisibleBrush, bounds);
                }
                e.DrawText(TextFormatFlags.HorizontalCenter);
            }
            else
            {
                if (e.ItemIndex % 2 == 0)
                {
                    g.FillRectangle(this.Singular, bounds);
                }
                else
                {
                    g.FillRectangle(this.Plural, bounds);
                }
                ListViewItemStates itemState = e.ItemState;
                if ((itemState & ListViewItemStates.Selected) == ListViewItemStates.Selected)
                {
                    g.FillRectangle(this.OtherSelected, bounds);
                }
                if (this.yyItems[e.ItemIndex].Selected == true)
                {
                    g.FillRectangle(this.OtherSelected, bounds);
                }
                string text=e.SubItem.Text;
               // text=text.Replace("\r","");
              //  text=text.Replace("\n","");
                if (!this.isShowEffect)
                {
                    text = CCHandle.TrimEffect(text);
                }
                if (this.yyItems[e.ItemIndex].Selected)
                {
                    TextRenderer.DrawText(
                        g,
                        text,
                        e.Item.Font,
                        bounds,
                        this.TextSelected,
                        TextFormatFlags.Left);
                }
                else
                {
                    TextRenderer.DrawText(
                        g,
                        text,
                        e.Item.Font,
                        bounds,
                        e.Item.ForeColor,
                        TextFormatFlags.Left);
                }
            }            
        }
        
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.X - this.Bounds.X >= this.Columns[0].Width - 0)
            {
                if (this.SeekTimeAction != null)
                {
                    if (e.X - this.Bounds.X >= this.Columns[0].Width && e.X - this.Bounds.X <= this.Columns[0].Width + this.Columns[1].Width)
                    {
                        this.SeekTimeAction(timeStart);
                    }
                    if (e.X - this.Bounds.X >= this.Columns[0].Width + this.Columns[1].Width && e.X - this.Bounds.X <= this.Columns[0].Width + this.Columns[1].Width + this.Columns[2].Width)
                    {
                        this.SeekTimeAction(timeEnd);
                    }
                }
                base.OnMouseDoubleClick(e);
                
            }
            else
            {
                int orderNum = (e.Y - this.Bounds.Y) / this.itemHeight;
                orderNum += this.TopItem.Index;
                if (this.DoubleClickAction != null)
                {
                    this.DoubleClickAction(orderNum);//cj 2013-12-15
                }
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.X - this.Bounds.X >= this.Columns[0].Width - 0)
            {
                base.OnMouseClick(e);
            }
            
        }
        public void SignItem(int itemIndex)//存在就取消，不存在就加上
        {
            if (this.SignPosition.Contains(itemIndex))
            {
                this.SignPosition.Remove(itemIndex);
            }
            else
            {
                this.SignPosition.Add(itemIndex);
            }
        }
        public void SignItem()
        {
            for (int i = 0; i < this.SelectedItems.Count; i++)
            {
                if (SelectedItems[i].Checked)
                {
                    SelectedItems[i].Checked = false;
                }
                else
                {
                    SelectedItems[i].Checked = true;
                }
            }
         
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        void YYListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (this.yyItems == null || this.yyItems.Count == 0)
            {
                return;
            }
            else
            {
                //e.Item = this.yyItems[e.ItemIndex];
                if (e.ItemIndex < this.yyItems.Count)
                {
                    //this.ItemsCollect = null;
                    e.Item = this.yyItems[e.ItemIndex];
                }
            }
        }


        public void YYRefresh()
        {
            this.VirtualListSize = this.yyItems.Count;
            this.Invalidate();
        }
        public void YYSort()
        {
            this.yyItems.Sort(this.yyListViewColumnSorterVirtual);
            this.Invalidate();
        }
        /// <summary>
        /// 插入空行
        /// </summary>
        /// <param name="num"></param>
        public void YYInsertBlank(int index,int count,double timeSpilt,bool style)
        {
            //在某个位置插入多少空行，那个位置不变，其他位置都增加
            for (int i = 0; i < this.yyItems.Count; i++)
            {
                int position = Int32.Parse(yyItems[i].SubItems[0].Text);
                if (position > Int32.Parse(yyItems[index].SubItems[0].Text))
                {
                    yyItems[i].SubItems[0].Text = position + count + "";
                }
            }
            SingleSentence singleSentenceBuf = new SingleSentence();
            for(int i=0;i<count;i++)
            {
                ListViewItem item=(ListViewItem)this.yyItems[0].Clone();
                if(style==true)
                {
                    item.SubItems[3].Text="";
                }
                if(style==false)
                {
                    item.SubItems[3].Text="空白内容行"+i+1;
                }
                item.SubItems[0].Text=Int32.Parse(this.yyItems[index].SubItems[0].Text)+i+1+"";
                item.SubItems[1].Text=TimeLineReadWrite.TimeOut(TimeLineReadWrite.TimeIn(this.yyItems[index+i].SubItems[2].Text));
                item.SubItems[2].Text=TimeLineReadWrite.TimeOut(TimeLineReadWrite.TimeIn(this.yyItems[index+i].SubItems[2].Text)+timeSpilt);//cj-2013-12-22

                singleSentenceBuf.content = item.SubItems[3].Text;
                singleSentenceBuf.startTime = TimeLineReadWrite.TimeIn(this.yyItems[index + i].SubItems[2].Text);
                singleSentenceBuf.endTime = TimeLineReadWrite.TimeIn(this.yyItems[index + i].SubItems[2].Text);

                item.SubItems[4].Text = singleSentenceBuf.everyLineLength;
                item.SubItems[5].Text = singleSentenceBuf.timeLength + "";
                item.SubItems[6].Text = TimeLineReadWrite.TimeOut(singleSentenceBuf.lineNum) + "";
                this.yyItems.Insert(index+i+1,item);
            }
        }
        /// <summary>
        /// 删除某一行
        /// </summary>
        /// <param name="index"></param>
        public void YYDeleteLine(int index)
        {
            int num = Int32.Parse(yyItems[index].SubItems[0].Text);
            yyItems.RemoveAt(index);
            for (int i = 0; i < this.yyItems.Count; i++)
            {
                int position = Int32.Parse(yyItems[i].SubItems[0].Text);
                if (position >num)
                {
                    yyItems[i].SubItems[0].Text = (position-1)+"";
                }
            }
        }
        /// <summary>
        /// 清空某一项
        /// </summary>
        /// <param name="index">序号</param>
        /// <param name="style">0，清空内容，1，清空时间</param>
        public void YYZero(int index,int style)
        {
            SingleSentence singleSentence = new SingleSentence();
            singleSentence.content = "";
            if(style==0)
            {
                this.yyItems[index].SubItems[3].Text = "";
                this.yyItems[index].SubItems[4].Text =TimeLineReadWrite.TimeOut(singleSentence.timeLength);
                this.yyItems[index].SubItems[6].Text = singleSentence.lineNum+"";
            }
            singleSentence.startTime = 0;
            singleSentence.endTime = 0;
            if (style == 1)
            {
                this.yyItems[index].SubItems[2].Text = TimeLineReadWrite.TimeOut(singleSentence.startTime);
                this.yyItems[index].SubItems[1].Text = TimeLineReadWrite.TimeOut(singleSentence.endTime);
                this.yyItems[index].SubItems[5].Text = TimeLineReadWrite.TimeOut(singleSentence.timeLength);
            }
        }
        /// <summary>
        /// 插入新行,如果在乱序情况下请先增加再删除
        /// </summary>
        /// <param name="index">显示的序号</param>
        /// <param name="content">在listview中排列的位置</param>
        public void YYInsertLine(int showPosition,int index, string[] content)
        {
            for (int i = 0; i < this.yyItems.Count; i++)
            {
                int position = Int32.Parse(yyItems[i].SubItems[0].Text);
                if (position >= index)
                {
                    yyItems[i].SubItems[0].Text = position +1+ "";
                }
            }

            ListViewItem item = (ListViewItem)this.yyItems[0].Clone();
            item.SubItems[0].Text = content[0];
            item.SubItems[1].Text = content[1];
            item.SubItems[2].Text = content[2];
            this.yyItems.Insert(showPosition, item);

        }
        /// <summary>
        /// 插入某个位置，插入之后的所有值往后移动
        /// </summary>
        /// <param name="showPosition"></param>
        /// <param name="index"></param>
        /// <param name="content"></param>
        public void YYInsertLine(int showPosition, int index, SingleSentence singleSentence)
        {
            for (int i = 0; i < this.yyItems.Count; i++)
            {
                int position = Int32.Parse(yyItems[i].SubItems[0].Text);
                if (position >= index)
                {
                    yyItems[i].SubItems[0].Text = position + 1 + "";
                }
            }

            ListViewItem item = (ListViewItem)this.yyItems[0].Clone();
            item.SubItems[0].Text = index + "";
            item.SubItems[1].Text = TimeLineReadWrite.TimeOut(singleSentence.startTime);
            item.SubItems[2].Text = TimeLineReadWrite.TimeOut(singleSentence.endTime);
            item.SubItems[3].Text = singleSentence.content;
            item.SubItems[4].Text = singleSentence.everyLineLength;
            item.SubItems[5].Text = TimeLineReadWrite.TimeOut(singleSentence.timeLength);
            item.SubItems[6].Text = singleSentence.lineNum+"";
            this.yyItems.Insert(showPosition, item);

        }
        /// <summary>
        /// 获取实际的在存储链表中的位置，即显示的所对应的实际位置
        /// </summary>
        /// <param name="index">显示的位置</param>
        public int YYGetRealPosition(int index)
        {
            int realPosition = -1;
            realPosition = Int32.Parse(this.yyItems[index].SubItems[0].Text);
            return realPosition;
        }
        /// <summary>
        /// 插入一个新行
        /// </summary>
        /// <param name="showPosition">新行显示位置</param>
        /// <param name="singleSentence"></param>
        public void YYInsertLine(int showPosition,SingleSentence singleSentence)
        {
            int index = this.YYGetRealPosition(showPosition);
            for (int i = 0; i < this.yyItems.Count; i++)
            {
                int position = Int32.Parse(yyItems[i].SubItems[0].Text);
                if (position >= index)
                {
                    yyItems[i].SubItems[0].Text = position + 1 + "";
                }
            }

            ListViewItem item = (ListViewItem)this.yyItems[0].Clone();
            item.SubItems[0].Text = index + "";
            item.SubItems[1].Text = TimeLineReadWrite.TimeOut(singleSentence.startTime);
            item.SubItems[2].Text = TimeLineReadWrite.TimeOut(singleSentence.endTime);
            item.SubItems[3].Text = singleSentence.content;
            item.SubItems[4].Text = singleSentence.everyLineLength;
            item.SubItems[5].Text = TimeLineReadWrite.TimeOut(singleSentence.timeLength);
            item.SubItems[6].Text = singleSentence.lineNum + "";
            if (showPosition < this.yyItems.Count)
            {
                this.yyItems.Insert(showPosition, item);
            }
            else
            {
                this.yyItems.Add(item);
            }
        }
        /// <summary>
        /// 插入一个新行
        /// </summary>
        /// <param name="singleSentence"></param>
        public void YYAddLine(SingleSentence singleSentence)
        {
            ListViewItem item = (ListViewItem)this.yyItems[0].Clone();
            item.SubItems[0].Text = this.yyItems.Count+1 + "";
            item.SubItems[1].Text = TimeLineReadWrite.TimeOut(singleSentence.startTime);
            item.SubItems[2].Text = TimeLineReadWrite.TimeOut(singleSentence.endTime);
            item.SubItems[3].Text = singleSentence.content;
            item.SubItems[4].Text = singleSentence.everyLineLength;
            item.SubItems[5].Text = TimeLineReadWrite.TimeOut(singleSentence.timeLength);
            item.SubItems[6].Text = singleSentence.lineNum + "";
            this.yyItems.Add(item);
        }
        
        
        #region RedoUndoInterface 成员

        private List<List<ListViewItem>> undoStack = new List<List<ListViewItem>>();
        private List<List<ListViewItem>> redoStack = new List<List<ListViewItem>>();
        public bool _Undo()
        {
            if (this.undoStack.Count == 0)
            {
                return false;
            }
            else
            {
                List<ListViewItem> sg = CopyObject.DeepCopy(this.yyItems);
                this.redoStack.Insert(0, sg);
                this.yyItems = this.undoStack[0];
                this.VirtualListSize = this.yyItems.Count;
                this.undoStack.RemoveAt(0);
                return true;
            }
        }

        public void _Save()
        {
            List<ListViewItem> sg = CopyObject.DeepCopy(this.yyItems);
            this.undoStack.Insert(0, this.yyItems);
            if (this.undoStack.Count >= 10)
            {
                this.undoStack.RemoveAt(this.undoStack.Count - 1);
            }
        }

        public bool _Redo()
        {
            if (this.redoStack.Count == 0)
            {
                return false;
            }
            else
            {
                List<ListViewItem> sg = CopyObject.DeepCopy(this.yyItems);
                this.undoStack.Insert(0, sg);
                this.yyItems = this.redoStack[0];
                this.VirtualListSize = this.yyItems.Count;
                this.redoStack.RemoveAt(0);
                return true;
            }

        }

        public void _Clear()
        {
            this.redoStack.Clear();
            this.undoStack.Clear();
        }
        #endregion

        #region CopyPasteInterface 成员

        public void _Copy()
        {
            if (this.SelectedIndices.Count > 0)
            {
                Clipboard.SetData(DataFormats.UnicodeText, this.yyItems[this.SelectedIndices[0]].SubItems[3].Text);
            }
        }

        public void _Paste()
        {
            string str = (string)Clipboard.GetData(DataFormats.UnicodeText);
            this.yyItems[this.SelectedIndices[0]].SubItems[3].Text = str;
        }

        #endregion



        const Int32 LVM_FIRST = 0x1000;
        const Int32 LVM_SCROLL = LVM_FIRST + 20;
        #region 滚动代码
        [DllImport("user32")]
        static extern IntPtr SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam,
        IntPtr lParam);

        private void ScrollHorizontal(int pixelsToScroll)
        {
            SendMessage(this.Handle, LVM_SCROLL, (IntPtr)pixelsToScroll,
            IntPtr.Zero);
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Ensure visible of a ListViewItem and SubItem Index.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="item"></param></span>
        /// <span class="code-SummaryComment"><param name="subItemIndex"></param></span>
        public void EnsureVisible(ListViewItem item, int subItemIndex)
        {
            if (item == null || subItemIndex > item.SubItems.Count - 1)
            {
                throw new ArgumentException();
            }

            // scroll to the item row.
            item.EnsureVisible();
            Rectangle bounds = item.SubItems[subItemIndex].Bounds;

            // need to set width from columnheader, first subitem includes
            // all subitems.
            bounds.Width = this.Columns[subItemIndex].Width;

            ScrollToRectangle(bounds);
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Scrolls the listview.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="bounds"></param></span>
        private void ScrollToRectangle(Rectangle bounds)
        {
            int scrollToLeft = bounds.X + bounds.Width+20;
            if (scrollToLeft > this.Bounds.Width)
            {
                this.ScrollHorizontal(scrollToLeft - this.Bounds.Width);
            }
            else
            {
                int scrollToRight = bounds.X-20;
                if (scrollToRight < 0)
                {
                    this.ScrollHorizontal(scrollToRight);
                }
            }
        }
        /// <summary>
        /// 滚动到指定位置
        /// </summary>
        /// <param name="showPosition"></param>
        public void YYEnsurVisible(int showPosition)
        {
            this.VisiblePosition = showPosition;
            this.EnsureVisible(yyItems[showPosition], 0);

        }
        #endregion

        /// <summary>
        /// 获取显示的实际位置
        /// </summary>
        /// <param name="index">真实位置</param>
        /// <returns>返回的listview中的实际位置，序号>0为正确</returns>
        public int YYGetShowPosition(int index)
        {
            for (int i = 0; i < this.yyItems.Count;i++ )
            {
                if (int.Parse(this.yyItems[i].SubItems[0].Text) == index)
                {
                    return i;
                }
            }
            return 0;

        }

        /// <summary>
        /// 清空所有行的选中标记
        /// </summary>
        public void YYClearSelected()
        {
            for (int i = 0; i < yyItems.Count; i++)
            {
                this.yyItems[i].Selected = false;
            }
        }
        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <param name="showPosition">选中项</param>
        /// <returns></returns>
        public bool YYSetSelected(int showPosition)
        {
            if (showPosition < this.yyItems.Count)
            {
                this.yyItems[showPosition].Selected = true;
                return true;
            }
            return false;
        }
    }
}

