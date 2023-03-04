using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";
            var code = "fakeUnexpectedCode";


            // Act
            Result act = Error.Unexpected(property, code, description);


            // Assert
            act.Should().ContainsError<UnexpectedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";


            // Act
            Result act = Error.Unexpected(property, description);


            // Assert
            act.Should().ContainsError<UnexpectedError>(
                property,
                ResultErrorCodes.UNEXPECTED,
                description);
        }

        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";
            var code = "fakeUnexpectedCode";


            // Act
            Result<FakeModel> act = Error.Unexpected(property, code, description);


            // Assert
            act.Should().ContainsError<UnexpectedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";


            // Act
            Result<FakeModel> act = Error.Unexpected(property, description);


            // Assert
            act.Should().ContainsError<UnexpectedError>(
                property,
                ResultErrorCodes.UNEXPECTED,
                description);
        }
    }

}
