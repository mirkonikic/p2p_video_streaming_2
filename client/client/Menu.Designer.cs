
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbMaxWatchers = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startStreamBtn
            // 
            this.startStreamBtn.Location = new System.Drawing.Point(638, 166);
            this.startStreamBtn.Name = "startStreamBtn";
            this.startStreamBtn.Size = new System.Drawing.Size(217, 127);
            this.startStreamBtn.TabIndex = 0;
            this.startStreamBtn.Text = "START STREAMING";
            this.startStreamBtn.UseVisualStyleBackColor = true;
            this.startStreamBtn.Click += new System.EventHandler(this.startStreamBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter max number of watchers";
            // 
            // tbMaxWatchers
            // 
            this.tbMaxWatchers.Location = new System.Drawing.Point(638, 103);
            this.tbMaxWatchers.Name = "tbMaxWatchers";
            this.tbMaxWatchers.Size = new System.Drawing.Size(217, 27);
            this.tbMaxWatchers.TabIndex = 2;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 464);
            this.Controls.Add(this.tbMaxWatchers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startStreamBtn);
            this.Name = "Menu";
            this.Text = "P2P Video Streaming";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startStreamBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMaxWatchers;
    }
}