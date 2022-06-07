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
        private static string _workingDirectory = @$"{Directory.GetCurrentDirectory()}\Logs\";
        public static int? SeverityToLog { get; set; } = null;

        public static void ConfigLogger(int severityToLog)
        {
            SeverityToLog = severityToLog;
            _logsPath = $"{_workingDirectory + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm")}.txt";
            File.Create(_logsPath);
        }

        public static void WriteData(int severity, string category, string message)
        {
            if (SeverityToLog is null)
                return;

            if (SeverityToLog.Value == severity)
                File.AppendAllText(_logsPath, $"{DateTime.UtcNow.ToString("hh:mm")}|{severity}|{category}|{message}\n\r");
        }

        public static void ClearLogs()
        {
            foreach (string logName in Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Logs"))
            {
                File.Delete(_workingDirectory + logName);
            }
        }

        public static void ClearLogs(DateTime dateTime)
        {
            foreach (string logName in Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Logs", dateTime.ToString("yyyy-MM-dd hh:mm")+"*"))
            {
                File.Delete(_workingDirectory + logName);
            }
        }
    }
}
