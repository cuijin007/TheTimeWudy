using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteAss:FileWriteFunction
    {
        public event ContentFunctionD ContentFunction; 
        #region FileWriteFunction 成员
        public  void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream, encoding);
            this.WriteAss(listSingleSentence, streamWriter, ref scriptInfo, ref styles);
            streamWriter.Close();
            fileStream.Close();
        }
        #endregion

        private void WriteAss(List<SingleSentence> listSingleSentence, System.IO.StreamWriter streamWriter, ref string scriptInfo, ref string styles)
        {
            streamWriter.WriteLine("[Script Info]");
            streamWriter.WriteLine(scriptInfo);
            streamWriter.WriteLine("[V4+ Styles]");
            streamWriter.WriteLine(styles);
            streamWriter.WriteLine("[Events]");
            streamWriter.WriteLine("Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text");
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
                    line += this.ContentFunction(listSingleSentence[i].content);
                }
                line = line.Replace("\r\n", "\\N");
                streamWriter.WriteLine(line);
            }
        }
    }
}
