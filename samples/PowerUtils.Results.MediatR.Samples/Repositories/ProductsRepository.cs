using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PowerUtils.Results.MediatR.Samples.Entities;

namespace PowerUtils.Results.MediatR.Samples.Repositories;

public interface IProductsRepository
{
    public Task<Product> Get(Guid id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Product>> List(CancellationToken cancellationToken = default);
    public Task<bool> Any(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> Any(string name, CancellationToken cancellationToken = default);
    public Task<bool> Any(Guid id, string name, CancellationToken cancellationToken = default);

    public Task Add(Product product, CancellationToken cancellationToken = default);
    public Task Update(Product product, CancellationToken cancellationToken = default);
    public Task Delete(Guid id, CancellationToken cancellationToken = default);
}


public class ProductsRepository : IProductsRepository
{
    private readonly IDictionary<Guid, Product> _database;

    public ProductsRepository()
        => _database = new Dictionary<Guid, Product>();


    public Task<Product> Get(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_database.SingleOrDefault(s => s.Key == id).Value);

    public Task<IEnumerable<Product>> List(CancellationToken cancellationToken = default)
        => Task.FromResult(_database.Values as IEnumerable<Product>);

    public Task<bool> Any(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_database.ContainsKey(id));

    public Task<bool> Any(string name, CancellationToken cancellationToken = default)
    {
        var result = _database
            .Values
            .Any(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        return Task.FromResult(result);
    }

    public Task<bool> Any(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var result = _database
            .Values
            .Any(a =>
                a.Id == id
                &&
                a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        return Task.FromResult(result);
    }

    public Task Add(Product product, CancellationToken cancellationToken = default)
    {
        _database.Add(product.Id, product);

        return Task.CompletedTask;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        _database.Remove(id);

        return Task.CompletedTask;
    }

    public Task Update(Product product, CancellationToken cancellationToken = default)
    {
        _database[product.Id] = product;

        return Task.CompletedTask;
    }
}
