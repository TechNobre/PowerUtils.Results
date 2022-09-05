namespace PowerUtils.Results
{
#if NET6_0_OR_GREATER
    public readonly partial record struct Error : IError
#else
    public partial record Error : IError
#endif
    {
        /// <summary>
        /// Creates a custom error result with the given error similar to a BadRequest:400
        /// </summary>
        public static Error Failure(string property, string code, string description = null)
            => new(property, code, description);



        /// <summary>
        /// Creates an 'Unauthorized:401' error result with the given error
        /// </summary>
        public static UnauthorizedError Unauthorized(string property, string code, string description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Unauthorized:401' error result with the given error and code: 'UNAUTHORIZED'
        /// </summary>
        public static UnauthorizedError Unauthorized(string property, string description = null)
            => new(property, ErrorCodes.UNAUTHORIZED, description);



        /// <summary>
        /// Creates an 'Forbidden:403' error result with the given error
        /// </summary>
        public static ForbiddenError Forbidden(string property, string code, string description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Forbidden:403' error result with the given error and code: 'FORBIDDEN'
        /// </summary>
        public static ForbiddenError Forbidden(string property, string description = null)
            => new(property, ErrorCodes.FORBIDDEN, description);



        /// <summary>
        /// Creates an 'NotFound:404' error result with the given error
        /// </summary>
        public static NotFoundError NotFound(string property, string code, string description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'NotFound:404' error result with the given error and code: 'NOT_FOUND'
        /// </summary>
        public static NotFoundError NotFound(string property, string description = null)
            => new(property, ErrorCodes.NOT_FOUND, description);



        /// <summary>
        /// Creates an 'Conflict:409' error result with the given error
        /// </summary>
        public static ConflictError Conflict(string property, string code, string description = null)
            => new(property, code, description);

        /// <summary>
        /// Creates an 'Conflict:409' error result with the given error and code: 'DUPLICATED'
        /// </summary>
        public static ConflictError Conflict(string property, string description = null)
            => new(property, ErrorCodes.DUPLICATED, description);

        internal static string BuildErrorDescription(string code, string description)
            => string.IsNullOrWhiteSpace(description) ? $"An error has occurred with code '{code}'" : description;
    }
}
