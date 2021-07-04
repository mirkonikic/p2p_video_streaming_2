using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//U ovom threadu samo primam poruke od streamera, jer protokol za poruke nije request response nego samo salje kad dobije
//RAZMISLI DA LI SI MOGAO OVDE DA KORISTIS BINARYREADER IZ PARENT_A
//onda ne bi morao da pravis nov binary reader i network stream ovde

namespace client
{
    class WatcherConnections
    {
        Watcher parent;
        NetworkStream ns;
        BinaryReader br;
        BinaryWriter bw;

        //bool isRunning = true;

        public WatcherConnections(Watcher parent) 
        {
            this.parent = parent;
            //isRunning = true;
        }

        public string parse(string data) 
        {
            string sentence = "";
            string[] parsed_sp_data = data.Split(null);
            string[] words = parsed_sp_data[1].Split("_");

            for (int i = 0; i<words.Length; i++) 
            {
                sentence += words[i] + " ";
            }

            return sentence;
        }

        public string parse_join(string data)
        {
            return data + " just joined!";
        }

        public string parse_disc(string data)
        {
            return data + " just disconnected!";
        }

        public void run() 
        {
            try
            {
                ns = parent.tcpClient.GetStream();
                br = new BinaryReader(ns);
                string read = br.ReadString();


                //while petlja koja prima samo i ispisuje u ChatTextBox
                while (parent.isRunning)
                {
                    string[] parsed_read = read.Split(null);
                    if (parsed_read[0].Equals("TEXT") && parsed_read.Length == 2)
                    {
                        //PRIMIO SAM: TEXT mirko:_ima_nekoga?_o.o
                        if (parent.username != "debug")
                            parent.updateChatBox(parse(read));
                    }
                    else if (parsed_read[0].Equals("STOP"))
                    {

                        parent.streamerOutput = null;
                        parent.Close();
                        MessageBox.Show("Live stream is currently offline, please refresh your streamer list!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (parsed_read[0].Equals("JOIN") && parsed_read.Length == 2)
                    {
                        if (parent.username != "debug")
                        {
                            parent.updateChatBox(parse_join(parsed_read[1]));
                            parent.plusViewLab();
                        }
                    }
                    else if (parsed_read[0].Equals("DISC") && parsed_read.Length == 2)
                    {
                        if (parent.username != "debug") 
                        {
                            parent.updateChatBox(parse_disc(parsed_read[1]));
                            parent.minusViewLab();
                        }
                    }
                    else if (parsed_read[0].Equals("VIEW") && parsed_read.Length == 2)
                    {
                        if (parent.username != "debug")
                        {
                            parent.updateLogLab(read);
                            parent.updateViewLab(parsed_read[1]);
                        }
                            //Dodacu i VIEW kao broj viewera da salje i onda je gotov taj protokol valjda
                    }
                    else
                    {
                        if (parent.username != "debug")
                            parent.updateLogLab(read);
                    }

                    read = br.ReadString();
                }
            }
            catch (Exception)
            {
                br.Close();
                ns.Close();
            }
        }
    }
}
