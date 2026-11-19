using LogHub.Interfaces;
using LogHub.Models;

namespace LogHub.Processing;

public class LogProcessor
{
    private readonly ILogParser _parser;
    private readonly ILogFilter _filter;
    private readonly IEnumerable<ILogSink> _sinks;

    public LogProcessor(ILogParser parser, ILogFilter filter, IEnumerable<ILogSink> sinks)
    {
        _parser = parser;
        _filter = filter;
        _sinks = sinks;
    }

    public void Process(IEnumerable<string> rawLines)
    {
        foreach (var entry in _parser.Parse(rawLines))
        {
            if (!_filter.ShouldLog(entry)) continue;
            foreach (var sink in _sinks) sink.Write(entry);
        }
    }
}