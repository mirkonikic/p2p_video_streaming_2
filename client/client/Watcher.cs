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

        public TcpClient tcpClient;

        NetworkStream streamerStream;
        //BinaryReader streamerInput;
        BinaryWriter streamerOutput;

        TcpListener videoListener;
        NetworkStream videoStream;
        BinaryReader videoInput;
        

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

            //Ovde prvo treba da trazi od trackera da dobije ip adresu, a port je uvek 9091
            //Onda otvori TCP konekciju sa streamerom i da mu udp info, pa zapocinje recieve
            tcpClient = new TcpClient("127.0.0.1", 9091);
            streamerStream = tcpClient.GetStream();
            //Ne treba binary reader ovde jer imam onaj drugi thread za primanje poruka
            streamerOutput = new BinaryWriter(streamerStream);

            videoListener = new TcpListener(IPAddress.Any, 9092);
            videoListener.Start();
            TcpClient videoStreamer;

            //updateChatBox("START mirko 127.0.0.1 " + tcpClient.Client.RemoteEndPoint.ToString().Split(":")[1]);
            streamerOutput.Write("START " + username + " " + tcpClient.Client.RemoteEndPoint.ToString().Split(":")[0] + " " + 9092);

            //Zapocinjem thread odvojen za TCP - TEXT primanje od servera
            WatcherConnections wc = new WatcherConnections(this);
            Thread tsc = new Thread(wc.run);
            tsc.Start();

            videoStreamer = videoListener.AcceptTcpClient();
            
            string receivedData = videoInput.ReadString();
            byte[] decodedData = Convert.FromBase64String(receivedData);

            File.WriteAllBytes(@"C:\Users\igorn\source\repos\p2p_video_streaming_2\img\test.png", decodedData);

        }

        public void updateLogLab(string data) 
        {
            logLab.Text = data;
        }

        public void updateChatBox(string data) 
        {
            chatBox.Text += data + "\n";

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            //Saljem streameru da gasim watching
            streamerOutput.Write("STOP " + username);

            tcpClient.Close();

            
            videoStream.Close();

            forma_parent.Show();
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //POSALJI TCPCLIENTU DA GASIS STRIM
            streamerOutput.Write("STOP " + username);

            tcpClient.Close();
            

            forma_parent.Show();
        }

        public string convertSpacesToUnderlines(string protocol, string data)
        {
            string prep_data = null;
            string[] split_data = data.Split(null);

            for (int i = 0; i < split_data.Length; i++)
            {
                if (i == (split_data.Length - 1))
                {
                    prep_data += split_data[i];
                    break;
                }

                prep_data += split_data[i] + "_";
            }

            return protocol + " " + prep_data;
        }

        private void btChat_Click(object sender, EventArgs e)
        {
            //POSALJI KAO TEXT <USERNAME> <TEXT IZ TEXTBOXA>
            updateChatBox(username + ": " + tbChat.Text);
            streamerOutput.Write(convertSpacesToUnderlines("TEXT", username + ": " + tbChat.Text));
        }
    }
}
