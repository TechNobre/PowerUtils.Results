using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.GenericErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";


            // Act
            var act = Error.Failure(property, code, description);


            // Assert
            act.Should().IsError<Error>(
                property,
                code,
                description);
        }
    }
}
