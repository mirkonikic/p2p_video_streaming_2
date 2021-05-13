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

//KLASA PAKET
    //Sequence Number
    //
    //FrameData
    //
    //byte[] header;
    //int payloadSize;
    //byte[] payload;

//KLIJENT POSTAJE STREAMER
    //Otvara TcpListener kao kontrolna tcp konekcija
    //Napravi array klijent objekata u koje cuva klijent objekte koji se konektuju?
    //Smisli protokol za CONTROL COMMANDS: Handshake, INFO, START, PAUSE, EXIT, (mozda i -> TIMESTAMP <Odakle da pusti>)
    //Smisli protokol za slanje video frejmova, tako da bude SEQ number, BrFrejmovaUSekundi, OffsetForFrameData..., JedanFrejmBinaryData   
    //Udp Socket pogledaj kako, i samo salje pakete koje smo napravili, mozda je najbolje napraviti klasu paket.

//KLIJENT POSTAJE WATCHER
    //Otvara TcpClient-a kao kontrolna tcp konekcija i povezuje se na ip adresu streamera kog zeli da gleda
    //Preko info komande dobije info na koj port da se prikaci za UDP video
    //Udp Socket koji prima pakete i prosledjuje u funkciju koja parsuje i vraca Image objekat?

//KLIJENT POSTAJE NESTO OD TA DVA
    //Imamo Thread Klasu za oba
    //Kad klikne da postane nesto samo se pokrene taj thread
    //Kad klikne da prestane samo se ugasi thread
    //Cuvaj threadove i npr dal je streamer ili watcher da ne bi mogao da bude oba u isto vreme

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

