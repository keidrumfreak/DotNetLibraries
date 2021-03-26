using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Win32.WNet
{
    public interface IWinnetwk
    {
        WinError WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        WinError WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

        WinError WNetGetUniversalName(string lpLocalPath, InfoLevel dwInfoLevel, IntPtr lpBuffer, ref int lpBufferSize);
    }
}
