using WebApi.Extensions;
using Application;
using Infrastructure;
using Microsoft.FeatureManagement;
using PandaVaultClient;
using ResponseCrafter;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog()
    .AddCors()
    .AddHealthChecks()
    .AddResponseCrafter()
    .RegisterPandaVaultEndpoint() //optional
    .RegisterAllCustomServices(); // Move to common

builder.Services
    .AddEndpointsApiExplorer()
    .AddPandaSwaggerGen(builder.Configuration) // Move to common
    .AddCustomFluentValidation()
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddApiVersioningFromHeader()
    .AddFeatureManagement().Services
    .AddControllers();

if (!builder.Environment.IsLocal())
    builder.Configuration.AddPandaVault();

var app = builder.Build();

app.UseResponseCrafter()
    .EnsureHealthy()
    .UseCors()
    .UseStaticFiles();

app.UsePandaSwagger(builder.Configuration);
app.MapPandaStandardEndpoints();


app.Run();