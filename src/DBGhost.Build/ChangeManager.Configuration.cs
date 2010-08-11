using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using DBGhost.Build.Extensions;

namespace DBGhost.Build.ChangeManager
{
    public class Configuration
    {
        #region Private Fields

            private const string CHANGE_PROCESS_TYPE_XPATH = "/DBGhost/ChangeManager/ChangeManagerProcessType";
            private const string CHANGE_SAVE_PATH_XPATH = "/DBGhost/ChangeManager/SavePath";
            private const string CHANGE_REPORT_PATH_XPATH = "/DBGhost/ChangeManager/ReportFileName";
            private const string CHANGE_ROOT_DIR_XPATH = "/DBGhost/ChangeManager/SchemaScripts/RootDirectory";
            private const string CHANGE_SCHEMA_SCRIPTS_XPATH = "/DBGhost/ChangeManager/SchemaScripts/";
            private const string CHANGE_SCHEMA_SCRIPTS_PATH_XPATH = CHANGE_SCHEMA_SCRIPTS_XPATH + "*/Path";
            private const string CHANGE_SCHEMA_SCRIPTS_DROP_CREATE_DBASE_XPATH = CHANGE_SCHEMA_SCRIPTS_XPATH + "DropCreateDatabaseScript";
        
            private const string CHANGE_BUILD_FILE_XPATH = "/DBGhost/ChangeManager/BuildSQLFileName";
            private const string CHANGE_DELTA_FILE_XPATH = "/DBGhost/ChangeManager/DeltaScriptsFileName";
            private const string CHANGE_BUILD_DB_XPATH = "/DBGhost/ChangeManager/BuildDBName";

            private const string CHANGE_TEMPLATE_DB_NAME_XPATH = "/DBGhost/ChangeManager/TemplateDB/DBName";
            private const string CHANGE_TEMPLATE_DB_SERVER_XPATH = "/DBGhost/ChangeManager/TemplateDB/DBServer";
            private const string CHANGE_TEMPLATE_DB_USERNAME_XPATH = "/DBGhost/ChangeManager/TemplateDB/DBUserName";
            private const string CHANGE_TEMPLATE_DB_PASSWORD_XPATH = "/DBGhost/ChangeManager/TemplateDB/DBPassword";
            private const string CHANGE_TEMPLATE_DB_AUTH_MODE_XPATH = "/DBGhost/ChangeManager/TemplateDB/AuthenticationMode";

            private const string CHANGE_SOURCE_DB_NAME_XPATH = "/DBGhost/ChangeManager/SourceDB/DBName";
            private const string CHANGE_SOURCE_DB_SERVER_XPATH = "/DBGhost/ChangeManager/SourceDB/DBServer";
            private const string CHANGE_SOURCE_DB_USERNAME_XPATH = "/DBGhost/ChangeManager/SourceDB/DBUserName";
            private const string CHANGE_SOURCE_DB_PASSWORD_XPATH = "/DBGhost/ChangeManager/SourceDB/DBPassword";
            private const string CHANGE_SOURCE_DB_AUTH_MODE_XPATH = "/DBGhost/ChangeManager/SourceDB/AuthenticationMode";

            private const string CHANGE_TARGET_DB_NAME_XPATH = "/DBGhost/ChangeManager/TargetDB/DBName";
            private const string CHANGE_TARGET_DB_SERVER_XPATH = "/DBGhost/ChangeManager/TargetDB/DBServer";
            private const string CHANGE_TARGET_DB_USERNAME_XPATH = "/DBGhost/ChangeManager/TargetDB/DBUserName";
            private const string CHANGE_TARGET_DB_PASSWORD_XPATH = "/DBGhost/ChangeManager/TargetDB/DBPassword";
            private const string CHANGE_TARGET_DB_AUTH_MODE_XPATH = "/DBGhost/ChangeManager/TargetDB/AuthenticationMode";

            private const string SCRIPT_REPORT_PATH_XPATH = "/DBGhost/Scripter/ReportFilename";
            private const string SCRIPT_OUTPUT_PATH_XPATH = "/DBGhost/Scripter/OutputFolder";

            private const string SCRIPT_DB_NAME_XPATH = "/DBGhost/Scripter/DatabaseToScript/Database";
            private const string SCRIPT_DB_SERVER_XPATH = "/DBGhost/Scripter/DatabaseToScript/Server";
            private const string SCRIPT_DB_USERNAME_XPATH = "/DBGhost/Scripter/DatabaseToScript/Username";
            private const string SCRIPT_DB_PASSWORD_XPATH = "/DBGhost/Scripter/DatabaseToScript/Password";

            private const string CHANGE_COMPARE_OPTIONS_XPATH = "/DBGhost/ChangeManager/CompareOptions/";
            private const string CHANGE_COMPARE_OPTIONS_KEEP_NEW_DB_XPATH = CHANGE_COMPARE_OPTIONS_XPATH + "KeepNewDatabase";

            System.Xml.XmlDocument configuration;

        #endregion

        #region Constructor

            public Configuration(string path)
            {
                configuration = new System.Xml.XmlDocument();
                configuration.Load(path);
            }

        #endregion

        #region Public Properties

            public string ProcessType
            { get { return configuration.GetValue(CHANGE_PROCESS_TYPE_XPATH); }
              set { configuration.SetValue(CHANGE_PROCESS_TYPE_XPATH, value); } }
        
            public string RootDirectory
            { get { return configuration.GetValue(CHANGE_ROOT_DIR_XPATH); }
              set 
              {
                  configuration.SetValue(CHANGE_ROOT_DIR_XPATH, value);
                  configuration.SetValue(SCRIPT_OUTPUT_PATH_XPATH, value);
                  SetScriptPathsRoot(value);
              } 
            }
        
            public string ConfigurationPath
            { get { return configuration.GetValue(CHANGE_SAVE_PATH_XPATH); }
              set { configuration.SetValue(CHANGE_SAVE_PATH_XPATH, value); } }
        
            public string BuildFile
            { get { return configuration.GetValue(CHANGE_BUILD_FILE_XPATH); } 
              set { configuration.SetValue(CHANGE_BUILD_FILE_XPATH, value); } }
        
            public string DeltaFile
            { get { return configuration.GetValue(CHANGE_DELTA_FILE_XPATH); } 
              set { configuration.SetValue(CHANGE_DELTA_FILE_XPATH, value); } }

            public string ReportPath
            { get { return configuration.GetValue(CHANGE_REPORT_PATH_XPATH); }
              set 
              {
                  configuration.SetValue(CHANGE_REPORT_PATH_XPATH, value);
                  configuration.SetValue(SCRIPT_REPORT_PATH_XPATH, value); 
              }
            }

            public XmlDocument Source
            { get { return configuration; } }

        #endregion

        #region Build Database Parameters

            public string BuildDatabaseName
            {
                get { return configuration.GetValue(CHANGE_BUILD_DB_XPATH); }
                set { configuration.SetValue(CHANGE_BUILD_DB_XPATH, value); }
            }
        
            public string PreserveBuildDatabase
            {
                get { return configuration.GetValue(CHANGE_COMPARE_OPTIONS_KEEP_NEW_DB_XPATH); }
                set { configuration.SetValue(CHANGE_COMPARE_OPTIONS_KEEP_NEW_DB_XPATH, value); }
            }

        #endregion

        #region Build Template Database Parameters
        
            public string BuildTemplateDatabaseScript
            { get { return configuration.GetValue(CHANGE_SCHEMA_SCRIPTS_DROP_CREATE_DBASE_XPATH); } 
              set { configuration.SetValue(CHANGE_SCHEMA_SCRIPTS_DROP_CREATE_DBASE_XPATH, value); } }

            public string BuildTemplateDatabaseName
            { get { return configuration.GetValue(CHANGE_TEMPLATE_DB_NAME_XPATH); } 
              set { configuration.SetValue(CHANGE_TEMPLATE_DB_NAME_XPATH, value); } }

            public string BuildTemplateDatabaseServer
            { get { return configuration.GetValue(CHANGE_TEMPLATE_DB_SERVER_XPATH); } 
              set { configuration.SetValue(CHANGE_TEMPLATE_DB_SERVER_XPATH, value); } }

            public string BuildTemplateDatabaseUsername
            { get { return configuration.GetValue(CHANGE_TEMPLATE_DB_USERNAME_XPATH); } 
              set { configuration.SetValue(CHANGE_TEMPLATE_DB_USERNAME_XPATH, value); } }

            public string BuildTemplateDatabasePassword
            { get { return configuration.GetValue(CHANGE_TEMPLATE_DB_PASSWORD_XPATH); } 
              set { configuration.SetValue(CHANGE_TEMPLATE_DB_PASSWORD_XPATH, value); } }

            public string BuildTemplateDatabaseAuthenticationMode
            { get { return configuration.GetValue(CHANGE_TEMPLATE_DB_AUTH_MODE_XPATH); } 
              set { configuration.SetValue(CHANGE_TEMPLATE_DB_AUTH_MODE_XPATH, value); } }
                
        #endregion

        #region Compare Source Database Parameters

            public string CompareSourceDatabaseName
            { get { return configuration.GetValue(CHANGE_SOURCE_DB_NAME_XPATH); } 
              set { configuration.SetValue(CHANGE_SOURCE_DB_NAME_XPATH, value); } }

            public string CompareSourceDatabaseServer
            { get { return configuration.GetValue(CHANGE_SOURCE_DB_SERVER_XPATH); } 
              set { configuration.SetValue(CHANGE_SOURCE_DB_SERVER_XPATH, value); } }

            public string CompareSourceDatabaseUsername
            { get { return configuration.GetValue(CHANGE_SOURCE_DB_USERNAME_XPATH); } 
              set { configuration.SetValue(CHANGE_SOURCE_DB_USERNAME_XPATH, value); } }

            public string CompareSourceDatabasePassword
            { get { return configuration.GetValue(CHANGE_SOURCE_DB_PASSWORD_XPATH); } 
              set { configuration.SetValue(CHANGE_SOURCE_DB_PASSWORD_XPATH, value); } }

            public string CompareSourceDatabaseAuthenticationMode
            { get { return configuration.GetValue(CHANGE_SOURCE_DB_AUTH_MODE_XPATH); } 
              set { configuration.SetValue(CHANGE_SOURCE_DB_AUTH_MODE_XPATH, value); }
            }

        #endregion

        #region Compare Target Database Parameters

            public string CompareTargetDatabaseName
            { get { return configuration.GetValue(CHANGE_TARGET_DB_NAME_XPATH); } 
              set { configuration.SetValue(CHANGE_TARGET_DB_NAME_XPATH, value); } }

            public string CompareTargetDatabaseServer
            { get { return configuration.GetValue(CHANGE_TARGET_DB_SERVER_XPATH); } 
              set { configuration.SetValue(CHANGE_TARGET_DB_SERVER_XPATH, value); } }

            public string CompareTargetDatabaseUsername
            { get { return configuration.GetValue(CHANGE_TARGET_DB_USERNAME_XPATH); } 
              set { configuration.SetValue(CHANGE_TARGET_DB_USERNAME_XPATH, value); } }

            public string CompareTargetDatabasePassword
            { get { return configuration.GetValue(CHANGE_TARGET_DB_PASSWORD_XPATH); } 
              set { configuration.SetValue(CHANGE_TARGET_DB_PASSWORD_XPATH, value); } }

            public string CompareTargetDatabaseAuthenticationMode
            { get { return configuration.GetValue(CHANGE_TARGET_DB_AUTH_MODE_XPATH); } 
              set { configuration.SetValue(CHANGE_TARGET_DB_AUTH_MODE_XPATH, value); }
            }

        #endregion

        #region Script Database Parameters

            public string ScriptDatabaseName
            { get { return configuration.GetValue(SCRIPT_DB_NAME_XPATH); } 
              set { configuration.SetValue(SCRIPT_DB_NAME_XPATH, value); } }

            public string ScriptDatabaseServer
            { get { return configuration.GetValue(SCRIPT_DB_SERVER_XPATH); } 
              set { configuration.SetValue(SCRIPT_DB_SERVER_XPATH, value); } }

            public string ScriptDatabaseUsername
            { get { return configuration.GetValue(SCRIPT_DB_USERNAME_XPATH); } 
              set { configuration.SetValue(SCRIPT_DB_USERNAME_XPATH, value); } }

            public string ScriptDatabasePassword
            { get { return configuration.GetValue(SCRIPT_DB_PASSWORD_XPATH); } 
              set { configuration.SetValue(SCRIPT_DB_PASSWORD_XPATH, value); } }

        #endregion

        #region Public Methods

            public Dictionary<string, string> GetSchemaScriptPaths()
            {
                Dictionary<string, string> scriptPaths =
                    new Dictionary<string, string>();

                XmlNodeList paths = configuration.SelectNodes(CHANGE_SCHEMA_SCRIPTS_PATH_XPATH);
                foreach (XmlNode pathNode in paths)
                {
                    string path = pathNode.FirstChild.Value;
                    if (!scriptPaths.ContainsKey(path))
                        scriptPaths.Add(path, pathNode.ParentNode.Name);
                }
                return scriptPaths;
            }

            public void Save()
            {
                configuration.Save(this.ConfigurationPath);
            }

        #endregion

        #region Private Methods

            private void SetScriptPathsRoot(string path)
            {
                if (string.IsNullOrEmpty(path)) return;

                XmlNodeList paths = configuration.SelectNodes(CHANGE_SCHEMA_SCRIPTS_PATH_XPATH);
                foreach (XmlNode pathNode in paths)
                {
                    if (pathNode.FirstChild != null && 
                        !pathNode.FirstChild.Value.IsNullOrEmpty(true))
                    {
                        string objectFolder;
                        string objectFile = null;

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

                        if (!string.IsNullOrEmpty(objectFile))
                            pathNode.FirstChild.Value = Path.Combine(Path.Combine(
                                path, objectFolder), objectFile);
                        else if (!string.IsNullOrEmpty(objectFolder))
                        {
                            string[] pathParts = objectFolder.Split('\\');
                            objectFolder = pathParts[pathParts.Length - 1];
                            pathNode.FirstChild.Value = Path.Combine(path, objectFolder);
                        }
                    }
                }
            }

        #endregion
    }
}
