using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerUtils.Results.MediatR.Samples.DTOs;

namespace PowerUtils.Results.MediatR.Samples.Services;

public record GetProductQuery(Guid Id) : IRequest<Result<ProductResponse>>
{
    public class Handler : IRequestHandler<GetProductQuery, Result<ProductResponse>>
    {
        public async Task<Result<ProductResponse>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            if(Random.Shared.Next(2) % 2 == 0)
            {
                return Error.NotFound(
                    nameof(query.Id),
                    "NOT_FOUND",
                    $"The product with Id: '{query.Id}' was not found"
                );
            }

            return new ProductResponse
            {
                Id = query.Id,
                Name = "Fake name",
                Quantity = 45646
            };
        }
    }
}
