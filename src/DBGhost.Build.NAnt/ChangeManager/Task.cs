using System.Linq;
using DbGhost.Build.ChangeManager;
using NAnt.Core;
using NAnt.Core.Attributes;
using DbGhost.Build.Extensions;

namespace DbGhost.Build.NAnt.ChangeManager
{
    [TaskName("dbghost-changemanager")]
    public class Task : global::NAnt.Core.Task
    {
        private readonly Parameters _parameters = new Parameters();

        // Basic Parameters

        [TaskAttribute("processType", Required = false)]
        public string ProcessType
        { get { return _parameters.ProcessMode.ToString(); }
            set { _parameters.ProcessMode = 
                value.ParseEnum<Parameters.ProcessType>(); } }

        [TaskAttribute("applicationPath", Required = false)]
        public string ApplicationPath
        { get { return _parameters.ApplicationPath; }
            set { _parameters.ApplicationPath = value; } }

        [TaskAttribute("templateConfigurationPath", Required = true)]
        public string TemplateConfigurationPath
        { get { return _parameters.TemplateConfigurationPath; }
            set { _parameters.TemplateConfigurationPath = value; } }

        [TaskAttribute("rootDirectory", Required = false)]
        public string RootFolder
        { get { return _parameters.RootDirectory; }
            set { _parameters.RootDirectory = value; } }

        [TaskAttribute("artifactsDirectory", Required = true)]
        public string ArtifactsFolder
        { get { return _parameters.ArtifactsDirectory; }
            set { _parameters.ArtifactsDirectory = value; } }

        [TaskAttribute("configurationPath", Required = false)]
        public string ConfigurationPath
        { get { return _parameters.ConfigurationPath; }
            set { _parameters.ConfigurationPath = value; } }

        [TaskAttribute("reportFile", Required = false)]
        public string ReportFilePath
        { get { return _parameters.ReportFilePath; }
            set { _parameters.ReportFilePath = value; } }

        [TaskAttribute("xmlReportFile", Required = false)]
        public string XmlReportFilePath
        { get { return _parameters.XmlReportFilePath; }
            set { _parameters.XmlReportFilePath = value; } }

        [TaskAttribute("compareDeltaScript", Required = false)]
        public string DeltaFilePath
        {
            get { return _parameters.CompareDeltaScriptPath; }
            set { _parameters.CompareDeltaScriptPath = value; }
        }

        [TaskAttribute("buildScript", Required = false)]
        public string BuildFilePath
        { get { return _parameters.BuildScriptPath; }
            set { _parameters.BuildScriptPath = value; } }

        [TaskAttribute("resultProperty")]
        public string ResultProperty
        { get; set; }

        // Script Source Database Parameters

        [TaskAttribute("scriptSourceDatabaseName", Required = false)]
        public string ScriptSourceDatabaseName
        { get { return _parameters.ScriptSourceDatabase.Name; }
            set { _parameters.ScriptSourceDatabase.Name = value; } }

        [TaskAttribute("scriptSourceDatabaseServer", Required = false)]
        public string ScriptSourceDatabaseServer
        { get { return _parameters.ScriptSourceDatabase.Server; }
            set { _parameters.ScriptSourceDatabase.Server = value; } }

        [TaskAttribute("scriptSourceDatabaseUsername", Required = false)]
        public string ScriptSourceDatabaseUsername
        { get { return _parameters.ScriptSourceDatabase.Username; }
            set { _parameters.ScriptSourceDatabase.Username = value; } }

        [TaskAttribute("scriptSourceDatabasePassword", Required = false)]
        public string ScriptSourceDatabasePassword
        { get { return _parameters.ScriptSourceDatabase.Password; }
            set { _parameters.ScriptSourceDatabase.Password = value; } }

        // Build Database Parameters

        [TaskAttribute("buildDatabaseName", Required = false)]
        public string BuildDatabaseName
        { get { return _parameters.BuildDatabase.Name; }
            set { _parameters.BuildDatabase.Name = value; } }

        [TaskAttribute("buildDatabaseServer", Required = false)]
        public string BuildDatabaseServer
        { get { return _parameters.BuildDatabase.Server; }
            set { _parameters.BuildDatabase.Server = value; } }

        [TaskAttribute("buildDatabaseUsername", Required = false)]
        public string BuildDatabaseUsername
        { get { return _parameters.BuildDatabase.Username; }
            set { _parameters.BuildDatabase.Username = value; } }

        [TaskAttribute("buildDatabasePassword", Required = false)]
        public string BuildDatabasePassword
        { get { return _parameters.BuildDatabase.Password; }
            set { _parameters.BuildDatabase.Password = value; } }

        [TaskAttribute("buildDatabaseAuthenticationMode", Required = false)]
        public string BuildDatabaseAuthenticationMode
        { get { return _parameters.BuildDatabase.Authentication.ToString(); }
            set { _parameters.BuildDatabase.Authentication = 
                value.ParseEnum<Parameters.Database.AuthenticationMode>(); }}

        [TaskAttribute("preserveBuildDatabase", Required = false)]
        public string PreserveBuildDatabase
        { get { return _parameters.PreserveBuildDatabase; }
            set { _parameters.PreserveBuildDatabase = value; } }

        // Build Template Database Parameters
        
        [TaskAttribute("buildDatabaseTemplateScript", Required = false)]
        public string BuildDatabaseTemplateScript
        { get { return _parameters.BuildDatabaseTemplateScript; }
            set { _parameters.BuildDatabaseTemplateScript = value; } }

        [TaskAttribute("buildDatabaseTemplateName", Required = false)]
        public string BuildDatabaseTemplateName
        { get { return _parameters.BuildDatabaseTemplateName; }
            set { _parameters.BuildDatabaseTemplateName = value; } }

        // Compare Source Database Parameters

        [TaskAttribute("compareSourceDatabaseName", Required = false)]
        public string CompareSourceDatabaseName
        { get { return _parameters.CompareSourceDatabase.Name; }
            set { _parameters.CompareSourceDatabase.Name = value; } }

        [TaskAttribute("compareSourceDatabaseServer", Required = false)]
        public string CompareSourceDatabaseServer
        { get { return _parameters.CompareSourceDatabase.Server; }
            set { _parameters.CompareSourceDatabase.Server = value; } }

        [TaskAttribute("compareSourceDatabaseUsername", Required = false)]
        public string CompareSourceDatabaseUsername
        { get { return _parameters.CompareSourceDatabase.Username; }
            set { _parameters.CompareSourceDatabase.Username = value; } }

        [TaskAttribute("compareSourceDatabasePassword", Required = false)]
        public string CompareSourceDatabasePassword
        { get { return _parameters.CompareSourceDatabase.Password; }
            set { _parameters.CompareSourceDatabase.Password = value; } }

        [TaskAttribute("compareSourceDatabaseAuthenticationMode", Required = false)]
        public string CompareSourceDatabaseAuthenticationMode
        { get { return _parameters.CompareSourceDatabase.Authentication.ToString(); }
            set { _parameters.CompareSourceDatabase.Authentication =
                value.ParseEnum<Parameters.Database.AuthenticationMode>();
            }
        }

        // Compare Target Database Parameters

        [TaskAttribute("compareTargetDatabaseName", Required = false)]
        public string CompareTargetDatabaseName
        { get { return _parameters.CompareTargetDatabase.Name; }
            set { _parameters.CompareTargetDatabase.Name = value; } }

        [TaskAttribute("compareTargetDatabaseServer", Required = false)]
        public string CompareTargetDatabaseServer
        { get { return _parameters.CompareTargetDatabase.Server; }
            set { _parameters.CompareTargetDatabase.Server = value; } }

        [TaskAttribute("compareTargetDatabaseUsername", Required = false)]
        public string CompareTargetDatabaseUsername
        { get { return _parameters.CompareTargetDatabase.Username; }
            set { _parameters.CompareTargetDatabase.Username = value; } }

        [TaskAttribute("compareTargetDatabasePassword", Required = false)]
        public string CompareTargetDatabasePassword
        { get { return _parameters.CompareTargetDatabase.Password; }
            set { _parameters.CompareTargetDatabase.Password = value; } }

        [TaskAttribute("compareTargetDatabaseAuthenticationMode", Required = false)]
        public string CompareTargetDatabaseAuthenticationMode
        { get { return _parameters.CompareTargetDatabase.Authentication.ToString(); }
            set { _parameters.CompareTargetDatabase.Authentication =
                value.ParseEnum<Parameters.Database.AuthenticationMode>();
            }
        }

        public void Run()
        {
            ExecuteTask();
        }

        protected override void ExecuteTask()
        {
            var result = new Application(_parameters).Run();

            result.Output
                  .SplitLines()
                  .Where(x => x.Contains("..."))
                  .Select(x => x.Split(2, "...").Last())
                  .Where(x => x.ContainsAlpha())
                  .ToList().ForEach(x => Log(Level.Info, x));

            if (!result.Success)
            {
                if (ResultProperty != null) Properties[ResultProperty] = "1";
                throw new BuildException("DBGhost Change Manager encountered an error. View the log for more information.");
            }
            if (ResultProperty != null) Properties[ResultProperty] = "0";
        }
    }
}
