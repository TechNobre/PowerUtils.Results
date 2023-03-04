using System;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class PropertyErrorsTests
    {
        [Fact]
        public void WithoutErrors_GetErrors_InvalidOperationException()
        {
            // Arrange
            var result = Result<FakeModel>.Ok(new());


            // Act
            var act = Record.Exception(() => result.Errors);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<InvalidOperationException>();
                act.Message.Should().Be("Errors can be retrieved only when the result is an error");
            }
        }
    }
}
