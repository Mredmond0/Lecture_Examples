using LogHub.Filtering;
using LogHub.Interfaces;
using LogHub.Parsing;
using LogHub.Processing;
using LogHub.Sinks;

var lines = new[]
{
    "2026-01-01 10:00:00|Error|NullReferenceException",
    "  at Service.Method()",
    "  at Controller.Handle()",
    "2026-01-01 10:01:00|Info|All good"
};

var processor = new LogProcessor(
    new MultiLineLogParser(),
    new SeverityFilter("Info"),
    new ILogSink[] { new ConsoleSink(), new FileSink("output.log") }
);

processor.Process(lines);
Console.WriteLine("Готово!");