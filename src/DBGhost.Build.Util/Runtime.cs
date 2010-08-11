using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DBGhost.Build.Util
{
    internal class Runtime
    {
        // ────────────────────────── Public Members ──────────────────────────

        public void Run(string[] args)
        {
            Console.WriteLine("DB Ghost Build Utility - {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine();

            Options options = new Options();
            Initialization.Options.Load(Environment.CommandLine, options);

            if (string.IsNullOrEmpty(options.Module))
            {
                Console.WriteLine(" Ex: DBGhost.Build.Util.exe /module=ChangeManager /processType=CopyDatabase ...");
                Console.WriteLine();

                Initialization.Options.Display(new Type[] { typeof(Options), typeof(ChangeManager.Options) });
            }
            else
            {
                switch (options.Module.ToLower())
                {
                    case "changemanager" :
                        RunChangeManager(args);
                        break;
                    default: 
                        Console.WriteLine("Error: Invalid module.");
                        break;
                }
            }

            Console.WriteLine();
        }

        // ────────────────────────── Private Members ──────────────────────────

        private void RunChangeManager(string[] args)
        {
            ChangeManager.Options options = new DBGhost.Build.Util.ChangeManager.Options();
            Initialization.Options.Load(Environment.CommandLine, options);
            DBGhost.Build.ChangeManager.Application application;
            application = new DBGhost.Build.ChangeManager.Application(GetChangeManagerParameters(options));
            if (!application.Run())
                Console.WriteLine("DBGhost Change Manager encountered and error. View the log for more information.");
        }

        private DBGhost.Build.ChangeManager.Parameters GetChangeManagerParameters(DBGhost.Build.Util.ChangeManager.Options options)
        {
            DBGhost.Build.ChangeManager.Parameters parameters = 
                new DBGhost.Build.ChangeManager.Parameters();

            parameters.ApplicationPath = options.ApplicationPath;
            parameters.ArtifactsDirectory = options.ArtifactsFolder;
            parameters.BuildDatabaseTemplateName = options.BuildDatabaseTemplateName;
            parameters.BuildDatabaseTemplateScript = options.BuildDatabaseTemplateScript;
            parameters.BuildDatabaseNoTemplate = options.BuildDatabaseNoTemplate;
            parameters.BuildScriptPath = options.BuildFilePath;
            parameters.CompareDeltaScriptPath = options.DeltaFilePath;
            parameters.ConfigurationPath = options.ConfigurationPath;
            parameters.PreserveBuildDatabase = options.PreserveBuildDatabase;
            parameters.ProcessMode = (DBGhost.Build.ChangeManager.Parameters.ProcessType)Enum.Parse(
                    typeof(DBGhost.Build.ChangeManager.Parameters.ProcessType),
                    options.ProcessType);
            parameters.ReportFilePath = options.ReportFilePath;
            parameters.RootDirectory = options.RootFolder;
            parameters.TemplateConfigurationPath = options.TemplateConfigurationPath;
            parameters.XmlReportFilePath = options.XmlReportFilePath;
            parameters.BuildDatabase = new DBGhost.Build.ChangeManager.Parameters.Database()
                {
                    Authentication = options.BuildDatabaseAuthenticationMode != null ?
                            (DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode)Enum.Parse(
                            typeof(DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode),
                            options.BuildDatabaseAuthenticationMode) : 
                            DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode.Windows,
                    Name = options.BuildDatabaseName,
                    Password = options.BuildDatabasePassword,
                    Server = options.BuildDatabaseServer,
                    Username = options.BuildDatabaseUsername
                };
            parameters.CompareSourceDatabase = new DBGhost.Build.ChangeManager.Parameters.Database()
                {
                    Authentication = options.CompareSourceDatabaseAuthenticationMode != null ?
                            (DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode)Enum.Parse(
                            typeof(DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode),
                            options.CompareSourceDatabaseAuthenticationMode) : 
                            DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode.Windows,
                    Name = options.CompareSourceDatabaseName,
                    Password = options.CompareSourceDatabasePassword,
                    Server = options.CompareSourceDatabaseServer,
                    Username = options.CompareSourceDatabaseUsername
                };
            parameters.CompareTargetDatabase = new DBGhost.Build.ChangeManager.Parameters.Database()
                {
                    Authentication = options.CompareTargetDatabaseAuthenticationMode != null ?
                            (DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode)Enum.Parse(
                            typeof(DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode),
                            options.CompareTargetDatabaseAuthenticationMode) : 
                            DBGhost.Build.ChangeManager.Parameters.Database.AuthenticationMode.Windows,
                    Name = options.CompareTargetDatabaseName,
                    Password = options.CompareTargetDatabasePassword,
                    Server = options.CompareTargetDatabaseServer,
                    Username = options.CompareTargetDatabaseUsername
                };
            parameters.ScriptSourceDatabase = new DBGhost.Build.ChangeManager.Parameters.Database()
                {
                    Name = options.ScriptSourceDatabaseName,
                    Password = options.ScriptSourceDatabasePassword,
                    Server = options.ScriptSourceDatabaseServer,
                    Username = options.ScriptSourceDatabaseUsername
                };

            return parameters;
        }
    }
}
