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
    public partial class SetSaveAuto : Form
    {
        public static int autoSaveCount = 0;
        public SetSaveAuto()
        {
            InitializeComponent();
        }

        private void Init()
        {
            this.path.Text = Config.DefaultConfig["AutoSavePath"];
            this.functionTime.Text = Config.DefaultConfig["AutoSaveFunctionTime"];
            this.count.Text = Config.DefaultConfig["AutoSaveCount"];

        }

        private void choosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.path.Text = fbd.SelectedPath;
            }
        }

        public static int GetAutoSaveCount()
        {
            int time=0;
            try
            {
                time=Int32.Parse(Config.DefaultConfig["AutoSaveCount"]);
            }
            catch
            {
                Config.DefaultConfig["AutoSaveCount"] = 1 + "";
            }
            autoSaveCount++;
            if (autoSaveCount <= 1)
            {
                autoSaveCount = 1;
            }
            else if ( autoSaveCount>time)
            {
                autoSaveCount = 1;
            }
            return autoSaveCount;
        }
        public static string GetAutoSavePath()
        {
            return Config.DefaultConfig["AutoSavePath"];
        }
        public static int GetAutoSaveFunctionTime()
        {
            try
            {
                return Int32.Parse(Config.DefaultConfig["AutoSaveFunctionTime"]);
            }
            catch
            {
                Config.DefaultConfig["AutoSaveFunctionTime"] = 10 + "";
                return 10;
            }
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            int timeI;
            int countI;
            try
            {
                timeI = Int32.Parse(this.functionTime.Text);
                countI = Int32.Parse(this.count.Text);
                if (countI > 0 && timeI > 0)
                {
                    Config.DefaultConfig["AutoSavePath"] = this.path.Text;
                    Config.DefaultConfig["AutoSaveFunctionTime"] = timeI + "";
                    Config.DefaultConfig["AutoSaveCount"] = countI + "";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("格式输入有误,执行次数，份数均要大于0");
                }
            }
            catch
            {
                MessageBox.Show("格式输入有误");
            }
           

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
