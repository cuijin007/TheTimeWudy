namespace TimeMDev
{
    partial class TranslateForm
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
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.nowTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selectedLine = new System.Windows.Forms.RadioButton();
            this.allLine = new System.Windows.Forms.RadioButton();
            this.betweenCheckdLine = new System.Windows.Forms.RadioButton();
            this.afterThisLine = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nowTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "为选择行设置新的开始时间";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(44, 227);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 1;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(171, 227);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // nowTime
            // 
            this.nowTime.Location = new System.Drawing.Point(113, 25);
            this.nowTime.Name = "nowTime";
            this.nowTime.Size = new System.Drawing.Size(141, 21);
            this.nowTime.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "小时:分:秒,毫秒";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.afterThisLine);
            this.groupBox2.Controls.Add(this.betweenCheckdLine);
            this.groupBox2.Controls.Add(this.allLine);
            this.groupBox2.Controls.Add(this.selectedLine);
            this.groupBox2.Location = new System.Drawing.Point(12, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 124);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择所选时间移动值";
            // 
            // selectedLine
            // 
            this.selectedLine.AutoSize = true;
            this.selectedLine.Checked = true;
            this.selectedLine.Location = new System.Drawing.Point(12, 20);
            this.selectedLine.Name = "selectedLine";
            this.selectedLine.Size = new System.Drawing.Size(59, 16);
            this.selectedLine.TabIndex = 0;
            this.selectedLine.TabStop = true;
            this.selectedLine.Text = "选择行";
            this.selectedLine.UseVisualStyleBackColor = true;
            // 
            // allLine
            // 
            this.allLine.AutoSize = true;
            this.allLine.Location = new System.Drawing.Point(12, 42);
            this.allLine.Name = "allLine";
            this.allLine.Size = new System.Drawing.Size(59, 16);
            this.allLine.TabIndex = 0;
            this.allLine.TabStop = true;
            this.allLine.Text = "所有行";
            this.allLine.UseVisualStyleBackColor = true;
            // 
            // betweenCheckdLine
            // 
            this.betweenCheckdLine.AutoSize = true;
            this.betweenCheckdLine.Location = new System.Drawing.Point(12, 64);
            this.betweenCheckdLine.Name = "betweenCheckdLine";
            this.betweenCheckdLine.Size = new System.Drawing.Size(119, 16);
            this.betweenCheckdLine.TabIndex = 0;
            this.betweenCheckdLine.TabStop = true;
            this.betweenCheckdLine.Text = "前后标记之间所有";
            this.betweenCheckdLine.UseVisualStyleBackColor = true;
            // 
            // afterThisLine
            // 
            this.afterThisLine.AutoSize = true;
            this.afterThisLine.Location = new System.Drawing.Point(12, 86);
            this.afterThisLine.Name = "afterThisLine";
            this.afterThisLine.Size = new System.Drawing.Size(107, 16);
            this.afterThisLine.TabIndex = 0;
            this.afterThisLine.TabStop = true;
            this.afterThisLine.Text = "当前行之后所有";
            this.afterThisLine.UseVisualStyleBackColor = true;
            // 
            // TranslateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox1);
            this.Name = "TranslateForm";
            this.Text = "平移时间";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nowTime;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton afterThisLine;
        private System.Windows.Forms.RadioButton betweenCheckdLine;
        private System.Windows.Forms.RadioButton allLine;
        private System.Windows.Forms.RadioButton selectedLine;
    }
}