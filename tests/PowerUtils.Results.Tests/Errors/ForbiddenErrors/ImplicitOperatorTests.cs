using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ForbiddenErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            Result act = Error.Forbidden(property, code, description);


            // Assert
            act.Should().ContainsError<ForbiddenError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var description = "fakeForbiddenDescription";


            // Act
            Result act = Error.Forbidden(property, description);


            // Assert
            act.Should().ContainsError<ForbiddenError>(
                property,
                ResultErrorCodes.FORBIDDEN,
                description);
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            Result<FakeModel> act = Error.Forbidden(property, code, description);


            // Assert
            act.Should().ContainsError<ForbiddenError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var description = "fakeForbiddenDescription";


            // Act
            Result<FakeModel> act = Error.Forbidden(property, description);


            // Assert
            act.Should().ContainsError<ForbiddenError>(
                property,
                ResultErrorCodes.FORBIDDEN,
                description);
        }
    }
}
