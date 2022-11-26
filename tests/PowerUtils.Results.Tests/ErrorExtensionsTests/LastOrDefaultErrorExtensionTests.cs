using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensionsTests
{
    public class LastOrDefaultErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public LastOrDefaultErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_LastOrDefaultError_Last()
        {
            // Arrange && Act
            var act = _result.LastOrDefaultError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void WithoutErrors_LastOrDefaultError_Null()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.LastOrDefaultError();


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void WithoutError_LastOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            var result = Result.Ok();
            static bool fakePredicate(IError x) => x.Code == "fakeErrorCodeToFail";


            //Act
            var act = result.LastOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void MoreThanOneError_LastOrDefaultErrorWithPredicate_LastError()
        {
            // Arrange
            bool fakePredicate(IError x) => x.Code == _lastError.Code;


            //Act
            var act = _result.LastOrDefaultError(fakePredicate);


            // Assert
            act.Should()
               .Be(_lastError);
        }

        [Fact]
        public void MoreThanOneError_LastOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            static bool fakePredicate(IError x) => x.Code == "fakeLastErrorCodeToFail";


            //Act
            var act = _result.LastOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }
    }
}
