using MediatR;
using BlazingTrails.Shared.Features.ManageTrails;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class UploadTrailImageHandler(HttpClient httpClient) :
    IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
    {
        var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

        using var content = new MultipartFormDataContent
        {
            { new StreamContent(fileContent), "image", request.File.Name }
        };

        var response = await httpClient.PostAsync(UploadTrailImageRequest.RouteTemplate.
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
