using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord.cchandle
{
    /// <summary>
    /// 去掉字符中相同的部分
    /// </summary>
    public class CutSame
    {
        string[] spiltWord = { " ", ",", "\r","\n" };
        List<SingleSentence> listSentence;
        public CutSame(List<SingleSentence> listSentence)
        {
            this.listSentence = listSentence;
        }

        public void CutSameFromList()
        {
            for (int i = this.listSentence.Count - 1; i > 0; i--)
            {
                this.listSentence[i].content = DeleteStartEnter(this.CutSameSentence(this.listSentence[i].content, this.listSentence[i - 1].content));
            }
        }


        /// <summary>
        /// 找到相同的部分并删除
        /// </summary>
        /// <param name="sentenceNeedCut"></param>
        /// <param name="sentenceCompare"></param>
        /// <returns></returns>
        private string CutSameSentence(string sentenceNeedCut, string sentenceCompare)
        {
            StringBuilder needCutSB = new StringBuilder(sentenceNeedCut);
            StringBuilder compareSB = new StringBuilder(sentenceCompare);
            if (needCutSB.Length == 0 || compareSB.Length == 0)
            {
                return sentenceNeedCut;
            }
            int mark=-1;
            int i=0;
            int j=0;
            for (i = needCutSB.Length - 1, j = compareSB.Length - 1; i >= 0; i--)
            {
                if (needCutSB[i] == compareSB[j])
                {
                    if (mark < 0)
                    {
                        mark = i;
                    }
                    j--;
                }
                else
                {
                    if (mark >= 0)
                    {
                        mark =-1;
                    }
                    j = compareSB.Length - 1;
                }
            }
            if (mark > 0)
            {
                int length = mark+1;
                if (length < needCutSB.Length)
                {
                    StringBuilder finalSB = needCutSB.Remove(0, length);
                    return finalSB.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return sentenceNeedCut;
            }
        }
        /// <summary>
        /// 删除开头的空格和回车
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string DeleteStartEnter(string sentence)
        {
            if (sentence.StartsWith("\r\n"))
            {
                sentence=sentence.Remove(0, 2);
                sentence=DeleteStartEnter(sentence);
            }
            if (sentence.StartsWith(" "))
            {
                sentence = sentence.Remove(0, 1);
                sentence = DeleteStartEnter(sentence);
            }
            return sentence;
        }

    }
}
