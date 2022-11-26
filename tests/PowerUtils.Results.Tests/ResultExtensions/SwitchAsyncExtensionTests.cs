using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class SwitchAsyncExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public async Task VoidResultWithoutErrors_SwitchAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok();

            async Task onSuccessAction()
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await result.SwitchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task VoidResultWithoutErrors_SwitchAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result> Method() => Task.FromResult(Result.Ok());

            async Task onSuccessAction()
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await Method().SwitchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public async Task VoidResultWithErrors_SwitchAsync_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var act = false;

            Result result = _error;

            Task onSuccessAction() => throw new FakeException();
            async Task onErrorsAction(IEnumerable<IError> _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public async Task ValueResultWithoutErrors_SwitchAsync_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok(new FakeModel());

            async Task onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await result.SwitchAsync(
               onSuccessAction,
               onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithoutErrors_SwitchAsyncExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            async Task onSuccessAction(FakeModel _)
            {
                await Task.Delay(20);
                act = true;
            }
            Task onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await Method().SwitchAsync(
               onSuccessAction,
               onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }




        [Fact]
        public async Task ValueResultWithErrors_SwitchAsync_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var act = false;

            Result<FakeModel> result = _error;

            Task onSuccessAction(FakeModel _) => throw new FakeException();
            async Task onErrorsAction(IEnumerable<IError> _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchAsync(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }
    }
}
