using System.Threading.Tasks;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultExtensions
{
    public class IsErrorExtensionTests
    {
        [Fact]
        public void ValueResultWithErrors_IsError_IsErrorTrueFalseValueNullAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result<FakeModel> result = Error.Forbidden(property);


            // Act
            var act = result.IsError(out var value, out var errors);


            // Assert
            value.Should().BeNull();
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeTrue();
        }

        [Fact]
        public void ValueResultWithoutErrors_IsError_IsErrorFalseValueNullAndErrors()
        {
            // Arrange
            Result<FakeModel> result = new FakeModel();


            // Act
            var act = result.IsError(out var value, out var errors);


            // Assert
            value.Should().NotBeNull();
            errors.Should().BeEmpty();

            act.Should().BeFalse();
        }


        [Fact]
        public void VoidResultWithErrors_IsError_IsErrorTrueAndErrors()
        {
            // Arrange
            var property = "Fake";
            Result result = Error.Forbidden(property);


            // Act
            var act = result.IsError(out var errors);


            // Assert
            errors.Should().ContainSingle(s => s.Property == property);

            act.Should().BeTrue();
        }

        [Fact]
        public void VoidResultWithoutErrors_IsError_IsErrorFalseAndErrors()
        {
            // Arrange
            Result result = new Success();


            // Act
            var act = result.IsError(out var errors);


            // Assert
            errors.Should().BeEmpty();

            act.Should().BeFalse();
        }



        [Fact]
        public async Task ValueResultWithErrors_IsErrorFromAsyncMethod_IsErrorTrueFalseValueNullAndErrors()
        {
            // Arrange
            const string PROPERTY = "Fake";

            async static Task<Result<FakeModel>> action()
            {
                await Task.Delay(20);
                return Error.Forbidden(PROPERTY);
            }


            // Act
            var act = (await action())
                .IsError(out var value, out var errors);


            // Assert
            value.Should().BeNull();
            errors.Should().ContainSingle(s => s.Property == PROPERTY);

            act.Should().BeTrue();
        }
    }
}
