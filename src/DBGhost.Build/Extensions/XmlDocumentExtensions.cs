using System;
using System.Xml;

namespace DbGhost.Build.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static T GetValue<T>(this XmlDocument document, string xpath)
        {
            var value = GetValue(document, xpath);
            if (value == null) return default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string GetValue(this XmlDocument document, string xpath)
        {
            var node = document.SelectSingleNode(xpath);
            if (node != null && node.FirstChild != null) return node.FirstChild.Value;
            return null;
        }

        public static void SetValue<T>(this XmlDocument document, string xpath, T value)
        {
            SetValue(document, xpath, value.ToString());
        }

        public static void SetValue(this XmlDocument document, string xpath, string value)
        {
            if (!value.IsNullOrEmpty(true))
            {
                var node = document.SelectSingleNode(xpath);
                if (node != null)
                    if (node.FirstChild != null)
                        node.FirstChild.Value = value;
                    else
                        node.AppendChild(document.CreateTextNode(value));
            }
        }
    }
}
