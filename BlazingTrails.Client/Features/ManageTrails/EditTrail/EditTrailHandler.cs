using MediatR;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class EditTrailHandler(HttpClient httpClient) :
    IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return new EditTrailRequest.Response(true);
        }
        else
        {
            return new EditTrailRequest.Response(false);
        }
    }
}
