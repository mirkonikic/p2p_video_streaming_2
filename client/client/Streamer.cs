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
        public static int max_clients = 256;
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

        public void createClient(TcpClient client, string username)
        {
            //find first free space
            int place = returnFirstFreePlaceInArray();
            if (place == -1)
            {
                client.Close();
                return;
            }

            //create client object
            client_array[place] = new Client(returnClientIpAddress(client), Int32.Parse(returnClientPort(client)), username, place);
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
            client_array = new Client[max_clients];
            nullOutClientArray();

            //Pre zapocetog snimanja pozivam novi thread, gde primam zahteve od klijenata i ubacujem ih u niz
            StreamerConnections sc = new StreamerConnections(this);
            Thread tsc = new Thread(sc.run);
            tsc.Start();

            //Otvori UDP Socket i snadji se


            //Zapocni snimanje
            capture = new VideoCapture();
            capture.ImageGrabbed += Cap_ImageGrabbed;
            capture.Start();
        }

        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                mat = new Mat();
                capture.Retrieve(mat);

                updateNumberOfClients();
                viewLab.Text = "" + number_of_clients + " " + "viewers";

                //sacuvajPosaljiSliku(mat.ToImage<Bgr, byte>().AsBitmap());

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

        private void sacuvajPosaljiSliku(Bitmap bitmap)
        {
            //Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();
            //Bitmap bmp = image.AsBitmap();

            //bmp.Save("slika" + ".jpg");
            //salje sliku
            byte[] zaSlanje = toByteArray(bitmap, ImageFormat.Bmp);
            //ENCODE THE BYTE AND INPUT INTO BW
            string zaSlanje_b64 = Convert.ToBase64String(zaSlanje, 0, zaSlanje.Length);

            //GRESKA JE BILA DA NISAM NI SLAO!!!
            //bw.Write(zaSlanje);
            serverOutput.Write(zaSlanje_b64);
            //label1.Text = zaSlanje_b64.Length.ToString();
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






        //NIJE GOTOVO KAD STIGNES ZAVRSI...
        //Treba da posalje svima 'TEXT <text....>' komandu
        //Onda svi dobiju poruku od streamera
        public void sendToAllClientsTcp(string Data)
        {

        }

        public void sendToAllClientsUdp(Byte[] Data) 
        {
            
        }













        //Gotovo i uredjeno

        public void updateLogLabel(string data) 
        {
            logLab.Text = data;
        }

        public string returnClientIpAddress(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[0]; }

        public string returnClientPort(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[1]; }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            //serverOutput.Write("STOP");
            //capture.Dispose();
            //listener.Stop();
            //forma_parent.Show();
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverOutput.Write("STOP");
            capture.Dispose();
            //listener.Stop();
            forma_parent.Show();
        }


    }
}
