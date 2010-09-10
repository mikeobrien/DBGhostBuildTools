using System.Xml.Linq;

namespace DbGhost.Build.Reports
{
    public class Report
    {
        private XDocument _report;
        private bool _hasErrors;

        public Report(string reportPath, IReportFormatter formatter, string configurationPath)
        {
            Load(reportPath, formatter, configurationPath);
        }

        public bool HasErrors
        { get { return _hasErrors; } }

        public void Save(string path)
        {
            _report.Save(path);
        }

        private void Load(string reportPath, IReportFormatter formatter, string configurationPath)
        {
            var formatterResult = formatter.Load(reportPath);
            var configuration = XDocument.Load(configurationPath);

            var report = new XDocument(
                new XElement(
                    "dbGhost",
                    new XAttribute("errors", formatterResult.HasErrors),
                    formatterResult.Report.Elements(),
                    configuration.Elements()));

            _report = report;
            _hasErrors = formatterResult.HasErrors;
        }
    }
}
