using BaseConverter;
using BaseConverter.Extensions;
using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;
using Pandatech.Crypto;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class CryptoExtensions
{
   public static WebApplicationBuilder AddPandaCrypto(this WebApplicationBuilder builder)
   {
      builder.ConfigureBaseConverter(builder.Configuration[ConfigurationPaths.Base36Chars]!);
      builder.Services.AddPandatechCryptoAes256(o => o.Key = builder.Configuration[ConfigurationPaths.AesKey]!);
      builder.Services.AddPandatechCryptoArgon2Id();

      return builder;
   }
}
