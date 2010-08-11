using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using DBGhost.Build;
using DBGhost.Build.ChangeManager;
using DBGhost.Build.Extensions;

namespace DBGhost.Build.NAnt.ChangeManager
{
    [TaskName("dbghost-changemanager")]
    public class Task : global::NAnt.Core.Task
    {
        #region Private Fields

            private Parameters parameters = new Parameters();

        #endregion

        #region Basic Parameters

            [TaskAttribute("processType", Required = false)]
            public string ProcessType
            { get { return parameters.ProcessMode.ToString(); }
              set { parameters.ProcessMode = 
                  value.ParseEnum<Parameters.ProcessType>(); } }

            [TaskAttribute("applicationPath", Required = false)]
            public string ApplicationPath
            { get { return parameters.ApplicationPath; }
              set { parameters.ApplicationPath = value; } }

            [TaskAttribute("templateConfigurationPath", Required = true)]
            public string TemplateConfigurationPath
            { get { return parameters.TemplateConfigurationPath; }
              set { parameters.TemplateConfigurationPath = value; } }

            [TaskAttribute("rootDirectory", Required = false)]
            public string RootFolder
            { get { return parameters.RootDirectory; }
              set { parameters.RootDirectory = value; } }

            [TaskAttribute("artifactsDirectory", Required = true)]
            public string ArtifactsFolder
            { get { return parameters.ArtifactsDirectory; }
              set { parameters.ArtifactsDirectory = value; } }

            [TaskAttribute("configurationPath", Required = false)]
            public string ConfigurationPath
            { get { return parameters.ConfigurationPath; }
              set { parameters.ConfigurationPath = value; } }

            [TaskAttribute("reportFile", Required = false)]
            public string ReportFilePath
            { get { return parameters.ReportFilePath; }
              set { parameters.ReportFilePath = value; } }

            [TaskAttribute("xmlReportFile", Required = false)]
            public string XmlReportFilePath
            { get { return parameters.XmlReportFilePath; }
              set { parameters.XmlReportFilePath = value; } }

            [TaskAttribute("compareDeltaScript", Required = false)]
            public string DeltaFilePath
            {
                get { return parameters.CompareDeltaScriptPath; }
                set { parameters.CompareDeltaScriptPath = value; }
            }

            [TaskAttribute("buildScript", Required = false)]
            public string BuildFilePath
            { get { return parameters.BuildScriptPath; }
              set { parameters.BuildScriptPath = value; } }

            [TaskAttribute("resultProperty")]
            public string ResultProperty
            { get; set; }

        #endregion

        #region Script Source Database Parameters

            [TaskAttribute("scriptSourceDatabaseName", Required = false)]
            public string ScriptSourceDatabaseName
            { get { return parameters.ScriptSourceDatabase.Name; }
              set { parameters.ScriptSourceDatabase.Name = value; } }

            [TaskAttribute("scriptSourceDatabaseServer", Required = false)]
            public string ScriptSourceDatabaseServer
            { get { return parameters.ScriptSourceDatabase.Server; }
              set { parameters.ScriptSourceDatabase.Server = value; } }

            [TaskAttribute("scriptSourceDatabaseUsername", Required = false)]
            public string ScriptSourceDatabaseUsername
            { get { return parameters.ScriptSourceDatabase.Username; }
              set { parameters.ScriptSourceDatabase.Username = value; } }

            [TaskAttribute("scriptSourceDatabasePassword", Required = false)]
            public string ScriptSourceDatabasePassword
            { get { return parameters.ScriptSourceDatabase.Password; }
              set { parameters.ScriptSourceDatabase.Password = value; } }

        #endregion
        
        #region Build Database Parameters

            [TaskAttribute("buildDatabaseName", Required = false)]
            public string BuildDatabaseName
            { get { return parameters.BuildDatabase.Name; }
              set { parameters.BuildDatabase.Name = value; } }

            [TaskAttribute("buildDatabaseServer", Required = false)]
            public string BuildDatabaseServer
            { get { return parameters.BuildDatabase.Server; }
              set { parameters.BuildDatabase.Server = value; } }

            [TaskAttribute("buildDatabaseUsername", Required = false)]
            public string BuildDatabaseUsername
            { get { return parameters.BuildDatabase.Username; }
              set { parameters.BuildDatabase.Username = value; } }

            [TaskAttribute("buildDatabasePassword", Required = false)]
            public string BuildDatabasePassword
            { get { return parameters.BuildDatabase.Password; }
              set { parameters.BuildDatabase.Password = value; } }

            [TaskAttribute("buildDatabaseAuthenticationMode", Required = false)]
            public string BuildDatabaseAuthenticationMode
            { get { return parameters.BuildDatabase.Authentication.ToString(); }
                set { parameters.BuildDatabase.Authentication = 
                    value.ParseEnum<Parameters.Database.AuthenticationMode>(); }}

            [TaskAttribute("preserveBuildDatabase", Required = false)]
            public string PreserveBuildDatabase
            { get { return parameters.PreserveBuildDatabase.ToString(); }
              set { parameters.PreserveBuildDatabase = value; } }

        #endregion

        #region Build Template Database Parameters
        
            [TaskAttribute("buildDatabaseTemplateScript", Required = false)]
            public string BuildDatabaseTemplateScript
            { get { return parameters.BuildDatabaseTemplateScript; }
              set { parameters.BuildDatabaseTemplateScript = value; } }

            [TaskAttribute("buildDatabaseTemplateName", Required = false)]
            public string BuildDatabaseTemplateName
            { get { return parameters.BuildDatabaseTemplateName; }
              set { parameters.BuildDatabaseTemplateName = value; } }

            [TaskAttribute("buildDatabaseNoTemplate", Required = false)]
            public string BuildDatabaseNoTemplate
            { get { return parameters.BuildDatabaseNoTemplate; }
              set { parameters.BuildDatabaseNoTemplate = value; } }

        #endregion

        #region Compare Source Database Parameters

            [TaskAttribute("compareSourceDatabaseName", Required = false)]
            public string CompareSourceDatabaseName
            { get { return parameters.CompareSourceDatabase.Name; }
              set { parameters.CompareSourceDatabase.Name = value; } }

            [TaskAttribute("compareSourceDatabaseServer", Required = false)]
            public string CompareSourceDatabaseServer
            { get { return parameters.CompareSourceDatabase.Server; }
              set { parameters.CompareSourceDatabase.Server = value; } }

            [TaskAttribute("compareSourceDatabaseUsername", Required = false)]
            public string CompareSourceDatabaseUsername
            { get { return parameters.CompareSourceDatabase.Username; }
              set { parameters.CompareSourceDatabase.Username = value; } }

            [TaskAttribute("compareSourceDatabasePassword", Required = false)]
            public string CompareSourceDatabasePassword
            { get { return parameters.CompareSourceDatabase.Password; }
              set { parameters.CompareSourceDatabase.Password = value; } }

            [TaskAttribute("compareSourceDatabaseAuthenticationMode", Required = false)]
            public string CompareSourceDatabaseAuthenticationMode
            { get { return parameters.CompareSourceDatabase.Authentication.ToString(); }
              set { parameters.CompareSourceDatabase.Authentication =
                  value.ParseEnum<Parameters.Database.AuthenticationMode>();
              }
            }

        #endregion

        #region Compare Target Database Parameters

            [TaskAttribute("compareTargetDatabaseName", Required = false)]
            public string CompareTargetDatabaseName
            { get { return parameters.CompareTargetDatabase.Name; }
              set { parameters.CompareTargetDatabase.Name = value; } }

            [TaskAttribute("compareTargetDatabaseServer", Required = false)]
            public string CompareTargetDatabaseServer
            { get { return parameters.CompareTargetDatabase.Server; }
              set { parameters.CompareTargetDatabase.Server = value; } }

            [TaskAttribute("compareTargetDatabaseUsername", Required = false)]
            public string CompareTargetDatabaseUsername
            { get { return parameters.CompareTargetDatabase.Username; }
              set { parameters.CompareTargetDatabase.Username = value; } }

            [TaskAttribute("compareTargetDatabasePassword", Required = false)]
            public string CompareTargetDatabasePassword
            { get { return parameters.CompareTargetDatabase.Password; }
              set { parameters.CompareTargetDatabase.Password = value; } }

            [TaskAttribute("compareTargetDatabaseAuthenticationMode", Required = false)]
            public string CompareTargetDatabaseAuthenticationMode
            { get { return parameters.CompareTargetDatabase.Authentication.ToString(); }
              set { parameters.CompareTargetDatabase.Authentication =
                  value.ParseEnum<Parameters.Database.AuthenticationMode>();
              }
            }

        #endregion

        #region Public Methods

            public void Run()
            {
                ExecuteTask();
            }

        #endregion

        #region Protected Overrides

            protected override void ExecuteTask()
            {
                Application application;
                application = new Application(parameters);
                if (!application.Run())
                {
                    if (ResultProperty != null) Properties[ResultProperty] = "1";
                    throw new BuildException("DBGhost Change Manager encountered and error. View the log for more information.");
                }
                else if (ResultProperty != null) Properties[ResultProperty] = "0";
            }

        #endregion
    }
}
