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
using Microsoft.VisualBasic;

namespace P2P_Video_Streaming
{
    public partial class Form2 : Form
    {

        NetworkStream stream;
        string username;
        BinaryReader serverInput;
        BinaryWriter serverOutput;
        public Form2(NetworkStream stream, string username)
        {
            InitializeComponent();
            this.stream = stream;
            this.username = username;
        }


        private void Form2_Load(object sender, EventArgs e)
        {

            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);

            serverOutput.Write("LIST");

            string strm = serverInput.ReadString();
            string[] code_streamers = strm.Split(null);
            string code = code_streamers[0];

            Label mylab = new Label();
            Button button = new Button();
            mylab.AutoSize = true;
            mylab.ForeColor = Color.Black;
            mylab.Font = new Font("Calibri", 10);

            if (code == "201")
            {
                string[] streamers = code_streamers[1].Split(';');
                string streamer = "";

                int i = 1;
                int j = 0;
                int counter = 0;


                foreach (string streamer_info in streamers)
                {
                    string[] info = streamer_info.Split(':');
                    string[] title_split = info[1].Split('_');
                    string title = "";

                    foreach (string title_info in title_split)
                    {
                        title += title_info + " ";
                    }

                    streamer = $"streamer{i}: {info[0]}    title: {title}";
                    
                    mylab.Text = streamer;

                    button.Text = "PLAY";
                    button.AutoSize = true;

                    if (counter < 1)
                    {
                        mylab.Location = new Point(12, mylab.Location.Y + mylab.Height);
                        button.Location = new Point(mylab.Width + 150, mylab.Location.Y);
                    }
                    else
                    {
                        mylab.Location = new Point(12, mylab.Location.Y + mylab.Height + j);
                        button.Location = new Point(mylab.Width + 150, mylab.Location.Y);
                    }

                    this.Controls.Add(mylab);
                    this.Controls.Add(button);

                    i++;
                    j += 30;
                    counter++;
                }
            } else if (code == "408")
            {
                mylab.Text = "No online streamers found";
                mylab.Location = new Point(12, mylab.Location.Y + mylab.Height);
                this.Controls.Add(mylab);
            }

        }

        private void startStreamBtn_Click(object sender, EventArgs e)
        {
            string titleName = Microsoft.VisualBasic.Interaction.InputBox("Enter title of your stream", "Title", "");
            string message = $"STRM {username} {titleName}";
            serverOutput.Write(message);
        }
    }
}
