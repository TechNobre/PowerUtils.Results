﻿using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class OfTypeFirstErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public OfTypeFirstErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_OfTypeFirstError_Error()
        {
            // Arrange && Act
            var act = _result.OfTypeFirstError();


            // Assert
            act.Should().Be(typeof(Error));
        }

        [Fact]
        public void NotFound_OfTypeFirstError_NotFoundError()
        {
            // Arrange
            Result result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.OfTypeFirstError();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }

        [Fact]
        public void WithoutErrors_OfTypeFirstError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(result.OfTypeFirstError);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<InvalidOperationException>();
                act.Message.Should().Be("Errors can be retrieved only when the result is an error");
            }
        }
    }
}
