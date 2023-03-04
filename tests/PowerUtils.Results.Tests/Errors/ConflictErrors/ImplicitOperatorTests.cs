using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            Result act = Error.Conflict(property, code, description);


            // Assert
            act.Should().ContainsError<ConflictError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var description = "fakeConflictDescription";


            // Act
            Result act = Error.Conflict(property, description);


            // Assert
            act.Should().ContainsError<ConflictError>(
                property,
                ResultErrorCodes.CONFLICT,
                description);
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            Result<FakeModel> act = Error.Conflict(property, code, description);


            // Assert
            act.Should().ContainsError<ConflictError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var description = "fakeConflictDescription";


            // Act
            Result<FakeModel> act = Error.Conflict(property, description);


            // Assert
            act.Should().ContainsError<ConflictError>(
                property,
                ResultErrorCodes.CONFLICT,
                description);
        }
    }
}
