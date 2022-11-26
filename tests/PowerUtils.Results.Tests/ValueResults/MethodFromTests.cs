using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class MethodFromTests
    {
        [Fact]
        public void CustomError_ResultFrom_ResultModel()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result<FakeModel>.From(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }


        [Fact]
        public void CustomError_ResultFromWithType_ResultModel()
        {
            // Arrange
            var property = "fakeCustom1Property";
            var code = "fakeCustom1Code";
            var description = "fakeCustom1Description";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From<FakeModel>(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }
    }
}
