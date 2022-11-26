using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class MethodIsSuccessTests
    {
        [Fact]
        public void ResultWithErrors_IsSuccess_IsSuccessFalseValueNullAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result<FakeModel> result = Error.Forbidden(property);


            // Act
            var act = result.IsSuccess(out var value, out var errors);


            // Assert
            value.Should().BeNull();
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithoutErrors_IsSuccess_IsSuccessTrueValueNullAndErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            var act = result.IsSuccess(out var value, out var errors);


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();

            act.Should().BeTrue();
        }
    }
}
