using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace tracker
{
    class Client
    {
        public string name = null;
        public string password = null;
        public string role = "x";

        //CONTROL
        public int id = -1;
        public Tracker tracker = null;
        public ClientThread client_thread = null;
        public TcpClient socket = null;
        public bool isLoggedIn = false;
        public string ip_address = null;

        public Client(TcpClient socket, Tracker tracker, int id) 
        {
            this.tracker = tracker;
            this.id = id;
            this.socket = socket;
            name = "user" + id;
        }

        public void Disconnect() 
        {
            //CLOSE ALL STREAMS
            client_thread.bw.Flush();
            client_thread.br.Close();
            client_thread.bw.Close();
            client_thread.n.Flush();
            client_thread.n.Close();
            socket.Close();
            client_thread.isRunning = false;

            return;
        }
    }
}
