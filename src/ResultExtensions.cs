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
    }
}
