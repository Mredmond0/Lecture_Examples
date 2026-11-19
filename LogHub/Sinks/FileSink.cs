using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Sinks;

public class FileSink : ILogSink
{
    private readonly string _path;
    public FileSink(string path) => _path = path;
    public void Write(LogEntry entry) => File.AppendAllText(_path, $"[FILE] {entry.Message}\n");
}