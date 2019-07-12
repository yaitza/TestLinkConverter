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
            this.alipayp = new System.Windows.Forms.Panel();
            this.alipayicop = new System.Windows.Forms.Panel();
            this.wechaticop = new System.Windows.Forms.Panel();
            this.wechat = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // alipayp
            // 
            this.alipayp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("alipayp.BackgroundImage")));
            this.alipayp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.alipayp.Location = new System.Drawing.Point(16, 126);
            this.alipayp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.alipayp.Name = "alipayp";
            this.alipayp.Size = new System.Drawing.Size(247, 256);
            this.alipayp.TabIndex = 1;
            // 
            // alipayicop
            // 
            this.alipayicop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("alipayicop.BackgroundImage")));
            this.alipayicop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.alipayicop.Location = new System.Drawing.Point(37, 26);
            this.alipayicop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.alipayicop.Name = "alipayicop";
            this.alipayicop.Size = new System.Drawing.Size(188, 75);
            this.alipayicop.TabIndex = 2;
            // 
            // wechaticop
            // 
            this.wechaticop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wechaticop.BackgroundImage")));
            this.wechaticop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wechaticop.Location = new System.Drawing.Point(319, 26);
            this.wechaticop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.wechaticop.Name = "wechaticop";
            this.wechaticop.Size = new System.Drawing.Size(229, 75);
            this.wechaticop.TabIndex = 3;
            // 
            // wechat
            // 
            this.wechat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wechat.BackgroundImage")));
            this.wechat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wechat.Location = new System.Drawing.Point(304, 126);
            this.wechat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.wechat.Name = "wechat";
            this.wechat.Size = new System.Drawing.Size(281, 256);
            this.wechat.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "如有问题，请联系yaitza@foxmail.com；或者Github留言，将第一时间回复；谢谢！";
            // 
            // DonateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 453);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wechat);
            this.Controls.Add(this.wechaticop);
            this.Controls.Add(this.alipayicop);
            this.Controls.Add(this.alipayp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DonateForm";
            this.Text = "打赏(Donate)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel alipayp;
        private System.Windows.Forms.Panel alipayicop;
        private System.Windows.Forms.Panel wechaticop;
        private System.Windows.Forms.Panel wechat;
        private System.Windows.Forms.Label label1;
    }
}