using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBGhost.Build;
using DBGhost.Build.ChangeManager;
using DBGhost.Build.Extensions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DBGhost.Build.MSBuild.ChangeManager
{
    public class DBGhostChangeManager : Microsoft.Build.Utilities.Task
    {
        // ────────────────────────── Private Fields ──────────────────────────

        private Parameters parameters = new Parameters();

        // ────────────────────────── Basic Parameters ──────────────────────────

        public string ProcessType
        { get { return parameters.ProcessMode.ToString(); }
          set { parameters.ProcessMode = 
              value.ParseEnum<Parameters.ProcessType>(); } }

        public string ApplicationPath
        { get { return parameters.ApplicationPath; }
          set { parameters.ApplicationPath = value; } }

        [Required]
        public string TemplateConfigurationPath
        { get { return parameters.TemplateConfigurationPath; }
          set { parameters.TemplateConfigurationPath = value; } }

        public string RootFolder
        { get { return parameters.RootDirectory; }
          set { parameters.RootDirectory = value; } }

        [Required]
        public string ArtifactsFolder
        { get { return parameters.ArtifactsDirectory; }
          set { parameters.ArtifactsDirectory = value; } }

        public string ConfigurationPath
        { get { return parameters.ConfigurationPath; }
          set { parameters.ConfigurationPath = value; } }

        public string ReportFilePath
        { get { return parameters.ReportFilePath; }
          set { parameters.ReportFilePath = value; } }

        public string XmlReportFilePath
        { get { return parameters.XmlReportFilePath; }
          set { parameters.XmlReportFilePath = value; } }

        public string DeltaFilePath
        {
            get { return parameters.CompareDeltaScriptPath; }
            set { parameters.CompareDeltaScriptPath = value; }
        }

        public string BuildFilePath
        { get { return parameters.BuildScriptPath; }
          set { parameters.BuildScriptPath = value; } }

        [Output]
        public bool Error
        { get; private set; }

        // ────────────────────────── Script Source Database Parameters ──────────────────────────

        public string ScriptSourceDatabaseName
        { get { return parameters.ScriptSourceDatabase.Name; }
          set { parameters.ScriptSourceDatabase.Name = value; } }

        public string ScriptSourceDatabaseServer
        { get { return parameters.ScriptSourceDatabase.Server; }
          set { parameters.ScriptSourceDatabase.Server = value; } }

        public string ScriptSourceDatabaseUsername
        { get { return parameters.ScriptSourceDatabase.Username; }
          set { parameters.ScriptSourceDatabase.Username = value; } }

        public string ScriptSourceDatabasePassword
        { get { return parameters.ScriptSourceDatabase.Password; }
          set { parameters.ScriptSourceDatabase.Password = value; } }

        // ────────────────────────── Build Database Parameters ──────────────────────────

        public string BuildDatabaseName
        { get { return parameters.BuildDatabase.Name; }
          set { parameters.BuildDatabase.Name = value; } }

        public string BuildDatabaseServer
        { get { return parameters.BuildDatabase.Server; }
          set { parameters.BuildDatabase.Server = value; } }

        public string BuildDatabaseUsername
        { get { return parameters.BuildDatabase.Username; }
          set { parameters.BuildDatabase.Username = value; } }

        public string BuildDatabasePassword
        { get { return parameters.BuildDatabase.Password; }
          set { parameters.BuildDatabase.Password = value; } }

        public string BuildDatabaseAuthenticationMode
        { get { return parameters.BuildDatabase.Authentication.ToString(); }
            set { parameters.BuildDatabase.Authentication = 
                value.ParseEnum<Parameters.Database.AuthenticationMode>(); }}

        public string PreserveBuildDatabase
        { get { return parameters.PreserveBuildDatabase.ToString(); }
          set { parameters.PreserveBuildDatabase = value; } }

        // ────────────────────────── Build Template Database Parameters ──────────────────────────
    
        public string BuildDatabaseTemplateScript
        { get { return parameters.BuildDatabaseTemplateScript; }
          set { parameters.BuildDatabaseTemplateScript = value; } }

        public string BuildDatabaseTemplateName
        { get { return parameters.BuildDatabaseTemplateName; }
          set { parameters.BuildDatabaseTemplateName = value; } }

        public string BuildDatabaseNoTemplate
        { get { return parameters.BuildDatabaseNoTemplate; }
          set { parameters.BuildDatabaseNoTemplate = value; } }

        // ────────────────────────── Compare Source Database Parameters ──────────────────────────

        public string CompareSourceDatabaseName
        { get { return parameters.CompareSourceDatabase.Name; }
          set { parameters.CompareSourceDatabase.Name = value; } }

        public string CompareSourceDatabaseServer
        { get { return parameters.CompareSourceDatabase.Server; }
          set { parameters.CompareSourceDatabase.Server = value; } }

        public string CompareSourceDatabaseUsername
        { get { return parameters.CompareSourceDatabase.Username; }
          set { parameters.CompareSourceDatabase.Username = value; } }

        public string CompareSourceDatabasePassword
        { get { return parameters.CompareSourceDatabase.Password; }
          set { parameters.CompareSourceDatabase.Password = value; } }

        public string CompareSourceDatabaseAuthenticationMode
        { get { return parameters.CompareSourceDatabase.Authentication.ToString(); }
          set { parameters.CompareSourceDatabase.Authentication =
              value.ParseEnum<Parameters.Database.AuthenticationMode>();
          }
        }

        // ────────────────────────── Compare Target Database Parameters ──────────────────────────

        public string CompareTargetDatabaseName
        { get { return parameters.CompareTargetDatabase.Name; }
          set { parameters.CompareTargetDatabase.Name = value; } }

        public string CompareTargetDatabaseServer
        { get { return parameters.CompareTargetDatabase.Server; }
          set { parameters.CompareTargetDatabase.Server = value; } }

        public string CompareTargetDatabaseUsername
        { get { return parameters.CompareTargetDatabase.Username; }
          set { parameters.CompareTargetDatabase.Username = value; } }

        public string CompareTargetDatabasePassword
        { get { return parameters.CompareTargetDatabase.Password; }
          set { parameters.CompareTargetDatabase.Password = value; } }

        public string CompareTargetDatabaseAuthenticationMode
        { get { return parameters.CompareTargetDatabase.Authentication.ToString(); }
          set { parameters.CompareTargetDatabase.Authentication =
              value.ParseEnum<Parameters.Database.AuthenticationMode>();
          }
        }

        // ────────────────────────── Overrided Members ──────────────────────────

        public override bool Execute()
        {
            try
            {
                Error = !(new Application(parameters).Run());
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
