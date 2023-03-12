using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ConflictErrors
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyConflictErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<ConflictError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<ConflictError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyConflictErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<ConflictError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<ConflictError[]>();
            }
        }

        [Fact]
        public void String_Deserialize_ConflictError()
        {
            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.ConflictError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<ConflictError>(json);


            // Assert
            act.Should().IsError<ConflictError>(
                "prop",
                "CODE",
                "Disc");
        }

        [Fact]
        public void ConflictError_Serialize_JsonStringWithTypePropertyCodeAndDescription()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Conflict(property, code, description);

            var type = error.GetType().FullName;
            var expected = $"{{\"_type\":\"{type}\",\"Property\":\"{property}\",\"Code\":\"{code}\",\"Description\":\"{description}\"}}";


            // Act
            var act = JsonSerializer.Serialize(error);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void ConflictError_SerializeDeserialize_SameOriginalError()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Conflict(property, code, description);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<ConflictError>(serialization);


            // Assert
            act.Should().IsError<ConflictError>(
                property,
                code,
                description);
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void ConflictError_DeserializeUsingIError_SameProperties()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var serialization = JsonSerializer.Serialize(
                Error.Conflict(property, code, description));


            // Act
            var act = JsonSerializer.Deserialize<IError>(serialization);


            // Assert
            act.Should().IsError<ConflictError>(
                property,
                code,
                description);
        }
#endif
    }
}
