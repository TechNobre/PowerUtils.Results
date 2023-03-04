using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensions
{
    public class LastErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public LastErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_LastError_Last()
        {
            // Arrange && Act
            var act = _result.LastError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void WithoutErrors_LastError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(result.LastError);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<InvalidOperationException>();
                act.Message.Should().Be("Errors can be retrieved only when the result is an error");
            }
        }
    }
}
