using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBGhost.Build;
using DBGhost.Build.ChangeManager;
using DBGhost.Build.Extensions;
using DBGhost.Build.Util.Initialization;

namespace DBGhost.Build.Util
{
    [OptionGroup("Build Utility Options", "Options for the DB Ghost Build Utility")]
    public class Options
    {
        [Option("module", "Module to execute: ChangeManager")]
        public string Module { get; set; }
    }
}
