using System;
using System.Collections.Generic;
using System.Linq;
using PowerUtils.Results.Samples.DTOs;
using PowerUtils.Results.Samples.Entities;
using PowerUtils.Results.Samples.Repositories;

namespace PowerUtils.Results.Samples.Services;

public interface IProductsService
{
    public Result<ProductResponse> Get(Guid id);
    public IEnumerable<ProductResponse> List();

    public Result<Guid> Add(ProductRequest request);
    public Result Update(Guid id, ProductRequest request);
    public Result Delete(Guid id);
}

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
        => _repository = repository;

    public Result<ProductResponse> Get(Guid id)
    {
        var product = _repository.Get(id);

        if(product is null)
        {
            return Error.NotFound(nameof(id));
        }

        return new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity
        };
    }

    public IEnumerable<ProductResponse> List()
    {
        var products = _repository.List();

        return products
            .Select(s => new ProductResponse
            {
                Id = s.Id,
                Name = s.Name,
                Quantity = s.Quantity
            });
    }

    public Result<Guid> Add(ProductRequest request)
    {
        var result = new Result<Guid>();
        if(request.Quantity < 1)
        {
            result.AddError(Error.Failure(nameof(request.Quantity), "MIN:1", "The quantity must be greater than 0"));
        }

        if(_repository.Any(request.Name))
        {
            result.AddError(Error.Conflict(nameof(request.Name), "The name is already in use"));
        }

        if(result.IsError)
        {
            return result;
        }


        var product = new Product(request.Name, request.Quantity);

        _repository.Add(product);

        return product.Id;
    }


    public Result Update(Guid id, ProductRequest request)
    {
        var result = new Result();

        var product = _repository.Get(id);

        if(product is null)
        {
            result.AddError(Error.NotFound(nameof(id)));
        }
        else if(_repository.Any(id, request.Name))
        {
            result.AddError(Error.Conflict(nameof(request.Name), "The name is already in use"));
        }

        if(request.Quantity < 1)
        {
            result.AddError(Error.Failure(nameof(request.Quantity), "MIN:1", "The quantity must be greater than 0"));
        }

        if(result.IsError)
        {
            return result;
        }

        product.UpdateName(request.Name);
        product.UpdateQuantity(request.Quantity);

        return Result.Ok();
    }

    public Result Delete(Guid id)
    {
        if(_repository.Any(id))
        {
            _repository.Delete(id);

            return default;
        }


        return Error.NotFound(nameof(id));
    }
}
