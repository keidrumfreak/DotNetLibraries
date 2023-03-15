using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommonLib.IO;

namespace CommonLib.Logging
{
    public class TextLogger : ILogger
    {
        IFileSystem fileSystem;
        string folder;

        public TextLogger(IFileSystem fileSystem, string folder)
        {
            this.fileSystem = fileSystem;
            this.folder = folder;
        }

        public TextLogger(string folder) : this(new FileSystem(), folder)
        { }

        public void TraceLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            write(message);
        }

        public void TraceInformationLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            write($"[INFO]:{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            write(message);
        }

        public void TraceWarningLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            write($"[WARN]:{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            write(message);
        }

        public void TraceErrorLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            write($"[ERROR]:{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            write(message);
        }

        public void TraceExceptionLog<T>(T ex, string message = null, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1) where T : Exception
        {
            write($"[FATAL]:{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            write(message);
            write(ex.Message);
            write(ex.StackTrace);

            if (ex.InnerException != null)
            {
                TraceExceptionLog(ex.InnerException, null, filePath, memberName, lineNmber);
            }
        }

        private void write(string message)
        {
            if (!fileSystem.Directory.Exists(folder))
                fileSystem.Directory.CreateDirectory(folder);

            fileSystem.File.AppendAllText(PathUtil.Combine(folder, $"{DateTime.Now:yyyyMMdd}.log"), message + Environment.NewLine);
        }
    }
}
