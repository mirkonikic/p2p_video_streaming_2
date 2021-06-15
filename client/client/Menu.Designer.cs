
namespace client
{
    partial class Menu
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
            this.SuspendLayout();
            // 
            // startStreamBtn
            // 
            this.startStreamBtn.Location = new System.Drawing.Point(561, 101);
            this.startStreamBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startStreamBtn.Name = "startStreamBtn";
            this.startStreamBtn.Size = new System.Drawing.Size(190, 95);
            this.startStreamBtn.TabIndex = 0;
            this.startStreamBtn.Text = "START STREAMING";
            this.startStreamBtn.UseVisualStyleBackColor = true;
            this.startStreamBtn.Click += new System.EventHandler(this.startStreamBtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 348);
            this.Controls.Add(this.startStreamBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "P2P Video Streaming";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStreamBtn;
    }
}