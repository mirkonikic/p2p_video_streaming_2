
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
            this.stopBtn = new System.Windows.Forms.Button();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.btChat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.logLab = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(573, 544);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(84, 24);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbVideo.Location = new System.Drawing.Point(11, 8);
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
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log:";
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
            this.button1.Location = new System.Drawing.Point(484, 544);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 24);
            this.button1.TabIndex = 8;
            this.button1.Text = "screenshot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Watcher
            // 
            this.AccessibleName = "tbData";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 595);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbVideo);
            this.Controls.Add(this.logLab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btChat);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.stopBtn);
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
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.Button btChat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label logLab;
        private System.Windows.Forms.Button button1;
    }
}