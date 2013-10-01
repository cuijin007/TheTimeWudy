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
            int compareResult = 0;
            ListViewItem listViewItemX = (ListViewItem)x;
            ListViewItem listViewItemY = (ListViewItem)y;
            if (this.SortSytle == 0)
            {
                compareResult = this.objectCompare.Compare(listViewItemX.SubItems[this.ColumnToSort].Text, listViewItemY.SubItems[this.ColumnToSort].Text);
            }
            else if (this.SortSytle == 1)
            {
                compareResult = this.objectCompare.Compare(listViewItemX.SubItems[this.ColumnToSort].Text.Length, listViewItemY.SubItems[this.ColumnToSort].Text.Length);
            }
            else if (this.SortSytle == 2)
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
            if (this.sortOrder == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (sortOrder == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
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
