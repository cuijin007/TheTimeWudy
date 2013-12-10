using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace TimeMDev.FileReadWriteFloder
{
    /// <summary>
    /// 读取ass
    /// </summary>
    class FileReadAss:FileReadFunction
    {

        string scriptInfo = "[Script Info]\r\n; // 此字幕由TimeM生成\r\n; // 欢迎访问人人影视 http://www.YYeTs.net" +
                                        "\r\nTitle:YYeTs" +
                                        "\r\nOriginal Script:YYeTs" +
                                        "\r\nSynch Point:0" +
                                        "\r\nScriptType:v4.00+" +
                                        "\r\nCollisions:Normal" +
                                        "\r\nTimer:100.0000";
        string styles = "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding" +
                                "\r\nStyle: Default,方正黑体简体,21,&H00FFFFFF,&HF0000000,&H006C3300,&H00000000,-1,0,0,0,100,100,0,0.00,1,2,1,2,5,5,5,134";

        private bool Additional=false;
        public FileReadAss(bool Additional, List<SingleSentence> listSingleSentence)
        {
            this.Additional = Additional;
            this.listSingleSentence = listSingleSentence;
        }
        public FileReadAss()
        {
        }
        private List<SingleSentence> listSingleSentence;
        #region FileReadFunction 成员
       public void Read(List<SingleSentence> listSingleSentence, System.IO.FileStream fileStream, string filePath, ref Encoding encoding, ref string scriptInfo, ref string styles)
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            encoding = TimeLineReadWrite.GetEncoding(fileStream);
            StreamReader streamReader = new StreamReader(fileStream, encoding);
           //如果不追加，则采用之前的listsinglesentence
           if (!this.Additional)
            {
                this.listSingleSentence = listSingleSentence;
            }
            if (this.ReadHeadInfo(streamReader))
            {
                this.ReadAllEvent(streamReader);
            }
            scriptInfo = this.scriptInfo;
            styles = this.styles;

            streamReader.Close();
            fileStream.Close();
        }
        #endregion
        /// <summary>
        /// 读文件头
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private bool ReadHeadInfo(StreamReader streamReader)
        {
            bool runMark = true;
            int state = -1;
            try
            {
                string str = "";
                while (runMark)
                {
                    str = streamReader.ReadLine();
                    if (str.Equals(""))
                    {
                        continue;
                    }
                    if (str.Equals("[Events]"))
                    {
                        runMark = false;
                        break;
                    }
                    if (str.Equals("[Script Info]"))
                    {
                        state = 1;//开始记录ScriptInfo
                        this.scriptInfo = "";
                        continue;
                    }
                    if (str.Equals("[V4+ Styles]"))
                    {
                        state = 2;
                        this.styles = "";
                        continue;
                    }
                    if (state == 1)
                    {
                        this.scriptInfo += str + "\r\n";
                    }
                    if (state == 2)
                    {
                        this.styles += str + "\r\n";
                    }
                }
                return true;
            }
            catch
            {
                runMark = false;
                return false;
            }
        }

        /// <summary>
        /// 读ass内容
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private bool ReadAllEvent(StreamReader streamReader)
        {
            string[] dictionaryType;
            string[] spiltChar = { ":", "," };
            string[] spiltChar2 = { "," };
            string[] markStr = { "Layer", "Start", "End", "Style", "Actor", "MarginL", "MarginR", "MarginV", "Effect", "Text" };
            string str = streamReader.ReadLine();
            bool runMark = true;
            int[] getPos = new int[markStr.Length];
            for (int i = 0; i < getPos.Length; i++)
            {
                getPos[i] = -1;
            }
            ///追加
            if (this.Additional)
            {
                this.listSingleSentence.Clear();
            }
            SingleSentence singleSentenceS = new SingleSentence();
            singleSentenceS.content = "";
            singleSentenceS.startTime = 0;
            singleSentenceS.endTime = 0;
            this.listSingleSentence.Add(singleSentenceS);
            ///初始化
            ///开始进行格式拆分
            if (str.StartsWith("Format:"))
            {
                str = str.Replace("Format", "");
                str = str.Replace(" ", "");
                dictionaryType = str.Split(spiltChar, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < markStr.Length; i++)
                {
                    for (int j = 0; j < dictionaryType.Length; j++)
                    {
                        if (markStr[i].Equals(dictionaryType[j]))
                        {
                            getPos[i] = j;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            while (runMark)
            {
                try
                {
                    str = streamReader.ReadLine();
                    if (str.Equals(""))
                    {
                        continue;
                    }
                    SingleSentence singleSentence = new SingleSentence();
                    str = str.Replace("Dialogue:", "");
                    singleSentence.matchCollectionEffect = Regex.Matches(str, @"\{.*\}");
                    str = Regex.Replace(str, @"\{.*\}", "");
                    string[] dialog = str.Split(spiltChar2, StringSplitOptions.None);

                    if (getPos[0] >= 0)
                    {
                        singleSentence.layer = dialog[getPos[0]];
                    }
                    if (getPos[1] >= 0)
                    {
                        singleSentence.startTime = TimeLineReadWrite.TimeInAss(dialog[getPos[1]]);
                    }
                    if (getPos[2] >= 0)
                    {
                        singleSentence.endTime = TimeLineReadWrite.TimeInAss(dialog[getPos[2]]);
                    }
                    if (getPos[3] >= 0)
                    {
                        singleSentence.style = dialog[getPos[3]];
                    }
                    if (getPos[4] >= 0)
                    {
                        singleSentence.actor = dialog[getPos[4]];
                    }
                    if (getPos[5] >= 0)
                    {
                        singleSentence.marginL = dialog[getPos[5]];
                    }
                    if (getPos[6] >= 0)
                    {
                        singleSentence.marginR = dialog[getPos[6]];
                    }
                    if (getPos[7] >= 0)
                    {
                        singleSentence.marginV = dialog[getPos[7]];
                    }
                    if (getPos[8] >= 0)
                    {
                        singleSentence.effect = dialog[getPos[8]];
                    }
                    if (getPos[9] >= 0)
                    {
                        singleSentence.content = dialog[getPos[9]];
                        //去掉中间的特效吧。
                        singleSentence.content = Regex.Replace(singleSentence.content, @"\{.*\}", "");
                        singleSentence.content = singleSentence.content.Replace("\\N", "\r\n");
                    }
                    this.listSingleSentence.Add(singleSentence);
                }
                catch
                {
                    runMark = false;
                    break;
                }
            }
            return true;
        }
    }
}
