using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

//KLIJENT POSTAJE WATCHER
//Salje trackeru WTCH <ime strimera sa liste na koju je kliknuo play>
//Otvara TcpClient-a kao kontrolna tcp konekcija i povezuje se na ip adresu streamera kog zeli da gleda
//Preko info komande dobije info na koj port da se prikaci za UDP video
//Udp Socket koji prima pakete i prosledjuje u funkciju koja parsuje i vraca Image objekat?

namespace client
{
    class Watcher
    {
        string streamer_name = "";
        string streamer_ip = "";
        //Za kontrolnu konekciju
        TcpClient rtcp; //rtp control protoco
        //Za podatke i frejmove
        UdpClient rtp;  //rtp protocol



        public Watcher(string streamer_name) 
        {
            this.streamer_name = streamer_name;
        }

        //U ovoj metodi pokrenemo thread?
        public void run() 
        {
            //Pozovemo WTCH streamer_name
            //Sacuvamo njegovu IP adresu
            //konektujemo tcp na njegov 9090 port
            //pitamo ga za udp port
            //konektujemo se na udp port
            //ili mu javimo samo nas udp port i ip adresu da krene da salje
            //to npr sa komandom START
            //kad posaljemo STOP on prestaje da salje
            //sa ove strane otvorimo loop koji prima frejmove
            //moguce da je bolje da ovde otvorimo pictureBox
            //i mislim da je to to
        }
    }
}
