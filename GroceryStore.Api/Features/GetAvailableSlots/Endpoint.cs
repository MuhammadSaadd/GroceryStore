using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GroceryStore.Application.Features.GetAvailableSlots;

namespace GroceryStore.Api.Features.GetAvailableSlots;

/* test case
{
 "orderDate": "2025-06-09T15:35:43.328Z"
*/
public class Endpoint(ISender sender) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpGet("api/slots/available")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSlotsResponse>))]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] DateTime orderDate, [FromQuery] List<Guid> ids)
    {
        var query = new Query { OrderDate = orderDate, Ids = ids };
        
        var slotsResult = await sender.Send(query, CancellationToken);

        return slotsResult.IsFailed ? NotFound(slotsResult.Errors) : Ok(slotsResult.Value);
    }
}