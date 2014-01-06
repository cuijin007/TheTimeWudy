namespace TimeMDev
{
    partial class SetAssForm
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
            this.scriptInfoBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.v4StyleBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.eventBox = new System.Windows.Forms.TextBox();
            this.englishEndBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.englishHeadBox = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scriptInfoBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[ScriptInfo]";
            // 
            // scriptInfoBox
            // 
            this.scriptInfoBox.Location = new System.Drawing.Point(7, 21);
            this.scriptInfoBox.Multiline = true;
            this.scriptInfoBox.Name = "scriptInfoBox";
            this.scriptInfoBox.Size = new System.Drawing.Size(480, 89);
            this.scriptInfoBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.v4StyleBox);
            this.groupBox2.Location = new System.Drawing.Point(13, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 125);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[V4+ Style]";
            // 
            // v4StyleBox
            // 
            this.v4StyleBox.Location = new System.Drawing.Point(7, 21);
            this.v4StyleBox.Multiline = true;
            this.v4StyleBox.Name = "v4StyleBox";
            this.v4StyleBox.Size = new System.Drawing.Size(480, 87);
            this.v4StyleBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.eventBox);
            this.groupBox3.Location = new System.Drawing.Point(13, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 90);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[Event]";
            // 
            // eventBox
            // 
            this.eventBox.Location = new System.Drawing.Point(7, 21);
            this.eventBox.Multiline = true;
            this.eventBox.Name = "eventBox";
            this.eventBox.Size = new System.Drawing.Size(480, 52);
            this.eventBox.TabIndex = 0;
            // 
            // englishEndBox
            // 
            this.englishEndBox.FormattingEnabled = true;
            this.englishEndBox.Location = new System.Drawing.Point(1, 21);
            this.englishEndBox.Name = "englishEndBox";
            this.englishEndBox.Size = new System.Drawing.Size(487, 20);
            this.englishEndBox.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.englishHeadBox);
            this.groupBox4.Location = new System.Drawing.Point(13, 368);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(493, 40);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "[英文行首特效代码]";
            // 
            // englishHeadBox
            // 
            this.englishHeadBox.FormattingEnabled = true;
            this.englishHeadBox.Items.AddRange(new object[] {
            "{\\fn方正黑体简体}{\\fs14}{\\bord1}{\\shad1}{\\b0}{\\c&HFFFFFF&}{\\3c&H111111&}{\\4c&H111111&}",
            "{\\fn微软雅黑}{\\fs14}{\\bord1}{\\shad1}{\\b0}{\\c&HFFFFFF&}{\\3c&H111111&}{\\4c&H111111&}",
            "{\\fn微软雅黑}{\\fs16}{\\bord1}{\\shad1}{\\b0}{\\c&HFFFFFF&}{\\3c&H111111&}{\\4c&H111111&}",
            "{\\fn微软雅黑}{\\fs15}{\\bord1}{\\shad1}{\\b0}{\\c&HFFFFFF&}{\\3c&H111111&}{\\4c&H111111&}",
            "{\\fn方正综艺简体}{\\fs14}{\\b0}{\\c&HFFFFFF&}{\\3c&H2F2F2F&}{\\4c&H000000&}",
            "{\\fn微软雅黑}{\\b0}{\\fs14}{\\3c&H202020&}{\\shad1}"});
            this.englishHeadBox.Location = new System.Drawing.Point(1, 14);
            this.englishHeadBox.Name = "englishHeadBox";
            this.englishHeadBox.Size = new System.Drawing.Size(487, 20);
            this.englishHeadBox.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.englishEndBox);
            this.groupBox5.Location = new System.Drawing.Point(14, 414);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(493, 50);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "[英文行末特效代码]";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(345, 486);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 1;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(432, 486);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SetAssForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 521);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SetAssForm";
            this.Text = "设置ASS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox scriptInfoBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox v4StyleBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox eventBox;
        private System.Windows.Forms.ComboBox englishEndBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox englishHeadBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;

    }
}