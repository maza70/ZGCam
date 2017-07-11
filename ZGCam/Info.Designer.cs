namespace ZGCam
{
    partial class Info
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
            this.PBIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LbVersion = new System.Windows.Forms.Label();
            this.RtbInfo = new System.Windows.Forms.RichTextBox();
            this.BtnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PBIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // PBIcon
            // 
            this.PBIcon.Image = global::ZGCam.Properties.Resources.Icon_64;
            this.PBIcon.Location = new System.Drawing.Point(12, 12);
            this.PBIcon.Name = "PBIcon";
            this.PBIcon.Size = new System.Drawing.Size(73, 64);
            this.PBIcon.TabIndex = 0;
            this.PBIcon.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(109, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "ZGCam © 2017  Matthias Zartmann ";
            // 
            // LbVersion
            // 
            this.LbVersion.AutoSize = true;
            this.LbVersion.Location = new System.Drawing.Point(110, 50);
            this.LbVersion.Name = "LbVersion";
            this.LbVersion.Size = new System.Drawing.Size(63, 13);
            this.LbVersion.TabIndex = 3;
            this.LbVersion.Text = "Version: 1,0";
            // 
            // RtbInfo
            // 
            this.RtbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RtbInfo.Location = new System.Drawing.Point(13, 79);
            this.RtbInfo.Name = "RtbInfo";
            this.RtbInfo.Size = new System.Drawing.Size(571, 236);
            this.RtbInfo.TabIndex = 8;
            this.RtbInfo.Text = "";
            this.RtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.RtbInfo_LinkClicked);
            this.RtbInfo.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Location = new System.Drawing.Point(458, 321);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(126, 23);
            this.BtnClose.TabIndex = 9;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // Info
            // 
            this.AcceptButton = this.BtnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 351);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.RtbInfo);
            this.Controls.Add(this.LbVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Info";
            this.ShowIcon = false;
            this.Text = "ZGCam Info";
            this.Load += new System.EventHandler(this.Info_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PBIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbVersion;
        private System.Windows.Forms.RichTextBox RtbInfo;
        private System.Windows.Forms.Button BtnClose;
    }
}