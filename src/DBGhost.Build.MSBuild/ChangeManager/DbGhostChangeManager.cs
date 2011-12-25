using System;
using DbGhost.Build.ChangeManager;
using DbGhost.Build.Extensions;
using Microsoft.Build.Framework;

namespace DbGhost.Build.MSBuild.ChangeManager
{
    public class DbGhostChangeManager : Microsoft.Build.Utilities.Task
    {
        // ────────────────────────── Private Fields ──────────────────────────

        private readonly Parameters _parameters = new Parameters();

        // ────────────────────────── Basic Parameters ──────────────────────────

        public string ProcessType
        { get { return _parameters.ProcessMode.ToString(); }
          set { _parameters.ProcessMode = 
              value.ParseEnum<Parameters.ProcessType>(); } }

        public string ApplicationPath
        { get { return _parameters.ApplicationPath; }
          set { _parameters.ApplicationPath = value; } }

        [Required]
        public string TemplateConfigurationPath
        { get { return _parameters.TemplateConfigurationPath; }
          set { _parameters.TemplateConfigurationPath = value; } }

        public string RootFolder
        { get { return _parameters.RootDirectory; }
          set { _parameters.RootDirectory = value; } }

        [Required]
        public string ArtifactsFolder
        { get { return _parameters.ArtifactsDirectory; }
          set { _parameters.ArtifactsDirectory = value; } }

        public string ConfigurationPath
        { get { return _parameters.ConfigurationPath; }
          set { _parameters.ConfigurationPath = value; } }

        public string ReportFilePath
        { get { return _parameters.ReportFilePath; }
          set { _parameters.ReportFilePath = value; } }

        public string XmlReportFilePath
        { get { return _parameters.XmlReportFilePath; }
          set { _parameters.XmlReportFilePath = value; } }

        public string DeltaFilePath
        {
            get { return _parameters.CompareDeltaScriptPath; }
            set { _parameters.CompareDeltaScriptPath = value; }
        }

        public string BuildFilePath
        { get { return _parameters.BuildScriptPath; }
          set { _parameters.BuildScriptPath = value; } }

        [Output]
        public bool Error
        { get; private set; }

        // ────────────────────────── Script Source Database Parameters ──────────────────────────

        public string ScriptSourceDatabaseName
        { get { return _parameters.ScriptSourceDatabase.Name; }
          set { _parameters.ScriptSourceDatabase.Name = value; } }

        public string ScriptSourceDatabaseServer
        { get { return _parameters.ScriptSourceDatabase.Server; }
          set { _parameters.ScriptSourceDatabase.Server = value; } }

        public string ScriptSourceDatabaseUsername
        { get { return _parameters.ScriptSourceDatabase.Username; }
          set { _parameters.ScriptSourceDatabase.Username = value; } }

        public string ScriptSourceDatabasePassword
        { get { return _parameters.ScriptSourceDatabase.Password; }
          set { _parameters.ScriptSourceDatabase.Password = value; } }

        // ────────────────────────── Build Database Parameters ──────────────────────────

        public string BuildDatabaseName
        { get { return _parameters.BuildDatabase.Name; }
          set { _parameters.BuildDatabase.Name = value; } }

        public string BuildDatabaseServer
        { get { return _parameters.BuildDatabase.Server; }
          set { _parameters.BuildDatabase.Server = value; } }

        public string BuildDatabaseUsername
        { get { return _parameters.BuildDatabase.Username; }
          set { _parameters.BuildDatabase.Username = value; } }

        public string BuildDatabasePassword
        { get { return _parameters.BuildDatabase.Password; }
          set { _parameters.BuildDatabase.Password = value; } }

        public string BuildDatabaseAuthenticationMode
        { get { return _parameters.BuildDatabase.Authentication.ToString(); }
            set { _parameters.BuildDatabase.Authentication = 
                value.ParseEnum<Parameters.Database.AuthenticationMode>(); }}

        public string PreserveBuildDatabase
        { get { return _parameters.PreserveBuildDatabase; }
          set { _parameters.PreserveBuildDatabase = value; } }

        // ────────────────────────── Build Template Database Parameters ──────────────────────────
    
        public string BuildDatabaseTemplateScript
        { get { return _parameters.BuildDatabaseTemplateScript; }
          set { _parameters.BuildDatabaseTemplateScript = value; } }

        public string BuildDatabaseTemplateName
        { get { return _parameters.BuildDatabaseTemplateName; }
          set { _parameters.BuildDatabaseTemplateName = value; } }

        // ────────────────────────── Compare Source Database Parameters ──────────────────────────

        public string CompareSourceDatabaseName
        { get { return _parameters.CompareSourceDatabase.Name; }
          set { _parameters.CompareSourceDatabase.Name = value; } }

        public string CompareSourceDatabaseServer
        { get { return _parameters.CompareSourceDatabase.Server; }
          set { _parameters.CompareSourceDatabase.Server = value; } }

        public string CompareSourceDatabaseUsername
        { get { return _parameters.CompareSourceDatabase.Username; }
          set { _parameters.CompareSourceDatabase.Username = value; } }

        public string CompareSourceDatabasePassword
        { get { return _parameters.CompareSourceDatabase.Password; }
          set { _parameters.CompareSourceDatabase.Password = value; } }

        public string CompareSourceDatabaseAuthenticationMode
        { get { return _parameters.CompareSourceDatabase.Authentication.ToString(); }
          set { _parameters.CompareSourceDatabase.Authentication =
              value.ParseEnum<Parameters.Database.AuthenticationMode>();
          }
        }

        // ────────────────────────── Compare Target Database Parameters ──────────────────────────

        public string CompareTargetDatabaseName
        { get { return _parameters.CompareTargetDatabase.Name; }
          set { _parameters.CompareTargetDatabase.Name = value; } }

        public string CompareTargetDatabaseServer
        { get { return _parameters.CompareTargetDatabase.Server; }
          set { _parameters.CompareTargetDatabase.Server = value; } }

        public string CompareTargetDatabaseUsername
        { get { return _parameters.CompareTargetDatabase.Username; }
          set { _parameters.CompareTargetDatabase.Username = value; } }

        public string CompareTargetDatabasePassword
        { get { return _parameters.CompareTargetDatabase.Password; }
          set { _parameters.CompareTargetDatabase.Password = value; } }

        public string CompareTargetDatabaseAuthenticationMode
        { get { return _parameters.CompareTargetDatabase.Authentication.ToString(); }
          set { _parameters.CompareTargetDatabase.Authentication =
              value.ParseEnum<Parameters.Database.AuthenticationMode>();
          }
        }

        // ────────────────────────── Overrided Members ──────────────────────────

        public override bool Execute()
        {
            try
            {
                Error = !(new Application(_parameters).Run().Success);
                return true;
            } catch (Exception exception)
            {
                Log.LogErrorFromException(exception);
                Error = true;
                return false;
            }
        }
    }
}
