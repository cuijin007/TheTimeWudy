namespace TimeMDev
{
    partial class AddMutiLinesForm
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
            this.lineCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.fristTimeLength = new System.Windows.Forms.CheckBox();
            this.timeSpiltText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.blankLine = new System.Windows.Forms.RadioButton();
            this.nonBlankLine = new System.Windows.Forms.RadioButton();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lineCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lineCount
            // 
            this.lineCount.Location = new System.Drawing.Point(168, 31);
            this.lineCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.lineCount.Name = "lineCount";
            this.lineCount.Size = new System.Drawing.Size(120, 21);
            this.lineCount.TabIndex = 0;
            this.lineCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(63, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "插入行数：";
            // 
            // fristTimeLength
            // 
            this.fristTimeLength.AutoSize = true;
            this.fristTimeLength.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fristTimeLength.Location = new System.Drawing.Point(12, 67);
            this.fristTimeLength.Name = "fristTimeLength";
            this.fristTimeLength.Size = new System.Drawing.Size(139, 20);
            this.fristTimeLength.TabIndex = 2;
            this.fristTimeLength.Text = "初始时间长度：";
            this.fristTimeLength.UseVisualStyleBackColor = true;
            this.fristTimeLength.CheckedChanged += new System.EventHandler(this.fristTimeLength_CheckedChanged);
            // 
            // timeSpiltText
            // 
            this.timeSpiltText.Enabled = false;
            this.timeSpiltText.Location = new System.Drawing.Point(168, 65);
            this.timeSpiltText.Name = "timeSpiltText";
            this.timeSpiltText.Size = new System.Drawing.Size(120, 21);
            this.timeSpiltText.TabIndex = 3;
            this.timeSpiltText.Text = "00:00:01,000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "时:分:秒,毫秒";
            // 
            // blankLine
            // 
            this.blankLine.AutoSize = true;
            this.blankLine.Checked = true;
            this.blankLine.Location = new System.Drawing.Point(38, 115);
            this.blankLine.Name = "blankLine";
            this.blankLine.Size = new System.Drawing.Size(59, 16);
            this.blankLine.TabIndex = 5;
            this.blankLine.TabStop = true;
            this.blankLine.Text = "空白行";
            this.blankLine.UseVisualStyleBackColor = true;
            // 
            // nonBlankLine
            // 
            this.nonBlankLine.AutoSize = true;
            this.nonBlankLine.Location = new System.Drawing.Point(168, 115);
            this.nonBlankLine.Name = "nonBlankLine";
            this.nonBlankLine.Size = new System.Drawing.Size(107, 16);
            this.nonBlankLine.TabIndex = 5;
            this.nonBlankLine.TabStop = true;
            this.nonBlankLine.Text = "按顺序填充序号";
            this.nonBlankLine.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(38, 160);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(84, 28);
            this.confirm.TabIndex = 6;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(168, 160);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(84, 28);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // AddMutiLinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 210);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.nonBlankLine);
            this.Controls.Add(this.blankLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeSpiltText);
            this.Controls.Add(this.fristTimeLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineCount);
            this.Name = "AddMutiLinesForm";
            this.Text = "选择插入字幕方式";
            ((System.ComponentModel.ISupportInitialize)(this.lineCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown lineCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox fristTimeLength;
        private System.Windows.Forms.TextBox timeSpiltText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton blankLine;
        private System.Windows.Forms.RadioButton nonBlankLine;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}