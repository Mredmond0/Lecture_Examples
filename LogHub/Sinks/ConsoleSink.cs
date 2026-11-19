using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Sinks;

public class ConsoleSink : ILogSink
{
    public void Write(LogEntry entry) => Console.WriteLine($"[CONSOLE] {entry.Message}");
}