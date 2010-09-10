using System;
using System.Collections.Generic;
using DbGhost.Build.Reports;
using DbGhost.Build.Reports.Text;
using DbGhost.Build.Extensions;

namespace DbGhost.Build.ChangeManager
{
    public class ReportFormatter : IReportFormatter 
    {
        readonly Converter _converter;
        bool _hasErrors;

        public ReportFormatter()
        {
            _converter = CreateConverter();
        }

        public FormatterResult Load(string path)
        {
            return new FormatterResult(
                _converter.Convert(path), _hasErrors);
        }

        private Converter CreateConverter()
        {
            var converter = new Converter("report");
            converter.BeginBoundry += (o, e) => { if (e.Value.Name == "error") _hasErrors = true; };

            var errorBoundary = 
                new Boundary(
                    "error",
                    e => e == "<ERROR>",
                    e => e == "</ERROR>",
                        new List<Boundary> { 
                            new Boundary("message")});

            var warningBoundary =
                new Boundary(
                    "warning",
                    e => e == "<WARNING>",
                    e => e == "</WARNING>",
                        new List<Boundary> { 
                            new Boundary("message")});

            converter.AddBoundary(
                new Boundary(
                    "scripter",
                    e => e.StartsWith("DB Ghost Data and Schema Scripter"),
                    e => e.StartsWith("DB Ghost Data and Schema Scripter") && e.Contains("complete"),
                    new List<Boundary> { 
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
                    new List<Boundary> { 
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
                    e => e.StartsWithAny(StringComparison.OrdinalIgnoreCase, "DB Ghost ChangeManager", "DB Ghost Change Manager", "Innovartis.DBGhost.ChangeManagerCmd", "Running (DB Ghost Change Manager)"),
                    e => (e.StartsWith("DB Ghost Change Manager") || e.StartsWith("Innovartis.DBGhost.ChangeManagerCmd")) && 
                        e.Contains("complete"),
                    new List<Boundary> { 
                        new Boundary(
                            "object", 
                            e => e.StartsWithAny(StringComparison.OrdinalIgnoreCase,
                                                 "Created", "Altered", "Inserted", "Deleted", "Added", 
                                                 "Updated", "Modified", "Dropped", "Renamed", "Delta file")),
                        errorBoundary,
                        warningBoundary
                    }));

            return converter;
        }
    }
}
