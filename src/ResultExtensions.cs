using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Add error in result
        /// </summary>
        public static void AddError(this IResult result, string property, string code, string description = null)
            => result.AddError(new Error(property, code, description));

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

        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action
        /// </summary>
        public static void Switch(this Result result, Action onSuccess, Action<IEnumerable<IError>> onErrors)
        {
            if(result.IsError)
            {
                onErrors(result.Errors);
                return;
            }

            onSuccess();
        }

        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action with value
        /// </summary>
        public static void Switch<TValue>(this Result<TValue> result, Action<TValue> onSuccess, Action<IEnumerable<IError>> onErrors)
        {
            if(result.IsError)
            {
                onErrors(result.Errors);
                return;
            }

            onSuccess(result.Value);
        }

        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the onSuccess action
        /// </summary>
        public static void SwitchFirst(this Result result, Action onSuccess, Action<IError> onError)
        {
            if(result.IsError)
            {
                onError(result.Errors.First());
                return;
            }

            onSuccess();
        }

        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the OnSuccess action with value
        /// </summary>
        public static void SwitchFirst<TValue>(this Result<TValue> result, Action<TValue> onSuccess, Action<IError> onError)
        {
            if(result.IsError)
            {
                onError(result.Errors.First());
                return;
            }

            onSuccess(result.Value);
        }
    }
}
