using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class MatchExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public void VoidResultWithoutErrors_Match_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok();

            string onSuccessAction() => expected;
            string onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = result.Match(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task VoidResultWithoutErrors_MatchExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result> Method() => Task.FromResult(Result.Ok());

            string onSuccessAction() => expected;
            string onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await Method().Match(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void VoidResultWithErrors_Match_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result result = _error;

            string onSuccessAction() => throw new FakeException();
            string onErrorsAction(IEnumerable<IError> _) => expected;


            // Act
            var act = result.Match(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }


        [Fact]
        public void ValueResultWithoutErrors_Match_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            var result = Result.Ok(new FakeModel());

            string onSuccessAction(FakeModel _) => expected;
            string onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = result.Match(
               onSuccessAction,
               onErrorsAction
           );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public async Task ValueResultWithoutErrors_MatchExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var expected = "I am here...!!!";

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            string onSuccessAction(FakeModel _) => expected;
            string onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            var act = await Method().Match(
               onSuccessAction,
               onErrorsAction
           );


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void ValueResultWithErrors_Match_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result<FakeModel> result = _error;

            string onSuccessAction(FakeModel _) => throw new FakeException();
            string onErrorsAction(IEnumerable<IError> _) => expected;


            // Act
            var act = result.Match(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().Be(expected);
        }
    }
}
