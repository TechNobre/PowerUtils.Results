using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Equals_False()
        {
            // Arrange
            UnexpectedError? left = null;
            UnexpectedError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Equals_True()
        {
            // Arrange
            UnexpectedError? left = Error.Unexpected("fake", "fake", "fake");
            UnexpectedError? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Equals_True()
        {
            // Arrange
            UnexpectedError? left = null;
            UnexpectedError? right = Error.Unexpected("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Equals_True()
        {
            // Arrange
            UnexpectedError? left = Error.Unexpected("fake1", "fake", "fake");
            UnexpectedError? right = Error.Unexpected("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Equals_False()
        {
            // Arrange
            UnexpectedError? left = Error.Unexpected("fake", "fake", "fake");
            UnexpectedError? right = Error.Unexpected("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
