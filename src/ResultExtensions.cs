using System;
using System.Linq;

namespace PowerUtils.Results
{
    public static class ResultExtensions
    {
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
                return null;
            }

            return result.Errors.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first error that satisfies the condition otherwise returns null
        /// </summary>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
                return null;
            }

            return result.Errors.LastOrDefault();
        }

        /// <summary>
        /// Gets the last error that satisfies the condition otherwise returns null
        /// </summary>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
                return null;
            }

            return result.Errors.SingleOrDefault();
        }

        public static IError SingleOrDefaultError(this IResult result, Func<IError, bool> predicate)
        {
            if(!result.IsError)
            {
                return default;
            }

            return result.Errors.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Gets the type of the first error
        /// </summary>
        public static Type OfTypeFirstError(this IResult result)
            => result.Errors.First().GetType();
    }
}
