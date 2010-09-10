using System;
using System.Collections.Generic;
using System.Linq;
using DbGhost.Build.Extensions;
using System.Xml.Linq;

namespace DbGhost.Build.Reports.Text
{
    public class Converter
    {
        private const string DateFormat = "yyyy-MM-ddThh:mm:ss";
        private const string AttribEnd = "end";
        private const string AttribStart = "start";
        private const string AttribDuration = "duration";

        private readonly Boundary _boundary;

        public event EventHandler<EventArgs<Boundary>> BeginBoundry;

        public Converter(string rootName)
        {
            _boundary = new Boundary(
                rootName,
                n => true,
                n => false);
        }

        public Boundary AddBoundary(Boundary boundary)
        {
            _boundary.Boundries.Add(boundary);
            return boundary;
        }

        public XDocument Convert(string path)
        {
            var document = new XDocument();

            using (var reader = new Reader(path))
            {
                var boundaries = new Stack<Boundary>();
                Entry entry;

                boundaries.Push(_boundary);
                var element = document.Add<XElement>(new XElement(_boundary.Name));

                do
                {
                    entry = reader.Read();

                    if (entry == null || entry.IsEmpty) continue;

                    var entryData = entry.Data;

                    var boundary = boundaries.Peek();

                    if (!boundary.IsEnd(entry.Data) &&
                        boundary.Boundries.Count > 0 &&
                        boundary.Boundries.Exists(b => b.IsStart(entryData)))
                    {
                        boundary = boundary.Boundries.First(b => b.IsStart(entryData));
                        boundaries.Push(boundary);
                        element = element.Add<XElement>(new XElement(boundary.Name));
                        EnsureStartTimestamp(entry, element);
                        if (BeginBoundry != null) BeginBoundry(this, new EventArgs<Boundary>(boundary));
                    }

                    if (element == null) continue;

                    if (boundary.Boundries.Count == 0)
                        element.Add(new XCData(boundary.Format(entry.Data)));

                    if (!boundary.IsEnd(entry.Data)) continue;

                    EnsureEndTimestamp(entry, element);
                    EnsureDuration(element);
                    element = element.Parent;
                    boundaries.Pop();
                } while (entry != null);
            }

            return document;
        }

        private static void EnsureStartTimestamp(Entry entry, XElement element)
        {
            if (entry.HasTimestamp)
            {
                element.Add(new XAttribute(AttribStart, entry.Timestamp.ToString(DateFormat)));
                if (element.Parent != null && element.Parent.Attribute(AttribStart) == null)
                    element.Parent.Add(new XAttribute(AttribStart, entry.Timestamp.ToString(DateFormat)));
            } 
        }

        private void EnsureEndTimestamp(Entry entry, XElement element)
        {
            if (entry.HasTimestamp && _boundary.Boundries.Count > 0)
                if (element.Attribute(AttribEnd) == null)
                    element.Add(new XAttribute(AttribEnd, entry.Timestamp.ToString(DateFormat)));
                
            if (entry.HasTimestamp)
                if (element.Parent != null && element.Parent.Attribute(AttribEnd) == null)
                    element.Parent.Add(new XAttribute(AttribEnd, entry.Timestamp.ToString(DateFormat)));
                else if (element.Parent != null && element.Parent.Attributes(AttribEnd).Any())
                    element.Parent.Attributes(AttribEnd).First().Value = entry.Timestamp.ToString(DateFormat);
        }

        private static void EnsureDuration(XElement element)
        {
            if (element.Parent == null) return;

            if (element.Parent.Attribute(AttribStart) != null &&
                element.Parent.Attribute(AttribEnd) != null)
                if (element.Parent.Attribute(AttribDuration) == null)
                    element.Parent.Add(new XAttribute(AttribDuration,
                                                        (DateTime.Parse(element.Parent.Attributes(AttribEnd).First().Value) -
                                                        DateTime.Parse(element.Parent.Attributes(AttribStart).First().Value)).ToString()));
                else
                    element.Parent.Attributes(AttribDuration).First().Value =
                        (DateTime.Parse(element.Parent.Attributes(AttribEnd).First().Value) -
                            DateTime.Parse(element.Parent.Attributes(AttribStart).First().Value)).ToString();
        }
    }
}
