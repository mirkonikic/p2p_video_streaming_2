using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client

{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        BinaryReader serverInput;
        BinaryWriter serverOutput;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Capture.png");
            pictureBox1.Image = (Image)(new Bitmap(image, new Size(400, 450)));


            client = new TcpClient("127.0.0.1", 9090);
            stream = client.GetStream();
            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = tbUsername.Text;
            string password = tbPassword.Text;

            string message = "USER " + username + " " + password;
            serverOutput.Write(message);

            string inputMessage = serverInput.ReadString();
            string[] inputSplit = inputMessage.Split(null);
            string code = inputSplit[0];

            
            if (code == "200")
            {
                this.Hide();
                var form2 = new Form2(stream, username);
                form2.Closed += (s, args) => this.Close();
                form2.Show();

                serverOutput.Flush();
            }

        }

        
    }

}

