namespace Pandatech.CleanArchitecture.Infrastructure.Helpers;

public static class ConfigurationPaths
{
   public const string Base36Chars = "Security:Base36Chars";
   public const string AesKey = "Security:AesKey";
   public const string ElasticSearchUrl = "ElasticSearch";
   public const string ElasticIndex = "ElasticIndexName";
   public const string RedisUrl = "Redis";
   public const string RabbitMqUrl = "RabbitMq";
   public const string PostgresUrl = "Postgres";
   public const string CorsOrigins = "CorsSettings:AllowedOrigins";
   public const string HangfireUsername = "Security:Hangfire:Username";
   public const string HangfirePassword = "Security:Hangfire:Password";
   public const string SuperUsername = "Security:SuperUser:Username";
   public const string SuperUserPassword = "Security:SuperUser:Password";
}
