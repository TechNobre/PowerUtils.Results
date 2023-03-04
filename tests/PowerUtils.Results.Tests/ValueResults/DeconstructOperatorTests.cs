using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class DeconstructOperatorTests
    {
        [Fact]
        public void ResultWithErrors_Deconstruct_ValueNullAndErrors()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            Result<FakeModel> result = Error.Conflict(property, code, description);


            // Act
            (var value, var errors) = result;


            // Assert
            using(new AssertionScope())
            {
                value.Should().BeNull();
                errors.Should().ContainsError<ConflictError>(
                    property,
                    code,
                    description);
            }
        }

        [Fact]
        public void ResultWithoutErrors_Deconstruct_WithValueAndEmptyErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            (var value, var errors) = result;


            // Assert
            using(new AssertionScope())
            {
                value.Should().NotBeNull();
                errors.Should().BeEmpty();
            }
        }
    }
}
