using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.CommonUtilsTests
{
    public class TryGetErrorTypeTests
    {
        [Fact]
        public void BuiltInError_TryGetErrorType_Type()
        {
            // Arrange
            var errorType = typeof(NotFoundError);
            var fullName = errorType.FullName;


            // Act
            var act = CommonUtils.TryGetErrorType(fullName);


            // Assert
            act.Should().Be(errorType);
        }

        [Fact]
        public void LowerName_TryGetErrorType_Type()
        {
            // Arrange
            var errorType = typeof(ForbiddenError);
            var fullName = errorType.FullName.ToLower();


            // Act
            var act = CommonUtils.TryGetErrorType(fullName);


            // Assert
            act.Should().Be(errorType);
        }

        [Fact]
        public void CustomError_TryGetErrorType_Type()
        {
            // Arrange
            var errorType = typeof(CustomError);
            var fullName = errorType.FullName;


            // Act
            var act = CommonUtils.TryGetErrorType(fullName);


            // Assert
            act.Should().Be(errorType);
        }

        [Fact]
        public void ExistentTypeNotIError_TryGetErrorType_Null()
        {
            // Arrange
            var fullName = typeof(FakeCustomer).FullName;


            // Act
            var act = Record.Exception(() => CommonUtils.TryGetErrorType(fullName));


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void NonExistentType_TryGetErrorType_Null()
        {
            // Arrange
            var fullName = "fakeType";


            // Act
            var act = Record.Exception(() => CommonUtils.TryGetErrorType(fullName));


            // Assert
            act.Should().BeNull();
        }
    }
}
