using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeMDev.FileReadWriteFloder
{
    public interface FileReadFunction
    {
        void Read(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, ref Encoding encoding, ref string scriptInfo, ref string styles);
    }
    public interface FileWriteFunction
    {
        void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, Encoding encoding, ref string scriptInfo, ref string styles);
    }
    public interface FileWriteFunctionMutiLanguage
    {
        void Write(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath,Encoding encoding,string scriptInfo, string styles);
    }
    class AllFileInterface
    {
    }
    public delegate string ContentFunctionD(string input);
}
