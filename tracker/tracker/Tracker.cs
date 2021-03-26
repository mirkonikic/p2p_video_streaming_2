using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace tracker
{
    class Tracker
    {
        //Client Array
        public Client[] client_array;
        int max_clients = 256;

        public bool isRunning = false;
        public bool isVisual = false;
        TcpListener main_listener;

        public string path = Directory.GetCurrentDirectory();

        public Tracker(TcpListener main_listener)
        {
            this.main_listener = main_listener;
            client_array = new Client[max_clients];
            nullOutClientArray();
            if (!path.EndsWith("\\")) { path += "\\"; }
            this.isRunning = true;
        }

        public void nullOutClientArray() 
        {
            for (int i = 0; i<max_clients; i++) 
            {
                client_array[i] = null;
            }
        }

        public void printOutClientArray() 
        {
            for (int i = client_array.Length - 1; i >= 0; i--) { vprint("[" + i + "]" + client_array[i] + " | " + client_array[i]?.name + " | " + client_array[i]?.ip_address); }
        }

        public int returnFirstFreePlaceInArray() 
        {
            for (int i = 0; i<max_clients; i++) 
            {
                if (client_array[i] == null) 
                {
                    vprint("Found free place: " + i);
                    return i;
                }
            }
            return -1;
        }

        public void createClient(TcpClient client)
        {
            vprint("new connection: " + client.ToString());

            //find first free space
            int place = returnFirstFreePlaceInArray();
            if(place == -1)
            {
                terror("No more free places in the array!");
                client.Close();
                return;
            }

            //create client object
            client_array[place] = new Client(client, this, place);

            //create clientThread
            ClientThread client_thread = new ClientThread(client_array[place]);
            Thread clientThread = new Thread(new ThreadStart(client_thread.Start));           //Creates Thread for -> tracker - client handler - console
            
            //put it inside client object
            client_array[place].client_thread = client_thread;

            clientThread.Start();
            vprint("Started new client thread: " + client_thread.ToString());
        }

        public void terror(string line) 
        {
            Console.WriteLine("TR! "+line);
        }

        public void tprint(string line) 
        {
            Console.WriteLine(line);
        }

        public string tcmd_read() 
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("tracker> ");
            Console.ForegroundColor = ConsoleColor.White;
            return tread();
        }

        public void vprint(string line, string host= null) 
        {
            if (isVisual == true)
            {
                if (host == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("visual> ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(line);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(host+"> ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(line);
                }
            }
        }

        public string tread() 
        {
            return Console.ReadLine();
        }

        public void tmenu() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //PRINT neki asciiart
            Console.WriteLine(@"               .----.");
            Thread.Sleep(200);
            Console.WriteLine(@"               |C>_ |");
            Thread.Sleep(200);
            Console.WriteLine(@"             __|____|__");
            Thread.Sleep(200); 
            Console.WriteLine(@"            |  ______--|");
            Thread.Sleep(200);
            Console.WriteLine(@"            `-/.::::.\-'a");
            Thread.Sleep(200);
            Console.WriteLine(@"             `--------'");
            Thread.Sleep(200);
            Console.WriteLine(@"                            p2p video streaming service");
            Thread.Sleep(200);
            Console.WriteLine("                            -Igor Mirko\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void tparseEcho(string line) 
        {
            string[] args = line.Split(null);
            string echo = "";
            for (int i = 1; i<args.Length; i++) 
            {
                echo += args[i] + " ";
            }
            tprint(echo);
        }

        public void tparseInfo(string line) 
        {
            tprint("tracker v1.12");
        }

        public void tparseEnd() 
        {
            closeClients();
            isRunning = false;
            main_listener.Stop();
        }

        public void tparseVisual(string line)
        {
            if (line.Split(null).Length != 2) { return; }
            switch (line.Split(null)[1]) 
            {
                case "OFF":
                    isVisual = false;
                    break;
                case "off":
                    isVisual = false;
                    break;
                case "ON":
                    isVisual = true;
                    break;
                case "on":
                    isVisual = true;
                    break;
                default:
                    break;
            }
            tprint("--visual set to " + isVisual);
        }

        public void pathMinusOne() 
        {
            if (path.EndsWith("\\")) { path = path.Remove(path.Length - 1, 1); } //OBRISI / SA KRAJA da ne bi bilo problema kod smanjivanja clanova
            string[] parsed_path = path.Split("\\");
            int length = parsed_path.Length;
            vprint("" + length);
            if (length <= 1)
            {
                terror("No parent directories...");
            }
            else 
            {
                string tmp_path = "";
                for (int i = 0; i < length - 1; i++)
                {
                    tmp_path += parsed_path[i] + "\\";
                }
                vprint("Path set to: " + tmp_path);
                path = tmp_path;
            }
        }

        public void tparsePath(string line) 
        {
            string[] line_parsed = line.Split(null);
            if (line_parsed.Length == 1) { tprint(path); }
            else if (line_parsed.Length == 2)
            {
                switch (line_parsed[1])
                {
                    case "--":
                        pathMinusOne();
                        break;
                    case "reset":
                        path = Directory.GetCurrentDirectory();
                        break;
                    case "RESET":
                        path = Directory.GetCurrentDirectory();
                        break;
                }
            }
            else if (line_parsed.Length == 3)
            {
                if (line_parsed[1].Equals("set") || line_parsed[1].Equals("SET")) { path = line_parsed[2]; }
            }
            else { terror("too many arguments!"); }
        }

        public void tparseList(string line) 
        {
            string[] parsed_line = line.Split(null);
            if (parsed_line.Length == 1)
            {
                string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                vprint("Tracker found " + dirs.Length + " directories on path: " + path);
                foreach (string dir in dirs)
                { tprint(dir); }
            }
            else if (parsed_line.Length == 2) 
            {
                switch (parsed_line[1]) 
                {
                    case "cli_arr":
                        printOutClientArray();
                        break;
                    case "file":
                        string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
                        foreach (string file in files) { vprint(file); }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                terror("Command List: badly framed command!");
            }
        }

        public void tparseCd(string line) 
        {
            string[] parsed_line = line.Split(null);
            if (parsed_line.Length != 2) { return; }

            switch (parsed_line[1]) 
            {
                case "..":
                    pathMinusOne();
                    break;
                case ".":
                    break;
                default:
                    //Proveri dal postoji
                    if (Directory.Exists(path + parsed_line[1] + "\\")) { vprint("Postoji: " + path + parsed_line[1] + "\\"); path = path + parsed_line[1] + "\\"; }
                    else { vprint("Ne postoji: " + path + parsed_line[1] + "\\"); }
                    break;
            }
        }

        public void closeClients() 
        {
            for (int i = 0; i<max_clients; i++) 
            {
                if (client_array[i] != null) 
                {
                    client_array[i].Disconnect();
                }
            }
        }

        //Console input that tracker takes from console
        public void Start()
        {
            tmenu();

            vprint("Tracker v1.12 started!");

            string input;
            while (isRunning == true) 
            {
                input = tcmd_read();
                vprint(input);
                switch (input.Split(null)[0]) 
                {
                    case "END":
                        tparseEnd();
                        break;
                    case "end":
                        tparseEnd();
                        break;
                    case "ECHO":
                        tparseEcho(input);
                        break;
                    case "echo":
                        tparseEcho(input);
                        break;
                    case "INFO":
                        tparseInfo(input);
                        break;
                    case "info":
                        tparseInfo(input);
                        break;
                    case "VISUAL":
                        tparseVisual(input);
                        break;
                    case "visual":
                        tparseVisual(input);
                        break;
                    case "PATH":
                        tparsePath(input);
                        break;
                    case "path":
                        tparsePath(input);
                        break;
                    case "LIST":
                        tparseList(input);
                        break;
                    case "list":
                        tparseList(input);
                        break;
                    case "CD":
                        tparseCd(input);
                        break;
                    case "cd":
                        tparseCd(input);
                        break;
                    case "HELP":
                        tprint("VISUAL \nCD \nLIST \nPATH \nECHO \nINFO \nHELP \nEND");
                        break;
                    case "help":
                        tprint("visual \ncd \nlist \npath \necho \ninfo \nhelp \nend");
                        break;
//DODAJ KICK, BAN, CAT(databaze), LIST(za druge foldere), INFO za streamere
                    default:
                        break;
                }
            }
        }
    }
}
