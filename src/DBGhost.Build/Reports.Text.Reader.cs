using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DBGhost.Build.Reports.Text
{
    internal class Reader : IDisposable 
    {
        #region Private Fields

            private TextReader reader;

        #endregion

        #region Constructor

            public Reader(string path)
            {
                reader = new StreamReader(path);
            }

        #endregion

        #region Public Methods

            public Entry Read()
            {
                string data;
                string[] fields;

                data = reader.ReadLine();

                if (data != null)
                {
                    data = data.Trim();
                    fields = data.Split(new string[] { "..." }, 2, StringSplitOptions.None);
                    switch (fields.Length)
                    {
                        case 1: return new Entry(data);
                        case 2: return new Entry(fields[0].Trim(), fields[1].Trim());
                        default: return new Entry(data);
                    }
                }
                else return null;
            }

        #endregion

        #region IDisposable Members

            public void Dispose()
            {
                if (reader != null) reader.Dispose();
            }

        #endregion
    }
}
