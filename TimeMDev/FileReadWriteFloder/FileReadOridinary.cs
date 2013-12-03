using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.FileReadWriteFloder
{
    class FileReadOridinary:FileReadFunction
    {
        private bool BlankInterval;
        public FileReadOridinary(bool BlankInterval)
        {
            this.BlankInterval = BlankInterval;
        }
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

        private void ReadByBlank(List<SingleSentence> listSingleSentence, System.IO.StreamReader streamReader)
        {
            listSingleSentence.Clear();
            string str = "";
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

        #region FileReadFunction 成员

        public void Read(List<SingleSentence> listSingleSentence, System.IO.StreamReader streamReader, ref string scriptInfo,ref string styles)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
