using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DBGhost.Build.Reports
{
    public class Report
    {
        #region Private Fields

            private XDocument report;
            private bool hasErrors;

        #endregion

        #region Constructor

            public Report(string reportPath, IReportFormatter formatter, string configurationPath)
            {
                Load(reportPath, formatter, configurationPath);
            }

        #endregion

        #region Public Properties

            public bool HasErrors
            { get { return hasErrors; } }

        #endregion

        #region Public Methods

            public void Save(string path)
            {
                report.Save(path);
            }

        #endregion

        #region Private Methods

            private void Load(string reportPath, IReportFormatter formatter, string configurationPath)
            {
                FormatterResult formatterResult = formatter.Load(reportPath);
                XDocument configuration = XDocument.Load(configurationPath);

                XDocument report = new XDocument(
                    new XElement(
                        "dbGhost",
                        new XAttribute("errors", formatterResult.HasErrors),
                        formatterResult.Report.Elements(),
                        configuration.Elements()));

                this.report = report;
                this.hasErrors = formatterResult.HasErrors;
            }

        #endregion
    }
}
