using BlazingTrails.Shared;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class GetTrailHandler(IHttpClientFactory httpClientFactory) :
	IRequestHandler<GetTrailRequest, GetTrailRequest.Response?>
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<GetTrailRequest.Response?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var client  = httpClientFactory.CreateClient(Constants.SecureAPIClient);

			return await client.GetFromJsonAsync<GetTrailRequest.Response>(
				GetTrailRequest.RouteTemplate.Replace(("{trailId}"), request.TrailId.ToString()));
		}
		catch (HttpRequestException)
		{
			return default!;
		}
	}
}
