using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class EqualsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Equals_True()
        {
            // Arrange
            ConflictError? left = null;
            ConflictError? right = null;


            // Act
            var act = left == right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNotNullRightNull_Equals_False()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake", "fake", "fake");
            ConflictError? right = null;


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNullRightNotNull_Equals_False()
        {
            // Arrange
            ConflictError? left = null;
            ConflictError? right = Error.Conflict("fake", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void BothDifferents_Equals_False()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake1", "fake", "fake");
            ConflictError? right = Error.Conflict("fake2", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void BothEquals_Equals_True()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake", "fake", "fake");
            ConflictError? right = Error.Conflict("fake", "fake", "fake");


            // Act
            var act = left == right;


            // Assert
            act.Should().BeTrue();
        }
    }
}
