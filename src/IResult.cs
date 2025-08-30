using System;
using System.Collections.Generic;

namespace PowerUtils.Results
{
    [Obsolete("This package has been discontinued because it never evolved, and the code present in this package does not justify its continuation. It is preferable to implement this code directly in the project if necessary.")]
    public interface IResult
    {
        /// <summary>
        /// Gets a value indicating whether the state is success
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the state is error
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Gets the list of errors
        /// </summary>
        IEnumerable<IError> Errors { get; }

        void AddError(IError error);
        void AddErrors(IEnumerable<IError> errors);
    }
}
