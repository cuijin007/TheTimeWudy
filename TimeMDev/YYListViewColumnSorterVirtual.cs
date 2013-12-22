using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TimeMDev
{
    class YYListViewColumnSorterVirtual : IComparer<ListViewItem>
    {
        #region IComparer<ListViewItem> 成员
        private int ColumnToSort;//指定需要排序的列
        private SortOrder sortOrder;
        private CaseInsensitiveComparer objectCompare;
        public int SortSytle = 0;

        public YYListViewColumnSorterVirtual()
        {
            this.ColumnToSort = 0;
            this.sortOrder = SortOrder.None;
            this.objectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(ListViewItem x, ListViewItem y)
        {
            try
            {
                int compareResult = 0;
                ListViewItem listViewItemX = (ListViewItem)x;
                ListViewItem listViewItemY = (ListViewItem)y;
                if (this.SortSytle == 0)//比较时间
                {
                    //compareResult = this.objectCompare.Compare(TimeLineReadWrite.TimeIn(listViewItemX.SubItems[this.ColumnToSort].Text),TimeLineReadWrite.TimeIn(listViewItemY.SubItems[this.ColumnToSort].Text));
                    if (TimeLineReadWrite.TimeIn(listViewItemX.SubItems[this.ColumnToSort].Text) >= TimeLineReadWrite.TimeIn(listViewItemY.SubItems[this.ColumnToSort].Text))
                    {
                        compareResult = 1;
                    }
                    else
                    {
                        compareResult = -1;
                    }
                }
                else if (this.SortSytle == 1)//比较字符长度
                {
                    //compareResult = this.objectCompare.Compare(listViewItemX.SubItems[this.ColumnToSort].Text.Length, listViewItemY.SubItems[this.ColumnToSort].Text.Length);
                    if (listViewItemX.SubItems[this.ColumnToSort].Text.Length >= listViewItemY.SubItems[this.ColumnToSort].Text.Length)
                    {
                        compareResult = 1;
                    }
                    else
                    {
                        compareResult = -1;
                    }
                }
                else if (this.SortSytle == 2)//单纯的比较两个值
                {
                    int xNum, yNum;
                    try
                    {
                        xNum = Int16.Parse(listViewItemX.SubItems[this.ColumnToSort].Text);
                        yNum = Int16.Parse(listViewItemY.SubItems[this.ColumnToSort].Text);
                        compareResult = this.objectCompare.Compare(xNum, yNum);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    compareResult = this.objectCompare.Compare(listViewItemX.SubItems[this.ColumnToSort].Text, listViewItemY.SubItems[this.ColumnToSort].Text);
                }
                if (this.Order == SortOrder.Ascending)
                {
                    return compareResult;
                }
                else if (this.Order == SortOrder.Descending)
                {
                    return (-compareResult);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 1;
            }
        }
        public int SortColumn
        {
            get { return ColumnToSort; }
            set { this.ColumnToSort = value; }
        }
        public SortOrder Order
        {
            set { this.sortOrder = value; }
            get { return sortOrder; }
        }
        #endregion
    }
}
