using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ErrorExtensionsTests
    {
        private readonly Error _firstError = new("FirstProperty", "FirstCode", "FirstDescription");
        private readonly Error _lastError = new("LastProperty", "LastCode", "LastDescription");

        private readonly Result _result;

        public ErrorExtensionsTests()
            => _result = new Error[] { _firstError, _lastError };



        [Fact]
        public void TwoErrors_FirstError_First()
        {
            // Arrange && Act
            var act = _result.FirstError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_FirstError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.FirstError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }



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
        public void TwoErrors_LastError_Last()
        {
            // Arrange && Act
            var act = _result.LastError();


            // Assert
            act.Should().Be(_lastError);
        }

        [Fact]
        public void WithoutErrors_LastError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.LastError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }



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
        public void TwoErrors_SingleError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(() => _result.SingleError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
        }

        [Fact]
        public void OneError_SingleError_Error()
        {
            // Arrange
            Result result = new Error[] { _firstError };



            // Act
            var act = result.SingleError();


            // Assert
            act.Should().Be(_firstError);
        }

        [Fact]
        public void WithoutErrors_SingleError_InvalidOperationException()
        {
            // Arrange
            var result = Result.Ok();


            // Act
            var act = Record.Exception(() => result.SingleError());


            // Assert
            act.Should().BeOfType<InvalidOperationException>();
            act.Message.Should().Be("Errors can be retrieved only when the result is an error");
        }



        [Fact]
        public void TwoErrors_SingleOrDefaultError_InvalidOperationException()
        {
            // Arrange && Act
            var act = Record.Exception(() => _result.SingleOrDefaultError());


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

        [Fact]
        public void ErrorList_AsList_ErrorsAsList()
        {
            // Arrange
            var property1 = "fakeProperty1";
            var code1 = "fakeCode1";
            var description1 = "fakeDescription1";

            var property2 = "fakeProperty2";
            var code2 = "fakeCode2";
            var description2 = "fakeDescription2";

            Result result = new List<Error>()
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };


            // Act
            var act = result.Errors.AsList();


            // Assert
            act.Should()
                .HaveCount(2);

            act.Should()
                .ContainSingle(s =>
                    s.Property == property1
                    &&
                    s.Code == code1
                    &&
                    s.Description == description1
                );

            act.Should()
                .ContainSingle(s =>
                    s.Property == property2
                    &&
                    s.Code == code2
                    &&
                    s.Description == description2
                );
        }
    }
}
