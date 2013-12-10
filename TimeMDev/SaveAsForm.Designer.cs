namespace TimeMDev
{
    partial class SaveAsForm
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
            this.comboEncodingChoose = new System.Windows.Forms.ComboBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboEncodingChoose
            // 
            this.comboEncodingChoose.FormattingEnabled = true;
            this.comboEncodingChoose.Items.AddRange(new object[] {
            "ANSI",
            "UNICODE",
            "UTF-8"});
            this.comboEncodingChoose.Location = new System.Drawing.Point(12, 43);
            this.comboEncodingChoose.Name = "comboEncodingChoose";
            this.comboEncodingChoose.Size = new System.Drawing.Size(259, 20);
            this.comboEncodingChoose.TabIndex = 0;
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(13, 100);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 1;
            this.continueButton.Text = "确认";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(197, 100);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "编码类型";
            // 
            // SaveAsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 158);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.comboEncodingChoose);
            this.Name = "SaveAsForm";
            this.Text = "SaveAsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboEncodingChoose;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
    }
}