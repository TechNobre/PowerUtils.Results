using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using PowerUtils.Results.Benchmarks.Fakes;

namespace PowerUtils.Results.Benchmarks.Tests
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class CreatingValueResultBenchmarks
    {
        private FakeModel _model;


        [GlobalSetup]
        public void Setup()
            => _model = new();


        [Benchmark(Baseline = true)]
        public void ImpimplicitCreationlicit()
        {
            Result<FakeModel> result = _model;
        }

        [Benchmark]
        public void Method_ResultTypeOk()
        {
            var result = Result<FakeModel>.Ok(_model);
        }

        [Benchmark]
        public void Method_ResultOkType()
        {
            var result = Result.Ok<FakeModel>(_model);
        }

        [Benchmark]
        public void Method_ResultOk()
        {
            var result = Result.Ok(_model);
        }
    }
}
