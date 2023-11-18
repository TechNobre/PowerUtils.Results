using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using PowerUtils.Results.Benchmarks.Fakes;

namespace PowerUtils.Results.Benchmarks.Tests
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class FactoryResultFromErrorOrValueBenchmarks
    {
        [Benchmark]
        public void WithErrors()
        {
            var result = Result.Create(
                new Error[] { Error.Failure("fake", "fake", "fake") },
                () => new FakeModel()
            );
        }

        [Benchmark]
        public void WithoutErrors()
        {
            var result = Result.Create(
                Array.Empty<Error>(),
                () => new FakeModel()
            );
        }
    }
}
