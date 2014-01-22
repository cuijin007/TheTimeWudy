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
    public partial class SetSaveEncoding : Form
    {
         private static Encoding encoding;
         public SetSaveEncoding()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            string encodingStr = Config.DefaultConfig["SaveSubEncoding"];
            this.comboEncodingShow.Text = encodingStr;
        }
        private void confirm_Click(object sender, EventArgs e)
        {
            Config.DefaultConfig["SaveSubEncoding"] = this.comboEncodingShow.Text;
            this.Close();
        }
        public static Encoding GetSaveSubEncoding()
        {
            string encodingStr = Config.DefaultConfig["SaveSubEncoding"];
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            if (encodingStr.Equals("unicode"))
            {
                encoding = Encoding.Unicode;
            }
            if (encodingStr.Equals("ansi"))
            {
                encoding = Encoding.Default;
            }
            if (encodingStr.Equals("utf8"))
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
