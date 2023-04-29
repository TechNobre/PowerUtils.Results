using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PowerUtils.Results.Serializers
{
    internal static class JsonSerializerUtils
    {
        internal const string TYPE_NAME = "_type";
        internal const string ERRORS_NAME = nameof(IResult.Errors);
        internal const string VALUE_NAME = "Value";

        internal static readonly JsonEncodedText JSON_PROPERTY_NAME = JsonEncodedText.Encode(nameof(IError.Property));
        internal static readonly JsonEncodedText JSON_CODE_NAME = JsonEncodedText.Encode(nameof(IError.Code));
        internal static readonly JsonEncodedText JSON_DESCRIPTION_NAME = JsonEncodedText.Encode(nameof(IError.Description));

        internal static readonly JsonEncodedText JSON_ISSUCCESS_NAME = JsonEncodedText.Encode(nameof(IResult.IsSuccess));
        internal static readonly JsonEncodedText JSON_ERRORS_NAME = JsonEncodedText.Encode(ERRORS_NAME);
        internal static readonly JsonEncodedText JSON_VALUE_NAME = JsonEncodedText.Encode(VALUE_NAME);



        internal static (Type? Type, string? Property, string? Code, string? Description) ReadError(ref Utf8JsonReader reader)
        {
            Type? type = null;
            string? property = null;
            string? code = null;
            string? description = null;

            while(reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
            {
                if(reader.TokenType is JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    if(TYPE_NAME.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        type = ResultReflection.TryGetErrorType(reader.GetString()!);
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

            return (type, property, code, description);
        }



        internal static List<IError> ReadErrors(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
#if NET6_0_OR_GREATER
            var errors = JsonSerializer.Deserialize(ref reader, typeof(List<IError>), options) as List<IError>;
#else
            var errors = new List<IError>();
            while(reader.Read() && reader.TokenType is not JsonTokenType.EndArray)
            {
                (var type, var property, var code, var description) = JsonSerializerUtils.ReadError(ref reader);

                var error = ResultReflection.CreateError<IError>(type, property, code, description);
                errors.Add(error);
            }
#endif
            return errors!;
        }
    }
}
