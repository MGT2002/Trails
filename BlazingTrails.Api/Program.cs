using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
