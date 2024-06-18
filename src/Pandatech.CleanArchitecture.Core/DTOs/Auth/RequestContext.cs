using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Core.DTOs.Auth;

public class RequestContext : IRequestContext
{
   public Identity Identity { get; set; } = new();
   public MetaData MetaData { get; set; } = new();
   public bool IsAuthenticated { get; set; } = false;
}
