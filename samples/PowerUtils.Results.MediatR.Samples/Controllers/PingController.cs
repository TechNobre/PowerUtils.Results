using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PowerUtils.Results.MediatR.Samples.Services;

namespace PowerUtils.Results.MediatR.Samples.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    private readonly IMediator _mediator;

    public PingController(IMediator mediator)
        => _mediator = mediator;


    [HttpPost]
    public async Task<IActionResult> Ping(
        [FromQuery] string message,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediator.Send(new PingCommand(message), cancellationToken));
}
