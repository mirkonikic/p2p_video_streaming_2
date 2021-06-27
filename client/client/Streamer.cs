using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

//KLIJENT POSTAJE STREAMER
//Otvara TcpListener kao kontrolna tcp konekcija
//Napravi array klijent objekata u koje cuva klijent objekte koji se konektuju?
//Smisli protokol za CONTROL COMMANDS: Handshake, INFO, START, PAUSE, EXIT, (mozda i -> TIMESTAMP <Odakle da pusti>)
//Smisli protokol za slanje video frejmova, tako da bude SEQ number, BrFrejmovaUSekundi, OffsetForFrameData..., JedanFrejmBinaryData   
//Udp Socket pogledaj kako, i samo salje pakete koje smo napravili, mozda je najbolje napraviti klasu paket.

//int MAX_CLIENTS = 255;

namespace client
{
    public partial class Streamer : Form
    {
        Menu forma_parent;
        //Client Array
        public Client[] client_array;
        public static int max_clients;
        public int number_of_clients = 0;

        //Listener za klijente
        public TcpListener listener;


        //Socketi za razgovor sa trackerom
        NetworkStream stream;
        string username;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        //Promenljive potrebne za prikupljanje i slanje frejmova
        VideoCapture capture;
        Mat mat;

        public Streamer(NetworkStream stream, string username, Menu forma2)
        {
            InitializeComponent();

            //Popunjavanje promenljivih
            this.forma_parent = forma2;
            this.stream = stream;
            this.username = username;

            //Inicijalizacija tracker input i output-a
            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);
            forma_parent.Hide();
        }

        private void Title()
        {
            string titleName = null;

            //Sve dok nije ukucao nesto u titl, ponavlja se upis
            while (string.IsNullOrEmpty(titleName))
            {
                //Inicijalizujem string
                titleName = Microsoft.VisualBasic.Interaction.InputBox("Enter title of your stream", "Title", "");

                if (!string.IsNullOrEmpty(titleName))
                {
                    //OVDE JE BILA GRESKA, treba STRM <title>, jer tracker pazi o username-u
                    string message = $"STRM {titleName}";
                    serverOutput.Write(message);

                    Console.WriteLine(serverInput.Read());
                }
                else 
                {
                    MessageBox.Show("Uspelo?");
                }
            }
        }

        public static int MaxClients
        {
            get { return max_clients; }
        }

        public void updateNumberOfClients() 
        {
            int nm = 0;
            for (int i = 0; i < max_clients; i++)
            {
                if (client_array[i] != null) 
                {
                    nm++;
                }
            }
            number_of_clients = nm;
        }

        public void nullOutClientArray()
        {
            for (int i = 0; i < max_clients; i++)
            {
                client_array[i] = null;
            }
            updateLogLabel("Nulled out the client array");
        }

        public int returnFirstFreePlaceInArray()
        {
            for (int i = 0; i < max_clients; i++)
            {
                if (client_array[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void createClient(Client client)
        {
            //find first free space
            int place = returnFirstFreePlaceInArray();
            if (place == -1)
            {
                client.serverInput.Close();
                client.serverOutput.Close();
                client.str.Close();
                client.socket.Close();
                return;
            }
            
            //create client object
            client_array[place] = client;
            client.place_id = place;
            client.parent = this;

            //Upise u log label da je dosao novi vjuer
            logLab.Text = client.username + " just joined!";
            updateNumberOfClients();
            updateViewersLabel();
            updateMsgBox(client.username + " just joined!");

            //create clientThread
            ClientThread client_thread = new ClientThread(client_array[place]);
            Thread clientThread = new Thread(client_thread.Start);           //Creates Thread for -> tracker - client handler - console
            clientThread.Start();

            //put it inside client object
            client_array[place].client_thread = client_thread;
        }

       
        private void Form3_Load(object sender, EventArgs e)
        {
            //Streamer treba da:
                //THREAD - Prihvata TCP konekcije od klijenata
                    //While petlja koja .accept klijenta i ubaci ga u array klijenata u ovoj klasi
                    //Lista klijenata ima i UDP port i IP adresu na koju salje i ime
                //OVDE - Prikuplja Frejmove sa kamere i pakuje ih u pakete
                    //Cap_ImageGrabbed to radi sa eventima, cim prikupi frejm salje svim iz niza preko multicasta
            
            //Ovde otvori nov thread koji prisluskuje i prikuplja klijente, pa ih pita na koj port da im salje
            Title();

            //Napravi client array
            max_clients = Int32.Parse(forma_parent.MaxWatchers);
            client_array = new Client[max_clients];
            nullOutClientArray();

            //Pre zapocetog snimanja pozivam novi thread, gde primam zahteve od klijenata i ubacujem ih u niz
            StreamerConnections sc = new StreamerConnections(this);
            Thread tsc = new Thread(sc.run);
            tsc.Start();

            //Zapocni snimanje
            capture = new VideoCapture();
            capture.ImageGrabbed += Cap_ImageGrabbed;
            viewLab.Text = "" + number_of_clients + " " + "viewers";
            capture.Start();

        }

        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                mat = new Mat();
                capture.Retrieve(mat);                

                if(number_of_clients != 0)
                {
                    //byte[] data = sacuvajPosaljiSliku(mat.ToImage<Bgr, byte>().AsBitmap());
                    string data = sacuvajPosaljiSliku(mat.ToImage<Bgr, byte>().AsBitmap());
                    sendToAllClientsUdp(data);
                }
          

                //Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();
                //img.Save("file.png", ImageFormat.Png);            450 Kb
                //img.Save("file.bmp", ImageFormat.Bmp);            1   Mb
                //BOLJA KOMPRESIJA JE PNG

                Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();

                pbVideo.Image = img;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //public byte[] sacuvajPosaljiSliku(Bitmap bitmap)
        public string sacuvajPosaljiSliku(Bitmap bitmap)
        {
            
            byte[] zaSlanje = toByteArray(bitmap, ImageFormat.Bmp);
            //ENCODE THE BYTE AND INPUT INTO BW
            string zaSlanje_b64 = Convert.ToBase64String(zaSlanje, 0, zaSlanje.Length);

            return zaSlanje_b64;
            //return Encoding.ASCII.GetBytes(zaSlanje_b64);
        }

        public byte[] toByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                //image.Save("trebaDaPosaljem.bmp", format);
                return ms.ToArray();
            }
        }

        public void sendToAllClientsUdp(string data)//byte[] data)
        {

            for (int i = 0; i < number_of_clients; i++)
            {
                client_array[i].videoOutput.Write(data);
            }
            //videoOutput.Write(data);
            //videoStream = null;
        }





        //NIJE GOTOVO KAD STIGNES ZAVRSI...
        //Treba da posalje svima 'TEXT <text....>' komandu
        //Onda svi dobiju poruku od streamera

        












        //Gotovo i uredjeno
        public void sendToAllClientsTcp(string Data)
        {
            for (int i = 0; i < number_of_clients; i++)
            {
                client_array[i].serverOutput.Write(Data);
            }
        }

        public void updateMsgBox(string data) 
        {
            msgBox.Text += data + "\n";
        }

        public void updateLogLabel(string data) 
        {
            //logLab.Text = data;
        }

        public void updateViewersLabel() 
        {
            //viewLab.Text = "" + number_of_clients;
        }

        public string returnClientIpAddress(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[0]; }

        public string returnClientPort(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[1]; }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            serverOutput.Write("STOP");
            capture.Dispose();
            listener.Stop();
            forma_parent.Show();
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverOutput.Write("STOP");
            capture.Dispose();
            listener.Stop();
            forma_parent.Show();
        }

        private void sacuvajSliku(Bitmap bitmap)
        {
            //Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();
            //Bitmap bmp = image.AsBitmap();

            //bmp.Save("slika" + ".jpg");
        }

        public string convertSpacesToUnderlines(string protocol, string data) 
        {
            string prep_data = null;
            string[] split_data = data.Split(null);

            for (int i = 0; i<split_data.Length; i++) 
            {
                if (i == (split_data.Length-1))
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
            updateMsgBox(username+": "+tbChat.Text);
            sendToAllClientsTcp(convertSpacesToUnderlines("TEXT", username + ": " + tbChat.Text));
        }
    }
}
