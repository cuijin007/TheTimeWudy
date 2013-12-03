using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileReadSrt : FileReadFunction
    {
        #region FileReadFunction 成员

        public void Read(List<SingleSentence> listSingleSentence, System.IO.StreamReader streamReader)
        {
            string content;
            bool runMark = true;
            string[] spiltChar = { ":", ",", "，", " --> " };
            listSingleSentence.Clear();
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

                    //取内容
                    listSingleSentence.Add(singleSentence);
                }
                catch (Exception ee)
                {
                    runMark = false;
                }
            }

        #endregion
        }
    }
}
