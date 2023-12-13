using System.Reflection;
using BaseConverter;
using FluentValidation;
using Infrastructure.Helpers;
using Pandatech.Crypto;
using PandaTech.IEnumerableFilters.Extensions;
using WebApi.Attributes;

namespace WebApi.Extensions;

public static class RegisterServicesExtension
{
    private static readonly Assembly ApplicationLayerAssembly = AppDomain.CurrentDomain.Load("Application");

    public static WebApplicationBuilder RegisterAllServices(this WebApplicationBuilder builder)
    {
        builder.AddCustomFluentValidation()
            .AddPandaStandardServices();

        return builder;
    }

    private static WebApplicationBuilder AddCustomFluentValidation(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(ApplicationLayerAssembly);
        builder.Services.AddMvc(options => { options.Filters.Add(typeof(ValidatorModelFilterAttribute)); });

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