using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace PowerUtils.Results.Benchmarks.Tests
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CreatingErrorVoidResultBenchmarks
    {
        private Error _error;

        [GlobalSetup]
        public void Setup()
            => _error = Error.Failure("fake", "fake", "fake");



        [Benchmark(Baseline = true)]
        public void ImplicitCreation()
        {
            Result result = _error;
        }

        [Benchmark]
        public void Factory_ResultFrom()
        {
            var result = Result.From(_error);
        }
    }
}
