using System;
using System.Collections.Generic;
using System.Linq;
using PowerUtils.Results.Samples.Entities;

namespace PowerUtils.Results.Samples.Repositories;

public interface IProductsRepository
{
    public Product Get(Guid id);
    public IEnumerable<Product> List();
    public bool Any(Guid id);
    public bool Any(string name);
    public bool Any(Guid id, string name);

    public void Add(Product product);
    public void Update(Product product);
    public void Delete(Guid id);
}


public class ProductsRepository : IProductsRepository
{
    private readonly IDictionary<Guid, Product> _database;

    public ProductsRepository()
        => _database = new Dictionary<Guid, Product>();


    public Product Get(Guid id)
        => _database.SingleOrDefault(s => s.Key == id).Value;

    public IEnumerable<Product> List()
        => _database.Values;

    public bool Any(Guid id)
        => _database.ContainsKey(id);

    public bool Any(string name)
        => _database
            .Values
            .Any(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

    public bool Any(Guid id, string name)
        => _database
            .Values
            .Any(a =>
                a.Id == id
                &&
                a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
            );

    public void Add(Product product)
        => _database.Add(product.Id, product);

    public void Delete(Guid id)
        => _database.Remove(id);

    public void Update(Product product)
        => _database[product.Id] = product;
}
