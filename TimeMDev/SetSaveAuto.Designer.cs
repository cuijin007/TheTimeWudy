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
            this.cancel = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.choosePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.count = new System.Windows.Forms.TextBox();
            this.functionTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chooseBufPath = new System.Windows.Forms.Button();
            this.pathBuf = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(336, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自动保存设置";
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(274, 324);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(12, 324);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(75, 23);
            this.confirm.TabIndex = 5;
            this.confirm.Text = "确认";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
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
            // count
            // 
            this.count.Location = new System.Drawing.Point(66, 124);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(58, 21);
            this.count.TabIndex = 1;
            // 
            // functionTime
            // 
            this.functionTime.Location = new System.Drawing.Point(66, 33);
            this.functionTime.Name = "functionTime";
            this.functionTime.Size = new System.Drawing.Size(58, 21);
            this.functionTime.TabIndex = 1;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "每执行";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chooseBufPath);
            this.groupBox2.Controls.Add(this.pathBuf);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(13, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 75);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "缓存路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "路径";
            // 
            // chooseBufPath
            // 
            this.chooseBufPath.Location = new System.Drawing.Point(261, 31);
            this.chooseBufPath.Name = "chooseBufPath";
            this.chooseBufPath.Size = new System.Drawing.Size(35, 23);
            this.chooseBufPath.TabIndex = 5;
            this.chooseBufPath.Text = "……";
            this.chooseBufPath.UseVisualStyleBackColor = true;
            this.chooseBufPath.Click += new System.EventHandler(this.chooseBufPath_Click);
            // 
            // pathBuf
            // 
            this.pathBuf.Location = new System.Drawing.Point(66, 31);
            this.pathBuf.Name = "pathBuf";
            this.pathBuf.ReadOnly = true;
            this.pathBuf.Size = new System.Drawing.Size(189, 21);
            this.pathBuf.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(13, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(336, 59);
            this.label6.TabIndex = 7;
            this.label6.Text = "特别说明：如果你只有c盘，那么将文件路径选择至                                                          " +
                "        库->文档->我的文档";
            // 
            // SetSaveAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 371);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.confirm);
            this.Name = "SetSaveAuto";
            this.Text = "自动保存设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button chooseBufPath;
        private System.Windows.Forms.TextBox pathBuf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}