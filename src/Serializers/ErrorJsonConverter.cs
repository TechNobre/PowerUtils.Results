using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUtils.Results.Serializers
{
    public sealed class ErrorJsonConverter<TError> : JsonConverter<TError>
        where TError : IError
    {
        public override TError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException("Unexpected start when reading JSON");
            }

            (var type, var property, var code, var description) = JsonSerializerUtils.ReadError(ref reader);

            return ResultReflection.CreateError<TError>(type, property, code, description);
        }

        public override void Write(Utf8JsonWriter writer, TError value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(JsonSerializerUtils.TYPE_NAME);
            writer.WriteStringValue(value.GetType().FullName);

            writer.WriteString(JsonSerializerUtils.JSON_PROPERTY_NAME, value.Property);
            writer.WriteString(JsonSerializerUtils.JSON_CODE_NAME, value.Code);
            writer.WriteString(JsonSerializerUtils.JSON_DESCRIPTION_NAME, value.Description);

            writer.WriteEndObject();
        }
    }
}
