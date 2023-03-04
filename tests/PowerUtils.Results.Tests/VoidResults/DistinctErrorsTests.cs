using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class DistinctErrorsTests
    {
        [Fact]
        public void ThreeDuplicatedErrorsAndTwoDifferents_DistinctErrors_TwoErrors()
        {
            // Arrange
            Result result = new IError[]
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

                act.Should().ContainsError<Error>(
                    "FakeProperty",
                    "FakeCode",
                    "FakeDescription");

                act.Should().ContainsError<ConflictError>(
                    "FakeProperty",
                    "FakeCode",
                    "FakeDescription");
            }
        }


        [Fact]
        public void AllErrorsDifferent_DistinctErrors_ReturnAllErrors()
        {
            // Arrange
            Result result = new IError[]
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
