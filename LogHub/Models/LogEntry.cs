namespace LogHub.Models;

public class LogEntry
{
    public DateTime Date { get; set; }
    public string Severity { get; set; } = "Info";
    public string Message { get; set; } = string.Empty;
}