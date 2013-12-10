namespace TimeMDev
{
    partial class ExportForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Srt_英文",
            "ANSI"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Srt_中文简体",
            "ANSI"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Srt_中文繁体",
            "ANSI"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Srt_中文简体_英文",
            "ANSI"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Srt_中文繁体_英文",
            "ANSI"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_英文",
            "UNICODE"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文简体",
            "UNICODE"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文繁体",
            "UNICODE"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文简体_英文",
            "UNICODE"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文繁体_英文",
            "UNICODE"}, -1);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectedOther = new System.Windows.Forms.Button();
            this.selectAll = new System.Windows.Forms.Button();
            this.setUnicode = new System.Windows.Forms.Button();
            this.setANSI = new System.Windows.Forms.Button();
            this.listViewShow = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.setAss = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chineseTEnd = new System.Windows.Forms.TextBox();
            this.chineseSEnd = new System.Windows.Forms.TextBox();
            this.englishEnd = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectePath = new System.Windows.Forms.Button();
            this.pathOut = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.setUTF8 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.setUTF8);
            this.groupBox1.Controls.Add(this.selectedOther);
            this.groupBox1.Controls.Add(this.selectAll);
            this.groupBox1.Controls.Add(this.setUnicode);
            this.groupBox1.Controls.Add(this.setANSI);
            this.groupBox1.Controls.Add(this.listViewShow);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 234);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件格式";
            // 
            // selectedOther
            // 
            this.selectedOther.Location = new System.Drawing.Point(332, 161);
            this.selectedOther.Name = "selectedOther";
            this.selectedOther.Size = new System.Drawing.Size(75, 23);
            this.selectedOther.TabIndex = 2;
            this.selectedOther.Text = "反选";
            this.selectedOther.UseVisualStyleBackColor = true;
            this.selectedOther.Click += new System.EventHandler(this.selectedOther_Click);
            // 
            // selectAll
            // 
            this.selectAll.Location = new System.Drawing.Point(332, 132);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(75, 23);
            this.selectAll.TabIndex = 2;
            this.selectAll.Text = "全选";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // setUnicode
            // 
            this.setUnicode.Location = new System.Drawing.Point(332, 50);
            this.setUnicode.Name = "setUnicode";
            this.setUnicode.Size = new System.Drawing.Size(128, 23);
            this.setUnicode.TabIndex = 1;
            this.setUnicode.Text = "设置为Unicode编码";
            this.setUnicode.UseVisualStyleBackColor = true;
            this.setUnicode.Click += new System.EventHandler(this.setUnicode_Click);
            // 
            // setANSI
            // 
            this.setANSI.Location = new System.Drawing.Point(333, 21);
            this.setANSI.Name = "setANSI";
            this.setANSI.Size = new System.Drawing.Size(127, 23);
            this.setANSI.TabIndex = 1;
            this.setANSI.Text = "设置为ANSI编码";
            this.setANSI.UseVisualStyleBackColor = true;
            this.setANSI.Click += new System.EventHandler(this.setANSI_Click);
            // 
            // listViewShow
            // 
            this.listViewShow.CheckBoxes = true;
            this.listViewShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            this.listViewShow.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.listViewShow.Location = new System.Drawing.Point(7, 21);
            this.listViewShow.Name = "listViewShow";
            this.listViewShow.Size = new System.Drawing.Size(319, 207);
            this.listViewShow.TabIndex = 0;
            this.listViewShow.UseCompatibleStateImageBehavior = false;
            this.listViewShow.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "内容格式";
            this.columnHeader1.Width = 194;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "编码";
            this.columnHeader2.Width = 111;
            // 
            // setAss
            // 
            this.setAss.Location = new System.Drawing.Point(19, 252);
            this.setAss.Name = "setAss";
            this.setAss.Size = new System.Drawing.Size(75, 23);
            this.setAss.TabIndex = 1;
            this.setAss.Text = "设置ASS";
            this.setAss.UseVisualStyleBackColor = true;
            this.setAss.Click += new System.EventHandler(this.setAss_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chineseTEnd);
            this.groupBox2.Controls.Add(this.chineseSEnd);
            this.groupBox2.Controls.Add(this.englishEnd);
            this.groupBox2.Location = new System.Drawing.Point(19, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(453, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "不同语言附加文件名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "繁体中文";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "简体中文";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "英文";
            // 
            // chineseTEnd
            // 
            this.chineseTEnd.Location = new System.Drawing.Point(350, 20);
            this.chineseTEnd.Name = "chineseTEnd";
            this.chineseTEnd.Size = new System.Drawing.Size(80, 21);
            this.chineseTEnd.TabIndex = 0;
            this.chineseTEnd.Text = "CHT";
            // 
            // chineseSEnd
            // 
            this.chineseSEnd.Location = new System.Drawing.Point(190, 20);
            this.chineseSEnd.Name = "chineseSEnd";
            this.chineseSEnd.Size = new System.Drawing.Size(77, 21);
            this.chineseSEnd.TabIndex = 0;
            this.chineseSEnd.Text = "CHS";
            // 
            // englishEnd
            // 
            this.englishEnd.Location = new System.Drawing.Point(47, 20);
            this.englishEnd.Name = "englishEnd";
            this.englishEnd.Size = new System.Drawing.Size(70, 21);
            this.englishEnd.TabIndex = 0;
            this.englishEnd.Text = "EN";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.selectePath);
            this.groupBox3.Controls.Add(this.pathOut);
            this.groupBox3.Location = new System.Drawing.Point(19, 359);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(453, 63);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输出文件夹";
            // 
            // selectePath
            // 
            this.selectePath.Location = new System.Drawing.Point(426, 19);
            this.selectePath.Name = "selectePath";
            this.selectePath.Size = new System.Drawing.Size(21, 23);
            this.selectePath.TabIndex = 1;
            this.selectePath.Text = "……";
            this.selectePath.UseVisualStyleBackColor = true;
            // 
            // pathOut
            // 
            this.pathOut.Location = new System.Drawing.Point(8, 21);
            this.pathOut.Name = "pathOut";
            this.pathOut.ReadOnly = true;
            this.pathOut.Size = new System.Drawing.Size(412, 21);
            this.pathOut.TabIndex = 0;
            this.pathOut.Text = "D:\\";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(66, 428);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(91, 33);
            this.confirm.TabIndex = 4;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(328, 428);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(91, 33);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // setUTF8
            // 
            this.setUTF8.Location = new System.Drawing.Point(333, 79);
            this.setUTF8.Name = "setUTF8";
            this.setUTF8.Size = new System.Drawing.Size(127, 23);
            this.setUTF8.TabIndex = 3;
            this.setUTF8.Text = "设置为UTF-8编码";
            this.setUTF8.UseVisualStyleBackColor = true;
            this.setUTF8.Click += new System.EventHandler(this.setUTF8_Click);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 467);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.setAss);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExportForm";
            this.Text = "输出";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewShow;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button setUnicode;
        private System.Windows.Forms.Button setANSI;
        private System.Windows.Forms.Button selectedOther;
        private System.Windows.Forms.Button selectAll;
        private System.Windows.Forms.Button setAss;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chineseTEnd;
        private System.Windows.Forms.TextBox chineseSEnd;
        private System.Windows.Forms.TextBox englishEnd;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button selectePath;
        private System.Windows.Forms.TextBox pathOut;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button setUTF8;
    }
}