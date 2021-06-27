
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
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
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
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(7, 8);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(821, 424);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 2;
            this.pbVideo.TabStop = false;
            // 
            // Watcher
            // 
            this.AccessibleName = "tbData";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 506);
            this.Controls.Add(this.pbVideo);
            this.Controls.Add(this.stopBtn);
            this.Name = "Watcher";
            this.Text = "Watcher";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.PictureBox pbVideo;
    }
}