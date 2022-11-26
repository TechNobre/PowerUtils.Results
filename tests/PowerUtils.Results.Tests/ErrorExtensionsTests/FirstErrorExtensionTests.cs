using System;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensionsTests
{
    public class FirstErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public FirstErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_FirstError_First()
        {
            // Arrange && Act
            var act = _result.FirstError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_FirstError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(result.FirstError);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }
    }
}
