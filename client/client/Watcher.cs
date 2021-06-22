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

        UdpClient udpReceiver;
        TcpClient tcpClient;
        NetworkStream streamerStream;
        BinaryReader streamerInput;
        BinaryWriter streamerOutput;

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

            streamerOutput.Write("DETAILS mirko");

            Thread t = new Thread(() =>
            {
                udpReceiver = new UdpClient(4545);
                var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //udpReceiver.Client.ReceiveBufferSize = int.MaxValue;
                try
                {
                    byte[] receivedData = udpReceiver.Receive(ref remoteEndPoint);
                    string message = Encoding.ASCII.GetString(receivedData);
                    tbVideo.Text = message;
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            t.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            tcpClient.Close();
            udpReceiver.Close();
            forma_parent.Show();
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClient.Close();
            udpReceiver.Close();
            forma_parent.Show();
        }
    }
}
