using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Formatting;

public class PlainFormatter : ILogFormatter
{
    public string Format(LogEntry entry) => $"{entry.Date:yyyy-MM-dd HH:mm:ss} [{entry.Severity}] {entry.Message}";
}