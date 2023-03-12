using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.VoidResults
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void SuccessResult_Serialize_JsonStringWithSuccessTrue()
        {
            // Arrange
            var result = Result.Success();


            // Act
            var act = JsonSerializer.Serialize(result);


            // Assert
            act.Should().Be("{\"IsSuccess\":true}");
        }

        [Fact]
        public void IsSuccessTrue_Deserialize_Success()
        {
            // Arrange
            var json = @"{
                ""IsSuccess"": true
            }";


            // Act
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            act.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void EmptyArrayJsonString_Deserialize_JsonException()
        {
            // Arrange
            var json = "[]";


            // Act
            var act = Record.Exception(() => JsonSerializer.Deserialize<Result>(json));


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<JsonException>();
                act.Message.Should().Be("Unexpected start when reading JSON");
            }
        }


        [Fact]
        public void EmptyJsonString_Deserialize_Success()
        {
            // Arrange
            var json = "{}";


            // Act
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            act.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Success_SerializeDeserialize_IsSuccessTrue()
        {
            // Arrange
            var result = Result.Success();


            // Act
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            act.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Errors_SerializeDeserialize_SameErrors()
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
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            using(new AssertionScope())
            {
                act.Should().ContainsError<Error>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<Error>(
                    property2,
                    code2,
                    description2);
            }
        }

        [Fact]
        public void DifferentErrorTypes_SerializeDeserialize_SameErrors()
        {
            // Arrange
            var property1 = "notFoundProperty";
            var code1 = "notFoundCode";
            var description1 = "notFoundDescription";

            var property2 = "unauthorizedProperty";
            var code2 = "unauthorizedCode";
            var description2 = "unauthorizedDescription";

            Result result = new List<IError>()
            {
                Error.NotFound(property1, code1, description1),
                Error.Unauthorized(property2, code2, description2)
            };


            // Act
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            using(new AssertionScope())
            {
                act.Should().ContainsError<NotFoundError>(
                    property1,
                    code1,
                    description1);

                act.Should().ContainsError<UnauthorizedError>(
                    property2,
                    code2,
                    description2);
            }
        }

        [Fact]
        public void Error_SerializeDeserialize_SameError()
        {
            // Arrange
            var property = "client";
            var code = "NOT_FOUND";
            var description = "Client not found";

            Result result = Error.NotFound(property, code, description);


            // Act
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result>(json);


            // Assert
            act.Should().ContainsError<NotFoundError>(
                property,
                code,
                description);
        }
    }
}
