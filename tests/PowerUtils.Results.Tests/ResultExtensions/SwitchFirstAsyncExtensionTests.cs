using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class SwitchFirstAsyncExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public async Task VoidResultWithoutErrors_SwitchFirstAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok();

            async Task onSuccessAction()
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorAction(IError _) => throw new FakeException();


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task VoidResultWithoutErrors_SwitchFirstAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result> Method() => Task.FromResult(Result.Ok());

            async Task onSuccessAction()
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorAction(IError _) => throw new FakeException();


            // Act
            await Method().SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public async Task VoidResultWithErrors_SwitchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result result = _error;

            Task onSuccessAction() => throw new FakeException();
            async Task onErrorAction(IError _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }


        [Fact]
        public async Task ValueResultWithoutErrors_SwitchFirstAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok(new FakeModel());

            async Task onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorAction(IError _) => throw new FakeException();


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithoutErrors_SwitchFirstAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            async Task onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorAction(IError _) => throw new FakeException();


            // Act
            await Method().SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithErrors_SwitchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result<FakeModel> result = _error;

            Task onSuccessAction(FakeModel _) => throw new FakeException();
            async Task onErrorAction(IError _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction);


            // Assert
            act.Should().BeTrue();
        }
    }
}
