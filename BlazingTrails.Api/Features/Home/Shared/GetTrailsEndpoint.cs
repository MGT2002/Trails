using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.Home.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.Home.Shared;

public class GetTrailsEndpoint(BlazingTrailsContext context) : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<GetTrailsRequest.Response>
{
    private readonly BlazingTrailsContext context = context;

    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailsRequest.Response>> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var trails = await context.Trails
            .Include(x => x.Waypoints)
            .ToListAsync(cancellationToken);

        var response = new GetTrailsRequest.Response(trails.Select(trail => new GetTrailsRequest.Trail(
            trail.Id,
            trail.Name,
            trail.Image,
            trail.Location,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description
            )));

        return Ok(response);
    }
}
