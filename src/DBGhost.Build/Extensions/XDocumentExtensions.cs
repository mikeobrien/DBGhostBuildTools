using System.Xml.Linq;

namespace DbGhost.Build.Extensions
{
    public static class XDocumentExtensions
    {
        public static T Add<T>(this XDocument document, T element)
        {
            document.Add(element);
            return element;
        }
    }
}
