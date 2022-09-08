using System.Collections.Generic;

namespace PowerUtils.Results.MediatR.Samples.Behaviors;

public interface IValidation
{
    List<IError> Validate();
}
