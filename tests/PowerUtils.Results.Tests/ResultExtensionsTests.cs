using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ResultExtensionsTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public ResultExtensionsTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_OfTypeFirstError_Error()
        {
            // Arrange && Act
            var act = _result.OfTypeFirstError();


            // Assert
            act.Should().Be(typeof(Error));
        }

        [Fact]
        public void NotFound_OfTypeFirstError_NotFoundError()
        {
            // Arrange
            Result result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.OfTypeFirstError();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }

        [Fact]
        public void WithoutErrors_OfTypeFirstError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.OfTypeFirstError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }



        [Fact]
        public void VoidResultWithoutErrors_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void VoidResultWithErrors_IsSuccess_False()
        {
            // Arrange
            Result result = _firstError;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }



        [Fact]
        public void ValueResultWithoutErrors_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok(new FakeModel());


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ValueResultWithErrors_IsSuccess_False()
        {
            // Arrange
            Result<FakeModel> result = _firstError;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ValueResultWithSpecificCondition_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok(new FakeModel { Id = 5 });
            static bool fakePredicate(FakeModel x) => x.Id == 5;

            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ValueResultWithSpecificCondition_IsSuccess_False()
        {
            // Arrange
            var result = Result.Ok(new FakeModel { Id = 5 });
            static bool fakePredicate(FakeModel x) => x.Id == 11;


            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ValueResultWithErrors_IsSuccessWithSpecificCondition_False()
        {
            // Arrange
            Result<FakeModel> result = _firstError;
            static bool fakePredicate(FakeModel x) => x.Id == 11;


            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }



        [Fact]
        public void ResultWithoutErrors_ContainsError_False()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.ContainsError();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithErrors_ContainsError_True()
        {
            // Arrange
            Result result = _firstError;


            // Act
            var act = result.ContainsError();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ResultWithoutErrors_ContainsSpecificErrorType_False()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.ContainsError<NotFoundError>();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithGenericError_ContainsSpecificErrorType_False()
        {
            // Arrange
            Result result = _firstError;


            // Act
            var act = result.ContainsError<NotFoundError>();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithForbiddenError_ContainsSpecificErrorType_False()
        {
            // Arrange
            Result result = Error.Forbidden("fake", "fake", "fake");


            // Act
            var act = result.ContainsError<ForbiddenError>();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ResultWithMultiErrors_ContainsSpecificErrorType_False()
        {
            // Arrange
            Result result = new List<IError>
            {
                Error.Forbidden("fake", "fake", "fake"),
                Error.NotFound("fake", "fake", "fake")
            };


            // Act
            var act = result.ContainsError<NotFoundError>();


            // Assert
            act.Should().BeTrue();
        }


        [Fact]
        public void ResultWithoutErrors_ContainsErrorWithCondition_False()
        {
            // Arrange
            var result = Result.Ok();
            static bool fakePredicate(IError x) => x.Code == "code";


            // Act
            var act = result.ContainsError<NotFoundError>(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithGenericError_ContainsErrorWithConditionFromDifferentType_False()
        {
            // Arrange
            Result result = _firstError;
            static bool fakePredicate(IError x) => x.Code == "code";


            // Act
            var act = result.ContainsError<NotFoundError>(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithForbiddenError_ContainsErrorWithDifferentConditionFromSameType_False()
        {
            // Arrange
            Result result = Error.Forbidden("fake", "fake", "fake");
            static bool fakePredicate(IError x) => x.Code == "code";


            // Act
            var act = result.ContainsError<ForbiddenError>(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithForbiddenError_ContainsErrorWithSameConditionFromSameType_True()
        {
            // Arrange
            Result result = Error.Conflict("fake", "code", "fake");
            static bool fakePredicate(IError x) => x.Code == "code";


            // Act
            var act = result.ContainsError<ConflictError>(fakePredicate);


            // Assert
            act.Should().BeTrue();
        }



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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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
                onErrorAction
            );


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
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }



        [Fact]
        public async Task VoidResultWithErrors_SwitchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result result = _firstError;

            Task onSuccessAction() => throw new FakeException();
            async Task onErrorAction(IError _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


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
                onErrorAction
            );


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
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public async Task ValueResultWithErrors_SwitchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var act = false;

            Result<FakeModel> result = _firstError;

            Task onSuccessAction(FakeModel _) => throw new FakeException();
            async Task onErrorAction(IError _)
            {
                await Task.Delay(20);
                act = true;
            }


            // Act
            await result.SwitchFirstAsync(
                onSuccessAction,
                onErrorAction
            );


            // Assert
            act.Should().BeTrue();
        }



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

            Result result = _firstError;

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

            Result<FakeModel> result = _firstError;

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

            Result result = _firstError;

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

            Task<Result<FakeModel>> Method() => Task.FromResult(Result.Ok(new FakeModel()));

            async Task<string> onSuccessAction(FakeModel _)
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
        public async Task ValueResultWithErrors_MatchFirstAsync_ShouldExecuteOnErrorAction()
        {
            // Arrange
            var expected = "Wow no, wow no...!!!";

            Result<FakeModel> result = _firstError;

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
