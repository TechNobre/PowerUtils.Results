using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerUtils.Results.MediatR.Samples.DTOs;
using PowerUtils.Results.MediatR.Samples.Repositories;

namespace PowerUtils.Results.MediatR.Samples.Services;

public record GetProductQuery(Guid Id) : IRequest<Result<ProductResponse>>
{
    public class Handler : IRequestHandler<GetProductQuery, Result<ProductResponse>>
    {
        private readonly IProductsRepository _repository;
        public Handler(IProductsRepository repository)
            => _repository = repository;


        public async Task<Result<ProductResponse>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _repository.Get(query.Id, cancellationToken);
            if(product is null)
            {
                return Error.NotFound(
                    nameof(query.Id),
                    "NOT_FOUND",
                    $"The product with Id: '{query.Id}' was not found");
            }

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
            };
        }
    }
}
