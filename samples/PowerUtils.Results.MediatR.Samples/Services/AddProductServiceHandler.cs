using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerUtils.Results.MediatR.Samples.DTOs;

namespace PowerUtils.Results.MediatR.Samples.Services;

public class AddProductServiceHandler : IRequestHandler<AddProductCommand, Result<ProductResponse>>
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
