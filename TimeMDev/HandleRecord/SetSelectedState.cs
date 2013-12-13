using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 设置标记
    /// </summary>
    public class SetSelectedState:HandleRecordBass
    {
        List<SingleSentence> listSingleSentence;
        YYListView yyListView;
        int index;
        int realPosition;
        bool state;
        public SetSelectedState(List<SingleSentence> listSingleSentence, YYListView yyListView, int showPosition,bool state)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.index = index;
            this.realPosition = realPosition;
            this.state = state;
        }
        public override void Execute()
        {

        }
        
        public override void UnExecute()
        {
        }
    }
}
