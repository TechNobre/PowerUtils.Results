using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerUtils.Results.Serializers
{
    public sealed class ErrorJsonConverter<TError> : JsonConverter<TError>
        where TError : IError
    {
        private const string TYPE_NAME = "_type";
        private static readonly JsonEncodedText _property = JsonEncodedText.Encode(nameof(IError.Property));
        private static readonly JsonEncodedText _code = JsonEncodedText.Encode(nameof(IError.Code));
        private static readonly JsonEncodedText _description = JsonEncodedText.Encode(nameof(IError.Description));



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

                    if(TYPE_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = CommonUtils.TryGetErrorType(reader.GetString());
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


            if(type is null)
            {
                throw new TypeLoadException($"Could not load type '{typeof(TError).FullName}'.");
            }

            try
            {
                return (TError)Activator.CreateInstance(type, args: new object[] { property, code, description });
            }
            catch(Exception exception)
            {
                throw new TargetInvocationException($"Could not create new instance for '{type.FullName}'", exception);
            }
        }

        public override void Write(Utf8JsonWriter writer, TError value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(TYPE_NAME);
            writer.WriteStringValue(value.GetType().FullName);

            writer.WriteString(_property, value.Property);
            writer.WriteString(_code, value.Code);
            writer.WriteString(_description, value.Description);

            writer.WriteEndObject();
        }
    }
}
