using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class InsertRecord:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        SingleSentence sentence;
        YYListView yyListView;
        int index;
        int showPosition;
        public InsertRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int showPosition, int index, SingleSentence sentence)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.sentence = sentence;
            this.index = index;
            this.showPosition = showPosition;
        }
        public override void Execute()
        {
            //this.yyListView.YYInsertLine(this.index, this.sentence);
            this.yyListView.YYInsertLine(this.showPosition, this.index, this.sentence);
            this.listSingleSentence.Insert(this.index, this.sentence);
        }

        public override void UnExecute()
        {
            this.yyListView.YYDeleteLine(this.showPosition);
            this.listSingleSentence.RemoveAt(this.index);
        }
    }
}
