using System;
using System.Runtime.InteropServices;

namespace CommonLib.Win32.WNet
{
    public class WNetProvider
    {
        public static IWinnetwk Winnetwk = new Winnetwk();

        public static string GetUniversalName(string path, int size = 1000)
        {
            if (new Uri(path).IsUnc)
                return path;

            var bufSize = size;
            var buffer = Marshal.AllocCoTaskMem(bufSize);
            try
            {
                var error = Winnetwk.WNetGetUniversalName(path, InfoLevel.Universal, buffer, ref bufSize);
                switch (error)
                {
                    case WinError.NO_ERROR:
                        break;
                    case WinError.ERROR_MORE_DATA:
                        return GetUniversalName(path, bufSize);
                    case WinError.ERROR_BAD_DEVICE:     // ローカルパス
                    case WinError.ERROR_NOT_CONNECTED:  // 既にUNC
                        return path;
                    default:
                        throw new Exception(error.ToString());
                }
                var info = (UNIVERSAL_NAME_INFO)Marshal.PtrToStructure(buffer, typeof(UNIVERSAL_NAME_INFO));
                return info.lpUniversalName;
            }
            finally
            {
                Marshal.FreeCoTaskMem(buffer);
            }
        }

        public static void MontNetworkDrive(string localName, string remoteName, string userID, string password)
        {
            var netResource = new NETRESOURCE
            {
                dwScope = 0,
                dwType = 1,
                dwDisplayType = 0,
                dwUsage = 0,
                lpLocalName = localName,
                lpRemoteName = remoteName,
                lpComment = string.Empty,
                lpProvider = string.Empty
            };

            // 一旦接続解除
            Winnetwk.WNetCancelConnection2(remoteName, 0, true);

            // ユーザID,およびパスワードが空の場合nullを渡す
            var id = string.IsNullOrEmpty(userID) ? null : userID;
            var pass = string.IsNullOrEmpty(password) ? null : password;

            var error = Winnetwk.WNetAddConnection2(ref netResource, pass, id, 0);
            if (error != WinError.NO_ERROR)
                throw new Exception(error.ToString());
        }
    }
}
