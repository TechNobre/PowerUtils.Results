using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.GenericErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";


            // Act
            var act = new Error(property, code, description);


            // Assert
            act.Should().IsError<Error>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";


            // Act
            var act = new Error(property, code);


            // Assert
            act.Should().IsError<Error>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
