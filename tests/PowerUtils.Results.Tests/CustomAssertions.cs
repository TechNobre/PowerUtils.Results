using System.Linq;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;

namespace PowerUtils.Results.Tests
{
    internal static class CustomAssertions
    {
        public static void ContainsError<TError>(this GenericCollectionAssertions<IError> assertions, string property, string code, string description)
            where TError : IError
            => assertions.ContainSingle(error =>
                error.Property == property &&
                error.Code == code &&
                error.Description == description &&
                error.GetType() == typeof(TError));

        public static void ContainsError<TError>(this ObjectAssertions assertions, string property, string code, string description)
            where TError : IError
            => assertions.Match<IResult>(result =>
                result.Errors.Any(error =>
                    error.Property == property &&
                    error.Code == code &&
                    error.Description == description &&
                    error.GetType() == typeof(TError))
                && result.IsError == true);

        public static void IsError<TError>(this ObjectAssertions assertions, string property, string code, string description)
            where TError : IError
            => assertions.Match<IError>(error =>
                error.Property == property &&
                error.Code == code &&
                error.Description == description &&
                error.GetType() == typeof(TError));

    }
}
