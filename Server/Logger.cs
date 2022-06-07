using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Logger
    {
        private static string _logsPath;
        public static int? SeverityToLog { get; set; } = null;

        public static void  ConfigureLogger(int severityToLog)
        {
            SeverityToLog = severityToLog;
            _logsPath = @$"{Directory.GetCurrentDirectory()}/Logs/{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm")}.txt";
            File.Create(_logsPath);
        }

        public static void WriteData(int severity, string category, string message)
        {
            if (SeverityToLog is null)
                return;

            if (SeverityToLog.Value == severity)
                File.AppendAllText(_logsPath, $"{DateTime.UtcNow.ToString("hh:mm")}|{severity}|{category}|{message}\n\r");
        }

        
    }
}
