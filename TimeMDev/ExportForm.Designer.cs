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
            "Unicode"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文简体",
            "Unicode"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文繁体",
            "Unicode"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文简体_英文",
            "Unicode"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ass_中文繁体_英文",
            "Unicode"}, -1);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewShow = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.turnToAnsiButton = new System.Windows.Forms.Button();
            this.turnToUnicodeButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.turnToUnicodeButton);
            this.groupBox1.Controls.Add(this.turnToAnsiButton);
            this.groupBox1.Controls.Add(this.listViewShow);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 234);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件格式";
            // 
            // listViewShow
            // 
            this.listViewShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
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
            // turnToAnsiButton
            // 
            this.turnToAnsiButton.Location = new System.Drawing.Point(333, 21);
            this.turnToAnsiButton.Name = "turnToAnsiButton";
            this.turnToAnsiButton.Size = new System.Drawing.Size(127, 23);
            this.turnToAnsiButton.TabIndex = 1;
            this.turnToAnsiButton.Text = "设置为ANSI编码";
            this.turnToAnsiButton.UseVisualStyleBackColor = true;
            // 
            // turnToUnicodeButton
            // 
            this.turnToUnicodeButton.Location = new System.Drawing.Point(332, 50);
            this.turnToUnicodeButton.Name = "turnToUnicodeButton";
            this.turnToUnicodeButton.Size = new System.Drawing.Size(128, 23);
            this.turnToUnicodeButton.TabIndex = 1;
            this.turnToUnicodeButton.Text = "设置为Unicode编码";
            this.turnToUnicodeButton.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 497);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExportForm";
            this.Text = "输出";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewShow;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button turnToUnicodeButton;
        private System.Windows.Forms.Button turnToAnsiButton;
    }
}