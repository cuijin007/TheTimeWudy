namespace TimeMDev
{
    partial class LoadTranslationForm
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
            this.byBlank = new System.Windows.Forms.RadioButton();
            this.byLine = new System.Windows.Forms.RadioButton();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.byBlank);
            this.groupBox1.Controls.Add(this.byLine);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入翻译稿";
            // 
            // byBlank
            // 
            this.byBlank.AutoSize = true;
            this.byBlank.Location = new System.Drawing.Point(27, 52);
            this.byBlank.Name = "byBlank";
            this.byBlank.Size = new System.Drawing.Size(83, 16);
            this.byBlank.TabIndex = 0;
            this.byBlank.Text = "按空行分割";
            this.byBlank.UseVisualStyleBackColor = true;
            // 
            // byLine
            // 
            this.byLine.AutoSize = true;
            this.byLine.Checked = true;
            this.byLine.Location = new System.Drawing.Point(27, 20);
            this.byLine.Name = "byLine";
            this.byLine.Size = new System.Drawing.Size(71, 16);
            this.byLine.TabIndex = 0;
            this.byLine.TabStop = true;
            this.byLine.Text = "按行分割";
            this.byLine.UseVisualStyleBackColor = true;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(12, 107);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(98, 30);
            this.confirm.TabIndex = 1;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(229, 107);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(100, 30);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // LoadTranslationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 160);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.groupBox1);
            this.Name = "LoadTranslationForm";
            this.Text = "导入翻译稿";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton byBlank;
        private System.Windows.Forms.RadioButton byLine;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}