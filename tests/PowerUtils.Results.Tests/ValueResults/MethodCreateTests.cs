using System;
using System.Collections.Generic;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class MethodCreateTests
    {
        [Fact]
        public void EmptyErrorList_Create_ValueResult()
        {
            // Arrange
            var errors = Array.Empty<Error>();


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void NullErrorList_Create_ValueResult()
        {
            // Arrange
            Error[] errors = null;


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void IErrorListwithOneError_Create_ErrorResult()
        {
            // Arrange
            var errors = new List<IError> { Error.Forbidden("fake", "fake", "fake") };


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(ForbiddenError));
        }

        [Fact]
        public void ErrorListwithOneError_Create_ErrorResult()
        {
            // Arrange
            var errors = new List<Error> { Error.Failure("fake", "fake", "fake") };


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(Error));
        }
    }
}
