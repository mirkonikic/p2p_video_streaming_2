using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            //TCP LISTENER STARTS
            TcpListener listener = new TcpListener(IPAddress.Any, 9090);
            listener.Start();

            //TRACKER
            Tracker tracker = new Tracker(listener);                             //Creates instance of tracker
            Thread thread = new Thread(new ThreadStart(tracker.Start));           //Creates Thread for -> tracker - client handler - console
            thread.Start();                                                       //Starts tracker Thread

            //ACCEPT CLIENT
            while (tracker.isRunning == true)
            {
                try                                                                 //If server is shutdown, try throws an exception
                {
                    TcpClient client = listener.AcceptTcpClient();                  //Accepts a new client
                    tracker.createClient(client);                                   //Passes that client to createClient method
                }
                catch (Exception e) 
                {
                    //SHUTTING DOWN
                    tracker.vprint("Ne znam sto ali exception kod listenera");
                }
            }

            thread.Join();
        }
    }
}