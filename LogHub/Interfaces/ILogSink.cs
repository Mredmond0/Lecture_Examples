using LogHub.Models;
namespace LogHub.Interfaces;
public interface ILogSink { void Write(LogEntry entry); }