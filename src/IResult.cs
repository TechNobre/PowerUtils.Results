using System.Collections.Generic;

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
}
