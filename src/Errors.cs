using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using PowerUtils.Results.Serializers;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif

namespace PowerUtils.Results
{
    /// <summary>
    /// Defines a error type
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonConverter(typeof(ErrorJsonConverter<IError>))]
#endif
    public interface IError :
        IType,
        IEquatable<IError>
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
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<Error>))]
#if NET6_0_OR_GREATER
    public readonly partial record struct Error : IError
#else
    public readonly partial struct Error : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<Error, Error, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(Error? left, Error? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(Error? left, Error? right)
            => !(left == right);

        public override int GetHashCode() => HashCode.Combine(Property, Code, Description);
    }



    /// <summary>
    /// Error type similar to a Unauthorized:401
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<UnauthorizedError>))]
#if NET6_0_OR_GREATER
    public readonly record struct UnauthorizedError : IError
#else
    public readonly struct UnauthorizedError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<UnauthorizedError, UnauthorizedError, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(UnauthorizedError? left, UnauthorizedError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(UnauthorizedError? left, UnauthorizedError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a Forbidden:403
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<ForbiddenError>))]
#if NET6_0_OR_GREATER
    public readonly record struct ForbiddenError : IError
#else
    public readonly struct ForbiddenError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<ForbiddenError, ForbiddenError, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(ForbiddenError? left, ForbiddenError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(ForbiddenError? left, ForbiddenError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a NotFound:404
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<NotFoundError>))]
#if NET6_0_OR_GREATER
    public readonly record struct NotFoundError : IError
#else
    public readonly struct NotFoundError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<NotFoundError, NotFoundError, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(NotFoundError? left, NotFoundError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(NotFoundError? left, NotFoundError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a Conflict:409
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<ConflictError>))]
#if NET6_0_OR_GREATER
    public readonly record struct ConflictError : IError
#else
    public readonly struct ConflictError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<ConflictError, ConflictError, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(ConflictError? left, ConflictError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(ConflictError? left, ConflictError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a Validation:400
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<ValidationError>))]
#if NET6_0_OR_GREATER
    public readonly record struct ValidationError : IError
#else
    public readonly struct ValidationError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<ValidationError, ValidationError, bool>
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(ValidationError? left, ValidationError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(ValidationError? left, ValidationError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a Unexpected:500
    /// </summary>
    [DebuggerDisplay(CommonUtils.ERROR_DEBUG_TEMPLATE)]
    [JsonConverter(typeof(ErrorJsonConverter<UnexpectedError>))]
#if NET6_0_OR_GREATER
    public readonly record struct UnexpectedError : IError
#else
    public readonly struct UnexpectedError : IError
#endif

#if NET7_0_OR_GREATER
        , IEqualityOperators<UnexpectedError, UnexpectedError, bool>
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

        public UnexpectedError(string property, string code, string description = null)
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

        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public bool Equals(IError other) => CommonUtils.Equals(this, ref other);

#if NET5_0
        /// <summary>
        /// Returns TRUE if the both errors is equals
        /// </summary>
        public override bool Equals(object obj)
            => Equals(obj as IError);
#endif

        public static bool operator ==(UnexpectedError? left, UnexpectedError? right)
            => CommonUtils.Equals(left, right);

        public static bool operator !=(UnexpectedError? left, UnexpectedError? right)
            => !(left == right);

        public override int GetHashCode() => base.GetHashCode();
    }
}
