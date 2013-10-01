using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev
{
    class TimeLineReadWrite
    {
        public List<SingleSentence> listSingleSentence = new List<SingleSentence>();
        public Encoding encoding = Encoding.Default;
        public string filePath = "Bs.srt";
        FileStream fileStream;
        StreamWriter streamWriter;
        StreamReader streamReader;
        public TimeLineReadWrite()
        {

        }
        public void Init(List<SingleSentence> listSingleSentence, string filePath, bool createNew)
        {
            this.listSingleSentence = listSingleSentence;
            this.filePath = filePath;
            if (streamReader != null)
            {
                streamReader.Close();
            }
            if (streamWriter != null)
            {
                streamWriter.Close();
            }
            if (fileStream != null)
            {
                fileStream.Close();
            }
            if (createNew)
            {
                if (File.Exists(this.filePath))
                {
                    File.Delete(this.filePath);
                }
                fileStream = new FileStream(this.filePath, FileMode.CreateNew, FileAccess.ReadWrite);
                StreamWriter streamWriter2 = new StreamWriter(fileStream);
                streamWriter2.Close();
                fileStream.Close();
            }
            else
            {
                fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fileStream.Close();
            }
            // streamWriter = new StreamWriter(fileStream);
            //streamReader = new StreamReader(fileStream);
        }
        public void ReadAllTimeline()
        {
            fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            this.encoding = this.GetEncoding(fileStream);
            streamReader = new StreamReader(fileStream, this.encoding);
            string content;
            bool runMark = true;
            string[] spiltChar = { ":", ",", "，", " --> " };
            this.listSingleSentence.Clear();
            //用之前先把这个东西初始化一下。
            SingleSentence singleSentenceS = new SingleSentence();
            singleSentenceS.content = "";
            singleSentenceS.startTime = 0;
            singleSentenceS.endTime = 0;
            this.listSingleSentence.Add(singleSentenceS);
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
                    this.listSingleSentence.Add(singleSentence);
                }
                catch (Exception ee)
                {
                    runMark = false;
                }
            }
            streamReader.Close();
            fileStream.Close();
            SingleSentence singleSentenceE = new SingleSentence();
            singleSentenceE.content = "";
            singleSentenceE.startTime = 36000;
            singleSentenceE.endTime = 36000;
            this.listSingleSentence.Add(singleSentenceE);
        }
        public void WriteAllTimeline()//这里可能有问题
        {
            fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream, this.encoding);
            int hour, minute, second, minsec;
            for (int i = 1; i < this.listSingleSentence.Count - 1; i++)//崔进修改于2013-4-9  只写中间的部分
            {
                //写号
                //this.streamWriter.WriteLine(i + 1);
                this.streamWriter.WriteLine(i);//修改于2013-4-10
                //写时间
                double startTime = listSingleSentence[i].startTime;
                hour = (int)startTime / 3600;
                minute = (int)(startTime - 3600 * hour) / 60;
                second = (int)(startTime - 3600 * hour - 60 * minute);
                minsec = (int)((startTime - (int)startTime) * 1000);
                string startTimeStr = hour + ":" + minute + ":" + second + "," + minsec;
                double endTime = listSingleSentence[i].endTime;
                hour = (int)endTime / 3600;
                minute = (int)(endTime - 3600 * hour) / 60;
                second = (int)(endTime - 3600 * hour - 60 * minute);
                minsec = (int)((endTime - (int)endTime) * 1000);
                string endTimeStr = hour + ":" + minute + ":" + second + "," + minsec;
                this.streamWriter.WriteLine(startTimeStr + " --> " + endTimeStr);
                //写内容
                this.streamWriter.WriteLine(listSingleSentence[i].content);
                this.streamWriter.WriteLine("");
            }
            streamWriter.Close();
            fileStream.Close();
        }

        public List<SingleSentence> GetListSingleSentence()
        {
            return this.listSingleSentence;
        }

        public string TimeOut(double time)
        {
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            int minsec = (int)((time - (int)time) * 1000);
            string TimeStr = hour + ":" + minute + ":" + second + "," + minsec;
            return TimeStr;
        }
        public double TimeIn(string time)
        {
            string[] timeBuffer;
            string[] spiltChar = { ",", "，", ":" };
            timeBuffer = time.Split(spiltChar, StringSplitOptions.RemoveEmptyEntries);
            if (timeBuffer.Length < 4)
            {
                return -1;
            }
            else
            {
                double timedouble = Double.Parse(timeBuffer[0]) * 3600 + Double.Parse(timeBuffer[1]) * 60 + Double.Parse(timeBuffer[2]) + Double.Parse(timeBuffer[3]) * 0.001;
                return timedouble;
            }
        }

        public void CreateNewFile(string path)
        {
            File.Create(path);
        }
        //获取字符编码
        public Encoding GetEncoding(FileStream fs)
        {
            byte[] bytes = new byte[2];
            fs.Read(bytes, 0, 2);
            fs.Seek(0, SeekOrigin.Begin);
            if (bytes[0] >= 0xef)
            {
                if (bytes[0] == 0xef && bytes[1] == 0xbb)
                {
                    return Encoding.UTF8;
                }
                else if (bytes[0] == 0xfe && bytes[1] == 0xff)
                {
                    return Encoding.BigEndianUnicode;
                }
                else if (bytes[0] == 0xff && bytes[1] == 0xfe)
                {
                    return Encoding.Unicode;
                }
                else
                {
                    return Encoding.Default;
                }
            }
            else
            {
                return Encoding.Default;
            }
        }
    }
}
