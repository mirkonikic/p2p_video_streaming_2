using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

//OVDE ZAPOCINJE PROGRAM, program.cs zove Form1() klasu

//ZA PROBLEM SA NOVIM STRIMERIMA, najlakse je refresh dugme ja msm kod klijenta, kad klikne, posalje LIST trackeru i apdejtuje

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

//KLIJENT POSTAJE NESTO OD TA DVA (nzm dal ovo preko threada)
    //Imamo Thread Klasu za oba
    //Kad klikne da postane nesto samo se pokrene taj thread
    //Kad klikne da prestane samo se ugasi thread
    //Cuvaj threadove i npr dal je streamer ili watcher da ne bi mogao da bude oba u isto vreme

namespace client

{
    public partial class Main : Form
    {
        //Promenljive za vezu sa trakerom
        TcpClient client;
        NetworkStream stream;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        //Inicijalizacija forme
        public Main()
        {
            InitializeComponent();
           
        }

        //Po ucitavanju/loadovanju forme poziva se ova metoda
        private void Form1_Load(object sender, EventArgs e)
        {
            //Ovo podesi sliku iz foldera na logo aplikacije
            Image image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Capture.png");
            pictureBox1.Image = (Image)(new Bitmap(image, new Size(400, 450)));
        }

        //Pojavljuje se login window i po slanju ako je stiglo 200 OK onda je ulogovan i prosledjuje se dalje - Form2()
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //UZMI IZ tracker_settings ip i port
            //proveri dal ima nesto u njima
            //ako nema koristi default => 127.0.0.1:9090
            string ip_address = "127.0.0.1";
            int port = 9090;

            

            //Proveri dal su polja prazna i ako nisu uporedi ih sa regexom
            //Ako jesu umesto praznih ubaci default

            //Proverava port
            if (!String.IsNullOrEmpty(tbPort.Text))
            {
                //REGEX za port
                String port_input = tbPort.Text;
                Regex port_rgx = new Regex(@"^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$");

                //Proveri dal je port dobro unesen
                if (port_rgx.IsMatch(port_input))
                {
                    port = Int16.Parse(port_input);
                }
                else 
                {
                    MessageBox.Show("Port number should consist only of numbers and should not exceed 65535", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //Proverava ip address
            if (!String.IsNullOrEmpty(tbIpAddress.Text)) 
            {
                //REGEX za ip adresu
                String ip_addr_input = tbIpAddress.Text;
                //Regex ip_rgx = new Regex(@"'\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b'");
                Regex ip_rgx = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

                //Proveri dal je ip addressa dobro unesena
                if (ip_rgx.IsMatch(ip_addr_input))
                {
                    MessageBox.Show("Match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ip_address = ip_addr_input;
                }
                else
                {
                    MessageBox.Show($"Ip address {ip_addr_input} should be in right format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (String.IsNullOrEmpty(tbUsername.Text))
            {
                MessageBox.Show("Username should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Proverava ip address
            if (String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Password should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Zapocne konekciju sa trakerom
            client = new TcpClient(ip_address, port);
            stream = client.GetStream();
            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);
            //Sada imamo serverInput za podataka od trackera i output za slanje ka trackeru

            //Sacuva pass i usernm i loguje se
            string username = tbUsername.Text;
            //string password = tbPassword.Text;

            //Kreira komandu koju salje
            string message = "USER " + username + " " + tbPassword.Text;
            serverOutput.Write(message);

            //Primi od servera response
            string inputMessage = serverInput.ReadString();
            string[] inputSplit = inputMessage.Split(null);
            string code = inputSplit[0];

            //Ako je 200, dobro izvrsen zahtev
            if (code == "200")
            {
                //Ako je tacno pozovi formu 2 i prosledi socket i username
                this.Hide();
                var form2 = new Menu(stream, username);
                form2.Closed += (s, args) => this.Close();
                form2.Show();

                serverOutput.Flush();
            }

        }

        
    }

}

