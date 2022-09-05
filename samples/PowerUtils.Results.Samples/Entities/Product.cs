using System;

namespace PowerUtils.Results.Samples.Entities;

public class Product
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public uint Quantity { get; private set; }


    public Product(string name, uint quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Quantity = quantity;
    }

    public void UpdateName(string name)
        => Name = name;

    public void UpdateQuantity(uint quantity)
        => Quantity = quantity;
}
