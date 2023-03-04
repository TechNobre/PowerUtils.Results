using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var code = "fakeValidationCode";
            var description = "fakeUnexpectedDescription";


            // Act
            var act = new UnexpectedError(property, code, description);


            // Assert
            act.Should().IsError<UnexpectedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var code = "fakeUnexpectedCode";


            // Act
            var act = new UnexpectedError(property, code);


            // Assert
            act.Should().IsError<UnexpectedError>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
