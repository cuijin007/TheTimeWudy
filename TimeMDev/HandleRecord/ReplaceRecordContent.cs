using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeMDev.HandleRecord
{
    public class ReplaceRecordContent:HandleRecordBass
    {
        List<SingleSentence> listSentences;
        YYListView yyListView;
        List<ReplaceParameter> replaceParameter;
        string contentSave;
        int showPosition;
        public ReplaceRecordContent(List<SingleSentence> listSentences,YYListView yyListView,
            int showPosition,
            List<ReplaceParameter> replaceParameter
            )
        {
            this.listSentences = listSentences;
            this.yyListView = yyListView;
            this.showPosition = showPosition;
            this.replaceParameter = replaceParameter;
        }
        public override void Execute()
        {

            this.contentSave = this.listSentences[this.yyListView.YYGetRealPosition(this.showPosition)].content;
            string contentBuf = contentSave;
            for (int i = 0; i < this.replaceParameter.Count; i++)
            {
                //if (!replaceParameter[i].caseSensitive)
                //{
                //    contentBuf = contentBuf.ToLower();
                //}
                if (this.replaceParameter[i].chineseChoosed && this.replaceParameter[i].englishChoosed)
                {
                }
                else
                {
                    string chinese;
                    string english;
                    CCHandle.SpiltRule(contentBuf, out chinese, out english);
                    if (this.replaceParameter[i].chineseChoosed && !this.replaceParameter[i].englishChoosed)
                    {
                        contentBuf = chinese;
                    }
                    if (!this.replaceParameter[i].chineseChoosed && this.replaceParameter[i].englishChoosed)
                    {
                        contentBuf = english;
                    }
                }
                if (replaceParameter[i].caseSensitive)
                {
                    //contentBuf = contentBuf.ToLower();
                    //Regex.Replace(contentBuf,
                    contentBuf = contentBuf.Replace(this.replaceParameter[i].orginalWord, this.replaceParameter[i].replaceWord);
                }
                else
                {
                    Regex regex = new Regex(this.replaceParameter[i].orginalWord, RegexOptions.IgnoreCase);
                    contentBuf=regex.Replace(contentBuf, this.replaceParameter[i].replaceWord);
                }
                this.yyListView.yyItems[this.showPosition].SubItems[3].Text = contentBuf;
                SingleSentence sentenceBuf = new SingleSentence();
                sentenceBuf.content = contentBuf;
                this.yyListView.yyItems[this.showPosition].SubItems[4].Text = sentenceBuf.everyLineLength;
                this.yyListView.yyItems[this.showPosition].SubItems[6].Text = sentenceBuf.lineNum+"";
                this.listSentences[this.yyListView.YYGetRealPosition(this.showPosition)].content = contentBuf;
            }
        }

        public override void UnExecute()
        {
            this.yyListView.yyItems[this.showPosition].SubItems[3].Text = this.contentSave;
            SingleSentence sentenceBuf = new SingleSentence();
            sentenceBuf.content = this.contentSave;
            this.yyListView.yyItems[this.showPosition].SubItems[4].Text = sentenceBuf.everyLineLength;
            this.yyListView.yyItems[this.showPosition].SubItems[6].Text = sentenceBuf.lineNum + "";
            this.listSentences[this.yyListView.YYGetRealPosition(this.showPosition)].content = this.contentSave;
        }
    }
}
