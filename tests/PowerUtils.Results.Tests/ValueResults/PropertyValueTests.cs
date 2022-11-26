using System;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class PropertyValueTests
    {
        [Fact]
        public void WithErrors_GetValue_InvalidOperationException()
        {
            // Arrange
            Result<FakeModel> result = Error.Failure("fake", "fake", "fake");


            // Act
            var act = Record.Exception(() => result.Value);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Value can be retrieved only when the result is not an error");
        }
    }
}
