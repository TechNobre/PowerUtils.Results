using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Types
{
    public class SuccessTests
    {
        [Fact]
        public void TwoDifferentsSuccess_MethodEquals_True()
        {
            // Arrange
            var left = Success.Create();
            var right = new Success();


            // Act
            var act = left.Equals(right);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void TwoDifferentsSuccess_GetHashCode_True()
        {
            // Arrange
            var left = new Success();
            var right = Success.Create();


            // Act
            var act = left.GetHashCode() == right.GetHashCode();


            // Assert
            act.Should().BeTrue();
        }
    }
}
