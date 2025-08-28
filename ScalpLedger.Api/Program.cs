using Scalar.AspNetCore;
using ScalpLedger.Api.Endpoints;
using ScalpLedger.Application;
using ScalpLedger.Infrastructure;
using ScalpLedger.Infrastructure.Clients.Sockets.ClientPerUser;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Docker");
ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

builder.Services.AddOpenApi();

builder.Services.Configure<HttpClientFactorySettings>(builder.Configuration.GetSection("PerClientSettings"));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionString);

builder.Services.AddHeaderPropagation(
    opt => { opt.Headers.Add("TL-TraceId"); }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapMarketsEndpoints();

app.Run();