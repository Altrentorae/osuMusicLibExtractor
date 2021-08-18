using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibGenerator {
    static class ExLogger {
        public static void LogException(this Exception ex) {
            string path = @"ErrLog.log";
            using (StreamWriter sw = File.AppendText(path)) {
                sw.WriteLine($"-----{DateTime.Now}-----");
                sw.WriteLine($"Exception thrown: {ex.ToString()}");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
