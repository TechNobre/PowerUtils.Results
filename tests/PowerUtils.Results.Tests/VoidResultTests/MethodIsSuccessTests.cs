using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResultTests
{
    public class MethodIsSuccessTests
    {
        [Fact]
        public void ResultWithErrors_IsSuccess_IsSuccessFalseAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result result = Error.Forbidden(property);


            // Act
            var act = result.IsSuccess(out var errors);


            // Assert
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithoutErrors_IsSuccess_IsSuccessTrueAndErrors()
        {
            // Arrange
            Result result = new Success();


            // Act
            var act = result.IsSuccess(out var errors);


            // Assert
            errors.Should().BeEmpty();

            act.Should().BeTrue();
        }
    }
}
