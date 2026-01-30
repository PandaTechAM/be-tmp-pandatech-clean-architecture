using FileExporter.Extensions;
using FluentMinimalApiMapper;
using Pandatech.CleanArchitecture.Application;
using Pandatech.CleanArchitecture.Core;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Infrastructure;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using Pandatech.Crypto.Extensions;
using ResponseCrafter.Enums;
using ResponseCrafter.Extensions;
using SharedKernel.Extensions;
using SharedKernel.Helpers;
using SharedKernel.Logging;
using SharedKernel.Logging.Middleware;
using SharedKernel.OpenApi;
using SharedKernel.ValidatorAndMediatR;

var builder = WebApplication.CreateBuilder(args);
builder.LogStartAttempt();
AssemblyRegistry.Add(typeof(Program).Assembly);

builder.WebHost.UseKestrel(o => o.AddServerHeader = false);

builder
   .ConfigureWithPandaVault()
   .AddOutboundLoggingHandler()
   .AddResponseCrafter(NamingConvention.ToSnakeCase)
   .AddOpenApi()
   .AddMinimalApis(AssemblyRegistry.ToArray())
   .AddControllers(AssemblyRegistry.ToArray())
   .MapDefaultTimeZone()
   .AddCors()
   .AddAes256Key(builder.Configuration.GetAesKey())
   .AddCoreLayer()
   .AddApplicationLayer()
   .AddInfrastructureLayer()
   .AddMediatrWithBehaviors(AssemblyRegistry.ToArray())
   .AddFileExporter(AssemblyRegistry.ToArray());

builder.Services.AddScoped<IRequestContext, RequestContext>();


var app = builder.Build();

var groupPolicy = app.MapGroup("")
                     .DisableAntiforgery();


app
   .UseRequestLogging()
   .UseResponseCrafter()
   .UseCors()
   .MapMinimalApis(groupPolicy)
   .MapHealthCheckEndpoints()
   .MapPrometheusExporterEndpoints()
   .UseOpenApi()
   .MapInfrastructureLayer()
   .ClearAssemblyRegistry()
   .MapControllers();

app.LogStartSuccess();
app.Run();