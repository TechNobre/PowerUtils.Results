using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResultTests
{
    public class MethodAddErrorTests
    {
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
    }
}
