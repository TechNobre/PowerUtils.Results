using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class MethodGetTypeTests
    {
        [Fact]
        public void WithoutError_GetType_Success()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(Success));
        }

        [Fact]
        public void WithConflictError_GetType_ConflictError()
        {
            // Arrange
            Result result = Error.Conflict("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(ConflictError));
        }

        [Fact]
        public void WithNotFoundError_GetType_NotFoundError()
        {
            // Arrange
            Result result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }
    }
}
