using System.Text.Json;

namespace PowerUtils.Results.Serializers
{
    internal static class SerializerConstants
    {
        internal const string TYPE_NAME = "_type";

        internal static readonly JsonEncodedText PROPERTY_NAME = JsonEncodedText.Encode(nameof(IError.Property));
        internal static readonly JsonEncodedText CODE_NAME = JsonEncodedText.Encode(nameof(IError.Code));
        internal static readonly JsonEncodedText DESCRIPTION_NAME = JsonEncodedText.Encode(nameof(IError.Description));
    }
}
