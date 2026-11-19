using LogHub.Models;
namespace LogHub.Interfaces;
public interface ILogParser { IEnumerable<LogEntry> Parse(IEnumerable<string> lines); }