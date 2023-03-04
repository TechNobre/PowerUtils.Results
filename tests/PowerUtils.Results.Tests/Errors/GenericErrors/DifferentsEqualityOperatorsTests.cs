using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.GenericErrors
{
    public class DifferentsEqualityOperatorsTests
    {
        [Fact]
        public void LeftNullRightNull_Differents_False()
        {
            // Arrange
            Error? left = null;
            Error? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void LeftNotNullRightNull_Differents_True()
        {
            // Arrange
            Error? left = Error.Failure("fake", "fake", "fake");
            Error? right = null;


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void LeftNullRightNotNull_Differents_True()
        {
            // Arrange
            Error? left = null;
            Error? right = Error.Failure("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothDifferents_Differents_True()
        {
            // Arrange
            Error? left = Error.Failure("fake1", "fake", "fake");
            Error? right = Error.Failure("fake2", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void BothEquals_Differents_False()
        {
            // Arrange
            Error? left = Error.Failure("fake", "fake", "fake");
            Error? right = Error.Failure("fake", "fake", "fake");


            // Act
            var act = left != right;


            // Assert
            act.Should().BeFalse();
        }
    }
}
