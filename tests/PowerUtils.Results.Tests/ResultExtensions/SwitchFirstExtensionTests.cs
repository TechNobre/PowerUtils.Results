using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class SwitchFirstExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public void VoidResultWithoutErrors_SwitchFirst_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok();

            void onSuccessAction() => act = true;
            void onErrorAction(IError _) => throw new FakeException();


            // Act
            result.SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task VoidResultWithoutErrors_SwitchFirstExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result> Method() => Task.FromResult(Result.Ok());

            void onSuccessAction() => act = true;
            void onErrorAction(IError _) => throw new FakeException();


            // Act
            await Method().SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public void VoidResultWithErrors_SwitchFirst_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result result = _error;

            void onSuccessAction() => throw new FakeException();
            void onErrorAction(IError _) => act = true;


            // Act
            result.SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public void ValueResultWithoutErrors_SwitchFirst_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok(new FakeModel());

            void onSuccessAction(FakeModel _) => act = true;
            void onErrorAction(IError _) => throw new FakeException();


            // Act
            result.SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithoutErrors_SwitchFirstExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            void onSuccessAction(FakeModel _) => act = true;
            void onErrorAction(IError _) => throw new FakeException();


            // Act
            await Method().SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public void ValueResultWithErrors_SwitchFirst_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result<FakeModel> result = _error;

            void onSuccessAction(FakeModel _) => throw new FakeException();
            void onErrorAction(IError _) => act = true;


            // Act
            result.SwitchFirst(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }
    }
}
