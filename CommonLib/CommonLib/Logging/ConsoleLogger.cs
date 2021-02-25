using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CommonLib.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void TraceLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            Console.WriteLine(message);
        }

        public void TraceInformationLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            Console.WriteLine(message);
        }

        public void TraceWarningLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            Console.WriteLine(message);
        }

        public void TraceErrorLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1)
        {
            Console.WriteLine(message);
        }

        public void TraceExceptionLog<T>(T ex, string message = null, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNmber = -1) where T : Exception
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                TraceExceptionLog(ex, message, filePath, memberName, lineNmber);
            }
            else
            {
                TraceErrorLog(message, filePath, memberName, lineNmber);
            }
        }
    }
}
