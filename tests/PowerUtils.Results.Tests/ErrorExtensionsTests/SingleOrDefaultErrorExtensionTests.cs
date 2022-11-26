using System;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests.ErrorExtensionsTests
{
    public class SingleOrDefaultErrorExtensionTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public SingleOrDefaultErrorExtensionTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void WithoutErrors_SigleOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            var result = Result.Ok();
            static bool fakePredicate(IError x) => x.Code == "fakeLastErrorCodeToFail";


            //Act
            var act = result.SingleOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void MoreThanOneError_SigleOrDefaultErrorWithPredicate_Error()
        {
            // Arrange
            bool fakePredicate(IError x) => x.Code == _firstError.Code;


            //Act
            var act = _result.SingleOrDefaultError(fakePredicate);


            // Assert
            act.Should()
               .Be(_firstError);
        }

        [Fact]
        public void MoreThanOneError_SigleOrDefaultErrorWithPredicate_Null()
        {
            // Arrange
            static bool fakePredicate(IError x) => x.Code == "fakeSingleErrorCodeToFail";


            //Act
            var act = _result.SingleOrDefaultError(fakePredicate);


            // Assert
            act.Should().BeNull();
        }

        [Fact]
        public void TwoErrors_SingleOrDefaultError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(_result.SingleOrDefaultError);


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneError_SingleOrDefaultError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };


            // Act
            var act = result.SingleOrDefaultError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_SingleOrDefaultError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();



            // Act
            var act = result.SingleOrDefaultError();


            // Assert
            act.Should().BeNull();
        }
    }
}
