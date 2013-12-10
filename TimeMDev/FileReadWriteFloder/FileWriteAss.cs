﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteAss:FileWriteFunction
    {
        Encoding encoding;
        AssInfo assInfo=new AssInfo();
        public event ContentFunctionD ContentFunction; 
        #region FileWriteFunction 成员
        public  void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
        public FileWriteAss(Encoding encoding, AssInfo assInfo)
        {
            this.encoding = encoding;
            this.assInfo = assInfo;
        }
        private void WriteAss(List<SingleSentence> listSingleSentence, System.IO.StreamWriter streamWriter, AssInfo assInfo)
        {
            streamWriter.WriteLine("[Script Info]");
            streamWriter.WriteLine(assInfo.ScriptInfo);
            streamWriter.WriteLine("[V4+ Styles]");
            streamWriter.WriteLine(assInfo.v4Style);
            streamWriter.WriteLine("[Events]");
            streamWriter.WriteLine(assInfo.eventContent);
            string line = "";
            for (int i = 1; i < listSingleSentence.Count; i++)
            {
                line = "Dialogue: ";
                line += listSingleSentence[i].layer + ",";
                line += TimeLineReadWrite.TimeOutAss(listSingleSentence[i].startTime) + ",";
                line += TimeLineReadWrite.TimeOutAss(listSingleSentence[i].endTime) + ",";
                line += listSingleSentence[i].style + ",";
                line += listSingleSentence[i].actor + ",";
                line += listSingleSentence[i].marginL + ",";
                line += listSingleSentence[i].marginR + ",";
                line += listSingleSentence[i].marginV + ",";
                line += listSingleSentence[i].effect + ",";
                if (this.ContentFunction == null)
                {
                    line += listSingleSentence[i].content;
                }
                else
                {
                    string buf = listSingleSentence[i].content;
                    line += this.ContentFunction(ref buf);
                }
                line = line.Replace("\r\n", "\\N");
                streamWriter.WriteLine(line);
            }
        }
    }
}
