using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorTests
{
    public class UnexpectedErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeUnexpectedProperty";
            var code = "fakeValidationCode";
            var description = "fakeUnexpectedDescription";


            // Act
            var act = new UnexpectedError(property, code, description);


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
            var property = "fakeUnexpectedProperty";
            var code = "fakeUnexpectedCode";


            // Act
            var act = new UnexpectedError(property, code);


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
            var property = "fakeUnexpectedProperty";
            var code = "fakeUnexpectedCode";
            var description = "fakeUnexpectedDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Unexpected(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }



        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";


            //Act
            Result act = Error.Unexpected(property, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == ResultErrorCodes.UNEXPECTED);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnexpectedError>();
        }

        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";
            var code = "fakeUnexpectedCode";


            //Act
            Result act = Error.Unexpected(property, code, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == code);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnexpectedError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";


            //Act
            Result<FakeModel> act = Error.Unexpected(property, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == ResultErrorCodes.UNEXPECTED);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnexpectedError>();
        }

        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            //Arrange
            var property = "fakeUnexpectedProperty";
            var description = "fakeUnexpectedDescription";
            var code = "fakeUnexpectedCode";


            //Act
            Result<FakeModel> act = Error.Unexpected(property, code, description);

            //Assert
            act.IsError.Should()
               .BeTrue();
            act.Errors.Should()
               .ContainSingle(s => s.Property == property);
            act.Errors.Should()
               .ContainSingle(s => s.Code == code);
            act.Errors.Should()
               .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<UnexpectedError>();
        }
    }

}
