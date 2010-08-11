using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DBGhost.Build.Extensions
{
    static class MiscExtensions
    {
        #region Global

            public static T Coalesce<T>(this T value, T newValue) where T : class
            {
                if (newValue != null)
                    return newValue;
                else
                    return value;
            }

        #endregion

        #region XDocument

            public static T Add<T>(this XDocument document, T element)
            {
                document.Add(element);
                return element;
            }

        #endregion

        #region XElement

            public static T Add<T>(this XElement parent, T element)
            {
                parent.Add(element);
                return element;
            }

        #endregion

        #region XmlDocument

            public static T GetValue<T>(this System.Xml.XmlDocument document, string xpath)
            {
                string value = GetValue(document, xpath);
                if (value == null)
                    return default(T);
                else
                    return (T)Convert.ChangeType(value, typeof(T));
            }

            public static string GetValue(this System.Xml.XmlDocument document, string xpath)
            {
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null && node.FirstChild != null)
                    return node.FirstChild.Value;
                else
                    return null;
            }

            public static void SetValue<T>(this System.Xml.XmlDocument document, string xpath, T value)
            {
                SetValue(document, xpath, value.ToString());
            }

            public static void SetValue(this System.Xml.XmlDocument document, string xpath, string value)
            {
                if (!value.IsNullOrEmpty(true))
                {
                    XmlNode node = document.SelectSingleNode(xpath);
                    if (node != null)
                        if (node.FirstChild != null)
                            node.FirstChild.Value = value;
                        else
                            node.AppendChild(document.CreateTextNode(value));
                }
            }

        #endregion
    }
}
