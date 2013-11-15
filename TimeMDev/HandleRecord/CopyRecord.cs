using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class CopyRecord:HandleRecordBass
    {
        DataProcess dataProcess;
        YYListView yyListView;
        System.Windows.Forms.ListView.SelectedIndexCollection showPosition;
        public CopyRecord(DataProcess dataProcess, YYListView yyListView, System.Windows.Forms.ListView.SelectedIndexCollection showPosition)
        {
            this.dataProcess = dataProcess;
            this.yyListView = yyListView;
            this.showPosition = showPosition;
        }


        public override void Execute()
        {
            int[] index=new int[this.showPosition.Count];
            for(int i=0;i<index.Length;i++)
            {
                index[i]=this.yyListView.YYGetRealPosition(showPosition[i]);
            }
            dataProcess._Copy(index);
        }

        public override void UnExecute()
        {

        }
    }
}
