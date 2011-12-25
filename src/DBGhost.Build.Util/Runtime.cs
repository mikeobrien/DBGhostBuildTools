using System;
using System.Reflection;
using DbGhost.Build.ChangeManager;

namespace DbGhost.Build.Util
{
    internal class Runtime
    {
        // ────────────────────────── Public Members ──────────────────────────

        public bool Run(string[] args)
        {
            Console.WriteLine("DB Ghost Build Utility - {0}", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();

            var result = true;
            var options = new Options();
            Initialization.Options.Load(Environment.CommandLine, options);

            if (string.IsNullOrEmpty(options.Module))
            {
                Console.WriteLine(" Ex: DBGhost.Build.Util.exe /module=ChangeManager /processType=CopyDatabase ...");
                Console.WriteLine();

                Initialization.Options.Display(new [] { typeof(Options), typeof(ChangeManager.Options) });
            }
            else
            {
                switch (options.Module.ToLower())
                {
                    case "changemanager" :
                        result = RunChangeManager();
                        break;
                    default: 
                        Console.WriteLine("Error: Invalid module.");
                        break;
                }
            }

            Console.WriteLine();
            return result;
        }

        // ────────────────────────── Private Members ──────────────────────────

        private static bool RunChangeManager()
        {
            var options = new ChangeManager.Options();
            Initialization.Options.Load(Environment.CommandLine, options);
            var application = new Application(GetChangeManagerParameters(options));
            var result = application.Run();
            if (!result.Success) Console.WriteLine("DBGhost Change Manager encountered and error. View the log for more information.");
            return result.Success;
        }

        private static Parameters GetChangeManagerParameters(ChangeManager.Options options)
        {
            var parameters = new Parameters();

            parameters.ApplicationPath = options.ApplicationPath;
            parameters.ArtifactsDirectory = options.ArtifactsFolder;
            parameters.BuildDatabaseTemplateName = options.BuildDatabaseTemplateName;
            parameters.BuildDatabaseTemplateScript = options.BuildDatabaseTemplateScript;
            parameters.BuildScriptPath = options.BuildFilePath;
            parameters.CompareDeltaScriptPath = options.DeltaFilePath;
            parameters.ConfigurationPath = options.ConfigurationPath;
            parameters.PreserveBuildDatabase = options.PreserveBuildDatabase;
            parameters.ProcessMode = (Parameters.ProcessType) Enum.Parse(typeof(Parameters.ProcessType), options.ProcessType);
            parameters.ReportFilePath = options.ReportFilePath;
            parameters.RootDirectory = options.RootFolder;
            parameters.TemplateConfigurationPath = options.TemplateConfigurationPath;
            parameters.XmlReportFilePath = options.XmlReportFilePath;
            parameters.BuildDatabase = new Parameters.Database
                                {
                                    Authentication = options.BuildDatabaseAuthenticationMode != null
                                                         ? (Parameters.Database.AuthenticationMode)
                                                           Enum.Parse(
                                                               typeof (
                                                                   Parameters.Database.AuthenticationMode),
                                                               options.BuildDatabaseAuthenticationMode)
                                                         : Parameters.Database.AuthenticationMode.Windows,
                                    Name = options.BuildDatabaseName,
                                    Password = options.BuildDatabasePassword,
                                    Server = options.BuildDatabaseServer,
                                    Username = options.BuildDatabaseUsername
                                };
            parameters.CompareSourceDatabase = new Parameters.Database
                                        {
                                            Authentication =
                                                options.CompareSourceDatabaseAuthenticationMode != null
                                                    ? (Parameters.Database.AuthenticationMode) Enum.Parse(
                                                        typeof (Parameters.Database.AuthenticationMode),
                                                        options.CompareSourceDatabaseAuthenticationMode)
                                                    : Parameters.Database.AuthenticationMode.Windows,
                                            Name = options.CompareSourceDatabaseName,
                                            Password = options.CompareSourceDatabasePassword,
                                            Server = options.CompareSourceDatabaseServer,
                                            Username = options.CompareSourceDatabaseUsername
                                        };
            parameters.CompareTargetDatabase = new Parameters.Database
                                        {
                                            Authentication =
                                                options.CompareTargetDatabaseAuthenticationMode != null
                                                    ? (Parameters.Database.AuthenticationMode) Enum.Parse(
                                                        typeof (Parameters.Database.AuthenticationMode),
                                                        options.CompareTargetDatabaseAuthenticationMode)
                                                    : Parameters.Database.AuthenticationMode.Windows,
                                            Name = options.CompareTargetDatabaseName,
                                            Password = options.CompareTargetDatabasePassword,
                                            Server = options.CompareTargetDatabaseServer,
                                            Username = options.CompareTargetDatabaseUsername
                                        };
            parameters.ScriptSourceDatabase = new Parameters.Database
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
