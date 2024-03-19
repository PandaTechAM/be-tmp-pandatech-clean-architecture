using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Core.DTOs.Auth;

public class RequestContext : IRequestContext
{
   public Identity Identity { get; set; } = null!;
   public MetaData MetaData { get; set; } = null!;
   public bool IsAuthenticated { get; set; } = false;
}
