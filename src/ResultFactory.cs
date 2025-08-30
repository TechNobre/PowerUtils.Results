using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
#if NET6_0_OR_GREATER
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public partial record struct Result : IResult
#else
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public sealed partial record Result : IResult
#endif
    {
        /// <summary>
        /// Creates a success result
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result Ok() => new();

        /// <summary>
        /// Creates a success result
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result<TValue> Ok<TValue>(TValue value) => Result<TValue>.Ok(value);

        /// <summary>
        /// Creates a success result
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result Success() => new();

        /// <summary>
        /// Creates a success result
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Ok(value);

        /// <summary>
        /// Creates an <see cref="Result"/> from an <see cref="IError"/>
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result From(IError error) => new(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an <see cref="List{IError}"/>
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result From(List<IError> errors) => new(errors);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="IError"/>
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result<TValue> From<TValue>(IError error) => Result<TValue>.From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="List{IError}"/>
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result<TValue> From<TValue>(List<IError> errors) => Result<TValue>.From(errors);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> if <see cref="IEnumerable{IError}"/> is empty or null. If not create an error result
        /// </summary>
        [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
        public static Result<TValue> Create<TError, TValue>(IEnumerable<TError> errors, Func<TValue> onCreator)
            where TError : IError
        {
            if(errors is null || !errors.Any())
            {
                return onCreator();
            }

            return errors.Select(s => s as IError).ToList();
        }
    }
}
