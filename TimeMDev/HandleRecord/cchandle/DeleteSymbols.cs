using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeMDev.ConfigSave
{
    /// <summary>
    /// 删除多余的符号
    /// </summary>
    public class DeleteSymbols
    {
        List<SingleSentence> listSingleSentence;
        public DeleteSymbols(List<SingleSentence> listSingleSentence)
        {
            this.listSingleSentence = listSingleSentence;
        }
        /// <summary>
        /// 删除开头的空格和回车和“-”
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string DeleteStartEnter(string sentence)
        {
            if (sentence.StartsWith("\r\n"))
            {
                sentence = sentence.Remove(0, 2);
                sentence = DeleteStartEnter(sentence);
            }
            if (sentence.StartsWith(" "))
            {
                sentence = sentence.Remove(0, 1);
                sentence = DeleteStartEnter(sentence);
            }
            if (sentence.StartsWith("-"))
            {
                sentence = sentence.Remove(0, 1);
                sentence = DeleteStartEnter(sentence);
            }
            if (sentence.StartsWith(">"))
            {
                sentence = sentence.Remove(0, 1);
                sentence = DeleteStartEnter(sentence);
            }
            return sentence;
        }
        /// <summary>
        /// 删除末尾的换行和空格
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string DeleteEndEnter(string sentence)
        {
            if (sentence.EndsWith("\r\n"))
            {
                sentence = sentence.Remove(sentence.Length-2, 2);
                sentence = DeleteStartEnter(sentence);
            }
            if (sentence.EndsWith(" "))
            {
                sentence = sentence.Remove(sentence.Length - 1, 1);
                sentence = DeleteStartEnter(sentence);
            }
            return sentence;
        }
        /// <summary>
        /// 删除多余的空格，将多个空格的地方变成一个空格
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string DeleteMutiSpace(string sentence)
        {
            if (sentence.Contains("  "))
            {
                sentence = sentence.Replace("  ", " ");
                sentence=this.DeleteMutiSpace(sentence);
            }
            if (sentence.Contains("\r\n"))
            {
                sentence = sentence.Replace("\r\n", " ");
                sentence = this.DeleteMutiSpace(sentence);
            }
            return sentence;
        }
        /// <summary>
        /// 删除注释
        /// </summary>
        /// <param name="sentence"></param>
        private string DeleteSpecialEffect(string sentence)
        {
            sentence = Regex.Replace(sentence, @"\<.*\>", "");
            sentence = Regex.Replace(sentence, "\\[.*?\\]|\\(.*?\\)", "");
            return sentence;
        }
        public void DeleteAction()
        {
            for (int i = this.listSingleSentence.Count-1; i >0; i--)
            {
                this.listSingleSentence[i].content = this.DeleteSpecialEffect(this.listSingleSentence[i].content);
                this.listSingleSentence[i].content = this.DeleteStartEnter(this.listSingleSentence[i].content);
                this.listSingleSentence[i].content = this.DeleteEndEnter(this.listSingleSentence[i].content);
                this.listSingleSentence[i].content = this.DeleteMutiSpace(this.listSingleSentence[i].content);
                if (this.listSingleSentence[i].content.Equals(""))
                {
                    this.listSingleSentence.RemoveAt(i);
                }
            }
        }
    }
}
