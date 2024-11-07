using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingTrails.Api.Persistence.Entities;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Authorization;
using BlazingTrails.Shared;

namespace BlazingTrails.Api.Features.ManageTrails.EditTrails;

public class EditTrailEndpoint(BlazingTrailsContext context) : EndpointBaseAsync
    .WithRequest<EditTrailRequest>
    .WithResult<ActionResult<bool>>
{
    private readonly BlazingTrailsContext context = context;

    [Authorize]
    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(
        EditTrailRequest request,
        CancellationToken cancellationToken = default)
    {
        var trail = await context.Trails
            .Include(x => x.Waypoints)
            .SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken);

        if (trail is null)
            return BadRequest("Trail could not be found.");

        if(!trail.Owner.Equals(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value, StringComparison.OrdinalIgnoreCase)
            && !HttpContext.User.IsInRole(Constants.Roles.Admin))
            return Unauthorized();

        trail.Name = request.Trail.Name;
        trail.Description = request.Trail.Description;
        trail.Location = request.Trail.Location;
        trail.TimeInMinutes = request.Trail.TimeInMinutes;
        trail.Length = request.Trail.Length;
        trail.Waypoints = request.Trail.Waypoints.Select(w => new Waypoint
        {
            Latitude = w.Latitude,
            Longitude = w.Longitude,
        }).ToList();

        if (request.Trail.ImageAction == ImageAction.Remove)
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", trail.Image!));
            trail.Image = null;
        }

        await context.SaveChangesAsync(true, cancellationToken);

        return Ok(true);
    }
}
