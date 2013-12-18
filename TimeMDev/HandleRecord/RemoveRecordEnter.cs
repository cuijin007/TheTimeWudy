using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 去掉里面所有的换行符号
    /// </summary>
   public class RemoveRecordEnter:HandleRecordBass
    {
       List<SingleSentence> listSingleSentence;
       YYListView yyListView;
       System.Windows.Forms.ListView.SelectedIndexCollection source;
       List<HandleRecordBass> command = new List<HandleRecordBass>();
       public RemoveRecordEnter(List<SingleSentence> listSingleSentence,YYListView yyListView,System.Windows.Forms.ListView.SelectedIndexCollection source)
       {
           this.listSingleSentence = listSingleSentence;
           this.yyListView = yyListView;
           this.source = source;
       }
        public override void Execute()
        {
            for (int i = 0; i < this.source.Count; i++)
            {
                SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])]);
                sentence.content = sentence.content.Replace("\r\n", "");
                ChangeRecord changeRecord=new ChangeRecord(this.listSingleSentence,this.yyListView,this.source[i],sentence);
                changeRecord.Execute();
                this.command.Add(changeRecord);
            }

        }

        public override void UnExecute()
        {
            for (int i = this.command.Count - 1; i >= 0; i--)
            {
                this.command[i].UnExecute();
            }
        }
    }
}
