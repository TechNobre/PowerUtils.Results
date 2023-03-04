using FluentAssertions;
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
            var act = Error.Conflict(property, code, description);


            // Assert
            act.Should().IsError<ConflictError>(
                property,
                code,
                description);
        }
    }
}
