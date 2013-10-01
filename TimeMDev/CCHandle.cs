using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev
{
    public class CCHandle
    {
        public static void HandleCCLongitudinal(List<SingleSentence> listSingleSentence)
        {
            HandleCCMutilineL(listSingleSentence);
            UpToLow(listSingleSentence);
        }
        /// <summary>
        /// 处理多行字幕,纵向的
        /// </summary>
        private static void HandleCCMutilineL(List<SingleSentence> listSingleSentence)
        {
            string str = "";
            if (listSingleSentence.Count > 0)
            {
                str = listSingleSentence[0].content;
                str=str.Replace("\r", "");
                str = str.Replace("\n", "");
                for (int i = 1; i < listSingleSentence.Count; i++)
                {
                    listSingleSentence[i].content = listSingleSentence[i].content.Replace(str, "");
                    listSingleSentence[i].content = listSingleSentence[i].content.Replace("\r", "");
                    listSingleSentence[i].content = listSingleSentence[i].content.Replace("\n", "");
                    str = listSingleSentence[i].content;
                }
            }
        }
        /// <summary>
        /// 大写转小写。
        /// </summary>
        /// <param name="listSingleSentence"></param>
        private static void UpToLow(List<SingleSentence> listSingleSentence)
        {
            for (int i = 0; i < listSingleSentence.Count; i++)
            {
                listSingleSentence[i].content = listSingleSentence[i].content.ToLowerInvariant();
            }
        }

        
    }
}
