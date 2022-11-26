using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnauthorizedErrors
{
    public class EqualsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Equals_True()
        {
            // Arrange
            UnauthorizedError? left = null;
            UnauthorizedError? right = null;


            // Act
            var act = left == right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNotNullRightNull_Equals_False()
        {
            // Arrange
            var left = Error.Unauthorized("fake", "fake", "fake");
            UnauthorizedError? right = null;


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNullRightNotNull_Equals_False()
        {
            // Arrange
            UnauthorizedError? left = null;
            UnauthorizedError? right = Error.Unauthorized("fake", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void BothDifferents_Equals_False()
        {
            // Arrange
            UnauthorizedError? left = Error.Unauthorized("fake1", "fake", "fake");
            UnauthorizedError? right = Error.Unauthorized("fake2", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void BothEquals_Equals_True()
        {
            // Arrange
            UnauthorizedError? left = Error.Unauthorized("fake", "fake", "fake");
            UnauthorizedError? right = Error.Unauthorized("fake", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeTrue();
        }
    }
}
