using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.NotFoundErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Differents_False()
        {
            // Arrange
            NotFoundError? left = null;
            NotFoundError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Differents_True()
        {
            // Arrange
            NotFoundError? left = Error.NotFound("fake", "fake", "fake");
            NotFoundError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Differents_True()
        {
            // Arrange
            NotFoundError? left = null;
            NotFoundError? right = Error.NotFound("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Differents_True()
        {
            // Arrange
            NotFoundError? left = Error.NotFound("fake1", "fake", "fake");
            NotFoundError? right = Error.NotFound("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Differents_False()
        {
            // Arrange
            NotFoundError? left = Error.NotFound("fake", "fake", "fake");
            NotFoundError? right = Error.NotFound("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
