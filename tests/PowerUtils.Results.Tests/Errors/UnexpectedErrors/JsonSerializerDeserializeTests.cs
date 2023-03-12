using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.UnexpectedErrors
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyUnexpectedErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<UnexpectedError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<UnexpectedError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyUnexpectedErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<UnexpectedError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<UnexpectedError[]>();
            }
        }

        [Fact]
        public void String_Deserialize_UnexpectedError()
        {
            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.UnexpectedError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<UnexpectedError>(json);


            // Assert
            act.Should().IsError<UnexpectedError>(
                "prop",
                "CODE",
                "Disc");
        }

        [Fact]
        public void UnexpectedError_Serialize_JsonStringWithTypePropertyCodeAndDescription()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Unexpected(property, code, description);

            var type = error.GetType().FullName;
            var expected = $"{{\"_type\":\"{type}\",\"Property\":\"{property}\",\"Code\":\"{code}\",\"Description\":\"{description}\"}}";


            // Act
            var act = JsonSerializer.Serialize(error);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void UnexpectedError_SerializeDeserialize_SameOriginalError()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Unexpected(property, code, description);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<UnexpectedError>(serialization);


            // Assert
            act.Should().IsError<UnexpectedError>(
                property,
                code,
                description);
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void UnexpectedError_DeserializeUsingIError_SameOriginalError()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var serialization = JsonSerializer.Serialize(
                Error.Unexpected(property, code, description));


            // Act
            var act = JsonSerializer.Deserialize<IError>(serialization);


            // Assert
            act.Should().IsError<UnexpectedError>(
                property,
                code,
                description);
        }
#endif
    }
}
