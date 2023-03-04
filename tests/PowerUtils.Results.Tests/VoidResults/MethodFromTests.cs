using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class MethodFromTests
    {
        [Fact]
        public void CustomError_ResultFrom_Result()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From(error);


            // Assert
            act.Should().ContainsError<CustomError>(
                property,
                code,
                description);
        }
    }
}
