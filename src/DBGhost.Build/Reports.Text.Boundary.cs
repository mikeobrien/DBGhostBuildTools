using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBGhost.Build.Reports.Text
{
    public class Boundary
    {
        #region Private Fields

            private string name;
            private Predicate<string> startMarker;
            private Predicate<string> endMarker;
            private List<Boundary> boundries;
            private Func<string, string> formatter;

        #endregion

        #region Constructor

            public Boundary(string name) : 
                this(name, e => true, e => true, new List<Boundary>(), null) {}

            public Boundary(
                string name, 
                Func<string, string> formatter) :
                this(name, e => true, e => true, new List<Boundary>(), formatter) { }

            public Boundary(
                string name,
                Predicate<string> startMarker) :
                this(name, startMarker, e => true, new List<Boundary>(), null) { }

            public Boundary(
                string name,
                Predicate<string> startMarker,
                Func<string, string> formatter) :
                this(name, startMarker, e => true, new List<Boundary>(), formatter) { }

            public Boundary(
                string name,
                Predicate<string> startMarker,
                Predicate<string> endMarker) :
                this(name, startMarker, endMarker, new List<Boundary>(), null) { }

            public Boundary(
                string name,
                Predicate<string> startMarker,
                Predicate<string> endMarker,
                Func<string, string> formatter) :
                this(name, startMarker, endMarker, new List<Boundary>(), formatter) { }

            public Boundary(
                string name,
                Predicate<string> startMarker,
                Predicate<string> endMarker,
                List<Boundary> boundries) :
                this(name, startMarker, endMarker, boundries, null) { }

            public Boundary(string name,
                Predicate<string> startMarker,
                Predicate<string> endMarker,
                List<Boundary> boundries,
                Func<string, string> formatter)
            {
                this.name = name;
                this.startMarker = startMarker;
                this.endMarker = endMarker;
                this.boundries = boundries;
                this.formatter = formatter;
            }

        #endregion

        #region Public Methods

            public string Name
            { get { return name; } }

            public bool IsStart(string value)
            {
                return startMarker(value);
            }

            public bool IsEnd(string value)
            {
                return endMarker(value);
            }

            public string Format(string value)
            {
                if (formatter != null)
                    return formatter(value);
                else
                    return value;
            }

            public List<Boundary> Boundries
            { get { return boundries; } }

        #endregion
    }
}
