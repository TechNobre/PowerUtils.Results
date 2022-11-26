using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ValidationErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Equals_False()
        {
            // Arrange
            ValidationError? left = null;
            ValidationError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Equals_True()
        {
            // Arrange
            ValidationError? left = Error.Validation("fake", "fake", "fake");
            ValidationError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Equals_True()
        {
            // Arrange
            ValidationError? left = null;
            ValidationError? right = Error.Validation("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Equals_True()
        {
            // Arrange
            ValidationError? left = Error.Validation("fake1", "fake", "fake");
            ValidationError? right = Error.Validation("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Equals_False()
        {
            // Arrange
            ValidationError? left = Error.Validation("fake", "fake", "fake");
            ValidationError? right = Error.Validation("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
