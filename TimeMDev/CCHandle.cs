using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using Microsoft.VisualBasic;

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

        /// <summary>
        /// 删除注释
        /// </summary>
        /// <param name="listSingleSentence"></param>
        public static void DeleteRemark(List<SingleSentence> listSingleSentence)
        {
            for (int i = 0; i < listSingleSentence.Count; i++)
            {
                listSingleSentence[i].content = Regex.Replace(listSingleSentence[i].content, @"\<.*\>", "");
                listSingleSentence[i].content=Regex.Replace(listSingleSentence[i].content, "\\[.*?\\]|\\(.*?\\)", "");
                if (listSingleSentence[i].content.Equals(""))
                {
                    listSingleSentence.Remove(listSingleSentence[i]);
                    i--;
                }
            }
        }

        /// <summary>
        /// 大小写格式转化，Mrs.，首字母大写等
        /// </summary>
        /// <param name="listSingleSentence"></param>
        public static void TurnUpLowPunctuation(List<SingleSentence> listSingleSentence)
        {
            //TextInfo textInfo=Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(
            StringBuilder sb = new StringBuilder(listSingleSentence[1].content);
            if (sb.Length > 0)
            {
                sb[0]=char.ToUpper(sb[0]);
                listSingleSentence[1].content = sb.ToString();
            }
            bool findMark=false;
            for (int i = 1; i < listSingleSentence.Count; i++)
            {
                sb = new StringBuilder(listSingleSentence[i].content);
                for (int j = 0; j < sb.Length; j++)
                {
                    if (findMark&&sb[j]!=' ')
                    {
                        sb[j]=char.ToUpper(sb[j]);
                        findMark = false;
                    }
                    if (sb[j] == '!' || sb[j] == '?' || sb[j] == '.')
                    {
                        findMark = true;
                    }
                    if (sb[j] =='i')
                    {
                        if (sb.Length>1)
                        {
                            if (j == 0)
                            {
                                if (sb[j + 1] == ' ')
                                {
                                    sb[j] = char.ToUpper(sb[j]);
                                }
                            }
                            else if (j == sb.Length - 1)
                            {
                                if (sb[j - 1] == ' ')
                                {
                                    sb[j] = char.ToUpper(sb[j]);
                                }
                            }
                            else
                            {
                                if (sb[j - 1] == ' ' && sb[j + 1] == ' ')
                                {
                                    sb[j] = char.ToUpper(sb[j]);
                                }
                                if (sb[j - 1] == ' ' && sb[j + 1] == '\'')
                                {
                                    sb[j] = char.ToUpper(sb[j]);
                                }
                            }
                        }
                        else
                        {
                            sb[j]=char.ToUpper(sb[j]);
                        }
                    }
                }
                listSingleSentence[i].content = sb.ToString();
            }
        }
    
        /// <summary>
        /// 根据组合将名字判断大小写
        /// </summary>
        /// <param name="?"></param>
        public static void TurnUpLowName(List<SingleSentence> listSingleSentence)
        {
            for (int i = 0; i < listSingleSentence.Count; i++)
            {
                listSingleSentence[i].content = listSingleSentence[i].content.Replace(" mrs.", " Mrs.");
                listSingleSentence[i].content = listSingleSentence[i].content.Replace(" dr.", " Dr.");
                listSingleSentence[i].content = listSingleSentence[i].content.Replace(" ms.", "Ms.");
                listSingleSentence[i].content = listSingleSentence[i].content.Replace(" mr.", " Mr.");
            }
        }

        /// <summary>
        /// 拆分规则
        /// </summary>
        /// <param name="content"></param>
        /// <param name="chinese"></param>
        /// <param name="english"></param>
        public static void SpiltRule(string content, out string chinese, out string english)
        {
            chinese = "";
            english = "";
            MatchCollection matchCollection = Regex.Matches("崔进抗八代"+content, "([\u4E00-\u9FA5]|[\uFE30-\uFFA0]).*([\u4E00-\u9FA5]|[\uFE30-\uFFA0])");
            if (matchCollection.Count > 0)
            {
                chinese = matchCollection[0].ToString();
                chinese = chinese.Replace("崔进抗八代", "");
            }
            if (!chinese.Equals(""))
            {
                english = content.Replace(chinese, "");
            }
            else
            {
                english = content;
            }
        }

        /// <summary>
        /// 从中文中获取英文字符
        /// </summary>
        /// <param name="content"></param>
        /// <param name="english"></param>
        /// <returns></returns>
        public static bool GetEnglishFromChinese(string content, out List<string> english)
        {
            string allChinese;//带英文的所有中文字段
            english = new List<string>();
            MatchCollection matchCollection = Regex.Matches("崔进抗八代" + content, "([\u4E00-\u9FA5]|[\uFE30-\uFFA0]).*([\u4E00-\u9FA5]|[\uFE30-\uFFA0])");
            if (matchCollection == null || matchCollection.Count == 0)
            {
                return false;
            }
            else
            {
                allChinese = matchCollection[0].ToString();
                allChinese = allChinese.Replace("崔进抗八代", "");
            }
            MatchCollection EnglishCollection = Regex.Matches(allChinese, @"[A-Za-z][A-Za-z\s]*[A-Za-z]");
            if (EnglishCollection == null && EnglishCollection.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < EnglishCollection.Count; i++)
                {
                    string name = EnglishCollection[i].ToString();
                    english.Add(name);
                }
            }
            return true;
        }
        /// <summary>
        /// 检查是否含有全角字符
        /// </summary>
        /// <param name="content">输入的变量</param>
        /// <returns>true，有全角，false，没有全角</returns>
        public static bool JudgeFullWidthSymbol(string content)
        {
            MatchCollection matchCollection = Regex.Matches(content, "[\uFF00-\uFFFF]");
            if(matchCollection!=null&&matchCollection.Count>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }
        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 去掉首部的换行符
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string TrimEnterStart(string str)
        {
            str = str.TrimStart('\r', '\n');
            str = str.TrimStart('\r', '\n');
            return str;
        }
        /// <summary>
        /// 去掉末尾的换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimEnterEnd(string str)
        {
            str = str.TrimEnd('\r', '\n');
            str = str.TrimEnd('\r', '\n');
            return str;
        }
    }
}
