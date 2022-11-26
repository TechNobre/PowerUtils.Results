using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensionsTests
{
    public class ContainsErrorExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



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
            Result result = _error;


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
            Result result = _error;


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
            Result result = _error;
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
    }
}
