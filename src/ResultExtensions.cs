using System;
using System.Linq;

namespace PowerUtils.Results
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Gets the type of the first error
        /// </summary>
        public static Type OfTypeFirstError(this IResult result)
            => result.Errors.First().GetType();


        /// <summary>
        /// Check if the result object is success
        /// </summary>
        public static bool IsSuccess(this IResult result) => !result.IsError;

        /// <summary>
        /// Check if the result object is success and contains a value from a specific type with a specific condition
        /// </summary>
        public static bool IsSuccess<TValue>(this Result<TValue> result, Func<TValue, bool> predicate)
        {
            if(result.IsError)
            {
                return false;
            }

            return predicate(result.Value);
        }
    }
}
