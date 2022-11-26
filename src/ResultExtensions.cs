using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public static bool IsSuccess(this IResult result) => result.IsSuccess;


        /// <summary>
        /// Checks if it is a successful result and deconstructs <see cref="List{IError}"/>
        /// </summary>
        public static bool IsSuccess(this Result result, out List<IError> errors)
        {
            if(result.IsSuccess)
            {
                errors = new();
                return true;
            }

            errors = result.Errors.AsList();
            return false;
        }

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
        /// Checks if it is a successful result and deconstructs <see cref="Result{TValue}"/> to value and <see cref="List{IError}"/>
        /// </summary>
        public static bool IsSuccess<TValue>(this Result<TValue> result, out TValue value, out List<IError> errors)
        {
            (value, errors) = result;
            return result.IsSuccess;
        }


        /// <summary>
        /// Checks if it is an error result and deconstructs <see cref="List{IError}"/>
        /// </summary>
        public static bool IsError(this Result result, out List<IError> errors)
            => !result.IsSuccess(out errors);

        /// <summary>
        /// Checks if it is an error result and deconstructs <see cref="Result{TValue}"/> to value and <see cref="List{IError}"/>
        /// </summary>
        public static bool IsError<TValue>(this Result<TValue> result, out TValue value, out List<IError> errors)
            => !result.IsSuccess(out value, out errors);


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
            if(result.IsSuccess)
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
            if(result.IsSuccess)
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
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task Switch(this Task<Result> result, Action onSuccess, Action<IEnumerable<IError>> onErrors)
            => (await result).Switch(onSuccess, onErrors);



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
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action with value
        /// </summary>
        public static async Task Switch<TValue>(this Task<Result<TValue>> result, Action<TValue> onSuccess, Action<IEnumerable<IError>> onErrors)
            => (await result).Switch(onSuccess, onErrors);



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
        /// Run the onError action with first error if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task SwitchFirst(this Task<Result> result, Action onSuccess, Action<IError> onError)
            => (await result).SwitchFirst(onSuccess, onError);



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


        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the OnSuccess action with value
        /// </summary>
        public static async Task SwitchFirst<TValue>(this Task<Result<TValue>> result, Action<TValue> onSuccess, Action<IError> onError)
            => (await result).SwitchFirst(onSuccess, onError);



        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task SwitchAsync(this Result result, Func<Task> onSuccess, Func<IEnumerable<IError>, Task> onErrors)
        {
            if(result.IsError)
            {
                await onErrors(result.Errors).ConfigureAwait(false);
                return;
            }

            await onSuccess().ConfigureAwait(false);
        }

        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task SwitchAsync(this Task<Result> result, Func<Task> onSuccess, Func<IEnumerable<IError>, Task> onErrors)
            => await (await result).SwitchAsync(onSuccess, onErrors);



        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action with value
        /// </summary>
        public static async Task SwitchAsync<TValue>(this Result<TValue> result, Func<TValue, Task> onSuccess, Func<IEnumerable<IError>, Task> onErrors)
        {
            if(result.IsError)
            {
                await onErrors(result.Errors).ConfigureAwait(false);
                return;
            }

            await onSuccess(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Run the onError action with error list if there are errors otherwise run the onSuccess action with value
        /// </summary>
        public static async Task SwitchAsync<TValue>(this Task<Result<TValue>> result, Func<TValue, Task> onSuccess, Func<IEnumerable<IError>, Task> onErrors)
            => await (await result).SwitchAsync(onSuccess, onErrors);



        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task SwitchFirstAsync(this Result result, Func<Task> onSuccess, Func<IError, Task> onError)
        {
            if(result.IsError)
            {
                await onError(result.Errors.First()).ConfigureAwait(false);
                return;
            }

            await onSuccess().ConfigureAwait(false);
        }

        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the onSuccess action
        /// </summary>
        public static async Task SwitchFirstAsync(this Task<Result> result, Func<Task> onSuccess, Func<IError, Task> onError)
            => await (await result).SwitchFirstAsync(onSuccess, onError);



        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the OnSuccess action with value
        /// </summary>
        public static async Task SwitchFirstAsync<TValue>(this Result<TValue> result, Func<TValue, Task> onSuccess, Func<IError, Task> onError)
        {
            if(result.IsError)
            {
                await onError(result.Errors.First()).ConfigureAwait(false);
                return;
            }

            await onSuccess(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Run the onError action with first error if there are errors otherwise run the OnSuccess action with value
        /// </summary>
        public static async Task SwitchFirstAsync<TValue>(this Task<Result<TValue>> result, Func<TValue, Task> onSuccess, Func<IError, Task> onError)
            => await (await result).SwitchFirstAsync(onSuccess, onError);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static TOutput Match<TOutput>(this Result result, Func<TOutput> onSuccess, Func<IEnumerable<IError>, TOutput> onErrors)
        {
            if(result.IsError)
            {
                return onErrors(result.Errors);
            }

            return onSuccess();
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> Match<TOutput>(this Task<Result> result, Func<TOutput> onSuccess, Func<IEnumerable<IError>, TOutput> onErrors)
            => (await result).Match(onSuccess, onErrors);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static TOutput Match<TValue, TOutput>(this Result<TValue> result, Func<TValue, TOutput> onSuccess, Func<IEnumerable<IError>, TOutput> onErrors)
        {
            if(result.IsError)
            {
                return onErrors(result.Errors);
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> Match<TValue, TOutput>(this Task<Result<TValue>> result, Func<TValue, TOutput> onSuccess, Func<IEnumerable<IError>, TOutput> onErrors)
            => (await result).Match(onSuccess, onErrors);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static TOutput MatchFirst<TOutput>(this Result result, Func<TOutput> onSuccess, Func<IError, TOutput> onError)
        {
            if(result.IsError)
            {
                return onError(result.Errors.First());
            }

            return onSuccess();
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirst<TOutput>(this Task<Result> result, Func<TOutput> onSuccess, Func<IError, TOutput> onError)
            => (await result).MatchFirst(onSuccess, onError);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static TOutput MatchFirst<TValue, TOutput>(this Result<TValue> result, Func<TValue, TOutput> onSuccess, Func<IError, TOutput> onError)
        {
            if(result.IsError)
            {
                return onError(result.Errors.First());
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirst<TValue, TOutput>(this Task<Result<TValue>> result, Func<TValue, TOutput> onSuccess, Func<IError, TOutput> onError)
            => (await result).MatchFirst(onSuccess, onError);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> MatchAsync<TOutput>(this Result result, Func<Task<TOutput>> onSuccess, Func<IEnumerable<IError>, Task<TOutput>> onErrors)
        {
            if(result.IsError)
            {
                return await onErrors(result.Errors).ConfigureAwait(false);
            }

            return await onSuccess().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> MatchAsync<TOutput>(this Task<Result> result, Func<Task<TOutput>> onSuccess, Func<IEnumerable<IError>, Task<TOutput>> onErrors)
            => await (await result).MatchAsync(onSuccess, onErrors);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> MatchAsync<TValue, TOutput>(this Result<TValue> result, Func<TValue, Task<TOutput>> onSuccess, Func<IEnumerable<IError>, Task<TOutput>> onErrors)
        {
            if(result.IsError)
            {
                return await onErrors(result.Errors).ConfigureAwait(false);
            }

            return await onSuccess(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onErrors"/> function
        /// </summary>
        public static async Task<TOutput> MatchAsync<TValue, TOutput>(this Task<Result<TValue>> result, Func<TValue, Task<TOutput>> onSuccess, Func<IEnumerable<IError>, Task<TOutput>> onErrors)
            => await (await result).MatchAsync(onSuccess, onErrors);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirstAsync<TOutput>(this Result result, Func<Task<TOutput>> onSuccess, Func<IError, Task<TOutput>> onError)
        {
            if(result.IsError)
            {
                return await onError(result.Errors.First()).ConfigureAwait(false);
            }

            return await onSuccess().ConfigureAwait(false);
        }


        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirstAsync<TOutput>(this Task<Result> result, Func<Task<TOutput>> onSuccess, Func<IError, Task<TOutput>> onError)
            => await (await result).MatchFirstAsync(onSuccess, onError);



        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirstAsync<TValue, TOutput>(this Result<TValue> result, Func<TValue, Task<TOutput>> onSuccess, Func<IError, Task<TOutput>> onError)
        {
            if(result.IsError)
            {
                return await onError(result.Errors.First()).ConfigureAwait(false);
            }

            return await onSuccess(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the result of the given <paramref name="onSuccess"/> function if the calling Result is a success. Otherwise, it returns the result of the given <paramref name="onError"/> function
        /// </summary>
        public static async Task<TOutput> MatchFirstAsync<TValue, TOutput>(this Task<Result<TValue>> result, Func<TValue, Task<TOutput>> onSuccess, Func<IError, Task<TOutput>> onError)
            => await (await result).MatchFirstAsync(onSuccess, onError);
    }
}
