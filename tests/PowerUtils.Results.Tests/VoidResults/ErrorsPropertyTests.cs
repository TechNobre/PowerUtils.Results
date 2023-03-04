using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class ErrorsPropertyTests
    {
        [Fact]
        public void WithoutErrors_ErrorsProperty_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


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
