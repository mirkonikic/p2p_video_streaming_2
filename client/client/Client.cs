using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class Client
    {
        public NetworkStream str;
        public BinaryReader serverInput;
        public BinaryWriter serverOutput;

        public NetworkStream videoStr;
        public BinaryWriter videoOutput;

        public ClientThread client_thread = null;
        public Streamer parent;
        public int place_id { get; set; }
        public int port { get; set; }
        public string ip_addr { get; set; }
        public string username { get; set; }
        public TcpClient socket { get; set; }

        public TcpClient videoSocket { get; set; }
        public Client(Streamer parent, int place_id, int port, string ip_addr, string username, TcpClient socket) 
        {
            this.parent = parent;
            this.place_id = place_id;
            this.port = port;
            this.ip_addr = ip_addr;
            this.username = username;
            this.socket = socket;
            this.videoSocket = socket;
        }

        public Client(TcpClient socket) 
        {
            this.socket = socket;
        }

        public void Disconnect() 
        {
            //gasim thread ovog klijenta
            client_thread.isRunning = false;
            //gasim socket ka njemu
            socket.Close();
            //i postavljam da nista vise ne pokazuje na ovaj objekat, pa ga pokupi garbage collector
            parent.client_array[place_id] = null;
            //na streamer metodi smanjujem broj watchera za jedan
            parent.updateNumberOfClients();

            parent.updateViewersLabel();

            //apdejtujem log label da se diskonektovao
            parent.updateLogLabel(username + " disconnected!");
            parent.sendToAllClientsTcp("DISC " + username, username);
        }
    }
}
