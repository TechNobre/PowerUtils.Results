using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using PowerUtils.Results.Tests.Fakes;
using Xunit;

namespace PowerUtils.Results.Tests.Errors.CustomErrors
{
    public class JsonSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyCustomErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<CustomError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<CustomError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyCustomErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<CustomError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<CustomError[]>();
            }
        }

        [Fact]
        public void String_Deserialize_CustomError()
        {
            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.Tests.Fakes.CustomError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<CustomError>(json);


            // Assert
            act.Should().IsError<CustomError>(
                "prop",
                "CODE",
                "Disc");
        }

        [Fact]
        public void CustomError_Serialize_JsonStringWithTypePropertyCodeAndDescription()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = new CustomError(property, code, description);

            var type = error.GetType().FullName;
            var expected = $"{{\"_type\":\"{type}\",\"Property\":\"{property}\",\"Code\":\"{code}\",\"Description\":\"{description}\"}}";


            // Act
            var act = JsonSerializer.Serialize(error);


            // Assert
            act.Should().Be(expected);
        }

        [Fact]
        public void CustomError_SerializeDeserialize_SameOriginalError()
        {
            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var error = new CustomError(property, code, description);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<CustomError>(serialization);


            // Assert
            act.Should().IsError<CustomError>(
                property,
                code,
                description);
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void CustomError_DeserializeUsingIError_SameOriginalError()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property = "prop";
            var code = "cod";
            var description = "disc";

            var serialization = JsonSerializer.Serialize(
                new CustomError(property, code, description));


            // Act
            var act = JsonSerializer.Deserialize<IError>(serialization);


            // Assert
            act.Should().IsError<CustomError>(
                property,
                code,
                description);
        }
#endif
    }
}
