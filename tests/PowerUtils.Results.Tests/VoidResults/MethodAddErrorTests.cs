﻿using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class MethodAddErrorTests
    {
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
            using(new AssertionScope())
            {
                act.Should().BeOfType<InvalidOperationException>();
                result.IsError.Should().BeFalse();
            }
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

                act.Should().ContainsError<Error>(
                    property3,
                    code3,
                    description3);
            }
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
    }
}
