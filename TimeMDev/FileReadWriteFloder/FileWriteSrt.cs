using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteSrt:FileWriteFunction
    {
        Encoding encoding;
        public event ContentFunctionD ContentFunction;
        string path;
        public FileWriteSrt()
        {
 
        }
        public FileWriteSrt(Encoding encoding)
        {
            this.encoding = encoding;
        }
        public FileWriteSrt(Encoding encoding,string path)
        {
            this.encoding = encoding;
            this.path = path;
        }
        
        #region FileWriteFunction 成员

        public void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
            ///后增加的，用于直接写入
            if (path != null)
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
            this.WriteSrt(listSingleSentence, streamWriter, ref scriptInfo, ref styles);
            streamWriter.Close();
            fileStream.Close();
        }

        #endregion
        /// <summary>
        /// 写srt
        /// </summary>
        /// <param name="listSingleSentence"></param>
        /// <param name="streamWriter"></param>
        /// <param name="scriptInfo"></param>
        /// <param name="styles"></param>
        private void WriteSrt(List<SingleSentence> listSingleSentence, System.IO.StreamWriter streamWriter, ref string scriptInfo, ref string styles)
        {
            int hour, minute, second, minsec;
            for (int i = 1; i < listSingleSentence.Count - 1; i++)//崔进修改于2013-4-9  只写中间的部分
            {
                //写号
                //this.streamWriter.WriteLine(i + 1);
                streamWriter.WriteLine(i);//修改于2013-4-10
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
                streamWriter.WriteLine(startTimeStr + " --> " + endTimeStr);
                //写内容
                if (this.ContentFunction == null)
                {
                    string buf = listSingleSentence[i].content;
                    streamWriter.WriteLine(buf);
                }
                else
                {
                    string buf = listSingleSentence[i].content;
                    streamWriter.WriteLine(this.ContentFunction(ref buf));
                }
                streamWriter.WriteLine("");
            }
        }
    }
}
