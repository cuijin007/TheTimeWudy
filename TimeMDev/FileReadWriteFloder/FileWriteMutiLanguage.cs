using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream,this.encoding);
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
                FileWriteAss fileWriteAss = new FileWriteAss();
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
            streamWriter.Close();
            fileStream.Close();
        }
        #endregion
        /// <summary>
        /// 简体中文变成繁体中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ChangeSToT(string input)
        {
            return CCHandle.ToTChinese(input);
        }
        /// <summary>
        /// 去掉中文
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        private string RemoveChinese(string input)
        {
            string chinese,english;
            CCHandle.SpiltRule(input, out chinese, out english);
            return english;
        }
        /// <summary>
        /// 去掉英文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string RemoveEnglish(string input)
        {
            string chinese, english;
            CCHandle.SpiltRule(input, out chinese, out english);
            return chinese;
        }
        /// <summary>
        /// 繁体中文变成简体中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ChangeTToS(string input)
        {
            return CCHandle.ToTChinese(input);
        }
    
        
    }
}
