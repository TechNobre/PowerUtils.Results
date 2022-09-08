using BenchmarkDotNet.Running;
using PowerUtils.Results.Benchmarks.Tests;

BenchmarkRunner.Run<CreatingValueResultBenchmarks>();

//BenchmarkRunner.Run<CreatingErrorVoidResultBenchmarks>();
//BenchmarkRunner.Run<CreatingErrorValueResultBenchmarks>();

//BenchmarkRunner.Run<FactoryResultFromErrorOrValueBenchmarks>();
