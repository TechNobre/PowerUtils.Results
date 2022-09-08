using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerUtils.Results.MediatR.Samples.DTOs;

namespace PowerUtils.Results.MediatR.Samples.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
        => _mediator = mediator;



    [HttpPost]
    public async Task<IActionResult> Add(ProductRequest request)
    {
        var result = await _mediator.Send(request.ToCommand());

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return Ok(result.Value);
    }
}
