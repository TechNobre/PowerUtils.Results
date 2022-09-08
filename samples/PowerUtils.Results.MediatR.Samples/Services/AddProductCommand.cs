using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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


    public class Handler : IRequestHandler<AddProductCommand, Result<ProductResponse>>
    {
        public async Task<Result<ProductResponse>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            if(Random.Shared.Next(2) % 2 == 0)
            {
                return Error.Conflict(
                    nameof(command.Name),
                    "DUPLICATED",
                    $"Already exists a product with name '{command.Name}'"
                );
            }

            return new ProductResponse
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Quantity = command.Quantity,
            };
        }
    }
}
