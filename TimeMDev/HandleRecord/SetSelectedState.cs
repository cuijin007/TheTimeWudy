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
        int realPosition;
        int showPosition;
        bool state;
        public SetSelectedState(List<SingleSentence> listSingleSentence, YYListView yyListView, int showPosition,bool state)
        {
            this.listSingleSentence = listSingleSentence;
            this.yyListView = yyListView;
            this.showPosition = showPosition;
            this.realPosition = this.yyListView.YYGetRealPosition(showPosition);
            this.state = state;
        }
        public override void Execute()
        {
            this.listSingleSentence[this.realPosition].Checked = state;
            this.yyListView.yyItems[this.showPosition].Checked = state;
        }
        
        public override void UnExecute()
        {
            this.listSingleSentence[this.realPosition].Checked = !state;
            this.yyListView.yyItems[this.showPosition].Checked = !state;
        }
    }
}
