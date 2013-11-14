using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 清空某一条目
    /// </summary>
    public class ZeroRecord:HandleRecordBass
    {
        int showPosition;
        int index;
        int style;
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        SingleSentence singleSentenceSave;
        /// <summary>
        /// 清空某一项
        /// </summary>
        /// <param name="singleSentence"></param>
        /// <param name="yyListView"></param>
        /// <param name="showPosition">显示的位置</param>
        /// <param name="style">0，为清空内容，1，为清空时间</param>
        public ZeroRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int showPosition,int style)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.showPosition = showPosition;
            this.style = style;
        }
        public override void Execute()
        {
            this.index = this.yyListView.YYGetRealPosition(showPosition);
            this.singleSentenceSave = CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[index]);
            if (style ==0)
            {
                this.yyListView.yyItems[showPosition].SubItems[3].Text = "";
                this.listSingleSentence[index].content = "";
            }
            if (style == 1)
            {
                this.yyListView.yyItems[showPosition].SubItems[1].Text = TimeLineReadWrite.TimeOut(0); ;
                this.yyListView.yyItems[showPosition].SubItems[2].Text = TimeLineReadWrite.TimeOut(0);
                this.listSingleSentence[index].startTime=0;
                this.listSingleSentence[index].endTime = 0;
            }
        }

        public override void UnExecute()
        {
             this.index = this.yyListView.YYGetRealPosition(showPosition);
            if (style == 0)
            {
                this.yyListView.yyItems[showPosition].SubItems[3].Text = this.singleSentenceSave.content;
            }
            if (style == 1)
            {
                this.yyListView.yyItems[showPosition].SubItems[1].Text = TimeLineReadWrite.TimeOut(this.singleSentenceSave.startTime); ;
                this.yyListView.yyItems[showPosition].SubItems[2].Text = TimeLineReadWrite.TimeOut(this.singleSentenceSave.endTime);
            }
            this.listSingleSentence[index] = this.singleSentenceSave;
        }
    }
}
