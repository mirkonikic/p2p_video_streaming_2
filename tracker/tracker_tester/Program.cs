using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace tracker_tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 9090);

            NetworkStream n = client.GetStream();
            BinaryWriter bw = new BinaryWriter(n);
            BinaryReader br = new BinaryReader(n);
            string input = "";

            bool end = false;

            listener l = new listener(br);
            Thread thread = new Thread(new ThreadStart(l.Start));
            thread.Start();

            while (end != true)
            {
                input = Console.ReadLine();
                if (input.Equals("end"))
                {
                    end = true;
                    l.br.Close();
                    l.end = true;
                }
                else
                {
                    bw.Write(input);
                }
            }

            n.Flush();
            bw.Close();
            br.Close();
            n.Close();
            client.Close();
        }
    }
}
