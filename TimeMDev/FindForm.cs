using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TimeMDev
{
    public partial class FindForm : Form
    {
        int mark=0;
        int markPosition = -1;//上次找到的位置
        Form1 form;
        public FindForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void findNextButton_Click(object sender, EventArgs e)
        {
            if(!this.searchBox.Text.Equals(""))
            {
                int startPosition;
                if(this.form.listView1.SelectedIndices.Count>0)
                {
                    startPosition=this.form.listView1.SelectedIndices[0];
                    if (startPosition == markPosition)
                    {
                        if (this.downWayRadio.Checked)
                        {
                            startPosition++;
                        }
                        else
                        {
                            startPosition--;
                        }
                    }
                }
                else
                {
                    if(this.downWayRadio.Checked)
                    {
                        startPosition=0;
                    }
                    else
                    {
                        startPosition=this.form.listView1.yyItems.Count-1;
                    }
                }
                this.markPosition = this.GetSearchContent(this.form.listView1, this.searchBox.Text, startPosition, this.downWayRadio.Checked, this.caseSensitiveCheck.Checked);
                if (this.markPosition == -1)
                {
                    MessageBox.Show("查找完成，未发现匹配条目");
                }
                else
                {
                    for (int i = 0; i < this.form.listView1.yyItems.Count; i++)
                    {
                        this.form.listView1.yyItems[i].Selected = false;
                    }
                    //this.form.listView1.yyItems[this.markPosition].EnsureVisible();
                    //this.form.listView1.yyItems[this.markPosition].Selected = true;
                    this.form.listView1.SelectedIndices.Clear();
                    this.form.listView1.yyItems[this.markPosition].EnsureVisible();
                    this.form.listView1.SelectedIndices.Add(this.markPosition);
                    this.form.listView1.EnsureVisible(this.form.listView1.yyItems[this.markPosition], 0);
                }
            }
            else
            {
                MessageBox.Show("查找字符为空");
            }
            this.form.listView1.YYRefresh();
        }
        /// <summary>
        /// 查找下一个条目
        /// </summary>
        /// <param name="yyListView"></param>
        /// <param name="search">待查找的项</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="direction">方向</param>
        /// <param name="caseSensitive">区分大小写</param>
        /// <returns></returns>
        private int GetSearchContent(YYListView yyListView, string search, int startPosition, bool direction,bool caseSensitive)
        {
            //if(startPosition==this.markPosition)
            //{
            //    this.StartPosition++;
            //}
            if (direction == true)
            {
                int i =startPosition;
                for (; i < yyListView.yyItems.Count; i++)
                {
                    if (this.Contain(yyListView.yyItems[i].SubItems[3].Text, search, caseSensitive))
                    {
                        //this.markPosition=i;
                        return i;
                    }
                }
                return -1;//代表查找到末尾
            }
            else
            {
                for (int i = startPosition; i >= 0; i--)
                {
                    if (this.Contain(yyListView.yyItems[i].SubItems[3].Text, search, caseSensitive))
                    {
                        return i;
                    }
                }
                return -1;//代表查找到末尾
            }
        }
        /// <summary>
        /// 寻找匹配项
        /// </summary>
        /// <param name="content">总内容</param>
        /// <param name="search">匹配项</param>
        /// <param name="caseSensitive">大小写</param>
        /// <returns>true，成功匹配，false，失败匹配</returns>
        private bool Contain(string content, string search, bool caseSensitive)
        {
            if (caseSensitive)
            {
                if (content.Contains(search))
                {
                    return true;
                }
            }
            else
            {
                Regex regex = new Regex(search, RegexOptions.IgnoreCase);
                if (regex.IsMatch(content))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
