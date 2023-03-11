using System.Text.Json.Serialization;
using PowerUtils.Results.Serializers;

namespace PowerUtils.Results.Tests.Fakes
{
    [JsonConverter(typeof(ErrorJsonConverter<CustomError>))]
    public class CustomError : IError
    {
        public string Property { get; init; }
        public string Code { get; init; }
        public string Description { get; init; }

        public CustomError(string property, string code, string description)
        {
            Property = property;
            Code = code;
            Description = description;
        }

        public bool Equals(IError other) => throw new System.NotImplementedException();
    }
}
