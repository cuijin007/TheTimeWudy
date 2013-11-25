using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 合并到单行，单行分为单行和多行显示
    /// </summary>
    public class MergeToOne:HandleRecordBass
    {
        YYListView yyListView;
        List<SingleSentence> listSingleSentence;
        int ShowPositionDest;
        System.Windows.Forms.ListView.SelectedIndexCollection source;
        List<HandleRecordBass> functions;
        bool isSingleLine;
        /// <summary>
        /// 合并到一行，初始化
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="yyListView"></param>
        /// <param name="ShowPositionDest">合并到的位置</param>
        /// <param name="source">需要合并的选中行</param>
        /// <param name="isSingleLine">合并到一行中，是多换行显示不换行显示</param>
        public MergeToOne(List<SingleSentence> listSingleSentence, 
            YYListView yyListView,
            int ShowPositionDest,
            System.Windows.Forms.ListView.SelectedIndexCollection source,
            bool isSingleLine
                )
        {
            this.yyListView = yyListView;
            this.listSingleSentence = listSingleSentence;
            this.ShowPositionDest = ShowPositionDest;
            this.source = source;
            this.functions = new List<HandleRecordBass>();
            this.isSingleLine = isSingleLine;
        }

        public override void Execute()
        {
            SingleSentence sentence = CopyObject.DeepCopy<SingleSentence>(this.listSingleSentence[this.yyListView.YYGetRealPosition(this.ShowPositionDest)]);
            sentence.content = "";
            for (int i = this.source.Count-1; i >= 0; i--)
            {
                if (this.isSingleLine)
                {
                    sentence.content += (this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content).Replace("\r\n", "");
                }
                else
                {
                    sentence.content += (this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content);
                    if (i > 0)
                    {
                        sentence.content += "\r\n";
                    }
                }
                DeleteRecord deleteRecord = new DeleteRecord(this.listSingleSentence, this.yyListView, source[i]);
                this.functions.Add(deleteRecord);
            }
            InsertRecord insertRecord = new InsertRecord(this.listSingleSentence, this.yyListView, this.ShowPositionDest, this.yyListView.YYGetRealPosition(this.ShowPositionDest), sentence);
            this.functions.Add(insertRecord);
            for (int i = 0; i < functions.Count; i++)
            {
                this.functions[i].Execute();
            }
        }

        public override void UnExecute()
        {
            for (int i = functions.Count - 1; i >= 0; i--)
            {
                this.functions[i].UnExecute();
            }
        }
    }
}
