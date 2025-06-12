using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GroceryStore.Application.Features.GetAvailableSlots;

namespace GroceryStore.Api.Features.GetAvailableSlots;

/* test case
{
 "orderDate": "2025-06-09T15:35:43.328Z",
 "ids": [
   "617d666f-bef4-4c33-bf60-dc8e6d4a321c",
   "b99798cb-c13b-4db4-ad99-dfc9b7f849d7",
   "d28c8f16-84a0-4247-8fd8-0b39a3d2fdbe",
   "f06d0e2b-4f00-412d-9467-c00189f8df59"
 ]
}
*/
public class Endpoint(ISender sender) : ControllerBase
{
    private CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("api/slots/available")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSlotsResponse>))]
    public async Task<IActionResult> GetAvailableSlots([FromBody] Query query)
    {
        var slotsResult = await sender.Send(query, CancellationToken);

        return slotsResult.IsFailed ? NotFound(slotsResult.Errors) : Ok(slotsResult.Value);
    }
}