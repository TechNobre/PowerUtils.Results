using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerUtils.Results.MediatR.Samples.DTOs;
using PowerUtils.Results.MediatR.Samples.Services;

namespace PowerUtils.Results.MediatR.Samples.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
        => _mediator = mediator;



    [HttpPost]
    public async Task<IActionResult> Add(ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.ToCommand(), cancellationToken);

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductQuery(id), cancellationToken);

        return result.MatchFirst<ProductResponse, IActionResult>(
            value => Ok(value),
            error => NotFound(error)
        );
    }
}
