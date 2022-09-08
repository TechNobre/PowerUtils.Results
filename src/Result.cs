using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUtils.Results
{
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether the state is error
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Gets the list of errors
        /// </summary>
        IEnumerable<IError> Errors { get; }

        void AddError(IError error);
    }


#if NET6_0_OR_GREATER
    public partial record struct Result : IResult
#else
    public partial record Result : IResult
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



#if NET6_0_OR_GREATER
    public record struct Result<TValue> : IResult
#else
    public record Result<TValue> : IResult
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
                    throw new InvalidOperationException("Value can be retrieved only when the result is not an error.");
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
        {
            _value = default;

            IsError = true;
            _errors = new List<IError> { error };
        }

        /// <summary>
        /// Creates an error result with the given errors
        /// </summary>
        private Result(List<IError> errors)
        {
            _value = default;

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

            return _value.GetType();
        }

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
        public static implicit operator Result<TValue>(List<Error> errors)
            => new(errors?.Select(s => s as IError).ToList());

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a array of errors
        /// </summary>
        public static implicit operator Result<TValue>(Error[] errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a list of errors
        /// </summary>
        public static implicit operator Result<TValue>(List<IError> errors) => new(errors);

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a array of errors
        /// </summary>
        public static implicit operator Result<TValue>(IError[] errors) => errors?.ToList();

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(Error error) => new(error);
#else
        public static implicit operator Result<TValue>(Error error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an unauthorized error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(UnauthorizedError error) => new(error);
#else
        public static implicit operator Result<TValue>(UnauthorizedError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an forbidden error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(ForbiddenError error) => new(error);
#else
        public static implicit operator Result<TValue>(ForbiddenError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an not found error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(NotFoundError error) => new(error);
#else
        public static implicit operator Result<TValue>(NotFoundError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an conflict error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(ConflictError error) => new(error);
#else
        public static implicit operator Result<TValue>(ConflictError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an validation error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(ValidationError error) => new(error);
#else
        public static implicit operator Result<TValue>(ValidationError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an unexpected error
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(UnexpectedError error) => new(error);
#else
        public static implicit operator Result<TValue>(UnexpectedError error) => new(error as IError);
#endif

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from a <see cref="Result"/>
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result<TValue>(Result result)
        {
            if(result.IsError)
            {
                return new(result.Errors.ToList());
            }

            return new();
        }
#else
        public static implicit operator Result<TValue>(Result result)
        {
            if(result?.IsError == true)
            {
                return result.Errors.ToList();
            }

            return new List<IError>();
        }
#endif

        /// <summary>
        /// Creates an <see cref="Result"/> from a <see cref="Result{TValue}"/>
        /// </summary>
#if NET6_0_OR_GREATER
        public static implicit operator Result(Result<TValue> result)
        {
            if(result.IsError)
            {
                return result.Errors.ToList();
            }

            return new();
        }
#else
        public static implicit operator Result(Result<TValue> result)
        {
            if(result?.IsError == true)
            {
                return result.Errors.ToList();
            }

            return new List<IError>();
        }
#endif

        /// <summary>
        /// Creates an value from a <see cref="Result{TValue}"/>
        /// </summary>
        public static implicit operator TValue(Result<TValue> result) => result.Value;

        /// <summary>
        /// Creates an <see cref="Result{TValue}"/> from an <see cref="IError"/>
        /// </summary>
        public static Result<TValue> From(IError error) => new(error);
    }
}
