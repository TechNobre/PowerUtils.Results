using System;

namespace PowerUtils.Results
{
#if NET6_0_OR_GREATER
    public readonly partial record struct Error : IError
#else
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
    public partial struct Error : IError
#endif
    {
        /// <summary>
        /// Creates a custom error result with the given error similar to a BadRequest:400
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Error Failure(string property, string code, string? description = null)
            => new(property, code, description);



        /// <summary>
        /// Creates an 'Unauthorized:401' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static UnauthorizedError Unauthorized(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Unauthorized:401' error result with the given error and code: 'UNAUTHORIZED'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static UnauthorizedError Unauthorized(string property, string? description = null)
            => new(property, ResultErrorCodes.UNAUTHORIZED, description);



        /// <summary>
        /// Creates an 'Forbidden:403' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ForbiddenError Forbidden(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Forbidden:403' error result with the given error and code: 'FORBIDDEN'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ForbiddenError Forbidden(string property, string? description = null)
            => new(property, ResultErrorCodes.FORBIDDEN, description);



        /// <summary>
        /// Creates an 'NotFound:404' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static NotFoundError NotFound(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'NotFound:404' error result with the given error and code: 'NOT_FOUND'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static NotFoundError NotFound(string property, string? description = null)
            => new(property, ResultErrorCodes.NOT_FOUND, description);



        /// <summary>
        /// Creates an 'Conflict:409' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ConflictError Conflict(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Conflict:409' error result with the given error and code: 'CONFLICT'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ConflictError Conflict(string property, string? description = null)
            => new(property, ResultErrorCodes.CONFLICT, description);



        /// <summary>
        /// Create an 'Validation:400' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ValidationError Validation(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Create an 'Validation:400' error result with the given error and code: 'VALIDATION'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static ValidationError Validation(string property, string? description = null)
            => new(property, ResultErrorCodes.VALIDATION, description);



        /// <summary>
        /// Create an 'Unexpected:500' error result with the given error
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static UnexpectedError Unexpected(string property, string code, string? description = null)
            => new(property, code, description);

        /// <summary>
        /// Create an 'Unexpected:500' error result with the given error and code: 'UNEXPECTED'
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static UnexpectedError Unexpected(string property, string? description = null)
            => new(property, ResultErrorCodes.UNEXPECTED, description);



        internal static string BuildErrorDescription(string code, string? description)
            => string.IsNullOrWhiteSpace(description) ? $"An error has occurred with code '{code}'" : description;
    }
}
