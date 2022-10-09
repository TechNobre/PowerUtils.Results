using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace PowerUtils.Results.Benchmarks.Tests
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CreatingVoidSuccessResultBenchmarks
    {
        [Benchmark(Baseline = true)]
        public void Contructor()
        {
            Result result = new();
        }

        [Benchmark]
        public void SuccessMethod()
        {
            var result = Result.Success();
        }

        [Benchmark]
        public void OkMethod()
        {
            var result = Result.Ok();
        }

        [Benchmark]
        public void ImplicitSuccess()
        {
            Result result = new Success();
        }
    }
}
