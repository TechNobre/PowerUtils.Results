using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorTests
{
    public class UnauthorizedErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            var act = new UnauthorizedError(property, code, description);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be(description);
        }

        [Fact]
        public void ErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";


            // Act
            var act = new UnauthorizedError(property, code);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be($"An error has occurred with code '{code}'");
        }



        [Fact]
        public void Error_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Unauthorized(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }



        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result act = Error.Unauthorized(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnauthorizedError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result act = Error.Unauthorized(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.UNAUTHORIZED);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnauthorizedError>();
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var code = "fakeUnauthorizedCode";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result<FakeModel> act = Error.Unauthorized(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnauthorizedError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeUnauthorizedProperty";
            var description = "fakeUnauthorizedDescription";


            // Act
            Result<FakeModel> act = Error.Unauthorized(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.UNAUTHORIZED);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnauthorizedError>();
        }
    }
}
