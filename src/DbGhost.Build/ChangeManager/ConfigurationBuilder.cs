using System;
using System.IO;
using System.Reflection;
using DbGhost.Build.Extensions;

namespace DbGhost.Build.ChangeManager
{
    public class ConfigurationBuilder
    {
        private readonly Parameters _parameters;

        public ConfigurationBuilder(Parameters parameters)
        {
            _parameters = parameters;
        }

        public Configuration Build()
        {
            EnsureCreateDatabaseScriptParameters(_parameters);
            return LoadConfiguration(_parameters);
        }

        private static void EnsureCreateDatabaseScriptParameters(Parameters parameters)
        {
            bool buildDatabaseNoTemplate;

            if (!bool.TryParse(parameters.BuildDatabaseNoTemplate, out buildDatabaseNoTemplate) ||
                !buildDatabaseNoTemplate || !string.IsNullOrEmpty(parameters.BuildDatabaseTemplateScript) ||
                !string.IsNullOrEmpty(parameters.BuildDatabaseTemplateName) ||
                (((parameters.ProcessMode != Parameters.ProcessType.BuildDatabase &&
                   parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompare) &&
                   parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompareAndCreateDelta) &&
                   parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompareAndSynchronize)) return;

            var name = Guid.NewGuid().ToString();
            var path = name.EnsureAbsolutePath(parameters.ArtifactsDirectory);
            parameters.BuildDatabaseTemplateScript = path;
            parameters.CompareSourceDatabase.Name = name;
            parameters.BuildDatabaseTemplateName = parameters.CompareTargetDatabase.Name;
            File.WriteAllText(
                path,
                string.Format(
                    new StreamReader(Assembly.GetExecutingAssembly().
                                     FindManifestResourceStream("DropAndCreateDatabase.sql")).
                                     ReadToEnd(),
                    name));
        }

        private static Configuration LoadConfiguration(Parameters parameters)
        {
            var configuration =
                new Configuration(parameters.TemplateConfigurationPath);

            configuration.ProcessTypeString =
                configuration.ProcessTypeString.Coalesce(parameters.ProcessMode);

            configuration.RootDirectory =
                configuration.RootDirectory.CoalesceReverse(parameters.RootDirectory);

            configuration.ConfigurationPath =
                configuration.ConfigurationPath.CoalesceReverse(
                    parameters.ConfigurationPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.BuildFile =
                configuration.BuildFile.CoalesceReverse(
                    parameters.BuildScriptPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.DeltaFile =
                configuration.DeltaFile.CoalesceReverse(
                    parameters.CompareDeltaScriptPath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            configuration.ReportPath =
                configuration.ReportPath.CoalesceReverse(
                    parameters.ReportFilePath.EnsureAbsolutePath(
                    parameters.ArtifactsDirectory));

            // Template for new database
            configuration.BuildTemplateDatabaseName =
                configuration.BuildTemplateDatabaseName.CoalesceReverse(parameters.BuildDatabaseTemplateName);

            configuration.BuildTemplateDatabaseScript =
                configuration.BuildTemplateDatabaseScript.CoalesceReverse(parameters.BuildDatabaseTemplateScript);

            // New database
            configuration.BuildDatabaseName =
                configuration.BuildDatabaseName.CoalesceReverse(parameters.BuildDatabase.Name);
            configuration.BuildTemplateDatabaseServer =
                configuration.BuildTemplateDatabaseServer.CoalesceReverse(parameters.BuildDatabase.Server);
            configuration.BuildTemplateDatabaseUsername =
                configuration.BuildTemplateDatabaseUsername.CoalesceReverse(parameters.BuildDatabase.Username);
            configuration.BuildTemplateDatabasePassword =
                configuration.BuildTemplateDatabasePassword.CoalesceReverse(parameters.BuildDatabase.Password);
            configuration.BuildTemplateDatabaseAuthenticationMode =
                configuration.BuildTemplateDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);

            configuration.PreserveBuildDatabase =
                configuration.PreserveBuildDatabase.CoalesceReverse(parameters.PreserveBuildDatabase);

            // Default the source to be the same as the build database unless it is specifically overriden
            configuration.CompareSourceDatabaseName =
                configuration.CompareSourceDatabaseName.CoalesceReverse(parameters.BuildDatabase.Name);
            configuration.CompareSourceDatabaseServer =
                configuration.CompareSourceDatabaseServer.CoalesceReverse(parameters.BuildDatabase.Server);
            configuration.CompareSourceDatabaseUsername =
                configuration.CompareSourceDatabaseUsername.CoalesceReverse(parameters.BuildDatabase.Username);
            configuration.CompareSourceDatabasePassword =
                configuration.CompareSourceDatabasePassword.CoalesceReverse(parameters.BuildDatabase.Password);
            configuration.CompareSourceDatabaseAuthenticationMode =
                configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);

            configuration.CompareSourceDatabaseName =
                configuration.CompareSourceDatabaseName.CoalesceReverse(parameters.CompareSourceDatabase.Name);
            configuration.CompareSourceDatabaseServer =
                configuration.CompareSourceDatabaseServer.CoalesceReverse(parameters.CompareSourceDatabase.Server);
            configuration.CompareSourceDatabaseUsername =
                configuration.CompareSourceDatabaseUsername.CoalesceReverse(parameters.CompareSourceDatabase.Username);
            configuration.CompareSourceDatabasePassword =
                configuration.CompareSourceDatabasePassword.CoalesceReverse(parameters.CompareSourceDatabase.Password);
            configuration.CompareSourceDatabaseAuthenticationMode =
                configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.CompareSourceDatabase.Authentication);

            configuration.CompareTargetDatabaseName =
                configuration.CompareTargetDatabaseName.CoalesceReverse(parameters.CompareTargetDatabase.Name);
            configuration.CompareTargetDatabaseServer =
                configuration.CompareTargetDatabaseServer.CoalesceReverse(parameters.CompareTargetDatabase.Server);
            configuration.CompareTargetDatabaseUsername =
                configuration.CompareTargetDatabaseUsername.CoalesceReverse(parameters.CompareTargetDatabase.Username);
            configuration.CompareTargetDatabasePassword =
                configuration.CompareTargetDatabasePassword.CoalesceReverse(parameters.CompareTargetDatabase.Password);
            configuration.CompareTargetDatabaseAuthenticationMode =
                configuration.CompareTargetDatabaseAuthenticationMode.Coalesce(parameters.CompareTargetDatabase.Authentication);

            configuration.ScriptDatabaseName =
                configuration.ScriptDatabaseName.CoalesceReverse(parameters.ScriptSourceDatabase.Name);
            configuration.ScriptDatabaseServer =
                configuration.ScriptDatabaseServer.CoalesceReverse(parameters.ScriptSourceDatabase.Server);
            configuration.ScriptDatabaseUsername =
                configuration.ScriptDatabaseUsername.CoalesceReverse(parameters.ScriptSourceDatabase.Username);
            configuration.ScriptDatabasePassword =
                configuration.ScriptDatabasePassword.CoalesceReverse(parameters.ScriptSourceDatabase.Password);

            return configuration;
        }
    }
}
