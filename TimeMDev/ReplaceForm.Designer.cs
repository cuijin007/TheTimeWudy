namespace TimeMDev
{
    partial class ReplaceForm
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
            this.searchBox = new System.Windows.Forms.TextBox();
            this.replaceBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.replaceButton = new System.Windows.Forms.Button();
            this.listViewShow = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.addTemplate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allRadio = new System.Windows.Forms.RadioButton();
            this.selectedRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.englishCheck = new System.Windows.Forms.CheckBox();
            this.chineseCheck = new System.Windows.Forms.CheckBox();
            this.caseSensitiveCheck = new System.Windows.Forms.CheckBox();
            this.replaceAllCheck = new System.Windows.Forms.CheckBox();
            this.deleteTemplate = new System.Windows.Forms.Button();
            this.loadTemplate = new System.Windows.Forms.Button();
            this.saveTemplate = new System.Windows.Forms.Button();
            this.closeTemplate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 31);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(193, 21);
            this.searchBox.TabIndex = 0;
            // 
            // replaceBox
            // 
            this.replaceBox.Location = new System.Drawing.Point(12, 80);
            this.replaceBox.Name = "replaceBox";
            this.replaceBox.Size = new System.Drawing.Size(193, 21);
            this.replaceBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查找内容：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "替换为：";
            // 
            // replaceButton
            // 
            this.replaceButton.Location = new System.Drawing.Point(337, 112);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(85, 27);
            this.replaceButton.TabIndex = 1;
            this.replaceButton.Text = "替换";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // listViewShow
            // 
            this.listViewShow.CheckBoxes = true;
            this.listViewShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewShow.FullRowSelect = true;
            this.listViewShow.Location = new System.Drawing.Point(6, 155);
            this.listViewShow.Name = "listViewShow";
            this.listViewShow.Size = new System.Drawing.Size(325, 238);
            this.listViewShow.TabIndex = 3;
            this.listViewShow.UseCompatibleStateImageBehavior = false;
            this.listViewShow.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "选中";
            this.columnHeader1.Width = 44;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "查找内容";
            this.columnHeader2.Width = 123;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "替换为";
            this.columnHeader3.Width = 128;
            // 
            // addTemplate
            // 
            this.addTemplate.Location = new System.Drawing.Point(337, 155);
            this.addTemplate.Name = "addTemplate";
            this.addTemplate.Size = new System.Drawing.Size(84, 25);
            this.addTemplate.TabIndex = 4;
            this.addTemplate.Text = "加入模板<<";
            this.addTemplate.UseVisualStyleBackColor = true;
            this.addTemplate.Click += new System.EventHandler(this.addTemplate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.allRadio);
            this.groupBox1.Controls.Add(this.selectedRadio);
            this.groupBox1.Location = new System.Drawing.Point(211, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(81, 79);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "替换范围";
            // 
            // allRadio
            // 
            this.allRadio.AutoSize = true;
            this.allRadio.Checked = true;
            this.allRadio.Location = new System.Drawing.Point(6, 43);
            this.allRadio.Name = "allRadio";
            this.allRadio.Size = new System.Drawing.Size(59, 16);
            this.allRadio.TabIndex = 0;
            this.allRadio.TabStop = true;
            this.allRadio.Text = "所有行";
            this.allRadio.UseVisualStyleBackColor = true;
            // 
            // selectedRadio
            // 
            this.selectedRadio.AutoSize = true;
            this.selectedRadio.Location = new System.Drawing.Point(6, 20);
            this.selectedRadio.Name = "selectedRadio";
            this.selectedRadio.Size = new System.Drawing.Size(59, 16);
            this.selectedRadio.TabIndex = 0;
            this.selectedRadio.TabStop = true;
            this.selectedRadio.Text = "选择行";
            this.selectedRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.englishCheck);
            this.groupBox2.Controls.Add(this.chineseCheck);
            this.groupBox2.Location = new System.Drawing.Point(298, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(123, 79);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "针对内容";
            // 
            // englishCheck
            // 
            this.englishCheck.AutoSize = true;
            this.englishCheck.Location = new System.Drawing.Point(6, 44);
            this.englishCheck.Name = "englishCheck";
            this.englishCheck.Size = new System.Drawing.Size(72, 16);
            this.englishCheck.TabIndex = 0;
            this.englishCheck.Text = "英文部分";
            this.englishCheck.UseVisualStyleBackColor = true;
            // 
            // chineseCheck
            // 
            this.chineseCheck.AutoSize = true;
            this.chineseCheck.Location = new System.Drawing.Point(6, 21);
            this.chineseCheck.Name = "chineseCheck";
            this.chineseCheck.Size = new System.Drawing.Size(72, 16);
            this.chineseCheck.TabIndex = 0;
            this.chineseCheck.Text = "中文部分";
            this.chineseCheck.UseVisualStyleBackColor = true;
            // 
            // caseSensitiveCheck
            // 
            this.caseSensitiveCheck.AutoSize = true;
            this.caseSensitiveCheck.Checked = true;
            this.caseSensitiveCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.caseSensitiveCheck.Location = new System.Drawing.Point(14, 112);
            this.caseSensitiveCheck.Name = "caseSensitiveCheck";
            this.caseSensitiveCheck.Size = new System.Drawing.Size(84, 16);
            this.caseSensitiveCheck.TabIndex = 0;
            this.caseSensitiveCheck.Text = "区分大小写";
            this.caseSensitiveCheck.UseVisualStyleBackColor = true;
            // 
            // replaceAllCheck
            // 
            this.replaceAllCheck.AutoSize = true;
            this.replaceAllCheck.Location = new System.Drawing.Point(109, 112);
            this.replaceAllCheck.Name = "replaceAllCheck";
            this.replaceAllCheck.Size = new System.Drawing.Size(96, 16);
            this.replaceAllCheck.TabIndex = 0;
            this.replaceAllCheck.Text = "使用批量替换";
            this.replaceAllCheck.UseVisualStyleBackColor = true;
            this.replaceAllCheck.CheckedChanged += new System.EventHandler(this.replaceAllCheck_CheckedChanged);
            // 
            // deleteTemplate
            // 
            this.deleteTemplate.Location = new System.Drawing.Point(337, 186);
            this.deleteTemplate.Name = "deleteTemplate";
            this.deleteTemplate.Size = new System.Drawing.Size(84, 25);
            this.deleteTemplate.TabIndex = 4;
            this.deleteTemplate.Text = "移除模板>>";
            this.deleteTemplate.UseVisualStyleBackColor = true;
            this.deleteTemplate.Click += new System.EventHandler(this.deleteTemplate_Click);
            // 
            // loadTemplate
            // 
            this.loadTemplate.Location = new System.Drawing.Point(337, 295);
            this.loadTemplate.Name = "loadTemplate";
            this.loadTemplate.Size = new System.Drawing.Size(84, 25);
            this.loadTemplate.TabIndex = 4;
            this.loadTemplate.Text = "载入模板";
            this.loadTemplate.UseVisualStyleBackColor = true;
            this.loadTemplate.Click += new System.EventHandler(this.loadTemplate_Click);
            // 
            // saveTemplate
            // 
            this.saveTemplate.Location = new System.Drawing.Point(337, 330);
            this.saveTemplate.Name = "saveTemplate";
            this.saveTemplate.Size = new System.Drawing.Size(84, 25);
            this.saveTemplate.TabIndex = 4;
            this.saveTemplate.Text = "保存模板";
            this.saveTemplate.UseVisualStyleBackColor = true;
            this.saveTemplate.Click += new System.EventHandler(this.saveTemplate_Click);
            // 
            // closeTemplate
            // 
            this.closeTemplate.Location = new System.Drawing.Point(337, 368);
            this.closeTemplate.Name = "closeTemplate";
            this.closeTemplate.Size = new System.Drawing.Size(84, 25);
            this.closeTemplate.TabIndex = 4;
            this.closeTemplate.Text = "关闭模板";
            this.closeTemplate.UseVisualStyleBackColor = true;
            this.closeTemplate.Click += new System.EventHandler(this.closeTemplate_Click);
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 142);
            this.Controls.Add(this.replaceAllCheck);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.caseSensitiveCheck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.closeTemplate);
            this.Controls.Add(this.saveTemplate);
            this.Controls.Add(this.loadTemplate);
            this.Controls.Add(this.deleteTemplate);
            this.Controls.Add(this.addTemplate);
            this.Controls.Add(this.listViewShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.replaceBox);
            this.Controls.Add(this.searchBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReplaceForm";
            this.Text = "替换";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.TextBox replaceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.ListView listViewShow;
        private System.Windows.Forms.Button addTemplate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton allRadio;
        private System.Windows.Forms.RadioButton selectedRadio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox englishCheck;
        private System.Windows.Forms.CheckBox chineseCheck;
        private System.Windows.Forms.CheckBox caseSensitiveCheck;
        private System.Windows.Forms.CheckBox replaceAllCheck;
        private System.Windows.Forms.Button deleteTemplate;
        private System.Windows.Forms.Button loadTemplate;
        private System.Windows.Forms.Button saveTemplate;
        private System.Windows.Forms.Button closeTemplate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}