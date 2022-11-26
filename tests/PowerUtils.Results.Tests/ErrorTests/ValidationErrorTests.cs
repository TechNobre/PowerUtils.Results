using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorTests
{
    public class ValidationErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";
            var description = "fakeValidationDescription";


            // Act
            var act = new ValidationError(property, code, description);


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
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";


            // Act
            var act = new ValidationError(property, code);


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
            var property = "fakeValidationProperty";
            var code = "fakeValidationCode";
            var description = "fakeValidationDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Validation(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }



        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeValidationProperty";
            var description = "fakeValidationDescription";


            //Act
            Result act = Error.Validation(property, description);


            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == ResultErrorCodes.VALIDATION);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ValidationError>();
        }

        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeValidationProperty";
            var description = "fakeValidationDescription";
            var code = "fakeValidationCode";


            //Act
            Result act = Error.Validation(property, code, description);


            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == code);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ValidationError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeValidationProperty";
            var description = "fakeValidationDescription";


            //Act
            Result<FakeModel> act = Error.Validation(property, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == ResultErrorCodes.VALIDATION);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ValidationError>();
        }

        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeValidationProperty";
            var description = "fakeValidationDescription";
            var code = "fakeValidationCode";


            //Act
            Result<FakeModel> act = Error.Validation(property, code, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == code);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ValidationError>();
        }
    }
}
