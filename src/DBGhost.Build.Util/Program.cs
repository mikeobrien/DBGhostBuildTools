namespace DbGhost.Build.Util
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Runtime().Run(args) ? 0 : 1;
        }
    }
}
