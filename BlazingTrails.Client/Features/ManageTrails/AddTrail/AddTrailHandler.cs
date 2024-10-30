using BlazingTrails.Shared;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.AddTrail;

public class AddTrailHandler(IHttpClientFactory httpClientFactory) :
    IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient(Constants.SecureAPIClient);
        var response = await client.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);

            return new AddTrailRequest.Response(trailId);
        }
        else
        {
            return new AddTrailRequest.Response(-1);
        }
    }
}
