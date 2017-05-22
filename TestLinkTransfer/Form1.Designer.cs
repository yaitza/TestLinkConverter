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
            this.filePathTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xeRb = new System.Windows.Forms.RadioButton();
            this.exRb = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // getFilePathBtn
            // 
            this.getFilePathBtn.Location = new System.Drawing.Point(359, 58);
            this.getFilePathBtn.Name = "getFilePathBtn";
            this.getFilePathBtn.Size = new System.Drawing.Size(46, 23);
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
            // filePathTb
            // 
            this.filePathTb.Location = new System.Drawing.Point(76, 60);
            this.filePathTb.Name = "filePathTb";
            this.filePathTb.Size = new System.Drawing.Size(277, 21);
            this.filePathTb.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件路径：";
            // 
            // xeRb
            // 
            this.xeRb.AutoSize = true;
            this.xeRb.Checked = true;
            this.xeRb.Location = new System.Drawing.Point(80, 19);
            this.xeRb.Name = "xeRb";
            this.xeRb.Size = new System.Drawing.Size(95, 16);
            this.xeRb.TabIndex = 3;
            this.xeRb.TabStop = true;
            this.xeRb.Text = "XML -> Excel";
            this.xeRb.UseVisualStyleBackColor = true;
            // 
            // exRb
            // 
            this.exRb.AutoSize = true;
            this.exRb.Location = new System.Drawing.Point(197, 19);
            this.exRb.Name = "exRb";
            this.exRb.Size = new System.Drawing.Size(95, 16);
            this.exRb.TabIndex = 4;
            this.exRb.Text = "Excel -> XML";
            this.exRb.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "转换形式：";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(359, 106);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 106);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(339, 23);
            this.progressBar.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 262);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exRb);
            this.Controls.Add(this.xeRb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filePathTb);
            this.Controls.Add(this.getFilePathBtn);
            this.Name = "Form1";
            this.Text = "TestLinkTransfer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getFilePathBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox filePathTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton xeRb;
        private System.Windows.Forms.RadioButton exRb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

