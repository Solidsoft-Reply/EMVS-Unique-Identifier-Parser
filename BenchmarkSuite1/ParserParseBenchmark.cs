using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

using System;

namespace Solidsoft.Reply.Parsers.Gs1Ai.Benchmarks;

#if NET7_0_OR_GREATER
//[ShortRunJob]
//[EventPipeProfiler(EventPipeProfile.GcVerbose)] // samples object allocations
[MemoryDiagnoser]
public class ParserParseBenchmark
{
    //private const string SampleGs1Data = "01012345678901281720010110ABC123\u001D21520177498093"; // Example GS1-encoded string
    //private List<ResolvedApplicationIdentifierRef> _results;
    private ResolvedEntityDelegate _resolvedEntityDelegate;

    [GlobalSetup]
    public void Setup()
    {
        //_results = [];
        _resolvedEntityDelegate = new(ResolvedEntityDelegate);
    }

    [Benchmark]
    public void Parse_Gs1String()
    {
        //_results.Clear();
        Span<char> sampleGs1Data = ['0', '1', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '8', '1', '7', '2', '0', '0', '1', '0', '1', '1', '0', 'A', 'B', 'C', '1', '2', '3', '\u001D', '2', '1', '5', '2', '0', '1', '7', '7', '4', '9', '8', '0', '9' ,'3'];
        Parser.ParseEx(sampleGs1Data, _resolvedEntityDelegate);

    }
    private void ResolvedEntityDelegate(in ResolvedApplicationIdentifierRef entity)
    {
        //_results.Add(entity);
    }
}
#endif

#if !NET7_0_OR_GREATER
//[ShortRunJob]
//[EventPipeProfiler(EventPipeProfile.GcVerbose)] // samples object allocations
[MemoryDiagnoser]
public class ParserParseBenchmark {
    private const string SampleGs1Data = "01012345678901281720010110ABC123\u001D21520177498093"; // Example GS1-encoded string

    [GlobalSetup]
    public void Setup() {
    }

    [Benchmark]
    public void Parse_Gs1String() {
        Parser.Parse(SampleGs1Data, ResolvedEntityDelegate);

    }
    private void ResolvedEntityDelegate(Common.IResolvedEntity entity) {
        //_results.Add(entity);
    }
}
#endif