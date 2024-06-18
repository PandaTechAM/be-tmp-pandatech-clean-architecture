namespace Pandatech.CleanArchitecture.Core.DTOs.Auth;

public class Cookie(string key, string value, DateTime expirationDate)
{
   public string Key { get; set; } = key;
   public string Value { get; set; } = value;
   public DateTime? ExpirationDate { get; set; } = expirationDate;
}
