using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;

namespace TimeMDev.ConfigSave
{
    public delegate void OnItemClick(object sender, ItemClickEventArgs e);
    public class RecentFile
    {
        /// <summary>
        /// 最多有五个变量
        /// </summary>
        public static int fileNameCount = 5;
        /// <summary>
        /// 最近打开文件
        /// </summary>
        private static List<string> recentFileList=new List<string>();
        private static List<BarButtonItem> items = new List<BarButtonItem>();
        /// <summary>
        /// 点击那些按钮之后的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public OnItemClick OnItemClickAction;
        public static List<string> RecentFileList
        {
            get 
            {
                return recentFileList;
            }
            set
            {
                recentFileList = value;
            }
        }
        private BarSubItem barSubItem;
        public RecentFile(BarSubItem barSubItem)
        {
            this.ReadItemSave();
            this.barSubItem = barSubItem;
            this.ChangeShow(barSubItem);
        }
        /// <summary>
        /// 读取config中的文件名
        /// </summary>
        private void ReadItemSave()
        {
            recentFileList.Clear();
            string str = Config.DefaultConfig["RecentFile"];
            string[] spilt=new string[1];
            spilt[0]=";";
            //this.
            if (!str.Equals(""))
            {
                string[] spiltBuf = str.Split(spilt,StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < spiltBuf.Length; i++)
                {
                    recentFileList.Add(spiltBuf[i]);
                }
            }
        }
        /// <summary>
        /// 写回config中的文件名
        /// </summary>
        private void WriteItemSave()
        {
            string buf = "";
            for (int i = 0; i < recentFileList.Count; i++)
            {
                buf += recentFileList[i];
                if (i < recentFileList.Count - 1)
                {
                    buf += ";";
                }
            }
            Config.DefaultConfig["RecentFile"] = buf;
        }
        /// <summary>
        /// 修改显示的内容
        /// </summary>
        /// <param name="barSubItem"></param>
        private void ChangeShow(BarSubItem barSubItem)
        {
            //barSubItem.RemoveLink(barSubItem);
            //BarManager bm = new BarManager();
            //bm.
            for (int i = 0; i < barSubItem.ItemLinks.Count; i++)
            {
                if (barSubItem.ItemLinks[i].Caption.StartsWith("|"))
                {
                    barSubItem.RemoveLink(barSubItem.ItemLinks[i]);
                    i--;
                }
            }
            for (int j =  recentFileList.Count-1; j >= 0; j--)
            {
                BarButtonItem item=new BarButtonItem();
                item.Caption="|"+recentFileList[j];
                item.ItemClick += new ItemClickEventHandler(item_ItemClick);
                barSubItem.InsertItem(barSubItem.ItemLinks[barSubItem.ItemLinks.Count - 2], item);
                if (j == 0)
                {
                    barSubItem.ItemLinks[barSubItem.ItemLinks.Count - 2 - recentFileList.Count].BeginGroup = true;
                }
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.OnItemClickAction != null)
            {
                this.OnItemClickAction(sender, e);
            }
        }
        /// <summary>
        /// 增加一个最近的文件
        /// </summary>
        /// <param name="file"></param>
        public void AddRecentFile(string file)
        {
            if (!recentFileList.Contains(file))
            {
                recentFileList.Add(file);
                if (recentFileList.Count > fileNameCount)
                {
                    recentFileList.RemoveAt(0);
                }
                this.WriteItemSave();
            }
            this.ChangeShow(this.barSubItem);
        }
        /// <summary>
        /// 刷新show
        /// </summary>
        public void RefreshShow()
        {
            this.ChangeShow(this.barSubItem);
        }
    }
}
