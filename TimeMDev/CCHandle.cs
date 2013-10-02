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
            HandleCCMutilineLong(listSingleSentence);
            UpToLow(listSingleSentence);
        }
        /// <summary>
        /// 处理多行字幕,纵向的
        /// </summary>
        private static void HandleCCMutilineLong(List<SingleSentence> listSingleSentence)
        {
            string str = "";
            char[] spilt = new char[2];
            spilt[0] = '\r';
            spilt[1] = '\n';
            string[] strSave;
            if (listSingleSentence.Count >=1)
            {
                str = listSingleSentence[1].content;
                //str=str.Replace("\r", "");
                //str = str.Replace("\n", "");
                for (int i = 1; i < listSingleSentence.Count; i++)
                {
                    try
                    {
                        //listSingleSentence[i].content = listSingleSentence[i].content.Replace(str, "");
                        //listSingleSentence[i].content = listSingleSentence[i].content.Replace("\r", "");
                        //listSingleSentence[i].content = listSingleSentence[i].content.Replace("\n", "");
                        strSave = listSingleSentence[i].content.Split(spilt,StringSplitOptions.RemoveEmptyEntries);
                        if (strSave.Length > 0)
                        {
                            listSingleSentence[i].content = strSave[0];
                        }
                    }
                    catch
                    {
 
                    }
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
        public static void HandleCCLatitude(List<SingleSentence> listSingleSentence)
        {
            HandleCCMutilineLa(listSingleSentence);
            UpToLow(listSingleSentence);
        }
        private static void HandleCCMutilineLa(List<SingleSentence> listSingleSentence)
        {
            string[] str;
            char[] spiltStr=new char[2];
            spiltStr[0]='\r';
            spiltStr[1]='\n';
            int start, end;
            start = 1;
            for (int i = 1; i < listSingleSentence.Count; i++)
            {
                end = i;
                str = listSingleSentence[i].content.Split(spiltStr, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length>= 2)
                {
                    if (!str[0].Equals("") &&!str[1].Equals(""))
                    {
                        SingleSentence node = new SingleSentence();
                        node.startTime = listSingleSentence[start].startTime;
                        node.endTime = listSingleSentence[end].endTime;
                        node.content = str[0];
                        for (int j = end; j >=start; j--)
                        {
                            listSingleSentence.Remove(listSingleSentence[j]);
                        }
                        listSingleSentence.Insert(start, node);
                        start++;
                        i = start;
                    }
                }
            }
        }
    }
}
