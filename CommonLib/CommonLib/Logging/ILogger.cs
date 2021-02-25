using System;
using System.Runtime.CompilerServices;

namespace CommonLib.Logging
{
    public interface ILogger
    {
        void TraceLog(string message, [CallerFilePath]string filePath = "", [CallerMemberName]string memberName = "", [CallerLineNumber]int lineNmber = -1);

        void TraceInformationLog(string message, [CallerFilePath]string filePath = "", [CallerMemberName]string memberName = "", [CallerLineNumber]int lineNmber = -1);

        void TraceWarningLog(string message, [CallerFilePath]string filePath = "", [CallerMemberName]string memberName = "", [CallerLineNumber]int lineNmber = -1);

        void TraceErrorLog(string message, [CallerFilePath]string filePath = "", [CallerMemberName]string memberName = "", [CallerLineNumber]int lineNmber = -1);

        void TraceExceptionLog<T>(T ex, string message = null, [CallerFilePath]string filePath = "", [CallerMemberName]string memberName = "", [CallerLineNumber]int lineNmber = -1)
            where T : Exception;
    }
}
