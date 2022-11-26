using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResultTests
{
    public class MethodOkTests
    {
        [Fact]
        public void Value_Ok_ResultModel()
        {
            // Arrange
            var id = 4723789;
            var name = "fake name";
            var mode = new FakeModel { Id = id, Name = name };


            // Act
            var act = Result<FakeModel>.Ok(mode);


            // Assert
            act.Value.Id.Should().Be(id);
            act.Value.Name.Should().Be(name);

            act.Should().BeOfType<FakeModel>();
        }

        [Fact]
        public void Value_OkImplicit_ResultModel()
        {
            // Arrange
            var id = 4723789;
            var name = "fake name";
            var mode = new FakeModel { Id = id, Name = name };


            // Act
            var act = Result.Ok(mode);


            // Assert
            act.Value.Id.Should().Be(id);
            act.Value.Name.Should().Be(name);

            act.Should().BeOfType<FakeModel>();
        }
    }
}
