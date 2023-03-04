using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ValidationErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";
            var description = "fakeValidationDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Validation(property, code, description);


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
