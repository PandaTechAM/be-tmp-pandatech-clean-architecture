using FluentMinimalApiMapper;
using Pandatech.CleanArchitecture.Api.Endpoints.SharedEndpoints;
using Pandatech.CleanArchitecture.Api.Extensions;
using Pandatech.CleanArchitecture.Application;
using Pandatech.CleanArchitecture.Core;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Infrastructure;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using PandaVaultClient;
using ResponseCrafter.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.LogStartAttempt();
AssemblyRegistry.AddAssemblies(typeof(Program).Assembly);

if (!builder.Environment.IsLocal())
{
   builder.Configuration.AddPandaVault();
}

builder
   .AddCors()
   .AddResponseCrafter()
   .AddCoreLayer()
   .AddInfrastructureLayer()
   .AddApplicationLayer()
   .AddSwagger()
   .AddMassTransit(AssemblyRegistry.GetAllAssemblies().ToArray())
   .AddMediatrWithBehaviors()
   .AddEndpoints()
   .RegisterAllServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseStaticFiles();
app.UseResponseCrafter()
   .UserInfrastructureLayer()
   .UseCors()
   .UseSwagger(builder.Configuration);

app.MapPandaEndpoints();
app.MapEndpoints();

AssemblyRegistry.RemoveAllAssemblies();
StartupLogger.LogStartSuccess();
app.Run();
