using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBGhost.Build;
using DBGhost.Build.ChangeManager;
using DBGhost.Build.Extensions;
using DBGhost.Build.Util.Initialization;

namespace DBGhost.Build.Util.ChangeManager
{
    [OptionGroup("Change Manager Options", "Options for the DB Ghost Change Manager")]
    public class Options
    {
        // ────────────────────────── Private Fields ──────────────────────────

        private const string OPTIONS_RESOURCE = "ChangeManager.Options.xml";
        private const string OPTIONS_DESCRIPTION_XPATH = "/options/option[@name='{name}']";

        // ────────────────────────── Basic Parameters ──────────────────────────

        [Option("processType", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ProcessType { get; set; }

        [Option("applicationPath", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ApplicationPath { get; set; }

        [Option("templateConfigPath", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string TemplateConfigurationPath { get; set; }

        [Option("rootDirectory", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string RootFolder { get; set; }

        [Option("artifactsDirectory", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ArtifactsFolder { get; set; }

        [Option("configPath", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ConfigurationPath { get; set; }

        [Option("reportFile", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ReportFilePath { get; set; }

        [Option("xmlReportFile", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string XmlReportFilePath { get; set; }

        [Option("compareDeltaScript", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string DeltaFilePath { get; set; }

        [Option("buildScript", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildFilePath { get; set; }

        // ────────────────────────── Script Source Database Parameters ──────────────────────────

        [Option("scriptSourceDBName", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ScriptSourceDatabaseName { get; set; }

        [Option("scriptSourceDBServer", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ScriptSourceDatabaseServer { get; set; }

        [Option("scriptSourceDBUsername", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ScriptSourceDatabaseUsername { get; set; }

        [Option("scriptSourceDBPassword", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string ScriptSourceDatabasePassword { get; set; }

        // ────────────────────────── Build Database Parameters ──────────────────────────

        [Option("buildDBName", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseName { get; set; }

        [Option("buildDBServer", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseServer { get; set; }

        [Option("buildDBUsername", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseUsername { get; set; }

        [Option("buildDBPassword", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabasePassword { get; set; }

        [Option("buildDBAuthMode", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseAuthenticationMode { get; set; }

        [Option("preserveBuildDB", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string PreserveBuildDatabase { get; set; }
        
        // ────────────────────────── Build Template Database Parameters ──────────────────────────

        [Option("buildDBTemplateScript", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseTemplateScript { get; set; }

        [Option("buildDBTemplateName", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseTemplateName { get; set; }

        [Option("buildDBNoTemplate", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string BuildDatabaseNoTemplate { get; set; }
        
        // ────────────────────────── Compare Source Database Parameters ──────────────────────────

        [Option("compareSourceDBName", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareSourceDatabaseName { get; set; }

        [Option("compareSourceDBServer", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareSourceDatabaseServer { get; set; }

        [Option("compareSourceDBUsername", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareSourceDatabaseUsername { get; set; }

        [Option("compareSourceDBPassword", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareSourceDatabasePassword { get; set; }

        [Option("compareSourceDBAuthMode", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareSourceDatabaseAuthenticationMode { get; set; }

        // ────────────────────────── Compare Target Database Parameters ──────────────────────────

        [Option("compareTargetDBName", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareTargetDatabaseName { get; set; }

        [Option("compareTargetDBServer", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareTargetDatabaseServer { get; set; }

        [Option("compareTargetDBUsername", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareTargetDatabaseUsername { get; set; }

        [Option("compareTargetDBPassword", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareTargetDatabasePassword { get; set; }

        [Option("compareTargetDBAuthMode", OPTIONS_RESOURCE, OPTIONS_DESCRIPTION_XPATH)]
        public string CompareTargetDatabaseAuthenticationMode { get; set; }
    }
}
