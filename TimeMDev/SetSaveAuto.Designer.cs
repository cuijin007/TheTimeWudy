namespace TimeMDev
{
    partial class SetSaveAuto
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.functionTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.choosePath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.count = new System.Windows.Forms.TextBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancel);
            this.groupBox1.Controls.Add(this.confirm);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.choosePath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.path);
            this.groupBox1.Controls.Add(this.count);
            this.groupBox1.Controls.Add(this.functionTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自动保存设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "每执行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "次有效操作自动保存一次";
            // 
            // functionTime
            // 
            this.functionTime.Location = new System.Drawing.Point(66, 33);
            this.functionTime.Name = "functionTime";
            this.functionTime.Size = new System.Drawing.Size(58, 21);
            this.functionTime.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "路径";
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(66, 77);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(189, 21);
            this.path.TabIndex = 1;
            // 
            // choosePath
            // 
            this.choosePath.Location = new System.Drawing.Point(261, 77);
            this.choosePath.Name = "choosePath";
            this.choosePath.Size = new System.Drawing.Size(35, 23);
            this.choosePath.TabIndex = 3;
            this.choosePath.Text = "……";
            this.choosePath.UseVisualStyleBackColor = true;
            this.choosePath.Click += new System.EventHandler(this.choosePath_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "份数";
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(66, 124);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(58, 21);
            this.count.TabIndex = 1;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(21, 159);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 5;
            this.confirm.Text = "确认";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(238, 159);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SetSaveAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 219);
            this.Controls.Add(this.groupBox1);
            this.Name = "SetSaveAuto";
            this.Text = "自动保存设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox functionTime;
        private System.Windows.Forms.Button choosePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button confirm;
    }
}