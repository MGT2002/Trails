using FluentValidation;
using BlazingTrails.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using BlazingTrails.Shared.Features.ManageTrails.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlazingTrailsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(nameof(BlazingTrailsContext)))
);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(typeof(TrailDto).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
