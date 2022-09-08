using System;
using Microsoft.AspNetCore.Mvc;

namespace PowerUtils.Results.MediatR.Samples.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult MapErrors(IResult result)
    {
        var baseError = result.FirstError();

        if(baseError is Error)
        {
            return BadRequest(result.Errors);
        }

        if(baseError is UnauthorizedError)
        {
            return Unauthorized(result.Errors);
        }

        if(baseError is ForbiddenError)
        {
            return BadRequest(result.Errors);
        }

        if(baseError is NotFoundError)
        {
            return NotFound(result.Errors);
        }

        if(baseError is ConflictError)
        {
            return Conflict(result.Errors);
        }

        if(baseError is ValidationError)
        {
            return BadRequest(result.Errors);
        }

        throw new InvalidOperationException();
    }
}
