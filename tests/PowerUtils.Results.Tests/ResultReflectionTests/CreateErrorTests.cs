using FluentAssertions;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ResultReflectionTests
{
    public class CreateErrorTests
    {
        private class ErrorWhioutCTOR : IError
        {
            public string Property { get; init; }
            public string Code { get; init; }
            public string Description { get; init; }

            public bool Equals(IError other) => throw new System.NotImplementedException();
        }

        private class ErrorPrivateCTOR : IError
        {
            public string Property { get; init; }
            public string Code { get; init; }
            public string Description { get; init; }

            private ErrorPrivateCTOR(string property, string code, string description) { }

            public bool Equals(IError other) => throw new System.NotImplementedException();
        }

        private class ErrorProtectedCTOR : IError
        {
            public string Property { get; init; }
            public string Code { get; init; }
            public string Description { get; init; }

            protected ErrorProtectedCTOR(string property, string code, string description)
            {
                Property = property;
                Code = code;
                Description = description;
            }

            public bool Equals(IError other) => throw new System.NotImplementedException();
        }

        private class ErrorAlternativeCTOR : IError
        {
            public string Property { get; init; }
            public string Code { get; init; }
            public string Description { get; init; }
            protected ErrorAlternativeCTOR(int _) { }
            public bool Equals(IError other) => throw new System.NotImplementedException();
        }



        [Fact]
        public void BuiltIn_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(ForbiddenError);


            // Act
            var act = ResultReflection.CreateError<ForbiddenError>(type, property, code, description);


            // Assert

            act.Should().IsError<ForbiddenError>(
                property,
                code,
                description);
        }

        [Fact]
        public void WithConstructor_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(CustomError);


            // Act
            var act = ResultReflection.CreateError<CustomError>(type, property, code, description);


            // Assert

            act.Should().IsError<CustomError>(
                property,
                code,
                description);
        }

        [Fact]
        public void WithoutConstructor_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(ErrorWhioutCTOR);


            // Act
            var act = ResultReflection.CreateError<ErrorWhioutCTOR>(type, property, code, description);


            // Assert

            act.Should().IsError<ErrorWhioutCTOR>(
                property,
                code,
                description);
        }

        [Fact]
        public void PrivateConstructor_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(ErrorPrivateCTOR);


            // Act
            var act = ResultReflection.CreateError<ErrorPrivateCTOR>(type, property, code, description);


            // Assert

            act.Should().IsError<ErrorPrivateCTOR>(
                property,
                code,
                description);
        }

        [Fact]
        public void ProtectedConstructor_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(ErrorProtectedCTOR);


            // Act
            var act = ResultReflection.CreateError<ErrorProtectedCTOR>(type, property, code, description);


            // Assert

            act.Should().IsError<ErrorProtectedCTOR>(
                property,
                code,
                description);
        }

        [Fact]
        public void AlternativeConstructor_CreateError_NewInstance()
        {
            // Arrange
            var property = "fakeProp";
            var code = "fakeC";
            var description = "fakeD";

            var type = typeof(ErrorAlternativeCTOR);


            // Act
            var act = ResultReflection.CreateError<ErrorAlternativeCTOR>(type, property, code, description);


            // Assert

            act.Should().IsError<ErrorAlternativeCTOR>(
                property,
                code,
                description);
        }
    }
}
