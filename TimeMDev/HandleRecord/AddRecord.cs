using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 插入空行的类
    /// </summary>
    public class AddRecord:HandleRecordBass
    {
        int index;
        int count;
        int[] indexArray;
        int type;//类型
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="yyListView"></param>
        /// <param name="index">yyListView的记录index</param>
        public AddRecord(List<SingleSentence> listSingleSentence,YYListView yyListView,int index)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.type = 0;
            
        }
        public AddRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index, int count)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.type = 1;
        }
        public AddRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index,string content)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.type = 2;
        }
        public AddRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index, int count,string content)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.type = 3;
        }
        public AddRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int[] index)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.type = 4;
        }
        /// <summary>
        /// 增加
        /// </summary>
        private void Add()
        {
            if (this.type == 0)
            {
                
            }
        }
        /// <summary>
        /// 反增加
        /// </summary>
        private void UnAdd()
        {
 
        }


        private void DataAdd()
        {
                
        }
        private void ShowAdd()
        {
            
        }

        public override void Execute()
        {
            
        }

        public override void UnExecute()
        {
            
        }
    }
}
