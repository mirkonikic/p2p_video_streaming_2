
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
            this.label4 = new System.Windows.Forms.Label();
            this.seqLab = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.payLenLab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbVideo.Location = new System.Drawing.Point(12, 8);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(645, 526);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 2;
            this.pbVideo.TabStop = false;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(674, 8);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(279, 526);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(674, 544);
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(198, 23);
            this.tbChat.TabIndex = 4;
            // 
            // btChat
            // 
            this.btChat.Location = new System.Drawing.Point(878, 543);
            this.btChat.Name = "btChat";
            this.btChat.Size = new System.Drawing.Size(75, 23);
            this.btChat.TabIndex = 5;
            this.btChat.Text = "send";
            this.btChat.UseVisualStyleBackColor = true;
            this.btChat.Click += new System.EventHandler(this.btChat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 547);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "log:";
            // 
            // logLab
            // 
            this.logLab.AutoSize = true;
            this.logLab.Location = new System.Drawing.Point(48, 547);
            this.logLab.Name = "logLab";
            this.logLab.Size = new System.Drawing.Size(0, 15);
            this.logLab.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(577, 543);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "screenshot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 571);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "viewers:";
            // 
            // viewLab
            // 
            this.viewLab.AutoSize = true;
            this.viewLab.Location = new System.Drawing.Point(71, 570);
            this.viewLab.Name = "viewLab";
            this.viewLab.Size = new System.Drawing.Size(0, 15);
            this.viewLab.TabIndex = 10;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(498, 543);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(73, 23);
            this.stopBtn.TabIndex = 11;
            this.stopBtn.Text = "stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 547);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "seq_number:";
            // 
            // seqLab
            // 
            this.seqLab.AutoSize = true;
            this.seqLab.Location = new System.Drawing.Point(377, 547);
            this.seqLab.Name = "seqLab";
            this.seqLab.Size = new System.Drawing.Size(0, 15);
            this.seqLab.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 567);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "payload_len:";
            // 
            // payLenLab
            // 
            this.payLenLab.AutoSize = true;
            this.payLenLab.Location = new System.Drawing.Point(375, 567);
            this.payLenLab.Name = "payLenLab";
            this.payLenLab.Size = new System.Drawing.Size(0, 15);
            this.payLenLab.TabIndex = 15;
            // 
            // Watcher
            // 
            this.AccessibleName = "tbData";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 592);
            this.Controls.Add(this.payLenLab);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.seqLab);
            this.Controls.Add(this.label4);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label seqLab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label payLenLab;
    }
}