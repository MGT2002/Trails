using BlazingTrails.Shared;
using BlazingTrails.Client;
using BlazingTrails.Client.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazingTrails.Client.Features.Auth;
using BlazingTrails.Client.State;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Constants.SecureAPIClient, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddTrailHandler).Assembly));

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    //options.UserOptions.NameClaim = "name";

    options.ProviderOptions.AdditionalProviderParameters.Add("audience", "https://blazingtrailsapi.com");//for jwt proper decryption
}).AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();