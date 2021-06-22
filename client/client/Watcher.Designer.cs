
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
            this.tbVideo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(375, 444);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(70, 48);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "STOP";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // tbVideo
            // 
            this.tbVideo.Location = new System.Drawing.Point(13, 11);
            this.tbVideo.Multiline = true;
            this.tbVideo.Name = "tbVideo";
            this.tbVideo.Size = new System.Drawing.Size(815, 399);
            this.tbVideo.TabIndex = 2;
            // 
            // Watcher
            // 
            this.AccessibleName = "tbData";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 506);
            this.Controls.Add(this.tbVideo);
            this.Controls.Add(this.stopBtn);
            this.Name = "Watcher";
            this.Text = "Watcher";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.TextBox tbVideo;
    }
}