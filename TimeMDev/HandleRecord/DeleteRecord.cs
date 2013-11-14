using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class DeleteRecord:HandleRecordBass
    {
        int index;
        YYListView yyListView;
        List<SingleSentence> listSingleSentence;
        SingleSentence sentenceSave;
        public DeleteRecord(List<SingleSentence>listSingleSentence,YYListView yyListView,int index)
        {
            this.index = index;
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;

        }

        /// <summary>
        /// 正序执行删除
        /// </summary>
        public override void Execute()
        {
            int realIndex = this.yyListView.YYGetRealPosition(this.index);
            this.sentenceSave = CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[realIndex]);
            this.listSingleSentence.RemoveAt(realIndex);
            this.yyListView.YYDeleteLine(index);
        }
        /// <summary>
        /// 逆序反删除，即增加
        /// </summary>
        public override void UnExecute()
        {
            this.yyListView.YYInsertLine(this.index, this.sentenceSave);
            int realIndex = this.yyListView.YYGetRealPosition(this.index);
            this.listSingleSentence.Insert(realIndex, this.sentenceSave);
        }
    }
}
