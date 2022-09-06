using System;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ResultExtensionsTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public ResultExtensionsTests()
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
            var act = Record.Exception(() => result.OfTypeFirstError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }



        [Fact]
        public void VoidResultWithoutErrors_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void VoidResultWithErrors_IsSuccess_False()
        {
            // Arrange
            Result result = _firstError;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }



        [Fact]
        public void WrapperResultWithoutErrors_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok(new FakeModel());


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void WrapperResultWithErrors_IsSuccess_False()
        {
            // Arrange
            Result<FakeModel> result = _firstError;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void WrapperResultWithSpecificCondition_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok(new FakeModel { Id = 5 });
            static bool fakePredicate(FakeModel x) => x.Id == 5;

            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void WrapperResultWithSpecificCondition_IsSuccess_False()
        {
            // Arrange
            var result = Result.Ok(new FakeModel { Id = 5 });
            static bool fakePredicate(FakeModel x) => x.Id == 11;


            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void WrapperWithErrors_IsSuccessWithSpecificCondition_False()
        {
            // Arrange
            Result<FakeModel> result = _firstError;
            static bool fakePredicate(FakeModel x) => x.Id == 11;


            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }
    }
}
