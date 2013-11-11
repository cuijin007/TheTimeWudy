using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace TimeMDev
{
    class EnglishNameChange
    {
        List<SingleSentence> sentences;
        List<NameTable> names;
        string path;
        public EnglishNameChange(List<SingleSentence> sentences,string filePath)
        {
            this.sentences = sentences;
            this.names = new List<NameTable>();
            this.path = filePath;
        }
        /// <summary>
        /// 将名字写入文件中
        /// </summary>
        private void WriteAllNameToFile()
        {
            FileStream fileStream = new FileStream(this.path, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            for (int i = 0; i < this.names.Count; i++)
            {
                streamWriter.WriteLine(names[i].englishName);
                streamWriter.WriteLine(names[i].chineseName);
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            fileStream.Close();

        }
        /// <summary>
        /// 读取名字
        /// </summary>
        private void ReadAllNameFromFile()
        {
            this.names.Clear();
            FileStream fileStream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader streamReader = new StreamReader(fileStream,System.Text.Encoding.Default);
            bool isReading=true;
            while (isReading)
            {
                try
                {
                    NameTable nameTable = new NameTable();
                    nameTable.englishName = streamReader.ReadLine();
                    nameTable.chineseName = streamReader.ReadLine();
                    streamReader.ReadLine();//读个空行吧
                    if (nameTable.englishName != null && nameTable.chineseName != null)
                    {
                        if (!this.names.Contains(nameTable))
                        {
                            this.names.Add(nameTable);
                        }
                    }
                    else
                    {
                        break;
                    }
                   
                }
                catch
                {
                    isReading = false;
                }
            }
            streamReader.Close();
            fileStream.Close();
            return;
        }
        /// <summary>
        /// 获得所有的英文名字列表
        /// </summary>
        private void GetAllNameFromList()
        {
            this.names.Clear();
            for (int i = 0; i < this.sentences.Count; i++)
            { 
                GetEnglishNameFromChinese(this.sentences[i].content);
            }
        }
        /// <summary>
        /// 从中文中获取英文名,如果有英文名，将已有的姓名列表清空，之后将名字全部加入。
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private bool GetEnglishNameFromChinese(string sentence)
        {
            string allChinese;//带英文的所有中文字段
            MatchCollection matchCollection = Regex.Matches("崔进抗八代"+sentence, "([\u4E00-\u9FA5]|[\uFE30-\uFFA0]).*([\u4E00-\u9FA5]|[\uFE30-\uFFA0])");
            if (matchCollection == null || matchCollection.Count == 0)
            {
                return false;
            }
            else
            {
                allChinese = matchCollection[0].ToString();
                allChinese = allChinese.Replace("崔进抗八代","");
            }
            MatchCollection EnglishCollection = Regex.Matches(allChinese, @"[A-Za-z][A-Za-z\s]*[A-Za-z]");
            if (EnglishCollection == null && EnglishCollection.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < EnglishCollection.Count; i++)
                {
                    string name = EnglishCollection[i].ToString();
                    NameTable nameTable=new NameTable();
                    nameTable.chineseName=name;
                    nameTable.englishName=name;
                    if(this.names.Contains(nameTable))
                    {
                       
                    }
                    else
                    {
                        this.names.Add(nameTable);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 将这些名字都换了
        /// </summary>
        private void ReplaceEnglishName()
        {
            this.NameListSort();
            for(int i=0;i<names.Count;i++)
            {
                for (int j = 0; j < sentences.Count; j++)
                {
                    this.sentences[j].content= this.sentences[j].content.Replace(this.names[i].englishName, this.names[i].chineseName);
                }
            }
        }
        /// <summary>
        /// 对名字进行排序
        /// </summary>
        private void NameListSort()
        {
            this.names.Sort(new NameTableCompare()); 
        }
        /// <summary>
        /// 生成名单，并将名单写入文件
        /// </summary>
        public void CreateAndWriteNameList()
        {
            this.GetAllNameFromList();
            this.WriteAllNameToFile();
            this.CallTxtEditor(this.path);
        }
        /// <summary>
        /// 读取文件，并将sentence中的名字全部切换
        /// </summary>
        public void ReadAndReplaceAllName()
        {
            this.ReadAllNameFromFile();
            this.ReplaceEnglishName();
        }
        /// <summary>
        /// 打开记事本
        /// </summary>
        public void CallTxtEditor(string path)
        {
            //Process.Start(path + "notepad.exe");
            System.Diagnostics.Process.Start("notepad.exe",path);
        }
        /// <summary>
        /// 获取处理之后的姓名列表列表
        /// </summary>
        public List<NameTable> GetNames()
        {
            return this.names;
        }
        /// <summary>
        /// 读取，并获得所有的姓名
        /// </summary>
        /// <returns></returns>
        public List<NameTable> ReadAndGetNames()
        {
            this.ReadAllNameFromFile();
            return this.names;
        }
    }
    public class NameTableCompare : IComparer<NameTable>
    {
        #region IComparer<NameTable> 成员

        public int Compare(NameTable x, NameTable y)
        {
            if (x.englishName.Length > y.englishName.Length)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
    public class NameTable:IEquatable<NameTable>
    {
        public string chineseName;
        public string englishName;

        #region IEquatable<NameTable> 成员

        public bool Equals(NameTable other)
        {
            if (this.chineseName.Equals(other.chineseName) && this.englishName.Equals(other.englishName))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
