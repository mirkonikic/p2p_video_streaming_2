using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

//Thread koji prihvata klijente i ubacuje ih nazad u prosledjenu promenljivu

namespace client
{
    class StreamerConnections
    {
        Streamer parent;

        public StreamerConnections(Streamer parent)
        {
            this.parent = parent;
        }

        //Trazim da mi posalje info
        public void findInfoAboutClient(TcpClient client) 
        {
            NetworkStream str = client.GetStream();
            BinaryReader serverInput = new BinaryReader(str);
            BinaryWriter serverOutput = new BinaryWriter(str);
            int i;
            string username;

            //3 pokusaja ima da mi kaze svoje ime, ako ne rip njegova konekcija
            for (i = 0; i<3; i++) 
            {
                string strm = serverInput.ReadString();
                string[] code_streamers = strm.Split(null);
                string code = code_streamers[0];

                if (code.Equals("DETAILS") && code_streamers.Length == 2) 
                {
                    username = code_streamers[1];
                    parent.createClient(client, username);
                    break;
                }
            }
        }

        //Metoda samo prihvata klijente i prosledjuje ih metodi u Streamer klasi
        public void run()
        {
            //Standard port na koji se klijent prikaci za streamera, da se ne bi sudarali streamer i tracker
            parent.listener = new TcpListener(IPAddress.Any, 9091);
            parent.listener.Start();
            TcpClient client;
            Boolean end = false;

            try
            {
                while (end != true)
                {
                    client = parent.listener.AcceptTcpClient();
                    findInfoAboutClient(client);
                }
            }
            catch (SocketException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
