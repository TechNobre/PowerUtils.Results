using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
#if NET6_0_OR_GREATER
    public partial record struct Result : IResult
#else
    public sealed partial record Result : IResult
#endif
    {
        /// <summary>
        /// Gets a value indicating whether the state is error
        /// </summary>
        public bool IsError { get; private set; }


        private List<IError> _errors;
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

                return _errors;
            }
        }


        /// <summary>
        /// Creates a success result
        /// </summary>
        public Result()
        {
            IsError = false;
            _errors = default;
        }

        /// <summary>
        /// Creates an error result with the given error
        /// </summary>
        private Result(IError error)
        {
            IsError = true;
            _errors = new List<IError> { error };
        }

        /// <summary>
        /// Creates an error result with the given errors
        /// </summary>
        private Result(List<IError> errors)
        {
            _errors = errors;
            if(_errors is null || _errors.Count == 0)
            {
                IsError = false;
            }
            else
            {
                IsError = true;
            }
        }

        /// <summary>
        /// Add error in result
        /// </summary>
        public void AddError(IError error)
        {
            IsError = true;

            _errors ??= new List<IError>();

            _errors.Add(error);
        }

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
        /// Creates an <see cref="Result"/> from a list of errors
        /// </summary>
        public static implicit operator Result(List<Error> errors)
            => new(errors?.Select(s => s as IError).ToList());

        /// <summary>
        /// Creates an <see cref="Result"/> from a array of errors
        /// </summary>
        public static implicit operator Result(Error[] errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result"/> from a list of errors
        /// </summary>
        public static implicit operator Result(List<IError> errors) => new(errors);

        /// <summary>
        /// Creates an <see cref="Result"/> from a array of errors
        /// </summary>
        public static implicit operator Result(IError[] errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result"/> from an error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(Error error) => new(error);
#else
        public static implicit operator Result(Error error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an unauthorized error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(UnauthorizedError error) => new(error);
#else
        public static implicit operator Result(UnauthorizedError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an forbidden error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(ForbiddenError error) => new(error);
#else
        public static implicit operator Result(ForbiddenError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an not found error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(NotFoundError error) => new(error);
#else
        public static implicit operator Result(NotFoundError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an conflict error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(ConflictError error) => new(error);
#else
        public static implicit operator Result(ConflictError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an validation error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(ValidationError error) => new(error);
#else
        public static implicit operator Result(ValidationError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from an unexpected error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(UnexpectedError error) => new(error);
#else
        public static implicit operator Result(UnexpectedError error) => new(error as IError);
#endif
    }
}
