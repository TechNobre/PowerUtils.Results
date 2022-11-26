using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class MethodSuccessTests
    {
        [Fact]
        public void SuccessMethod_CreateResult_IsErrorFalse()
        {
            // Arrange && Act
            var act = Result.Success();


            // Assert
            act.IsError.Should()
                .BeFalse();
        }
    }
}
