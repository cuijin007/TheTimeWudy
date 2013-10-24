namespace TimeMDev
{
    partial class CustomSpilt
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
            this.label1 = new System.Windows.Forms.Label();
            this.timeLength = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.singleTimeLine = new System.Windows.Forms.RadioButton();
            this.doubleTimeLine = new System.Windows.Forms.RadioButton();
            this.pictureShowCut = new TimeMDev.PictureShowCut();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "时长：";
            // 
            // timeLength
            // 
            this.timeLength.AutoSize = true;
            this.timeLength.Location = new System.Drawing.Point(69, 25);
            this.timeLength.Name = "timeLength";
            this.timeLength.Size = new System.Drawing.Size(41, 12);
            this.timeLength.TabIndex = 2;
            this.timeLength.Text = "label2";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(387, 291);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "确认";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(480, 291);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // singleTimeLine
            // 
            this.singleTimeLine.AutoSize = true;
            this.singleTimeLine.Location = new System.Drawing.Point(304, 23);
            this.singleTimeLine.Name = "singleTimeLine";
            this.singleTimeLine.Size = new System.Drawing.Size(71, 16);
            this.singleTimeLine.TabIndex = 4;
            this.singleTimeLine.TabStop = true;
            this.singleTimeLine.Text = "单语字幕";
            this.singleTimeLine.UseVisualStyleBackColor = true;
            // 
            // doubleTimeLine
            // 
            this.doubleTimeLine.AutoSize = true;
            this.doubleTimeLine.Location = new System.Drawing.Point(417, 25);
            this.doubleTimeLine.Name = "doubleTimeLine";
            this.doubleTimeLine.Size = new System.Drawing.Size(71, 16);
            this.doubleTimeLine.TabIndex = 4;
            this.doubleTimeLine.TabStop = true;
            this.doubleTimeLine.Text = "双语字幕";
            this.doubleTimeLine.UseVisualStyleBackColor = true;
            // 
            // pictureShowCut
            // 
            this.pictureShowCut.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureShowCut.Location = new System.Drawing.Point(23, 53);
            this.pictureShowCut.Name = "pictureShowCut";
            this.pictureShowCut.Size = new System.Drawing.Size(519, 223);
            this.pictureShowCut.TabIndex = 0;
            // 
            // CustomSpilt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 326);
            this.Controls.Add(this.pictureShowCut);
            this.Controls.Add(this.doubleTimeLine);
            this.Controls.Add(this.singleTimeLine);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.timeLength);
            this.Controls.Add(this.label1);
            this.Name = "CustomSpilt";
            this.Text = "自定义拆分";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureShowCut pictureShowCut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeLength;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.RadioButton singleTimeLine;
        private System.Windows.Forms.RadioButton doubleTimeLine;
    }
}