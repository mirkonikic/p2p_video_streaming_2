﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        bool isRunning = true;

        public WatcherConnections(Watcher parent) 
        {
            this.parent = parent;
            isRunning = true;
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

        public void run() 
        {
            ns = parent.tcpClient.GetStream();
            br = new BinaryReader(ns);
            string read = br.ReadString();

            //while petlja koja prima samo i ispisuje u ChatTextBox
            while (isRunning) 
            {
                string[] parsed_read = read.Split(null);
                if (parsed_read[0].Equals("TEXT") && parsed_read.Length == 2) 
                {
                    //PRIMIO SAM: TEXT mirko:_ima_nekoga?_o.o
                    parent.updateChatBox(parse(read));
                }
                else 
                {
                    parent.updateLogLab(read);
                }

                read = br.ReadString();
            }
        }
    }
}