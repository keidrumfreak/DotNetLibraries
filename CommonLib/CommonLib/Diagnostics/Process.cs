using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace CommonLib.Diagnostics
{
    /// <inheritdoc cref="Process"/>
    public interface IProcess : IComponent, IDisposable
    {
        /// <inheritdoc cref="Process.PriorityClass"/>
        ProcessPriorityClass PriorityClass { get; set; }

        /// <inheritdoc cref="Process.PriorityBoostEnabled"/>
        bool PriorityBoostEnabled { get; set; }

        /// <inheritdoc cref="Process.PeakVirtualMemorySize64"/>
        long PeakVirtualMemorySize64 { get; }

        /// <inheritdoc cref="Process.PeakVirtualMemorySize"/>
        int PeakVirtualMemorySize { get; }

        /// <inheritdoc cref="Process.PeakWorkingSet64"/>
        long PeakWorkingSet64 { get; }

        /// <inheritdoc cref="Process.PeakWorkingSet"/>
        int PeakWorkingSet { get; }

        /// <inheritdoc cref="Process.PeakPagedMemorySize64"/>
        long PeakPagedMemorySize64 { get; }

        /// <inheritdoc cref="Process.PrivateMemorySize"/>
        int PrivateMemorySize { get; }

        /// <inheritdoc cref="Process.PeakPagedMemorySize"/>
        int PeakPagedMemorySize { get; }

        /// <inheritdoc cref="Process.PagedSystemMemorySize"/>
        int PagedSystemMemorySize { get; }

        /// <inheritdoc cref="Process.PagedMemorySize64"/>
        long PagedMemorySize64 { get; }

        /// <inheritdoc cref="Process.PagedMemorySize"/>
        int PagedMemorySize { get; }

        /// <inheritdoc cref="Process.NonpagedSystemMemorySize64"/>
        long NonpagedSystemMemorySize64 { get; }

        /// <inheritdoc cref="Process.NonpagedSystemMemorySize"/>
        int NonpagedSystemMemorySize { get; }

        /// <inheritdoc cref="Process.Modules"/>
        ProcessModuleCollection Modules { get; }

        /// <inheritdoc cref="Process.MinWorkingSet"/>
        IntPtr MinWorkingSet { get; set; }

        /// <inheritdoc cref="Process.PagedSystemMemorySize64"/>
        long PagedSystemMemorySize64 { get; }

        /// <inheritdoc cref="Process.PrivateMemorySize64"/>
        long PrivateMemorySize64 { get; }

        /// <inheritdoc cref="Process.PrivilegedProcessorTime"/>
        TimeSpan PrivilegedProcessorTime { get; }

        /// <inheritdoc cref="Process.ProcessName"/>
        string ProcessName { get; }

        /// <inheritdoc cref="Process.WorkingSet64"/>
        long WorkingSet64 { get; }

        /// <inheritdoc cref="Process.WorkingSet"/>
        int WorkingSet { get; }

        /// <inheritdoc cref="Process.StandardError"/>
        StreamReader StandardError { get; }

        /// <inheritdoc cref="Process.StandardOutput"/>
        StreamReader StandardOutput { get; }

        /// <inheritdoc cref="Process.StandardInput"/>
        StreamWriter StandardInput { get; }

        /// <inheritdoc cref="Process.EnableRaisingEvents"/>
        bool EnableRaisingEvents { get; set; }

        /// <inheritdoc cref="Process.VirtualMemorySize64"/>
        long VirtualMemorySize64 { get; }

        /// <inheritdoc cref="Process.VirtualMemorySize"/>
        int VirtualMemorySize { get; }

        /// <inheritdoc cref="Process.UserProcessorTime"/>
        TimeSpan UserProcessorTime { get; }

        /// <inheritdoc cref="Process.TotalProcessorTime"/>
        TimeSpan TotalProcessorTime { get; }

        /// <inheritdoc cref="Process.Threads"/>
        ProcessThreadCollection Threads { get; }

        /// <inheritdoc cref="Process.SynchronizingObject"/>
        ISynchronizeInvoke SynchronizingObject { get; set; }

        /// <inheritdoc cref="Process.StartTime"/>
        DateTime StartTime { get; }

        /// <inheritdoc cref="Process.StartInfo"/>
        ProcessStartInfo StartInfo { get; set; }

        /// <inheritdoc cref="Process.SessionId"/>
        int SessionId { get; }

        /// <inheritdoc cref="Process.Responding"/>
        bool Responding { get; }

        /// <inheritdoc cref="Process.ProcessorAffinity"/>
        IntPtr ProcessorAffinity { get; set; }

        /// <inheritdoc cref="Process.MaxWorkingSet"/>
        IntPtr MaxWorkingSet { get; set; }

        /// <inheritdoc cref="Process.MainModule"/>
        ProcessModule MainModule { get; }

        /// <inheritdoc cref="Process.MainWindowTitle"/>
        string MainWindowTitle { get; }

        /// <inheritdoc cref="Process.MachineName"/>
        string MachineName { get; }

        /// <inheritdoc cref="Process.Id"/>
        int Id { get; }

        /// <inheritdoc cref="Process.HandleCount"/>
        int HandleCount { get; }

        /// <inheritdoc cref="Process.SafeHandle"/>
        SafeProcessHandle SafeHandle { get; }

        /// <inheritdoc cref="Process.Handle"/>
        IntPtr Handle { get; }

        /// <inheritdoc cref="Process.ExitTime"/>
        DateTime ExitTime { get; }

        /// <inheritdoc cref="Process.HasExited"/>
        bool HasExited { get; }

        /// <inheritdoc cref="Process.ExitCode"/>
        int ExitCode { get; }

        /// <inheritdoc cref="Process.BasePriority"/>
        int BasePriority { get; }

        /// <inheritdoc cref="Process.MainWindowHandle"/>
        IntPtr MainWindowHandle { get; }


        /// <inheritdoc cref="Process.ErrorDataReceived"/>
        event DataReceivedEventHandler ErrorDataReceived;

        /// <inheritdoc cref="Process.OutputDataReceived"/>
        event DataReceivedEventHandler OutputDataReceived;

        /// <inheritdoc cref="Process.Exited"/>
        event EventHandler Exited;


        /// <inheritdoc cref="Process.BeginErrorReadLine"/>
        void BeginErrorReadLine();

        /// <inheritdoc cref="Process.BeginOutputReadLine"/>
        void BeginOutputReadLine();

        /// <inheritdoc cref="Process.CancelErrorRead"/>
        void CancelErrorRead();

        /// <inheritdoc cref="Process.CancelOutputRead"/>
        void CancelOutputRead();

        /// <inheritdoc cref="Process.Close"/>
        void Close();

        /// <inheritdoc cref="Process.CloseMainWindow"/>
        bool CloseMainWindow();

        /// <inheritdoc cref="Process.Kill"/>
        void Kill();

        /// <inheritdoc cref="Process.Refresh"/>
        void Refresh();

        /// <inheritdoc cref="Process.Start"/>
        bool Start();

        /// <inheritdoc cref="Process.WaitForExit(int)"/>
        bool WaitForExit(int milliseconds);

        /// <inheritdoc cref="Process.WaitForExit"/>
        void WaitForExit();

        /// <inheritdoc cref="Process.WaitForInputIdle(int)"/>
        bool WaitForInputIdle(int milliseconds);

        /// <inheritdoc cref="Process.WaitForInputIdle(int)"/>
        bool WaitForInputIdle();
    }

    /// <summary>
    /// System.Diagnostics.Processクラスのラッパ
    /// </summary>
    class ProcessWrapper : Process, IProcess
    {
    }
}
