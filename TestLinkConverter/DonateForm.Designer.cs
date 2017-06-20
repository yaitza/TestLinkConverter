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
            this.SuspendLayout();
            // 
            // alipayp
            // 
            this.alipayp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("alipayp.BackgroundImage")));
            this.alipayp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.alipayp.Location = new System.Drawing.Point(12, 101);
            this.alipayp.Name = "alipayp";
            this.alipayp.Size = new System.Drawing.Size(185, 205);
            this.alipayp.TabIndex = 1;
            // 
            // alipayicop
            // 
            this.alipayicop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("alipayicop.BackgroundImage")));
            this.alipayicop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.alipayicop.Location = new System.Drawing.Point(28, 21);
            this.alipayicop.Name = "alipayicop";
            this.alipayicop.Size = new System.Drawing.Size(141, 60);
            this.alipayicop.TabIndex = 2;
            // 
            // wechaticop
            // 
            this.wechaticop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wechaticop.BackgroundImage")));
            this.wechaticop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wechaticop.Location = new System.Drawing.Point(239, 21);
            this.wechaticop.Name = "wechaticop";
            this.wechaticop.Size = new System.Drawing.Size(172, 60);
            this.wechaticop.TabIndex = 3;
            // 
            // wechat
            // 
            this.wechat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wechat.BackgroundImage")));
            this.wechat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wechat.Location = new System.Drawing.Point(228, 101);
            this.wechat.Name = "wechat";
            this.wechat.Size = new System.Drawing.Size(211, 205);
            this.wechat.TabIndex = 4;
            // 
            // DonateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 318);
            this.Controls.Add(this.wechat);
            this.Controls.Add(this.wechaticop);
            this.Controls.Add(this.alipayicop);
            this.Controls.Add(this.alipayp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DonateForm";
            this.Text = "打赏(Donate)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel alipayp;
        private System.Windows.Forms.Panel alipayicop;
        private System.Windows.Forms.Panel wechaticop;
        private System.Windows.Forms.Panel wechat;
    }
}