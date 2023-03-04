using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class MethodFromTests
    {
        [Fact]
        public void CustomError_ResultFrom_ResultWithError()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From(error);


            // Assert
            act.Should().ContainsError<CustomError>(
                property,
                code,
                description);
        }

        [Fact]
        public void ErrorList_ResultFrom_ResultWithError()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            var errors = new List<IError>
            {
                new CustomError(property1, code1, description1),
                new ForbiddenError(property2, code2, description2)
            };


            // Act
            var act = Result.From(errors);


            // Assert
            using(new AssertionScope())
            {
                act.Should().ContainsError<CustomError>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<ForbiddenError>(
                    property2,
                    code2,
                    description2);
            }
        }
    }
}
