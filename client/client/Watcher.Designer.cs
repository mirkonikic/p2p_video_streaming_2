﻿
namespace client
{
    partial class Watcher
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
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.btChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.logLab = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.viewLab = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbVideo.Location = new System.Drawing.Point(14, 13);
            this.pbVideo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(737, 698);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 2;
            this.pbVideo.TabStop = false;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(770, 13);
            this.chatBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(318, 698);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(770, 725);
            this.tbChat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(226, 27);
            this.tbChat.TabIndex = 4;
            // 
            // btChat
            // 
            this.btChat.Location = new System.Drawing.Point(1002, 723);
            this.btChat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btChat.Name = "btChat";
            this.btChat.Size = new System.Drawing.Size(86, 31);
            this.btChat.TabIndex = 5;
            this.btChat.Text = "send";
            this.btChat.UseVisualStyleBackColor = true;
            this.btChat.Click += new System.EventHandler(this.btChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 729);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log:";
            // 
            // logLab
            // 
            this.logLab.AutoSize = true;
            this.logLab.Location = new System.Drawing.Point(55, 729);
            this.logLab.Name = "logLab";
            this.logLab.Size = new System.Drawing.Size(0, 20);
            this.logLab.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(656, 758);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "screenshot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 761);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Viewers:";
            // 
            // viewLab
            // 
            this.viewLab.AutoSize = true;
            this.viewLab.Location = new System.Drawing.Point(81, 760);
            this.viewLab.Name = "viewLab";
            this.viewLab.Size = new System.Drawing.Size(0, 20);
            this.viewLab.TabIndex = 10;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(656, 721);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(95, 31);
            this.stopBtn.TabIndex = 11;
            this.stopBtn.Text = "stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Watcher
            // 
            this.AccessibleName = "tbData";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 794);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.viewLab);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbVideo);
            this.Controls.Add(this.logLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btChat);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.chatBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Watcher";
            this.Text = "Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.Button btChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label logLab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label viewLab;
        private System.Windows.Forms.Button stopBtn;
    }
}