using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TimeMDev
{
    public class YYListView:ListView
    {
        public List<int> SignPosition;//变颜色的位置
        public Brush Selected;
        public Brush NoSelected;
        public Brush Singular;
        public Brush Plural;
        public Brush OtherSelected;
        public Brush SeletedLineColor = Brushes.Blue;
        public List<ListViewItem> yyItems = new List<ListViewItem>();
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
        public YYListView()
        {
            this.OwnerDraw = true;
            this.SignPosition = new List<int>();
            this.Selected = Brushes.Orange;
            this.NoSelected = Brushes.Gray;
            this.Singular = Brushes.LightSeaGreen;
            this.Plural = Brushes.White;
            this.OtherSelected = Brushes.DarkGreen;
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
                if (this.Items[e.ItemIndex].Selected == true)
                {
                    g.FillRectangle(this.OtherSelected, bounds);
                }
                string text=e.SubItem.Text;
               // text=text.Replace("\r","");
              //  text=text.Replace("\n","");
                TextRenderer.DrawText(
                    g,
                    text,
                    e.Item.Font,
                    bounds,
                    e.Item.ForeColor,
                    TextFormatFlags.Left);             
            }            
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.X-this.Bounds.X >= this.Columns[0].Width - 0)
            {
                base.OnMouseDoubleClick(e);
            }
            else
            {
                int orderNum = (e.Y - this.Bounds.Y) / this.itemHeight;
                orderNum += this.TopItem.Index;
                if (this.Items[orderNum].Checked == true)
                {
                    this.Items[orderNum].Checked = false;
                }
                else
                {
                    this.Items[orderNum].Checked= true;
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
                if (position > Int32.Parse(yyItems[index-1].SubItems[0].Text))
                {
                    yyItems[i].SubItems[0].Text = position + count + "";
                }
            }
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
                item.SubItems[0].Text=Int32.Parse(this.yyItems[index-1].SubItems[0].Text)+i+1+"";
                item.SubItems[1].Text=TimeLineReadWrite.TimeOut(TimeLineReadWrite.TimeIn(this.yyItems[index-1+i].SubItems[2].Text));
                item.SubItems[2].Text=TimeLineReadWrite.TimeOut(TimeLineReadWrite.TimeIn(this.yyItems[index-1+i].SubItems[2].Text)+1);
                this.yyItems.Insert(index+i,item);
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
            if(style==0)
            {
                this.yyItems[index].SubItems[3].Text = "";
            }
            if (style == 1)
            {
                this.yyItems[index].SubItems[2].Text = "";
                this.yyItems[index].SubItems[1].Text = "";
            }
        }


    }
}

