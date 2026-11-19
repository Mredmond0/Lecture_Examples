using LogHub.Filtering;
using LogHub.Interfaces;
using LogHub.Models;
using LogHub.Parsing;
using LogHub.Processing;
using Moq;

namespace LogHubTests;

public class LogProcessorTests
{
    [Fact]
    public void Parser_ParsesSingleLine()
    {
        var parser = new MultiLineLogParser();
        var result = parser.Parse(new[] { "2026-01-01 10:00:00|Error|Ошибка" }).ToList();
        Assert.Single(result);
        Assert.Equal("Ошибка", result[0].Message);
    }

    [Fact]
    public void Parser_GluesMultiLineStackTraces()
    {
        var parser = new MultiLineLogParser();
        var result = parser.Parse(new[] { "2026-01-01 10:00:00|Error|Exception", "  at MethodA()" }).ToList();
        Assert.Single(result);
        Assert.Contains("\n  at MethodA()", result[0].Message);
    }

    [Fact]
    public void Filter_WorksCorrectly()
    {
        var filter = new SeverityFilter("Warning");
        Assert.False(filter.ShouldLog(new LogEntry { Severity = "Info" }));
        Assert.True(filter.ShouldLog(new LogEntry { Severity = "Error" }));
    }

    [Fact]
    public void Processor_CallsSink()
    {
        var mockSink = new Mock<ILogSink>();
        var processor = new LogProcessor(new MultiLineLogParser(), new SeverityFilter("Info"), new[] { mockSink.Object });
        processor.Process(new[] { "2026-01-01 10:00:00|Info|test" });
        mockSink.Verify(s => s.Write(It.IsAny<LogEntry>()), Times.Once);
    }

    [Fact]
    public void Processor_DoesNotWriteFilteredLogs()
    {
        var mockSink = new Mock<ILogSink>();
        var processor = new LogProcessor(new MultiLineLogParser(), new SeverityFilter("Error"), new[] { mockSink.Object });
        processor.Process(new[] { "2026-01-01 10:00:00|Info|test" });
        mockSink.Verify(s => s.Write(It.IsAny<LogEntry>()), Times.Never);
    }

    [Fact]
    public void Processor_ProcessesMultipleLines()
    {
        var mockSink = new Mock<ILogSink>();
        var processor = new LogProcessor(new MultiLineLogParser(), new SeverityFilter("Info"), new[] { mockSink.Object });
        processor.Process(new[] { "2026-01-01 10:00:00|Info|1", "2026-01-01 10:00:01|Info|2" });
        mockSink.Verify(s => s.Write(It.IsAny<LogEntry>()), Times.Exactly(2));
    }
}