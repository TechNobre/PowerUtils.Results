using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResultTests
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
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }
    }
}
