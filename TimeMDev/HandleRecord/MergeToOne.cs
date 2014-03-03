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
            string englishSave = "";
            string chineseSave="";
            bool chineseFrist=false;
            string script="";
            if (this.source.Count > 0)
            {
                sentence.endTime = this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[this.source.Count - 1])].endTime;
            }
            for (int i = this.source.Count-1; i >= 0; i--)
            //for (int i = 0; i < this.source.Count;i++ )
            {
                if (this.isSingleLine)
                {
                    string chinese = "";
                    string english = "";
                    string contentBuf=this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content.Replace("\r\n","");
                    script = CCHandle.GetScript(contentBuf);
                    contentBuf = CCHandle.CutSrtScript(contentBuf);

                    //CCHandle.SpiltRule((this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content).Replace("\r\n", ""), out chinese, out english);
                    //if ((this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content).Replace("\r\n", "").StartsWith(chinese))
                    CCHandle.SpiltRule(contentBuf.Replace("\r\n", ""), out chinese, out english);
                    if(contentBuf.StartsWith(chinese))
                    {
                        chineseFrist = true;
                    }
                    englishSave = CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(english))+" "+englishSave;
                    chineseSave = CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(chinese))+chineseSave;
                    //sentence.content += (this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content).Replace("\r\n", "");
                }
                else
                {
                    sentence.content = (this.listSingleSentence[this.yyListView.YYGetRealPosition(this.source[i])].content)+sentence.content;
                    if (i > 0)
                    {
                        sentence.content = "\r\n"+sentence.content;
                    }
                }
                DeleteRecord deleteRecord = new DeleteRecord(this.listSingleSentence, this.yyListView, source[i]);
                this.functions.Add(deleteRecord);
            }
            if (isSingleLine)
            {
                if (chineseFrist)
                {
                    sentence.content = chineseSave + "\r\n" +script+ englishSave;
                }
                else
                {
                    sentence.content = script+englishSave + "\r\n" + chineseSave;
                }
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
