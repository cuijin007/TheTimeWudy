using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 23帧转25帧
    /// </summary>
    public class Turn23To25FPS:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        public Turn23To25FPS(List<SingleSentence> listSingleSentence,YYListView yyListView)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
        }
        public override void Execute()
        {
            for (int i = 1; i < this.listSingleSentence.Count; i++)
            {
                this.listSingleSentence[i].startTime = (this.listSingleSentence[i].startTime * 23.976) / 25;
                this.listSingleSentence[i].endTime = (this.listSingleSentence[i].endTime * 23.976) / 25;
            }
            for (int j = 0; j < this.yyListView.yyItems.Count; j++)
            {
                this.yyListView.yyItems[j].SubItems[1].Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[yyListView.YYGetRealPosition(j)].startTime);
                this.yyListView.yyItems[j].SubItems[2].Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[yyListView.YYGetRealPosition(j)].startTime);
            }
        }

        public override void UnExecute()
        {
            for (int i = 1; i < this.listSingleSentence.Count; i++)
            {
                this.listSingleSentence[i].startTime = (this.listSingleSentence[i].startTime * 25) / 23.976;
                this.listSingleSentence[i].endTime = (this.listSingleSentence[i].endTime * 25) / 23.976;
            }
            for (int j = 0; j < this.yyListView.yyItems.Count; j++)
            {
                this.yyListView.yyItems[j].SubItems[1].Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[yyListView.YYGetRealPosition(j)].startTime);
                this.yyListView.yyItems[j].SubItems[2].Text = TimeLineReadWrite.TimeOut(this.listSingleSentence[yyListView.YYGetRealPosition(j)].startTime);
            }
        }
    }
}
