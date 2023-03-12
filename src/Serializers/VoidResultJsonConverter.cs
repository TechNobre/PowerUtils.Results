using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUtils.Results.Serializers
{
    internal sealed class VoidResultJsonConverter : JsonConverter<Result>
    {
        public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException("Unexpected start when reading JSON");
            }

            List<IError> errors = null;
            while(reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
            {
                if(reader.TokenType is JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if(JsonSerializerUtils.ERRORS_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
#if NET6_0_OR_GREATER
                        errors = JsonSerializer.Deserialize(ref reader, typeof(List<IError>), options) as List<IError>;
#else
                        errors = new List<IError>();
                        while(reader.Read() && reader.TokenType is not JsonTokenType.EndArray)
                        {
                            (var type, var property, var code, var description) = JsonSerializerUtils.ReadError(ref reader);

                            var error = ResultReflection.CreateError<IError>(type, property, code, description);
                            errors.Add(error);
                        }
#endif
                    }
                }
            }

            if(reader.TokenType is not JsonTokenType.EndObject)
            {
                throw new JsonException("Unexpected end when reading JSON");
            }

            return errors;
        }

        public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteBoolean(JsonSerializerUtils.JSON_ISSUCCESS_NAME, value.IsSuccess);

            if(value.IsError)
            {
                writer.WritePropertyName(JsonSerializerUtils.JSON_ERRORS_NAME);
                writer.WriteStartArray();

                foreach(var error in value.Errors)
                {
                    JsonSerializer.Serialize(writer, error, error.GetType(), options);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
