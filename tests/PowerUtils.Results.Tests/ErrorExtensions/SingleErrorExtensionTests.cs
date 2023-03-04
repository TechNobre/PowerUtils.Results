using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensions
{
    public class SingleErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public SingleErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_SingleError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(_result.SingleError);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneError_SingleError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };



            // Act
            var act = result.SingleError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_SingleError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(result.SingleError);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<InvalidOperationException>();
                act.Message.Should().Be("Errors can be retrieved only when the result is an error");
            }
        }
    }
}
