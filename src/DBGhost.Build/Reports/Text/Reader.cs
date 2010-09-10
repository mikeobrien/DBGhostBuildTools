using System;
using System.IO;

namespace DbGhost.Build.Reports.Text
{
    internal class Reader : IDisposable 
    {
        private readonly TextReader _reader;

        public Reader(string path)
        {
            _reader = new StreamReader(path);
        }

        public Entry Read()
        {
            string[] fields;

            var data = _reader.ReadLine();

            if (data != null)
            {
                data = data.Trim();
                fields = data.Split(new [] { "..." }, 2, StringSplitOptions.None);
                switch (fields.Length)
                {
                    case 1: return new Entry(data);
                    case 2: return new Entry(fields[0].Trim(), fields[1].Trim());
                    default: return new Entry(data);
                }
            }
            return null;
        }

        public void Dispose()
        {
            if (_reader != null) _reader.Dispose();
        }
    }
}
