using MediatR;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using System.Net.Http.Json;
using BlazingTrails.Shared;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class EditTrailHandler(IHttpClientFactory httpClientFactory) :
    IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient(Constants.SecureAPIClient);

        var response = await client.PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

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
