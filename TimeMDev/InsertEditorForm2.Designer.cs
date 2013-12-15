namespace TimeMDev
{
    partial class InsertEditorForm2
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
            this.listViewShow = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startTimeBox = new System.Windows.Forms.TextBox();
            this.endtimeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.contentBox = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewShow
            // 
            this.listViewShow.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewShow.FullRowSelect = true;
            this.listViewShow.Location = new System.Drawing.Point(13, 13);
            this.listViewShow.Name = "listViewShow";
            this.listViewShow.Size = new System.Drawing.Size(216, 342);
            this.listViewShow.TabIndex = 0;
            this.listViewShow.UseCompatibleStateImageBehavior = false;
            this.listViewShow.View = System.Windows.Forms.View.Details;
            this.listViewShow.ItemActivate += new System.EventHandler(this.listViewShow_ItemActivate);
            this.listViewShow.Click += new System.EventHandler(this.listViewShow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.contentBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.endtimeBox);
            this.groupBox1.Controls.Add(this.startTimeBox);
            this.groupBox1.Location = new System.Drawing.Point(236, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 266);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插入字幕行";
            // 
            // startTimeBox
            // 
            this.startTimeBox.Location = new System.Drawing.Point(67, 20);
            this.startTimeBox.Name = "startTimeBox";
            this.startTimeBox.Size = new System.Drawing.Size(100, 21);
            this.startTimeBox.TabIndex = 0;
            // 
            // endtimeBox
            // 
            this.endtimeBox.Location = new System.Drawing.Point(235, 20);
            this.endtimeBox.Name = "endtimeBox";
            this.endtimeBox.Size = new System.Drawing.Size(100, 21);
            this.endtimeBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "结束时间";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "时长";
            this.columnHeader2.Width = 135;
            // 
            // contentBox
            // 
            this.contentBox.Location = new System.Drawing.Point(8, 59);
            this.contentBox.Multiline = true;
            this.contentBox.Name = "contentBox";
            this.contentBox.Size = new System.Drawing.Size(327, 190);
            this.contentBox.TabIndex = 2;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(244, 308);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(82, 27);
            this.confirm.TabIndex = 2;
            this.confirm.Text = "确认";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(489, 308);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(82, 27);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InsertEditorForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 367);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewShow);
            this.Name = "InsertEditorForm2";
            this.Text = "空闲行";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewShow;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox endtimeBox;
        private System.Windows.Forms.TextBox startTimeBox;
        private System.Windows.Forms.TextBox contentBox;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}