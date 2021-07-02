
namespace client
{
    partial class Streamer
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
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.viewLab = new System.Windows.Forms.Label();
            this.logLab = new System.Windows.Forms.Label();
            this.msgBox = new System.Windows.Forms.RichTextBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.btChat = new System.Windows.Forms.Button();
            this.btScreenShot = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVideo.Location = new System.Drawing.Point(14, 16);
            this.pbVideo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(737, 701);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            // 
            // stopBtn
            // 
            this.stopBtn.BackColor = System.Drawing.Color.Red;
            this.stopBtn.FlatAppearance.BorderSize = 0;
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.ForeColor = System.Drawing.Color.White;
            this.stopBtn.Location = new System.Drawing.Point(655, 725);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(96, 32);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "stop";
            this.stopBtn.UseVisualStyleBackColor = false;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 725);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "viewers:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 757);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "log:";
            // 
            // viewLab
            // 
            this.viewLab.AutoSize = true;
            this.viewLab.Location = new System.Drawing.Point(78, 725);
            this.viewLab.Name = "viewLab";
            this.viewLab.Size = new System.Drawing.Size(15, 20);
            this.viewLab.TabIndex = 4;
            this.viewLab.Text = "/";
            // 
            // logLab
            // 
            this.logLab.AutoSize = true;
            this.logLab.Location = new System.Drawing.Point(78, 757);
            this.logLab.Name = "logLab";
            this.logLab.Size = new System.Drawing.Size(15, 20);
            this.logLab.TabIndex = 5;
            this.logLab.Text = "/";
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(770, 16);
            this.msgBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(318, 700);
            this.msgBox.TabIndex = 6;
            this.msgBox.Text = "";
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(770, 725);
            this.tbChat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(226, 27);
            this.tbChat.TabIndex = 7;
            // 
            // btChat
            // 
            this.btChat.Location = new System.Drawing.Point(1003, 725);
            this.btChat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btChat.Name = "btChat";
            this.btChat.Size = new System.Drawing.Size(86, 31);
            this.btChat.TabIndex = 8;
            this.btChat.Text = "send";
            this.btChat.UseVisualStyleBackColor = true;
            this.btChat.Click += new System.EventHandler(this.btChat_Click);
            // 
            // btScreenShot
            // 
            this.btScreenShot.Location = new System.Drawing.Point(655, 764);
            this.btScreenShot.Name = "btScreenShot";
            this.btScreenShot.Size = new System.Drawing.Size(96, 32);
            this.btScreenShot.TabIndex = 9;
            this.btScreenShot.Text = "screenshot";
            this.btScreenShot.UseVisualStyleBackColor = true;
            this.btScreenShot.Click += new System.EventHandler(this.btScreenShot_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(770, 764);
            this.hScrollBar1.Maximum = 75;
            this.hScrollBar1.Minimum = -75;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(226, 18);
            this.hScrollBar1.TabIndex = 10;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1003, 764);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "brightness:";
            // 
            // Streamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 800);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.btScreenShot);
            this.Controls.Add(this.btChat);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.logLab);
            this.Controls.Add(this.viewLab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.pbVideo);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Streamer";
            this.Text = "/";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label viewLab;
        private System.Windows.Forms.Label logLab;
        private System.Windows.Forms.RichTextBox msgBox;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.Button btChat;
        private System.Windows.Forms.Button btScreenShot;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label3;
    }
}