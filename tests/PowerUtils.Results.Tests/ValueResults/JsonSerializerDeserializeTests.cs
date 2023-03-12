using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.ValueResults
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void ValueResult_Serialize_JsonStringWithSuccessTrueAndValue()
        {
            // Arrange
            var value = "Erat erat et facilisis lorem ut consetetur dolore dolore stet rebum adipiscing et ea ipsum justo amet diam lorem delenit";
            var result = Result.Success(value);

            var expected = $"{{\"IsSuccess\":true,\"Value\":\"{value}\"}}";


            // Act
            var act = JsonSerializer.Serialize(result);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void BoolTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(bool);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<bool>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }


        [Fact]
        public void IntTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(int);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<int>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }

        [Fact]
        public void DoubleTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(double);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<double>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }

        [Fact]
        public void StringTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(string);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<string>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }

        [Fact]
        public void ClassTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(FakeModel);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<FakeModel>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }

        [Fact]
        public void StructTypeSuccessWithoutValue_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var defaultValue = default(FakeStruct);
            var json = "{\"IsSuccess\":true}";


            // Act
            var act = JsonSerializer.Deserialize<Result<FakeStruct>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(defaultValue);
            }
        }

        [Fact]
        public void EmptyArrayJsonString_Deserialize_JsonException()
        {
            // Arrange
            var json = "[]";


            // Act
            var act = Record.Exception(() => JsonSerializer.Deserialize<Result<FakeCustomer>>(json));


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<JsonException>();
                act.Message.Should().Be("Unexpected start when reading JSON");
            }
        }

        [Fact]
        public void EmptyJsonString_Deserialize_SuccessAndDefaultValue()
        {
            // Arrange
            var json = "{}";


            // Act
            var act = JsonSerializer.Deserialize<Result<ulong>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(default);
            }
        }

        [Fact]
        public void Success_SerializeDeserialize_IsSuccessTrue()
        {
            // Arrange
            Result<string> result = Result.Success();


            // Act
            var serialization = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result<string>>(serialization);


            // Assert
            act.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void PrimitiveValue_SerializeDeserialize_SameValue()
        {
            // Arrange
            var value = 46.54;
            Result<double> result = value;


            // Act
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result<double>>(json);


            // Assert
            using(new AssertionScope())
            {
                act.IsSuccess.Should().BeTrue();
                act.Value.Should().Be(value);
            }
        }

        [Fact]
        public void ModelValue_SerializeDeserialize_SameValue()
        {
            // Arrange
            var value = new FakeCustomer("Nelson", 25);
            Result<FakeCustomer> result = value;


            // Act
            var serialization = JsonSerializer.Serialize(result, new JsonSerializerOptions { IncludeFields = true });
            var act = JsonSerializer.Deserialize<Result<FakeCustomer>>(serialization);


            // Assert
            using(new AssertionScope(serialization))
            {
                act.IsSuccess.Should().BeTrue();

                act.Value.Name.Should().Be(value.Name);
                act.Value.Age.Should().Be(value.Age);
            }
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

            Result<FakeCustomer> result = new List<Error>()
            {
                new(property1, code1, description1),
                new(property2, code2, description2)
            };


            // Act
            var json = JsonSerializer.Serialize(result);
            var act = JsonSerializer.Deserialize<Result<FakeCustomer>>(json);


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
        public void DifferentErrorTypes_Deserialize_SameErrors()
        {
            // Arrange
            var property1 = "notFoundProperty";
            var code1 = "notFoundCode";
            var description1 = "notFoundDescription";

            var property2 = "unauthorizedProperty";
            var code2 = "unauthorizedCode";
            var description2 = "unauthorizedDescription";

            Result<FakeCustomer> result = new List<IError>()
            {
                Error.NotFound(property1, code1, description1),
                Error.Unauthorized(property2, code2, description2)
            };

            var serialization = JsonSerializer.Serialize(result);


            // Act
            var act = JsonSerializer.Deserialize<Result<FakeCustomer>>(serialization);


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

            Result<string> result = Error.NotFound(property, code, description);


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
