using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class FactorySuccessTests
    {
        [Fact]
        public void SuccessFactory_CreateResult_IsErrorFalse()
        {
            // Arrange && Act
            var act = Result.Success();


            // Assert
            act.IsError.Should().BeFalse();
        }
    }
}
