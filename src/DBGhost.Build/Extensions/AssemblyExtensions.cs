using System.Linq;
using System.IO;
using System.Reflection;

namespace DbGhost.Build.Extensions
{
    public static class AssemblyExtensions
    {
        public static Stream FindManifestResourceStream(this Assembly assembly, string name)
        {
            var names = assembly.GetManifestResourceNames();
            if (name != null && names.Length > 0)
            {
                var resourceName = names.OrderByDescending(n => n.Length).
                    FirstOrDefault(n => n.EndsWith(name));
                if (!string.IsNullOrEmpty(resourceName))
                    return assembly.GetManifestResourceStream(resourceName);
            }
            return null;
        }
    }
}
