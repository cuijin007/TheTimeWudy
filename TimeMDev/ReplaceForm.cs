using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TimeMDev.HandleRecord;

namespace TimeMDev
{
    public partial class ReplaceForm : Form
    {
        Form1 form;
        CommandManage commandManager;
        YYListView yyListView;
        List<SingleSentence> listSentences;
        EnglishNameChange englishNameChange;
        List<NameTable> listNameTable;
        public ReplaceForm(CommandManage commandManager,Form1 form)
        {
            InitializeComponent();
            this.form = form;
            this.commandManager = commandManager;
            this.yyListView = form.listView1;
            this.listSentences = this.form.DataProcessGet.listSingleSentence;
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            List<ReplaceParameter> listReplaceParameter=new List<ReplaceParameter>();
            if (!this.replaceAllCheck.Checked)
            {
                ReplaceParameter replaceParameter = new ReplaceParameter();
                replaceParameter.chineseChoosed = this.chineseCheck.Checked;
                replaceParameter.englishChoosed = this.englishCheck.Checked;
                replaceParameter.caseSensitive = this.caseSensitiveCheck.Checked;
                replaceParameter.orginalWord = this.searchBox.Text;
                replaceParameter.replaceWord = this.replaceBox.Text;
                listReplaceParameter.Add(replaceParameter);
            }
            else
            {
                for (int i = 0; i < this.listViewShow.Items.Count ;i++)
                {
                    if (this.listViewShow.Items[i].Checked)
                    {
                        ReplaceParameter replaceParameter = new ReplaceParameter();
                        replaceParameter.chineseChoosed = this.chineseCheck.Checked;
                        replaceParameter.englishChoosed = this.englishCheck.Checked;
                        replaceParameter.caseSensitive = this.caseSensitiveCheck.Checked;
                        replaceParameter.orginalWord = this.listViewShow.Items[i].SubItems[1].Text;
                        replaceParameter.replaceWord = this.listViewShow.Items[i].SubItems[2].Text;
                        listReplaceParameter.Add(replaceParameter);
                    }
                }
            }

            ReplaceMutiRecordContent command;
            if (this.allRadio.Checked)
            {
                command = new ReplaceMutiRecordContent(this.form.DataProcessGet.listSingleSentence, this.form.listView1, listReplaceParameter);
            }
            else
            {
                command = new ReplaceMutiRecordContent(this.form.DataProcessGet.listSingleSentence, this.form.listView1, listReplaceParameter,this.form.listView1.SelectedIndices);
            }
            this.commandManager.CommandRun(command);
            this.yyListView.YYRefresh();
        }

        private void addTemplate_Click(object sender, EventArgs e)
        {
            string[] name = new string[3];
            name[1] = this.searchBox.Text;
            name[2] = this.replaceBox.Text;
            ListViewItem item = new ListViewItem(name);
            item.Checked = true;
            this.listViewShow.Items.Add(item);
        }

        private void loadTemplate_Click(object sender, EventArgs e)
        {
             OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "替换文件|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.englishNameChange = new EnglishNameChange(this.form.DataProcessGet.listSingleSentence, dialog.FileName);
               listNameTable = englishNameChange.ReadAndGetNames();
                for (int i = 0; i < listNameTable.Count; i++)
                {
                    string[] name=new string[3];
                    name[1] = listNameTable[i].englishName;
                    name[2] = listNameTable[i].chineseName;
                    ListViewItem item = new ListViewItem(name);
                    item.Checked = true;
                    this.listViewShow.Items.Add(item);
                }
            }
        }

        private void saveTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.englishNameChange = new EnglishNameChange(this.form.DataProcessGet.listSingleSentence, saveFileDialog.FileName);
                for (int i = 0; i < this.listViewShow.Items.Count; i++)
                {
                    NameTable nameTable = new NameTable();
                    nameTable.englishName = this.listViewShow.Items[i].SubItems[1].Text;
                    nameTable.chineseName = this.listViewShow.Items[i].SubItems[2].Text;
                    this.englishNameChange.GetNames().Add(nameTable);
                }
                this.englishNameChange.WriteAndShowNameList();
            }
        }

        private void closeTemplate_Click(object sender, EventArgs e)
        {
            this.listViewShow.Items.Clear();
        }

        private void deleteTemplate_Click(object sender, EventArgs e)
        {
            for(int i=this.listViewShow.SelectedIndices.Count-1;i>=0;i--)
            {
                this.listViewShow.Items.RemoveAt(this.listViewShow.SelectedIndices[i]);
            }
        }

        private void replaceAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.replaceAllCheck.Checked == true)
            {
                this.Height = 433;
            }
            else
            {
                this.Height = 170;
            }
        }
    }
}
