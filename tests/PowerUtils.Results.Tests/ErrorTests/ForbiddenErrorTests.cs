using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorTests
{
    public class ForbiddenErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            var act = new ForbiddenError(property, code, description);


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
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";


            // Act
            var act = new ForbiddenError(property, code);


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
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Forbidden(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }



        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            Result act = Error.Forbidden(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ForbiddenError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var description = "fakeForbiddenDescription";


            // Act
            Result act = Error.Forbidden(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.FORBIDDEN);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ForbiddenError>();
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var code = "fakeForbiddenCode";
            var description = "fakeForbiddenDescription";


            // Act
            Result<FakeModel> act = Error.Forbidden(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ForbiddenError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeForbiddenProperty";
            var description = "fakeForbiddenDescription";


            // Act
            Result<FakeModel> act = Error.Forbidden(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.FORBIDDEN);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ForbiddenError>();
        }
    }
}
