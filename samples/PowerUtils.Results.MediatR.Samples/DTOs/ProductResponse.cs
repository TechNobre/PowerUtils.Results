using System;

namespace PowerUtils.Results.MediatR.Samples.DTOs;

public record ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint Quantity { get; set; }
}
