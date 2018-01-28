using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ETL_System {

    // Utility Class that manages all Log actions
    public static class LogManager {


        public static void writeErrorToLog(string message, string log_path) {

        }

        public static void writeStartEvent(string message,string log_path) {
            string timestamp = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            string stamp = $"[{timestamp}]:[SERVER_START_EVENT]:{message}{Environment.NewLine}";
            if (File.Exists(log_path)) {
                File.AppendAllText(log_path, stamp);
            }
        }
    }
}
