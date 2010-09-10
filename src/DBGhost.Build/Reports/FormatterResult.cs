using System.Xml.Linq;

namespace DbGhost.Build.Reports
{
    public class FormatterResult
    {
        public FormatterResult(XDocument report, bool hasErrors)
        {
            Report = report;
            HasErrors = hasErrors;
        }

        public XDocument Report { get; private set; }
        public bool HasErrors { get; private set; }
    }
}
