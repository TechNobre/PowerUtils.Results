namespace PowerUtils.Results
{
    /// <summary>
    /// Basic structure of an error
    /// </summary>
    public interface IError : IType
    {
        /// <summary>
        /// Property with error
        /// </summary>
        string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        string Description { get; init; }
    }



    /// <summary>
    /// Generic error
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly partial record struct Error : IError
#else
    public partial record Error : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public Error(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }



    /// <summary>
    /// Error type similar to a Unauthorized:401
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct UnauthorizedError : IError
#else
    public record UnauthorizedError : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public UnauthorizedError(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = Error.BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }



    /// <summary>
    /// Error type similar to a Forbidden:403
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct ForbiddenError : IError
#else
    public record ForbiddenError : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public ForbiddenError(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = Error.BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }



    /// <summary>
    /// Error type similar to a NotFound:404
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct NotFoundError : IError
#else
    public record NotFoundError : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public NotFoundError(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = Error.BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }



    /// <summary>
    /// Error type similar to a Conflict:409
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct ConflictError : IError
#else
    public record ConflictError : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public ConflictError(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = Error.BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }



    /// <summary>
    /// Error type similar to a Validation:400
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct ValidationError : IError
#else
    public record ValidationError : IError
#endif
    {
        /// <summary>
        /// Property with error
        /// </summary>
        public string Property { get; init; }

        /// <summary>
        /// Code that defines the error
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description
        /// </summary>
        public string Description { get; init; }

        public ValidationError(string property, string code, string description = null)
        {
            Property = property;
            Code = code;

            Description = Error.BuildErrorDescription(Code, description);
        }

        /// <summary>
        /// Gets properties of the error
        /// </summary>
        public void Deconstruct(out string property, out string code, out string description)
        {
            property = Property;
            code = Code;
            description = Description;
        }
    }
}
