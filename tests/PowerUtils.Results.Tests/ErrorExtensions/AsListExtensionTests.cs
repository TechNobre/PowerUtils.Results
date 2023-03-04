using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensions
{
    public class AsListExtensionTests
    {
        [Fact]
        public void ErrorList_AsList_ErrorsAsList()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            Result result = new List<Error>()
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };


            // Act
            var act = result.Errors.AsList();


            // Assert
            using(new AssertionScope())
            {
                act.Should().HaveCount(2);

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
