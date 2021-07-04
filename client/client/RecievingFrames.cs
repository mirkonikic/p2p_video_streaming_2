using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace client
{
    class RecievingFrames
    {
        Watcher parent;
        UdpClient videoInput;
        UInt32 SEQ_number_current = UInt32.MinValue;
        UInt32 SEQ_number_old = UInt32.MinValue;
        UInt32 DataLength = UInt32.MinValue;
        //BinaryReader videoInput;

        public RecievingFrames(Watcher parent, UdpClient videoInput)
        {
            this.parent = parent;
            this.videoInput = videoInput;
        }

        public byte[] ParsePacket(byte[] data) 
        {
            //| Version 4B | SEQnum 4B | FrameDivisor 4B | PayloadSize 4B |
            //Treba da iscitas prvih 16B
            byte[] header = new byte[16];
            byte[] payload = new byte[data.Length - 16];
            //Kopiram iz data arraya od 0-og byte-a u header array od 0-og byte-a, duzinu od 16B
            Array.Copy(data, 0, header, 0, 16);
            //Kopiram iz data arraya 16o mesto u payload header 0o mesto, duzinu data paketa - 16B, ili mozemo i samo dataLength al to ako ovo uspe
            Array.Copy(data, 16, payload, 0, data.Length-16);

            //Parsujem SEQ broj iz paketa
            byte[] seq_num = new byte[4];
            Array.Copy(header, 4, seq_num, 0, 4);

            SEQ_number_current = (uint)BitConverter.ToInt32(seq_num, 0);

            //Parsujem payloadLength broj iz paketa
            byte[] payload_len = new byte[4];
            Array.Copy(header, 12, payload_len, 0, 4);

            DataLength = (uint)BitConverter.ToInt32(payload_len, 0);

            return payload;
        }

        public void run()
        {
            byte[] receivedData;
            byte[] decodedData;
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while (parent.isRunning)
                {
                    receivedData = videoInput.Receive(ref RemoteIpEndPoint);
                    //decodedData = Convert.FromBase64String(System.Text.Encoding.Default.GetString(receivedData));
                    //PARSE(decodedData)
                    decodedData = ParsePacket(Convert.FromBase64String(System.Text.Encoding.Default.GetString(receivedData)));

                    //Ovo cuvanje radi, sad jos samo da se prebaci na pictureBox
                    //File.WriteAllBytes(@"C:\Users\Mirko\source\test.png", decodedData);

                    using (var ms = new MemoryStream(decodedData))
                    {
                        //Image recv_img = Image.FromStream(ms);
                        //Odbacujem ako fali neki stari frejm
                        parent.updateSeqLab("" + SEQ_number_current);
                        if (SEQ_number_current > SEQ_number_old || SEQ_number_current < 10)
                        {
                            SEQ_number_old = SEQ_number_current;
                            parent.updatePictureBox(Image.FromStream(ms));
                        }
                        
                        parent.updatePayLenLab("" + DataLength);
                    }
                }
            }
            catch (Exception)
            {
                videoInput.Close();
            }

            //Image file0 = decodedData;
            //Image image1 = Image.FromFile("c:\\FakePhoto1.jpg");
            //Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();

            //parent.updatePictureBox(img);
        }
    }
}

