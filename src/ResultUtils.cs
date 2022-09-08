using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
#if NET6_0_OR_GREATER
    public partial record struct Result : IResult
#else
    public partial record Result : IResult
#endif
    {
        /// <summary>
        /// Creates a success result
        /// </summary>
        public static Result Ok() => new();

        /// <summary>
        /// Creates a success result
        /// </summary>
        public static Result<TValue> Ok<TValue>(TValue value) => Result<TValue>.Ok(value);

        /// <summary>
        /// Creates an <see cref="Result"/> from an <see cref="IError"/>
        /// </summary>
        public static Result From(IError error) => new(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="IError"/>
        /// </summary>
        public static Result<TValue> From<TValue>(IError error) => Result<TValue>.From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> if <see cref="IEnumerable{IError}"/> is empty or null. If not create an error result
        /// </summary>
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
