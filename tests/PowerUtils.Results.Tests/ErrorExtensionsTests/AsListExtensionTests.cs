using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensionsTests
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
            act.Should()
                .HaveCount(2);

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
