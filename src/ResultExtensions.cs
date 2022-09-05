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
        /// Gets the first error, if does not contain errors return null
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
        /// Gets the last error
        /// </summary>
        public static IError LastError(this IResult result)
            => result.Errors.Last();

        /// <summary>
        /// Gets the last error, if does not contain errors return null
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
        /// Gets the error when only exists one
        /// </summary>
        public static IError SingleError(this IResult result)
            => result.Errors.Single();

        /// <summary>
        /// Gets the error when only exists one, if does not contain errors return null
        /// </summary>
        public static IError SingleOrDefaultError(this IResult result)
        {
            if(!result.IsError)
            {
                return null;
            }

            return result.Errors.SingleOrDefault();
        }
    }
}
