using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

//KLIJENT POSTAJE STREAMER
//Otvara TcpListener kao kontrolna tcp konekcija
//Napravi array klijent objekata u koje cuva klijent objekte koji se konektuju?
//Smisli protokol za CONTROL COMMANDS: Handshake, INFO, START, PAUSE, EXIT, (mozda i -> TIMESTAMP <Odakle da pusti>)
//Smisli protokol za slanje video frejmova, tako da bude SEQ number, BrFrejmovaUSekundi, OffsetForFrameData..., JedanFrejmBinaryData   
//Udp Socket pogledaj kako, i samo salje pakete koje smo napravili, mozda je najbolje napraviti klasu paket.

namespace client
{
    class Streamer
    {
        string title = "";
        //Za kontrolnu konekciju
        TcpClient rtcp; //rtp control protoco
        //Za podatke i frejmove
        UdpClient rtp;  //rtp protocol



        public Streamer(string title)
        {
            this.title = title;
        }

        //U ovoj metodi pokrenemo thread?
        public void run()
        {
            //Pozovemo STRM title
            //otvorimo tcp port na 9090 (Nek to bude standard za sve ove strimere i trakera)
            //Ako u C# postoji ona preprocesorska direktiva kao u C-u mozemo da stavimo u Main ili Program.cs
                //#define PORT 9090, pa onda lakse menjamo svima npr
            //otvaramo udp port bilo gde i kad nam jaqvi neko preko tcp konekcije njegovu watcher ip adresu
            //krecemo da spamujemo frejmovima taj port
            //kad posalju STOP izbacujemo tu ip adresu iz arraya watchera
            //Vec imamo funkciju koja salje frejmove, samo treba da je prevbacimo ovde
            //Jedino treba nekako da se otvori forma i ovde nzm kako samo
            //i mislim da je to to
        }
    }
}
