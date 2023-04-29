using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using PowerUtils.Results.Serializers;

namespace PowerUtils.Results
{
    // The '\"\"' before '_value' is used to avoid the following error: "error CS0173: Type of conditional expression cannot be determined because there is no implicit conversion between 'TValue' and 'string'"
    [DebuggerDisplay("{IsSuccess ? \"\" + _value : \"Errors: \" + _errors.Count}")]
    [JsonConverter(typeof(ValueResultJsonConverter))]
#if NET6_0_OR_GREATER
    public record struct Result<TValue> : IResult
#else
    public sealed record Result<TValue> : IResult
#endif
    {
        /// <summary>
        /// Gets a value indicating whether the state is success
        /// </summary>
        public bool IsSuccess => !IsError;

        /// <summary>
        /// Gets a value indicating whether the state is error
        /// </summary>
        public bool IsError { get; private set; }


        private List<IError>? _errors;
        /// <summary>
        /// Gets the list of errors
        /// </summary>
        public IEnumerable<IError> Errors
        {
            get
            {
                if(IsSuccess)
                {
                    throw new InvalidOperationException("Errors can be retrieved only when the result is an error");
                }

                return _errors!;
            }
        }

        private readonly TValue _value;
        /// <summary>
        /// Gets the value
        /// </summary>
        public TValue Value
        {
            get
            {
                if(IsError)
                {
                    throw new InvalidOperationException("Value can be retrieved only when the result is not an error");
                }

                return _value;
            }
        }



        /// <summary>
        /// Creates a success result
        /// </summary>
        private Result(TValue value)
        {
            _value = value;

            IsError = false;
            _errors = default;
        }

        /// <summary>
        /// Creates an error result with the given error
        /// </summary>
        private Result(IError error)
            : this(new List<IError> { error }) { }

        /// <summary>
        /// Creates an error result with the given errors
        /// </summary>
        private Result(List<IError>? errors)
        {
            _value = default!;

            _errors = errors;
            IsError = CommonUtils.RemoveNulls(ref _errors);
        }



        /// <summary>
        /// Deconstruct <see cref="Result{TValue}"/> to value and <see cref="List{IError}"/>
        /// </summary>
        /// <param name="value">Value in result</param>
        /// <param name="errors">Error list</param>
        public void Deconstruct(out TValue value, out List<IError> errors)
        {
            value = _value;
            errors = _errors ?? new();
        }


        /// <summary>
        /// Add error in result
        /// </summary>
        public void AddError(IError error)
            => IsError = CommonUtils.AddError(ref _errors, ref error);

        /// <summary>
        /// Add errors in result
        /// </summary>
        public void AddErrors(IEnumerable<IError> errors)
            => IsError = CommonUtils.AddErrors(ref _errors, ref errors);

        /// <summary>
        /// Gets the type of the value or type of the first error
        /// </summary>
        public new Type GetType()
        {
            if(IsError)
            {
                return this.OfTypeFirstError();
            }

            return typeof(TValue);
        }

        /// <summary>
        /// Create boolean status with result status (Valid or not valid)
        /// </summary>
        public static implicit operator bool(Result<TValue> result) => !result.IsError;

        /// <summary>
        /// Create an <see cref="List{IError}"/> from a result
        /// </summary>
        public static implicit operator List<IError>?(Result<TValue> result) => result._errors;

        /// <summary>
        /// Creates a success result
        /// </summary>
        public static Result<TValue> Ok(TValue value) => new(value);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a value
        /// </summary>
        public static implicit operator Result<TValue>(TValue value) => new(value);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a list of errors
        /// </summary>
        public static implicit operator Result<TValue>(List<Error>? errors)
            => new(errors?.Select(s => s as IError).ToList());

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a array of errors
        /// </summary>
        public static implicit operator Result<TValue>(Error[]? errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a list of errors
        /// </summary>
        public static implicit operator Result<TValue>(List<IError>? errors) => new(errors);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a array of errors
        /// </summary>
        public static implicit operator Result<TValue>(IError[]? errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an error
        /// </summary>
        public static implicit operator Result<TValue>(Error error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an unauthorized error
        /// </summary>
        public static implicit operator Result<TValue>(UnauthorizedError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an forbidden error
        /// </summary>
        public static implicit operator Result<TValue>(ForbiddenError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an not found error
        /// </summary>
        public static implicit operator Result<TValue>(NotFoundError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an conflict error
        /// </summary>
        public static implicit operator Result<TValue>(ConflictError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an validation error
        /// </summary>
        public static implicit operator Result<TValue>(ValidationError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an unexpected error
        /// </summary>
        public static implicit operator Result<TValue>(UnexpectedError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a <see cref="Result"/>
        /// </summary>

        public static implicit operator Result<TValue>(Result result)
        {
#if NET6_0_OR_GREATER
            if(result.IsError)
#else
            if(result?.IsError is true)
#endif
            {
                return new(result!.Errors.ToList());
            }

            return new List<IError>();
        }

        /// <summary>
        /// Creates an <see cref="Result"/> from a <see cref="Result{TValue}"/>
        /// </summary>
        public static implicit operator Result(Result<TValue> result)
        {
#if NET6_0_OR_GREATER
            if(result.IsError)
#else
            if(result?.IsError is true)
#endif
            {
                return result!.Errors.ToList();
            }

            return new();
        }

        /// <summary>
        /// Creates an value from a <see cref="Result{TValue}"/>
        /// </summary>
        public static implicit operator TValue(Result<TValue> result) => result.Value;

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="IError"/>
        /// </summary>
        public static Result<TValue> From(IError error) => new(error);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="List{IError}"/>
        /// </summary>
        internal static Result<TValue> From(List<IError> errors) => new(errors);
    }
}
