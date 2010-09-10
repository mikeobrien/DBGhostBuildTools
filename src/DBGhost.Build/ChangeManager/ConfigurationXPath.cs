namespace DbGhost.Build.ChangeManager
{
    public static class ConfigurationXPath
    {
        public const string ChangeProcessType = "/DBGhost/ChangeManager/ChangeManagerProcessType";
        public const string ChangeSavePath = "/DBGhost/ChangeManager/SavePath";
        public const string ChangeReportPath = "/DBGhost/ChangeManager/ReportFileName";
        public const string ChangeRootDir = "/DBGhost/ChangeManager/SchemaScripts/RootDirectory";
        public const string ChangeSchemaScripts = "/DBGhost/ChangeManager/SchemaScripts/";
        public const string ChangeSchemaScriptsPath = ChangeSchemaScripts + "*/Path";
        public const string ChangeSchemaScriptsDropCreateDbase = ChangeSchemaScripts + "DropCreateDatabaseScript";

        public const string ChangeBuildFile = "/DBGhost/ChangeManager/BuildSQLFileName";
        public const string ChangeDeltaFile = "/DBGhost/ChangeManager/DeltaScriptsFileName";
        public const string ChangeBuildDb = "/DBGhost/ChangeManager/BuildDBName";

        public const string ChangeTemplateDbName = "/DBGhost/ChangeManager/TemplateDB/DBName";
        public const string ChangeTemplateDbServer = "/DBGhost/ChangeManager/TemplateDB/DBServer";
        public const string ChangeTemplateDbUsername = "/DBGhost/ChangeManager/TemplateDB/DBUserName";
        public const string ChangeTemplateDbPassword = "/DBGhost/ChangeManager/TemplateDB/DBPassword";
        public const string ChangeTemplateDbAuthMode = "/DBGhost/ChangeManager/TemplateDB/AuthenticationMode";

        public const string ChangeSourceDbName = "/DBGhost/ChangeManager/SourceDB/DBName";
        public const string ChangeSourceDbServer = "/DBGhost/ChangeManager/SourceDB/DBServer";
        public const string ChangeSourceDbUsername = "/DBGhost/ChangeManager/SourceDB/DBUserName";
        public const string ChangeSourceDbPassword = "/DBGhost/ChangeManager/SourceDB/DBPassword";
        public const string ChangeSourceDbAuthMode = "/DBGhost/ChangeManager/SourceDB/AuthenticationMode";

        public const string ChangeTargetDbName = "/DBGhost/ChangeManager/TargetDB/DBName";
        public const string ChangeTargetDbServer = "/DBGhost/ChangeManager/TargetDB/DBServer";
        public const string ChangeTargetDbUsername = "/DBGhost/ChangeManager/TargetDB/DBUserName";
        public const string ChangeTargetDbPassword = "/DBGhost/ChangeManager/TargetDB/DBPassword";
        public const string ChangeTargetDbAuthMode = "/DBGhost/ChangeManager/TargetDB/AuthenticationMode";

        public const string ScriptReportPath = "/DBGhost/Scripter/ReportFilename";
        public const string ScriptOutputPath = "/DBGhost/Scripter/OutputFolder";

        public const string ScriptDbName = "/DBGhost/Scripter/DatabaseToScript/Database";
        public const string ScriptDbServer = "/DBGhost/Scripter/DatabaseToScript/Server";
        public const string ScriptDbUsername = "/DBGhost/Scripter/DatabaseToScript/Username";
        public const string ScriptDbPassword = "/DBGhost/Scripter/DatabaseToScript/Password";

        public const string ChangeCompareOptions = "/DBGhost/ChangeManager/CompareOptions/";
        public const string ChangeCompareOptionsKeepNewDb = ChangeCompareOptions + "KeepNewDatabase";
    }
}
