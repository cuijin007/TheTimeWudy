using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using TimeMDev.FileReadWriteFloder;

namespace TimeMDev
{
    public class TimeLineReadWrite
    {
        public List<SingleSentence> listSingleSentence = new List<SingleSentence>();
        public Encoding encoding = Encoding.Default;
        public string filePath = "Bs.srt";
        FileStream fileStream;
        StreamWriter streamWriter;
        StreamReader streamReader;
        string scriptInfo = "[Script Info]\r\n; // 此字幕由TimeM生成\r\n; // 欢迎访问人人影视 http://www.YYeTs.net"+
                                        "\r\nTitle:YYeTs"+
                                        "\r\nOriginal Script:YYeTs"+
                                        "\r\nSynch Point:0"+
                                        "\r\nScriptType:v4.00+"+
                                        "\r\nCollisions:Normal"+
                                        "\r\nTimer:100.0000";
        string styles = "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding" +
                                "\r\nStyle: Default,微软雅黑,19,&H00FFFFFF,&H0000FFFF,&H00000000,&H80000000,0,0,0,0,100,100,0,0,1,2,3,2,20,20,20,1";
            //"\r\nStyle: Default,方正黑体简体,21,&H00FFFFFF,&HF0000000,&H006C3300,&H00000000,-1,0,0,0,100,100,0,0.00,1,2,1,2,5,5,5,134";
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
            this.encoding = GetEncoding(fileStream);
            streamReader = new StreamReader(fileStream, this.encoding);
            string content;
            bool runMark = true;
            string[] spiltChar = { ":", ",", "，", " --> " };
            this.listSingleSentence.Clear();
            //用之前先把这个东西初始化一下。有一个空的字节。
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
                    //2013-12-22修改了末尾增加一行的bug
                    singleSentence.content = CCHandle.TrimEnterEnd(singleSentence.content);
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
        public static string TimeOut(double time)
        {
            /*
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            int minsec = (int)((time - (int)time) * 1000);
            string TimeStr = hour + ":" + minute + ":" + second + "," + minsec;
            return TimeStr;
             * */
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            int minsec = (int)(time*1000 - ((int)time)*1000);//2013-12-22将原有的转换函数转变否则会出现700转为699的问题
            string TimeStr = hour.ToString().PadLeft(2,'0') + ":" + minute.ToString().PadLeft(2,'0') + ":" + second.ToString().PadLeft(2,'0') + "," + minsec.ToString().PadLeft(3,'0');
            return TimeStr;
        }
        public static double TimeIn(string time)
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
        static public Encoding GetEncoding(FileStream fs)
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

        public void ReadAllTimeLineAss()
        {
            fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            this.encoding = GetEncoding(fileStream);
            streamReader = new StreamReader(fileStream, this.encoding);
            bool runMark=true;
            int readPositionState=0;//读取的位置的变量
            if (this.ReadHeadInfo(streamReader))
            {
                this.ReadAllEvent(streamReader);
            }
            streamReader.Close();
            fileStream.Close();
        }

        private bool ReadHeadInfo(StreamReader streamReader)
        {
            bool runMark=true;
            int state=-1;
            try
            {
                string str="";
                while(runMark)
                {
                    str=streamReader.ReadLine();
                    if (str.Equals(""))
                    {
                        continue;
                    }
                    if(str.Equals("[Events]"))
                    {
                        runMark=false;
                        break;
                    }
                    if(str.Equals("[Script Info]"))
                    {
                        state=1;//开始记录ScriptInfo
                        this.scriptInfo="";
                        continue;
                    }
                    if(str.Equals("[V4+ Styles]"))
                    {
                        state=2;
                        this.styles="";
                        continue;
                    }
                    if(state==1)
                    {
                        this.scriptInfo+=str+"\r\n";
                    }
                    if(state==2)
                    {
                        this.styles+=str+"\r\n";
                    }
                }
                return true;
            }
            catch
            {
                runMark=false;
                return false;
            }
        }
        private bool ReadAllEvent(StreamReader streamReader)
        {
            string[] dictionaryType;
            string[] spiltChar = { ":", ","};
             string[] spiltChar2 = {","};
            string[] markStr={"Layer", "Start", "End", "Style", "Actor", "MarginL", "MarginR", "MarginV", "Effect", "Text"};
            string str =streamReader.ReadLine();
            bool runMark=true;
            int[] getPos=new int[markStr.Length];
            for(int i=0;i<getPos.Length;i++)
            {
                getPos[i]=-1;
            }
            this.listSingleSentence.Clear();
            SingleSentence singleSentenceS = new SingleSentence();
            singleSentenceS.content = "";
            singleSentenceS.startTime = 0;
            singleSentenceS.endTime = 0;
            this.listSingleSentence.Add(singleSentenceS);
            ///初始化
            ///开始进行格式拆分
            if(str.StartsWith("Format:"))
            {
                str=str.Replace("Format","");
                str=str.Replace(" ","");
                dictionaryType=str.Split(spiltChar,StringSplitOptions.RemoveEmptyEntries);
                for(int i=0;i<markStr.Length;i++)
                {
                    for(int j=0;j<dictionaryType.Length;j++)
                    {
                        if(markStr[i].Equals(dictionaryType[j]))
                        {
                            getPos[i]=j;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            while(runMark)
            {
                try
                {
                    str=streamReader.ReadLine();                    
                    if (str.Equals(""))
                    {
                        continue;
                    }
                    SingleSentence singleSentence = new SingleSentence();
                    str=str.Replace("Dialogue:","");
                    singleSentence.matchCollectionEffect = Regex.Matches(str, @"\{.*\}");                    
                    str = Regex.Replace(str, @"\{.*\}", "");
                    string[] dialog=str.Split(spiltChar2,StringSplitOptions.None);
                    
                    if(getPos[0]>=0)
                    {
                        singleSentence.layer=dialog[getPos[0]];
                    }
                    if(getPos[1]>=0)
                    {
                        singleSentence.startTime=TimeInAss(dialog[getPos[1]]);
                    }
                    if(getPos[2]>=0)
                    {
                        singleSentence.endTime=TimeInAss(dialog[getPos[2]]);
                    }
                    if(getPos[3]>=0)
                    {
                        singleSentence.style=dialog[getPos[3]];
                    }
                     if(getPos[4]>=0)
                    {
                        singleSentence.actor=dialog[getPos[4]];
                    }
                     if(getPos[5]>=0)
                    {
                        singleSentence.marginL=dialog[getPos[5]];
                    }
                     if(getPos[6]>=0)
                    {
                        singleSentence.marginR=dialog[getPos[6]];
                    }
                     if(getPos[7]>=0)
                    {
                        singleSentence.marginV=dialog[getPos[7]];
                    }
                     if(getPos[8]>=0)
                    {
                        singleSentence.effect=dialog[getPos[8]];
                    }
                     if(getPos[9]>=0)
                    {
                        singleSentence.content=dialog[getPos[9]];
                         //去掉中间的特效吧。
                         singleSentence.content=Regex.Replace(singleSentence.content, @"\{.*\}", "");
                         singleSentence.content=singleSentence.content.Replace("\\N","\r\n");
                    }
                     //2013-12-22修改了末尾增加一行的bug
                     singleSentence.content = CCHandle.TrimEnterEnd(singleSentence.content);

                    this.listSingleSentence.Add(singleSentence);
                }
                catch
                {
                    runMark=false;
                    break;
                }
            }
            return true;
        }
        public static string TimeOutAss(double time)
        {
            time = ((double)((int)(time * 1000))) / 1000;
            int hour = (int)time / 3600;
            int minute = (int)(time - 3600 * hour) / 60;
            int second = (int)(time - 3600 * hour - 60 * minute);
            //int minsec = (int)((time - (int)time) * 1000);
            int minsec = (int)(time * 1000 - ((int)time) * 1000);//2013-12-22将原有的转换函数转变否则会出现700转为699的问题
            minsec = minsec / 10;//2014-1-1修改两位时间问题
            string TimeStr = hour + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0') + "." + minsec.ToString().PadLeft(2, '0');
            return TimeStr;
        }
        public static double TimeInAss(string time)
        {
            string[] timeBuffer;
            string[] spiltChar = { ".", "。",",", ":" };
            timeBuffer = time.Split(spiltChar, StringSplitOptions.RemoveEmptyEntries);
            if (timeBuffer.Length < 4)
            {
                return -1;
            }
            else
            {
                double timedouble = Double.Parse(timeBuffer[0]) * 3600 + Double.Parse(timeBuffer[1]) * 60 + Double.Parse(timeBuffer[2]) + Double.Parse(timeBuffer[3]) * 0.01;
                return timedouble;
            }
        }
        public void WriteAllTimelineAss()
        {
            fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            streamWriter = new StreamWriter(fileStream, this.encoding);
            this.streamWriter.WriteLine("[Script Info]");
            this.streamWriter.WriteLine(this.scriptInfo);
            this.streamWriter.WriteLine("[V4+ Styles]");
            this.streamWriter.WriteLine(this.styles);
            this.streamWriter.WriteLine("[Events]");
            this.streamWriter.WriteLine("Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text");
            string line="";
            for (int i = 1; i < this.listSingleSentence.Count; i++)
            {
                line = "Dialogue: ";
                line += this.listSingleSentence[i].layer + ",";
                line += TimeOutAss(this.listSingleSentence[i].startTime) + ",";
                line += TimeOutAss(this.listSingleSentence[i].endTime) + ",";
                line += this.listSingleSentence[i].style + ",";
                line += this.listSingleSentence[i].actor + ",";
                line += this.listSingleSentence[i].marginL + ",";
                line += this.listSingleSentence[i].marginR + ",";
                line += this.listSingleSentence[i].marginV + ",";
                line += this.listSingleSentence[i].effect + ",";
                line += this.listSingleSentence[i].content;
                line = line.Replace("\r\n", "\\N");
                streamWriter.WriteLine(line);
            }
            streamWriter.Close();
            fileStream.Close();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="fileReadFunction"></param>
        public void Read(FileReadFunction fileReadFunction)
        {
            fileReadFunction.Read(this.listSingleSentence, this.fileStream, this.filePath, ref this.encoding,ref this.scriptInfo, ref this.styles);
        }
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="fileWriteFunction"></param>
        public void Write(FileWriteFunction fileWriteFunction)
        {
            fileWriteFunction.Write(this.listSingleSentence, this.fileStream, this.filePath, this.encoding, ref this.scriptInfo, ref styles);
        }

        public bool DeleteBuf(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                return true;
            }
            else
            {
                return false;
            }
        }


        #region 单例模式特效
        private static AssInfo assInfo;
        public static AssInfo GetAssInfo()
        {
            if (assInfo == null)
            {
                assInfo = new AssInfo();
                return assInfo;
            }
            else
            {
                return assInfo;
            }
        }
        #endregion
    }
}
