using Carter;
using Pandatech.CleanArchitecture.Api.Endpoints.SharedEndpoints;
using Pandatech.CleanArchitecture.Api.Extensions;
using Pandatech.CleanArchitecture.Infrastructure;
using Pandatech.CleanArchitecture.Application;
using Pandatech.CleanArchitecture.Core.Extensions;
using PandaVaultClient;
using ResponseCrafter;

var builder = WebApplication.CreateBuilder(args);
builder.LogStartAttempt();

if (!builder.Environment.IsLocal())
   builder.Configuration.AddPandaVault();

builder
   .AddCors()
   .AddResponseCrafter()
   .AddInfrastructureLayer()
   .AddApplicationLayer()
   .AddSwagger()
   .RegisterAllServices(); // Move to common

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseStaticFiles();
app.UseResponseCrafter()
   .UserInfrastructureLayer()
   .UseCors()
   .UseSwagger(builder.Configuration);

app.MapPandaEndpoints();
app.MapCarter();

StartupLogger.LogStartSuccess();
app.Run();
