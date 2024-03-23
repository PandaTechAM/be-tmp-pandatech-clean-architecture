using Carter;
using Pandatech.CleanArchitecture.Api.Endpoints.SharedEndpoints;
using Pandatech.CleanArchitecture.Api.Extensions;
using Pandatech.CleanArchitecture.Infrastructure;
using Pandatech.CleanArchitecture.Application;
using Pandatech.CleanArchitecture.Core;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Core.Helpers;
using PandaVaultClient;
using ResponseCrafter;

var builder = WebApplication.CreateBuilder(args);
builder.LogStartAttempt();
AssemblyRegistry.AddAssemblies(typeof(Program).Assembly);

if (!builder.Environment.IsLocal())
   builder.Configuration.AddPandaVault();

builder
   .AddCors()
   .AddResponseCrafter()
   .AddCoreLayer()
   .AddInfrastructureLayer()
   .AddApplicationLayer()
   .AddSwagger()
   .AddMediatrWithBehaviors()
   .RegisterAllServices();

builder.Services.AddCarter();
builder.Services.AddHttpContextAccessor();

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
