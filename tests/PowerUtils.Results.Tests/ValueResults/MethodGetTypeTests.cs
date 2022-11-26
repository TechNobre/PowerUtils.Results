using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class MethodGetTypeTests
    {
        [Fact]
        public void WithoutError_GetType_TypeOfModel()
        {
            // Arrange
            var model = new FakeModel();
            var result = Result<FakeModel>.Ok(model);


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void WithConflictError_GetType_ConflictError()
        {
            // Arrange
            Result<FakeModel> result = Error.Conflict("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(ConflictError));
        }

        [Fact]
        public void WithNotFoundError_GetType_NotFoundError()
        {
            // Arrange
            Result<FakeModel> result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }
    }
}
