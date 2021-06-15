
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

//Pogledaj todo.txt za raspored Form-i, ova ce biti samo za klijenta

namespace client
{
    public partial class Menu : Form
    {
        //Socketi za razgovor sa trackerom
        NetworkStream stream;
        string username;
        BinaryReader serverInput;
        BinaryWriter serverOutput;

        //Konstruktor klase Form2(), plus inicijalizacija
        public Menu(NetworkStream stream, string username)
        {
            InitializeComponent();
            this.stream = stream;
            this.username = username;

            //Inicijalizacija tracker input i output-a
            serverInput = new BinaryReader(stream);
            serverOutput = new BinaryWriter(stream);
        }

        //Kad se ucita forma 2, onda...
        private void Form2_Load(object sender, EventArgs e)
        {
            //Pozovi LIST komandu trackera, da vidis ko je sve online i strimuje
            serverOutput.Write("LIST");

            //Ucitaj od trackera response i parsuj ga da pogledas status code
            string strm = serverInput.ReadString();
            string[] code_streamers = strm.Split(null);
            string code = code_streamers[0];

            //Provera status code-a
            if (code == "201")
            {
                //Ako je 201 - Sending LIST
                string[] streamers = code_streamers[1].Split(';');
                string streamer = "";

                int i = 1;
                int j = 0;
                int counter = 0;

                //Za svakog primljenog strimera koji sada strimuje
                foreach (string streamer_info in streamers)
                {
                    //Parsuj podatke koje su stigli za njih
                    string[] info = streamer_info.Split(':');
                    string[] title_split = info[1].Split('_');
                    string title = "";

                    foreach (string title_info in title_split)
                    {
                        title += title_info + " ";
                    }

                    streamer = $"streamer{i}: {info[0]}    title: {title}";

                    //Kreiraj Label i Button za svakog od njih
                    Label mylab = new Label();
                    Button button = new Button();

                    mylab.AutoSize = true;
                    mylab.ForeColor = Color.Black;
                    mylab.Font = new Font("Calibri", 10);
                    mylab.Text = streamer;


                    button.AccessibleName = "btnPlay";
                    button.Text = "PLAY";
                    button.AutoSize = true;


                    //Podesava poziciju strimera koji strimuju

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
            else if (code == "408") //No Streamers
            {
                Label notFound = new Label();
                notFound.AutoSize = true;
                notFound.Text = "No online streamers found";
                notFound.Location = new Point(12, notFound.Location.Y + notFound.Height);
                this.Controls.Add(notFound);
            }
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            var form3 = new Streamer(stream, username, this);
            //Form3 krije i pokazuje ovu formu preko ^
            form3.Show();
        }

        private void startStreamBtn_Click(object sender, EventArgs e)
        {
            var form3 = new Streamer(stream, username, this);
            //Form3 krije i pokazuje ovu formu preko ^
            form3.Show();
        }
    }
}
