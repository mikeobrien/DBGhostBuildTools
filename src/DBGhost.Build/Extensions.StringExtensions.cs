using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DBGhost.Build.Extensions
{
    public static class StringExtensions
    {
        public static bool StartsWithAny(this string value, string[] values, StringComparison comparisonType)
        {
            if (value == null) return false;
            foreach (string comparand in values)
                if (value.StartsWith(comparand, comparisonType)) return true;
            return false;
        }

        public static bool IsNullOrEmpty(this string value, bool ignoreWhitespace)
        {
            if (value != null)
            {
                return IsEmpty(value, ignoreWhitespace);
            }
            return true;
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
                if (Path.IsPathRooted(value))
                    return value;
                else
                    return Path.GetFullPath(Path.Combine(path, value));
            }
            return null;
        }

        public static string ToRelativePath(this string root, string path)
        {
            if (path.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                return path.Substring(root.Length);
            else
                return path;
        }

        public static string GetParentDirectoryName(this string path)
        {
            string[] folders = Path.GetDirectoryName(path).Split(new char[] {Path.DirectorySeparatorChar});
            return folders[folders.GetUpperBound(0)];
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static string Coalesce<T>(this string value, Nullable<T> newValue) where T : struct
        {
            if (newValue.HasValue)
                return newValue.Value.ToString();
            else
                return value;
        }
    }
}
