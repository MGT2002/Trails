using BlazingTrails.Shared;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Test;

#if DEBUG

public class TestHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<TestRequest, string>
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<string> Handle(TestRequest request, CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient(Constants.SecureAPIClient);
        string response = await client.GetStringAsync(TestRequest.Url, cancellationToken);

        return response;
    }
}

public class TestRequest : IRequest<string>
{
    public const string Url = "/api/test/getclaims";

    string data;
}

#endif
