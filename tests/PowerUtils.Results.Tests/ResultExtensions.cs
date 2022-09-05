using System;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ResultExtensions
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public ResultExtensions()
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
        public void TwoErrors_LastError_First()
        {
            // Arrange && Act
            var act = _result.LastError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void TwoErrors_SingleError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(() => _result.SingleError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneErrorSingleError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };



            // Act
            var act = result.SingleError();


            // Assert
            act.Should().Be(_firstError);
        }
    }
}
