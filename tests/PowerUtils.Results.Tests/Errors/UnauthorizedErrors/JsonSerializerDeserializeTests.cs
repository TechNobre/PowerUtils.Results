using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnauthorizedErrors
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyUnauthorizedErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<UnauthorizedError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<UnauthorizedError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyUnauthorizedErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<UnauthorizedError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<UnauthorizedError[]>();
            }
        }

        [Fact]
        public void String_Deserialize_UnauthorizedError()
        {
            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.UnauthorizedError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<UnauthorizedError>(json);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                "prop",
                "CODE",
                "Disc");
        }

        [Fact]
        public void UnauthorizedError_Serialize_JsonStringWithTypePropertyCodeAndDescription()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Unauthorized(property, code, description);

            var type = error.GetType().FullName;
            var expected = $"{{\"_type\":\"{type}\",\"Property\":\"{property}\",\"Code\":\"{code}\",\"Description\":\"{description}\"}}";


            // Act
            var act = JsonSerializer.Serialize(error);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void UnauthorizedError_SerializeDeserialize_SameOriginalError()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Unauthorized(property, code, description);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<UnauthorizedError>(serialization);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                property,
                code,
                description);
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void UnauthorizedError_DeserializeUsingIError_SameOriginalError()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var serialization = JsonSerializer.Serialize(
                Error.Unauthorized(property, code, description));


            // Act
            var act = JsonSerializer.Deserialize<IError>(serialization);


            // Assert
            act.Should().IsError<UnauthorizedError>(
                property,
                code,
                description);
        }
#endif
    }
}
