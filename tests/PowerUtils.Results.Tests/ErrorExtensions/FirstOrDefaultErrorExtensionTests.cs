using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensions
{
    public class FirstOrDefaultErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public FirstOrDefaultErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_FirstOrDefaultError_First()
        {
            // Arrange && Act
            var act = _result.FirstOrDefaultError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_FirstOrDefaultError_Null()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = result.FirstOrDefaultError();


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void WithoutErrors_FirstOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            var result = Result.Ok();
            static bool fakePredicate(IError x) => x.Code == ResultErrorCodes.VALIDATION;

            //Act
            var act = result.FirstOrDefaultError(fakePredicate);

            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void MoreThanOneError_FirstOrDefaultErrorWithPredicate_FirstError()
        {
            // Arrange
            bool fakePredicate(IError x) => x.Code == _firstError.Code;


            //Act
            var act = _result.FirstOrDefaultError(fakePredicate);


            // Assert
            act.Should()
               .Be(_firstError);
        }

        [Fact]
        public void MoreThanOneError_FirstOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            static bool fakePredicate(IError x) => x.Code == "fakeErrorCodeToFail";


            //Act
            var act = _result.FirstOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }
    }
}
