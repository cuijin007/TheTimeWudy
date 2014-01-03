using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev
{
    public partial class SetAssForm : Form
    {
        AssInfo assInfo;
        public SetAssForm(AssInfo assInfo)
        {
            InitializeComponent();
            this.InitBox();
            this.assInfo=assInfo;
        }
        public SetAssForm()
        {
            InitializeComponent();
            this.scriptInfoBox.Text = TimeLineReadWrite.GetAssInfo().ScriptInfo;
            this.v4StyleBox.Text = TimeLineReadWrite.GetAssInfo().v4Style;
            this.eventBox.Text = TimeLineReadWrite.GetAssInfo().eventContent;
            this.englishHeadBox.Text = TimeLineReadWrite.GetAssInfo().EnglishHead;
            this.englishEndBox.Text = TimeLineReadWrite.GetAssInfo().EnglishEnd;
            this.assInfo = TimeLineReadWrite.GetAssInfo();
        }
        private void confirm_Click(object sender, EventArgs e)
        {
            this.assInfo.eventContent=eventBox.Text;
            this.assInfo.EnglishHead=this.englishHeadBox.Text;
            this.assInfo.EnglishEnd=this.englishEndBox.Text;
            this.assInfo.v4Style=this.v4StyleBox.Text;
            this.assInfo.ScriptInfo=this.scriptInfoBox.Text;
        }


        private void InitBox()
        {
            this.scriptInfoBox.Text = "; // 此字幕由TimeM生成\r\n; // 欢迎访问人人影视 http://www.YYeTs.net\r\nTitle:YYeTs\r\nOriginal Script:YYeTs\r\nSynch Point:1\r\nScriptType:v4.00+\r\nCollisions:Normal\r\n";
            this.v4StyleBox.Text = "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding\r\nStyle: Default,方正黑体简体,20,&H00FFFFFF,&HF0000000,&H00000000,&H32000000,0,0,0,0,100,10";
            this.eventBox.Text = "Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text";
            this.englishHeadBox.SelectedIndex = 0;
        }

    }
    public class AssInfo
    {
        public string ScriptInfo="; // 此字幕由TimeM生成\r\n; // 欢迎访问人人影视 http://www.YYeTs.net\r\nTitle:YYeTs\r\nOriginal Script:YYeTs\r\nSynch Point:1\r\nScriptType:v4.00+\r\nCollisions:Normal\r\n";
        public string v4Style="Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding\r\nStyle: Default,方正黑体简体,20,&H00FFFFFF,&HF0000000,&H00000000,&H32000000,0,0,0,0,100,10";
        public string eventContent="Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text";
        public string EnglishHead="{\\fn方正黑体简体}{\\fs14}{\\bord1}{\\shad1}{\\b0}{\\c&HFFFFFF&}{\\3c&H111111&}{\\4c&H111111&}";
        public string EnglishEnd="";
    }
}
