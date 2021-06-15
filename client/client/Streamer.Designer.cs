
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
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVideo
            // 
            this.pbVideo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVideo.Location = new System.Drawing.Point(12, 12);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(645, 526);
            this.pbVideo.TabIndex = 0;
            this.pbVideo.TabStop = false;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(573, 544);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(84, 24);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Viewers:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 568);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Log:";
            // 
            // viewLab
            // 
            this.viewLab.AutoSize = true;
            this.viewLab.Location = new System.Drawing.Point(68, 544);
            this.viewLab.Name = "viewLab";
            this.viewLab.Size = new System.Drawing.Size(12, 15);
            this.viewLab.TabIndex = 4;
            this.viewLab.Text = "/";
            // 
            // logLab
            // 
            this.logLab.AutoSize = true;
            this.logLab.Location = new System.Drawing.Point(68, 568);
            this.logLab.Name = "logLab";
            this.logLab.Size = new System.Drawing.Size(12, 15);
            this.logLab.TabIndex = 5;
            this.logLab.Text = "/";
            // 
            // Streamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 595);
            this.Controls.Add(this.logLab);
            this.Controls.Add(this.viewLab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.pbVideo);
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
    }
}