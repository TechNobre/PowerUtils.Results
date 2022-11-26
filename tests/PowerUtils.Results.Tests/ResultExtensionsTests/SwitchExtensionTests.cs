using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class SwitchExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



        [Fact]
        public void VoidResultWithoutErrors_Switch_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok();

            void onSuccessAction() => act = true;
            void onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            result.Switch(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task VoidResultWithoutErrors_SwitchExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result> Method() => Task.FromResult(Result.Ok());

            void onSuccessAction() => act = true;
            void onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await Method().Switch(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public void VoidResultWithErrors_Switch_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var act = false;

            Result result = _error;

            void onSuccessAction() => throw new FakeException();
            void onErrorsAction(IEnumerable<IError> _) => act = true;


            // Act
            result.Switch(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ValueResultWithoutErrors_Switch_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            var result = Result.Ok(new FakeModel());

            void onSuccessAction(FakeModel _) => act = true;
            void onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            result.Switch(
               onSuccessAction,
               onErrorsAction
           );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithoutErrors_SwitchExtensions_ShouldExecuteOnSuccessAction()
        {
            // Arrange
            var act = false;

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            void onSuccessAction(FakeModel _) => act = true;
            void onErrorsAction(IEnumerable<IError> _) => throw new FakeException();


            // Act
            await Method().Switch(
               onSuccessAction,
               onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public void ValueResultWithErrors_Switch_ShouldExecuteOnErrorsAction()
        {
            // Arrange
            var act = false;

            Result<FakeModel> result = _error;

            void onSuccessAction(FakeModel _) => throw new FakeException();
            void onErrorsAction(IEnumerable<IError> _) => act = true;


            // Act
            result.Switch(
                onSuccessAction,
                onErrorsAction
            );


            // Assert
            act.Should().BeTrue();
        }
    }
}
