using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var code = "fakeUnexpectedCode";
            var description = "fakeUnexpectedDescription";


            // Act
            var act = Error.Unexpected(property, code, description);


            // Assert
            act.Should().IsError<UnexpectedError>(
                property,
                code,
                description);
        }
    }
}
