
namespace TimeMDev
{
    partial class ShortCutSettingsForm
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
            this.keyInp = new System.Windows.Forms.TextBox();
            this.okBtn = new DevExpress.XtraEditors.SimpleButton();
            this.shortCutsLst = new DevExpress.XtraEditors.ListBoxControl();
            this.saveBtn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.shortCutsLst)).BeginInit();
            this.SuspendLayout();
            // 
            // keyInp
            // 
            this.keyInp.Location = new System.Drawing.Point(12, 276);
            this.keyInp.Name = "keyInp";
            this.keyInp.ReadOnly = true;
            this.keyInp.Size = new System.Drawing.Size(239, 21);
            this.keyInp.TabIndex = 0;
            this.keyInp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_KeyDown);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(257, 273);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "设置";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // shortCutsLst
            // 
            this.shortCutsLst.Location = new System.Drawing.Point(12, 12);
            this.shortCutsLst.Name = "shortCutsLst";
            this.shortCutsLst.Size = new System.Drawing.Size(401, 256);
            this.shortCutsLst.TabIndex = 4;
            this.shortCutsLst.SelectedIndexChanged += new System.EventHandler(this.shortCutsLst_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(338, 273);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "保存";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // ShortCutSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 309);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.shortCutsLst);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.keyInp);
            this.Name = "ShortCutSettingsForm";
            this.Text = "ShortCutSettings";
            this.Load += new System.EventHandler(this.ShortCutSettingsForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShortCutSettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.shortCutsLst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyInp;
        private DevExpress.XtraEditors.SimpleButton okBtn;
        private DevExpress.XtraEditors.ListBoxControl shortCutsLst;
        private DevExpress.XtraEditors.SimpleButton saveBtn;

     

    }
}