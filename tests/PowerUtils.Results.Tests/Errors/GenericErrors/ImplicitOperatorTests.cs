using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.GenericErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeFailureProperty";
            var code = "fakeFailureCode";
            var description = "fakeFailureDescription";


            // Act
            Result act = Error.Failure(property, code, description);


            // Assert
            act.Should().ContainsError<Error>(
                property,
                code,
                description);
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeFailureProperty";
            var code = "fakeFailureCode";
            var description = "fakeFailureDescription";


            // Act
            Result<FakeModel> act = Error.Failure(property, code, description);


            // Assert
            act.Should().ContainsError<Error>(
                property,
                code,
                description);
        }
    }
}
