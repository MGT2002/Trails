using AutoFixture;
using BlazingTrails.Shared.Features.Home.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingTrails.Tests.Client.Features.Home;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)

    {
        var fixture = new Fixture();
        var dummyTrails = fixture.CreateMany<GetTrailsRequest.Trail>();

        return new GetTrailsRequest.Response(dummyTrails);
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
