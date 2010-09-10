using DbGhost.Build.Util.Initialization;

namespace DbGhost.Build.Util.ChangeManager
{
    [OptionGroup("Change Manager Options", "Options for the DB Ghost Change Manager")]
    public class Options
    {
        // ────────────────────────── Private Fields ──────────────────────────

        private const string OptionsResource = "ChangeManager.Options.xml";
        private const string OptionsDescriptionXpath = "/options/option[@name='{name}']";

        // ────────────────────────── Basic Parameters ──────────────────────────

        [Option("processType", OptionsResource, OptionsDescriptionXpath)]
        public string ProcessType { get; set; }

        [Option("applicationPath", OptionsResource, OptionsDescriptionXpath)]
        public string ApplicationPath { get; set; }

        [Option("templateConfigPath", OptionsResource, OptionsDescriptionXpath)]
        public string TemplateConfigurationPath { get; set; }

        [Option("rootDirectory", OptionsResource, OptionsDescriptionXpath)]
        public string RootFolder { get; set; }

        [Option("artifactsDirectory", OptionsResource, OptionsDescriptionXpath)]
        public string ArtifactsFolder { get; set; }

        [Option("configPath", OptionsResource, OptionsDescriptionXpath)]
        public string ConfigurationPath { get; set; }

        [Option("reportFile", OptionsResource, OptionsDescriptionXpath)]
        public string ReportFilePath { get; set; }

        [Option("xmlReportFile", OptionsResource, OptionsDescriptionXpath)]
        public string XmlReportFilePath { get; set; }

        [Option("compareDeltaScript", OptionsResource, OptionsDescriptionXpath)]
        public string DeltaFilePath { get; set; }

        [Option("buildScript", OptionsResource, OptionsDescriptionXpath)]
        public string BuildFilePath { get; set; }

        // ────────────────────────── Script Source Database Parameters ──────────────────────────

        [Option("scriptSourceDBName", OptionsResource, OptionsDescriptionXpath)]
        public string ScriptSourceDatabaseName { get; set; }

        [Option("scriptSourceDBServer", OptionsResource, OptionsDescriptionXpath)]
        public string ScriptSourceDatabaseServer { get; set; }

        [Option("scriptSourceDBUsername", OptionsResource, OptionsDescriptionXpath)]
        public string ScriptSourceDatabaseUsername { get; set; }

        [Option("scriptSourceDBPassword", OptionsResource, OptionsDescriptionXpath)]
        public string ScriptSourceDatabasePassword { get; set; }

        // ────────────────────────── Build Database Parameters ──────────────────────────

        [Option("buildDBName", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseName { get; set; }

        [Option("buildDBServer", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseServer { get; set; }

        [Option("buildDBUsername", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseUsername { get; set; }

        [Option("buildDBPassword", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabasePassword { get; set; }

        [Option("buildDBAuthMode", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseAuthenticationMode { get; set; }

        [Option("preserveBuildDB", OptionsResource, OptionsDescriptionXpath)]
        public string PreserveBuildDatabase { get; set; }
        
        // ────────────────────────── Build Template Database Parameters ──────────────────────────

        [Option("buildDBTemplateScript", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseTemplateScript { get; set; }

        [Option("buildDBTemplateName", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseTemplateName { get; set; }

        [Option("buildDBNoTemplate", OptionsResource, OptionsDescriptionXpath)]
        public string BuildDatabaseNoTemplate { get; set; }
        
        // ────────────────────────── Compare Source Database Parameters ──────────────────────────

        [Option("compareSourceDBName", OptionsResource, OptionsDescriptionXpath)]
        public string CompareSourceDatabaseName { get; set; }

        [Option("compareSourceDBServer", OptionsResource, OptionsDescriptionXpath)]
        public string CompareSourceDatabaseServer { get; set; }

        [Option("compareSourceDBUsername", OptionsResource, OptionsDescriptionXpath)]
        public string CompareSourceDatabaseUsername { get; set; }

        [Option("compareSourceDBPassword", OptionsResource, OptionsDescriptionXpath)]
        public string CompareSourceDatabasePassword { get; set; }

        [Option("compareSourceDBAuthMode", OptionsResource, OptionsDescriptionXpath)]
        public string CompareSourceDatabaseAuthenticationMode { get; set; }

        // ────────────────────────── Compare Target Database Parameters ──────────────────────────

        [Option("compareTargetDBName", OptionsResource, OptionsDescriptionXpath)]
        public string CompareTargetDatabaseName { get; set; }

        [Option("compareTargetDBServer", OptionsResource, OptionsDescriptionXpath)]
        public string CompareTargetDatabaseServer { get; set; }

        [Option("compareTargetDBUsername", OptionsResource, OptionsDescriptionXpath)]
        public string CompareTargetDatabaseUsername { get; set; }

        [Option("compareTargetDBPassword", OptionsResource, OptionsDescriptionXpath)]
        public string CompareTargetDatabasePassword { get; set; }

        [Option("compareTargetDBAuthMode", OptionsResource, OptionsDescriptionXpath)]
        public string CompareTargetDatabaseAuthenticationMode { get; set; }
    }
}
