using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ForbiddenErrors
{
    public class ConstructTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            var act = new ForbiddenError(property, code, description);


            // Assert
            act.Should().IsError<ForbiddenError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";


            // Act
            var act = new ForbiddenError(property, code);


            // Assert
            act.Should().IsError<ForbiddenError>(
                property,
                code,
                $"An error has occurred with code '{code}'");
        }
    }
}
