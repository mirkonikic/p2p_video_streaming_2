
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

//Ovo nek bude thread za ako je klijent samo

//NEK PRVO RADI SVE SAMO SA FREJMOVIMA PA TEK ONDA UBACIMO OVU PACKET.CS klasu

namespace client
{
    public partial class Form2 : Form
    {

        NetworkStream stream;
        string username;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        VideoCapture capture;
        Mat mat;

        public Form2(NetworkStream stream, string username)
        {
            InitializeComponent();
            this.stream = stream;
            this.username = username;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pbVideo.Hide();
            stopBtn.Hide();


            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);

            serverOutput.Write("LIST");

            string strm = serverInput.ReadString();
            string[] code_streamers = strm.Split(null);
            string code = code_streamers[0];



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

                    Label mylab = new Label();
                    Button button = new Button();

                    mylab.AutoSize = true;
                    mylab.ForeColor = Color.Black;
                    mylab.Font = new Font("Calibri", 10);
                    mylab.Text = streamer;


                    button.AccessibleName = "btnPlay";
                    button.Text = "PLAY";
                    button.AutoSize = true;

                    if (counter < 1)
                    {
                        mylab.Location = new Point(12, mylab.Location.Y + mylab.Height);
                        button.Location = new Point(mylab.Width + 200, mylab.Location.Y - 5);
                    }
                    else
                    {
                        mylab.Location = new Point(12, mylab.Location.Y + mylab.Height + j);
                        button.Location = new Point(mylab.Width + 200, mylab.Location.Y - 5);
                    }

                    this.Controls.Add(mylab);
                    this.Controls.Add(button);

                    i++;
                    j += 50;
                    counter++;
                }
            }
            else if (code == "408")
            {
                Label notFound = new Label();
                notFound.AutoSize = true;
                notFound.Text = "No online streamers found";
                notFound.Location = new Point(12, notFound.Location.Y + notFound.Height);
                this.Controls.Add(notFound);
            }
        }


        //OVDE MOZEMO SAMO DA OTVORIMO NOVI THREAD i npr podesimo neku promenljivu da se zna da je strimer
        //da ne moze da zapocne i gledanje u isto vreme
        private void startStreamBtn_Click(object sender, EventArgs e)
        {

            string titleName = Microsoft.VisualBasic.Interaction.InputBox("Enter title of your stream", "Title", "");

            if (!string.IsNullOrEmpty(titleName))
            {
                string message = $"STRM {username} {titleName}";
                serverOutput.Write(message);

                startStreamBtn.Hide();
                pbVideo.Show();
                stopBtn.Show();

                capture = new VideoCapture();
                capture.ImageGrabbed += Cap_ImageGrabbed;
                capture.Start();
            }


        }


        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                mat = new Mat();
                capture.Retrieve(mat);

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

        private void stopBtn_Click(object sender, EventArgs e)
        {
            capture.Dispose();
            pbVideo.Image = null;
            this.Controls.Remove(pbVideo);
            stopBtn.Hide();

            serverOutput.Write("STOP");

            startStreamBtn.Show();
            this.Controls.Add(pbVideo);
        }
    }
}
