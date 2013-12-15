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
    public partial class InsertEditorForm1 : Form
    {
        InsertEditorForm1Para insertEditorForm1Para;
        public InsertEditorForm1(InsertEditorForm1Para insertEditorForm1Para)
        {
            InitializeComponent();
            this.insertEditorForm1Para = insertEditorForm1Para;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.insertEditorForm1Para.time = TimeLineReadWrite.TimeInAss(this.nowTime.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.insertEditorForm1Para.time = TimeLineReadWrite.TimeInAss(this.nowTime.Text);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public class InsertEditorForm1Para
        {
            public double time;
        }
    }
}
