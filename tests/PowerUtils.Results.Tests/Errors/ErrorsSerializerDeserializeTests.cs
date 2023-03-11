using System;
using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace PowerUtils.Results.Tests.Errors
{
    public class ErrorsSerializerDeserializeTests
    {
        [Fact]
        public void EmptyArrayString_DeserializeToIErrorList_EmptyIErrorList()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<List<IError>>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<List<IError>>();
            }
        }

        [Fact]
        public void EmptyArrayString_DeserializeToIErrorArray_EmptyIErrorArray()
        {
            // Arrange
            var serialization = "[]";


            // Act
            var act = JsonSerializer.Deserialize<IError[]>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeEmpty();
                act.Should().BeOfType<IError[]>();
            }
        }


        [Fact]
        public void StringWithoutType_Deserialize_TypeLoadException()
        {
            // Arrange
            var json = @"{
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = Record.Exception(() => JsonSerializer.Deserialize<Error>(json));


            // Assert
            using(new AssertionScope())
            {
                act.Should().BeOfType<TypeLoadException>();
                act.Message.Should().Be($"Could not load type '{typeof(Error).FullName}'.");
            }
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void String_Deserialize_IError()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var json = @"{
                ""_type"": ""PowerUtils.Results.ValidationError"",
                ""Property"": ""prop"",
                ""Code"": ""CODE"",
                ""Description"": ""Disc""
            }";


            // Act
            var act = JsonSerializer.Deserialize<IError>(json);


            // Assert
            act.Should().IsError<ValidationError>(
                "prop",
                "CODE",
                "Disc");
        }
#endif

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void PropertiesWithoutValues_SerializeDeserialize_ErrorWithoutValues(string expected)
        {
            // Arrange
            var error = new Error(expected, expected, expected);


            // Act
            var serialization = JsonSerializer.Serialize(error);
            var act = JsonSerializer.Deserialize<Error>(serialization);


            // Assert
            using(new AssertionScope())
            {
                act.Property.Should().Be(expected);
                act.Code.Should().Be(expected);

                // The description never is null, because the constructor always set a value using the 'BuildErrorDescription'
                act.Description.Should().NotBeNullOrWhiteSpace();
            }
        }

#if NET6_0_OR_GREATER
        [Fact]
        public void IErrorListWithDifferentErrorTypes_SerializeDeserialize_SameErrors()
        {
            // The System.Text.Json serializer does not support deserialization of interface types
            //      Exception returned -> 'System.NotSupportedException : Deserialization of interface types is not supported...'


            // Arrange
            var property1 = "notFoundProperty";
            var code1 = "notFoundCode";
            var description1 = "notFoundDescription";

            var property2 = "unauthorizedProperty";
            var code2 = "unauthorizedCode";
            var description2 = "unauthorizedDescription";

            var errors = new List<IError>()
            {
                Error.NotFound(property1, code1, description1),
                Error.Unauthorized(property2, code2, description2)
            };


            // Act
            var serialization = JsonSerializer.Serialize(errors);
            var act = JsonSerializer.Deserialize<List<IError>>(serialization);


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
#endif
    }
}
