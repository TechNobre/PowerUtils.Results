using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerUtils.Results.Samples.DTOs;
using PowerUtils.Results.Samples.Services;

namespace PowerUtils.Results.Samples.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductsService service) : ApiController
{
    private readonly IProductsService _service = service;



    [HttpGet("{id}", Name = nameof(Get))]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public IActionResult Get(Guid id)
    {
        var result = _service.Get(id);

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public IActionResult List()
        => Ok(_service.List());

    [HttpPost]
    public IActionResult Add(ProductRequest request)
    {
        var result = _service.Add(request);

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return new CreatedAtRouteResult(
           nameof(Get),
           new { id = result.Value },
           result.Value);
    }

    [HttpPut("{id}")]
    public IActionResult Update(
        Guid id,
        ProductRequest request)
    {
        var result = _service.Update(id, request);

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _service.Delete(id);

        if(result.IsError)
        {
            return MapErrors(result);
        }

        return NoContent();
    }
}
