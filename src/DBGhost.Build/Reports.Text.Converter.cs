using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBGhost.Build.Extensions;
using System.Xml;
using System.Xml.Linq;

namespace DBGhost.Build.Reports.Text
{
    public class Converter
    {
        #region Private Fields

            private const string DATE_FORMAT = "yyyy-MM-ddThh:mm:ss";
            private const string ATTRIB_END = "end";
            private const string ATTRIB_START = "start";
            private const string ATTRIB_DURATION = "duration";

            private Boundary boundary;

        #endregion

        #region Events

            public event EventHandler<EventArgs<Boundary>> BeginBoundry;

        #endregion

        #region Constructor

            public Converter(string rootName)
            {
                boundary = new Boundary(
                    rootName,
                    n => true,
                    n => false);
            }

        #endregion

        #region Public Methods

            public Boundary AddBoundary(Boundary boundary)
            {
                this.boundary.Boundries.Add(boundary);
                return boundary;
            }

            public XDocument Convert(string path)
            {
                XDocument document = new XDocument();

                using (Reader reader = new Reader(path))
                {
                    XElement element;
                    Stack<Boundary> boundaries = new Stack<Boundary>();
                    Entry entry;

                    boundaries.Push(this.boundary);
                    element = document.Add<XElement>(new XElement(this.boundary.Name));

                    do
                    {
                        entry = reader.Read();

                        if (entry != null && !entry.IsEmpty)
                        {
                            Boundary boundary = boundaries.Peek();

                            if (!boundary.IsEnd(entry.Data) &&
                                boundary.Boundries.Count > 0 &&
                                boundary.Boundries.Exists(b => b.IsStart(entry.Data)))
                            {
                                boundary = boundary.Boundries.First(b => b.IsStart(entry.Data));
                                boundaries.Push(boundary);
                                element = element.Add<XElement>(new XElement(boundary.Name));
                                EnsureStartTimestamp(entry, element);
                                if (BeginBoundry != null) BeginBoundry(this, new EventArgs<Boundary>(boundary));
                            }

                            if (boundary.Boundries.Count == 0)
                                element.Add(new XCData(boundary.Format(entry.Data)));

                            if (boundary.IsEnd(entry.Data))
                            {
                                EnsureEndTimestamp(entry, boundary, element);
                                EnsureDuration(element);
                                element = element.Parent;
                                boundaries.Pop();
                            }
                        }
                    } while (entry != null);
                }

                return document;
            }

            private void EnsureStartTimestamp(Entry entry, XElement element)
            {
                if (entry.HasTimestamp)
                {
                    element.Add(new XAttribute(ATTRIB_START, entry.Timestamp.ToString(DATE_FORMAT)));
                    if (element.Parent.Attribute(ATTRIB_START) == null)
                        element.Parent.Add(new XAttribute(ATTRIB_START, entry.Timestamp.ToString(DATE_FORMAT)));
                } 
            }

            private void EnsureEndTimestamp(Entry entry, Boundary bounrary, XElement element)
            {
                if (entry.HasTimestamp && boundary.Boundries.Count > 0)
                    if (element.Attribute(ATTRIB_END) == null)
                        element.Add(new XAttribute(ATTRIB_END, entry.Timestamp.ToString(DATE_FORMAT)));
                
                if (entry.HasTimestamp)
                    if (element.Parent.Attribute(ATTRIB_END) == null)
                            element.Parent.Add(new XAttribute(ATTRIB_END, entry.Timestamp.ToString(DATE_FORMAT)));
                        else
                            element.Parent.Attribute(ATTRIB_END).Value = entry.Timestamp.ToString(DATE_FORMAT);
            }

            private void EnsureDuration(XElement element)
            {
                if (element.Parent.Attribute(ATTRIB_START) != null &&
                    element.Parent.Attribute(ATTRIB_END) != null)
                    if (element.Parent.Attribute(ATTRIB_DURATION) == null)
                        element.Parent.Add(new XAttribute(ATTRIB_DURATION,
                            (DateTime.Parse(element.Parent.Attribute(ATTRIB_END).Value) -
                            DateTime.Parse(element.Parent.Attribute(ATTRIB_START).Value)).ToString()));
                    else
                        element.Parent.Attribute(ATTRIB_DURATION).Value =
                            (DateTime.Parse(element.Parent.Attribute(ATTRIB_END).Value) -
                            DateTime.Parse(element.Parent.Attribute(ATTRIB_START).Value)).ToString();
            }

        #endregion
    }
}
