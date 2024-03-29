﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using PowerUtils.Results.Serializers;

namespace PowerUtils.Results
{
    [DebuggerDisplay("Errors: {IsSuccess ? 0 : _errors.Count}")]
    [JsonConverter(typeof(VoidResultJsonConverter))]
#if NET6_0_OR_GREATER
    public partial record struct Result : IResult
#else
    public sealed partial record Result : IResult
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
                if(!IsError)
                {
                    throw new InvalidOperationException("Errors can be retrieved only when the result is an error");
                }

                return _errors!;
            }
        }



        /// <summary>
        /// Creates a success result
        /// </summary>
        public Result()
        {
#if NET6_0
            IsError = false;
            _errors = default;
#endif
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
            _errors = errors;
            IsError = CommonUtils.RemoveNulls(ref _errors);
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

            return typeof(Success);
        }

        /// <summary>
        /// Create boolean status with result status (Valid or not valid)
        /// </summary>
        public static implicit operator bool(Result result) => !result.IsError;

        /// <summary>
        /// Create an <see cref="List{IError}"/> from a result
        /// </summary>
        public static implicit operator List<IError>?(Result result) => result._errors;

        /// <summary>
        /// Creates a success result
        /// </summary>
        public static implicit operator Result(Success _) => new();

        /// <summary>
        /// Creates an <see cref="Result"/> from a list of errors
        /// </summary>
        public static implicit operator Result(List<Error>? errors)
            => new(errors?.Select(s => s as IError).ToList());

        /// <summary>
        /// Creates an <see cref="Result"/> from a array of errors
        /// </summary>
        public static implicit operator Result(Error[]? errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result"/> from a list of errors
        /// </summary>
        public static implicit operator Result(List<IError>? errors) => new(errors);

        /// <summary>
        /// Creates an <see cref="Result"/> from a array of errors
        /// </summary>
        public static implicit operator Result(IError[]? errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result"/> from an error
        /// </summary>
        public static implicit operator Result(Error error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an unauthorized error
        /// </summary>
        public static implicit operator Result(UnauthorizedError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an forbidden error
        /// </summary>
        public static implicit operator Result(ForbiddenError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an not found error
        /// </summary>
        public static implicit operator Result(NotFoundError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an conflict error
        /// </summary>
        public static implicit operator Result(ConflictError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an validation error
        /// </summary>
        public static implicit operator Result(ValidationError error) => From(error);

        /// <summary>
        /// Creates an <see cref="Result"/> from an unexpected error
        /// </summary>
        public static implicit operator Result(UnexpectedError error) => From(error);
    }
}
