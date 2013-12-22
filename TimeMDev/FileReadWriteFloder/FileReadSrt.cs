using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileReadSrt : FileReadFunction
    {
        bool Additional;
        List<SingleSentence> listSingleSentence;
        public FileReadSrt(bool Additional,List<SingleSentence> listSingleSentence)
        {
            this.Additional = Additional;
            this.listSingleSentence = listSingleSentence;
        }
        public FileReadSrt()
        {
        }
        #region FileReadFunction 成员

        public void Read(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, ref Encoding encoding, ref string scriptInfo, ref string styles)
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            encoding = TimeLineReadWrite.GetEncoding(fileStream);
            StreamReader streamReader = new StreamReader(fileStream, encoding); 
            this.ReadSrt(listSingleSentence, streamReader);

            streamReader.Close();
            fileStream.Close();
        }
        #endregion
        /// <summary>
        /// 读取srt
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="streamReader"></param>
        private void ReadSrt(List<SingleSentence> listSingleSentenceMain, System.IO.StreamReader streamReader)
        {
            string content;
            bool runMark = true;
            string[] spiltChar = { ":", ",", "，", " --> " };
            //判断是不是追加
            if (this.Additional)
            {
                listSingleSentence.Clear();
            }
            else
            {
                this.listSingleSentence = listSingleSentenceMain;
                this.listSingleSentence.Clear();
            }
            //用之前先把这个东西初始化一下。有一个空的字节。
            SingleSentence singleSentenceS = new SingleSentence();
            singleSentenceS.content = "";
            singleSentenceS.startTime = 0;
            singleSentenceS.endTime = 0;
            listSingleSentence.Add(singleSentenceS);
            while (runMark)
            {
                try
                {
                    SingleSentence singleSentence = new SingleSentence();
                    bool readNumMark = true;
                    content = "";
                    while (readNumMark)
                    {
                        content = streamReader.ReadLine();
                        if (!content.Equals(""))
                        {
                            readNumMark = false;
                        }
                    }
                    singleSentence.count = Int32.Parse(content);
                    //以上是取序号
                    content = streamReader.ReadLine();
                    string[] time = content.Split(spiltChar, StringSplitOptions.RemoveEmptyEntries);
                    singleSentence.startTime = 3600 * double.Parse(time[0]) +
                                                            60 * double.Parse(time[1]) +
                                                            double.Parse(time[2]) +
                                                            0.001 * double.Parse(time[3]);
                    singleSentence.endTime = 3600 * double.Parse(time[4]) +
                                                            60 * double.Parse(time[5]) +
                                                            double.Parse(time[6]) +
                                                            0.001 * double.Parse(time[7]);
                    //取时间
                    bool readContentMark = true;
                    string content2 = "";
                    content = "";
                    bool fristSentence = true;
                    while (readContentMark)
                    {
                        content2 = streamReader.ReadLine();
                        if (content2.Equals(""))
                        {
                            readContentMark = false;
                        }
                        if (fristSentence)
                        {
                            singleSentence.content += content2;//2013-4-12  增加换行
                        }
                        else
                        {
                            singleSentence.content += "\r\n" + content2;//2013-4-12  增加换行
                        }
                        fristSentence = false;
                    }
                    //2013-12-22修改了末尾增加一行的bug
                    singleSentence.content = CCHandle.TrimEnterEnd(singleSentence.content);

                    //取内容
                    listSingleSentence.Add(singleSentence);
                }
                catch (Exception ee)
                {
                    runMark = false;
                }
            }


        }
    }

}
