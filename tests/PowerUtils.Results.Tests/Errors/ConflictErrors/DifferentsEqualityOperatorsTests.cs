using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Differents_False()
        {
            // Arrange
            ConflictError? left = null;
            ConflictError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Differents_True()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake", "fake", "fake");
            ConflictError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Differents_True()
        {
            // Arrange
            ConflictError? left = null;
            ConflictError? right = Error.Conflict("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Differents_True()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake1", "fake", "fake");
            ConflictError? right = Error.Conflict("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Differents_False()
        {
            // Arrange
            ConflictError? left = Error.Conflict("fake", "fake", "fake");
            ConflictError? right = Error.Conflict("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
