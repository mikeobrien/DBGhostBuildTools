using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DBGhost.Build.Reports
{
    public class FormatterResult
    {
        #region Constructor

            public FormatterResult(XDocument report, bool hasErrors)
            {
                this.Report = report;
                this.HasErrors = hasErrors;
            }

        #endregion

        #region Public Properties

            public XDocument Report { get; private set; }
            public bool HasErrors { get; private set; }

        #endregion
    }
}
