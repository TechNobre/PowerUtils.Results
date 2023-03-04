using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ForbiddenErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Forbidden(property, code, description);


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
