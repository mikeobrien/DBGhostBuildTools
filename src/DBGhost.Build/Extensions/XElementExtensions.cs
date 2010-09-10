using System.Xml.Linq;

namespace DbGhost.Build.Extensions
{
    public static class XElementExtensions
    {
        public static T Add<T>(this XElement parent, T element)
        {
            parent.Add(element);
            return element;
        }
    }
}
