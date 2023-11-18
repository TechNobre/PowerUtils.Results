using System;

namespace PowerUtils.Results.MediatR.Samples.Entities;

public class Product(string name, uint quantity)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public uint Quantity { get; private set; } = quantity;

    public void UpdateName(string name)
        => Name = name;

    public void UpdateQuantity(uint quantity)
        => Quantity = quantity;
}
