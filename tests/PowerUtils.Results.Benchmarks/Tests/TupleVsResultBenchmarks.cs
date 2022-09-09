using System.Collections.Generic;
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
    public class TupleVsResultBenchmarks
    {
        [Benchmark]
        public void HandleWithModelResult()
        {
            var result = _handleWithModelResult();
        }

        [Benchmark]
        public void HandleWithModelTuple()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            (var model, var errors) = _handleWithModelTuple();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        [Benchmark]
        public void HandleWithOneErrorResult()
        {
            var result = _handleWithOneErrorResult();
        }

        [Benchmark]
        public void HandleWithOneErrorTuple()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            (var model, var errors) = _handleWithOneErrorTuple();
#pragma warning restore IDE0022 // Use expression body for methods
        }

        [Benchmark]
        public void HandleWithTwoErrorsResult()
        {
            var result = _handleWithTwoErrorsResult();
        }

        [Benchmark]
        public void HandleWithTwoErrorsTuple()
        {
#pragma warning disable IDE0022 // Use expression body for methods
            (var model, var errors) = _handleWithTwoErrorsTuple();
#pragma warning restore IDE0022 // Use expression body for methods
        }



        private Result<FakeModel> _handleWithModelResult() => new FakeModel();
        private (FakeModel model, List<Error> errors) _handleWithModelTuple() => (new FakeModel(), null);

        private Result<FakeModel> _handleWithOneErrorResult() => Error.Failure("fake", "fake", "fake");
        private (FakeModel model, List<IError> errors) _handleWithOneErrorTuple() => (null, new List<IError> { Error.Failure("fake", "fake", "fake") });

        private Result<FakeModel> _handleWithTwoErrorsResult()
            => new List<IError>
            {
                Error.Failure("fake1", "fake1", "fake1"),
                Error.Failure("fake2", "fake2", "fake2")
            };

        private (FakeModel model, List<IError> errors) _handleWithTwoErrorsTuple()
            =>
            (
                null,
                new List<IError>
                {
                    Error.Failure("fake1", "fake1", "fake1"),
                    Error.Failure("fake2", "fake2", "fake2")
                }
            );
    }
}
