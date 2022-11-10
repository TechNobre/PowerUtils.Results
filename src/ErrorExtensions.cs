using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
    public static class ErrorExtensions
    {
        /// <summary>
        /// Get Errors as a list
        /// </summary>
        public static List<IError> AsList(this IEnumerable<IError> errors) => errors as List<IError>;


        /// <summary>
        /// Gets the first error
        /// </summary>
        public static IError FirstError(this IResult result)
            => result.Errors.First();

        /// <summary>
        /// Gets the first error, if does not contain errors returns null
        /// </summary>
        public static IError FirstOrDefaultError(this IResult result)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first error that satisfies the condition otherwise returns null
        /// </summary>
        public static IError FirstOrDefaultError(this IResult result, Func<IError, bool> predicate)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets the last error
        /// </summary>
        public static IError LastError(this IResult result)
            => result.Errors.Last();

        /// <summary>
        /// Gets the last error, if does not contain errors returns null
        /// </summary>
        public static IError LastOrDefaultError(this IResult result)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.LastOrDefault();
        }

        /// <summary>
        /// Gets the last error that satisfies the condition otherwise returns null
        /// </summary>
        public static IError LastOrDefaultError(this IResult result, Func<IError, bool> predicate)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.LastOrDefault(predicate);
        }

        /// <summary>
        /// Gets the error when only exists one
        /// </summary>
        public static IError SingleError(this IResult result)
            => result.Errors.Single();

        /// <summary>
        /// Gets the error when only exists one, if does not contain errors returns null
        /// </summary>
        public static IError SingleOrDefaultError(this IResult result)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.SingleOrDefault();
        }

        /// <summary>
        /// Gets the error when only one satisfies the condition otherwise returns null
        /// </summary>
        public static IError SingleOrDefaultError(this IResult result, Func<IError, bool> predicate)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.SingleOrDefault(predicate);
        }
    }
}
