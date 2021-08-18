using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TagLib;

namespace MusicLibGenerator {
     class Program {

        enum Exit : byte {
            Success = 0,
            LoggedErr = 1,
            IncorrectAmountArgs = 2,
            DupecheckArgUnresolved = 3,
            InvalidSongPath = 4,
            NoMethodSpecified = 5
        }

        enum Method : byte {
            Simple = 0,
            ini = 1,
            None = 255
        }

        static string osuDirectory;
        static string newDirectory;
        static bool dupeCheckByID;
        static Method method = Method.ini;

        //DUPECHECK VARS
        static List<string> IDs = new List<string>();

        //CONSOLE WINDOW VARS
        static int totalSongs = 0;
        static int count = 0;
        static int successfulCopies = 0;
        static int failedCopies = 0;
        static int skippedOrExisting = 0;

        static Song Main_FolderMethod_Simple(Song song, string innerpath) {
            //GET AUDIO
            try {
                song.path = Directory.GetFiles(innerpath, "audio.mp3", SearchOption.TopDirectoryOnly)[0];
            }
            catch (IndexOutOfRangeException) {
                failedCopies++;
                song.ShouldSkip = true;
                return song;
            }

            //PATHS
            song.fileName = song.path.Split('\\')[song.path.Split('\\').Length - 2];
            song.newDir = newDirectory + "\\" + song.fileName + ".mp3";

            //DUPECHECK
            if (dupeCheckByID) {
                song.ID = song.fileName.Split('-')[0];
                if (IDs.Contains(song.ID)) {
                    skippedOrExisting++;
                    song.ShouldSkip = true;
                    return song;
                }
                IDs.Add(song.ID);
            }

            //TITLE
            if (song.fileName.Split('-').Length > 1) {
                song.Title = song.fileName.Split('-')[1];
            }
            else {
                song.Title = null;
            }

            //ARTIST
            song.Artist = song.fileName.Split('-')[0];
            List<string> aParts = song.Artist.Split(' ').ToList();
            aParts.RemoveAt(0);
            song.Artist = string.Join(" ", aParts);

            return song;
        }

        static List<Song> Main_iniMethod_Complex(List<Song> innersong, string innerpath) {

            //GET MASTERS
            List<string> osuFiles = new List<string>();
            List<string> innerSongFiles = new List<string>();
            try {
                 osuFiles = Directory.GetFiles(innerpath, "*.osu", SearchOption.TopDirectoryOnly).ToList();
            }
            catch (IndexOutOfRangeException) {
                failedCopies++;
                return innersong;
            }

            //ADD ALL SONG PATHS FROM MASTER FILES
            List<Song> innerSongs = new List<Song>();
            foreach(string mfile in osuFiles) {

                //Read file fir details
                string[] lines = System.IO.File.ReadAllLines(mfile);
                Song innerSong = new Song();
                bool p = false;
                bool t = false;
                bool a = false;

                foreach(string line in lines) {
                    string[] split = line.Split(':');
                    //Get Audio
                    if (split[0] == "AudioFilename") {
                        innerSong.path=(innerpath+"\\"+split[1].TrimStart());
                        
                        p = true;
                    }
                    //Get title
                    if(split[0] == "Title") {
                        innerSong.Title = split[1];
                        t = true;
                    }
                    //Get artist
                    if(split[0] == "Artist") {
                        innerSong.Artist = split[1];
                        a = true;
                    }
                    //check if all details gathered
                    if (p&&t&&a) {
                        break;
                    }

                    //for testing purposes only
                    if (innerSongs.Count > 1) {
                        int gniuoegunheio = innerSongs.Count;
                    }
                }

                innerSong.newDir = newDirectory + "\\" + SanitizePathString(innerSong.Artist) + " - " + SanitizePathString(innerSong.Title) + ".mp3";
              
                bool add = true;
                foreach(Song song in innerSongs) {
                    if(song.path == innerSong.path) {
                        add = false;
                    }
                }
                if (add) {
                    innerSongs.Add(innerSong);
                }
            }

            return innerSongs;

        }

        public static string SanitizePathString(string input) {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid) {
                if(input == null) { continue; }
                input = input.Replace(c.ToString(), "");
            }
            return input;
        }

        static void WriteFile(List<Song> songs) {

            foreach (Song song in songs) {
                //FILE WRITING
                if (!System.IO.File.Exists(song.path)) {
                    skippedOrExisting++;
                    return;
                }
                if (System.IO.File.Exists(song.newDir)) {
                    skippedOrExisting++;
                    return;
                }
                System.IO.File.Copy(song.path, song.newDir, true);

                //FILE TAGGING
                var tfile = TagLib.File.Create(song.newDir);
                tfile.Tag.Title = string.IsNullOrWhiteSpace(tfile.Tag.Title) ? song.Title : tfile.Tag.Title;

                if (tfile.Tag.Performers.Length == 0) {
                    tfile.Tag.Performers = new string[] { song.Artist };
                }

                tfile.Save();

                //STAT MANAGEMENT
                successfulCopies++;
            }
        }

        

        static void Main(string[] args) {
            //VALIDATE ARGS
            if(args[0].ToLower() == "/help") {
                Console.WriteLine("Argument 1 should be your osu! songs directory. Example: E:\\osu!\\Songs " +
                    "\nArgument 2 should be the directory to copy them to. Example: E:\\Music " +
                    "\nArgument 3 must be either True or False. This argument decides whether to skip songs with the same folder ID");
                Environment.Exit((int)Exit.Success);
            }
            if(args.Length < 3) {
                Console.Write($"Invalid number of arguments ({args.Length}) given, (3) required");
                Environment.Exit((int)Exit.IncorrectAmountArgs);
            }
            //if (!args[1].EndsWith("\\osu!\\Songs")) {
            //    Console.WriteLine("Song path not expected. Path expected to end in \\osu!\\Songs");
            //    Environment.Exit((int)Exit.InvalidSongPath);
            //}
            if(!bool.TryParse(args[2], out dupeCheckByID)) {
                Console.Write($"Argument 3 invalid, ID Dupechecking must be either True or False");
                Environment.Exit((int)Exit.DupecheckArgUnresolved);
            }
            if (args.Length > 3) {
                method = args[3].ToLower() == "-s" ? Method.Simple : Method.None;
                method = args[3].ToLower() == "-i" ? Method.ini : Method.None;
                if(method == Method.None) {
                    Console.Write("Method not specified in argument 4, use -s for old hierarchy method, and -i for ini indexing method");
                    Environment.Exit((int)Exit.NoMethodSpecified);
                }
            }
            bool NO_WINDOW = false;
            if(args.Length > 4) {
                if(args[4].ToLower() == "-nw") {
                    NO_WINDOW = true;
                }
            }

            try {
                //ARGS
                osuDirectory = args[0];
                newDirectory = args[1];
                
                //Get list of folders
                string[] SongDirs = Directory.GetDirectories(osuDirectory);

                //
                totalSongs = SongDirs.Length;

                //PRELOOP SETUP
                //if (dupeCheckByID) { Console.WriteLine("Running with DupeCheck"); }

                //MAIN LOOP
                foreach (string s in SongDirs) {

                    count++;
                    if (!NO_WINDOW) {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"{count} of {totalSongs}");
                    }
                    //FRAMEWORK
                    List<Song> songs = new List<Song>();

                    switch (method) {
                        case Method.Simple: songs[0] = Main_FolderMethod_Simple(songs[0], s); break;
                        case Method.ini: songs = Main_iniMethod_Complex(songs, s); break;
                    }                   

                    WriteFile(songs);

                }

                //STATS
                if (!NO_WINDOW) {
                    Console.WriteLine();
                    Console.WriteLine("Successful file extractions: " + successfulCopies);
                    Console.WriteLine("Failed file extractions----: " + failedCopies);
                    Console.WriteLine("Skipped or existing files--: " + skippedOrExisting);
                    Console.WriteLine("------------------------------");
                }
                //Console.WriteLine("Press any key to end");
                //Console.ReadKey();
                Environment.Exit((int)Exit.Success);


            }
            catch (Exception e) {
                if (!NO_WINDOW) {
                    Console.WriteLine("An Exception has occurred, and been logged to ErrLog.log");
                }
                e.LogException();
                Environment.Exit((int)Exit.LoggedErr);
            }
        }

        
    }
}
