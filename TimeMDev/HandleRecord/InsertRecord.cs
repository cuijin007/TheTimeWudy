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
        public InsertRecord(List<SingleSentence> listSingleSentence, YYListView yyListView, int index, SingleSentence sentence)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.sentence = sentence;
        }
        public override void Execute()
        {
            this.yyListView.YYInsertLine(this.index, this.sentence);
            this.listSingleSentence.Insert(yyListView.YYGetRealPosition(this.index), this.sentence);
        }

        public override void UnExecute()
        {
            this.yyListView.YYDeleteLine(index);
            this.listSingleSentence.RemoveAt(yyListView.YYGetRealPosition(this.index));
        }
    }
}
