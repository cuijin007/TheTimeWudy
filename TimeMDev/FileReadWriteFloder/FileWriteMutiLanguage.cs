using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteMutiLanguage:FileWriteFunction
    {
        string filePath;
        Encoding encoding=Encoding.Default;
        int chineseType;
        int englishType;
        int isAss;
        private event ContentFunctionD ContentFunction;
        AssInfo assInfo;
        /// <summary>
        /// 初始化多路输出
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="chineseType">中文格式0,没有1简体2繁体</param>
        /// <param name="englishType">英文格式</param>
        /// <param name="isAss">是否输出ass</param>
        public FileWriteMutiLanguage(string filePath,int chineseType,int englishType,int isAss)
        {
            this.filePath = filePath;
            this.chineseType = chineseType;
            this.englishType = englishType;
            this.isAss = isAss;
        }
        /// <summary>
        /// 初始化多路输出
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="chineseType"></param>
        /// <param name="englishType"></param>
        /// <param name="isAss"></param>
        /// <param name="assInfo"></param>
        public FileWriteMutiLanguage(string filePath, int chineseType, int englishType, int isAss,AssInfo assInfo)
        {
            this.filePath = filePath;
            this.chineseType = chineseType;
            this.englishType = englishType;
            this.isAss = isAss;
            this.assInfo = assInfo;
        }
        #region FileWriteFunctionMutiLanguage 成员
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="fileStream"></param>
        /// <param name="scriptInfo"></param>
        /// <param name="styles"></param>
        public void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
           // fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
           // StreamWriter streamWriter = new StreamWriter(fileStream,this.encoding);
            switch (this.chineseType)
            {
                case 0:
                    {
                        ContentFunction += new ContentFunctionD(this.RemoveChinese);
                        break;
                    }
                case 1:
                    {
                        ContentFunction+=new ContentFunctionD(this.ChangeTToS);
                        break;
                    }
                case 2:
                    {
                        ContentFunction+=new ContentFunctionD(this.ChangeSToT);
                        break;
                    }
            }
            switch (this.englishType)
            {
                case 0:
                    {
                        ContentFunction+=new ContentFunctionD(this.RemoveEnglish);
                        break;
                    }
                case 1:
                    {
                        break;
                    }
            }
            FileWriteFunction fileWriteFunction;
            if (this.isAss == 1)
            {
                FileWriteAss fileWriteAss =new FileWriteAss(this.encoding,this.assInfo);
                fileWriteAss.ContentFunction += ContentFunction;
                fileWriteFunction = fileWriteAss;
            }
            else
            {
                FileWriteSrt fileWriteSrt = new FileWriteSrt();
                fileWriteSrt.ContentFunction += ContentFunction;
                fileWriteFunction = fileWriteSrt;
            }
            fileWriteFunction.Write(listSingleSentence, fileStream, this.filePath, this.encoding, ref scriptInfo, ref styles);
            //streamWriter.Close();
            //fileStream.Close();
        }
        #endregion
        /// <summary>
        /// 简体中文变成繁体中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ChangeSToT(ref string input)
        {
            string[] spilt = new string[1];
            spilt[0] = @"{\";
            string[] buf = this.SpiltContent(input);
            string content = "";
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i].StartsWith(@"{\"))
                {
                    content += buf[i];
                }
                else
                {
                    content += CCHandle.ToTChinese(buf[i]);
                }
            }
            //    return CCHandle.ToTChinese(input);
            input = content;
            return content;
        }
        /// <summary>
        /// 去掉中文
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        private string RemoveChinese(ref string input)
        {
            string[] str = this.SpiltContent(input);
            for (int i = 0; i < str.Length; i++)
            {
                if (!str[i].StartsWith(@"{\"))
                {
                    string chinese,english;
                    CCHandle.SpiltRule(str[i], out chinese, out english);
                    str[i] = CCHandle.TrimEnterStart(CCHandle.TrimEnterEnd(english));
                }
            }
            for (int i = str.Length-1; i >=0; i--)
            {
                if (str[i].StartsWith(@"{\"))
                {
                    str[i] = "";

                }
                else
                {
                    break;
                }
            }
            string content2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                content2 += str[i];
            }
            input = content2;
            return input;
        }
        /// <summary>
        /// 去掉英文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string RemoveEnglish(ref string input)
        {
            string[] str = this.SpiltContent(input);
            for (int i = 0; i < str.Length; i++)
            {
                if (!str[i].StartsWith(@"{\"))
                {
                    string chinese, english;
                    CCHandle.SpiltRule(str[i], out chinese, out english);
                    str[i] = CCHandle.TrimEnterStart(CCHandle.TrimEnterEnd(chinese));
                }
            }
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i].StartsWith(@"{\"))
                {
                    str[i] = "";

                }
                else
                {
                    break;
                }
            }
            string content2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                content2 += str[i];
            }
            input = content2;
            return input;
        }
        /// <summary>
        /// 繁体中文变成简体中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ChangeTToS(ref string input)
        {
            string[] spilt = new string[1];
            spilt[0] = @"{\";
            string[] buf = this.SpiltContent(input);
            string content = "";
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i].StartsWith(@"{\"))
                {
                    content +=buf[i];
                }
                else
                {
                    content += CCHandle.ToSChinese(buf[i]);
                }
            }
            //    return CCHandle.ToTChinese(input);
            input = content;
            return content;
        }

        private string[] SpiltContent(string content)
        {
            content=content.Replace(@"{\",@"{$$$$\");
            string[] str = content.Split('{', '}');
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].StartsWith(@"$$$$\"))
                {
                    str[i] = @"{\" + str[i].Replace(@"$$$$\", "") + "}";
                }
            }
            return str;
        }
    }
}



//private string RemoveChinese(ref string input)
//        {
//            string[] spilt = new string[1];
//            spilt[0] = @"{\";
//            //string[] buf6 = Regex.;
            
//            string[] buf = input.Split(spilt, StringSplitOptions.RemoveEmptyEntries);
//            string content = "";
//            for (int i = 0; i < buf.Length; i++)
//            {
//                if (buf[i].EndsWith("}"))
//                {
//                    content += @"{\" + buf[i];
//                }
//                else
//                {
//                    string chinese, english;
//                    CCHandle.SpiltRule(buf[i], out chinese, out english);
//                    content += english;
//                }
//            }
//            buf = content.Split(spilt, StringSplitOptions.None);
//            string content2 = "";
//           bool isDeleteEffectOver = false;
//            for (int i = buf.Length - 1; i >= 0; i--)
//            {
//                if (buf[i].EndsWith("}")&&isDeleteEffectOver!=true)
//                {
//                    buf[i] = "";
//                }
//                else if (buf[i].EndsWith("}"))
//                {
//                    buf[i]=@"(\"+buf[i];
//                    //break;                    
//                }
//            }
//            for (int i = 0; i < buf.Length; i++)
//            {
//                content2 += buf[i];
//            }
//            input = content2;
//                return content2;
//        }