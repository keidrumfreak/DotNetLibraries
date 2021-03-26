using System;
using System.Runtime.InteropServices;

namespace CommonLib.Win32.WNet
{
    class Winnetwk : IWinnetwk
    {
        public WinError WNetCancelConnection2(string lpName, int dwFlags, bool fForce)
        {
            return NativeMethods.WNetCancelConnection2(lpName, dwFlags, fForce);
        }

        public WinError WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags)
        {
            return NativeMethods.WNetAddConnection2(ref lpNetResource, lpPassword, lpUsername, dwFlags);
        }

        public WinError WNetGetUniversalName(string lpLocalPath, InfoLevel dwInfoLevel, IntPtr lpBuffer, ref int lpBufferSize)
        {
            return NativeMethods.WNetGetUniversalName(lpLocalPath, dwInfoLevel, lpBuffer, ref lpBufferSize);
        }

        class NativeMethods
        {
            const string dllName = "mpr.dll";

            [DllImport(dllName, CharSet = CharSet.Unicode)]
            public static extern WinError WNetCancelConnection2(string lpName, int dwFlags, bool fForce);
            [DllImport(dllName, CharSet = CharSet.Unicode)]
            public static extern WinError WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);
            [DllImport(dllName, CharSet = CharSet.Unicode)]
            public static extern WinError WNetGetUniversalName(string lpLocalPath, [MarshalAs(UnmanagedType.U4)] InfoLevel dwInfoLevel, IntPtr lpBuffer, [MarshalAs(UnmanagedType.U4)] ref int lpBufferSize);
        }
    }
}
