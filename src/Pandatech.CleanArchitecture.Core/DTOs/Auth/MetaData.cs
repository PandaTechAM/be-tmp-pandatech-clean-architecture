using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Core.DTOs.Auth;

public class MetaData
{
   public string RequestId { get; set; } = null!;
   public DateTime RequestTime { get; set; }
   public SupportedLanguageType LanguageId { get; set; }
   public ClientType ClientType { get; set; }
}
