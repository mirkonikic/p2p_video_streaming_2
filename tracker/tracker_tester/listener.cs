using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace tracker_tester
{
    class listener
    {
        public bool end = false;
        public BinaryReader br = null;
        public listener(BinaryReader br) 
        {
            this.br = br;
        }

        public void Start() 
        {
            while (!end) { Console.WriteLine(br.ReadString()); }
        }
    }
}
