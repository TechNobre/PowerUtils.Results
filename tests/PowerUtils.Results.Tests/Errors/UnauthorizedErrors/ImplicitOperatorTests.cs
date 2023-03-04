using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnauthorizedErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result act = Error.Unauthorized(property, code, description);


            // Assert
            act.Should().ContainsError<UnauthorizedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result act = Error.Unauthorized(property, description);


            // Assert
            act.Should().ContainsError<UnauthorizedError>(
                property,
                ResultErrorCodes.UNAUTHORIZED,
                description);
        }

        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result<FakeModel> act = Error.Unauthorized(property, code, description);


            // Assert
            act.Should().ContainsError<UnauthorizedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result<FakeModel> act = Error.Unauthorized(property, description);


            // Assert
            act.Should().ContainsError<UnauthorizedError>(
                property,
                ResultErrorCodes.UNAUTHORIZED,
                description);
        }
    }
}
