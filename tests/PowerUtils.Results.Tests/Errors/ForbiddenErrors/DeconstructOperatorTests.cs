using FluentAssertions;
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
            var act = Error.Forbidden(property, code, description);


            // Assert
            act.Should().IsError<ForbiddenError>(
                property,
                code,
                description);
        }
    }
}
