namespace TimeMDev
{
    partial class SetDelayTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioMin = new System.Windows.Forms.RadioButton();
            this.radioPlus = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.timeShow = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancel);
            this.groupBox1.Controls.Add(this.confirm);
            this.groupBox1.Controls.Add(this.timeShow);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioPlus);
            this.groupBox1.Controls.Add(this.radioMin);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对齐播放器时间";
            // 
            // radioMin
            // 
            this.radioMin.AutoSize = true;
            this.radioMin.Location = new System.Drawing.Point(20, 32);
            this.radioMin.Name = "radioMin";
            this.radioMin.Size = new System.Drawing.Size(77, 16);
            this.radioMin.TabIndex = 0;
            this.radioMin.TabStop = true;
            this.radioMin.Text = "提前（-）";
            this.radioMin.UseVisualStyleBackColor = true;
            // 
            // radioPlus
            // 
            this.radioPlus.AutoSize = true;
            this.radioPlus.Location = new System.Drawing.Point(176, 32);
            this.radioPlus.Name = "radioPlus";
            this.radioPlus.Size = new System.Drawing.Size(77, 16);
            this.radioPlus.TabIndex = 0;
            this.radioPlus.TabStop = true;
            this.radioPlus.Text = "延迟（+）";
            this.radioPlus.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "小时:分:秒,毫秒";
            // 
            // timeShow
            // 
            this.timeShow.Location = new System.Drawing.Point(146, 64);
            this.timeShow.Name = "timeShow";
            this.timeShow.Size = new System.Drawing.Size(125, 21);
            this.timeShow.TabIndex = 2;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(20, 109);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(196, 109);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SetDelayTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 164);
            this.Controls.Add(this.groupBox1);
            this.Name = "SetDelayTime";
            this.Text = "设置延迟时间";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox timeShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioPlus;
        private System.Windows.Forms.RadioButton radioMin;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button confirm;
    }
}