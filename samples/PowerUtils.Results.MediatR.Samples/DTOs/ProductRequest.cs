using PowerUtils.Results.MediatR.Samples.Services;

namespace PowerUtils.Results.MediatR.Samples.DTOs;

public record ProductRequest
{
    public string Name { get; set; }
    public uint Quantity { get; set; }

    public AddProductCommand ToCommand()
        => new(Name, Quantity);
}
