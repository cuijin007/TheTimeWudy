using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeMDev.ConfigSave;

namespace TimeMDev.HandleRecord.cchandle
{
    public class CCHandle2:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        public CCHandle2(List<SingleSentence> listSingleSentence)
        {
            this.listSingleSentence = listSingleSentence;
        }

        public override void Execute()
        {
            (new CutSame(this.listSingleSentence)).CutSameFromList();
            (new DeleteSymbols(this.listSingleSentence)).DeleteAction();
            (new FristMergeAndResolution(this.listSingleSentence)).MergeResolutionFunction();
        }

        public override void UnExecute()
        {
           
        }
    }
}
