namespace TimeMDev
{
    partial class FindForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.caseSensitiveCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.upWayRadio = new System.Windows.Forms.RadioButton();
            this.downWayRadio = new System.Windows.Forms.RadioButton();
            this.findNextButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(83, 24);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(271, 21);
            this.searchBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找内容：";
            // 
            // caseSensitiveCheck
            // 
            this.caseSensitiveCheck.AutoSize = true;
            this.caseSensitiveCheck.Checked = true;
            this.caseSensitiveCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.caseSensitiveCheck.Location = new System.Drawing.Point(14, 71);
            this.caseSensitiveCheck.Name = "caseSensitiveCheck";
            this.caseSensitiveCheck.Size = new System.Drawing.Size(84, 16);
            this.caseSensitiveCheck.TabIndex = 2;
            this.caseSensitiveCheck.Text = "区分大小写";
            this.caseSensitiveCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.downWayRadio);
            this.groupBox1.Controls.Add(this.upWayRadio);
            this.groupBox1.Location = new System.Drawing.Point(104, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            // 
            // upWayRadio
            // 
            this.upWayRadio.AutoSize = true;
            this.upWayRadio.Location = new System.Drawing.Point(7, 21);
            this.upWayRadio.Name = "upWayRadio";
            this.upWayRadio.Size = new System.Drawing.Size(47, 16);
            this.upWayRadio.TabIndex = 0;
            this.upWayRadio.TabStop = true;
            this.upWayRadio.Text = "向上";
            this.upWayRadio.UseVisualStyleBackColor = true;
            // 
            // downWayRadio
            // 
            this.downWayRadio.AutoSize = true;
            this.downWayRadio.Checked = true;
            this.downWayRadio.Location = new System.Drawing.Point(93, 20);
            this.downWayRadio.Name = "downWayRadio";
            this.downWayRadio.Size = new System.Drawing.Size(47, 16);
            this.downWayRadio.TabIndex = 0;
            this.downWayRadio.TabStop = true;
            this.downWayRadio.Text = "向下";
            this.downWayRadio.UseVisualStyleBackColor = true;
            // 
            // findNextButton
            // 
            this.findNextButton.Location = new System.Drawing.Point(269, 74);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(104, 23);
            this.findNextButton.TabIndex = 4;
            this.findNextButton.Text = "查找下一个";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.findNextButton_Click);
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 120);
            this.Controls.Add(this.findNextButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.caseSensitiveCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FindForm";
            this.Text = "查找";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox caseSensitiveCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton downWayRadio;
        private System.Windows.Forms.RadioButton upWayRadio;
        private System.Windows.Forms.Button findNextButton;
    }
}