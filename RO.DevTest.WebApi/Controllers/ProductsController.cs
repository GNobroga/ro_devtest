using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Queries.GetPagedProductsQuery;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/product")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase {

    private readonly IMediator _mediator = mediator;

    private readonly string[] searchFields = [
        "Price",
        "Name",
        "Description"
    ];


    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] PagedFilter filter) {
        GetPagedProductsQuery query = new(filter, searchFields);
        PageResult<GetPagedProductsResult> result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request) {
       CreateProductResult result = await _mediator.Send(request);
       return Created(HttpContext.Request.GetDisplayUrl(), result); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductCommand request) {
        request.ProductId = id;
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}