using System;

namespace DbGhost.Build.Util.Initialization
{
	internal class Options
	{
        public static void Display(params Type[] optionGroups)
        {
            var writer = new OptionWriter(optionGroups, Console.WindowWidth, true);
            Console.Write(writer.ToString());
        }

        public static void Load(string args, object options)
        {
            OptionLoader.Load(options, args);
        }
	}
}
