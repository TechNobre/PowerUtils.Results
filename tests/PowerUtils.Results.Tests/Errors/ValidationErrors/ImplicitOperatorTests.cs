using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ValidationErrors
{
    public class ImplicitOperatorTests
    {
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
