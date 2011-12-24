﻿using System;
using System.IO;
using System.Diagnostics;
using DbGhost.Build.Extensions;
using DbGhost.Build.Reports;

namespace DbGhost.Build.ChangeManager
{
    public class Application
    {
        private readonly Parameters _parameters;

        public Application(Parameters parameters)
        {
            _parameters = parameters;
        }

        public bool Run(Action<string> logAction = null)
        {
            var configuration = new ConfigurationBuilder(_parameters).Build();

            // Persist the custom configuration file
            configuration.Save();

            // Kill the existing report if one exists
            if (File.Exists(configuration.ReportPath))
                File.Delete(configuration.ReportPath);

            // Execute DBGhost
            var processInfo =
                new ProcessStartInfo(_parameters.ApplicationPath)
                    {
                        Arguments = string.Format("\"{0}\"", configuration.ConfigurationPath),
                        UseShellExecute = false,
						RedirectStandardOutput = logAction != null
                    };

        	bool result;
			using (Process process = Process.Start(processInfo))
			{
				if (logAction != null)
				{
					using (StreamReader reader = process.StandardOutput)
					{
						string line;

						while ((line = reader.ReadLine()) != null)
						{
							logAction(line);
						}
					}
				}
				process.WaitForExit();
				result = process.ExitCode == 0;
			}

        	// Convert the text report to xml
            if (File.Exists(configuration.ReportPath))
            {
                var xmlReportPath = _parameters.XmlReportFilePath.EnsureAbsolutePath(_parameters.ArtifactsDirectory);
                var report = new Report(
                    configuration.ReportPath,
                    new ReportFormatter(),
                    configuration.ConfigurationPath);

                report.Save(xmlReportPath);

                if (!GenerateReport(configuration, xmlReportPath)) result = false;
            }

            return result;
        }

        private static bool GenerateReport(Configuration configuration, string xmlReportPath)
        {
            var report = new Report(
                configuration.ReportPath,
                new ReportFormatter(),
                configuration.ConfigurationPath);

            report.Save(xmlReportPath);

            return (!report.HasErrors);
        }
    }
}
