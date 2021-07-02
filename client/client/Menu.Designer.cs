
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.startStreamBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMaxWatchers = new System.Windows.Forms.TextBox();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startStreamBtn
            // 
            this.startStreamBtn.Location = new System.Drawing.Point(558, 124);
            this.startStreamBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startStreamBtn.Name = "startStreamBtn";
            this.startStreamBtn.Size = new System.Drawing.Size(190, 95);
            this.startStreamBtn.TabIndex = 0;
            this.startStreamBtn.Text = "START STREAMING";
            this.startStreamBtn.UseVisualStyleBackColor = true;
            this.startStreamBtn.Click += new System.EventHandler(this.startStreamBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(558, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter max number of watchers";
            // 
            // tbMaxWatchers
            // 
            this.tbMaxWatchers.Location = new System.Drawing.Point(558, 77);
            this.tbMaxWatchers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbMaxWatchers.Name = "tbMaxWatchers";
            this.tbMaxWatchers.Size = new System.Drawing.Size(190, 23);
            this.tbMaxWatchers.TabIndex = 2;
            // 
            // refreshBtn
            // 
            this.refreshBtn.Image = ((System.Drawing.Image)(resources.GetObject("refreshBtn.Image")));
            this.refreshBtn.Location = new System.Drawing.Point(558, 9);
            this.refreshBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(35, 28);
            this.refreshBtn.TabIndex = 3;
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(598, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Refresh streamer list";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 348);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.tbMaxWatchers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startStreamBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Label label2;
    }
}