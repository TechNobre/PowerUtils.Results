using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.NotFoundErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            var act = Error.NotFound(property, code, description);


            // Assert
            act.Should().IsError<NotFoundError>(
                property,
                code,
                description);
        }
    }
}
