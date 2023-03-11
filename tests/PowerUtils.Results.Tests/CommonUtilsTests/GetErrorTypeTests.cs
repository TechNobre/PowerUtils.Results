using System;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.CommonUtilsTests
{
    public class GetErrorTypeTests
    {
        [Fact]
        public void BuiltInError_GetErrorType_Type()
        {
            // Arrange
            var errorType = typeof(NotFoundError);
            var fullName = errorType.FullName;


            // Act
            var act = CommonUtils.GetErrorType(fullName);


            // Assert
            act.Should().Be(errorType);
        }

        [Fact]
        public void CustomError_GetErrorType_Type()
        {
            // Arrange
            var errorType = typeof(CustomError);
            var fullName = errorType.FullName;


            // Act
            var act = CommonUtils.GetErrorType(fullName);


            // Assert
            act.Should().Be(errorType);
        }

        [Fact]
        public void ExistentTypeNotIError_GetErrorType_TypeLoadException()
        {
            // Arrange
            var fullName = typeof(FakeCustomer).FullName;


            // Act
            var act = Record.Exception(() => CommonUtils.GetErrorType(fullName));


            // Assert
            act.Should().BeOfType<TypeLoadException>();
        }
    }
}
