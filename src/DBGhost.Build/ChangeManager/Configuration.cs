using System.Collections.Generic;
using System.Xml;
using System.IO;
using DbGhost.Build.Extensions;
using XPath = DbGhost.Build.ChangeManager.ConfigurationXPath;

namespace DbGhost.Build.ChangeManager
{
    public class Configuration
    {
        readonly XmlDocument _configuration;

        public Configuration(string path)
        {
            _configuration = new XmlDocument();
            _configuration.Load(path);
        }

        public string ProcessType
        {
            get { return _configuration.GetValue(XPath.ChangeProcessType); }
            set { _configuration.SetValue(XPath.ChangeProcessType, value); }
        }
        
        public string RootDirectory
        {
            get { return _configuration.GetValue(XPath.ChangeRootDir); }
            set 
            {
                _configuration.SetValue(XPath.ChangeRootDir, value);
                _configuration.SetValue(XPath.ScriptOutputPath, value);
                SetScriptPathsRoot(value);
            } 
        }
        
        public string ConfigurationPath
        {
            get { return _configuration.GetValue(XPath.ChangeSavePath); }
            set { _configuration.SetValue(XPath.ChangeSavePath, value); }
        }
        
        public string BuildFile
        {
            get { return _configuration.GetValue(XPath.ChangeBuildFile); }
            set { _configuration.SetValue(XPath.ChangeBuildFile, value); }
        }
        
        public string DeltaFile
        {
            get { return _configuration.GetValue(XPath.ChangeDeltaFile); }
            set { _configuration.SetValue(XPath.ChangeDeltaFile, value); }
        }

        public string ReportPath
        {
            get { return _configuration.GetValue(XPath.ChangeReportPath); }
            set 
            {
                _configuration.SetValue(XPath.ChangeReportPath, value);
                _configuration.SetValue(XPath.ScriptReportPath, value); 
            }
        }

        public XmlDocument Source
        { get { return _configuration; } }

        // Build Database Parameters

        public string BuildDatabaseName
        {
            get { return _configuration.GetValue(XPath.ChangeBuildDb); }
            set { _configuration.SetValue(XPath.ChangeBuildDb, value); }
        }
        
        public string PreserveBuildDatabase
        {
            get { return _configuration.GetValue(XPath.ChangeCompareOptionsKeepNewDb); }
            set { _configuration.SetValue(XPath.ChangeCompareOptionsKeepNewDb, value); }
        }

        // Build Template Database Parameters
        
        public string BuildTemplateDatabaseScript
        {
            get { return _configuration.GetValue(XPath.ChangeSchemaScriptsDropCreateDbase); }
            set { _configuration.SetValue(XPath.ChangeSchemaScriptsDropCreateDbase, value); }
        }

        public string BuildTemplateDatabaseName
        {
            get { return _configuration.GetValue(XPath.ChangeTemplateDbName); }
            set { _configuration.SetValue(XPath.ChangeTemplateDbName, value); }
        }

        public string BuildTemplateDatabaseServer
        {
            get { return _configuration.GetValue(XPath.ChangeTemplateDbServer); }
            set { _configuration.SetValue(XPath.ChangeTemplateDbServer, value); }
        }

        public string BuildTemplateDatabaseUsername
        {
            get { return _configuration.GetValue(XPath.ChangeTemplateDbUsername); }
            set { _configuration.SetValue(XPath.ChangeTemplateDbUsername, value); }
        }

        public string BuildTemplateDatabasePassword
        {
            get { return _configuration.GetValue(XPath.ChangeTemplateDbPassword); }
            set { _configuration.SetValue(XPath.ChangeTemplateDbPassword, value); }
        }

        public string BuildTemplateDatabaseAuthenticationMode
        {
            get { return _configuration.GetValue(XPath.ChangeTemplateDbAuthMode); }
            set { _configuration.SetValue(XPath.ChangeTemplateDbAuthMode, value); }
        }
                
        // Compare Source Database Parameters

        public string CompareSourceDatabaseName
        {
            get { return _configuration.GetValue(XPath.ChangeSourceDbName); }
            set { _configuration.SetValue(XPath.ChangeSourceDbName, value); }
        }

        public string CompareSourceDatabaseServer
        {
            get { return _configuration.GetValue(XPath.ChangeSourceDbServer); }
            set { _configuration.SetValue(XPath.ChangeSourceDbServer, value); }
        }

        public string CompareSourceDatabaseUsername
        {
            get { return _configuration.GetValue(XPath.ChangeSourceDbUsername); }
            set { _configuration.SetValue(XPath.ChangeSourceDbUsername, value); }
        }

        public string CompareSourceDatabasePassword
        {
            get { return _configuration.GetValue(XPath.ChangeSourceDbPassword); }
            set { _configuration.SetValue(XPath.ChangeSourceDbPassword, value); }
        }

        public string CompareSourceDatabaseAuthenticationMode
        {
            get { return _configuration.GetValue(XPath.ChangeSourceDbAuthMode); }
            set { _configuration.SetValue(XPath.ChangeSourceDbAuthMode, value); }
        }

        // Compare Target Database Parameters

        public string CompareTargetDatabaseName
        {
            get { return _configuration.GetValue(XPath.ChangeTargetDbName); }
            set { _configuration.SetValue(XPath.ChangeTargetDbName, value); }
        }

        public string CompareTargetDatabaseServer
        {
            get { return _configuration.GetValue(XPath.ChangeTargetDbServer); }
            set { _configuration.SetValue(XPath.ChangeTargetDbServer, value); }
        }

        public string CompareTargetDatabaseUsername
        {
            get { return _configuration.GetValue(XPath.ChangeTargetDbUsername); }
            set { _configuration.SetValue(XPath.ChangeTargetDbUsername, value); }
        }

        public string CompareTargetDatabasePassword
        {
            get { return _configuration.GetValue(XPath.ChangeTargetDbPassword); }
            set { _configuration.SetValue(XPath.ChangeTargetDbPassword, value); }
        }

        public string CompareTargetDatabaseAuthenticationMode
        {
            get { return _configuration.GetValue(XPath.ChangeTargetDbAuthMode); }
            set { _configuration.SetValue(XPath.ChangeTargetDbAuthMode, value); }
        }

        // Script Database Parameters

        public string ScriptDatabaseName
        {
            get { return _configuration.GetValue(XPath.ScriptDbName); }
            set { _configuration.SetValue(XPath.ScriptDbName, value); }
        }

        public string ScriptDatabaseServer
        {
            get { return _configuration.GetValue(XPath.ScriptDbServer); }
            set { _configuration.SetValue(XPath.ScriptDbServer, value); }
        }

        public string ScriptDatabaseUsername
        {
            get { return _configuration.GetValue(XPath.ScriptDbUsername); }
            set { _configuration.SetValue(XPath.ScriptDbUsername, value); }
        }

        public string ScriptDatabasePassword
        {
            get { return _configuration.GetValue(XPath.ScriptDbPassword); }
            set { _configuration.SetValue(XPath.ScriptDbPassword, value); }
        }

        public Dictionary<string, string> GetSchemaScriptPaths()
        {
            var scriptPaths = new Dictionary<string, string>();

            var paths = _configuration.SelectNodes(XPath.ChangeSchemaScriptsPath);
            if (paths != null)
            {
                foreach (XmlNode pathNode in paths)
                {
                    var path = pathNode.FirstChild.Value;
                    if (!scriptPaths.ContainsKey(path) && pathNode.ParentNode != null)
                        scriptPaths.Add(path, pathNode.ParentNode.Name);
                }
            }
            return scriptPaths;
        }

        public void Save()
        {
            _configuration.Save(ConfigurationPath);
        }

        private void SetScriptPathsRoot(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            var paths = _configuration.SelectNodes(XPath.ChangeSchemaScriptsPath);
            if (paths == null) return;

            foreach (XmlNode pathNode in paths)
            {
                if (pathNode.FirstChild == null || pathNode.FirstChild.Value.IsNullOrEmpty(true)) continue;

                string objectFolder = null;
                string objectFile = null;

                if (pathNode.ParentNode != null)
                    switch (pathNode.ParentNode.Name)
                    {
                        case "AfterBuildScript":
                        case "BeforeSyncScript":
                        case "AfterSyncScript":
                            objectFolder = Path.GetDirectoryName(pathNode.FirstChild.Value);
                            objectFile = Path.GetFileName(pathNode.FirstChild.Value);
                            break;
                        default:
                            objectFolder = pathNode.FirstChild.Value;
                            break;
                    }

                if (string.IsNullOrEmpty(objectFolder)) continue;

                if (!string.IsNullOrEmpty(objectFile))
                    pathNode.FirstChild.Value = Path.Combine(Path.Combine(path, objectFolder), objectFile);
                else
                {
                    var pathParts = objectFolder.Split('\\');
                    objectFolder = pathParts[pathParts.Length - 1];
                    pathNode.FirstChild.Value = Path.Combine(path, objectFolder);
                }
            }
        }
    }
}
