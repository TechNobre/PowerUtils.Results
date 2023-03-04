using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Conflict(property, code, description);


            // Assert
            using(new AssertionScope())
            {
                actProperty.Should().Be(property);
                actCode.Should().Be(code);
                actDescription.Should().Be(description);
            }
        }
    }
}
