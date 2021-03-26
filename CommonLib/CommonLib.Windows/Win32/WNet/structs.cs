using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Win32.WNet
{
    [StructLayout(LayoutKind.Sequential)]
    struct REMOTE_NAME_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpUniversalName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpConnectionName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpRemainingPath;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct UNIVERSAL_NAME_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpUniversalName;
    }

    //APIに渡す接続情報
    [StructLayout(LayoutKind.Sequential)]
    public struct NETRESOURCE
    {
        public int dwScope;//列挙の範囲
        public int dwType;//リソースタイプ
        public int dwDisplayType;//表示オブジェクト
        public int dwUsage;//リソースの使用方法
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpLocalName;//ローカルデバイス名。使わないならNULL。
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpRemoteName;//リモートネットワーク名。使わないならNULL
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpComment;//ネットワーク内の提供者に提供された文字列
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpProvider;//リソースを所有しているプロバイダ名
    }
}
