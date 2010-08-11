using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DBGhost.Build.ChangeManager
{
    public class Parameters
    {
        #region Constants

            public enum ProcessType
            {
                ScriptDatabase,
                ScriptDatabaseAndBuildDatabase,
                ScriptDatabaseAndBuildDatabaseAndCompare,
                ScriptDatabaseAndBuildDatabaseAndCompareAndCreateDelta,
                ScriptDatabaseAndBuildDatabaseAndCompareAndSynchronize,
                BuildDatabase,
                BuildDatabaseAndCompare,
                BuildDatabaseAndCompareAndSynchronize,
                BuildDatabaseAndCompareAndCreateDelta,
                CompareOnly,
                CompareAndSynchronize,
                CompareAndCreateDelta,
                CopyDatabase
            }

        #endregion

        #region Private Fields

            private string DEFAULT_APP_PATH_32 = @"C:\Program Files\DB Ghost\ChangeManagerCmd.exe";
            private string DEFAULT_APP_PATH_64 = @"C:\Program Files (x86)\DB Ghost\ChangeManagerCmd.exe";
            private string _applicationPath;

        #endregion

        #region Constructor

            public Parameters()
            {
                if (File.Exists(DEFAULT_APP_PATH_32))
                    ApplicationPath = DEFAULT_APP_PATH_32;
                else if (File.Exists(DEFAULT_APP_PATH_64))
                    ApplicationPath = DEFAULT_APP_PATH_64; 
                
                XmlReportFilePath = "DBGhost.ChangeManager.Report.xml";

                ScriptSourceDatabase = new Database();
                BuildDatabase = new Database();
                CompareSourceDatabase = new Database();
                CompareTargetDatabase = new Database();
            }

        #endregion

        #region Public Properties

            public ProcessType? ProcessMode { get; set; }
            public string ApplicationPath { 
                get { return _applicationPath; } 
                set { _applicationPath = value ?? _applicationPath; } }
            public string TemplateConfigurationPath { get; set; }
            public string RootDirectory { get; set; }
            public string ArtifactsDirectory { get; set; }
            public string ConfigurationPath { get; set; }
            public string ReportFilePath { get; set; }
            public string XmlReportFilePath { get; set; }
            public string BuildDatabaseTemplateName { get; set; }
            public string BuildDatabaseTemplateScript { get; set; }
            public string BuildDatabaseNoTemplate { get; set; }
            public string PreserveBuildDatabase { get; set; }

            public string BuildScriptPath { get; set; }
            public string CompareDeltaScriptPath { get; set; }

            public Database ScriptSourceDatabase { get; set; }
            public Database BuildDatabase { get; set; }
            public Database CompareSourceDatabase { get; set; }
            public Database CompareTargetDatabase { get; set; }

        #endregion

        #region Database Class

            public class Database
            {
                public enum AuthenticationMode
                {
                    SQLServer,
                    Windows
                }

                public string Name { get; set; }
                public string Server { get; set; }
                public string Username { get; set; }
                public string Password { get; set; }
                public AuthenticationMode? Authentication { get; set; }
            }

        #endregion
    }
}
