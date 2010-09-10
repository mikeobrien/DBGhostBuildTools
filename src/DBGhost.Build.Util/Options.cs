using DbGhost.Build.Util.Initialization;

namespace DbGhost.Build.Util
{
    [OptionGroup("Build Utility Options", "Options for the DB Ghost Build Utility")]
    public class Options
    {
        [Option("module", "Module to execute: ChangeManager")]
        public string Module { get; set; }
    }
}
