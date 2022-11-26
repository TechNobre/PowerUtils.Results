using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class ExplicitOperatorTests
    {
        [Fact]
        public void ResultWithoutErrors_ImplicitAssignmentBool_ShouldBeFalse()
        {
            // Arrange
            Result result = new Success();


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeTrue();
        }

        [Fact]
        public void ResultWithErrors_ImplicitAssignmentBool_ShouldBeTrue()
        {
            // Arrange
            Result result = Error.Forbidden("prop");


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeFalse();
        }
    }
}
