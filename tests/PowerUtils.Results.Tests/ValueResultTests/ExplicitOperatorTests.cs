using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResultTests
{
    public class ExplicitOperatorTests
    {
        [Fact]
        public void ResultWithoutErrors_ImplicitAssignmentBool_ShouldBeFalse()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


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
            Result<FakeModel> result = Error.Forbidden("prop");


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeFalse();
        }
    }
}
