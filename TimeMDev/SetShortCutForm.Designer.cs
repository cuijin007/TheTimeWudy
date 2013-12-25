namespace TimeMDev
{
    partial class SetShortCutForm
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
            this.mainCombo = new System.Windows.Forms.ComboBox();
            this.subCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addShortCut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.newKey = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nowKey = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clearShortCut = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainCombo
            // 
            this.mainCombo.FormattingEnabled = true;
            this.mainCombo.Location = new System.Drawing.Point(109, 368);
            this.mainCombo.Name = "mainCombo";
            this.mainCombo.Size = new System.Drawing.Size(147, 20);
            this.mainCombo.TabIndex = 0;
            this.mainCombo.SelectedIndexChanged += new System.EventHandler(this.mainCombo_SelectedIndexChanged);
            // 
            // subCombo
            // 
            this.subCombo.FormattingEnabled = true;
            this.subCombo.Location = new System.Drawing.Point(109, 421);
            this.subCombo.Name = "subCombo";
            this.subCombo.Size = new System.Drawing.Size(147, 20);
            this.subCombo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "标题按钮:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "下拉选项:";
            // 
            // addShortCut
            // 
            this.addShortCut.Location = new System.Drawing.Point(9, 175);
            this.addShortCut.Name = "addShortCut";
            this.addShortCut.Size = new System.Drawing.Size(70, 23);
            this.addShortCut.TabIndex = 3;
            this.addShortCut.Text = "添加";
            this.addShortCut.UseVisualStyleBackColor = true;
            this.addShortCut.Click += new System.EventHandler(this.addShortCut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.newKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nowKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.clearShortCut);
            this.groupBox1.Controls.Add(this.addShortCut);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 215);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置快捷键";
            // 
            // newKey
            // 
            this.newKey.AutoSize = true;
            this.newKey.Location = new System.Drawing.Point(280, 120);
            this.newKey.Name = "newKey";
            this.newKey.Size = new System.Drawing.Size(65, 12);
            this.newKey.TabIndex = 4;
            this.newKey.Text = "请按下按键";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "新按键";
            // 
            // nowKey
            // 
            this.nowKey.AutoSize = true;
            this.nowKey.Location = new System.Drawing.Point(280, 39);
            this.nowKey.Name = "nowKey";
            this.nowKey.Size = new System.Drawing.Size(53, 12);
            this.nowKey.TabIndex = 4;
            this.nowKey.Text = "现有按键";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "现有按键";
            // 
            // clearShortCut
            // 
            this.clearShortCut.Location = new System.Drawing.Point(97, 175);
            this.clearShortCut.Name = "clearShortCut";
            this.clearShortCut.Size = new System.Drawing.Size(68, 23);
            this.clearShortCut.TabIndex = 3;
            this.clearShortCut.Text = "清空";
            this.clearShortCut.UseVisualStyleBackColor = true;
            this.clearShortCut.Click += new System.EventHandler(this.clearShortCut_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(10, 21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(250, 148);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "功能";
            this.columnHeader1.Width = 99;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "快捷键";
            this.columnHeader2.Width = 118;
            // 
            // SetShortCutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 237);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.subCombo);
            this.KeyPreview = true;
            this.Name = "SetShortCutForm";
            this.Text = "设置快捷键";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SetShortCutForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetShortCutForm_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetShortCutForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox mainCombo;
        private System.Windows.Forms.ComboBox subCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addShortCut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button clearShortCut;
        private System.Windows.Forms.Label newKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label nowKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}