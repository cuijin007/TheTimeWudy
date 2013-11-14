using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 排序命令,在程序初始化的时候一定保证其是按一定次序排序的，否则回滚无效
    /// </summary>
   public class ListViewSort:HandleRecordBass
    {
       //带_为上个一排序的状态。将这些状态保存起来，撤销的时候用
        int _columnToSort;
        int _sortStyle;
        SortOrder _sortOrder;
       int columnToSort;
       int sortStyle;
       SortOrder sortOrder;
       YYListView yyListView;
       List<ListViewItem> itemsSave;
       public ListViewSort(YYListView yyListView,int columnToSort,int sortStyle,SortOrder sortOrder)
       {
           //this._columnToSort = this.yyListView.ColumnToSort;
           //this._sortOrder = this.yyListView.Order;
           //this._sortStyle = this.yyListView.SortStyle;
           this.columnToSort = columnToSort;
           this.sortOrder = sortOrder;
           this.sortStyle = sortStyle;
           this.yyListView = yyListView;
       }
       /// <summary>
       /// 排序的方法，具体如何排序还是遵循外面可控的策略，这里面只是进行了简单的设置而已
       /// </summary>
        public override void Execute()
        {
            this.itemsSave = CopyObject.DeepCopy<List<ListViewItem>>(this.yyListView.yyItems);
            this.yyListView.Order = this.sortOrder;
            this.yyListView.ColumnToSort = this.columnToSort;
            this.yyListView.SortStyle = this.sortStyle;
            if (columnToSort == 0)
            {
                this.yyListView.SortStyle = 2;
            }
            else if (columnToSort == 3)
            {
                this.yyListView.SortStyle = 1;
            }
            else
            {
                this.yyListView.SortStyle = 0;
            }
            this.yyListView.YYSort();
            
        }

        public override void UnExecute()
        {
            //this.yyListView.SortStyle = this._sortStyle;
            //this.yyListView.Order = this._sortOrder;
            //this.yyListView.ColumnToSort = this._columnToSort;
            //this.yyListView.Sort();
            this.yyListView.yyItems = CopyObject.DeepCopy<List<ListViewItem>>(this.itemsSave);
        }
    }
}
