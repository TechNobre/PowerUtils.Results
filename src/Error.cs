using System;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif

namespace PowerUtils.Results
{
    /// <summary>
    /// Defines a error type
    /// </summary>
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


    public interface IError<TSelf> :
        IError
#if NET7_0_OR_GREATER
        , IEqualityOperators<TSelf, TSelf, bool> where TSelf : IEqualityOperators<TSelf, TSelf, bool>
#endif
    { }


    /// <summary>
    /// Generic error
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly partial record struct Error : IError<Error>
#else
    public readonly partial struct Error : IError
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

        public override int GetHashCode() => base.GetHashCode();
    }



    /// <summary>
    /// Error type similar to a Unauthorized:401
    /// </summary>
#if NET6_0_OR_GREATER
    public readonly record struct UnauthorizedError : IError<UnauthorizedError>
#else
    public readonly struct UnauthorizedError : IError
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
#if NET6_0_OR_GREATER
    public readonly record struct ForbiddenError : IError<ForbiddenError>
#else
    public readonly struct ForbiddenError : IError
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
#if NET6_0_OR_GREATER
    public readonly record struct NotFoundError : IError<NotFoundError>
#else
    public readonly struct NotFoundError : IError
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
#if NET6_0_OR_GREATER
    public readonly record struct ConflictError : IError<ConflictError>
#else
    public readonly struct ConflictError : IError
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
#if NET6_0_OR_GREATER
    public readonly record struct ValidationError : IError<ValidationError>
#else
    public readonly struct ValidationError : IError
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
#if NET6_0_OR_GREATER
    public readonly record struct UnexpectedError : IError<UnexpectedError>
#else
    public readonly struct UnexpectedError : IError
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
