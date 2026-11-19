using LogHub.Models;
namespace LogHub.Interfaces;
public interface ILogFormatter { string Format(LogEntry entry); }