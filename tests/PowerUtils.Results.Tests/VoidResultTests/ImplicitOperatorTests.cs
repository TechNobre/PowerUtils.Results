using System;
using System.Collections.Generic;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResultTests
{
    public class ImplicitOperatorTests
    {
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
        public void Success_ImplicitSuccess_IsErrorFalse()
        {
            // Arrange && Act
            Result act = new Success();


            // Assert
            act.IsError.Should()
                .BeFalse();
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
    }

}
