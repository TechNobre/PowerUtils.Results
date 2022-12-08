using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class DistinctErrorsTests
    {
        [Fact]
        public void ThreeDuplicatedErrorsAndTwoDifferents_DistinctErrors_TwoErrors()
        {
            // Arrange
            Result<FakeModel> result = new IError[]
            {
                Error.Failure("FakeProperty", "FakeCode", "FakeDescription"),
                Error.Failure("FakeProperty", "FakeCode", "FakeDescription"),
                Error.Failure("FakeProperty", "FakeCode", "FakeDescription"),
                Error.Conflict("FakeProperty", "FakeCode", "FakeDescription")
            };


            // Act
            var act = result.DistinctErrors();


            // Assert
            using(new AssertionScope())
            {
                act.Should().HaveCount(2);

                act.First().Property.Should().Be("FakeProperty");
                act.First().Code.Should().Be("FakeCode");
                act.First().Description.Should().Be("FakeDescription");
                act.First().Should().BeOfType<Error>();

                act.Last().Property.Should().Be("FakeProperty");
                act.Last().Code.Should().Be("FakeCode");
                act.Last().Description.Should().Be("FakeDescription");
                act.Last().Should().BeOfType<ConflictError>();
            }
        }


        [Fact]
        public void AllErrorsDifferent_DistinctErrors_ReturnAllErrors()
        {
            // Arrange
            Result<FakeModel> result = new IError[]
            {
                Error.Failure("FakeProperty", "FakeCode", "FakeDescription"),
                Error.Failure("FakeProperty1", "FakeCode", "FakeDescription"),
                Error.Failure("FakeProperty", "FakeCode2", "FakeDescription"),
                Error.Failure("FakeProperty", "FakeCode2", "FakeDescription3"),
                Error.Conflict("FakeProperty", "FakeCode", "FakeDescription"),
                Error.Unauthorized("FakeProperty", "FakeCode2", "FakeDescription3"),
            };


            // Act
            var act = result.DistinctErrors();


            // Assert
            act.Should().HaveCount(6);
        }
    }
}
