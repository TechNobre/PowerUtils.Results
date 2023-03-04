using FluentAssertions;
using FluentAssertions.Execution;
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
            var (actProperty, actCode, actDescription) = Error.Unexpected(property, code, description);


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
