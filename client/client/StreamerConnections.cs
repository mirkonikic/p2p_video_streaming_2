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
        public void findInfoAboutClient(TcpClient client_socket) 
        {
            Client client = new Client(client_socket);

            client.str = client_socket.GetStream();
            client.serverInput = new BinaryReader(client.str);
            client.serverOutput = new BinaryWriter(client.str);
            string username;
            string ip_addr;
            string port;

            //1 pokusaj ima da mi kaze svoje ime, ako ne rip njegova konekcija
            string strm = client.serverInput.ReadString();
            string[] code_streamers = strm.Split(null);
            string code = code_streamers[0];

            if (code.Equals("START") && code_streamers.Length == 4)
            {
                username = code_streamers[1];
                ip_addr = code_streamers[2];
                port = code_streamers[3];

                client.username = username;
                client.ip_addr = ip_addr;
                client.port = Int32.Parse(port);

                if(parent.username != "debug")
                    parent.updateMsgBox(strm);

                //Pise da ne treba Binary Reader i Writer za UDP
                //client.videoSocket = new TcpClient(client.ip_addr, client.port);
                //client.videoStr = client.videoSocket.GetStream();
                //client.videoOutput = new BinaryWriter(client.videoStr);

                parent.createClient(client);
            }
            else 
            {
                client.serverInput.Close();
                client.serverOutput.Close();
                client.str.Close();
                client_socket.Close();
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
