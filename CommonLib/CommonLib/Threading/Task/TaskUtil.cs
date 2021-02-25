using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Logging;

namespace CommonLib.Threading.Tasks
{
    public static class TaskUtil
    {
        public static void FireAndForget(this Task task, ILogger logger = null)
        {
            task.ContinueWith(x => logger?.TraceExceptionLog(x.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
