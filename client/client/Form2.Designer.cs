
namespace client
{
    partial class Form2
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
            this.startStreamBtn = new System.Windows.Forms.Button();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.stopBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // startStreamBtn
            // 
            this.startStreamBtn.Location = new System.Drawing.Point(641, 135);
            this.startStreamBtn.Name = "startStreamBtn";
            this.startStreamBtn.Size = new System.Drawing.Size(217, 127);
            this.startStreamBtn.TabIndex = 0;
            this.startStreamBtn.Text = "START STREAMING";
            this.startStreamBtn.UseVisualStyleBackColor = true;
            this.startStreamBtn.Click += new System.EventHandler(this.startStreamBtn_Click);
            // 
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(2, 2);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(876, 390);
            this.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVideo.TabIndex = 1;
            this.pbVideo.TabStop = false;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(362, 410);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(176, 42);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "STOP STREAMING";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 464);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.pbVideo);
            this.Controls.Add(this.startStreamBtn);
            this.Name = "Form2";
            this.Text = "P2P Video Streaming";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStreamBtn;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.Button stopBtn;
    }
}