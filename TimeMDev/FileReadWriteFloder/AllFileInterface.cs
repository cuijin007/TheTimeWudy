using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public interface FileReadFunction
    {
        void Read(List<SingleSentence> listSingleSentence, StreamReader streamReader);
    }
    class AllFileInterface
    {
    }
}
