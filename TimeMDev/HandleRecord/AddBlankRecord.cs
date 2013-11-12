using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class AddBlankRecord:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        int index;
        int count;
        double timeSpilt;
        bool style;
        /// <summary>
        /// 插入空白行命令
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="yyListView"></param>
        /// <param name="index">listview中的序号</param>
        /// <param name="count">数量</param>
        /// <param name="timeSpilt">时间间隔</param>
        /// <param name="style">true为空白行，false为非空白行</param>
        public AddBlankRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index, int count, double timeSpilt, bool style)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.index = index;
            this.count = count;
            this.timeSpilt = timeSpilt;
            this.style = style;
        }
        /// <summary>
        /// 插入一行或多行
        /// </summary>
        public override void Execute()
        {
            int realIndex = int.Parse(this.yyListView.yyItems[this.index].SubItems[0].Text);
            double time=this.listSingleSentence[realIndex].endTime;
            for (int i = 0; i < this.count; i++)
            {
                SingleSentence sentence = new SingleSentence();
                sentence.startTime = time;
                sentence.endTime = time + this.timeSpilt;
                if (this.style)
                {
                    sentence.content = "";
                }
                else
                {
                    sentence.content = "空白行"+i;
                }
                this.listSingleSentence.Insert(realIndex + 1 + i, sentence);
                time += this.timeSpilt;
            }
            this.yyListView.YYInsertBlank(this.index, this.count, this.timeSpilt, this.style);
        }
        /// <summary>
        /// 反插入一行或多行
        /// </summary>
        public override void UnExecute()
        {
            int realIndex = int.Parse(this.yyListView.yyItems[this.index].SubItems[0].Text);
            for (int i = 0; i < count; i++)
            {
                this.listSingleSentence.RemoveAt(realIndex + 1);
                this.yyListView.YYDeleteLine(this.index + 1);
            }
           
        }
    }
}
