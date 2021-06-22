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
    public class ClientThread
    {
        Client client = null;
        public bool isRunning = false;
        //IDEJA za resavanje problema kako poslati LIST-u klijentu

        string input = "";

        public ClientThread(Client client)
        {
            this.client = client;
            isRunning = true;
        }

        public void Start()
        {
            while (isRunning == true)
            {
                try
                {
                    input = client.serverInput.ReadString();
                    createResponse(parseRequest(input));
                }
                catch (Exception)
                {
                    client.Disconnect();
                    return;
                }
            }
        }

        public int parseRequest(string line)
        {
            int return_code = 500;
            switch (line.Split(null)[0])
            {
                case "TEXT":
                    return_code = parseText(line);
                    break;
                case "STOP":
                    parseStop();
                    return 500;
                default:
                    return 500;
            }

            return return_code;
        }

        public void createResponse(int request_code)
        {
            string response = null;
            switch (request_code)
            {
                case 101:                   //HELP
                    response = "101 Commands available: TEXT, STOP";
                    break;
                case 200:
                    response = "200 Ok";
                    break;
                case 400:
                    response = "400 Bad Request";
                    break;
                default:
                    response = "500 Internal Server Error";
                    break;
            }
            client.serverOutput.Write(response);
        }

        public string returnSentence(string[] parsed_words) 
        {
            string sentence = "";
            for (int i = 0; i < parsed_words.Length; i++) 
            {
                sentence += parsed_words[i] + " ";
            }
            return sentence;
        }

        public int parseText(string line)
        {
            //Posalji parentu da upise u text box
            //NEK SA WATCHER STRANE BUDE UMESTO SPACE - DONJA CRTA

            //TEXT USERNAME <tekst_sa_donjim_crtama>
            string[] parsed_line = line.Split(null);
            string[] parsed_words = parsed_line[2].Split("_");

            //Saljem u for petlju da mi vrati recenicu od parsovanih reci
            string sentence = returnSentence(parsed_words);

            client.parent.updateMsgBox(client.username + ": " + sentence);

            //PROSLEDI OSTALIM CLIENTIMA 
            //Posto mi je klijent slao - TEXT USERNAME tekst_sa_donjim_crtama
            client.parent.sendToAllClientsTcp("TEXT " + client.username + " " + parsed_line[1]);

            return 200;
        }

        //Zaustavlja streaming/watching (vraca se u meni), ako je role=w onda ga bris iz gore navedenih, ako je role=s onda ga brise iz prethodno navedenih
        public void parseStop()
        {
            client.Disconnect();
        }
    }
}