using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnauthorizedErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            var act = new UnauthorizedError(property, code, description);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";


            // Act
            var act = new UnauthorizedError(property, code);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
