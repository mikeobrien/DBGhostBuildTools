using System;
using System.IO;
using System.Diagnostics;
using DbGhost.Build.Extensions;
using DbGhost.Build.Reports;
using System.Reflection;

namespace DbGhost.Build.ChangeManager
{
    public class Application
    {
        private readonly Parameters _parameters;

        public Application(Parameters parameters)
        {
            _parameters = parameters;
        }

        public bool Run()
        {
            string buildDatabaseTemplateScript = EnsureBuildDatabaseScript(_parameters);

            Configuration configuration = LoadConfiguration(_parameters);
            string xmlReportPath = _parameters.XmlReportFilePath.EnsureAbsolutePath(
                _parameters.ArtifactsDirectory);

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
                        UseShellExecute = false
                    };

            Process process = Process.Start(processInfo);
            process.WaitForExit();

            if (buildDatabaseTemplateScript != null && File.Exists(buildDatabaseTemplateScript))
                File.Delete(buildDatabaseTemplateScript);

            if (File.Exists(configuration.ReportPath))
            {
                // Convert the text report to xml
                var report = new Report(
                    configuration.ReportPath,
                    new ReportFormatter(),
                    configuration.ConfigurationPath);

                report.Save(xmlReportPath);

                return (!report.HasErrors);
            }
            return false;
        }

        private static string EnsureBuildDatabaseScript(Parameters parameters)
        {
            bool buildDatabaseNoTemplate;
            if (bool.TryParse(parameters.BuildDatabaseNoTemplate, out buildDatabaseNoTemplate) &&
                buildDatabaseNoTemplate && string.IsNullOrEmpty(parameters.BuildDatabaseTemplateScript) &&
                string.IsNullOrEmpty(parameters.BuildDatabaseTemplateName) &&
                (parameters.ProcessMode == Parameters.ProcessType.BuildDatabase ||
                parameters.ProcessMode == Parameters.ProcessType.BuildDatabaseAndCompare ||
                parameters.ProcessMode == Parameters.ProcessType.BuildDatabaseAndCompareAndCreateDelta ||
                parameters.ProcessMode == Parameters.ProcessType.BuildDatabaseAndCompareAndSynchronize))
            {
                string name = Guid.NewGuid().ToString();
                string path = name.EnsureAbsolutePath(parameters.ArtifactsDirectory);
                parameters.BuildDatabaseTemplateScript = path;
                parameters.CompareSourceDatabase.Name = name;
                parameters.BuildDatabaseTemplateName = parameters.CompareTargetDatabase.Name;
                File.WriteAllText(
                    path, 
                    string.Format(
                        new StreamReader(Assembly.GetExecutingAssembly().
                            FindManifestResourceStream("DropAndCreateDatabase.sql")).ReadToEnd(),
                        name));
                return path;
            }
            return null;
        }

        private static Configuration LoadConfiguration(Parameters parameters)
        {
            var configuration =
                new Configuration(parameters.TemplateConfigurationPath);

            configuration.ProcessType = 
                configuration.ProcessType.Coalesce(parameters.ProcessMode);

            configuration.RootDirectory = 
                configuration.RootDirectory.Coalesce(parameters.RootDirectory);

            configuration.ConfigurationPath = 
                configuration.ConfigurationPath.Coalesce(
                    parameters.ConfigurationPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.BuildFile = 
                configuration.BuildFile.Coalesce(
                    parameters.BuildScriptPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.DeltaFile = 
                configuration.DeltaFile.Coalesce(
                    parameters.CompareDeltaScriptPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.ReportPath = 
                configuration.ReportPath.Coalesce(
                    parameters.ReportFilePath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            // Template for new database
            configuration.BuildTemplateDatabaseName = 
                configuration.BuildTemplateDatabaseName.Coalesce(parameters.BuildDatabaseTemplateName);

            configuration.BuildTemplateDatabaseScript = 
                configuration.BuildTemplateDatabaseScript.Coalesce(parameters.BuildDatabaseTemplateScript);

            // New database
            configuration.BuildDatabaseName = 
                configuration.BuildDatabaseName.Coalesce(parameters.BuildDatabase.Name);
            configuration.BuildTemplateDatabaseServer = 
                configuration.BuildTemplateDatabaseServer.Coalesce(parameters.BuildDatabase.Server);
            configuration.BuildTemplateDatabaseUsername = 
                configuration.BuildTemplateDatabaseUsername.Coalesce(parameters.BuildDatabase.Username);
            configuration.BuildTemplateDatabasePassword = 
                configuration.BuildTemplateDatabasePassword.Coalesce(parameters.BuildDatabase.Password);
            configuration.BuildTemplateDatabaseAuthenticationMode = 
                configuration.BuildTemplateDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);
                
            configuration.PreserveBuildDatabase = 
                configuration.PreserveBuildDatabase.Coalesce(parameters.PreserveBuildDatabase);

            // Default the source to be the same as the build database unless it is specifically overriden
            configuration.CompareSourceDatabaseName = 
                configuration.CompareSourceDatabaseName.Coalesce(parameters.BuildDatabase.Name);
            configuration.CompareSourceDatabaseServer = 
                configuration.CompareSourceDatabaseServer.Coalesce(parameters.BuildDatabase.Server);
            configuration.CompareSourceDatabaseUsername = 
                configuration.CompareSourceDatabaseUsername.Coalesce(parameters.BuildDatabase.Username);
            configuration.CompareSourceDatabasePassword = 
                configuration.CompareSourceDatabasePassword.Coalesce(parameters.BuildDatabase.Password);
            configuration.CompareSourceDatabaseAuthenticationMode = 
                configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);

            configuration.CompareSourceDatabaseName = 
                configuration.CompareSourceDatabaseName.Coalesce(parameters.CompareSourceDatabase.Name);
            configuration.CompareSourceDatabaseServer = 
                configuration.CompareSourceDatabaseServer.Coalesce(parameters.CompareSourceDatabase.Server);
            configuration.CompareSourceDatabaseUsername = 
                configuration.CompareSourceDatabaseUsername.Coalesce(parameters.CompareSourceDatabase.Username);
            configuration.CompareSourceDatabasePassword = 
                configuration.CompareSourceDatabasePassword.Coalesce(parameters.CompareSourceDatabase.Password);
            configuration.CompareSourceDatabaseAuthenticationMode = 
                configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.CompareSourceDatabase.Authentication);

            configuration.CompareTargetDatabaseName = 
                configuration.CompareTargetDatabaseName.Coalesce(parameters.CompareTargetDatabase.Name);
            configuration.CompareTargetDatabaseServer = 
                configuration.CompareTargetDatabaseServer.Coalesce(parameters.CompareTargetDatabase.Server);
            configuration.CompareTargetDatabaseUsername = 
                configuration.CompareTargetDatabaseUsername.Coalesce(parameters.CompareTargetDatabase.Username);
            configuration.CompareTargetDatabasePassword = 
                configuration.CompareTargetDatabasePassword.Coalesce(parameters.CompareTargetDatabase.Password);
            configuration.CompareTargetDatabaseAuthenticationMode = 
                configuration.CompareTargetDatabaseAuthenticationMode.Coalesce(parameters.CompareTargetDatabase.Authentication);

            configuration.ScriptDatabaseName = 
                configuration.ScriptDatabaseName.Coalesce(parameters.ScriptSourceDatabase.Name);
            configuration.ScriptDatabaseServer = 
                configuration.ScriptDatabaseServer.Coalesce(parameters.ScriptSourceDatabase.Server);
            configuration.ScriptDatabaseUsername = 
                configuration.ScriptDatabaseUsername.Coalesce(parameters.ScriptSourceDatabase.Username);
            configuration.ScriptDatabasePassword = 
                configuration.ScriptDatabasePassword.Coalesce(parameters.ScriptSourceDatabase.Password);

            return configuration;
        }
    }
}
