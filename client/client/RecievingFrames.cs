using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace client
{
    class RecievingFrames
    {
        Watcher parent;
        BinaryReader videoInput;

        public RecievingFrames(Watcher parent, BinaryReader videoInput) 
        {
            this.parent = parent;
            this.videoInput = videoInput;
        }

        public void run() 
        {
            string receivedData;
            byte[] decodedData;

            while (parent.isRunning)
            {
                receivedData = videoInput.ReadString();
                decodedData = Convert.FromBase64String(receivedData);

                //Ovo cuvanje radi, sad jos samo da se prebaci na pictureBox
                //File.WriteAllBytes(@"C:\Users\Mirko\source\test.png", decodedData);

                using (var ms = new MemoryStream(decodedData))
                {
                    parent.updatePictureBox(Image.FromStream(ms));
                }

                //Image file0 = decodedData;
                //Image image1 = Image.FromFile("c:\\FakePhoto1.jpg");
                //Bitmap img = mat.ToImage<Bgr, byte>().AsBitmap();

                //parent.updatePictureBox(img);
            }
        }
    }
}
