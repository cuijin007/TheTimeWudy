using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeMDev.FileReadWriteFloder;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 追加字幕
    /// </summary>
    public class AdditionalSubtitle:HandleRecordBass
    {
        string additionalSubtitlePath;
        string formalPath;
        TimeLineReadWrite timeLineReadWrite;
        int startAdd = 0;
        int count = 0;
        YYListView yyListView;
        List<HandleRecordBass> listFunction = new List<HandleRecordBass>();
        public AdditionalSubtitle(TimeLineReadWrite timeLineReadWrite,YYListView yyListView,string path)
        {
            this.additionalSubtitlePath = path;
            this.timeLineReadWrite = timeLineReadWrite;
            this.formalPath = timeLineReadWrite.filePath;
            this.yyListView = yyListView;
        }
        /// <summary>
        /// 执行增加操作
        /// </summary>
        public override void Execute()
        {
            List<SingleSentence> listSingleSentenceBuf = new List<SingleSentence>();
            FileReadFunction function=null;
            if (additionalSubtitlePath.EndsWith(".srt"))
            {
                function = new FileReadSrt(true, listSingleSentenceBuf);
            }
            if (additionalSubtitlePath.EndsWith(".ass"))
            {
                function = new FileReadAss(true, listSingleSentenceBuf);
            }
            if (function != null)
            {
                this.timeLineReadWrite.Read(function);
            }
            for (int i = 0; i < listSingleSentenceBuf.Count; i++)
            {
                AddRecord addRecord = new AddRecord(this.timeLineReadWrite.listSingleSentence, this.yyListView, listSingleSentenceBuf[i]);
                this.listFunction.Add(addRecord);
                addRecord.Execute();
            }
        }
        /// <summary>
        /// 去掉追加的那些行
        /// </summary>
        public override void UnExecute()
        {
            for (int i = this.listFunction.Count-1; i >= 0; i--)
            {
                this.listFunction[i].UnExecute();
            }
        }
    }
}
