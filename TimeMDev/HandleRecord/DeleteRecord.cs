using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class DeleteRecord:HandleRecordBass
    {
        int index;
        public DeleteRecord(List<SingleSentence>listSingleSentence,int index)
        {
            this.index = index;
        }
        public override void Execute()
        {
            
        }

        public override void UnExecute()
        {
           
        }
    }
}
