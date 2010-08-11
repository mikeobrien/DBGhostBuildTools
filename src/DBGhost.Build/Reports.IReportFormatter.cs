using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DBGhost.Build.Reports
{
    public interface IReportFormatter
    {
        FormatterResult Load(string path);
    }
}
