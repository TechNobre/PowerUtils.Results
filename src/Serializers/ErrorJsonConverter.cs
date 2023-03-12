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

            Type type = null;
            string property = null;
            string code = null;
            string description = null;

            while(reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
            {
                if(reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if(SerializerConstants.TYPE_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ResultReflection.TryGetErrorType(reader.GetString());
                    }

                    else if(nameof(IError.Property).Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        property = reader.GetString();
                    }

                    else if(nameof(IError.Code).Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        code = reader.GetString();
                    }

                    else if(nameof(IError.Description).Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        description = reader.GetString();
                    }
                }
            }

            if(reader.TokenType is not JsonTokenType.EndObject)
            {
                throw new JsonException("Unexpected end when reading JSON");
            }

            return ResultReflection.CreateError<TError>(type, property, code, description);
        }

        public override void Write(Utf8JsonWriter writer, TError value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(SerializerConstants.TYPE_NAME);
            writer.WriteStringValue(value.GetType().FullName);

            writer.WriteString(SerializerConstants.PROPERTY_NAME, value.Property);
            writer.WriteString(SerializerConstants.CODE_NAME, value.Code);
            writer.WriteString(SerializerConstants.DESCRIPTION_NAME, value.Description);

            writer.WriteEndObject();
        }
    }
}
