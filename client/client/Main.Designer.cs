
namespace client
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "USERNAME";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(12, 26);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(182, 23);
            this.tbUsername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "PASSWORD";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(14, 84);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(180, 23);
            this.tbPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(191, 53);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(88, 27);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(292, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(332, 339);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "ip_address";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(105, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 23);
            this.textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(105, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 23);
            this.textBox2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 234);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 92);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "tracker_settings";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 338);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.Text = "Login Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

