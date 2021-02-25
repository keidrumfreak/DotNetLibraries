using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace CommonLib.Logging
{
    public class EventLogger : ILogger
    {
        static EventLogger instance;
        public static EventLogger Instance
        {
            get { return instance ?? (instance = new EventLogger()); }
            set { instance = value; }
        }

        public string LogName { get; set; }
        public string Source { get; set; }

        public EventLogger() : this("Application", "Application") { }

        public EventLogger(string source) : this("Application", source) { }

        public EventLogger(string logName, string source)
        {
            LogName = logName;
            Source = source;
        }

        public void TraceErrorLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1)
        {
            writeEntry(text(message, filePath, memberName, lineNumber), EventLogEntryType.Error);
        }

        public void TraceExceptionLog<T>(T ex, string message = null, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1) where T : Exception
        {
            var msg = string.IsNullOrEmpty(message) ? string.Empty : $"{message}{Environment.NewLine}";

            msg = msg
                + $"[Exception]{Environment.NewLine}"
                + $"Type:{Environment.NewLine}    {ex.GetType().Name}{Environment.NewLine}"
                + $"Message:{Environment.NewLine}    {ex.Message}{Environment.NewLine}"
                + $"StackTrace:{Environment.NewLine}{ex.StackTrace}";

            if (ex.InnerException != null)
            {
                TraceExceptionLog(ex.InnerException, msg, filePath, memberName, lineNumber);
            }
            else
            {
                writeEntry(text(msg, filePath, memberName, lineNumber), EventLogEntryType.Error);
            }
        }

        public void TraceInformationLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1)
        {
            writeEntry(text(message, filePath, memberName, lineNumber), EventLogEntryType.Information);
        }

        public void TraceLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1)
        {
            TraceInformationLog(message, filePath, memberName, lineNumber);
        }

        public void TraceWarningLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1)
        {
            writeEntry(text(message, filePath, memberName, lineNumber), EventLogEntryType.Warning);
        }

        private string text(string message, string filePath, string memberName, int lineNumber)
        {
            return $"[{Path.GetFileNameWithoutExtension(filePath)}.{memberName} line:{lineNumber}]{Environment.NewLine}{message}";
        }

        private void writeEntry(string message, EventLogEntryType type)
        {
            try
            {
                var logger = new EventLog(LogName, ".", Source);
                logger.WriteEntry(message, type);
            }
            catch
            {
                // とりあえずApplicationログに書き込む
                var logger = new EventLog("Application", ".", "Application");
                var msg = $"[{LogName}][{Source}]{Environment.NewLine}{message}";
                logger.WriteEntry(msg, type);
            }
        }
    }
}
