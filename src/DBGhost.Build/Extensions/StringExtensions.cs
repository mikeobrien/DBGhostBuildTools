using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DbGhost.Build.Extensions
{
    public static class StringExtensions
    {
        public static string[] Split(this string value, int limit, params string[] args)
        {
            return value.Split(args, StringSplitOptions.None);
        }

        public static string[] SplitLines(this string value)
        {
            return value.Split(new[] { "\r\n" }, StringSplitOptions.None);
        }

        private readonly static Regex ContainsAlphaRegex = new Regex(@"[a-zA-Z]+", RegexOptions.Compiled);

        public static bool ContainsAlpha(this string value)
        {
            return ContainsAlphaRegex.IsMatch(value);
        }

        public static bool StartsWithAny(this string value, StringComparison comparisonType, params string[] values)
        {
            return value != null && values.Any(comparand => value.StartsWith(comparand, comparisonType));
        }

        public static bool IsNullOrEmpty(this string value, bool ignoreWhitespace)
        {
            return value == null || IsEmpty(value, ignoreWhitespace);
        }

        public static bool IsEmpty(this string value, bool ignoreWhitespace)
        {
            if (value != null)
            {
                return
                    (!ignoreWhitespace && value.Length == 0) ||
                    (ignoreWhitespace && value.Trim().Length == 0);
            }
            return false;
        }

        public static string EnsureAbsolutePath(this string value, string path)
        {
            if (value != null)
            {
                return Path.IsPathRooted(value) ? value : Path.GetFullPath(Path.Combine(path, value));
            }
            return null;
        }

        public static string ToRelativePath(this string root, string path)
        {
            return path.StartsWith(root, StringComparison.OrdinalIgnoreCase) ? path.Substring(root.Length) : path;
        }

        public static string GetParentDirectoryName(this string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (directory == null) return null;

            var folders = directory.Split(new[] {Path.DirectorySeparatorChar});
            return folders[folders.GetUpperBound(0)];
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static string Coalesce<T>(this string value, T? newValue) where T : struct
        {
            return newValue.HasValue ? newValue.Value.ToString() : value;
        }
    }
}
