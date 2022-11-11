using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class VoidResultTests
    {
        [Fact]
        public void SuccessMethod_CreateResult_IsErrorFalse()
        {
            // Arrange && Act
            var act = Result.Success();


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void Success_ImplicitCreation_IsErrorFalse()
        {
            // Arrange && Act
            Result act = new Success();


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void WithoutErrors_GetErrors_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.Errors);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }

        [Fact]
        public void ErrorListWithNull_InplicitCreation_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError> { null };


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyErrorList_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<Error>();


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyIErrorList_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError>();


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyErrorArray_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<Error>();


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyIErrorArray_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<IError>();


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullErrorList_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            List<Error> errors = null;


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullIErrorList_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            List<IError> errors = null;


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullErrorArray_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            Error[] errors = null;


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullIErrorArray_CreateVoidResult_IsErrorFalse()
        {
            // Arrange
            IError[] errors = null;


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void ErrorList_ImplicitAssignment_ErrorResult()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            var errors = new List<Error>()
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );
        }

        [Fact]
        public void ErrorArray_ImplicitAssignment_ErrorResult()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            var errors = new Error[]
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };


            // Act
            Result act = errors;


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );
        }

        [Fact]
        public void Null_AddError_OneError()
        {
            // Arrange
            Result result = Error.Unauthorized("Fake1");


            // Act
            result.AddError(null);


            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void ListOfErrorsWithNull_AddErrors_ZeroErrors()
        {
            // Arrange
            var result = Result.Ok();
            var listOfErrors = new IError[] { null };


            // Act
            result.AddErrors(listOfErrors);
            var act = Record.Exception(() => result.Errors.Count());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            result.IsError.Should().BeFalse();
        }

        [Fact]
        public void ThreeErrors_AddError_HasErrors()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            var property3 = "fakeProperty3";
            var code3 = "fakeCode3";
            var description3 = "fakeDescription3";


            // Act
            Result act = new Error(property1, code1, description1);
            act.AddError(new Error(property2, code2, description2));
            act.AddError(new Error(property3, code3, description3));


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property3
                    &&
                    s.Code == code3
                    &&
                    s.Description == description3
                );
        }

        [Fact]
        public void ListOfErrors_AddErrors_FourErrors()
        {
            // Arrange
            Result result = Error.Forbidden("Fake1");
            var listOfErrors = new IError[]
            {
                Error.Failure("Fake2", "Failed"),
                Error.NotFound("Fake3"),
                Error.Validation("Fake4")
            };


            // Act
            result.AddErrors(listOfErrors);


            // Assert
            result.Errors.Should().HaveCount(4);
        }

        [Fact]
        public void TwoErrors_AddObjectFromDefaultConstructor_HasErrors()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";


            // Act
            var act = new Result();
            act.AddError(new Error(property1, code1, description1));
            act.AddError(property2, code2, description2);


            // Assert
            act.IsError.Should()
                .BeTrue();

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Errors.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );
        }

        [Fact]
        public void WithoutError_GetType_Success()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(Success));
        }

        [Fact]
        public void WithConflictError_GetType_ConflictError()
        {
            // Arrange
            Result result = Error.Conflict("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(ConflictError));
        }

        [Fact]
        public void WithNotFoundError_GetType_NotFoundError()
        {
            // Arrange
            Result result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }

        [Fact]
        public void CustomError_ResultFrom_Result()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }


        [Fact]
        public void ValueResultWithErrors_ImplicitAssignment_VoidResult()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";
            Result<FakeModel> result = Error.Conflict(property, code, description);


            // Act
            Result act = result;


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(ConflictError));
        }

        [Fact]
        public void ValueResultWithoutErrors_ImplicitAssignment_VoidResult()
        {
            // Arrange
            Result<FakeModel> result = Array.Empty<Error>();


            // Act
            Result act = result;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

#if NET5_0
        [Fact]
        public void ValueResultNull_ImplicitAssignment_VoidResult()
        {
            // Arrange
            Result<FakeModel> result = null;


            // Act
            Result act = result;


            // Assert
            act?.IsError.Should()
                .BeFalse();
        }
#endif

        [Fact]
        public void ResultWithoutErrors_ImplicitAssignmentBool_ShouldBeFalse()
        {
            // Arrange
            Result result = new Success();


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeTrue();
        }

        [Fact]
        public void ResultWithErrors_ImplicitAssignmentBool_ShouldBeTrue()
        {
            // Arrange
            Result result = Error.Forbidden("prop");


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeFalse();
        }

        [Fact]
        public void ResultWithErrors_ImplicitAssignmentListError_ListError()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            var errors = new List<Error>()
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };

            Result result = errors;


            // Act
            List<IError> act = result;


            // Assert
            act.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );
        }



        [Fact]
        public void ResultWithErrors_IsSuccess_IsSuccessFalseAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result result = Error.Forbidden(property);


            // Act
            var act = result.IsSuccess(out var errors);


            // Assert
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithoutErrors_IsSuccess_IsSuccessTrueAndErrors()
        {
            // Arrange
            Result result = new Success();


            // Act
            var act = result.IsSuccess(out var errors);


            // Assert
            errors.Should().BeEmpty();

            act.Should().BeTrue();
        }
    }
}
