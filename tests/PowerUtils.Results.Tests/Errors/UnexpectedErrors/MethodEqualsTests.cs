using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class MethodEqualsTests
    {
        [Fact]
        public void Null_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);


            // Act
            var act = error.Equals(null);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void NullIError_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = null;


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void DifferentProperty_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = Error.Unexpected("fake", code, description);


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void DifferentCode_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = Error.Unexpected(property, "fake", description);


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void DifferentDescription_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = Error.Unexpected(property, code, "fake");


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void DifferentType_Equals_False()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = Error.Unauthorized(property, code, description);


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeFalse();
        }

        [Fact]
        public void AllPropertiesEquals_Equals_True()
        {
            // Arrange
            var property = "fakeProperty";
            var code = "fakeCode";
            var description = "fakeDescription";

            IError error = Error.Unexpected(property, code, description);
            IError other = Error.Unexpected(property, code, description);


            // Act
            var act = error.Equals(other);


            // Assert
            act.Should().BeTrue();
        }
    }
}
