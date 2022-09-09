using System;
using System.Collections.Generic;
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
    }
}
