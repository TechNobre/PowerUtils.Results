using System.Collections.Generic;
using MediatR;
using PowerUtils.Results.MediatR.Samples.Behaviors;
using PowerUtils.Results.MediatR.Samples.DTOs;

namespace PowerUtils.Results.MediatR.Samples.Services;

public record AddProductCommand(string Name, uint Quantity) : IRequest<Result<ProductResponse>>, IValidation
{
    public List<IError> Validate()
    {
        var errors = new List<IError>();

        if(string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(Error.Validation(
               nameof(Name),
               "REQUIRED",
               "The name is required"
            ));
        }

        if(Quantity < 1)
        {
            errors.Add(Error.Validation(
               nameof(Name),
               "MIN:1",
               "The minimum quantity is 1"
            ));
        }

        return errors;
    }
}
