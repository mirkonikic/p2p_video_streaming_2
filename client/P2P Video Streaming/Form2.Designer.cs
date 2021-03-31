
namespace P2P_Video_Streaming
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
            this.SuspendLayout();
            // 
            // startStreamBtn
            // 
            this.startStreamBtn.Location = new System.Drawing.Point(619, 122);
            this.startStreamBtn.Name = "startStreamBtn";
            this.startStreamBtn.Size = new System.Drawing.Size(206, 119);
            this.startStreamBtn.TabIndex = 0;
            this.startStreamBtn.Text = "START STREAMING";
            this.startStreamBtn.UseVisualStyleBackColor = true;
            this.startStreamBtn.Click += new System.EventHandler(this.startStreamBtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 464);
            this.Controls.Add(this.startStreamBtn);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStreamBtn;
    }
}