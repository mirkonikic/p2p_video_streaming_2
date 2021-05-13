using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Packet
    {
        static int HEADER_SIZE = 16; //16 Bytes
        //MOZEMO ILI DA NAPRAVIMO SEQ int STATIC PA DA U KONSTRUKTORU SAMO POVECAVAMO ZA JEDAN
        //ILI DA PRATIMO ODAKLE ZOVEMO OVU KLASU I DA TAMO POVECAVAMO A OVDE SAMO UPISEMO
        int sequence = 0;
        byte version = 1; //Verzija naseg protokola
        //DA LI JE POTREBAN TIME STAMP
        //On je prikazan u milisekundama ili sekundama? (mozda su lakse milisekunde jer kad delis 1sekund/fps dobijas milisekunde)
        //  od kad je zapoceto snimanje
        //86 000 000 ima milisekundi u 24 sata, INT_MAX je 2 000 000 000, znaci da moze da snima malo vise od 500 sati pa dodje do overflowa
        //Mozemo to da ogranicimo i onda je 4B dovoljno za ovaj deo
        int timeStamp;

        byte[] header;
        //Size of a frame in bytes, ali to moze i da se izracuna kao Size celog paketa - Size headera...
        int payloadSize;
        //U payload uvek saljemo samo jedan frame
        //Ili mozemo i dva frejma al moramo da smislimo bolji nacin za to
        //Kako da razlikujemo gde prvi frejm zavrsava a sledeci pocinje
        byte[] payload;

        //Ovde se salje Packet.packet
        byte[] packet;

        //padding je tri bajta popunjena nulama da bi sve stalo u 16B

        //DA LI NAM TREBA CHECKSUM???
        //moze checksum umesto padding-a

        //      | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 | 09 | 10 | 11 | 12 | 13 | 14 | 15 | 16 |
        //      | vr |   padding    |     sequence      |     timeStamp     |    payloadSize    |
        //      |                                                                               |
        //      |                                    PAYLOAD                                    |

        public Packet(int sequence, int timeStamp, int payloadSize, byte[] payload) 
        {
            this.sequence = sequence;
            this.timeStamp = timeStamp;
            this.payloadSize = payloadSize;

            //POPUNI SVE PROMENLJIVE
            header = createHeader();
            this.payload = payload;
            packet = createPacket();
        }

        //NAPRAVI METODU KOJA PARSUJE PAKET i vraca payload
        public byte[] parseHeader() 
        {
            return null;
        }

        //NAPRAVI METODU KOJA PAKUJE HEADER KAKO TREBA I VRATI byte[]
        public byte[] createHeader() 
        {
            return null;
        }

        //NAPRAVI METODU KOJA PAKUJE HEADER I PAYLOAD I VRATI PAKET
        public byte[] createPacket() 
        {
            return null;
        }
    }
}
