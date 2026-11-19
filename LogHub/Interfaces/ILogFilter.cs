using LogHub.Models;
namespace LogHub.Interfaces;
public interface ILogFilter { bool ShouldLog(LogEntry entry); }