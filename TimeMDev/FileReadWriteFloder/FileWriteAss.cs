using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteAss:FileWriteFunction
    {
        Encoding encoding;
        AssInfo assInfo;
        public event ContentFunctionD ContentFunction;
        string path;
        #region FileWriteFunction 成员
        public  void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
            if (this.path != null)
            {
                filePath = path;
            }
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter streamWriter;
            if (this.encoding == null)
            {
                streamWriter = new StreamWriter(fileStream, encoding);
            }
            else
            {
                streamWriter = new StreamWriter(fileStream, this.encoding);
            }
            this.WriteAss(listSingleSentence, streamWriter, this.assInfo);
            streamWriter.Close();
            fileStream.Close();
        }
        #endregion

        public FileWriteAss()
        {
 
        }
        public FileWriteAss(Encoding encoding)
        {
            this.encoding = encoding;
        }
        public FileWriteAss(Encoding encoding, string path,AssInfo assInfo)
        {
            this.encoding = encoding;
            this.path = path;
            this.assInfo = assInfo;
        }
        public FileWriteAss(Encoding encoding, string path)
        {
            this.encoding = encoding;
            this.path = path;
        }
        public FileWriteAss(Encoding encoding, AssInfo assInfo)
        {
            this.encoding = encoding;
            this.assInfo = assInfo;
        }
        private void WriteAss(List<SingleSentence> listSingleSentence, System.IO.StreamWriter streamWriter, AssInfo assInfo)
        {
            if (assInfo != null)
            {
                streamWriter.WriteLine("[Script Info]");
                streamWriter.WriteLine(assInfo.ScriptInfo);
                streamWriter.WriteLine("[V4+ Styles]");
                streamWriter.WriteLine(assInfo.v4Style);
                streamWriter.WriteLine("[Events]");
                streamWriter.WriteLine(assInfo.eventContent);
            }
            else
            {
                streamWriter.WriteLine("[Script Info]");
                streamWriter.WriteLine(TimeLineReadWrite.GetAssInfo().ScriptInfo);
                streamWriter.WriteLine("[V4+ Styles]");
                streamWriter.WriteLine(TimeLineReadWrite.GetAssInfo().v4Style);
                streamWriter.WriteLine("[Events]");
                streamWriter.WriteLine(TimeLineReadWrite.GetAssInfo().eventContent);
            }
            string line = "";
            for (int i = 1; i < listSingleSentence.Count; i++)
            {
                line = "Dialogue: ";
                line += (listSingleSentence[i].layer + ",").Trim();
                line += TimeLineReadWrite.TimeOutAss(listSingleSentence[i].startTime) + ",";
                line += TimeLineReadWrite.TimeOutAss(listSingleSentence[i].endTime) + ",";
                line += listSingleSentence[i].style + ",";
                line += listSingleSentence[i].actor + ",";
                line += listSingleSentence[i].marginL + ",";
                line += listSingleSentence[i].marginR + ",";
                line += listSingleSentence[i].marginV + ",";
                //line += listSingleSentence[i].effect + ",";
                line += ",";
                //if (this.ContentFunction == null)
                {
                    if (this.assInfo != null)
                    {
                        string chinese, english;
                        string lineBuf = "";
                        string content = listSingleSentence[i].content;
                        content = CCHandle.CutSrtScript(content);
                        CCHandle.SpiltRule(content, out chinese, out english);
                        try
                        {
                            chinese = CCHandle.TrimEnterEnd(chinese);
                            chinese = CCHandle.TrimEnterStart(chinese);
                            english = CCHandle.TrimEnterEnd(english);
                            english = CCHandle.TrimEnterStart(english);
                        }
                        catch
                        {

                        }
                        if (content.StartsWith(chinese))
                        {
                            //if (listSingleSentence[i].effect != null && !listSingleSentence[i].effect.Equals(""))
                            //if(listSingleSentence[i].content.Contains("{"))
                            {
                                if (!english.Equals(""))
                                {
                                    lineBuf += chinese + "\r\n" + assInfo.EnglishHead + english;
                                }
                                else
                                {
                                    lineBuf += chinese;
                                }
                                lineBuf = CCHandle.TrimEnterStart(lineBuf);
                                lineBuf = CCHandle.TrimEnterEnd(lineBuf);
                            }
                        }
                        else if (content.StartsWith(english))
                        {
                            //if (listSingleSentence[i].effect != null && !listSingleSentence[i].effect.Equals(""))
                            //if (listSingleSentence[i].content.Contains("{"))
                            {
                                if (!english.Equals(""))
                                {
                                    lineBuf += assInfo.EnglishHead + english + "\r\n" + chinese;
                                }
                                else
                                {
                                    lineBuf += chinese;
                                }
                            }
                            lineBuf = CCHandle.TrimEnterStart(lineBuf);
                            lineBuf = CCHandle.TrimEnterEnd(lineBuf);
                        }
                        if (this.ContentFunction == null)
                        {
                            line +=CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(lineBuf));
                        }
                        else
                        {
                            line += CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(this.ContentFunction(ref lineBuf)));
                        }
                        // line += listSingleSentence[i].content;
                    }
                    else
                    {
                        if (this.ContentFunction == null)
                        {
                            line += CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(listSingleSentence[i].content));
                        }
                        else
                        {
                            string contentBuf = listSingleSentence[i].content;
                            line += CCHandle.TrimEnterEnd(CCHandle.TrimEnterStart(this.ContentFunction(ref contentBuf)));
                        }
                    }
                }
                if(this.ContentFunction!=null)
                {
                    //string str = listSingleSentence[i].content;
                    //line += this.ContentFunction(ref str);
                   // line = this.ContentFunction(ref listSingleSentence[i].content);
                }
                    line = line.Replace("\r\n", "\\N");
                    line = CCHandle.TrimEnterStart(line);
                    line = CCHandle.TrimEnterEnd(line);
                    streamWriter.WriteLine(line);
                }
            }
        
    }
}



/*
 if (listSingleSentence[i].content.StartsWith(chinese))
 {
     //if (listSingleSentence[i].effect != null && !listSingleSentence[i].effect.Equals(""))
     if(listSingleSentence[i].content.Contains("{"))
     {
         if (!english.Equals(""))
         {
             line += chinese + "\r\n" + listSingleSentence[i].effect + english;
         }
         else
         {
             line += chinese;
         }
     }
     else
     {
         if (!english.Equals(""))
         {
             line += chinese + "\r\n" + TimeLineReadWrite.GetAssInfo().EnglishHead + english;
         }
         else
         {
             line += chinese;
         }
     }
 }
 else
 {
     //if (listSingleSentence[i].effect != null && !listSingleSentence[i].effect.Equals(""))
     if (listSingleSentence[i].content.Contains("{"))
     {
         if (!english.Equals(""))
         {
             line += listSingleSentence[i].effect + english + "\r\n" + chinese;
         }
         else
         {
             line += chinese;
         }
     }
     else
     {
         if (!english.Equals(""))
         {
             line += TimeLineReadWrite.GetAssInfo().EnglishHead + english + "\r\n" + chinese;
         }
         else
         {
             line += chinese;
         }
     }
 }
// line += listSingleSentence[i].content;
}
else
{
 string buf = listSingleSentence[i].content;
 line += this.ContentFunction(ref buf);
}
  * */