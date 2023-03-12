using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.ValidationErrors
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyValidationErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<ValidationError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<ValidationError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyValidationErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<ValidationError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<ValidationError[]>();
            }
        }

        [Fact]
        public void String_Deserialize_ValidationError()
        {
            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.ValidationError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<ValidationError>(json);


            // Assert
            act.Should().IsError<ValidationError>(
                "prop",
                "CODE",
                "Disc");
        }

        [Fact]
        public void ValidationError_Serialize_JsonStringWithTypePropertyCodeAndDescription()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Validation(property, code, description);

            var type = error.GetType().FullName;
            var expected = $"{{\"_type\":\"{type}\",\"Property\":\"{property}\",\"Code\":\"{code}\",\"Description\":\"{description}\"}}";


            // Act
            var act = JsonSerializer.Serialize(error);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void ValidationError_SerializeDeserialize_SameOriginalError()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = Error.Validation(property, code, description);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<ValidationError>(serialization);


            // Assert
            act.Should().IsError<ValidationError>(
                property,
                code,
                description);
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void ValidationError_DeserializeUsingIError_SameOriginalError()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var serialization = JsonSerializer.Serialize(
                Error.Validation(property, code, description));


            // Act
            var act = JsonSerializer.Deserialize<IError>(serialization);


            // Assert
            act.Should().IsError<ValidationError>(
                property,
                code,
                description);
        }
#endif
    }
}
