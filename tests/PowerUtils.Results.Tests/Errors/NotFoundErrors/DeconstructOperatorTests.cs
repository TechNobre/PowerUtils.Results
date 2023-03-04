using FluentAssertions;
using FluentAssertions.Execution;
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
            var (actProperty, actCode, actDescription) = Error.NotFound(property, code, description);


            // Assert
            using(new AssertionScope())
            {
                actProperty.Should().Be(property);
                actCode.Should().Be(code);
                actDescription.Should().Be(description);
            }
        }
    }
}
