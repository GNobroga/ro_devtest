using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Queries.GetPagedProductsQuery;
using RO.DevTest.Application.Features.Queries.GetProductByIdQuery;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/product")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase {

    private readonly IMediator _mediator = mediator;

    private static readonly string[] SearchFields = [
        "Price",
        "Name",
        "Description"
    ];

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PageResult<GetPagedProductsResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts([FromQuery] PagedFilter filter) {
        GetPagedProductsQuery query = new(filter, SearchFields);
        PageResult<GetPagedProductsResult> result = await _mediator.Send(query);
        var response = ApiResponse<PageResult<GetPagedProductsResult>>.FromSuccess(result);
        return Ok(response);
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetProductByIdResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid id) {
        GetProductByIdQuery query = new(id);
        GetProductByIdResult result = await _mediator.Send(query);
        return Ok(ApiResponse<GetProductByIdResult>.FromSuccess(result));
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DeleteProductResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProductById(Guid id) {
        Console.WriteLine(User);
        DeleteProductCommand command = new(id);
        DeleteProductResult result = await _mediator.Send(command);
        return Ok(ApiResponse<DeleteProductResult>.FromSuccess(result));
    }
    
   // [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request) {
       CreateProductResult result = await _mediator.Send(request);
       var response = ApiResponse<CreateProductResult>.FromSuccess(result, (int) HttpStatusCode.Created);
       var productId = response?.Data!.Id;
       return CreatedAtAction(nameof(GetProductById), new { id = productId }, response); 
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductCommand request) {
        request.ProductId = id;
        var result = await _mediator.Send(request);
        return Ok(ApiResponse<UpdateProductResult>.FromSuccess(result));
    }
}