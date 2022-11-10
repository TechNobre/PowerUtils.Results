using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ValueResultTests
    {
        [Fact]
        public void SuccessMethod_CreateResult_IsErrorFalse()
        {
            // Arrange && Act
            var act = Result.Success(new FakeModel());


            // Assert
            act.GetType().Should()
                .Be<FakeModel>();
        }

        [Fact]
        public void WithoutErrors_GetErrors_InvalidOperationException()
        {
            // Arrange
            var result = Result<FakeModel>.Ok(new());


            // Act
            var act = Record.Exception(() => result.Errors);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }

        [Fact]
        public void WithErrors_GetValue_InvalidOperationException()
        {
            // Arrange
            Result<FakeModel> result = Error.Failure("fake", "fake", "fake");


            // Act
            var act = Record.Exception(() => result.Value);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Value can be retrieved only when the result is not an error");
        }

        [Fact]
        public void EmptyErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<Error>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void ErrorListWithNull_InplicitCreation_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError> { null };


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyIErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<Error>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void EmptyIErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<IError>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            List<Error> errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullIErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            List<IError> errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            Error[] errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void NullIErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            IError[] errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should()
                .BeFalse();
        }

        [Fact]
        public void WithValue_GetValue_SameValue()
        {
            // Arrange
            var id = 343;
            var name = "FakeName";

            var result = Result<FakeModel>.Ok(new(id, name));


            // Act
            var act = result.Value;


            // Assert
            act.Id.Should()
                .Be(id);
            act.Name.Should()
                .Be(name);
        }

        [Fact]
        public void Value_ImplicitAssignment_SameValue()
        {
            // Arrange
            var id = 343;
            var name = "FakeName";

            var model = new FakeModel(id, name);


            // Act
            Result<FakeModel> act = model;


            // Assert
            act.Value.Id.Should()
                .Be(id);
            act.Value.Name.Should()
                .Be(name);
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
            Result<FakeModel> act = errors;


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
            Result<FakeModel> act = errors;


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
            Result<FakeModel> result = Error.Forbidden("Fake1");


            // Act
            result.AddError(null);


            // Assert
            result.Errors.Should().HaveCount(1);
        }

        [Fact]
        public void ListOfErrorsWithNull_AddErrors_TwoErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();
            var listOfErrors = new List<IError> { null };


            // Act
            result.AddErrors(listOfErrors);
            var act = Record.Exception(() => result.Errors.Count());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            result.IsError.Should().BeFalse();
        }

        [Fact]
        public void ListOfErrors_AddErrors_ThreeErrors()
        {
            // Arrange
            Result<FakeModel> result = Error.Forbidden("Fake1");
            var listOfErrors = new List<IError>
            {
                Error.Unauthorized("Fake2"),
                Error.Unexpected("Fake3")
            };


            // Act
            result.AddErrors(listOfErrors);


            // Assert
            result.Errors.Should().HaveCount(3);
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
            Result<FakeModel> act = new Error(property1, code1, description1);
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
            var act = Result<FakeModel>.Ok(null);
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
        public void WithoutError_GetType_TypeOfModel()
        {
            // Arrange
            var model = new FakeModel();
            var result = Result<FakeModel>.Ok(model);


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void WithConflictError_GetType_ConflictError()
        {
            // Arrange
            Result<FakeModel> result = Error.Conflict("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(ConflictError));
        }

        [Fact]
        public void WithNotFoundError_GetType_NotFoundError()
        {
            // Arrange
            Result<FakeModel> result = Error.NotFound("prop", "code", "disc");


            // Act
            var act = result.GetType();


            // Assert
            act.Should().Be(typeof(NotFoundError));
        }

        [Fact]
        public void ResultWithValue_ImplicitAssignment_Value()
        {
            // Arrange
            var id = 45615;
            var name = "fake name";

            Result<FakeModel> result = new FakeModel { Id = id, Name = name };


            // Act
            FakeModel act = result;


            // Assert
            act.Id.Should().Be(id);
            act.Name.Should().Be(name);
        }



        [Fact]
        public void VoidResultWithErrors_ImplicitAssignment_ValueResult()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";
            Result result = Error.Conflict(property, code, description);


            // Act
            Result<FakeModel> act = result;


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
        public void VoidResultWithoutErrors_ImplicitAssignment_ValueResult()
        {
            // Arrange
            Result result = Array.Empty<Error>();


            // Act
            Result<FakeModel> act = result;


            // Assert
            act.IsError.Should()
                .BeFalse();
            act.Value.Should()
                .BeNull();
        }

#if NET5_0
        [Fact]
        public void VoidResultNull_ImplicitAssignment_ValueResult()
        {
            // Arrange
            Result result = null;


            // Act
            Result<FakeModel> act = result;


            // Assert
            act?.IsError.Should()
                .BeFalse();
            act.Value.Should()
                .BeNull();
        }
#endif

        [Fact]
        public void ResultWithoutErrors_ImplicitAssignmentBool_ShouldBeFalse()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


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
            Result<FakeModel> result = Error.Forbidden("prop");


            // Act
            var act = (bool)result;


            // Assert
            act.Should()
                .BeFalse();
        }

        [Fact]
        public void ResultWithErrors_ImplicitAssignmentIEnumerableError_IEnumerableError()
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

            Result<FakeModel> result = errors;


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
        public void ResultWithErrors_Deconstruct_ValueNullAndErrors()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            Result<FakeModel> result = Error.Conflict(property, code, description);


            // Act
            (var value, var errors) = result;


            // Assert
            value.Should().BeNull();
            errors.Should()
                .ContainSingle(s =>
                    s.Property == property
                    &&
                    s.Code == code
                    &&
                    s.Description == description
                );
        }

        [Fact]
        public void ResultWithoutErrors_Deconstruct_WithValueAndEmptyErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            (var value, var errors) = result;


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();
        }

        [Fact]
        public void ResultWithErrors_If_ValueNullAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result<FakeModel> result = Error.Forbidden(property);


            // Act
            FakeModel value = null;
            List<IError> errors = null;
            var condition = 0;

            if(result is (var value1, var errors1) && errors1.Count == 0)
            {
                value = value1;
                errors = errors1;
                condition = 1;
            }

            if(result is (var value2, var errors2) && errors2.Count == 1)
            {
                value = value2;
                errors = errors2;
                condition = 2;
            }


            // Assert
            value.Should().BeNull();
            errors.Should().ContainSingle(s => s.Property == property);

            condition.Should().Be(2);
        }

        [Fact]
        public void ResultWithoutErrors_If_ValueNullAndErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            FakeModel value = null;
            List<IError> errors = null;
            var condition = 0;

            if(result is (var value1, var errors1) && errors1.Count == 0)
            {
                value = value1;
                errors = errors1;
                condition = 1;
            }

            if(result is (var value2, var errors2) && errors2.Count == 1)
            {
                value = value2;
                errors = errors2;
                condition = 2;
            }


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();

            condition.Should().Be(1);
        }

        [Fact]
        public void ResultWithErrors_IsSuccess_IsSuccessFalseValueNullAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result<FakeModel> result = Error.Forbidden(property);


            // Act
            var act = result.IsSuccess(out var value, out var errors);


            // Assert
            value.Should().BeNull();
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeFalse();
        }

        [Fact]
        public void ResultWithoutErrors_IsSuccess_IsSuccessTrueValueNullAndErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            var act = result.IsSuccess(out var value, out var errors);


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();

            act.Should().BeTrue();
        }
    }
}
