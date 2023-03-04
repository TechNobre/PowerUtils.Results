using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ValidationErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";
            var description = "fakeValidationDescription";


            // Act
            var act = new ValidationError(property, code, description);


            // Assert
            act.Should().IsError<ValidationError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";


            // Act
            var act = new ValidationError(property, code);


            // Assert
            act.Should().IsError<ValidationError>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
