using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class MatchFirstExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public void VoidResultWithoutErrors_MatchFirst_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok();

            string onSuccessAction() => expected;
            string onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = result.MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task VoidResultWithoutErrors_MatchFirstExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result> Method() => Task.FromResult(Result.Ok());

            string onSuccessAction() => expected;
            string onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await Method().MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void VoidResultWithErrors_MatchFirst_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result result = _error;

            string onSuccessAction() => throw new FakeException();
            string onErrorAction(IError _) => expected;


            // Act
            var act = result.MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }



        [Fact]
        public void ValueResultWithoutErrors_MatchFirst_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok(new FakeModel());

            string onSuccessAction(FakeModel _) => expected;
            string onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = result.MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithoutErrors_MatchFirstExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            string onSuccessAction(FakeModel _) => expected;
            string onErrorAction(IError _) => throw new FakeException();


            // Act
            var act = await Method().MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void ValueResultWithErrors_MatchFirst_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result<FakeModel> result = _error;

            string onSuccessAction(FakeModel _) => throw new FakeException();
            string onErrorAction(IError _) => expected;


            // Act
            var act = result.MatchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().Be(expected);
        }
    }
}
