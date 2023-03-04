using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnauthorizedErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            var act = Error.Unauthorized(property, code, description);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                property,
                code,
                description);
        }
    }
}
