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
        public static void LogErr(string message)
        {
            string path = @"ErrLog.log";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"-----{DateTime.Now}-----");
                sw.WriteLine($"Exception thrown: {message}");
#if DEBUG
                Console.WriteLine(message);
#endif
            }
        }

        public static bool LoggingEnabled = true;

        public static void Log(string title, string message)
        {
            if (!LoggingEnabled) { return; }
            string path = @"Log.log";
            using (StreamWriter sw = File.AppendText(path))
            {
                if (title != null) { sw.WriteLine($"{DateTime.Now} - {title}: {message}"); }
                else { sw.WriteLine($"{DateTime.Now} - Message Logged: {message}"); }
#if DEBUG
                if (title != null) { Console.WriteLine($"{DateTime.Now} - {title}: {message}"); }
                else { Console.WriteLine($"{DateTime.Now} - Message Logged: {message}"); }
#endif
            }
        }

        public static void LogSeparator(string message)
        {
            if (!LoggingEnabled) { return; }
            string path = @"Log.log";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"----- {DateTime.Now} - {message} -----");
#if DEBUG
                Console.WriteLine($"----- {DateTime.Now} - {message} -----");
#endif
            }
        }

        public static void LogNewline()
        {
            if (!LoggingEnabled) { return; }
            string path = @"Log.log";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("");
            }
        }
    }
}
