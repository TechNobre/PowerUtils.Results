using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class MatchAsyncExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public async Task VoidResultWithoutErrors_MatchAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok();

            async Task<string> onSuccessAction()
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await result.MatchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }



        [Fact]
        public async Task VoidResultWithoutErrors_MatchAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result> Method() => Task.FromResult(Result.Ok());

            async Task<string> onSuccessAction()
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await Method().MatchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }



        [Fact]
        public async Task VoidResultWithErrors_MatchAsync_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result result = _error;

            Task<string> onSuccessAction() => throw new FakeException();
            async Task<string> onErrorsAction(IEnumerable<IError> _)
            {
                await Task.Delay(20);
                return expected;
            }


            // Act
            var act = await result.MatchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }


        [Fact]
        public async Task ValueResultWithoutErrors_MatchAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok(new FakeModel());

            async Task<string> onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await result.MatchAsync(
               onSuccessAction,
               onErrorsAction
           );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithoutErrors_MatchAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            async Task<string> onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await Method().MatchAsync(
               onSuccessAction,
               onErrorsAction
           );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithErrors_MatchAsync_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result<FakeModel> result = _error;

            Task<string> onSuccessAction(FakeModel _) => throw new FakeException();
            async Task<string> onErrorsAction(IEnumerable<IError> _)
            {
                await Task.Delay(20);
                return expected;
            }


            // Act
            var act = await result.MatchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }
    }
}
