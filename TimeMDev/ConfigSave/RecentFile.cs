﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;

namespace TimeMDev.ConfigSave
{
    public class RecentFile
    {
        /// <summary>
        /// 最多有五个变量
        /// </summary>
        public static int fileNameCount = 5;
        private static List<string> recentFileList=new List<string>();
        private static List<BarButtonItem> items = new List<BarButtonItem>();
        public delegate void OnItemClick(object sender, ItemClickEventArgs e);
        OnItemClick OnItemClickAction;
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
        private void ChangeShow(BarSubItem barSubItem)
        {
            //barSubItem.RemoveLink(barSubItem);
            //BarManager bm = new BarManager();
            //bm.
            for(int i=0;i<barSubItem.ItemLinks.Count;i++)
            {
                if(barSubItem.ItemLinks[i].Caption.StartsWith("|"))
                {
                    barSubItem.RemoveLink(barSubItem.ItemLinks[i]);
                }
            }
            for (int j =  recentFileList.Count-1; j >= 0; j--)
            {
                BarButtonItem item=new BarButtonItem();
                item.Caption="!"+recentFileList[j];
                item.ItemClick += new ItemClickEventHandler(item_ItemClick);
                barSubItem.InsertItem(barSubItem.ItemLinks[barSubItem.ItemLinks.Count - 2], item);
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
            recentFileList.Add(file);
            if (recentFileList.Count > fileNameCount)
            {
                recentFileList.RemoveAt(0);
            }
            this.WriteItemSave();
        }
    
    }
}
