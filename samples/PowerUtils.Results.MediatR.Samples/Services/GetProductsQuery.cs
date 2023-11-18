using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerUtils.Results.MediatR.Samples.DTOs;
using PowerUtils.Results.MediatR.Samples.Repositories;

namespace PowerUtils.Results.MediatR.Samples.Services;

public record GetProductsQuery() : IRequest<Result<IQueryable<ProductResponse>>>
{
    public class Handler(IProductsRepository repository) : IRequestHandler<GetProductsQuery, Result<IQueryable<ProductResponse>>>
    {
        private readonly IProductsRepository _repository = repository;


        public async Task<Result<IQueryable<ProductResponse>>> Handle(GetProductsQuery _, CancellationToken cancellationToken)
        {
            var products = (await _repository
                .List(cancellationToken))
                .Select(s => new ProductResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Quantity = s.Quantity
                }).AsQueryable();

            return Result.Success();
        }
    }
}
