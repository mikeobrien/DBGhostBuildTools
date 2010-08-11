using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBGhost.Build.Reports.Text
{
    internal class Entry
    {
        #region Private Fields

            private bool hasTimestamp;
            private DateTime timestamp;
            private string data;

        #endregion

        #region Constructor

            internal Entry()
            {
                hasTimestamp = false;
                this.data = string.Empty;
            }

            internal Entry(string data)
            {
                hasTimestamp = false;
                this.data = data;
            }

            internal Entry(string timestamp, string data)
            {
                hasTimestamp = DateTime.TryParse(timestamp, out this.timestamp);
                this.data = data;
            }

        #endregion

        #region Public Fields

            public bool IsEmpty
            { get { return (string.IsNullOrEmpty(data) && !hasTimestamp); } }

            public bool HasTimestamp
            { get { return hasTimestamp; } }

            public DateTime Timestamp
            { get { return timestamp; } }

            public string Data
            { get { return data; } }

        #endregion
    }
}
