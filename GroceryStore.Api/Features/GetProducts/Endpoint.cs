using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GroceryStore.Application.Features.GetProducts;

namespace GroceryStore.Api.Features.GetProducts;


public class Endpoint(ISender sender) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet("api/products")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductResponse>))]
    public async Task<IActionResult> GetProducts()
    {
        var products = await sender.Send(new Query(), CancellationToken);

        return products.IsFailed ? BadRequest(products.Errors) : Ok(products.Value);
    }
}