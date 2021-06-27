using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class VideoConnections
    {
        Streamer parent;

        public VideoConnections(Streamer parent)
        {
            this.parent = parent;
        }

        public void getInfoAboutClient(TcpClient client)
        {
            parent.videoStream = client.GetStream();
            parent.videoOutput = new BinaryWriter(parent.videoStream);
        }
        public void run()
        {
            //Standard port na koji se klijent prikaci za streamera, da se ne bi sudarali streamer i tracker
            parent.videoListener = new TcpListener(IPAddress.Any, 9092);
            parent.videoListener.Start();
            TcpClient client;
            Boolean end = false;

            try
            {
                while (end != true)
                {
                    client = parent.videoListener.AcceptTcpClient();
                    getInfoAboutClient(client);
                }
            }
            catch (SocketException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

    }
}
