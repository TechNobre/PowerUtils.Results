using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorTests
{
    public class ConflictErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            var act = new ConflictError(property, code, description);


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
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";


            // Act
            var act = new ConflictError(property, code);


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
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Conflict(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }



        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            Result act = Error.Conflict(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ConflictError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var description = "fakeConflictDescription";


            // Act
            Result act = Error.Conflict(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.CONFLICT);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ConflictError>();
        }



        [Fact]
        public void Error_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var code = "fakeConflictCode";
            var description = "fakeConflictDescription";


            // Act
            Result<FakeModel> act = Error.Conflict(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ConflictError>();
        }

        [Fact]
        public void ErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeConflictProperty";
            var description = "fakeConflictDescription";


            // Act
            Result<FakeModel> act = Error.Conflict(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.CONFLICT);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<ConflictError>();
        }
    }
}
