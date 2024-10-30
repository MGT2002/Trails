﻿using MediatR;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using BlazingTrails.Shared;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class UploadTrailImageHandler(IHttpClientFactory httpClientFactory) :
    IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
    {
        var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

        using var content = new MultipartFormDataContent
        {
            { new StreamContent(fileContent), "image", request.File.Name }
        };

        var client = httpClientFactory.CreateClient(Constants.SecureAPIClient);
        var response = await client.PostAsync(UploadTrailImageRequest.RouteTemplate.
            Replace("{trailId}", request.TrailId.ToString()),
            content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var fileName = await response.Content.ReadAsStringAsync(cancellationToken);

            return new UploadTrailImageRequest.Response(fileName);
        }
        else
        {
            return new UploadTrailImageRequest.Response("");
        }
    }
}
