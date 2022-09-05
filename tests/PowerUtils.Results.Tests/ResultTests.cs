using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
namespace PowerUtils.Results.Tests
{
    public class ResultTests
    {
        [Fact]
        public void WithoutErrors_GetErrors_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.Errors);


            // Assert
            act.Should()
                .BeOfType<InvalidOperationException>();
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
    }
}
