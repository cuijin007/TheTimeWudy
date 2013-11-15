using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class CutRecord:HandleRecordBass
    {
        List<HandleRecordBass> listCommand = new List<HandleRecordBass>();
        DataProcess dataProcess;
        YYListView yyListView;
        System.Windows.Forms.ListView.SelectedIndexCollection showPosition;
        public CutRecord(DataProcess dataProcess, YYListView yyListView, System.Windows.Forms.ListView.SelectedIndexCollection showPosition)
        {
            this.dataProcess = dataProcess;
            this.yyListView = yyListView;
            this.showPosition = showPosition;
        }
        public override void Execute()
        {
            CopyRecord copyRecord=new CopyRecord(this.dataProcess,this.yyListView,this.showPosition);
            copyRecord.Execute();
            for (int i = 0; i < this.showPosition.Count; i++)
            {
                DeleteRecord deleteRecord = new DeleteRecord(this.dataProcess.listSingleSentence, yyListView, this.showPosition[i]);
                this.listCommand.Add(deleteRecord);
                deleteRecord.Execute();
            }
        }
        public override void UnExecute()
        {
            for (int i = this.listCommand.Count - 1; i >= 0; i--)
            {
                this.listCommand[i].UnExecute();
            }
        }
    }
}
