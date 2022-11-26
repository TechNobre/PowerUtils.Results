using FluentAssertions;
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
            value.Should().BeNull();
            errors.Should()
                .ContainSingle(s =>
                    s.Property == property
                    &&
                    s.Code == code
                    &&
                    s.Description == description
                );
        }

        [Fact]
        public void ResultWithoutErrors_Deconstruct_WithValueAndEmptyErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            (var value, var errors) = result;


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();
        }
    }
}
