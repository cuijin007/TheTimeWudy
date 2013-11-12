using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev.HandleRecord
{
    public class ChangeRecord:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        SingleSentence sentence;
        YYListView yyListView;
        int index;
        SingleSentence sentenceSave;
        public ChangeRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index, SingleSentence sentence)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.index = index;
            this.sentence = sentence;
        }
        public override void Execute()
        {
            int realIndex = this.yyListView.YYGetRealPosition(this.index);
            this.sentenceSave = CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[realIndex]);
            this.yyListView.yyItems[this.index].SubItems[1].Text = TimeLineReadWrite.TimeOut(this.sentence.startTime);
            this.yyListView.yyItems[this.index].SubItems[2].Text = TimeLineReadWrite.TimeOut(this.sentence.endTime);
            this.yyListView.yyItems[this.index].SubItems[3].Text = this.sentence.content;
            this.listSingleSentence[realIndex] = sentence;
        }

        public override void UnExecute()
        {
            int realIndex = this.yyListView.YYGetRealPosition(this.index);
            this.yyListView.yyItems[this.index].SubItems[1].Text = TimeLineReadWrite.TimeOut(this.sentenceSave.startTime);
            this.yyListView.yyItems[this.index].SubItems[2].Text = TimeLineReadWrite.TimeOut(this.sentenceSave.endTime);
            this.yyListView.yyItems[this.index].SubItems[3].Text = this.sentenceSave.content;
            this.listSingleSentence[realIndex] = this.sentenceSave;
        }
    }
}
