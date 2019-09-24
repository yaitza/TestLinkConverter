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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonateForm));
            this.wechat = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.alipayp = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // wechat
            // 
            this.wechat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wechat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wechat.BackgroundImage")));
            this.wechat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wechat.Location = new System.Drawing.Point(302, 13);
            this.wechat.Margin = new System.Windows.Forms.Padding(4);
            this.wechat.Name = "wechat";
            this.wechat.Size = new System.Drawing.Size(279, 369);
            this.wechat.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "如有问题，请联系yaitza@foxmail.com；或者Github留言，将第一时间回复；谢谢！";
            // 
            // alipayp
            // 
            this.alipayp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.alipayp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("alipayp.BackgroundImage")));
            this.alipayp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.alipayp.Location = new System.Drawing.Point(3, 13);
            this.alipayp.Margin = new System.Windows.Forms.Padding(4);
            this.alipayp.Name = "alipayp";
            this.alipayp.Size = new System.Drawing.Size(279, 369);
            this.alipayp.TabIndex = 1;
            // 
            // DonateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 453);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wechat);
            this.Controls.Add(this.alipayp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DonateForm";
            this.Text = "打赏(Donate)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel wechat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel alipayp;
    }
}