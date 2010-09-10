namespace DbGhost.Build.Reports
{
    public interface IReportFormatter
    {
        FormatterResult Load(string path);
    }
}
