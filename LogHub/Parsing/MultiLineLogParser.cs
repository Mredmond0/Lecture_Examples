using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Parsing;

public class MultiLineLogParser : ILogParser
{
    public IEnumerable<LogEntry> Parse(IEnumerable<string> lines)
    {
        var result = new List<LogEntry>();
        LogEntry? current = null;

        foreach (var line in lines)
        {
            if (line.StartsWith(" ") || line.StartsWith("at "))
            {
                if (current != null) current.Message += "\n" + line;
                continue;
            }

            if (current != null) result.Add(current);
            
            var parts = line.Split('|', 3);
            if (parts.Length == 3)
            {
                current = new LogEntry
                {
                    Date = DateTime.TryParse(parts[0], out var d) ? d : DateTime.Now,
                    Severity = parts[1].Trim(),
                    Message = parts[2].Trim()
                };
            }
            else
            {
                current = null;
            }
        }

        if (current != null) result.Add(current);
        return result;
    }
}