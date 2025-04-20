using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Queries.GetPagedProductsQuery;
using RO.DevTest.Application.Features.Queries.GetProductByIdQuery;
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
        var response = ApiResponse<PageResult<GetPagedProductsResult>>.FromSuccess(result);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id) {
        GetProductByIdQuery query = new(id);
        GetProductByIdResult result = await _mediator.Send(query);
        return Ok(ApiResponse<GetProductByIdResult>.FromSuccess(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductById(Guid id) {
        DeleteProductCommand command = new(id);
        DeleteProductResult result = await _mediator.Send(command);
        return Ok(ApiResponse<DeleteProductResult>.FromSuccess(result));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request) {
       CreateProductResult result = await _mediator.Send(request);
       var response = ApiResponse<CreateProductResult>.FromSuccess(result, (int) HttpStatusCode.Created);
       return Created(HttpContext.Request.GetDisplayUrl(), response); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductCommand request) {
        request.ProductId = id;
        var result = await _mediator.Send(request);
        return Ok(ApiResponse<UpdateProductResult>.FromSuccess(result));
    }
}