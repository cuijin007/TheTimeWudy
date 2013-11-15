using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev.HandleRecord
{
    public class PasteRecord:HandleRecordBass
    {
        /// <summary>
        /// 粘进来的条目的序号
        /// </summary>
        int startPosition;
        int count;
        DataProcess dataProcess;
        YYListView yyListView;
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="dataProcess"></param>
        /// <param name="yylistView"></param>
        /// <param name="showPosition">粘贴的位置的序号</param>
        public PasteRecord(DataProcess dataProcess,YYListView yyListView,int showPosition)
        {
            this.dataProcess = dataProcess;
            this.yyListView = yyListView;
            this.startPosition = showPosition;
        }
        public override void Execute()
        {
            try
            {
                DataFormats.Format format = new DataFormats.Format("cuijin", 891008);
                List<SingleSentence> sentences = (List<SingleSentence>)Clipboard.GetData(format.Name);
                this.count = sentences.Count;
                for (int i = 0; i < this.count; i++)
                {
                    //this.dataProcess.listSingleSentence.Insert(yyListView.YYGetRealPosition(this.startPosition), sentences);
                    this.yyListView.YYInsertLine(this.startPosition + i, sentences[i]);
                }
                dataProcess._Paste(yyListView.YYGetRealPosition(this.startPosition));
            }
            catch
            { }
        }

        public override void UnExecute()
        {
            for (int i = 0; i < this.count; i++)
            {
                this.dataProcess.listSingleSentence.RemoveAt(startPosition);
                this.yyListView.YYDeleteLine(startPosition);
            }
        }
    }
}
