using BlazingTrails.Shared.Features.Home.Shared;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.Home.Shared;

public class GetTrailsHandler(HttpClient httpClient) : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response?>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<GetTrailsRequest.Response?> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
		try
		{
			return await httpClient.GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);
		}
		catch (HttpRequestException)
		{
			return default!;
		}
    }
}
