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
        public string content;
        public double startTime, endTime;
        public int count;
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
        public string layer;
        public string style;
        public string actor;
        public string marginL;
        public string marginR;
        public string marginV;
        public string effect;
        //string content
        public string textEffect;
        public MatchCollection matchCollectionEffect;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
