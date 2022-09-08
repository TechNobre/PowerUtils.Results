using System;
using System.Collections.Generic;
using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests
{
    public class ResultUtilsTests
    {
        [Fact]
        public void CustomError_ResultFrom_ResultModel()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result<FakeModel>.From(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }


        [Fact]
        public void CustomError_ResultFromWithType_ResultModel()
        {
            // Arrange
            var property = "fakeCustom1Property";
            var code = "fakeCustom1Code";
            var description = "fakeCustom1Description";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From<FakeModel>(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }

        [Fact]
        public void Value_Ok_ResultModel()
        {
            // Arrange
            var id = 4723789;
            var name = "fake name";
            var mode = new FakeModel { Id = id, Name = name };

            var result = Result<FakeModel>.Ok(mode);


            // Act
            var act = result.Value;


            // Assert
            act.Id.Should().Be(id);
            act.Name.Should().Be(name);

            act.Should().BeOfType<FakeModel>();
        }

        [Fact]
        public void Value_OkImplicit_ResultModel()
        {
            // Arrange
            var id = 4723789;
            var name = "fake name";
            var mode = new FakeModel { Id = id, Name = name };

            var result = Result.Ok(mode);


            // Act
            var act = result.Value;


            // Assert
            act.Id.Should().Be(id);
            act.Name.Should().Be(name);

            act.Should().BeOfType<FakeModel>();
        }


        [Fact]
        public void EmptyErrorList_Create_ValueResult()
        {
            // Arrange
            var errors = Array.Empty<Error>();


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void NullErrorList_Create_ValueResult()
        {
            // Arrange
            Error[] errors = null;


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(FakeModel));
        }

        [Fact]
        public void IErrorListwithOneError_Create_ErrorResult()
        {
            // Arrange
            var errors = new List<IError> { Error.Forbidden("fake", "fake", "fake") };


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(ForbiddenError));
        }

        [Fact]
        public void ErrorListwithOneError_Create_ErrorResult()
        {
            // Arrange
            var errors = new List<Error> { Error.Failure("fake", "fake", "fake") };


            // Act
            var act = Result.Create(
                errors,
                () => new FakeModel()
            );


            // Assert
            act.GetType().Should().Be(typeof(Error));
        }

        [Fact]
        public void CustomError_ResultFrom_Result()
        {
            // Arrange
            var property = "fakeCustomProperty";
            var code = "fakeCustomCode";
            var description = "fakeCustomDescription";

            var error = new CustomError(property, code, description);


            // Act
            var act = Result.From(error);


            // Assert
            act.FirstError().Property.Should()
                .Be(property);
            act.FirstError().Code.Should()
                .Be(code);
            act.FirstError().Description.Should()
                .Be(description);

            act.GetType().Should().Be(typeof(CustomError));
        }
    }
}
