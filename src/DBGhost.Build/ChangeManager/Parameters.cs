using System.IO;

namespace DbGhost.Build.ChangeManager
{
    public class Parameters
    {
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
            CopyDatabase,
            GenerateXmlReport
        }

        private const string DefaultAppPath32 = @"C:\Program Files\DB Ghost\ChangeManagerCmd.exe";
        private const string DefaultAppPath64 = @"C:\Program Files (x86)\DB Ghost\ChangeManagerCmd.exe";
        private string _applicationPath;

        public Parameters()
        {
            if (File.Exists(DefaultAppPath32))
                ApplicationPath = DefaultAppPath32;
            else if (File.Exists(DefaultAppPath64))
                ApplicationPath = DefaultAppPath64; 
                
            XmlReportFilePath = "DBGhost.ChangeManager.Report.xml";

            ScriptSourceDatabase = new Database();
            BuildDatabase = new Database();
            CompareSourceDatabase = new Database();
            CompareTargetDatabase = new Database();
        }

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

        public class Database
        {
            public enum AuthenticationMode
            {
                SqlServer,
                Windows
            }

            public string Name { get; set; }
            public string Server { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public AuthenticationMode? Authentication { get; set; }
        }
    }
}
