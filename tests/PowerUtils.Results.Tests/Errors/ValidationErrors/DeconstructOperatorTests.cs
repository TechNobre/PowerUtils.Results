using FluentAssertions;
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
            var act = Error.Validation(property, code, description);


            // Assert
            act.Should().IsError<ValidationError>(
                property,
                code,
                description);
        }
    }
}
