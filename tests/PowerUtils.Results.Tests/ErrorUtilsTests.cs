﻿using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ErrorUtilsTests
    {
        [Fact]
        public void Error_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeFailureProperty";
            var code = "fakeFailureCode";
            var description = "fakeFailureDescription";


            // Act
            Result act = Error.Failure(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<Error>();
        }

        [Fact]
        public void UnauthorizedError_ImplicitResult_IsErrorTrue()
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
        public void UnauthorizedErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
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
        public void ForbiddenError_ImplicitResult_IsErrorTrue()
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
        public void ForbiddenErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
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
        public void NotFoundError_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            Result act = Error.NotFound(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public void NotFoundErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var description = "fakeNotFoundDescription";


            // Act
            Result act = Error.NotFound(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.NOT_FOUND);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public void ConflictError_ImplicitResult_IsErrorTrue()
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
        public void ConflictErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
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
            var property = "fakeFailureProperty";
            var code = "fakeFailureCode";
            var description = "fakeFailureDescription";


            // Act
            Result<FakeModel> act = Error.Failure(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<Error>();
        }

        [Fact]
        public void UnauthorizedError_ImplicitValueResult_IsErrorTrue()
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
        public void UnauthorizedErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
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

        [Fact]
        public void ForbiddenError_ImplicitValueResult_IsErrorTrue()
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
        public void ForbiddenErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
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

        [Fact]
        public void NotFoundError_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var code = "fakeNotFoundCode";
            var description = "fakeNotFoundDescription";


            // Act
            Result<FakeModel> act = Error.NotFound(property, code, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == code);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public void NotFoundErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
        {
            // Arrange
            var property = "fakeNotFoundProperty";
            var description = "fakeNotFoundDescription";


            // Act
            Result<FakeModel> act = Error.NotFound(property, description);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s => s.Property == property);
            act.Errors.Should()
                .ContainSingle(s => s.Code == ResultErrorCodes.NOT_FOUND);
            act.Errors.Should()
                .ContainSingle(s => s.Description == description);

            act.Errors.First().Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public void ConflictError_ImplicitValueResult_IsErrorTrue()
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
        public void ConflictErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
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

        [Fact]
        public void ValidationErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
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
        public void ValidationError_ImplicitResult_IsErrorTrue()
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
        public void ValidationErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
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
        public void ValidationError_ImplicitValueResult_IsErrorTrue()
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



        [Fact]
        public void UnexpectedErrorWithDefaultCode_ImplicitResult_IsErrorTrue()
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
        public void UnexpectedError_ImplicitResult_IsErrorTrue()
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
        public void UnexpectedErrorWithDefaultCode_ImplicitValueResult_IsErrorTrue()
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
        public void UnexpectedError_ImplicitValueResult_IsErrorTrue()
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