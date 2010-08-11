using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBGhost.Build.Reports;
using DBGhost.Build.Reports.Text;
using DBGhost.Build.Extensions;

namespace DBGhost.Build.ChangeManager
{
    public class ReportFormatter : IReportFormatter 
    {
        #region Private Fields

            Converter converter;
            bool hasErrors = false;

        #endregion

        #region Constructor

            public ReportFormatter()
            {
                converter = CreateConverter();
            }

        #endregion

        #region IReportFormatter Members

            public FormatterResult Load(string path)
            {
                return new FormatterResult(
                    converter.Convert(path), hasErrors);
            }

        #endregion

        #region Private Methods

            private Converter CreateConverter()
            {
                Converter converter = new Converter("report");
                converter.BeginBoundry += (o, e) => { if (e.Value.Name == "error") this.hasErrors = true; };

                Boundary errorBoundary = 
                    new Boundary(
                        "error",
                        e => e == "<ERROR>",
                        e => e == "</ERROR>",
                            new List<Boundary>() { 
                                new Boundary("message")});

                Boundary warningBoundary =
                    new Boundary(
                        "warning",
                        e => e == "<WARNING>",
                        e => e == "</WARNING>",
                            new List<Boundary>() { 
                                new Boundary("message")});

                converter.AddBoundary(
                    new Boundary(
                        "scripter",
                        e => e.StartsWith("DB Ghost Data and Schema Scripter"),
                        e => e.StartsWith("DB Ghost Data and Schema Scripter") && e.Contains("complete"),
                        new List<Boundary>() { 
                            new Boundary(
                                "script", 
                                e => e.StartsWith("File Scripted"), 
                                f => f.Replace("File Scripted", string.Empty).Trim()),
                            errorBoundary,
                            warningBoundary
                        }));

                converter.AddBoundary(
                    new Boundary(
                        "builder",
                        e => e.StartsWith("DB Ghost Database Builder"),
                        e => e.StartsWith("DB Ghost Database Builder") && e.Contains("complete"),
                        new List<Boundary>() { 
                            new Boundary(
                                "script", 
                                e => e.StartsWith("Executing file"),
                                f => f.Remove(0, f.IndexOf("-") + 1).Trim()),
                            errorBoundary,
                            warningBoundary
                        }));

                converter.AddBoundary(
                    new Boundary(
                        "compare",
                        e => e.StartsWith("DB Ghost ChangeManager") || e.StartsWith("DB Ghost Change Manager") || e.StartsWith("Innovartis.DBGhost.ChangeManagerCmd"),
                        e => (e.StartsWith("DB Ghost Change Manager") || e.StartsWith("Innovartis.DBGhost.ChangeManagerCmd")) && 
                            e.Contains("complete"),
                        new List<Boundary>() { 
                            new Boundary(
                                "object", 
                                e => e.StartsWithAny(
                                    new string[] {"Created", "Altered", "Inserted", "Deleted", "Added", 
                                                  "Updated", "Modified", "Dropped", "Renamed", "Delta file"}, 
                                    StringComparison.OrdinalIgnoreCase)),
                            errorBoundary,
                            warningBoundary
                        }));

                return converter;
            }

        #endregion
    }
}
