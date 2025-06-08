using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GroceryStore.Application.Features.GetAvailableSlots;

namespace GroceryStore.Api.Features.GetAvailableSlots;

public class Endpoint(ISender sender) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("api/available_slots")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSlotsResponse>))]
    public async Task<IActionResult> GetAvailableSlots([FromBody] Query query)
    {
        var slotsResult = await sender.Send(query, CancellationToken);

        return slotsResult.IsFailed ? NotFound(slotsResult.Errors) : Ok(slotsResult.Value);
    }
}