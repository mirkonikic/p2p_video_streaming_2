using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace tracker
{
    class ClientThread
    {
        Client client = null;
        public bool isRunning = false;
        //IDEJA za resavanje problema kako poslati LIST-u klijentu 
        string response200 = null;             //Da mozemo da kontrolisemo sta se prosledi klijentu

        string input = "";
        string last_cmd = "";
        public NetworkStream n;
        public BinaryReader br;
        public BinaryWriter bw;

        public ClientThread(Client client)
        {
            this.client = client;
            isRunning = true;
        }

        public void Start()
        {
            vprint("client " + client.socket.Client.RemoteEndPoint.ToString() + " just connnected!" + isRunning);
            n = client.socket.GetStream();
            bw = new BinaryWriter(n);
            br = new BinaryReader(n);

            vprint(n + " " + br + " " + bw, client.name);

            while (isRunning == true)
            {
                try
                {
                    input = br.ReadString();
                    createResponse(parseRequest(input));
                }
                catch (Exception)
                {
                    vprint("je iznenada prekinuo vezu!", client.name);
                    client.Disconnect();
                    return;
                }
            }
        }

        public int parseRequest(string line)
        {
            int return_code = 500;
            vprint(line, client.name);

            if (client.isLoggedIn)
            {
                switch (line.Split(null)[0])
                {
                    case "HELP":
                        return_code = parseHelp(line);
                        break;
                    case "EXIT":
                        return_code = parseExit(line);
                        break;
                    case "LIST":
                        return_code = parseList(line);
                        break;
                    case "LIWA":
                        return_code = parseLiwa(line);
                        break;
                    case "STRM":
                        return_code = parseStrm(line);
                        break;
                    case "WTCH":
                        return_code = parseWtch(line);
                        break;
                    case "STOP":
                        return_code = parseStop(line);
                        break;
                    case "USER":
                        return_code = 403;
                        break;
                    default:
                        vprint("Default se izvrsio", client.name);
                        return 500;
                }
            }
            else 
            {
                switch (line.Split(null)[0])
                {
                    case "INFO":
                        return_code = parseInfo(line);
                        break;
                    case "USER":
                        return_code = parseUser(line);
                        break;
                    default:
                        return 401;
                }
            }

            return return_code;
        }


        /*RESPONSE CODES

            - 1xx: Informational - Request received, continuing process

            - 2xx: Success - The action was successfully received,
                understood, and accepted

            - 3xx: Redirection - Further action must be taken in order to
                complete the request

            - 4xx: Client Error - The request contains bad syntax or cannot
                be fulfilled

            - 5xx: Server Error - The server failed to fulfill an apparently
                valid request


            Status-Code    =
            | "100"  ; Info... (Name of program)
            | "101"  ; Help... (Commands)

            | "200"  ; OK (+ procita vrednost iz response200 variable;)
            | "201"  ; Sending List s1;s2;s3;...
            | "202"  ; Sending List w1;w2;w3;...

            | "300"  ; //Further action must be taken in order to complete this req
                
                    Bad Request
                    Unauthorized
                //USER
                    Wrong password (pokriva i pogresan password i username postoji vec)
                    Already logged in (vec ulogovan)
                    Ok (pokriva tacan password i nema usera pa ga registruje)
                //STRM
                    Title too long (npr ako ima vise od 8 reci)
                    Vec snimate ili gledate
                    Ok (strimujete sada)
                //WTCH
                    User ne postoji (pokriva da stvarno ne postoji i da ne strimuje sada)
                    Vec gledate nesto (ako si vec watcher)
                    Ok (gledas ga sada)
                //STOP
                    Cant stop (pokriva ako ne strimujete i ne gledate)
                    Ok (uspesno resetovan role)
                //LIST
                    Sending list (Ako saljemo kroz response nek bude npr 233 streamer1;streamer2;streamer3...)
                //LIWA
                    Sending list (Isto kao LIST, 234 watccher1;watcher2;watcher3...)

            | "400"  ; Bad Request
            | "401"  ; Unauthorized
            | "402"  ; Wrong password
            | "403"  ; Already logged in
            | "404"  ; User not found
            | "405"  ; Title too long
            | "406"  ; Already watcher/Streamer
            | "407"  ; Cant stop
            
            | "500"  ; Internal Server Error
            | "501"  ; Not Implemented

        */

        public void createResponse(int request_code)
        {
            string response = null;
            //switch: 200, 100, 300 => return(OK, ERROR, GOOD)
            switch (request_code)
            {
                case 100:                   //INFO
                    response = "100 This is p2p streaming application by Mirko and Igor";
                    break;
                case 101:                   //HELP
                    response = "101 Commands available: STRM, WTCH, STOP, LIST, LIWA";
                    break;
                case 200:
                    if (response200 == null) { response = "200 Ok"; }
                    else { response = "200 " + response200; response200 = null; }
                    break;
                case 201:                   //Sending LIST
                    if (response200 == null || response200.Equals("") ) { response = "201 Ok"; }   //Ako nema strimera sada
                    else { response = "201 " + response200; response200 = null; }
                    break;
                case 202:                   //Sending LIWA
                    if (response200 == null || response200.Equals("")) { response = "202 Ok"; }   //Ako nema watchera sada
                    else { response = "202 " + response200; response200 = null; }
                    break;
                case 300:
                    response = "300 Further action must be taken";
                    break;
                case 400:
                    response = "400 Bad Request";
                    break;
                case 401:
                    response = "401 Unauthorized";
                    break;
                case 402:
                    response = "402 Wrong password";
                    break;
                case 403:
                    response = "403 Already logged in";
                    break;
                case 404:
                    response = "404 Not Found";
                    break;
                case 405:
                    response = "405 Title too long";
                    break;
                case 406:
                    response = "406 Already watcher/Streamer";
                    break;
                case 407:
                    response = "407 Cant stop";
                    break;
                case 408:
                    response = "408 No Streamers";
                    break;
                case 409:
                    response = "409 No Watchers";
                    break;
                case 500:
                    response = "500 Internal Server Error";
                    break;
                case 501:
                    response = "501 Not Implemented";
                    break;
                default:
                    response = "500 Internal Server Error";
                    break;
            }
            bw.Write(response);
        }

        public bool registerUser(string line) 
        {
            string pattern = $"user[0-{Tracker.MaxClients}]";
            Regex rgx = new Regex(pattern);

            string[] parsed_line = line.Split(null);
            //Proverava da li rgx matchuje prosledjen username i ako je broj metchovanih veci od 0 vraca false
            if (rgx.Matches(parsed_line[1]).Count>0) { vprint(parsed_line[1]+" je metchovan sa regexom \"user[0-256]\" i odbijen je za registraciju"); return false; }
            vprint(parsed_line[1] + " nije metchovan sa regexom \"user[0-256]\" i prosledjen je na registraciju");

            string db_line = parsed_line[1] + " " + parsed_line[2] + " " + returnClientIpAddress() + " " + "x";
            vprint(db_line);
            appendToDb(db_line, "user.dat");
            return true;
        }

        public int parseUser(string line)
        {
            //PROVERI DAL JE ULOGOVAN
            if (client.isLoggedIn == true) { vprint("Korisnik " + client.name + " je vec ulogovan!"); return 403; }

            //PROVERI DAL IMA 3 argumenta
            string[] parsed_line = line.Split(null);
            if (parsed_line.Length != 3) { vprint("ERROR: parsed_line nije duzine 3 elementa, "+parsed_line.Length, client.name);  return 400; }   //VRATI LOSE UNETA KOMANDA

            //POZOVI PROVERU DAL POSTOJI FAJL
            if (!checkFilesystem()) { return 500; }
            if (!checkDb("user.dat")) { return 500; }

            //VRATI IZ FAJLA USERNAME LINIJU
            string user_line = returnFromDatabase(0, parsed_line[1], "user.dat");
            string[] parsed_user = user_line?.Split(null);
            vprint("Returned from db:user.dat, " + user_line, client.name);

            //AKO VRATI NULL, ternary operator (bool)?if_true:if_false;
            if (user_line == null) 
            {
                if (registerUser(line))
                {
                    //SREDI KOD MALO DA SE NE PONAVLJA
                    client.name = parsed_line[1];
                    client.password = parsed_line[2];
                    client.isLoggedIn = true;
                    client.ip_address = client.socket.Client.RemoteEndPoint.ToString();
                    client.role = "x";
                    return 200;
                }
                else { return 500; }
            }
            else if (parsed_user[0].Equals(parsed_line[1]) && parsed_user[1].Equals(parsed_line[2])) 
            {
                client.name = parsed_line[1];
                client.password = parsed_line[2];
                client.isLoggedIn = true;
                client.ip_address = client.socket.Client.RemoteEndPoint.ToString();
                client.role = "x";
                return 200;
            }
            else { return 402; }
        }

        public int parseInfo(string line) 
        {
            return 100;
        }

        public int parseHelp(string line) 
        {
            return 101;
        }

        public int parseExit(string line) 
        {
            //bw.Write("OK");
            client.Disconnect();
            return 200;
        }

        //Iscita iz streamers.dat fajla u niz i posalje ih tako da klijent moze da split(":") i da dobije razdvojene strimere
        public int parseList(string line)
        {
            string[] parsed_line = line.Split(null);
            if (parsed_line.Length != 1) { vprint("ERROR: parsed_line nije duzine 1 elementa, " + parsed_line.Length, client.name); return 400; }   //VRATI LOSE UNETA KOMANDA

            if (!checkFilesystem()) { return 500; }
            if (!checkDb("streamers.dat")) { return 500; }

            string[] streamers = File.ReadAllLines(client.tracker.path + "Data\\streamers.dat");

            if (streamers == null) { return 408; }      //No streamers
            else
            {
                string streamers_list = "";
                
                for(int i = 0; i < streamers.Length; i++)
                {
                    string[] streamer = streamers[i].Split(null);
                    streamers_list += streamer[0]+":"+streamer[1];
                    if(i + 1 != streamers.Length)
                    {
                        streamers_list += ';';
                    }
                }

                //bw.Write(streamers_list);
                response200 = streamers_list;
                return streamers_list.Equals("") ? 408 : 201;
            }

        }

        //Zeli da strimuje, apenduje ga u streamers.dat i pravi mu fajl <>.dat, takodje u user.dat stavjla w i u client.role=w;
        public int parseStrm(string line) 
        {
            //PROVERI KOMANDU        STRM <title>
            vprint("Stigao sam", client.name);
            string[] parsed_line = line.Split(null);
            vprint(line, client.name);
            if (parsed_line.Length < 2) { return 400; }

            //PROVERI DAL VEC STRIMUJE ILI WATCHUJE
            if (client.role.Equals("w") || client.role.Equals("s")) { return 406; } //already streaming or watching

            //PROVERI FILESYSTEM i DATABAZU
            if (!checkFilesystem()) { return 500; }
            if (!checkDb("streamers.dat")) { return 500; }

            //UPISI U USER>DAT S
            modifyDb(0, client.name, 3, "s", "user.dat");
            vprint("Trebalo bi da sam modifikovao da budem streamer", client.name);

            //UPISI U STRMRS.DAT USERA
            //username		title
            //prikupi title iz komande
            if (line.Length>25) { return 405; }      //Title too long

            string title = "";
            for (int i = 2; i<parsed_line.Length; i++) 
            {
                title += parsed_line[i] + "_";
            }
            if (title.EndsWith("_")) { title = title.Remove(title.Length - 1, 1); }
            vprint(title, client.name);

            vprint("Upisujem: "+client.name+" "+title, client.name);
            appendToDb(client.name + " " + title, "streamers.dat");

            //KREIRAJ <>.DAT
            emptyOutFile(client.name + ".dat");
            vprint("Kreirani fajl: "+client.name+".dat postoji? "+File.Exists(returnDbPath(client.name+".dat")), client.name);

            client.role = "s";

            return 200;
        }

        //Zeli da gleda nekoga, salje mu info o tom streameru, stavlja mu w u user.dat, dodaje ga u <>.dat, watchers.dat i stavlja client.role=w
        public int parseWtch(string line)
        {
            //PROVERI KOMANDU        WTCH <username>
            vprint("Stigao sam", client.name);
            string[] parsed_line = line.Split(null);
            vprint(line, client.name);
            if (parsed_line.Length < 2) { return 400; }

            //PROVERI DAL VEC STRIMUJE ILI WATCHUJE
            if (client.role.Equals("w") || client.role.Equals("s")) { return 406; } //already streaming or watching

            //PROVERI FILESYSTEM i DATABAZU
            if (!checkFilesystem()) { return 500; }
            if (!checkDb("streamers.dat")) { return 500; }

            //PROVERI DAL ZELJENI USER POSTOJI
            string streamer = returnFromDatabase(0, parsed_line[1], "streamers.dat");
            if (streamer == null) { vprint("Streamer "+parsed_line[1]+" ne postoji.");  return 404; }
            
            //UPISI U USER>DAT W
            modifyDb(0, client.name, 3, "w", "user.dat");
            vprint("Trebalo bi da sam modifikovao da budem watcher", client.name);

            //PROVERI DAL POSTOJI <username>.DAT
            if (!checkDb(parsed_line[1] + ".dat")) { vprint("Ne postoji "+parsed_line[1]+".dat");  return 500; }

            //UPISI U <username>.DAT USERA
            //username_w	ip_address_w
            vprint("Upisujem " + client.name + " " + returnClientIpAddress(), client.name);
            appendToDb(client.name + " " + returnClientIpAddress(), parsed_line[1]+".dat");

            client.streamer_kog_gleda = parsed_line[1];
            client.role = "w";

            //RESI OVO -> KAKO POSLATI RETURN STRIMER INFO
            //return streamer.Split(null)[1];
            return 200;
        }

        //Zaustavlja streaming/watching (vraca se u meni), ako je role=w onda ga bris iz gore navedenih, ako je role=s onda ga brise iz prethodno navedenih
        public int parseStop(string line) 
        {
            //IF STREAMER BRISI IZ STREAM FAJLOVA, IF WATCHER BRISI IZ WACTEHR FAJLOVA

            //PROVERI KOMANDU        STRM <title>
            vprint("Stigao sam", client.name);
            string[] parsed_line = line.Split(null);
            vprint(line, client.name);
            if (parsed_line.Length > 1) { return 400; }

            //PROVERI DAL VEC STRIMUJE ILI WATCHUJE
            if (client.role.Equals("x")) { return 407; } //cant stop

            //PROVERI FILESYSTEM i DATABAZU
            if (!checkFilesystem()) { return 500; }
            if (!checkDb("user.dat")) { return 500; }

            //UPISI U USER>DAT S
            modifyDb(0, client.name, 3, "x", "user.dat");
            vprint("Trebalo bi da sam modifikovao da ne budem s/w", client.name);

            if (client.role.Equals("w"))
            {
                //PRONADJI KOG STREAMERA GLEDA
                //1_IDEJA PRODJI KROZ SVAKOG USERNAME.DAT I NADJI WATCHERA
                //2_IDEJA CUVAJ U PROMENLJIVU IME KOGA GLEDA
                //3_IDEJA NAPRAVI WATCHER.DAT I TU SE CUVA KO KOGA GLEDA
                //4_IDEJA 
                //Za sada ideja 2 je najlaksa
                //IZBRISI IZ <>.DAT USERA
                vprint(client.streamer_kog_gleda + " je ime username.dat");
                deleteFromDb(0, client.name, client.streamer_kog_gleda + ".dat");
                client.streamer_kog_gleda = null;

                return 200;
            }
            else if (client.role.Equals("s"))
            {
                //IZBRISI IZ STRMRS.DAT USERA
                deleteFromDb(0, client.name, "streamers.dat");

                //PODRAZUMEVAMO DA STREAMER JAVI WATCHERIMA DA GASI STREAM PA MOZEMO DA OCEKUJEMO OD NJIH STOP
                //KOJA CE IH POJEDINACNO PREPRAVITI
                //OBRISI <>.DAT
                deleteDb(client.name+".dat");
                vprint("Kreirani fajl: " + client.name + ".dat postoji? " + File.Exists(returnDbPath(client.name + ".dat")), client.name);
                
                //PODESI SVE WATCHERE NA X??
                //JAVI IM BAR DA JE STREAMER PRESTAO DA STRIMUJE??

                client.role = "x";

                return 200;
            }
            else
            {
                vprint("Uloga klijenta: " + client.role, client.name);
                return 407;
            }
        }

        //Iscita watchere za specificnog strimera, i to cita iz <>.dat
        public int parseLiwa(string line) 
        {
            string[] parsed_line = line.Split(null);
            if (parsed_line.Length != 1) { vprint("ERROR: parsed_line nije duzine 1 elementa, " + parsed_line.Length, client.name); return 400; }   //VRATI LOSE UNETA KOMANDA

            if (!checkFilesystem()) { return 500; }
            if (!checkDb(client.name + ".dat")) { return 500; }

            string[] watchers = File.ReadAllLines(client.tracker.path + "Data\\"+client.name+".dat");

            if (watchers == null) { return 409; }   //NEMA WATCHERA
            else
            {
                string watchers_list = "";

                for (int i = 0; i < watchers.Length; i++)
                {
                    watchers_list += watchers[i];
                    if (i + 1 != watchers.Length)
                    {
                        watchers_list += ':';
                    }
                }

                //SMISLITI KAKO DA SE RESI OVO ISTO
                //bw.Write(watchers_list);
                response200 = watchers_list.Equals("") ? null : watchers_list;
            }

            return 202;
        }

        public bool checkFilesystem()
        {
            //Mora da postoji Data(Folder): -user.dat -streamers.dat -watchers.dat -pri_kliktanju_na_watcher_kreira_se_<streamer>.dat
            if (Directory.Exists(client.tracker.path + "Data\\"))
            {
                //Check and create files inside of the directory
                if (!File.Exists(client.tracker.path + "Data\\user.dat")) { File.Create(client.tracker.path + "Data\\user.dat").Close(); }
                if (!File.Exists(client.tracker.path + "Data\\streamers.dat")) { File.Create(client.tracker.path + "Data\\streamers.dat").Close(); }
            }
            else 
            {
                //CREATE IT
                Directory.CreateDirectory(client.tracker.path + "Data\\");
                File.Create(client.tracker.path + "Data\\user.dat").Close();
                File.Create(client.tracker.path + "Data\\streamers.dat").Close();
            }

            //After creating check if they exist again and return based on the result
            vprint(File.Exists(client.tracker.path + "Data\\user.dat") + " - user.dat"); 
            vprint(File.Exists(client.tracker.path + "Data\\streamers.dat") + " - streamers.dat");
            if (File.Exists(client.tracker.path + "Data\\user.dat") && File.Exists(client.tracker.path + "Data\\streamers.dat")) { return true; }
            vprint("Vracam false izgleda");
            return false;
        }

        public bool checkDb(string db) { return File.Exists(returnDbPath(db)); }

        public bool appendToDb(string data_line, string db) 
        {
            if (!checkDb(db)) { return false; }
            StreamWriter sw = new StreamWriter(returnDbPath(db), true);
            sw.WriteLine(data_line);
            sw.Close();
            return true;
        }

        public void emptyOutFile(string db) { File.Delete(returnDbPath(db)); File.Create(returnDbPath(db)).Close(); return; }

        //sa kolonom position_q uporedjuje string query i ako su isti menja vrednost na poziciji position_v u string value u bazi db
        public void modifyDb(int position_q, String query, int position_v, String value, String db)
        {
            StreamReader sr = new StreamReader(returnDbPath(db));
            
            string[] lines = null;

            lines = File.ReadAllLines(returnDbPath(db));
            sr.Close();

            StreamWriter sw = new StreamWriter(returnDbPath(db), false);
            
            sw.Close();

            StreamWriter sa = new StreamWriter(returnDbPath(db), true);

            string tmp = "";
            foreach (string line in lines)
            {
                vprint("prosao sam "+line+". krug provere usernamea");
                tmp = line;
                if (line.Split(null)[position_q].Equals(query))
                {
                    string[] parsed_line = line.Split();
                    parsed_line[position_v] = value;
                    tmp = "";
                    for (int i = 0; i<parsed_line.Length; i++) { tmp += parsed_line[i] + " "; }
                }
                vprint(tmp, client.name);
                sa.WriteLine(tmp);
            }

            sa.Close();
        }

        public bool deleteFromDb(int position, string query, string db) 
        {
            if (!checkDb(db)) { return false; }
            
            string[] lines = File.ReadAllLines(returnDbPath(db));
            emptyOutFile(db);

            StreamWriter sw = new StreamWriter(returnDbPath(db), true);
            foreach (string line in lines) 
            {
                string[] parsed_line = line.Split(null);
                if (parsed_line[position].Equals(query)) { vprint("Nasao sam " + query, client.name); }
                else { sw.WriteLine(line); }
            }
            sw.Close();
            return true;
        }

        public bool deleteDb(string db) { File.Delete(returnDbPath(db)); return !File.Exists(returnDbPath(db)); }

        public string returnDbPath(string db) { return client.tracker.path + "Data\\" + db; }

        public string returnFromDatabase(int position, string query, string db) 
        {
            StreamReader sr = new StreamReader(returnDbPath(db));
            string[] lines = null;

            lines = File.ReadAllLines(returnDbPath(db));
            foreach (string line in lines) 
            {
                if (line.Split(null)[position].Equals(query)) 
                {
                    sr.Close();
                    return line;
                }
            }
            sr.Close();
            return null;
        }

        public string returnClientIpAddress() { return client.socket.Client.RemoteEndPoint.ToString().Split(":")[0]; }

        public string returnClientPort() { return client.socket.Client.RemoteEndPoint.ToString().Split(":")[1]; }

        public void vprint(string line, string host=null)
        {
            client.tracker.vprint(line, host);
        }
    }
}