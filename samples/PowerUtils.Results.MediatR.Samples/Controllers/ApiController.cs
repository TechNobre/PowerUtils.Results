using System;
using Microsoft.AspNetCore.Mvc;

namespace PowerUtils.Results.MediatR.Samples.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult MapErrors(IResult result)
        => MapError(result.FirstError());

    protected IActionResult MapError(IError error)
    {
        if(error is Error)
        {
            return BadRequest(error);
        }

        if(error is UnauthorizedError)
        {
            return Unauthorized(error);
        }

        if(error is ForbiddenError)
        {
            return BadRequest(error);
        }

        if(error is NotFoundError)
        {
            return NotFound(error);
        }

        if(error is ConflictError)
        {
            return Conflict(error);
        }

        if(error is ValidationError)
        {
            return BadRequest(error);
        }

        throw new InvalidOperationException();
    }
}
