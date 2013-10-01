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
        int itemHeight=1;
        YYListViewColumnSorter yyListViewColumnSorter = new YYListViewColumnSorter();
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip listViewMenu;
        private ToolStripMenuItem addOneLineContext;
        private ToolStripMenuItem addMutiLineContext;
        private ToolStripMenuItem moveTimeContext;
        private ToolStripMenuItem align;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolStripMenuItem14;
        private ToolStripMenuItem toolStripMenuItem15;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripMenuItem toolStripMenuItem17;
       public int selectedLine;
        public int SortStyle
       {
           get { return yyListViewColumnSorter.SortSytle; }
           set { this.yyListViewColumnSorter.SortSytle = value; }
       }
       public int ColumnToSort
       {
           get { return yyListViewColumnSorter.SortColumn; }
           set { this.yyListViewColumnSorter.SortColumn = value; }
       }
       public SortOrder Order
       {
           get { return yyListViewColumnSorter.Order; }
           set { this.yyListViewColumnSorter.Order = value; }
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
                text=text.Replace("\r","");
                text=text.Replace("\n","");
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
            this.components = new System.ComponentModel.Container();
            this.listViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addOneLineContext = new System.Windows.Forms.ToolStripMenuItem();
            this.addMutiLineContext = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTimeContext = new System.Windows.Forms.ToolStripMenuItem();
            this.align = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMenu
            // 
            this.listViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addOneLineContext,
            this.addMutiLineContext,
            this.moveTimeContext,
            this.align,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator1,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripSeparator2,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripSeparator3,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripSeparator4,
            this.toolStripMenuItem14,
            this.toolStripMenuItem15,
            this.toolStripMenuItem16,
            this.toolStripMenuItem17});
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(229, 446);
            // 
            // addOneLineContext
            // 
            this.addOneLineContext.Name = "addOneLineContext";
            this.addOneLineContext.Size = new System.Drawing.Size(228, 22);
            this.addOneLineContext.Text = "插入单行字幕";
            // 
            // addMutiLineContext
            // 
            this.addMutiLineContext.Name = "addMutiLineContext";
            this.addMutiLineContext.Size = new System.Drawing.Size(228, 22);
            this.addMutiLineContext.Text = "插入多行字幕";
            // 
            // moveTimeContext
            // 
            this.moveTimeContext.Name = "moveTimeContext";
            this.moveTimeContext.Size = new System.Drawing.Size(228, 22);
            this.moveTimeContext.Text = "平移时间";
            // 
            // align
            // 
            this.align.Name = "align";
            this.align.Size = new System.Drawing.Size(228, 22);
            this.align.Text = "对其播放器时间(当前行)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem4.Text = "对其播放器时间(当前行以后)";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem5.Text = "toolStripMenuItem5";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem6.Text = "自定义拆分";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem7.Text = "合并为单行显示";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem8.Text = "合并为多行显示";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem9.Text = "删除行";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem10.Text = "仅时间清零";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem11.Text = "仅内容清空";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem12.Text = "撤销";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem13.Text = "重做";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem14.Text = "剪切";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem15.Text = "复制";
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem16.Text = "粘贴";
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(228, 22);
            this.toolStripMenuItem17.Text = "全选";
            // 
            // YYListView
            // 
            this.ContextMenuStrip = this.listViewMenu;
            this.listViewMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
