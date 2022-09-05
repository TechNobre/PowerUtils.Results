using System;
using System.Collections.Generic;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class WrapperResultTests
    {
        [Fact]
        public void WithoutErrors_GetErrors_InvalidOperationException()
        {
            // Arrange
            var result = Result<FakeModel>.Ok(new());


            // Act
            var act = Record.Exception(() => result.Errors);


            // Assert
            act.Should()
                .BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void WithErrors_GetValue_InvalidOperationException()
        {
            // Arrange
            Result<FakeModel> result = Error.Failure("fake", "fake", "fake");


            // Act
            var act = Record.Exception(() => result.Value);


            // Assert
            act.Should()
                .BeOfType<InvalidOperationException>();
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
        public void ThreeErrors_Add_HasErrors()
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
    }
}
