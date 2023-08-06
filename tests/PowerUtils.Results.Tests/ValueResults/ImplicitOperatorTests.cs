using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class ImplicitOperatorTests
    {
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
            using(new AssertionScope())
            {
                act.Value.Id.Should().Be(id);
                act.Value.Name.Should().Be(name);
            }
        }

        [Fact]
        public void EmptyErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<Error>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void ErrorListWithNull_InplicitCreation_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError> { null };


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void ErrorListWithThreeNulls_InplicitCreation_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError> { null, null, null };


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void EmptyIErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = new List<IError>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void EmptyErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<Error>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void EmptyIErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            var errors = Array.Empty<IError>();


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void NullErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            List<Error> errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void NullIErrorList_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            List<IError> errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void NullErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            Error[] errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
        }

        [Fact]
        public void NullIErrorArray_CreateValueResult_IsErrorFalse()
        {
            // Arrange
            IError[] errors = null;


            // Act
            Result<FakeModel> act = errors;


            // Assert
            act.IsError.Should().BeFalse();
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
            using(new AssertionScope())
            {
                act.Id.Should().Be(id);
                act.Name.Should().Be(name);
            }
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
            using(new AssertionScope())
            {
                act.Should().ContainsError<Error>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<Error>(
                    property2,
                    code2,
                    description2);
            }
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
            using(new AssertionScope())
            {
                act.Should().ContainsError<Error>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<Error>(
                    property2,
                    code2,
                    description2);
            }
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
            using(new AssertionScope())
            {
                act.Id.Should().Be(id);
                act.Name.Should().Be(name);
            }
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
            act.Should().ContainsError<ConflictError>(
                property,
                code,
                description);
        }

        [Fact]
        public void VoidResultWithoutErrors_ImplicitAssignment_ValueResult()
        {
            // Arrange
            Result result = Array.Empty<Error>();


            // Act
            Result<FakeModel> act = result;


            // Assert
            using(new AssertionScope())
            {
                act.IsError.Should().BeFalse();
                act.Value.Should().BeNull();
            }
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
            using(new AssertionScope())
            {
                act?.IsError.Should().BeFalse();
                act.Value.Should().BeNull();
            }
        }
#endif

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
            using(new AssertionScope())
            {
                act.Should().ContainsError<Error>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<Error>(
                    property2,
                    code2,
                    description2);
            }
        }


        [Fact]
        public void IQueryable_Implicit_OriginalValues()
        {
            // Arrange
            var arr = new[] { "me", "you", "them" };
            var queryable = arr.AsQueryable();


            // Act
            var act = Result.Success(queryable);


            // Assert
            act.Value.Should().BeEquivalentTo(arr);
        }
    }
}
