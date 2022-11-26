using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class MethodSuccessTests
    {
        [Fact]
        public void SuccessMethod_CreateResult_IsErrorFalse()
        {
            // Arrange && Act
            var act = Result.Success(new FakeModel());


            // Assert
            act.GetType().Should()
                .Be<FakeModel>();
        }
    }
}
