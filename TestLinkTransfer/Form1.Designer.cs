namespace TestLinkTransfer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.getFilePathBtn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.filePathtB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // getFilePathBtn
            // 
            this.getFilePathBtn.Location = new System.Drawing.Point(166, 131);
            this.getFilePathBtn.Name = "getFilePathBtn";
            this.getFilePathBtn.Size = new System.Drawing.Size(75, 23);
            this.getFilePathBtn.TabIndex = 0;
            this.getFilePathBtn.Text = "...";
            this.getFilePathBtn.UseVisualStyleBackColor = true;
            this.getFilePathBtn.Click += new System.EventHandler(this.getFilePathBtn_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "xml(*.xml)|*.xml|Excel 2003(*.xls)|*.xls|Excel 2007Plus(*.xlsx)|*.xlsx";
            // 
            // filePathtB
            // 
            this.filePathtB.Location = new System.Drawing.Point(4, 60);
            this.filePathtB.Name = "filePathtB";
            this.filePathtB.Size = new System.Drawing.Size(277, 21);
            this.filePathtB.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.filePathtB);
            this.Controls.Add(this.getFilePathBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getFilePathBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePathtB;
    }
}

