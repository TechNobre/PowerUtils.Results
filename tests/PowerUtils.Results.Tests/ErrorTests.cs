using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ErrorTests
    {
        [Fact]
        public void ErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";


            // Act
            var act = new Error(property, code, description);


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
            var property = "fakeProperty";
            var code = "fakeCode";


            // Act
            var act = new Error(property, code);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be($"An error has occurred with code '{code}'");
        }

        [Fact]
        public void UnauthorizedErrorWithDescription_Construct_CustomDescription()
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
        public void UnauthorizedErrorWithoutDescription_Construct_DefaultDescription()
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
        public void ForbiddenErrorWithDescription_Construct_CustomDescription()
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
        public void ForbiddenErrorWithoutDescription_Construct_DefaultDescription()
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
        public void NotFoundErrorWithDescription_Construct_CustomDescription()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            var act = new NotFoundError(property, code, description);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be(description);
        }

        [Fact]
        public void NotFoundErrorWithoutDescription_Construct_DefaultDescription()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";


            // Act
            var act = new NotFoundError(property, code);


            // Assert
            act.Property.Should()
                .Be(property);
            act.Code.Should()
                .Be(code);
            act.Description.Should()
                .Be($"An error has occurred with code '{code}'");
        }

        [Fact]
        public void ConflictErrorWithDescription_Construct_CustomDescription()
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
        public void ConflictErrorWithoutDescription_Construct_DefaultDescription()
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
        public void ValidationErrorWithDescription_Construct_CustomDescription()
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
        public void ValidationErrorWithoutDescription_Construct_DefaultDescription()
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
        public void UnexpectedErrorWithDescription_Construct_CustomDescription()
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
        public void UnexpectedErrorWithoutDescription_Construct_DefaultDescription()
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
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.Failure(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }

        [Fact]
        public void UnauthorizedError_Deconstruct_Properties()
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
        public void ForbiddenError_Deconstruct_Properties()
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
        public void NotFoundError_Deconstruct_Properties()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            var (actProperty, actCode, actDescription) = Error.NotFound(property, code, description);


            // Assert
            actProperty.Should().Be(property);
            actCode.Should().Be(code);
            actDescription.Should().Be(description);
        }

        [Fact]
        public void ConflictError_Deconstruct_Properties()
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
        public void ValidationError_Deconstruct_Properties()
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
        public void UnexpectedError_Deconstruct_Properties()
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
    }
}
