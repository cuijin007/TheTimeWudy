using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.FileReadWriteFloder
{
    public class FileWriteSegmentation:FileWriteFunction
    {
        string filepath;
        int segment;
        int isAss;
        public FileWriteSegmentation(string path,int segment,int isAss)
        {
            this.filepath = path;
            this.segment = segment;
            this.isAss = isAss;
        }

        private void CopyList(ref List<SingleSentence> listDest,List<SingleSentence> source,int startPos,int count)
        {
            listDest = new List<SingleSentence>();
            listDest.Add(new SingleSentence());
            for (int i = 0; i < count; i++)
            {
                listDest.Add(source[startPos + i]);

            }
        }

        #region FileWriteFunction 成员

        public void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles)
        {
            int everySegCount = (listSingleSentence.Count-2) / this.segment;
            for (int i = 0; i < this.segment; i++)
            {
                List<SingleSentence> listSingleSentenceBuf = new List<SingleSentence>();
                this.CopyList(ref listSingleSentenceBuf, listSingleSentence, 1 + i * everySegCount, everySegCount);
                FileWriteFunction fileWriteFunction;
                if (this.isAss == 1)
                {
                    fileWriteFunction = new FileWriteAss();
                    fileWriteFunction.Write(listSingleSentenceBuf, fileStream, filepath.Replace(".ass", (i + 1) + ".ass"), encoding, ref scriptInfo, ref styles);
                }
                else
                {
                    fileWriteFunction = new FileWriteSrt();
                    fileWriteFunction.Write(listSingleSentenceBuf, fileStream, filepath.Replace(".srt", (i + 1) + ".srt"), encoding, ref scriptInfo, ref styles);
                }
                //fileWriteFunction.Write(listSingleSentence, fileStream, filepath.Replace(".ass",i+1+".ass"), encoding, ref scriptInfo, ref styles);
            }
        }

        #endregion
    }
}
