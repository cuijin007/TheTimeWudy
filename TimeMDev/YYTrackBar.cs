using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMDev
{
    public partial class YYTrackBar : TrackBar
    {
        public YYTrackBar()
        {
            InitializeComponent();
        }
        public void SetDoubleBuffer(bool buffer)
        {
            this.DoubleBuffered = buffer;
        }
        protected override bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }
    }
}
