using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace MusicLibGenerator {
    class Song {
        public Song() {
            ShouldSkip = false;
        }
        public string path { get; set; }
        public string imgPath { get; set; }
        public string ID { get; set; }
        public byte[] IDHash { get; private set; }
        public string newDir { get; set; }
        public string fileName { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public long HOLength { get; set; }
        public string Album { get; set; }
        public string DiffName { get; set; }
        public bool ShouldSkip { get; set; }

        public void SetSongHash()
        {
            if(IDHash != null) { throw new Exception("Value already set"); }
            byte[] dataArray = Encoding.UTF8.GetBytes(Artist + Title + HOLength.ToString());

            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(dataArray);

            IDHash = result;
        }
    }
}
