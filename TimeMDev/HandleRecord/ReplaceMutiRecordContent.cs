using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    public class ReplaceMutiRecordContent:HandleRecordBass
    {
        List<SingleSentence> listSingleSentences;
        YYListView yyListView;
        List<ReplaceParameter> replaceParameter;
        System.Windows.Forms.ListView.SelectedIndexCollection showCollection;
        List<HandleRecordBass> listCommand = new List<HandleRecordBass>();
        public ReplaceMutiRecordContent(List<SingleSentence> listSingleSentences, YYListView yyListView, List<ReplaceParameter> replaceParameter)
        {
            this.listSingleSentences=listSingleSentences;
            this.yyListView=yyListView;
            this.replaceParameter=replaceParameter;
        }
        public ReplaceMutiRecordContent(List<SingleSentence> listSingleSentences, YYListView yyListView, List<ReplaceParameter> replaceParameter,System.Windows.Forms.ListView.SelectedIndexCollection showCollection)
        {
            this.listSingleSentences=listSingleSentences;
            this.yyListView=yyListView;
            this.replaceParameter=replaceParameter;
            this.showCollection = showCollection;
        }
        public override void Execute()
        {
            if (this.showCollection == null)
            {
                for (int i = 0; i < this.yyListView.yyItems.Count; i++)
                {
                    ReplaceRecordContent replaceContent = new ReplaceRecordContent(this.listSingleSentences, this.yyListView, i, this.replaceParameter);
                    listCommand.Add(replaceContent);
                    replaceContent.Execute();
                }
            }
            else
            {
                for (int i = 0; i < this.showCollection.Count; i++)
                {
                    ReplaceRecordContent replaceContent = new ReplaceRecordContent(this.listSingleSentences, this.yyListView, this.showCollection[i], this.replaceParameter);
                    listCommand.Add(replaceContent);
                    replaceContent.Execute();
                }
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
    public class ReplaceParameter
    {
        public string orginalWord;
        public string replaceWord;
        public bool chineseChoosed;
        public bool englishChoosed;
        public bool caseSensitive;
    }
}
