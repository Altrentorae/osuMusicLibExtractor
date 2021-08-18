using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace MusicLibGenerator {
    class Song {
        public Song() {
            ShouldSkip = false;
        }
        public string path { get; set; }
        public string ID { get; set; }
        public string newDir { get; set; }
        public string fileName { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string DiffName { get; set; }
        public bool ShouldSkip { get; set; }

        
    }
}
