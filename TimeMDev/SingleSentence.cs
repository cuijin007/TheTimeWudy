using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeMDev
{
    [Serializable]
    public class SingleSentence
    {
        private string contentSave;
        public string content
        {
            get
            {
                return contentSave;
            }
            set
            {
                this.contentSave = value;
                this.CaculateString(out this.lineNum, out this.everyLineLength, this.contentSave);
            }
        }
        public bool Checked = false;

        public double startTime, endTime;
        /// <summary>
        /// 时长
        /// </summary>
        public double timeLength
        {
            get
            {
                return endTime - startTime;
            }
        }
        public int count;
        public int lineNum;
        public string everyLineLength;
        /// <summary>
        /// 是否判断选中
        /// </summary>
        public bool isSelected = false;
        /// <summary>
        /// 是否在外界选中 
        /// </summary>
        public bool isSelected2 = false;
        public bool isKeyBoard = false;
        //所有的需要的ass需要的变量。
        public string layer="0";
        public string style = "*Default";
        public string actor = "说话人";
        public string marginL = "0000";
        public string marginR = "0000";
        public string marginV = "0000";
        public string effect;
        //string content
        public string textEffect;
        public MatchCollection matchCollectionEffect;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        /// <summary>
        /// 计算行数和每行的长度
        /// </summary>
        /// <param name="lineNum"></param>
        /// <param name="everyLineLength"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool CaculateString(out int lineNum,out string everyLineLength,string content)
        {
            lineNum=0;
            everyLineLength = "";
            string[] spiltChar=new string[1];
            spiltChar[0]="\r\n";
            string[] spilt=content.Split(spiltChar,StringSplitOptions.None);
            lineNum = spilt.Length;
           
            for (int i = 0; i < spilt.Length; i++)
            {
                everyLineLength += spilt[i].Length;
                everyLineLength += "\r\n";
            }
            return true;
        }
    }

}
