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
    public class CreatingErrorValueResultBenchmarks
    {
        private Error _error;

        [GlobalSetup]
        public void Setup()
            => _error = Error.Failure("fake", "fake", "fake");



        [Benchmark(Baseline = true)]
        public void ImplicitCreation()
        {
            Result<FakeModel> result = _error;
        }

        [Benchmark]
        public void Factory_ResultTypeFrom()
        {
            var result = Result<FakeModel>.From(_error);
        }

        [Benchmark]
        public void Factory_ResultFromType()
        {
            var result = Result.From<FakeModel>(_error);
        }
    }
}
