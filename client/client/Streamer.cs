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
using System.Drawing.Drawing2D;

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

        int scale = 1;

        //Socketi za razgovor sa trackerom
        NetworkStream stream;
        public string username;
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
            if(username != "debug")
                logLab.Text = client.username + " just joined!";

            updateNumberOfClients();

            updateViewersLabel();

            
            updateMsgBox(client.username + " just joined!");
            sendToAllClientsTcp("JOIN " + client.username, client.username);    //Napisi ostalim klijentima da se prikacio nov
            client.serverOutput.Write("VIEW " + number_of_clients);             //Napisi novom klijentu koliko postoji gledalaca na ovom strimu
            
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
            
            //Bez smanjivanja je 1MB velicina

            //110KB velicina, ovo bi valjda moglo kroz udp, sad cu da probam
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 1280);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 720);

            capture.ImageGrabbed += Cap_ImageGrabbed;
            capture.Start();

        }

        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                mat = new Mat();
                capture.Retrieve(mat);

                Image slika = vratiKompresovanuSliku();

                //RESIZE

                if (username != "debug")
                    viewLab.Text = "" + number_of_clients + " " + "viewers";

                
                //byte[] data = sacuvajPosaljiSliku(mat.ToImage<Bgr, byte>().AsBitmap());
                //string data = sacuvajPosaljiSliku(mat.ToImage<Bgr, byte>().AsBitmap());

                //ODVOJI KOMPRESOVANJE SLIKE I PREBACIVANJE U BAJTOVE U RAZLICITE METODE
                ////string data = sacuvajPosaljiSliku();
                string data = enkodujSliku(slika);
                //PROVERI DAL JE DATA VECI OD 65535B i ako jeste prikazi warning da mora da poveca kompresiju
                if (data.Length <= 65535)
                {
                    if (number_of_clients != 0)
                        sendToAllClientsUdp(data);
                    pbVideo.Image = slika;
                }
                else
                {
                    pbVideo.Image = Image.FromFile("CompressionWarning.png");
                }


                //Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();
                //img.Save("file.png", ImageFormat.Png);            450 Kb
                //img.Save("file.bmp", ImageFormat.Bmp);            1   Mb
                //BOLJA KOMPRESIJA JE PNG

                //Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();
                ////sacuvajSliku(img);
                //pbVideo.Image = slika;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        //Prvi deo koda iz SacuvajPosaljiSliku metode
        public Image vratiKompresovanuSliku() 
        {
            Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();
            Bitmap bmp = image.AsBitmap();
            Image slika_posle_2_kompresije;

            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                slika_posle_2_kompresije = ShrinkImage(Image.FromStream(ms), scale);
            }

            return slika_posle_2_kompresije;
        }

        public string enkodujSliku(Image img) 
        {
            byte[] zaSlanje;        //byte array koji ce sadrzati bajtove slike
            zaSlanje = toByteArray(img, ImageFormat.Jpeg);      //izvuce bajtove iz prosledjene slike i kompresuje jos jednom po Jpeg kompresiji

            string zaSlanje_b64 = Convert.ToBase64String(zaSlanje, 0, zaSlanje.Length);     //prevede u base64

            return zaSlanje_b64;
        }

        /*
        //public byte[] sacuvajPosaljiSliku(Bitmap bitmap)
        public string sacuvajPosaljiSliku()//Bitmap bitmap)
        {
            Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();
            Bitmap bmp = image.AsBitmap();
            byte[] zaSlanje;

            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                //image.Save("trebaDaPosaljem.bmp", format);
                
                Image slika_posle_2_kompresije = ShrinkImage(Image.FromStream(ms), scale);
                //zaSlanje =  ms.ToArray();
                zaSlanje = toByteArray(slika_posle_2_kompresije, ImageFormat.Jpeg);
            }
            
            
            //byte[] zaSlanje = toByteArray(bitmap, ImageFormat.Bmp);
            //ENCODE THE BYTE AND INPUT INTO BW
            string zaSlanje_b64 = Convert.ToBase64String(zaSlanje, 0, zaSlanje.Length);

            return zaSlanje_b64;
            //return Encoding.ASCII.GetBytes(zaSlanje_b64);
        }
        */

        public static Image ShrinkImage(Image original, int scale)
        {
            Bitmap bmp = new Bitmap((original.Width * 2) / scale, (original.Height * 2) / scale,
                                    original.PixelFormat);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.SmoothingMode = SmoothingMode.HighQuality;
                Rectangle srcRect = new Rectangle(0, 0, original.Width, original.Height);
                Rectangle destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                G.DrawImage(original, destRect, srcRect, GraphicsUnit.Pixel);
                bmp.SetResolution(original.HorizontalResolution, original.VerticalResolution);
            }
            return (Image)bmp;
        }

        private void sacuvajSliku()//Bitmap bitmap)
        {
            Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();

            Bitmap bitmap = image.AsBitmap();
            bitmap.Save("slika.png", ImageFormat.Jpeg);
        }

        public void sendToAllClientsUdp(string data)//byte[] data)
        {
            //Poslednja verzija algoritma valjda
            int i = 0;  //ovaj broji koliko je ne null klijenata presao
            int j = 0;  //ovaj broji koliko klijenata ima
            //Sve dok nisam presao sve ne null klijente iz niza
            while (i < number_of_clients)
            {
                if (j > max_clients)    //ako sam sa j presao vise nego sto array podrzava klijenata, break;
                {
                    MessageBox.Show($"{i} je i {number_of_clients} je nm {client_array[i]?.username} sam nasao");
                    updateNumberOfClients();
                    i = number_of_clients;
                }

                //proveri dal nije null, ako nije:
                if (client_array[j] != null)
                {
                    //Posalji mu frejm
                    client_array[j].videoOutput.Write(data);
                    //Upisi da si pronasao jos jednog
                    i++;
                }

                j++;
            }
            
            /*
            //Ima neki broj klijenata sada -> number_of_clients
            //Ostali su null
            //sa i brojim broj klijenata koje sam posetio koji nisu null
            //ako broj i predje max_clients broj onda gasim while i updateujem nuber of clients

            int i = 0;
            //Sve dok nisam presao sve ne null klijente iz niza
            while (i<number_of_clients) 
            {
                //proveri dal nije null, ako nije:
                if (client_array[i] != null) 
                {
                    //Posalji mu frejm
                    client_array[i].videoOutput.Write(data);
                    //Upisi da si pronasao jos jednog
                    i++;
                }

                if (i > max_clients)
                {
                    MessageBox.Show($"{i} je i {number_of_clients} je nm {client_array[0]?.username} {client_array[1]?.username} {client_array[2]?.username}");
                    updateNumberOfClients();
                    i = number_of_clients;
                }
            }
            */

            /*
            for (int i = 0; i < number_of_clients; i++)
            {
                if (client_array[i] != null)
                {
                    client_array[i].videoOutput.Write(data);
                }
            }
            */

            /*
            for (int i = 0; i < number_of_clients; i++)
            {
                if (client_array[i] == null)
                {
                    client_array[i] = client_array[i + 1];
                    i--;
                    continue;
                }
                client_array[i].videoOutput.Write(data);
            }*/

            //videoOutput.Write(data);
            //videoStream = null;
        }





        //NIJE GOTOVO KAD STIGNES ZAVRSI...
        //Treba da posalje svima 'TEXT <text....>' komandu
        //Onda svi dobiju poruku od streamera

        












        //Gotovo i uredjeno
        public void sendToAllClientsTcp(string Data, string username)
        {
            int i = 0;  //ovaj broji koliko je ne null klijenata presao
            int j = 0;  //ovaj broji koliko klijenata ima
            //Sve dok nisam presao sve ne null klijente iz niza
            while (i < number_of_clients)
            {
                if (j > max_clients)    //ako sam sa j presao vise nego sto array podrzava klijenata, break;
                {
                    MessageBox.Show($"{i} je i {number_of_clients} je nm {client_array[i]?.username} sam nasao");
                    updateNumberOfClients();
                    i = number_of_clients;
                }

                //proveri dal nije null, ako nije:
                if (client_array[j] != null)
                {
                    //Posalji mu data ako se ne zove isto kao username, ali povecaj i jer si nasao ne null klijenta
                    if(!client_array[j].username.Equals(username))
                        client_array[j].serverOutput.Write(Data);
                    //Upisi da si pronasao jos jednog
                    i++;
                }

                j++;
            }

            /*
            for (int i = 0; i < number_of_clients; i++)
            {
                if (client_array[i] != null && !client_array[i].username.Equals(username))
                {
                    client_array[i].serverOutput.Write(Data);
                }
            }
            */

            /*for (int i = 0; i < number_of_clients; i++)
            {
                if (client_array[i] == null)
                {
                    client_array[i] = client_array[i + 1];
                    i--;
                    continue;
                }

                if (!client_array[i].username.Equals(username))
                    client_array[i].serverOutput.Write(Data);
            }*/
        }

        public void sendToAllClientsTcp(string Data)
        {
            int i = 0;
            //Sve dok nisam presao sve ne null klijente iz niza
            while (i < number_of_clients)
            {
                //proveri dal nije null, ako nije:
                if (client_array[i] != null)
                {
                    //Posalji mu data, nebitno dal je username ili ne
                    client_array[i].serverOutput.Write(Data);
                    //Upisi da si pronasao jos jednog
                    i++;
                }

                if (i > max_clients)
                {
                    updateNumberOfClients();
                    i = number_of_clients;
                }
            }

            /*
            for (int i = 0; i < number_of_clients; i++)
            {
                if (client_array[i] == null)
                {
                    client_array[i] = client_array[i + 1];
                    i--;
                    continue;
                }
                client_array[i].serverOutput.Write(Data);
            }*/
        }

        public void updateMsgBox(string data) 
        {
            if(!username.Equals("debug"))
                msgBox.Text += data + "\n";
        }

        public void updateLogLabel(string data) 
        {
            if (!username.Equals("debug"))
                logLab.Text = data;
        }

        public void updateViewersLabel() 
        {
            if (!username.Equals("debug"))
                viewLab.Text = "" + number_of_clients;
        }

        public string returnClientIpAddress(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[0]; }

        public string returnClientPort(TcpClient client) { return client.Client.RemoteEndPoint.ToString().Split(":")[1]; }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            /*serverOutput.Write("STOP");
            capture.Dispose();
            listener.Stop();
            forma_parent.Show();*/
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverOutput.Write("STOP");
            capture.Dispose();
            listener.Stop();
            sendToAllClientsTcp("STOP");
            forma_parent.Show();
        }

        private void btScreenShot_Click(object sender, EventArgs e)
        {
            //Image<Bgr, Byte> image = mat?.ToImage<Bgr, Byte>();
            //Bitmap bmp = image.AsBitmap();
            sacuvajSliku();
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
            if(username != "debug")
                updateMsgBox(username+": "+tbChat.Text);

            sendToAllClientsTcp(convertSpacesToUnderlines("TEXT", username + ": " + tbChat.Text));
            tbChat.Text = "";
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            scale = hScrollBar1.Value;
            //capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Brightness ,hScrollBar1.Value);
        }
    }
}
