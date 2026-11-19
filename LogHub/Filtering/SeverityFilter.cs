using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Filtering;

public class SeverityFilter : ILogFilter
{
    private readonly string _minSeverity;
    public SeverityFilter(string minSeverity) => _minSeverity = minSeverity;

    public bool ShouldLog(LogEntry entry) => SeverityToInt(entry.Severity) >= SeverityToInt(_minSeverity);

    private static int SeverityToInt(string severity) => severity switch
    {
        "Debug" => 0, "Info" => 1, "Warning" => 2, "Error" => 3, "Critical" => 4, _ => 1
    };
}