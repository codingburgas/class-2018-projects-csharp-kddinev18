using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Logger
    {
        private string _logsPath;
        private string _workingDirectory = @$"{Directory.GetCurrentDirectory()}\Logs\";
        public int? SeverityToLog { get; set; } = null;

        public Logger(int severityToLog)
        {
            SeverityToLog = severityToLog;
            _logsPath = $"{_workingDirectory + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm")}.txt";
            File.Create(_logsPath);
        }

        public void WriteData(int severity, string category, string message)
        {
            if (SeverityToLog is null)
                return;

            if (SeverityToLog.Value == severity)
                File.AppendAllText(_logsPath, $"{DateTime.UtcNow.ToString("hh:mm")}|{severity}|{category}|{message}\n\r");
        }

        public void ClearLogs()
        {
            foreach (string logName in Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Logs"))
            {
                File.Delete(_workingDirectory + logName);
            }
        }
    }
}
