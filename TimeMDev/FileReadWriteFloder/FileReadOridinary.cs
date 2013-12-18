using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    /// <summary>
    /// 读取翻译稿
    /// </summary>
    class FileReadOridinary:FileReadFunction
    {
        private bool BlankInterval;
        /// <summary>
        /// 是否按行读取
        /// </summary>
        /// <param name="BlankInterval"></param>
        public FileReadOridinary(bool BlankInterval)
        {
            this.BlankInterval = BlankInterval;
        }
        /// <summary>
        /// 一行一个为一个翻译段进行读取
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="streamReader"></param>
        private void ReadByLine(List<SingleSentence> listSingleSentence, System.IO.StreamReader streamReader)
        {
            listSingleSentence.Clear();
            string str="";
            while (str != null)
            {
                str = streamReader.ReadLine();
                if (str == null)
                {
                    break;
                }
                SingleSentence sentence = new SingleSentence();
                sentence.content = str;
                listSingleSentence.Add(sentence);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="streamReader"></param>
        private void ReadByBlank(List<SingleSentence> listSingleSentence, System.IO.StreamReader streamReader)
        {
            listSingleSentence.Clear();
            string str = "";
            while (str != null)
            {
                str = "";
                bool fristTime = true;
                while (str == null || str.Equals(""))
                {
                    if (!fristTime)
                    {
                        str += "\r\n";
                    }
                    str += streamReader.ReadLine();
                }
                SingleSentence sentence = new SingleSentence();
                sentence.content = str;
                listSingleSentence.Add(sentence);
            }
        }

        #region FileReadFunction 成员

        public void Read(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath,ref Encoding encoding, ref string scriptInfo, ref string styles)
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            encoding = TimeLineReadWrite.GetEncoding(fileStream);
            StreamReader streamReader = new StreamReader(fileStream, encoding); 
            if (this.BlankInterval)
            {
                this.ReadByBlank(listSingleSentence, streamReader);
            }
            else
            {
                this.ReadByLine(listSingleSentence, streamReader);
            }

            streamReader.Close();
            fileStream.Close();
        }

        #endregion

    }
}
