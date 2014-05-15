using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord.cchandle
{
    /// <summary>
    /// cc字幕的首次合并和拆分，先按
    /// 末尾是不是有标点，轴是不是在一起的方式进行合并，之后再进行拆分
    /// </summary>
    public class FristMergeAndResolution
    {
        double mergeTimeBetween = 0.5;//超过0.5s不合并
        string[] endCase = {",","?","!",".","。" };//结束标识
        int maxLength = 74;//单句最长字符数
        int maxCountCutMark = 10;//从后面数第几个位置是切字符串的位置

        int resolutionTimeLength=5;
        string[] importantWord={"and", "or", "that", "who", "which", "when", "how", "why", "what", "if", "whether", "but", "until",",",".","?","!"};

        string[] resolutionCutCase = { " ", ",", ".", "!", "?", "-" };


        List<SingleSentence> listSingleSentence;
        public FristMergeAndResolution(List<SingleSentence> listSingleSentence)
        {
            this.listSingleSentence = listSingleSentence;
        }
        /// <summary>
        /// 拆分和合并
        /// </summary>
        public void MergeResolutionFunction()
        {
            this.Merge();

            CCHandle.UpToLow(listSingleSentence);
            CCHandle.TurnUpLowPunctuation(this.listSingleSentence);
            CCHandle.TurnUpLowName(this.listSingleSentence);

            this.Resolution();
        }

        /// <summary>
        /// 合并
        /// </summary>
        private void Merge()
        {
            for (int i = this.listSingleSentence.Count - 2; i > 0; i--)
            {
                if (Math.Abs(this.listSingleSentence[i].endTime - this.listSingleSentence[i + 1].startTime) < mergeTimeBetween)
                {
                    if (!this.JudgeEndCase(this.listSingleSentence[i].content))
                    {
                        this.listSingleSentence[i].endTime = this.listSingleSentence[i].endTime;
                        this.listSingleSentence[i].content += " "+this.listSingleSentence[i + 1].content;
                        this.listSingleSentence.RemoveAt(i + 1);
                    }
                }
            }
        }
        /// <summary>
        /// 拆分
        /// </summary>
        private void Resolution()
        {
            for (int i = this.listSingleSentence.Count - 1; i > 0; i--)
            {
                List<SingleSentence> result=this.CutSentence(this.listSingleSentence[i]);
                if (result != null)
                {
                    this.listSingleSentence.RemoveAt(i);
                    for (int j = 0; j < result.Count; j++)
                    {
                        this.listSingleSentence.Insert(i + j, result[j]);
                    }
                }
            }
        }

        /// <summary>
        /// 将句子拆分成子句
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private List<SingleSentence> CutSentence(SingleSentence sentence)
        {
            if (sentence.content.Length > this.maxLength)
            {
                string sentenceContentBuf = sentence.content;
                List<SingleSentence> returnSentence = new List<SingleSentence>();
                string[] sentenceAfterSpilt;
                for (int i = 0; i < this.resolutionCutCase.Length; i++)
                {
                    sentenceContentBuf= sentenceContentBuf.Replace(this.resolutionCutCase[i], "$%$#" + this.resolutionCutCase[i] + "$%$#");
                }
                string[] spiltWord = { "$%$#" };
                sentenceAfterSpilt = sentenceContentBuf.Split(spiltWord, StringSplitOptions.None);

                int plusAllMark = -1;
                if (sentenceAfterSpilt.Length > this.maxCountCutMark)
                {
                    for (int i = sentenceAfterSpilt.Length - this.maxCountCutMark; i > 0; i--)
                    {
                        if (this.JudgeImportantWord(sentenceAfterSpilt[i]))
                        {
                            plusAllMark = i;
                            break;
                        }
                    }

                    if (plusAllMark < 0)
                    {
                        return null;
                    }
                    
                    SingleSentence result1 = CopyObject.DeepCopy<SingleSentence>(sentence);
                    SingleSentence result2 = CopyObject.DeepCopy<SingleSentence>(sentence);
                    result1.content = "";
                    result2.content = "";
                    double allTime = Math.Abs(sentence.endTime - sentence.startTime);
                    result1.endTime = result1.startTime + allTime * (plusAllMark / sentenceAfterSpilt.Length);
                    result2.startTime = result1.endTime + 0.01;

                    for (int k1 = 0; k1 < plusAllMark; k1++)
                    {
                        result1.content += sentenceAfterSpilt[k1];
                    }
                    for (int k2 = plusAllMark; k2 < sentenceAfterSpilt.Length; k2++)
                    {
                        result2.content += sentenceAfterSpilt[k2];
                    }


                    returnSentence.Insert(0, result1);
                    returnSentence.Insert(1, result2);
                    List<SingleSentence> returnSentenceBuf = CutSentence(result1);
                    if (returnSentenceBuf != null)
                    {
                        returnSentence.RemoveAt(0);
                        for (int j = 0; j < returnSentenceBuf.Count; j++)
                        {
                            returnSentence.Insert(j, returnSentenceBuf[j]);
                        }
                        return returnSentence;
                    }
                    else
                    {
                        return returnSentence;
                    }
                   

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        private bool JudgeImportantWord(string word)
        {
            for (int i = 0; i < this.importantWord.Length; i++)
            {
                if (importantWord[i].ToUpper().Equals(word)||
                    importantWord[i].ToLower().Equals(word))
                {
                    return true;
                }
                
            }
            return false;
        }


        /// <summary>
        /// 判断末尾是不是标点
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private bool JudgeEndCase(string sentence)
        {
            for (int i = 0; i < endCase.Length; i++)
            {
                if (sentence.EndsWith(endCase[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
