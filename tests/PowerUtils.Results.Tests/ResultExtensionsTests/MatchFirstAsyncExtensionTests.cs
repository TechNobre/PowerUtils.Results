using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class MatchFirstAsyncExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public async Task VoidResultWithoutErrors_MatchFirstAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok();

            async Task<string> onSuccessAction()
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await result.MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task VoidResultWithoutErrors_MatchFirstAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result> Method() => Task.FromResult(Result.Ok());

            async Task<string> onSuccessAction()
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await Method().MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task VoidResultWithErrors_MatchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result result = _error;

            Task<string> onSuccessAction() => throw new FakeException();
            async Task<string> onErrorAction(IError _)
            {
                await Task.Delay(20);
                return expected;
            }


            // Act
            var act = await result.MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }



        [Fact]
        public async Task ValueResultWithoutErrors_MatchFirstAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok(new FakeModel());

            async Task<string> onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                return expected;
            }
            Task<string> onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await result.MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithoutErrors_MatchFirstAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel(expected)));

            async Task<string> onSuccessAction(FakeModel model)
            {
                await Task.Delay(20);
                return model.Name;
            }
            Task<string> onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await Method().MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithErrors_MatchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result<FakeModel> result = _error;

            Task<string> onSuccessAction(FakeModel _) => throw new FakeException();
            async Task<string> onErrorAction(IError _)
            {
                await Task.Delay(20);
                return expected;
            }


            // Act
            var act = await result.MatchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }
    }
}
