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
        /// Gets the last error
        /// </summary>
        public static IError LastError(this IResult result)
            => result.Errors.Last();

        /// <summary>
        /// Gets the error when only exists one
        /// </summary>
        public static IError SingleError(this IResult result)
            => result.Errors.Single();
    }
}
