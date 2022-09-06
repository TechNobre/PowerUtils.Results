using System;
using FluentAssertions;
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
        public void TwoErrors_FirstError_First()
        {
            // Arrange && Act
            var act = _result.FirstError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_FirstError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.FirstError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }



        [Fact]
        public void TwoErrors_FirstOrDefaultError_First()
        {
            // Arrange && Act
            var act = _result.FirstOrDefaultError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_FirstOrDefaultError_Null()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.FirstOrDefaultError();


            // Assert
            act.Should().BeNull();
        }



        [Fact]
        public void TwoErrors_LastError_Last()
        {
            // Arrange && Act
            var act = _result.LastError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void WithoutErrors_LastError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.LastError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }



        [Fact]
        public void TwoErrors_LastOrDefaultError_Last()
        {
            // Arrange && Act
            var act = _result.LastOrDefaultError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void WithoutErrors_LastOrDefaultError_Null()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.LastOrDefaultError();


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void WithoutErrors_FirstOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            var result = Result.Ok();
            Func<IError, bool> fakePredicate = x => x.Code == ErrorCodes.VALIDATION;

            //Act
            var act = result.FirstOrDefaultError(fakePredicate);

            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void OneError_FirstOrDefaultErrorWithPredicate_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };
            Func<IError, bool> fakePredicate = x => x.Code == _firstError.Code;


            //Act
            var act = result.FirstOrDefaultError(fakePredicate);


            // Assert
            act.Should()
               .Be(_firstError);
        }

        [Fact]
        public void MoreThanOneError_FirstOrDefaultErrorWithPredicate_Error()
        {
            // Arrange
            Func<IError, bool> fakePredicate = x => x.Code == _firstError.Code;


            //Act
            var act = _result.FirstOrDefaultError(fakePredicate);


            // Assert
            act.Should()
               .Be(_firstError);
        }

        [Fact]
        public void MoreThanOneError_FirstOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            Func<IError, bool> fakePredicate = x => x.Code == "fakeErrorCodeToFail";


            //Act
            var act = _result.FirstOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void TwoErrors_SingleError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(() => _result.SingleError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneError_SingleError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };



            // Act
            var act = result.SingleError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_SingleError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.SingleError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }



        [Fact]
        public void TwoErrors_SingleOrDefaultError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(() => _result.SingleOrDefaultError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneError_SingleOrDefaultError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };


            // Act
            var act = result.SingleOrDefaultError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_SingleOrDefaultError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();



            // Act
            var act = result.SingleOrDefaultError();


            // Assert
            act.Should().BeNull();
        }

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
    }
}
