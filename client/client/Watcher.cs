using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        public string username;
        string streamer;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        public TcpClient tcpClient;

        NetworkStream streamerStream;
        //BinaryReader streamerInput;
        public BinaryWriter streamerOutput;

        UdpClient videoListener;
        IPEndPoint RemoteIpEndPoint;
        //TcpListener videoListener;
        //NetworkStream videoStream;
        //BinaryReader videoInput;

        public bool isRunning = true;

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
            updateViewLab("" + 0);
            //Ovde prvo treba da trazi od trackera da dobije ip adresu, a port je uvek 9091
            //Onda otvori TCP konekciju sa streamerom i da mu udp info, pa zapocinje recieve
            serverOutput.Write($"WTCH {streamer}");
            string response = serverInput.ReadString();
            string[] parsed_response = response.Split(null);

            string ip_addr = "127.0.0.1";

            if (parsed_response[0] == "200")
            {
                ip_addr = parsed_response[1];
            }
            else 
            {
                this.Close();
            }


            tcpClient = new TcpClient(ip_addr, 9091);
            streamerStream = tcpClient.GetStream();
            //Ne treba binary reader ovde jer imam onaj drugi thread za primanje poruka
            streamerOutput = new BinaryWriter(streamerStream);


            //videoListener = new TcpListener(IPAddress.Any, 0);
            videoListener = new UdpClient(0);

            //videoListener.Start();
            TcpClient videoStreamer;

            //updateChatBox("START mirko 127.0.0.1 " + tcpClient.Client.RemoteEndPoint.ToString().Split(":")[1]);
            streamerOutput.Write("START " + username + " " + tcpClient.Client.LocalEndPoint.ToString().Split(":")[0] + " " + videoListener.Client.LocalEndPoint.ToString().Split(":")[1]);

            //Zapocinjem thread odvojen za TCP - TEXT primanje od servera
            WatcherConnections wc = new WatcherConnections(this);
            Thread tsc = new Thread(wc.run);
            tsc.Start();

            //Inicijalizuje socket - Sad mi ne treba video streamer socket jer nema konekcije nego samo primam pakete
            //videoStreamer = videoListener.AcceptTcpClient();
            //videoStream = videoStreamer.GetStream();
            //videoInput = new BinaryReader(videoStream);

            //Cuva samo jednu sliku
            /*KOmentarisem da ako pogresim mozemo da vratimo
            string receivedData = videoInput.ReadString();
            //updateChatBox(receivedData);
            byte[] decodedData = Convert.FromBase64String(receivedData);

            //Sacuvaj jednu sliku samo u bin/Debug/net-50/test.png
            File.WriteAllBytes(@"test.png", decodedData);
            */

            //Ovo mozda u drugi thread
            //Jer gusi ovu nit
            RecievingFrames rf = new RecievingFrames(this, videoListener);
            Thread pf = new Thread(rf.run); //pf - prijem frejmova
            pf.Start();
        }

        public void updatePictureBox(Image img) 
        {
            pbVideo.Image = img;
        }

        public void updateLogLab(string data) 
        {

            if (!username.Equals("debug"))
                logLab.Text = data;
        }

        public void updateSeqLab(string data)
        {
            if (!username.Equals("debug"))
                seqLab.Text = data;
        }

        public void updatePayLenLab(string data)
        {
            if (!username.Equals("debug"))
                payLenLab.Text = data;
        }

        public void minusViewLab() 
        {
            if (!username.Equals("debug"))
            {
                int num = (Int32.Parse(viewLab.Text) - 1);
                viewLab.Text = "" + num;
            }
        }

        public void plusViewLab() 
        {
            if (!username.Equals("debug"))
            {
                int num = (Int32.Parse(viewLab.Text) + 1);
                viewLab.Text = "" + num;
            }
        }

        public void updateViewLab(string data) 
        {
            if (!username.Equals("debug"))
                viewLab.Text = data;
        }

        public void updateChatBox(string data) 
        {
            if (!username.Equals("debug"))
                chatBox.Text += data + "\n";
        }

        public void stopBtn_Click(object sender, EventArgs e)
        {
            //Saljem streameru da gasim watching
            /*isRunning = false;

            serverOutput.Write("STOP");
            string status = serverInput.ReadString();

            streamerOutput.Write("STOP " + username);

            tcpClient.Close();

            videoListener.Stop();

            forma_parent.Show();*/
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;

            serverOutput.Write("STOP");
            string status = serverInput.ReadString(); 

            //POSALJI TCPCLIENTU DA GASIS STRIM
            if(streamerOutput != null)
            {
                streamerOutput.Write("STOP " + username);
            }


            tcpClient.Close();

            videoListener.Close();

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
            if(username != "debug")
                updateChatBox(username + ": " + tbChat.Text);
            streamerOutput.Write(convertSpacesToUnderlines("TEXT", username + ": " + tbChat.Text));
            tbChat.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pbVideo.Image.Save(@"~\Pictures\slika2.png", ImageFormat.Jpeg);
            //Image<Bgr, byte> image = pbVideo.Image;
            //Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();

            //Bitmap bitmap = image.AsBitmap();
            //bitmap.Save("slika.png", ImageFormat.Jpeg);
        }
    }
}
