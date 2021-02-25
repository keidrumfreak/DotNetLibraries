using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLib.IO
{
    public static class PathUtil
    {
        /// <summary>
        /// Path.Combineの置き換え
        /// ドライブルートとの結合、第二引数以降の先頭にセパレータがある際に直感的な挙動になるようにしたメソッド
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            var sb = new StringBuilder();
            var root = paths[0];
            sb.Append(root);
            if (root.Last() != Path.DirectorySeparatorChar)
                sb.Append(Path.DirectorySeparatorChar);
            foreach (var path in paths.Skip(1))
            {
                var tmp = path;
                while (tmp[0] == Path.DirectorySeparatorChar)
                    tmp = tmp.Substring(1);
                sb.Append(tmp);
                if (tmp.Last() != Path.DirectorySeparatorChar)
                    sb.Append(Path.DirectorySeparatorChar);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetCurrentAppDir()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// ドライブルート、プロトコルを含まないパスに不正なファイル名が存在するかチェックします
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsInvalidPathWithoutRoot(string path)
        {
            return path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Any(entity => IsInvalidFileName(entity));
        }

        /// <summary>
        /// 指定したファイル名に不正な文字が含まれている、あるいは予約語であるかチェックする
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsInvalidFileName(string name)
        {
            return Regex.IsMatch(name, @"[\x00-\x1f<>:""/\\\|?*]|^(CON|PRN|AUX|NUL|CLOCK\$|COM[0-9]|LPT[0-9])(\.|$)");
        }
    }
}
