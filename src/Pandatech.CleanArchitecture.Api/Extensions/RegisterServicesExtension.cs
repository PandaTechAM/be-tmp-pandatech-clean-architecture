using System.Reflection;
using BaseConverter;
using FluentValidation;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;
using Pandatech.Crypto;
using PandaTech.IEnumerableFilters.Extensions;

namespace Pandatech.CleanArchitecture.Api.Extensions;

public static class RegisterServicesExtension
{
    private static readonly Assembly ApplicationLayerAssembly = AppDomain.CurrentDomain.Load("Pandatech.CleanArchitecture.Application");

    public static WebApplicationBuilder RegisterAllServices(this WebApplicationBuilder builder)
    {
        builder.AddCustomFluentValidation()
            .AddPandaStandardServices();

        return builder;
    }

    private static WebApplicationBuilder AddCustomFluentValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(ApplicationLayerAssembly);


        return builder;
    }

    private static WebApplicationBuilder AddPandaStandardServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsLocal())
        {
            builder.Services.AddSingleton<DatabaseHelper>();
        }
        builder.ConfigureBaseConverter(builder.Configuration["Security:Base36Chars"]!);
        builder.ConfigureEncryptedConverter(builder.Configuration["Security:AesKey"]!);
        builder.Services.AddPandatechCryptoAes256(o => o.Key = builder.Configuration["Security:AESKey"]!);
        builder.Services.AddPandatechCryptoArgon2Id();
        return builder;
    }
}
