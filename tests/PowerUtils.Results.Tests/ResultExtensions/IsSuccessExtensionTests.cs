using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class IsSuccessExtensionTests
    {
        private readonly Error _error = new("FirstProperty", "FirstCode", "FirstDescription");



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
            Result result = _error;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }



        [Fact]
        public void ValueResultWithoutErrors_IsSuccess_True()
        {
            // Arrange
            var result = Result.Ok(new FakeModel());


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeTrue();
        }

        [Fact]
        public void ValueResultWithErrors_IsSuccess_False()
        {
            // Arrange
            Result<FakeModel> result = _error;


            // Act
            var act = result.IsSuccess();


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void ValueResultWithSpecificCondition_IsSuccess_True()
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
        public void ValueResultWithSpecificCondition_IsSuccess_False()
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
        public void ValueResultWithErrors_IsSuccessWithSpecificCondition_False()
        {
            // Arrange
            Result<FakeModel> result = _error;
            static bool fakePredicate(FakeModel x) => x.Id == 11;


            // Act
            var act = result.IsSuccess(fakePredicate);


            // Assert
            act.Should().BeFalse();
        }
    }
}
