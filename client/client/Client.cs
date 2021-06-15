using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class Client
    {
        public int place_id { get; set; }
        public int port { get; set; }
        public string ip_addr { get; set; }
        public string username { get; set; }

        public Client(string ip_addr, int port, string username, int place) 
        {
            this.place_id = place;
            this.ip_addr = ip_addr;
            this.port = port;
            this.username = username;
        }


    }
}
