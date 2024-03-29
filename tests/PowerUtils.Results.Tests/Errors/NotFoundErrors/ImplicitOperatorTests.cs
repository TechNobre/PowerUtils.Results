﻿using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.NotFoundErrors
{
    public class ImplicitOperatorTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            Result act = Error.NotFound(property, code, description);


            // Assert
            act.Should().ContainsError<NotFoundError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var description = "fakeNotFoundDescription";


            // Act
            Result act = Error.NotFound(property, description);


            // Assert
            act.Should().ContainsError<NotFoundError>(
                property,
                ResultErrorCodes.NOT_FOUND,
                description);
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            Result<FakeModel> act = Error.NotFound(property, code, description);


            // Assert
            act.Should().ContainsError<NotFoundError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var description = "fakeNotFoundDescription";


            // Act
            Result<FakeModel> act = Error.NotFound(property, description);


            // Assert
            act.Should().ContainsError<NotFoundError>(
                property,
                ResultErrorCodes.NOT_FOUND,
                description);
        }
    }
}
