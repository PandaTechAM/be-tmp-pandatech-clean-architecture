namespace Pandatech.CleanArchitecture.Api.Configurations.Options;

public class SwaggerOptions
{
   public required Dictionary<string, SwaggerVersionOptions> Versions { get; set; }
}