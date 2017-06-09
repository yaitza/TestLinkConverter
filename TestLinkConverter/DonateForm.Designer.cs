namespace TestLinkConverter
{
    partial class DonateForm
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
            this.alipyPB = new System.Windows.Forms.PictureBox();
            this.tencentPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.alipyPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tencentPB)).BeginInit();
            this.SuspendLayout();
            // 
            // alipyPB
            // 
            this.alipyPB.Location = new System.Drawing.Point(34, 46);
            this.alipyPB.Name = "alipyPB";
            this.alipyPB.Size = new System.Drawing.Size(100, 50);
            this.alipyPB.TabIndex = 0;
            this.alipyPB.TabStop = false;
            // 
            // tencentPB
            // 
            this.tencentPB.Location = new System.Drawing.Point(34, 141);
            this.tencentPB.Name = "tencentPB";
            this.tencentPB.Size = new System.Drawing.Size(100, 50);
            this.tencentPB.TabIndex = 1;
            this.tencentPB.TabStop = false;
            // 
            // DonateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tencentPB);
            this.Controls.Add(this.alipyPB);
            this.Name = "DonateForm";
            this.Text = "打赏(Donate)";
            ((System.ComponentModel.ISupportInitialize)(this.alipyPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tencentPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox alipyPB;
        private System.Windows.Forms.PictureBox tencentPB;
    }
}