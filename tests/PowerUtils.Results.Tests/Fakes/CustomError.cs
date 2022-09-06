namespace PowerUtils.Results.Tests.Fakes
{
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
    }
}
