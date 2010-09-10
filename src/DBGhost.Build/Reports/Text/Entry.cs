using System;

namespace DbGhost.Build.Reports.Text
{
    internal class Entry
    {
        private readonly bool _hasTimestamp;
        private readonly DateTime _timestamp;
        private readonly string _data;

        internal Entry()
        {
            _hasTimestamp = false;
            _data = string.Empty;
        }

        internal Entry(string data)
        {
            _hasTimestamp = false;
            _data = data;
        }

        internal Entry(string timestamp, string data)
        {
            _hasTimestamp = DateTime.TryParse(timestamp, out _timestamp);
            _data = data;
        }

        public bool IsEmpty
        { get { return (string.IsNullOrEmpty(_data) && !_hasTimestamp); } }

        public bool HasTimestamp
        { get { return _hasTimestamp; } }

        public DateTime Timestamp
        { get { return _timestamp; } }

        public string Data
        { get { return _data; } }
    }
}
