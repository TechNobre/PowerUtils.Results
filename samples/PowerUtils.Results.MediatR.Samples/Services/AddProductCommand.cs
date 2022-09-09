using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PowerUtils.Results.MediatR.Samples.DTOs;
using PowerUtils.Results.MediatR.Samples.Entities;
using PowerUtils.Results.MediatR.Samples.Repositories;
using PowerUtils.Results.MediatR.Samples.Validations;

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
        private readonly IProductsRepository _repository;
        public Handler(IProductsRepository repository)
            => _repository = repository;


        public async Task<Result<ProductResponse>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            if(await _repository.Any(command.Name, cancellationToken))
            {
                return Error.Conflict(
                    nameof(command.Name),
                    "DUPLICATED",
                    $"Already exists a product with name '{command.Name}'"
                );
            }


            var product = new Product(command.Name, command.Quantity);
            await _repository.Add(product, cancellationToken);


            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
            };
        }
    }
}
