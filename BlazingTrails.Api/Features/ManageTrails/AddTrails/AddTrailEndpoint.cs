using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails.AddTrails;

public class AddTrailEndpoint(BlazingTrailsContext database) : EndpointBaseAsync
    .WithRequest<AddTrailRequest>
    .WithResult<ActionResult<int>>
{
    private readonly BlazingTrailsContext database = database;

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = request.Trail.TimeInMinutes,
            Length = request.Trail.Length
        };

        await database.Trails.AddAsync(trail, cancellationToken);

        var waypoints = request.Trail.Waypoints.Select(x => new Waypoint
        {
            Latitude = x.Latitude,
            Longitude = x.Longitude,
            Trail = trail
        });

        await database.Waypoints.AddRangeAsync(waypoints, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}
