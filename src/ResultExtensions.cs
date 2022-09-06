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



        /// <summary>
        /// Check if the result contains an error
        /// </summary>
        public static bool ContainsError(this IResult result) => result.IsError;

        /// <summary>
        /// Check if the result contains an error from a specific type
        /// </summary>
        public static bool ContainsError<TError>(this IResult result)
            where TError : IError
        {
            if(!result.IsError)
            {
                return false;
            }

            return result.Errors.Any(a => a.GetType() == typeof(TError));
        }

        /// <summary>
        /// Check if the result contains an error from a specific type and with a specific condition
        /// </summary>
        public static bool ContainsError<TError>(this IResult result, Func<IError, bool> predicate)
            where TError : IError
        {
            if(!result.IsError)
            {
                return false;
            }


            foreach(var error in result.Errors)
            {
                if(error.GetType() == typeof(TError) && predicate(error))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
