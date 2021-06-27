using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Watcher : Form
    {
        Menu forma_parent;
        NetworkStream stream;
        string username;
        string streamer;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        TcpClient tcpClient;
        NetworkStream streamerStream;
        BinaryReader streamerInput;
        BinaryWriter streamerOutput;

        TcpClient tcpClientVideo;
        NetworkStream videoStream;
        BinaryReader videoInput;
        BinaryWriter videoOutput;

        public Watcher(NetworkStream stream, string username, Menu forma2, string streamer)
        {
            InitializeComponent();

            this.forma_parent = forma2;
            this.stream = stream;
            this.username = username;
            this.streamer = streamer;

            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);
            forma_parent.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            tcpClient = new TcpClient("127.0.0.1", 9091);
            streamerStream = tcpClient.GetStream();
            streamerInput = new BinaryReader(streamerStream);
            streamerOutput = new BinaryWriter(streamerStream);
            streamerOutput.Write($"START mirko {tcpClient.Client.RemoteEndPoint.ToString().Split(":")[0]} 4545");


            tcpClientVideo = new TcpClient("127.0.0.1", 9092);
            videoStream = tcpClientVideo.GetStream();
            videoInput = new BinaryReader(videoStream);

            byte[] receivedData = videoInput.ReadBytes(2048);
            string receivedData_b64 = Encoding.ASCII.GetString(receivedData);
            byte[] decodedData = Convert.FromBase64String(receivedData_b64);

            File.WriteAllBytes(@"C:\Users\igorn\source\repos\p2p_video_streaming_2\img\test.png", decodedData);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            tcpClient.Close();
            tcpClientVideo.Close();
            videoStream.Close();
            videoOutput.Close();
            forma_parent.Show();
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClient.Close();
            tcpClientVideo.Close();
            videoStream.Close();
            videoOutput.Close();
            forma_parent.Show();
        }
    }
}
