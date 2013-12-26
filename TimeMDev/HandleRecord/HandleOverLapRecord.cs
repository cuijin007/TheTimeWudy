using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class HandleOverLapRecord:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        List<HandleRecordBass> command = new List<HandleRecordBass>();
        public HandleOverLapRecord(List<SingleSentence> listSingleSentence, YYListView yyListView)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
        }
        public override void Execute()
        {
            for (int i = 0; i < this.listSingleSentence.Count-2; i++)
            {
                if (this.listSingleSentence[i].endTime > this.listSingleSentence[i+1].startTime)
                {
                    SingleSentence sentence=CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[i]);
                    sentence.endTime = this.listSingleSentence[i + 1].startTime;
                    ChangeRecord changeRecord = new ChangeRecord(this.listSingleSentence, this.yyListView, this.yyListView.YYGetShowPosition(i), sentence);
                    changeRecord.Execute();
                    this.command.Add(changeRecord);
                }
               
            }
        }

        public override void UnExecute()
        {
            for (int i = command.Count - 1; i >= 0; i--)
            {
                this.command[i].UnExecute();
            }
        }
    }
}
