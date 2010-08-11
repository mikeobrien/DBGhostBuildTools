using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DBGhost.Build.ChangeManager;
using DBGhost.Build.Reports;

namespace DBGhost.Build.Util
{
    class Program
    {
        static void Main(string[] args)
        {
            new Runtime().Run(args);
        }
    }
}
