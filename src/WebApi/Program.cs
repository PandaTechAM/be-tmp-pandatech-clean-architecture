using WebApi.Extensions;
using Application;
using Infrastructure;
using Microsoft.FeatureManagement;
using PandaVaultClient;
using ResponseCrafter;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsLocal())
    builder.Configuration.AddPandaVault();

builder.AddSerilog()
    .AddCors()
    .AddHealthChecks()
    .AddResponseCrafter()
    .RegisterPandaVaultEndpoint() //optional
    .RegisterAllServices(); // Move to common

builder.Services
    .AddEndpointsApiExplorer()
    .AddPandaSwaggerGen(builder.Configuration) // Move to common
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddApiVersioningFromHeader()
    .AddFeatureManagement().Services
    .AddControllers();


var app = builder.Build();

app.UseResponseCrafter()
    .EnsureHealthy()
    .UseCors()
    .UseStaticFiles();

app.UsePandaSwagger(builder.Configuration);
app.MapPandaStandardEndpoints();


app.Run();