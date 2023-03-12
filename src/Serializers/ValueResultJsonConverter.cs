using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUtils.Results.Serializers
{
    internal sealed class ValueResultJsonConverter : JsonConverterFactory
    {
        // It is not being validated if it is possible to create a valid `JsonConverter`, because this converter is `internal`
        //   and we have full control which is only used to serilize and decirilize `Result<TValue>` objects
        public override bool CanConvert(Type typeToConvert) => true;

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            var valueType = type.GetGenericArguments()[0];

            return (JsonConverter)Activator.CreateInstance(
                typeof(ValueResultJsonSerializer<>).MakeGenericType(new Type[] { valueType }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);
        }
    }

    internal sealed class ValueResultJsonSerializer<TValue> : JsonConverter<Result<TValue>>
    {
        private readonly JsonConverter<TValue> _valueConverter;
        private readonly Type _valueType;

        public ValueResultJsonSerializer(JsonSerializerOptions options)
        {
            // For performance, use the existing converter.
            _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));

            // Cache the value type.
            _valueType = typeof(TValue);
        }

        public override Result<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException("Unexpected start when reading JSON");
            }

            List<IError> errors = null;
            TValue value = default;
            var isError = false;

            while(reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
            {
                if(reader.TokenType is JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if(JsonSerializerUtils.ERRORS_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isError = true;
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

                    if(JsonSerializerUtils.VALUE_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isError = false;
                        value = _valueConverter.Read(ref reader, _valueType, options);
                    }
                }
            }

            if(isError)
            {
                return errors;
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, Result<TValue> value, JsonSerializerOptions options)
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
            else
            {
                writer.WritePropertyName(JsonSerializerUtils.JSON_VALUE_NAME);
                _valueConverter.Write(writer, value.Value, options);
            }

            writer.WriteEndObject();
        }
    }
}
