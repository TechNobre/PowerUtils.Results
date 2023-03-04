using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ForbiddenErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Differents_False()
        {
            // Arrange
            ForbiddenError? left = null;
            ForbiddenError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Differents_True()
        {
            // Arrange
            ForbiddenError? left = Error.Forbidden("fake", "fake", "fake");
            ForbiddenError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Differents_True()
        {
            // Arrange
            ForbiddenError? left = null;
            ForbiddenError? right = Error.Forbidden("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Differents_True()
        {
            // Arrange
            ForbiddenError? left = Error.Forbidden("fake1", "fake", "fake");
            ForbiddenError? right = Error.Forbidden("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Differents_False()
        {
            // Arrange
            ForbiddenError? left = Error.Forbidden("fake", "fake", "fake");
            ForbiddenError? right = Error.Forbidden("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
