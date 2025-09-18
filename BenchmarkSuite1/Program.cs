using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using Solidsoft.Reply.Parsers.Gs1Ai.Benchmarks;

namespace BenchmarkSuite1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Use BenchmarkSwitcher to discover all benchmarks in the assembly with default config
            //var bms = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, BenchmarkDotNet.Configs.DefaultConfig.Instance);
            //foreach (var item in bms) {
                var config = DefaultConfig.Instance
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);
                BenchmarkRunner.Run<ParserParseBenchmark>(config);

            //}
        }
    }
}